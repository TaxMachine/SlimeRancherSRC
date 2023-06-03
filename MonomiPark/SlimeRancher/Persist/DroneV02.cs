// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DroneV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DroneV02 : VersionedPersistedDataSet<DroneV01>
  {
    public Vector3V02 position;
    public Vector3V02 rotation;
    public AmmoDataV02 ammo;
    public List<DroneProgramV01> programs;
    public List<Identifiable.Id> fashions;

    public override string Identifier => "SRDRONE";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      position = LoadPersistable<Vector3V02>(reader);
      rotation = LoadPersistable<Vector3V02>(reader);
      ammo = LoadPersistable<AmmoDataV02>(reader);
      programs = LoadList<DroneProgramV01>(reader);
      fashions = LoadList(reader, (Func<int, Identifiable.Id>) (v => (Identifiable.Id) v));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WritePersistable(writer, position);
      WritePersistable(writer, rotation);
      WritePersistable(writer, ammo);
      WriteList(writer, programs);
      WriteList(writer, fashions, v => (int) v);
    }

    protected override void UpgradeFrom(DroneV01 legacy)
    {
      position = legacy.position;
      rotation = legacy.rotation;
      ammo = legacy.ammo;
      programs = legacy.programs;
      fashions = new List<Identifiable.Id>();
    }

    public static void AssertAreEqual(DroneV02 expected, DroneV02 actual)
    {
      if (!TestUtil.AssertNullness(expected, actual))
        return;
      AmmoDataV02.AssertAreEqual(expected.ammo, actual.ammo);
      TestUtil.AssertAreEqual(expected.programs, actual.programs, (e, a, m) => DroneProgramV01.AssertAreEqual(e, a), "programs");
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
    }

    public static void AssertAreEqual(DroneV01 expected, DroneV02 actual)
    {
      if (!TestUtil.AssertNullness(expected, actual))
        return;
      AmmoDataV02.AssertAreEqual(expected.ammo, actual.ammo);
      TestUtil.AssertAreEqual(expected.programs, actual.programs, (e, a, m) => DroneProgramV01.AssertAreEqual(e, a), "programs");
    }
  }
}
