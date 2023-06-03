// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.TreasurePodV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class TreasurePodV01 : PersistedDataSet
  {
    public TreasurePod.State state;
    public List<Identifiable.Id> spawnQueue = new List<Identifiable.Id>();

    public override string Identifier => "SRTP";

    public override uint Version => 1;

    public static TreasurePodV01 Load(BinaryReader reader)
    {
      TreasurePodV01 treasurePodV01 = new TreasurePodV01();
      treasurePodV01.Load(reader.BaseStream);
      return treasurePodV01;
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) state);
      WriteList(writer, spawnQueue, id => (int) id);
    }

    protected override void LoadData(BinaryReader reader)
    {
      state = (TreasurePod.State) reader.ReadInt32();
      spawnQueue = LoadList(reader, (Func<int, Identifiable.Id>) (id => (Identifiable.Id) id));
    }

    public static void AssertAreEqual(TreasurePodV01 expected, TreasurePodV01 actual) => TestUtil.AssertAreEqual(expected.spawnQueue, actual.spawnQueue, "spawnQueue");
  }
}
