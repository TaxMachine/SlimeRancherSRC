// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.LandPlotV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class LandPlotV02 : PersistedDataSet
  {
    public float feederNextTime;
    public int feederPendingCount;
    public float collectorNextTime;
    public float fastforwarderDisableTime;
    public float attachedDeathTime;
    public LandPlot.Id id;
    public SpawnResource.Id attachedId;
    public Vector3V02 pos;
    public Vector3V02 rot;
    public List<LandPlot.Upgrade> upgrades;
    public Dictionary<SiloStorage.StorageType, List<AmmoDataV02>> siloAmmo = new Dictionary<SiloStorage.StorageType, List<AmmoDataV02>>();

    public override string Identifier => "SRLP";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      feederNextTime = reader.ReadSingle();
      feederPendingCount = reader.ReadInt32();
      collectorNextTime = reader.ReadSingle();
      fastforwarderDisableTime = reader.ReadSingle();
      attachedDeathTime = reader.ReadSingle();
      id = (LandPlot.Id) reader.ReadInt32();
      attachedId = (SpawnResource.Id) reader.ReadInt32();
      pos = Vector3V02.Load(reader);
      rot = Vector3V02.Load(reader);
      upgrades = LoadList(reader, (Func<int, LandPlot.Upgrade>) (v => (LandPlot.Upgrade) v));
      siloAmmo = LoadDictionary(reader, r => (SiloStorage.StorageType) r.ReadInt32(), r => LoadList<AmmoDataV02>(reader));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(feederNextTime);
      writer.Write(feederPendingCount);
      writer.Write(collectorNextTime);
      writer.Write(fastforwarderDisableTime);
      writer.Write(attachedDeathTime);
      writer.Write((int) id);
      writer.Write((int) attachedId);
      pos.Write(writer.BaseStream);
      rot.Write(writer.BaseStream);
      WriteList(writer, upgrades, v => (int) v);
      WriteDictionary(writer, siloAmmo, (w, key) => w.Write((int) key), (w, val) => WriteList(w, val));
    }

    public static void AssertAreEqual(LandPlotV02 expected, LandPlotV02 actual)
    {
      TestUtil.AssertAreEqual(expected.upgrades, actual.upgrades, "upgrades");
      TestUtil.AssertAreEqual(expected.siloAmmo, actual.siloAmmo, (e, a) => AssertAreEqual(e, a), "siloAmmo");
    }

    private static void AssertAreEqual(List<AmmoDataV02> expected, List<AmmoDataV02> actual)
    {
      for (int index = 0; index < expected.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected[index], actual[index]);
    }
  }
}
