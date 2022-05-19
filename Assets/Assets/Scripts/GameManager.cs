using Assets.Scripts;
using Assets.Scripts.Notes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  private int MesureTemps;
  private int Tempo;

  private List<Note> Alterations;

  private static GameManager instance;

  public static GameManager Instance => instance;

  private ConfigurationLevel configurationLevel;

  public List<ErreurMesure> ErreursMesure;

  public Difficulte Difficulte;
  public int Level;

  public GameObject VictoryCanvas;
  public GameObject UILevel;

  private void Awake()
  {
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);

    //DontDestroyOnLoad(gameObject);

    this.Alterations = new List<Note>();
    ErreursMesure = new List<ErreurMesure>();
    if (SceneManager.GetActiveScene().name == "EditorLevel")
      LoadConfigurationLevel(Difficulte.Dev, -1);
    //else if (ConfigureLevelToLoad.Instance.Level <= 0)
    //  LoadConfigurationLevel(Difficulte.Facile, 1);
  }

  public void ValidatePuzzle()
  {
    AnalyseAnwser();
    //GetComponent<ManageMidi>().ListenAnswer(AnalyseAnwser);
  }

  public void AnalyseAnwser()
  {
    var mesuresMainDroite = Compositeur.Instance.GetMesuresMainDroite();
    var mesuresMainGauche = Compositeur.Instance.GetMesuresMainGauche();
    var solutionMesuresMainDroite = configurationLevel.Solution.MainDroite;
    var solutionMesuresMainGauche = configurationLevel.Solution.MainGauche;
    bool okMainDroite = CompareSolution(mesuresMainDroite, solutionMesuresMainDroite, true);
    bool okMainGauche = CompareSolution(mesuresMainGauche, solutionMesuresMainGauche, false);
    Debug.Log($"Solution main droite: {okMainDroite}");
    Debug.Log($"Solution main gauche: {okMainGauche}");

    if (okMainDroite && okMainGauche)
      DisplayVictory();
  }

  private void DisplayVictory()
  {
    UILevel.SetActive(false);
    VictoryCanvas.SetActive(true);
  }

  private bool CompareSolution(ManageMesure[] mesures, List<ManageMesure> solutionMesures, bool isMainDroite)
  {
    for (int i = 0; i < solutionMesures.Count; i++)
    {
      if (i >= mesures.Count())
      {
        ErreursMesure.Add(new ErreurMesure() { Index = i, IsMainDroite = isMainDroite });
        return false;
      }

      var mesure = mesures[i];
      var solution = solutionMesures[i];

      if (solution.Notes.Count != mesure.Notes.Count)
      {
        ErreursMesure.Add(new ErreurMesure() { Index = i, IsMainDroite = isMainDroite });
        return false;
      }

      for (int n = 0; n < solution.Notes.Count; n++)
      {
        var solutionNote = solution.Notes[n];
        var note = mesure.Notes[n];
        if (!solutionNote.CompareSolution(note))
        {
          ErreursMesure.Add(new ErreurMesure() { Index = i, IsMainDroite = isMainDroite });
          return false;
        }
      }
    }

    return !ErreursMesure.Any(x => x.IsMainDroite == isMainDroite);
  }

  public void LoadConfigurationLevel(Difficulte difficulte, int level)
  {
    string file = $"{(int)difficulte}/{level}";
    var rawFile = Resources.Load($"Text/Solutions/{file}") as TextAsset;

    configurationLevel = JsonUtility.FromJson<ConfigurationLevel>(rawFile.text);
    Debug.Log(configurationLevel.ToString());
    MesureTemps = configurationLevel.Temps;
    Tempo = configurationLevel.Tempo;

    var objects = Resources.FindObjectsOfTypeAll<GameObject>();

    for (int i = configurationLevel.NombreSystemeMainDroite; i < 2; i += 1)
    {
      var systemeMainDroite = objects.FirstOrDefault(x => x.name == $"SystemeMainDroite{i + 1}");
      if (systemeMainDroite != null)
        systemeMainDroite.SetActive(false);
    }

    for (int i = configurationLevel.NombreSystemeMainGauche; i < 2; i += 1)
    {
      var systemeMainGauche = objects.FirstOrDefault(x => x.name == $"SystemeMainGauche{i + 1}");
      if (systemeMainGauche != null)
        systemeMainGauche.SetActive(false);
    }

    if (configurationLevel.SignatureDiese > 0)
      this.Alterations.Add(new Note() { TypeNote = TypeNote.Fa, TypeAlteration = TypeAlteration.Diese });
    if (configurationLevel.SignatureDiese > 1)
      this.Alterations.Add(new Note() { TypeNote = TypeNote.Do, TypeAlteration = TypeAlteration.Diese });
    if (configurationLevel.SignatureDiese > 2)
      this.Alterations.Add(new Note() { TypeNote = TypeNote.Sol, TypeAlteration = TypeAlteration.Diese });
    if (configurationLevel.SignatureBemol > 0)
      this.Alterations.Add(new Note() { TypeNote = TypeNote.Si, TypeAlteration = TypeAlteration.Bemol });
    if (configurationLevel.SignatureBemol > 1)
      this.Alterations.Add(new Note() { TypeNote = TypeNote.Mi, TypeAlteration = TypeAlteration.Bemol });
    if (configurationLevel.SignatureBemol > 2)
      this.Alterations.Add(new Note() { TypeNote = TypeNote.La, TypeAlteration = TypeAlteration.Bemol });
  }

  public int GetNombreMesuresMainDroite()
  {
    return configurationLevel.NombreSystemeMainDroite * 4;
  }

  public int GetNombreMesuresMainGauche()
  {
    return configurationLevel.NombreSystemeMainGauche * 4;
  }

  public List<Note> GetAlterations()
  {
    return Alterations;
  }

  public int GetMesureTemps()
  {
    return MesureTemps;
  }

  public int GetTempo()
  {
    return Tempo;
  }

  public void SetMesureTemps(int mesureTemps)
  {
    this.MesureTemps = mesureTemps;
  }

  public void SetTempo(int tempo)
  {
    this.Tempo = tempo;
  }

  public string GetAuteurMusique()
  {
    return configurationLevel.Auteur;
  }

  public string GetTitreMusique()
  {
    return configurationLevel.Titre;
  }
}
