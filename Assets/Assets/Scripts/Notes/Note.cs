using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
  [Serializable]
  public class Note
  {
    public TypeCadenceNote TypeCadenceNote;

    public TypeNote TypeNote;

    [NonSerialized]
    protected float Temps;

    public bool IsPointe;

    [NonSerialized]
    public GameObject Sprite;

    public TypeGamme TypeGamme;

    public TypeAlteration TypeAlteration;

    public float GetTemps()
    {
      return Temps + (this.IsPointe ? this.Temps / 2 : 0);
    }

    public bool CompareSolution(Note note)
    {
      return this.TypeNote == note.TypeNote && this.TypeCadenceNote == note.TypeCadenceNote && this.TypeGamme == note.TypeGamme && this.IsPointe == note.IsPointe && this.TypeAlteration == note.TypeAlteration;
    }
  }
}
