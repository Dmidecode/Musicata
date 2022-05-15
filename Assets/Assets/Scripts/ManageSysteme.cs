using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManageSysteme : MonoBehaviour
{
  public GameObject ClefSol;
  public GameObject ClefFa;
  public bool IsClefSol;

  private const int maxSignature = 3;
  void Start()
  {
    var clef = ClefSol;
    if (!IsClefSol)
      clef = ClefFa;

    clef.SetActive(true);
    var signatureTransform = clef.transform.Find("Signature");
    if (signatureTransform != null)
    {
      var notes = GameManager.Instance.GetAlterations();
      bool isBemol = notes.Any(x => x.TypeAlteration == Assets.Scripts.TypeAlteration.Bemol);
      string gameObjectSignature = isBemol ? "Bemol" : "Diese";
      for (int i = 0; i < Mathf.Min(notes.Count, maxSignature); i += 1)
      {
        var keyTransform = signatureTransform.transform.Find($"{gameObjectSignature}{i}");
        keyTransform.gameObject.SetActive(true);
      }
    }
  }
}
