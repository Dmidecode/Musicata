using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ChoixLevel : MonoBehaviour
{
  public GameObject ChoixDifficultePanel;

  public GameObject LevelsPanel;

  private int Difficulte;

  public void ChoixDifficulte()
  {
    gameObject.SetActive(false);
    ChoixDifficultePanel.SetActive(true);
  }

  public void SetDifficulte(int difficulte)
  {
    this.Difficulte = difficulte;
    LoadLevels();
  }

  private void LoadLevels()
  {
    DirectoryInfo levelDirectoryPath = new DirectoryInfo(Path.Combine(Application.dataPath, "Assets", "Resources", "Text", "Solutions", Difficulte.ToString()));
    FileInfo[] fileInfo = levelDirectoryPath.GetFiles("*.json", SearchOption.AllDirectories);

    int index = 1;
    foreach (FileInfo file in fileInfo)
    {
      var buttonLevelPrefab = Resources.Load("Prefab/UI/ButtonLevel") as GameObject;
      var buttonLevelInstance = Instantiate(buttonLevelPrefab, LevelsPanel.transform);
      buttonLevelInstance.GetComponent<ButtonLevel>().Level = index;
      buttonLevelInstance.GetComponentInChildren<TMP_Text>().text = index.ToString();
      index += 1;
    }
  }
}
