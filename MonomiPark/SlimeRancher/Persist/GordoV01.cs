// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GordoV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GordoV01 : PersistedDataSet
  {
    public int eatenCount;
    public List<Identifiable.Id> fashions = new List<Identifiable.Id>();

    public override string Identifier => "SRG";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader)
    {
      eatenCount = reader.ReadInt32();
      fashions = LoadList(reader, (Func<int, Identifiable.Id>) (v => (Identifiable.Id) v));
    }

    public static GordoV01 Load(BinaryReader reader)
    {
      GordoV01 gordoV01 = new GordoV01();
      gordoV01.Load(reader.BaseStream);
      return gordoV01;
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(eatenCount);
      WriteList(writer, fashions, v => (int) v);
    }

    public static void AssertAreEqual(GordoV01 expected, GordoV01 actual) => TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
  }
}
