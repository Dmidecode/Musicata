using Assets.Scripts;
using Assets.Scripts.Notes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Compositeur : MonoBehaviour
{
  public Transform Mesure1;
  public Transform Mesure2;
  public Transform Mesure3;
  public Transform Mesure4;

  public GammeNormale GammeNormale { get; set; }

  public GammeBasse GammeBasse { get; set; }

  public GammeHaute GammeHaute { get; set; }

  private float _tempo;

  private ManageMesure[] Mesures;

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

    // Temporairement
    SetTempo(4);
  }

  private void Start()
  {
    Mesures = new ManageMesure[4] { new ManageMesure(_tempo, Mesure1.transform), new ManageMesure(_tempo, Mesure2.transform), new ManageMesure(_tempo, Mesure3.transform), new ManageMesure(_tempo, Mesure4.transform) };
  }

  void Update()
  {
    SpawnNoire();
  }

  public float GetTempo() => _tempo;

  public void SetTempo(float tempo) => _tempo = tempo;

  public void AddNote(TypeNote typeNote, TypeCadenceNote typeCadenceNote, TypeGamme typeGamme, bool isPointe)
  {
    if (isPointe && !typeCadenceNote.CanPointe()) return;

    ManageMesure mesure = Mesures.FirstOrDefault(x => !x.IsCompleted());
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

    spawner.AddNote(typeNote, typeCadenceNote, isPointe, mesure);
  }

  public void DeleteLastNote()
  {
    ManageMesure mesure = Mesures.FirstOrDefault(x => !x.IsCompleted());
    if (mesure == null || !mesure.Notes.Any())
    {
      mesure = Mesures.LastOrDefault(x => x.IsCompleted());
      if (mesure == null || !mesure.Notes.Any())
        return;
    }

    GammeNormale.DeleteLastNote(mesure);
  }

  private void SpawnNoire()
  {
    if (Input.GetKeyDown(KeyCode.KeypadEnter))
    {
      DeleteLastNote();
    }
    else if (Input.GetKeyDown(KeyCode.A))
    {
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, false);
    }
    else if (Input.GetKeyDown(KeyCode.Z))
    {
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, true);
    }
    else if (Input.GetKeyDown(KeyCode.E))
    {
      AddNote(TypeNote.Mi, TypeCadenceNote.Blanche, TypeGamme.Normale, true);
    }
    else if (Input.GetKeyDown(KeyCode.R))
    {
      AddNote(TypeNote.Fa, TypeCadenceNote.Noire, TypeGamme.Normale, false);
    }
    else if (Input.GetKeyDown(KeyCode.T))
    {
      AddNote(TypeNote.Sol, TypeCadenceNote.Noire, TypeGamme.Normale, true);
    }
    else if (Input.GetKeyDown(KeyCode.Y))
    {
      AddNote(TypeNote.La, TypeCadenceNote.Croche, TypeGamme.Normale, false);
    }
    else if (Input.GetKeyDown(KeyCode.U))
    {
      AddNote(TypeNote.La, TypeCadenceNote.Croche, TypeGamme.Normale, true);
    }
    else if (Input.GetKeyDown(KeyCode.I))
    {
      AddNote(TypeNote.Si, TypeCadenceNote.Croche, TypeGamme.Normale, false);
    }
  }
}
