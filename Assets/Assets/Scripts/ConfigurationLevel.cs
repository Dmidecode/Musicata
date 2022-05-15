using Assets.Scripts;
using Assets.Scripts.Notes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class ConfigurationLevel
{
  public Difficulte Difficulte;
  public string Titre;
  public int NombreSystemeMainDroite;
  public int NombreSystemeMainGauche;
  public int SignatureDiese;
  public int SignatureBemol;
  public int Tempo;
  public int Temps;

  public Solution Solution;

  public ConfigurationLevel()
  {
  }

  public override string ToString()
  {
    StringBuilder sb = new StringBuilder();
    sb.AppendLine($"Titre: {Titre}");
    sb.AppendLine($"Difficulte: {Difficulte}");
    sb.AppendLine($"NombreSystemeMainDroite: {NombreSystemeMainDroite}");
    sb.AppendLine($"NombreSystemeMainGauche: {NombreSystemeMainGauche}");
    sb.AppendLine($"SignatureDiese: {SignatureDiese}");
    sb.AppendLine($"SignatureBemol: {SignatureBemol}");
    sb.AppendLine($"Tempo: {Tempo}");
    sb.AppendLine($"Temps: {Temps}");
    sb.AppendLine($"\tSolution main droite:");
    int index = 0;
    foreach (var mesure in Solution.MainDroite)
    {
      sb.AppendLine($"\t\tMesure {index++}: {{");
      foreach (var note in mesure.Notes)
      {
        sb.AppendLine($"\t\tNote: {{");
        sb.AppendLine($"\t\t\tTypeNote={note.TypeNote}");
        sb.AppendLine($"\t\t\tTypeGamme={note.TypeGamme}");
        sb.AppendLine($"\t\t\tTypeCadenceNote={note.TypeCadenceNote}");
        sb.AppendLine($"\t\t\tTypeAlteration={note.TypeAlteration}");
        sb.AppendLine($"\t\t\tIsPointe={note.IsPointe}");
        sb.AppendLine($"\t\t}}");
      }
    }

    index = 0;
    sb.AppendLine($"\tSolution main gauche:");
    foreach (var mesure in Solution.MainGauche)
    {
      sb.AppendLine($"\t\tMesure {index++}: {{");
      foreach (var note in mesure.Notes)
      {
        sb.AppendLine($"\t\tNote: {{");
        sb.AppendLine($"\t\t\tTypeNote={note.TypeNote}");
        sb.AppendLine($"\t\t\tTypeGamme={note.TypeGamme}");
        sb.AppendLine($"\t\t\tTypeCadenceNote={note.TypeCadenceNote}");
        sb.AppendLine($"\t\t\tTypeAlteration={note.TypeAlteration}");
        sb.AppendLine($"\t\t\tIsPointe={note.IsPointe}");
        sb.AppendLine($"\t\t}}");
      }
    }

    return sb.ToString();
  }
}

[Serializable]
public class Solution
{
  public List<ManageMesure> MainDroite;
  public List<ManageMesure> MainGauche;

  public Solution()
  {
    this.MainDroite = new List<ManageMesure>();
    this.MainGauche = new List<ManageMesure>();
  }
}
