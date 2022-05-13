using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammeNormale : SpawnerNote
{
  private void Start()
  {
    TypeGamme = TypeGamme.Normale; 
  }

  protected override void AjusteHampe(GameObject hampe, GameObject hampeCroche, TypeNote typeNote)
  {
    if (hampe == null) return;
    if (!IsHampeInferieur(typeNote))
      hampe.transform.localPosition = positionHampeSuperieur;
    else
      hampe.transform.localPosition = positionHampeInferieur;

    if (hampeCroche == null) return;

    if (!IsHampeInferieur(typeNote))
      hampeCroche.transform.localPosition = positionHampeCrocheSuperieur;
    else
    {
      hampeCroche.transform.localPosition = positionHampeCrocheInferieur;
      hampeCroche.GetComponent<SpriteRenderer>().flipY = true;
    }
  }

  public override void Do(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameNormal, startPositionMesure.z);
    SpawnBarreNoire(note.transform, 0);
  }

  public override void Re(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameNormal + ton, startPositionMesure.z);
  }

  public override void Mi(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameNormal + ton * 2, startPositionMesure.z);
  }

  public override void Fa(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameNormal + ton * 3, startPositionMesure.z);
  }

  public override void Sol(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameNormal + ton * 4, startPositionMesure.z);
  }

  public override void La(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameNormal + ton * 5, startPositionMesure.z);
  }

  public override void Si(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameNormal + ton * 6, startPositionMesure.z);
  }
}
