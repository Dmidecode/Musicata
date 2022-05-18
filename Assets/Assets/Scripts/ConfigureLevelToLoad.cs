using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureLevelToLoad : MonoBehaviour
{

  private static ConfigureLevelToLoad instance;

  public static ConfigureLevelToLoad Instance => instance;

  public Difficulte Difficulte;
  public int Level;

  private void Awake()
  {
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);

    DontDestroyOnLoad(gameObject);
  }
}
