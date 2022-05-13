using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
  public abstract class Note
  {
    public TypeCadenceNote TypeCadenceNote { get; set; }

    public TypeNote TypeNote { get; set; }

    protected float Tempo { get; set; }

    public bool IsPointe { get; set; }

    public GameObject Sprite { get; set; }

    public TypeGamme TypeGamme { get; set; }

    public float GetTempo()
    {
      return Tempo + (this.IsPointe ? this.Tempo / 2 : 0);
    }
  }
}
