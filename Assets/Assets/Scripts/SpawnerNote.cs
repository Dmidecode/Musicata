using Assets.Scripts;
using Assets.Scripts.Notes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SpawnerNote : MonoBehaviour
{
  protected float[] positionNoteToCm;
  protected const float ton = 0.5f;
  protected float espacement = 2.2f;
  protected const float gammeTon = 3.5f;
  protected const float positionGameNormal = -5f;
  protected const float positionHampeY = 1.651f;
  protected const float positionHampeX = 0.442f;
  protected const float hauteurHampeY = 3.4f;

  protected readonly Vector3 positionHampeSuperieur = new Vector3(positionHampeX, positionHampeY, 0);
  protected readonly Vector3 positionHampeInferieur = new Vector3(-positionHampeX, -positionHampeY, 0);

  protected readonly Vector3 positionHampeCrocheSuperieur = new Vector3(0.725f, 1.75f, 0);
  protected readonly Vector3 positionHampeCrocheInferieur = new Vector3(-0.156f, -1.75f, 0);

  protected TypeGamme TypeGamme;

  protected virtual void Awake()
  {
    positionNoteToCm = new float[8];
    float position = 2f;
    for (int i = 0; i < 8; i++)
    {
      positionNoteToCm[i] = position;
      position += espacement;
    }
  }

  public abstract void Do(GameObject note, Vector3 startPositionMesure, int positionDansMesure);
  public abstract void Re(GameObject note, Vector3 startPositionMesure, int positionDansMesure);
  public abstract void Mi(GameObject note, Vector3 startPositionMesure, int positionDansMesure);
  public abstract void Fa(GameObject note, Vector3 startPositionMesure, int positionDansMesure);
  public abstract void Sol(GameObject note, Vector3 startPositionMesure, int positionDansMesure);
  public abstract void La(GameObject note, Vector3 startPositionMesure, int positionDansMesure);
  public abstract void Si(GameObject note, Vector3 startPositionMesure, int positionDansMesure);

  protected abstract void AjusteHampe(GameObject hampe, GameObject hampeCroche, TypeNote typeNote);

  protected bool IsHampeInferieur(TypeNote typeNote)
  {
    return TypeGamme.IsHampeInferieur(typeNote);
  }

  public void DeleteLastNote(ManageMesure mesure)
  {
    if (!mesure.Notes.Any()) return;

    var note = mesure.Notes.LastOrDefault();
    Destroy(note.Sprite);

    mesure.Notes.Remove(note);
    if (note.TypeCadenceNote == TypeCadenceNote.Croche)
    {
      var noteSuivante = mesure.GetNotes().LastOrDefault();
      if (noteSuivante?.TypeCadenceNote == TypeCadenceNote.Croche)
      {
        DeleteLastNote(mesure);

        SpawnerNote spawner = this;
        switch (noteSuivante.TypeGamme)
        {
          case TypeGamme.Basse:
            spawner = Compositeur.Instance.GammeBasse;
            break;
          case TypeGamme.Haute:
            spawner = Compositeur.Instance.GammeHaute;
            break;
        }

        spawner.AddNote(noteSuivante.TypeNote, noteSuivante.TypeCadenceNote, noteSuivante.IsPointe, mesure);
      }

      LinkCroches(mesure.GetNotes());
    }
  }

  public void AddNote(TypeNote typeNote, TypeCadenceNote typeCadenceNote, bool isPointe, ManageMesure mesure)
  {
    GameObject noteResource = null;
    switch (typeCadenceNote)
    {
      case TypeCadenceNote.Noire:
      case TypeCadenceNote.Croche:
        noteResource = Resources.Load("Prefab/Notes/Noire") as GameObject;
        break;
      case TypeCadenceNote.Blanche:
      case TypeCadenceNote.Ronde:
        noteResource = Resources.Load("Prefab/Notes/Ronde") as GameObject;
        break;
      case TypeCadenceNote.DemiSoupir:
        noteResource = Resources.Load("Prefab/Silences/DemiSoupir") as GameObject;
        break;
      case TypeCadenceNote.Soupir:
        noteResource = Resources.Load("Prefab/Silences/Soupir") as GameObject;
        break;
      case TypeCadenceNote.DemiPause:
        noteResource = Resources.Load("Prefab/Silences/DemiPause") as GameObject;
        break;
      case TypeCadenceNote.Pause:
        noteResource = Resources.Load("Prefab/Silences/Pause") as GameObject;
        break;
    }

    GameObject note = Instantiate(noteResource);

    if (typeCadenceNote.IsNote())
    {
      AddHampe(note, typeNote, typeCadenceNote);

      Action<GameObject, Vector3, int> funcNote = null;
      switch (typeNote)
      {
        case TypeNote.Do:
          funcNote = Do;
          break;
        case TypeNote.Re:
          funcNote = Re;
          break;
        case TypeNote.Mi:
          funcNote = Mi;
          break;
        case TypeNote.Fa:
          funcNote = Fa;
          break;
        case TypeNote.Sol:
          funcNote = Sol;
          break;
        case TypeNote.La:
          funcNote = La;
          break;
        case TypeNote.Si:
          funcNote = Si;
          break;
      }

      funcNote(note, mesure.MesureStart.position, mesure.GetPosition());
    }
    else
      AddSilence(note, mesure.MesureStart.position, mesure.GetPosition());

    if (isPointe)
    {
      Instantiate(Resources.Load("Prefab/Notes/Pointe") as GameObject, note.transform);
    }

    mesure.AddNote(typeCadenceNote, typeNote, TypeGamme, isPointe, note);

    if (typeCadenceNote == TypeCadenceNote.Croche)
      LinkCroches(mesure.GetNotes());
  }

  private void AddSilence(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], note.transform.position.y, startPositionMesure.z);
  }

  private void LinkCroches(List<Note> notes)
  {
    if (notes.Count < 1) return;
    Note addedNote = notes[notes.Count - 1];

    // Récupérer la suite de croche
    Note lastNoteCroche = null;
    List<Note> croches = new List<Note>();
    for (int i = notes.Count - 1; i >= 0; i -= 1)
    {
      if (notes[i].TypeCadenceNote != TypeCadenceNote.Croche)
        break;

      lastNoteCroche = notes[i];
      croches.Add(notes[i]);
    }

    if (croches.Count < 2)
      return;

    // Regarder la direction de la hampe de la 1ère croche de la série
    bool hampeInferieur = lastNoteCroche.TypeGamme.IsHampeInferieur(lastNoteCroche.TypeNote);

    // Retirer la hampe des croches
    DisabledHampe(croches);

    // Calculer la haute de la hampe en fonction de la + ou - haute note
    Note referenceNote = null;
    if (hampeInferieur)
      referenceNote = croches.OrderBy(x => x.TypeGamme).ThenBy(x => x.TypeNote).FirstOrDefault();
    else
      referenceNote = croches.OrderByDescending(x => x.TypeGamme).ThenByDescending(x => x.TypeNote).FirstOrDefault();

    // Dans quelle gamme je suis + (le ton de la note) + la position de la hampe + la hauteur de la hampe
    float positionWorldHampeY = CalculatePositionWorldHampeY(hampeInferieur, referenceNote);

    // Ajouter une hampe basique en fonction de la croche initial
    AddBasicHampe(croches, hampeInferieur, positionWorldHampeY);

    // Ajouter une barre de liaison
    AddBarreLiaisonCroche(addedNote, croches, hampeInferieur, referenceNote);
  }

  private void AddBarreLiaisonCroche(Note addedNote, List<Note> croches, bool hampeInferieur, Note referenceNote)
  {
    // On prend addedNote car on va démarrer de celle-ci pour la position en x
    GameObject lastNote = addedNote.Sprite;
    var dummyLast = GetDummy(lastNote, "Dummy");
    float xWorld = 0f;
    foreach (Transform t in dummyLast.transform)
    {
      if (t.tag == "Hampe")
        xWorld = t.position.x;
    }

    // On récupère la 'referenceNote' juste pour avoir la position y (car c'est elle qui est à l'extrémité verticalement)
    GameObject objectReferenceNote = referenceNote.Sprite;
    var barreNoire = Instantiate(Resources.Load("Prefab/Notes/BarreNoire") as GameObject, lastNote.transform);
    barreNoire.name = "Liaison";
    barreNoire.transform.localScale = new Vector3(espacement * (croches.Count - 1) / lastNote.transform.localScale.x, 0.2f, 0);
    barreNoire.transform.position = new Vector3(xWorld - (espacement / 2 * (croches.Count - 1)), objectReferenceNote.transform.position.y + (hauteurHampeY * (!hampeInferieur ? 1 : -1)), 0);
  }

  private void AddBasicHampe(List<Note> croches, bool hampeInferieur, float positionWorldHampeY)
  {
    GameObject hampeResource = Resources.Load("Prefab/Notes/Hampe") as GameObject;

    foreach (var note in croches)
    {
      GameObject objectNote = note.Sprite;
      var dummy = GetDummy(objectNote, "Dummy");
      var hampe = Instantiate(hampeResource, dummy.transform);
      if (!hampeInferieur)
        hampe.transform.localPosition = positionHampeSuperieur;
      else
        hampe.transform.localPosition = positionHampeInferieur;

      // On modifie la hauteur de la hampe
      hampe.transform.localScale = new Vector3(hampe.transform.localScale.x, positionWorldHampeY - hampe.transform.position.y, hampe.transform.localScale.z);
      hampe.transform.localPosition = new Vector3(hampe.transform.localPosition.x, hampe.transform.localScale.y / 2, hampe.transform.localPosition.z);
    }
  }

  private static float CalculatePositionWorldHampeY(bool hampeInferieur, Note referenceNote)
  {
    float gammeTonPosition = 0f;
    if (referenceNote.TypeGamme == TypeGamme.Basse)
      gammeTonPosition = -gammeTon;
    else if (referenceNote.TypeGamme == TypeGamme.Haute)
      gammeTonPosition = gammeTon;

    float positionWorldHampeY = (positionGameNormal + gammeTonPosition) + (ton * (int)referenceNote.TypeNote);
    if (hampeInferieur)
      positionWorldHampeY -= positionHampeY + hauteurHampeY;
    else
      positionWorldHampeY += positionHampeY + hauteurHampeY;
    return positionWorldHampeY;
  }

  private void DisabledHampe(List<Note> croches)
  {
    foreach (var croche in croches)
    {
      GameObject objectNote = croche.Sprite;
      Transform t = objectNote.transform;
      DisabledChildGameObject(t);
    }
  }

  private void DisabledChildGameObject(Transform parent)
  {
    foreach (Transform tr in parent)
    {
      if ((tr.tag == "Hampe" || tr.tag == "HampeCroche" || tr.name == "Liaison") && tr.gameObject.activeInHierarchy)
        tr.gameObject.SetActive(false);

      if (tr.name == "Dummy")
      {
        Transform tDummy = tr.transform;
        DisabledChildGameObject(tDummy);
      }
    }
  }

  public GameObject GetDummy(GameObject referenceNote, string name)
  {
    Transform t = referenceNote.transform;
    foreach (Transform tr in t)
    {
      if (tr.name == name)
        return tr.gameObject;
    }

    GameObject dummy = new GameObject();
    dummy.transform.parent = referenceNote.transform;
    dummy.transform.localPosition = Vector3.zero;
    dummy.transform.localScale = Vector3.one;
    dummy.name = name;
    return dummy;
  }

  protected void AddHampe(GameObject note, TypeNote typeNote, TypeCadenceNote typeCadenceNote)
  {
    GameObject hampeResource = null;
    GameObject hampeCrocheResource = null;
    switch (typeCadenceNote)
    {
      case TypeCadenceNote.Noire:
      case TypeCadenceNote.Blanche:
        hampeResource = Resources.Load("Prefab/Notes/Hampe") as GameObject;
        break;

      case TypeCadenceNote.Croche:
        hampeResource = Resources.Load("Prefab/Notes/Hampe") as GameObject;
        hampeCrocheResource = Resources.Load("Prefab/Notes/HampeCroche") as GameObject;
        break;
    }

    GameObject hampe = null;
    GameObject hampeCroche = null;
    if (hampeResource != null)
      hampe = Instantiate(hampeResource, note.transform);

    if (hampeCrocheResource != null)
      hampeCroche = Instantiate(hampeCrocheResource, note.transform);

    AjusteHampe(hampe, hampeCroche, typeNote);
  }

  protected void SpawnBarreNoire(Transform transform, float tonPosition)
  {
    var barreNoire = Instantiate(Resources.Load("Prefab/Notes/BarreNoire") as GameObject, transform);
    barreNoire.transform.localPosition = new Vector3(0, tonPosition, 0);
    barreNoire.transform.localScale = new Vector3(1.5f, 0.2f, 0.0f);
  }

  protected void SpawnBarresNoire(GameObject doNote, int start, int nombreIteration)
  {
    for (int i = start, j = 0; j < nombreIteration; i += 2, j += 1)
      SpawnBarreNoire(doNote.transform, ton * i);
  }
}
