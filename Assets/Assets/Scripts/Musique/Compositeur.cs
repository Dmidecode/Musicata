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
  public int NombreMesureMainDroite;
  public int NombreMesureMainGauche;


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
    NombreMesureMainDroite = GameManager.Instance.GetNombreMesuresMainDroite();
    NombreMesureMainGauche = GameManager.Instance.GetNombreMesuresMainGauche();

    int mesureTemps = GameManager.Instance.GetMesureTemps();
    MesuresMainDroite = new ManageMesure[NombreMesureMainDroite];
    MesuresMainGauche = new ManageMesure[NombreMesureMainGauche];
    for (int i = 0; i < NombreMesureMainDroite; i += 1)
      MesuresMainDroite[i] = new ManageMesure(mesureTemps, true, MesuresMainDroiteTransform[i].transform);
    for (int i = 0; i < NombreMesureMainGauche; i += 1)
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

  public void DeleteLastNote(bool isMainDroite)
  {
    ManageMesure[] mesuresMain = null;
    if (isMainDroite)
      mesuresMain = MesuresMainDroite;
    else
      mesuresMain = MesuresMainGauche;

    ManageMesure mesure = mesuresMain.FirstOrDefault(x => !x.IsCompleted());
    if (mesure == null || !mesure.Notes.Any())
    {
      mesure = mesuresMain.LastOrDefault(x => x.IsCompleted());
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
      DeleteLastNote(true);
    }
    else if (Input.GetKeyDown(KeyCode.KeypadPlus))
    {
      DeleteLastNote(false);
    }
    else if (Input.GetKeyDown(KeyCode.A))
    {
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Mi, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Fa, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Sol, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.La, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Si, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      //AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Haute, TypeAlteration.None, false, true);
      //AddNote(TypeNote.Si, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.La, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Sol, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Fa, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Mi, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
      AddNote(TypeNote.Do, TypeCadenceNote.Ronde, TypeGamme.Normale, TypeAlteration.None, false, false);
    }
    else if (Input.GetKeyDown(KeyCode.Z))
    {
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, false);
    }
    else if (Input.GetKeyDown(KeyCode.E))
    {
      AddNote(TypeNote.Sol, TypeCadenceNote.Pause, TypeGamme.Haute, TypeAlteration.None, false, false);
    }
    else if (Input.GetKeyDown(KeyCode.R))
    {
      AddNote(TypeNote.Fa, TypeCadenceNote.Soupir, TypeGamme.Haute, TypeAlteration.None, false, false);
    }
    else if (Input.GetKeyDown(KeyCode.T))
    {
      AddNote(TypeNote.Sol, TypeCadenceNote.DemiSoupir, TypeGamme.Haute, TypeAlteration.None, false, false);
    }
    else if (Input.GetKeyDown(KeyCode.Y))
    {
      AddNote(TypeNote.La, TypeCadenceNote.DemiPause, TypeGamme.Haute, TypeAlteration.None, false, false);
    }
    else if (Input.GetKeyDown(KeyCode.U))
    {
      AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Mi, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Fa, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Sol, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.La, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Si, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      //AddNote(TypeNote.Do, TypeCadenceNote.Noire, TypeGamme.Haute, TypeAlteration.None, false, true);
      //AddNote(TypeNote.Si, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.La, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Sol, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Fa, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Mi, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Re, TypeCadenceNote.Noire, TypeGamme.Normale, TypeAlteration.None, false, true);
      AddNote(TypeNote.Do, TypeCadenceNote.Ronde, TypeGamme.Normale, TypeAlteration.None, false, true);
    }
    else if (Input.GetKeyDown(KeyCode.I))
    {
      AddNote(TypeNote.Sol, TypeCadenceNote.Pause, TypeGamme.Haute, TypeAlteration.None, false, true);
    }
  }
}
