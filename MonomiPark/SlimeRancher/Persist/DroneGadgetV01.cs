// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DroneGadgetV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DroneGadgetV01 : PersistedDataSet
  {
    public DroneV05 drone = new DroneV05();
    public DroneStationV01 station = new DroneStationV01();
    public List<DroneProgramV01> programs = new List<DroneProgramV01>();

    public override string Identifier => "SRDRGD";

    public override uint Version => 1;

    public DroneGadgetV01()
    {
    }

    public DroneGadgetV01(DroneV04 drone)
    {
      this.drone = new DroneV05(drone);
      station = drone.station;
      programs = drone.programs;
    }

    protected override void LoadData(BinaryReader reader)
    {
      drone = LoadPersistable<DroneV05>(reader);
      station = LoadPersistable<DroneStationV01>(reader);
      programs = LoadList<DroneProgramV01>(reader);
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WritePersistable(writer, drone);
      WritePersistable(writer, station);
      WriteList(writer, programs);
    }

    public static void AssertAreEqual(DroneGadgetV01 expected, DroneGadgetV01 actual)
    {
      if (!TestUtil.AssertNullness(expected, actual))
        return;
      DroneV05.AssertAreEqual(expected.drone, actual.drone);
      DroneStationV01.AssertAreEqual(expected.station, actual.station);
      TestUtil.AssertAreEqual(expected.programs, actual.programs, (e, a, m) => DroneProgramV01.AssertAreEqual(e, a), "programs");
    }
  }
}
