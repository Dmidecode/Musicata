using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour
{
  public int Level;

  public void LoadLevel()
  {
    ConfigureLevelToLoad.Instance.Level = Level;
    LoaderScene.Instance.LoadLevel("PuzzleLevel");
  }
}
