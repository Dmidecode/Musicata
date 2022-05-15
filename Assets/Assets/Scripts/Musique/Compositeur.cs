using Assets.Scripts;
using Assets.Scripts.Notes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Compositeur : MonoBehaviour
{
  public GammeNormale GammeNormale { get; set; }

  public GammeBasse GammeBasse { get; set; }

  public GammeHaute GammeHaute { get; set; }

  public Transform[] MesuresMainDroiteTransform;
  private ManageMesure[] MesuresMainDroite;
  public Transform[] MesuresMainGaucheTransform;
  private ManageMesure[] MesuresMainGauche;


  private static Compositeur instance;

  public static Compositeur Instance => instance;

  private void Awake()
  {
    if (instance != null && instance != this)
      Destroy(gameObject);

    instance = this;

    GammeNormale = GetComponent<GammeNormale>();
    GammeBasse = GetComponent<GammeBasse>();
    GammeHaute = GetComponent<GammeHaute>();
  }

  private void Start()
  {
    int mesureTemps = GameManager.Instance.GetMesureTemps();
    MesuresMainDroite = new ManageMesure[MesuresMainDroiteTransform.Length];
    MesuresMainGauche = new ManageMesure[MesuresMainGaucheTransform.Length];
    for (int i = 0;i < MesuresMainDroiteTransform.Length; i += 1)
      MesuresMainDroite[i] = new ManageMesure(mesureTemps, true, MesuresMainDroiteTransform[i].transform);
    for (int i = 0; i < MesuresMainGaucheTransform.Length; i += 1)
      MesuresMainGauche[i] = new ManageMesure(mesureTemps, false, MesuresMainGaucheTransform[i].transform);
  }

  void Update()
  {
    SpawnMusic();
  }

  public void AddNote(TypeNote typeNote, TypeCadenceNote typeCadenceNote, TypeGamme typeGamme, TypeAlteration typeAlteration, bool isPointe, bool isMainDroite)
  {
    if (isPointe && !typeCadenceNote.CanPointe()) return;

    ManageMesure[] manageMesures = isMainDroite ? MesuresMainDroite : MesuresMainGauche;
    ManageMesure mesure = manageMesures.FirstOrDefault(x => !x.IsCompleted());
    if (mesure == null || !mesure.CanAddNote(typeCadenceNote, isPointe)) return;

    SpawnerNote spawner = GammeNormale;
    switch (typeGamme)
    {
      case TypeGamme.Basse:
        spawner = GammeBasse;
        break;
      case TypeGamme.Haute:
        spawner = GammeHaute;
        break;
    }

    spawner.AddNote(typeNote, typeCadenceNote, typeAlteration, isPointe, mesure);
  }

  public void DeleteLastNote()
  {
    ManageMesure mesure = MesuresMainDroite.FirstOrDefault(x => !x.IsCompleted());
    if (mesure == null || !mesure.Notes.Any())
    {
      mesure = MesuresMainDroite.LastOrDefault(x => x.IsCompleted());
      if (mesure == null || !mesure.Notes.Any())
        return;
    }

    GammeNormale.DeleteLastNote(mesure);
  }

  public ManageMesure[] GetMesuresMainDroite()
  {
    return MesuresMainDroite;
  }

  public ManageMesure[] GetMesuresMainGauche()
  {
    return MesuresMainGauche;
  }

  private void SpawnMusic()
  {
    if (Input.GetKeyDown(KeyCode.KeypadEnter))
    {
      DeleteLastNote();
    }
    else if (Input.GetKeyDown(KeyCode.A))
    {
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Mi, TypeCadenceNote.Blanche, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Re, TypeCadenceNote.Blanche, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Mi, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Do, TypeCadenceNote.Blanche, TypeGamme.Normale, TypeAlteration.None, false, true);
    }
    else if (Input.GetKeyDown(KeyCode.Z))
    {
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Mi, TypeCadenceNote.Blanche, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Re, TypeCadenceNote.Blanche, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Mi, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Do, TypeCadenceNote.Blanche, TypeGamme.Normale, TypeAlteration.None, false, false);
    }
    else if (Input.GetKeyDown(KeyCode.E))
    {
      AddNote(TypeNote.Mi, TypeCadenceNote.Noire, TypeGamme.Haute, TypeAlteration.None, false, true);
    }
    else if (Input.GetKeyDown(KeyCode.R))
    {
      AddNote(TypeNote.Fa, TypeCadenceNote.Noire, TypeGamme.Haute, TypeAlteration.None, false, true);
    }
    else if (Input.GetKeyDown(KeyCode.T))
    {
      AddNote(TypeNote.Sol, TypeCadenceNote.Noire, TypeGamme.Haute, TypeAlteration.None, false, true);
    }
    else if (Input.GetKeyDown(KeyCode.Y))
    {
      AddNote(TypeNote.La, TypeCadenceNote.Noire, TypeGamme.Haute, TypeAlteration.None, false, true);
    }
    else if (Input.GetKeyDown(KeyCode.U))
    {
      AddNote(TypeNote.La, TypeCadenceNote.Noire, TypeGamme.Haute, TypeAlteration.None, false, true);
    }
    else if (Input.GetKeyDown(KeyCode.I))
    {
      AddNote(TypeNote.Si, TypeCadenceNote.Noire, TypeGamme.Haute, TypeAlteration.None, false, true);
    }
  }
}
