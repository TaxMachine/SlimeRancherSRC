// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GlitchSlimeDataV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GlitchSlimeDataV01 : PersistedDataSet
  {
    public float exposureChance;
    public double deathTime;

    public override string Identifier => "SRAD_GS";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader)
    {
      exposureChance = reader.ReadSingle();
      deathTime = reader.ReadDouble();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(exposureChance);
      writer.Write(deathTime);
    }

    public static void AssertAreEqual(GlitchSlimeDataV01 expected, GlitchSlimeDataV01 actual)
    {
    }
  }
}
