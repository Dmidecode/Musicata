using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixDifficulte : MonoBehaviour
{
  public GameObject MainMenuPanel;
  public GameObject ChoixLevelPanel;
  public ChoixLevel ChoixLevel;

  public void MenuPrincipal()
  {
    gameObject.SetActive(false);
    MainMenuPanel.SetActive(true);
  }

  public void ChoixFacile()
  {
    Choix(Difficulte.Facile);
  }

  public void ChoixNormal()
  {
    Choix(Difficulte.Normal);
  }
  public void ChoixDifficile()
  {
    Choix(Difficulte.Difficile);
  }
  public void ChoixBeethoven()
  {
    Choix(Difficulte.Beethoven);
  }

  private void Choix(Difficulte difficulte)
  {
    ConfigureLevelToLoad.Instance.Difficulte = difficulte;
    ChoixLevel.SetDifficulte((int)difficulte);
    ChoixLevelPanel.SetActive(true);
    gameObject.SetActive(false);
  }
}
