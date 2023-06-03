﻿// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.LandPlotV08
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class LandPlotV08 : VersionedPersistedDataSet<LandPlotV07>
  {
    public double feederNextTime;
    public int feederPendingCount;
    public SlimeFeeder.FeedSpeed feederCycleSpeed;
    public double collectorNextTime;
    public double attachedDeathTime;
    public LandPlot.Id typeId;
    public SpawnResource.Id attachedId;
    public string id;
    public List<LandPlot.Upgrade> upgrades;
    public Dictionary<SiloStorage.StorageType, List<AmmoDataV02>> siloAmmo = new Dictionary<SiloStorage.StorageType, List<AmmoDataV02>>();
    public List<int> siloActivatorIndices = new List<int>();
    public float ashUnits;

    public override string Identifier => "SRLP";

    public override uint Version => 8;

    public LandPlotV08()
    {
    }

    public LandPlotV08(LandPlotV07 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      feederNextTime = reader.ReadDouble();
      feederPendingCount = reader.ReadInt32();
      feederCycleSpeed = (SlimeFeeder.FeedSpeed) reader.ReadInt32();
      collectorNextTime = reader.ReadDouble();
      attachedDeathTime = reader.ReadDouble();
      typeId = (LandPlot.Id) reader.ReadInt32();
      attachedId = (SpawnResource.Id) reader.ReadInt32();
      id = reader.ReadString();
      upgrades = LoadList(reader, (Func<int, LandPlot.Upgrade>) (v => (LandPlot.Upgrade) v));
      siloAmmo = LoadDictionary(reader, r => (SiloStorage.StorageType) r.ReadInt32(), r => LoadList<AmmoDataV02>(reader));
      siloActivatorIndices = LoadList(reader, (Func<int, int>) (i => i));
      ashUnits = reader.ReadSingle();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(feederNextTime);
      writer.Write(feederPendingCount);
      writer.Write((int) feederCycleSpeed);
      writer.Write(collectorNextTime);
      writer.Write(attachedDeathTime);
      writer.Write((int) typeId);
      writer.Write((int) attachedId);
      writer.Write(id);
      WriteList(writer, upgrades, v => (int) v);
      WriteDictionary(writer, siloAmmo, (w, key) => w.Write((int) key), (w, val) => WriteList(w, val));
      WriteList(writer, siloActivatorIndices, i => i);
      writer.Write(ashUnits);
    }

    public static void AssertAreEqual(LandPlotV08 expected, LandPlotV08 actual)
    {
      TestUtil.AssertAreEqual(expected.upgrades, actual.upgrades, "upgrades");
      TestUtil.AssertAreEqual(expected.siloAmmo, actual.siloAmmo, (e, a) => AmmoDataV02.AssertAreEqual(e, a), "siloAmmo typeId: " + actual.typeId);
      TestUtil.AssertAreEqual(expected.siloActivatorIndices, actual.siloActivatorIndices, "siloActivatorIndices");
    }

    public static void AssertAreEqual(LandPlotV07 expected, LandPlotV08 actual)
    {
      TestUtil.AssertAreEqual(expected.upgrades, actual.upgrades, "upgrades");
      TestUtil.AssertAreEqual(expected.siloAmmo, actual.siloAmmo, (e, a) => AmmoDataV02.AssertAreEqual(e, a), "siloAmmo");
      TestUtil.AssertAreEqual(expected.siloActivatorIndices, actual.siloActivatorIndices, "siloActivatorIndices");
    }

    protected override void UpgradeFrom(LandPlotV07 legacyData)
    {
      feederNextTime = legacyData.feederNextTime;
      feederPendingCount = legacyData.feederPendingCount;
      feederCycleSpeed = legacyData.feederCycleSpeed;
      collectorNextTime = legacyData.collectorNextTime;
      attachedDeathTime = legacyData.attachedDeathTime;
      typeId = legacyData.typeId;
      attachedId = legacyData.attachedId;
      id = legacyData.id;
      upgrades = legacyData.upgrades;
      siloAmmo = legacyData.siloAmmo;
      siloActivatorIndices = legacyData.siloActivatorIndices;
      feederCycleSpeed = legacyData.feederCycleSpeed;
      ashUnits = UpgradeAshUnitsForType(typeId);
    }

    private static float UpgradeAshUnitsForType(LandPlot.Id typeId) => typeId == LandPlot.Id.INCINERATOR ? 20f : 0.0f;
  }
}
