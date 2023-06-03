// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.QuicksilverEnergyGeneratorV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class QuicksilverEnergyGeneratorV01 : PersistedDataSet
  {
    public QuicksilverEnergyGenerator.State state;
    public float? timer;

    public override string Identifier => "SRQSEG";

    public override uint Version => 1;

    public static QuicksilverEnergyGeneratorV01 Load(BinaryReader reader)
    {
      QuicksilverEnergyGeneratorV01 energyGeneratorV01 = new QuicksilverEnergyGeneratorV01();
      energyGeneratorV01.Load(reader.BaseStream);
      return energyGeneratorV01;
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) state);
      WriteNullable(writer, timer);
    }

    protected override void LoadData(BinaryReader reader)
    {
      state = (QuicksilverEnergyGenerator.State) reader.ReadInt32();
      LoadNullable(reader, out timer);
    }

    public static void AssertAreEqual(
      QuicksilverEnergyGeneratorV01 expected,
      QuicksilverEnergyGeneratorV01 actual)
    {
    }
  }
}
