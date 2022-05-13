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
    positionGameBasse = positionGameNormal - gammeTon;
  }

  protected override void AjusteHampe(GameObject hampe, GameObject hampeCroche, TypeNote typeNote)
  {
    if (hampe == null) return;
    hampe.transform.localPosition = positionHampeSuperieur;

    if (hampeCroche == null) return;
    hampeCroche.transform.localPosition = positionHampeCrocheSuperieur;
  }

  public override void Do(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse, startPositionMesure.z);
    SpawnBarresNoire(note, 1, 4);
  }

  public override void Re(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + ton, startPositionMesure.z);
    SpawnBarresNoire(note, 0, 4);
  }

  public override void Mi(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + ton * 2, startPositionMesure.z);
    SpawnBarresNoire(note, 1, 3);
  }

  public override void Fa(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + ton * 3, startPositionMesure.z);
    SpawnBarresNoire(note, 0, 3);
  }

  public override void Sol(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + ton * 4, startPositionMesure.z);
    SpawnBarresNoire(note, 1, 2);
  }

  public override void La(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + ton * 5, startPositionMesure.z);
    SpawnBarresNoire(note, 0, 2);
  }

  public override void Si(GameObject note, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], positionGameBasse + ton * 6, startPositionMesure.z);
    SpawnBarreNoire(note.transform, ton);
  }
}
