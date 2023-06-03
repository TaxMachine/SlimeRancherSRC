// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.InstrumentV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class InstrumentV01 : PersistedDataSet
  {
    public List<InstrumentModel.Instrument> unlocks = new List<InstrumentModel.Instrument>();
    public InstrumentModel.Instrument selection = InstrumentModel.Instrument.NONE;

    public override string Identifier => "SRINSTR";

    public override uint Version => 1;

    public static InstrumentV01 Load(BinaryReader reader)
    {
      InstrumentV01 instrumentV01 = new InstrumentV01();
      instrumentV01.Load(reader.BaseStream);
      return instrumentV01;
    }

    protected override void LoadData(BinaryReader reader)
    {
      unlocks = LoadList(reader, (Func<int, InstrumentModel.Instrument>) (val => (InstrumentModel.Instrument) val));
      selection = (InstrumentModel.Instrument) reader.ReadInt32();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteList(writer, unlocks, instrument => (int) instrument);
      writer.Write((int) selection);
    }

    public static void AssertAreEqual(InstrumentV01 expected, InstrumentV01 actual) => TestUtil.AssertAreEqual(expected.unlocks, actual.unlocks);
  }
}
