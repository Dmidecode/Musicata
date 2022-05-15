using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using System;
using System.IO;
using Assets.Scripts;
using Assets.Scripts.Notes;
using System.Linq;

public class ManageMidi : MonoBehaviour
{
  MidiFileWriter2 fileWriter;

  int channel;
  int track;

  long tickMainDroite;
  long tickMainGauche;

  int ticksPerNoire;
  int ticksPerCroche;
  int ticksPerBlanche;
  int ticksPerRonde;

  Dictionary<TypeNote, int> mainDroiteMappingMidi;
  Dictionary<TypeNote, int> mainGaucheMappingMidi;

  public void Awake()
  {
    fileWriter = new MidiFileWriter2();
    fileWriter.MPTK_AddText(0, 0, MPTKMeta.Copyright, "Generate Midi Stream");

    mainDroiteMappingMidi = new Dictionary<TypeNote, int>()
    {
      { TypeNote.Do, 60 },
      { TypeNote.Re, 62 },
      { TypeNote.Mi, 64 },
      { TypeNote.Fa, 65 },
      { TypeNote.Sol, 67 },
      { TypeNote.La, 69 },
      { TypeNote.Si, 71 },
    };

    mainGaucheMappingMidi = new Dictionary<TypeNote, int>()
    {
      { TypeNote.Do, 48 },
      { TypeNote.Re, 50 },
      { TypeNote.Mi, 52 },
      { TypeNote.Fa, 53 },
      { TypeNote.Sol, 55 },
      { TypeNote.La, 57 },
      { TypeNote.Si, 59 },
    };
  }

  public void Init(int tempo)
  {
    fileWriter.MPTK_AddBPMChange(0, 0, tempo);

    channel = 0;
    track = 1;
    tickMainDroite = 0;
    tickMainGauche = 0;

    ticksPerNoire = fileWriter.MPTK_DeltaTicksPerQuarterNote;
    ticksPerCroche = ticksPerNoire / 2;
    ticksPerBlanche = ticksPerNoire * 2;
    ticksPerRonde = ticksPerBlanche * 2;
  }

  public void ListenAnswer(Action callback)
  {
    Init(GameManager.Instance.GetTempo());
    var mesuresMainDroite = Compositeur.Instance.GetMesuresMainDroite();
    var mesuresMainGauche = Compositeur.Instance.GetMesuresMainGauche();
    AddNoteMidi(mesuresMainDroite, tickMainDroite);
    AddNoteMidi(mesuresMainGauche, tickMainGauche);

    PlayMidiSequence(callback);
  }

  private void AddNoteMidi(ManageMesure[] mesures, long tick)
  {
    for (int i = 0; i < mesures.Length; i += 1)
    {
      var mesure = mesures[i];
      foreach (var note in mesure.GetNotes())
        tick = AddNote(tick, GetNoteValueMidi(mesure, note), note.TypeCadenceNote.IsNote() ? 100 : 0, GetNoteCadenceMidi(note));
    }
  }

  public long AddNote(long tick, int value, int volume, int duration)
  {
    fileWriter.MPTK_AddNote(track, tick, channel, value, volume, duration);
    return tick + duration;
  }

  public void PlayMidiSequence(Action callback)
  {
    MidiFilePlayer midiPlayer = FindObjectOfType<MidiFilePlayer>();

    midiPlayer.MPTK_Stop();
    fileWriter.MPTK_MidiName = "level";

    midiPlayer.OnEventStartPlayMidi.RemoveAllListeners();
    midiPlayer.OnEventStartPlayMidi.AddListener((string midiname) =>
    {
      //  startPlaying = DateTime.Now;
      //  Debug.Log($"Start playing {midiname} at {startPlaying}");
    });

    midiPlayer.OnEventEndPlayMidi.RemoveAllListeners();
    midiPlayer.OnEventEndPlayMidi.AddListener((string midiname, EventEndMidiEnum reason) =>
    {
      callback();
    });

    midiPlayer.OnEventNotesMidi.RemoveAllListeners();
    midiPlayer.OnEventNotesMidi.AddListener((List<MPTKEvent> events) =>
    {
      //foreach (MPTKEvent midievent in events)
      //  Debug.Log($"At {midievent.RealTime:F1} ms play: {midievent.ToString()}");
    });

    fileWriter.MPTK_SortEvents();
    fileWriter.MPTK_Debug();

    midiPlayer.MPTK_Play(fileWriter);
  }

  private void WriteMidiSequenceToFile(string name, MidiFileWriter2 mfw)
  {
    // build the path + filename to the midi
    string filename = Path.Combine(Application.persistentDataPath, name + ".mid");
    Debug.Log("Write Midi file:" + filename);

    // Sort the events by ascending absolute time (optional)
    mfw.MPTK_SortEvents();
    mfw.MPTK_Debug();

    // Write the midi file
    mfw.MPTK_WriteToFile(filename);
  }

  private int GetNoteCadenceMidi(Note note)
  {
    int duration = 0;
    switch (note.TypeCadenceNote)
    {
      case TypeCadenceNote.Croche:
      case TypeCadenceNote.DemiSoupir:
        duration = ticksPerCroche;
        break;
      case TypeCadenceNote.Blanche:
      case TypeCadenceNote.DemiPause:
        duration = ticksPerBlanche;
        break;
      case TypeCadenceNote.Ronde:
      case TypeCadenceNote.Pause:
        duration = ticksPerRonde;
        break;
      case TypeCadenceNote.Noire:
      case TypeCadenceNote.Soupir:
      default:
        duration = ticksPerNoire;
        break;
    }

    if (note.IsPointe)
      duration += duration / 2;

    return duration;
  }

  private int GetNoteValueMidi(ManageMesure mesure, Note note)
  {
    Dictionary<TypeNote, int> mappingMidi = mesure.IsMainDroite() ? mainDroiteMappingMidi : mainGaucheMappingMidi;

    int deltaGamme = 0;
    if (note.TypeGamme == TypeGamme.Basse)
      deltaGamme = -12;
    else if (note.TypeGamme == TypeGamme.Haute)
      deltaGamme = 12;

    int valueNote = mappingMidi[note.TypeNote] + deltaGamme;

    // On parcourt les altérations du système pour vérifier si notre note est dedans
    var alteration = GameManager.Instance.GetAlterations().FirstOrDefault(x => x.TypeNote == note.TypeNote);
    if (alteration != null && note.TypeAlteration != TypeAlteration.Becarre)
    {
      if (alteration.TypeAlteration == TypeAlteration.Diese)
        valueNote += 1;
      else
        valueNote -= 1;
    }

    // On applique l'altération de la note
    if (note.TypeAlteration == TypeAlteration.Diese)
      valueNote += 1;
    else
      valueNote -= 1;

    return valueNote;
  }
}
