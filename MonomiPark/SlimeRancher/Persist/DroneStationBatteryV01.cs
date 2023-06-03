// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DroneStationBatteryV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DroneStationBatteryV01 : PersistedDataSet
  {
    public double time;

    public override string Identifier => "SRDRSTB";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader) => time = reader.ReadDouble();

    protected override void WriteData(BinaryWriter writer) => writer.Write(time);

    public static void AssertAreEqual(
      DroneStationBatteryV01 expected,
      DroneStationBatteryV01 actual)
    {
      TestUtil.AssertNullness(expected, actual);
    }

    public static DroneStationBatteryV01 GetSample() => new DroneStationBatteryV01()
    {
      time = 3200.0
    };
  }
}
