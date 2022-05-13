using Assets.Scripts.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
  public static class EnumHelper
  {
    public static Note ToNote(this TypeCadenceNote typeNote)
    {
      switch (typeNote)
      {
        case TypeCadenceNote.Croche: return new Croche();
        case TypeCadenceNote.Noire: return new Noire();
        case TypeCadenceNote.Blanche: return new Blanche();
        case TypeCadenceNote.Ronde: return new Ronde();
        case TypeCadenceNote.DemiSoupir: return new DemiSoupir();
        case TypeCadenceNote.Soupir: return new Soupir();
        case TypeCadenceNote.DemiPause: return new DemiPause();
        case TypeCadenceNote.Pause: return new Pause();
      }

      return new Noire();
    }

    public static bool IsHampeInferieur(this TypeGamme typeGamme, TypeNote typeNote)
    {
      switch (typeGamme)
      {
        case TypeGamme.Basse: return false;
        case TypeGamme.Haute: return true;
        case TypeGamme.Normale:
        default:
          return typeNote > TypeNote.Sol;
      }
    }

    public static bool IsNote(this TypeCadenceNote typeCadenceNote)
    {
      switch (typeCadenceNote)
      {
        case TypeCadenceNote.Croche:
        case TypeCadenceNote.Noire:
        case TypeCadenceNote.Blanche:
        case TypeCadenceNote.Ronde:
          return true;
      }

      return false;
    }

    public static bool CanPointe(this TypeCadenceNote typeCadenceNote)
    {
      return typeCadenceNote == TypeCadenceNote.Noire || typeCadenceNote == TypeCadenceNote.Blanche;
    }
  }
}
