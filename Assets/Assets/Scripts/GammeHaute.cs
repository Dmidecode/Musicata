using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammeHaute : SpawnerNote
{
  private float positionGameHaute;

  private void Start()
  {
    TypeGamme = TypeGamme.Haute;
    positionGameHaute = gammeTon;
  }

  protected override void AjusteHampe(GameObject hampe, GameObject hampeCroche, TypeNote typeNote)
  {
    if (hampe == null) return;
    hampe.transform.localPosition = positionHampeInferieur;

    if (hampeCroche == null) return;
    hampeCroche.transform.localPosition = positionHampeCrocheInferieur;
    hampeCroche.GetComponent<SpriteRenderer>().flipY = true;
  }

  public override void Do(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameHaute + startPositionMesure.y, startPositionMesure.z);
  }

  public override void Re(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameHaute + startPositionMesure.y + ton, startPositionMesure.z);
  }

  public override void Mi(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameHaute + startPositionMesure.y + ton * 2, startPositionMesure.z);
  }

  public override void Fa(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameHaute + startPositionMesure.y + ton * 3, startPositionMesure.z);
  }

  public override void Sol(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameHaute + startPositionMesure.y + ton * 4, startPositionMesure.z);
  }

  public override void La(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameHaute + startPositionMesure.y + ton * 5, startPositionMesure.z);
    SpawnBarreNoire(note.transform, 0);
  }

  public override void Si(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameHaute + startPositionMesure.y + ton * 6, startPositionMesure.z);
    SpawnBarreNoire(note.transform, -ton);
  }
}
