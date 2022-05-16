using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
  public GameObject ChoixDifficultePanel;
  public void ChoixDifficulte()
  {
    gameObject.SetActive(false);
    ChoixDifficultePanel.SetActive(true);
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
