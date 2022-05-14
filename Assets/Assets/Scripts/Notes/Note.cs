using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
  public class Note
  {
    public TypeCadenceNote TypeCadenceNote { get; set; }

    public TypeNote TypeNote { get; set; }

    protected float Temps { get; set; }

    public bool IsPointe { get; set; }

    public GameObject Sprite { get; set; }

    public TypeGamme TypeGamme { get; set; }

    public TypeAlteration TypeAlteration { get; set; }

    public float GetTemps()
    {
      return Temps + (this.IsPointe ? this.Temps / 2 : 0);
    }
  }
}
