using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using System;
using System.IO;

public class CreateMidi : MonoBehaviour
{
  MidiFileWriter2 fileWriter;

  int channel;
  int track;

  long time;

  int ticksPerNoire;
  int ticksPerCroche;
  int ticksPerBlanche;
  int ticksPerRonde;


  //int doo = 60;
  //int re = 62;
  //int mi = 64;
  //int fa = 65;
  //int sol = 67;
  //int la = 69;
  //int si = 71;
  //int doAigu = 72;
  //int track1 = 1;
  //int channel0 = 0;
  //int volume = 100;


  public CreateMidi()
  {
    fileWriter = new MidiFileWriter2(); 
    fileWriter.MPTK_AddText(0, 0, MPTKMeta.Copyright, "Generate Midi Stream");
  }

  public void Init(int tempo)
  {
    fileWriter.MPTK_AddBPMChange(0, 0, tempo);

    channel = 0;
    track = 1;
    time = 0;

    ticksPerNoire = fileWriter.MPTK_DeltaTicksPerQuarterNote;
    ticksPerCroche = ticksPerNoire / 2;
    ticksPerBlanche = ticksPerNoire * 2;
    ticksPerRonde = ticksPerBlanche * 2;
  }

  public void AddNote(int value, int volume, int duration)
  {
    fileWriter.MPTK_AddNote(track, time, channel, value, volume, duration);
    time += duration;
  }

  public int GetTicksPerNoire()
  {
    return ticksPerNoire;
  }

  public void PlayMidiSequence()
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
      //Debug.Log($"End playing {midiname} {reason} Duration={(DateTime.Now - startPlaying).TotalSeconds:F3}");
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
}
