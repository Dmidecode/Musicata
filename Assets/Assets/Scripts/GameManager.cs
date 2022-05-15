using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  private int MesureTemps;
  private int Tempo;

  private List<Note> Alterations;

  private static GameManager instance;

  public static GameManager Instance => instance;

  private void Awake()
  {
    if (instance != null && instance != this)
      Destroy(gameObject);

    instance = this;

    this.Alterations = new List<Note>(); 

    MesureTemps = 4;
    Tempo = 130;

    // Bemol
    //this.Alterations.Add(new Note() { TypeNote = TypeNote.Si, TypeAlteration = TypeAlteration.Bemol });
    //this.Alterations.Add(new Note() { TypeNote = TypeNote.Mi, TypeAlteration = TypeAlteration.Bemol });
    //this.Alterations.Add(new Note() { TypeNote = TypeNote.La, TypeAlteration = TypeAlteration.Bemol });

    // Diese
    this.Alterations.Add(new Note() { TypeNote = TypeNote.Fa, TypeAlteration = TypeAlteration.Diese });
    this.Alterations.Add(new Note() { TypeNote = TypeNote.Do, TypeAlteration = TypeAlteration.Diese });
    //this.Alterations.Add(new Note() { TypeNote = TypeNote.Sol, TypeAlteration = TypeAlteration.Diese });
  }

  public void LoadConfigurationLevel(int difficulte, int level)
  {
    string file = $"{difficulte}/{level}.json";
  }

  public List<Note> GetAlterations()
  {
    return Alterations;
  }

  public int GetMesureTemps()
  {
    return MesureTemps;
  }

  public int GetTempo()
  {
    return Tempo;
  }

  public void SetMesureTemps(int mesureTemps)
  {
    this.MesureTemps = mesureTemps;
  }

  public void SetTempo(int tempo)
  {
    this.Tempo = tempo;
  }
}
