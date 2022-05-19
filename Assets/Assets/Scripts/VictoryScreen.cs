using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
  public TMP_Text Auteur;
  public TMP_Text Titre;

  public GameObject ButtonNextLevel;

  private void OnEnable()
  {
    Auteur.text = GameManager.Instance.GetAuteurMusique();
    Titre.text = GameManager.Instance.GetTitreMusique();
    DirectoryInfo levelDirectoryPath = new DirectoryInfo(Path.Combine(Application.dataPath, "Assets", "Resources", "Text", "Solutions", GameManager.Instance.Difficulte.ToString()));
    FileInfo[] fileInfo = levelDirectoryPath.GetFiles("*", SearchOption.AllDirectories);
    ButtonNextLevel.SetActive(GameManager.Instance.Level + 1 < fileInfo.Length);
  }

  public void NextLevel()
  {
    GameManager.Instance.Level += 1;
    ConfigureLevelToLoad.Instance.Difficulte = GameManager.Instance.Difficulte;
    ConfigureLevelToLoad.Instance.Level = GameManager.Instance.Level;
    LoaderScene.Instance.LoadLevel("PuzzleLevel");
  }

  public void MenuPrincipal()
  {
    LoaderScene.Instance.LoadLevel("MenuPrincipal");
  }
}
