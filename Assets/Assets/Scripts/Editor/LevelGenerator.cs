using UnityEngine;
using UnityEditor;
using Assets.Scripts;

public class LevelGenerator : EditorWindow
{
  private int level;
  private int selectedDifficulte = 0;
  private int selectedNSMD = 1;
  private int selectedNSMG = 0;
  private int selectedSignatureDiese = 0;
  private int selectedSignatureBemol = 0;
  private int tempo = 130;
  private int selectedTemps = 4;

  private int cadenceNote = 0;
  private int note = 0;
  private int alteration = 0;
  private int gamme = 1;
  private bool isPointe = false;
  private bool isMainDroite = true;

  [MenuItem("Window/Level Generator")]
  public static void ShowWindows()
  {
    GetWindow<LevelGenerator>("Level Generator");
  }

  void OnGUI()
  {
    GuiLine();
    EditorGUILayout.Space();
    EditorGUILayout.TextField("Titre", "Titre de la musique");
    
    string[] optionsDifficulte = new string[]
    {
     "Facile", "Normal", "Difficile", "Beethoven",
    };
    selectedDifficulte = EditorGUILayout.Popup("Difficulte", selectedDifficulte, optionsDifficulte);


    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Level");
    level = EditorGUILayout.IntField(level);
    EditorGUILayout.EndHorizontal();

    int[] optionsNSMD = new int[] { 0, 1, 2 };
    string[] optionsTextNSMD = new string[] { "0", "1", "2" };
    selectedNSMD = EditorGUILayout.IntPopup("Nombre Système Main Droite", selectedNSMD, optionsTextNSMD, optionsNSMD);

    
    int[] optionsNSMG = new int[] { 0, 1, 2 };
    string[] optionsTextNSMG = new string[] { "0", "1", "2" };
    selectedNSMG = EditorGUILayout.IntPopup("Nombre Système Main Gauche", selectedNSMG, optionsTextNSMG, optionsNSMG);

    
    int[] optionsSignatureDiese = new int[] { 0, 1, 2, 3 };
    string[] optionsTextSignatureDiese = new string[] { "0", "1", "2", "3" };
    selectedSignatureDiese = EditorGUILayout.IntPopup("Nombre Signature Dièse", selectedSignatureDiese, optionsTextSignatureDiese, optionsSignatureDiese);

    
    int[] optionsSignatureBemol = new int[] { 0, 1, 2, 3 };
    string[] optionsTextSignatureBemol = new string[] { "0", "1", "2", "3" };
    selectedSignatureBemol = EditorGUILayout.IntPopup("Nombre Signature Bemol", selectedSignatureBemol, optionsTextSignatureBemol, optionsSignatureBemol);


    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Tempo");
    tempo = EditorGUILayout.IntSlider(tempo, 60, 250);
    EditorGUILayout.EndHorizontal();

    int[] optionsTemps = new int[] { 3, 4 };
    string[] optionsTextTemps = new string[] { "3", "4" };
    selectedTemps = EditorGUILayout.IntPopup("Temps", selectedTemps, optionsTextTemps, optionsTemps);

    EditorGUILayout.Space();
    GuiLine();
    EditorGUILayout.Space();

    int[] optionsNote = new int[] { 0, 1, 2, 3, 4, 5, 6 };
    string[] optionsTextNote = new string[] { "Do", "Ré", "Mi", "Fa", "Sol", "La", "Si" };
    note = EditorGUILayout.IntPopup("Note", note, optionsTextNote, optionsNote);

    int[] optionsCadence = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
    string[] optionsTextCadence = new string[] { "Croche", "Noire", "Blanche", "Ronde", "Demi Soupir", "Soupir", "Demi Pause", "Pause" };
    cadenceNote = EditorGUILayout.IntPopup("Cadence", cadenceNote, optionsTextCadence, optionsCadence);

    int[] optionsAlteration = new int[] { 0, 1, 2, 3 };
    string[] optionsTextAlteration = new string[] { "None", "Becarre", "Diese", "Bemol" };
    alteration = EditorGUILayout.IntPopup("Altération", alteration, optionsTextAlteration, optionsAlteration);

    int[] optionsGamme = new int[] { 0, 1, 2 };
    string[] optionsTextGamme = new string[] { "Basse", "Normal", "Haute" };
    gamme = EditorGUILayout.IntPopup("Gamme", gamme, optionsTextGamme, optionsGamme);

    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Est pointée ?");
    isPointe = EditorGUILayout.Toggle(isPointe);
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Est main droite?");
    isMainDroite = EditorGUILayout.Toggle(isMainDroite);
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.Space();
    GuiLine();
    EditorGUILayout.Space();
    if (GUILayout.Button("Ajouter une note"))
    {
      if (Compositeur.Instance == null) return;

      Compositeur.Instance.AddNote((TypeNote)note, (TypeCadenceNote)cadenceNote, (TypeGamme)gamme, (TypeAlteration)alteration, isPointe, isMainDroite);
    }

    EditorGUILayout.Space();
    GuiLine();
    EditorGUILayout.Space();
    if (GUILayout.Button("Generer le fichier"))
    {
      GenererFichier();
      Debug.Log("Fichier généré");
    }
  }

  public void GenererFichier()
  {
    //ConfigurationLevel configuration = new ConfigurationLevel();

  }

  void GuiLine(int i_height = 1)
  {
    Rect rect = EditorGUILayout.GetControlRect(false, i_height);
    rect.height = i_height;
    EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
  }
}
