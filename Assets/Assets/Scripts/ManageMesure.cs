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
    public float MesureTemps;

    public Transform MesureStart;

    public List<Note> Notes;

    public ManageMesure(float tempo, Transform mesureStart)
    {
      Notes = new List<Note>();
      MesureTemps = tempo;
      MesureStart = mesureStart;
    }

    public bool IsCompleted()
    {
      return GetCurrentMesureTemps() >= MesureTemps;
    }

    public int GetPosition()
    {

      return Mathf.FloorToInt(GetCurrentMesureTemps() * 2);
    }

    public float GetCurrentMesureTemps()
    {
      return Notes.Sum(x => x.GetTemps());
    }

    public bool CanAddNote(TypeCadenceNote typeCadenceNote, bool isPointe)
    {
      Note note = typeCadenceNote.ToNote();
      note.IsPointe = isPointe;
      return GetCurrentMesureTemps() + note.GetTemps() <= MesureTemps;
    }

    public void AddNote(TypeCadenceNote typeCadenceNote, TypeNote typeNote, TypeGamme typeGamme, TypeAlteration typeAlteration, bool isPointe, GameObject sprite)
    {
      Note note = typeCadenceNote.ToNote();
      note.IsPointe = isPointe;
      note.Sprite = sprite;
      note.TypeNote = typeNote;
      note.TypeGamme = typeGamme;
      note.TypeAlteration = typeAlteration;
      Notes.Add(note);
    }

    public List<Note> GetNotes()
    {
      return Notes;
    }
  }
}
