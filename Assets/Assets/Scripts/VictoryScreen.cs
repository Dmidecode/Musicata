using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
  public void NextLevel()
  {
    GameManager.Instance.Level += 1;
    ConfigureLevelToLoad.Instance.Difficulte = GameManager.Instance.Difficulte;
    ConfigureLevelToLoad.Instance.Level = GameManager.Instance.Level;
    LoaderScene.Instance.LoadLevel();
  }
}
