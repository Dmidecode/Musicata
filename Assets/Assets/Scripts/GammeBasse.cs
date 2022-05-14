using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammeBasse : SpawnerNote
{
  private float positionGameBasse;

  private void Start()
  {
    TypeGamme = TypeGamme.Basse;
    positionGameBasse = -gammeTon;
  }

  protected override void AjusteHampe(GameObject hampe, GameObject hampeCroche, TypeNote typeNote, bool isMainDroite)
  {
    if (hampe == null) return;
    hampe.transform.localPosition = positionHampeSuperieur;

    if (hampeCroche == null) return;
    hampeCroche.transform.localPosition = positionHampeCrocheSuperieur;
  }

  public override void Do(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + startPositionMesure.y, startPositionMesure.z);
    if (isMainDroite)
      SpawnBarresNoire(note, 1, 4, 1);
    else
      SpawnBarresNoire(note, 0, 2, 1);
  }

  public override void Re(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + startPositionMesure.y + ton, startPositionMesure.z);
    if (isMainDroite)
      SpawnBarresNoire(note, 0, 4, 1);
    else
      SpawnBarresNoire(note, 1, 1, 1);
  }

  public override void Mi(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + startPositionMesure.y + ton * 2, startPositionMesure.z);
    if (isMainDroite)
      SpawnBarresNoire(note, 1, 3, 1);
    else
      SpawnBarreNoire(note.transform, 0);
  }

  public override void Fa(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + startPositionMesure.y + ton * 3, startPositionMesure.z);
    if (isMainDroite)
      SpawnBarresNoire(note, 0, 3, 1);
  }

  public override void Sol(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + startPositionMesure.y + ton * 4, startPositionMesure.z);
    if (isMainDroite)
      SpawnBarresNoire(note, 1, 2, 1);
  }

  public override void La(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + startPositionMesure.y + ton * 5, startPositionMesure.z);
    if (isMainDroite)
      SpawnBarresNoire(note, 0, 2, 1);
  }

  public override void Si(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + startPositionMesure.y + ton * 6, startPositionMesure.z);
    if (isMainDroite)
      SpawnBarreNoire(note.transform, ton);
  }
}
