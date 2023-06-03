// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.AmmoDataV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class AmmoDataV02 : PersistedDataSet
  {
    public Identifiable.Id id;
    public SlimeEmotionDataV02 emotionData;
    public int count;

    public override string Identifier => "SRAD";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      id = (Identifiable.Id) reader.ReadInt32();
      count = reader.ReadInt32();
      emotionData = new SlimeEmotionDataV02();
      emotionData.Load(reader.BaseStream);
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) id);
      writer.Write(count);
      emotionData.Write(writer.BaseStream);
    }

    public static void AssertAreEqual(AmmoDataV02 expected, AmmoDataV02 actual) => SlimeEmotionDataV02.AssertAreEqual(expected.emotionData, actual.emotionData);

    public static void AssertAreEqual(List<AmmoDataV02> expected, List<AmmoDataV02> actual) => TestUtil.AssertAreEqual(expected, actual, (a, b, m) => AssertAreEqual(a, b));
  }
}
