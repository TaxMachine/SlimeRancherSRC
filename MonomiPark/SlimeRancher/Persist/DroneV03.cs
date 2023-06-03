// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DroneV03
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DroneV03 : VersionedPersistedDataSet<DroneV02>
  {
    public Vector3V02 position;
    public Vector3V02 rotation;
    public AmmoDataV02 ammo;
    public List<DroneProgramV01> programs;
    public List<Identifiable.Id> fashions;
    public DroneStationV01 station;

    public override string Identifier => "SRDRONE";

    public override uint Version => 3;

    protected override void LoadData(BinaryReader reader)
    {
      position = LoadPersistable<Vector3V02>(reader);
      rotation = LoadPersistable<Vector3V02>(reader);
      ammo = LoadPersistable<AmmoDataV02>(reader);
      programs = LoadList<DroneProgramV01>(reader);
      fashions = LoadList(reader, (Func<int, Identifiable.Id>) (v => (Identifiable.Id) v));
      station = LoadPersistable<DroneStationV01>(reader);
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WritePersistable(writer, position);
      WritePersistable(writer, rotation);
      WritePersistable(writer, ammo);
      WriteList(writer, programs);
      WriteList(writer, fashions, v => (int) v);
      WritePersistable(writer, station);
    }

    protected override void UpgradeFrom(DroneV02 legacy)
    {
      position = legacy.position;
      rotation = legacy.rotation;
      ammo = legacy.ammo;
      programs = legacy.programs;
      fashions = legacy.fashions;
      station = new DroneStationV01();
    }

    public static void AssertAreEqual(DroneV03 expected, DroneV03 actual)
    {
      if (!TestUtil.AssertNullness(expected, actual))
        return;
      AmmoDataV02.AssertAreEqual(expected.ammo, actual.ammo);
      TestUtil.AssertAreEqual(expected.programs, actual.programs, (e, a, m) => DroneProgramV01.AssertAreEqual(e, a), "programs");
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
    }

    public static void AssertAreEqual(DroneV02 expected, DroneV03 actual)
    {
      if (!TestUtil.AssertNullness(expected, actual))
        return;
      AmmoDataV02.AssertAreEqual(expected.ammo, actual.ammo);
      TestUtil.AssertAreEqual(expected.programs, actual.programs, (e, a, m) => DroneProgramV01.AssertAreEqual(e, a), "programs");
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
      DroneStationV01.AssertAreEqual(new DroneStationV01(), actual.station);
    }
  }
}
