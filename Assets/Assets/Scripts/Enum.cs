using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
  public enum Difficulte
  {
    Facile,
    Normal,
    Difficile,
    Beethoven,
    Dev = -1
  }

  public enum TypeAlteration
  {
    None,
    Becarre,
    Diese,
    Bemol
  }

  public enum TypeGamme
  {
    Basse,
    Normale,
    Haute
  }

  public enum TypeNote
  {
    Do,
    Re,
    Mi,
    Fa,
    Sol,
    La,
    Si
  }

  public enum TypeCadenceNote
  {
    Croche,
    Noire,
    Blanche,
    Ronde,
    DemiSoupir,
    Soupir,
    DemiPause,
    Pause
  }
}
