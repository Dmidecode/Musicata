using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Notes
{
  public class ManageMesure
  {
    public float Tempo;

    public Transform MesureStart;

    public List<Note> Notes;

    public ManageMesure(float tempo, Transform mesureStart)
    {
      Notes = new List<Note>();
      Tempo = tempo;
      MesureStart = mesureStart;
    }

    public bool IsCompleted()
    {
      return GetCurrentTempo() >= Tempo;
    }

    public float GetMaxTempo()
    {
      return Tempo;
    }

    public int GetPosition()
    {

      return Mathf.FloorToInt(GetCurrentTempo() * 2);
    }

    public float GetCurrentTempo()
    {
      return Notes.Sum(x => x.GetTempo());
    }

    public bool CanAddNote(TypeCadenceNote typeCadenceNote, bool isPointe)
    {
      Note note = typeCadenceNote.ToNote();
      note.IsPointe = isPointe;
      return GetCurrentTempo() + note.GetTempo() <= Tempo;
    }

    public void AddNote(TypeCadenceNote typeCadenceNote, TypeNote typeNote, TypeGamme typeGamme, bool isPointe, GameObject sprite)
    {
      Note note = typeCadenceNote.ToNote();
      note.IsPointe = isPointe;
      note.Sprite = sprite;
      note.TypeNote = typeNote;
      note.TypeGamme = typeGamme;
      Notes.Add(note);
    }

    public List<Note> GetNotes()
    {
      return Notes;
    }
  }
}
