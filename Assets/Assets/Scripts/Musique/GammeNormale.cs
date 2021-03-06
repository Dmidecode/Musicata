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

  protected override void AjusteHampe(GameObject hampe, GameObject hampeCroche, TypeNote typeNote, bool isMainDroite)
  {
    if (hampe == null) return;
    if (!IsHampeInferieur(typeNote, isMainDroite))
      hampe.transform.localPosition = positionHampeSuperieur;
    else
      hampe.transform.localPosition = positionHampeInferieur;

    if (hampeCroche == null) return;

    if (!IsHampeInferieur(typeNote, isMainDroite))
      hampeCroche.transform.localPosition = positionHampeCrocheSuperieur;
    else
    {
      hampeCroche.transform.localPosition = positionHampeCrocheInferieur;
      hampeCroche.GetComponent<SpriteRenderer>().flipY = true;
    }
  }

  public override void Do(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], startPositionMesure.y, startPositionMesure.z);
    if (isMainDroite)
      SpawnBarreNoire(note.transform, 0);
  }

  public override void Re(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], startPositionMesure.y + ton, startPositionMesure.z);
  }

  public override void Mi(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], startPositionMesure.y + ton * 2, startPositionMesure.z);
  }

  public override void Fa(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], startPositionMesure.y + ton * 3, startPositionMesure.z);
  }

  public override void Sol(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], startPositionMesure.y + ton * 4, startPositionMesure.z);
  }

  public override void La(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], startPositionMesure.y + ton * 5, startPositionMesure.z);
  }

  public override void Si(GameObject note, bool isMainDroite, Vector3 startPositionMesure, int positionDansMesure)
  {
    note.transform.position = new Vector3(startPositionMesure.x + positionNoteToCm[positionDansMesure], startPositionMesure.y + ton * 6, startPositionMesure.z);
  }
}
