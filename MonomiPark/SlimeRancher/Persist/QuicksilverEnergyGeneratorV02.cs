// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.QuicksilverEnergyGeneratorV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class QuicksilverEnergyGeneratorV02 : 
    VersionedPersistedDataSet<QuicksilverEnergyGeneratorV01>
  {
    public QuicksilverEnergyGenerator.State state;
    public double? timer;

    public override string Identifier => "SRQSEG";

    public override uint Version => 2;

    public QuicksilverEnergyGeneratorV02()
    {
    }

    public QuicksilverEnergyGeneratorV02(QuicksilverEnergyGeneratorV01 legacy) => UpgradeFrom(legacy);

    public static QuicksilverEnergyGeneratorV02 Load(BinaryReader reader)
    {
      QuicksilverEnergyGeneratorV02 energyGeneratorV02 = new QuicksilverEnergyGeneratorV02();
      energyGeneratorV02.Load(reader.BaseStream);
      return energyGeneratorV02;
    }

    protected override void LoadData(BinaryReader reader)
    {
      state = (QuicksilverEnergyGenerator.State) reader.ReadInt32();
      LoadNullable(reader, out timer);
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) state);
      WriteNullable(writer, timer);
    }

    protected override void UpgradeFrom(QuicksilverEnergyGeneratorV01 legacy)
    {
      state = legacy.state;
      float? timer = legacy.timer;
      this.timer = timer.HasValue ? new double?(timer.GetValueOrDefault()) : new double?();
    }

    public static void AssertAreEqual(
      QuicksilverEnergyGeneratorV02 expected,
      QuicksilverEnergyGeneratorV02 actual)
    {
    }

    public static void AssertAreEqual(
      QuicksilverEnergyGeneratorV01 expected,
      QuicksilverEnergyGeneratorV02 actual)
    {
    }
  }
}
