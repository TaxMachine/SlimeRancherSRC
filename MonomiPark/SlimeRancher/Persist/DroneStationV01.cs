// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DroneStationV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DroneStationV01 : PersistedDataSet
  {
    public DroneStationBatteryV01 battery = new DroneStationBatteryV01();

    public override string Identifier => "SRDRST";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader) => battery = LoadPersistable<DroneStationBatteryV01>(reader);

    protected override void WriteData(BinaryWriter writer) => WritePersistable(writer, battery);

    public static void AssertAreEqual(DroneStationV01 expected, DroneStationV01 actual)
    {
      if (!TestUtil.AssertNullness(expected, actual))
        return;
      DroneStationBatteryV01.AssertAreEqual(expected.battery, actual.battery);
    }
  }
}
