// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.InstrumentModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class InstrumentModel
  {
    private Participant participant;
    private HashSet<Instrument> instrumentUnlocks = new HashSet<Instrument>();
    private Instrument instrumentSelection;

    public void SetParticipant(Participant participant) => this.participant = this.participant == null ? participant : throw new Exception("Replacing participant on InstrumentModel");

    public void Init() => participant?.InitModel(this);

    public void NotifyParticipants() => participant?.SetModel(this);

    public void UnlockInstrument(Instrument instrument) => instrumentUnlocks.Add(instrument);

    public void SelectInstrument(Instrument instrument) => instrumentSelection = instrument;

    public HashSet<Instrument> GetUnlockedInstruments() => instrumentUnlocks;

    public Instrument GetCurrentlySelectedInstrument() => instrumentSelection;

    public void Push(InstrumentV01 persistence)
    {
      instrumentSelection = persistence.selection;
      instrumentUnlocks = new HashSet<Instrument>(persistence.unlocks);
    }

    public InstrumentV01 Pull() => new InstrumentV01()
    {
      selection = instrumentSelection,
      unlocks = instrumentUnlocks.ToList()
    };

    public enum Instrument
    {
      NONE = -1, // 0xFFFFFFFF
      MARIMBA = 0,
      BELLS = 1,
      CORA = 2,
      GLOCK = 3,
      GLOCK_LOW = 4,
      MUSIC_BOX = 5,
      ORGAN = 6,
      PIANO = 7,
      PIANO_ELECTRIC = 8,
      PLUNKY = 9,
      SINGING_LONG = 10, // 0x0000000A
      SINGING_SHORT = 11, // 0x0000000B
      VIBE = 12, // 0x0000000C
      VIBE_LOW = 13, // 0x0000000D
      CHIPTUNE = 14, // 0x0000000E
      VIOLIN = 15, // 0x0000000F
      FLUTE = 16, // 0x00000010
    }

    public interface Participant
    {
      void InitModel(InstrumentModel model);

      void SetModel(InstrumentModel model);
    }
  }
}
