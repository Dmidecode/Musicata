using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
  private void Awake()
  {
    LoadConfigurationLevel();
  }

  public void LoadConfigurationLevel()
  {
    if (ConfigureLevelToLoad.Instance.Level > 0)
      GameManager.Instance.LoadConfigurationLevel(ConfigureLevelToLoad.Instance.Difficulte, ConfigureLevelToLoad.Instance.Level);
  }
}
