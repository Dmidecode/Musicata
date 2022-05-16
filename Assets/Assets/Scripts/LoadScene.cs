using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
  private void Awake()
  {
    GameManager.Instance.LoadConfigurationLevel(GameManager.Instance.Difficulte, GameManager.Instance.Level);
  }
}
