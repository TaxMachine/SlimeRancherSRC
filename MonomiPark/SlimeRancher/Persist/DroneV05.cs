// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DroneV05
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DroneV05 : VersionedPersistedDataSet<DroneV04>
  {
    public Vector3V02 position = new Vector3V02();
    public Vector3V02 rotation = new Vector3V02();
    public AmmoDataV02 ammo;
    public List<Identifiable.Id> fashions;
    public bool noClip;

    public override string Identifier => "SRDRONE";

    public override uint Version => 5;

    public DroneV05()
    {
    }

    public DroneV05(DroneV04 legacy) => UpgradeFrom(legacy);

    protected override void LoadData(BinaryReader reader)
    {
      position = LoadPersistable<Vector3V02>(reader);
      rotation = LoadPersistable<Vector3V02>(reader);
      ammo = LoadPersistable<AmmoDataV02>(reader);
      fashions = LoadList(reader, (Func<int, Identifiable.Id>) (v => (Identifiable.Id) v));
      noClip = reader.ReadBoolean();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WritePersistable(writer, position);
      WritePersistable(writer, rotation);
      WritePersistable(writer, ammo);
      WriteList(writer, fashions, v => (int) v);
      writer.Write(noClip);
    }

    protected override void UpgradeFrom(DroneV04 legacy)
    {
      position = legacy.position;
      rotation = legacy.rotation;
      ammo = legacy.ammo;
      fashions = legacy.fashions;
      noClip = legacy.noClip;
    }

    public static void AssertAreEqual(DroneV05 expected, DroneV05 actual)
    {
      if (!TestUtil.AssertNullness(expected, actual))
        return;
      AmmoDataV02.AssertAreEqual(expected.ammo, actual.ammo);
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
    }

    public static void AssertAreEqual(DroneV04 expected, DroneV05 actual)
    {
      if (!TestUtil.AssertNullness(expected, actual))
        return;
      AmmoDataV02.AssertAreEqual(expected.ammo, actual.ammo);
      TestUtil.AssertAreEqual(expected.fashions, actual.fashions, "fashions");
    }
  }
}
