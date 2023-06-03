// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.LandPlotV05
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MonomiPark.SlimeRancher.Persist
{
  public class LandPlotV05 : VersionedPersistedDataSet<LandPlotV04>
  {
    public double feederNextTime;
    public int feederPendingCount;
    public SlimeFeeder.FeedSpeed feederCycleSpeed;
    public double collectorNextTime;
    public double fastforwarderDisableTime;
    public double attachedDeathTime;
    public LandPlot.Id typeId;
    public SpawnResource.Id attachedId;
    public string id;
    public List<LandPlot.Upgrade> upgrades;
    public Dictionary<SiloStorage.StorageType, List<AmmoDataV02>> siloAmmo = new Dictionary<SiloStorage.StorageType, List<AmmoDataV02>>();

    public override string Identifier => "SRLP";

    public override uint Version => 5;

    public LandPlotV05()
    {
    }

    public LandPlotV05(LandPlotV04 legacyData) => UpgradeFromLegacy(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      feederNextTime = reader.ReadDouble();
      feederPendingCount = reader.ReadInt32();
      feederCycleSpeed = (SlimeFeeder.FeedSpeed) reader.ReadInt32();
      collectorNextTime = reader.ReadDouble();
      fastforwarderDisableTime = reader.ReadDouble();
      attachedDeathTime = reader.ReadDouble();
      typeId = (LandPlot.Id) reader.ReadInt32();
      attachedId = (SpawnResource.Id) reader.ReadInt32();
      id = reader.ReadString();
      upgrades = LoadList(reader, (Func<int, LandPlot.Upgrade>) (v => (LandPlot.Upgrade) v));
      siloAmmo = LoadDictionary(reader, r => (SiloStorage.StorageType) r.ReadInt32(), r => LoadList<AmmoDataV02>(reader));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(feederNextTime);
      writer.Write(feederPendingCount);
      writer.Write((int) feederCycleSpeed);
      writer.Write(collectorNextTime);
      writer.Write(fastforwarderDisableTime);
      writer.Write(attachedDeathTime);
      writer.Write((int) typeId);
      writer.Write((int) attachedId);
      writer.Write(id);
      WriteList(writer, upgrades, v => (int) v);
      WriteDictionary(writer, siloAmmo, (w, key) => w.Write((int) key), (w, val) => WriteList(w, val));
    }

    protected override void UpgradeFrom(LandPlotV04 legacyData)
    {
      feederNextTime = legacyData.feederNextTime;
      feederPendingCount = legacyData.feederPendingCount;
      feederCycleSpeed = SlimeFeeder.FeedSpeed.Normal;
      collectorNextTime = legacyData.collectorNextTime;
      fastforwarderDisableTime = legacyData.fastforwarderDisableTime;
      attachedDeathTime = legacyData.attachedDeathTime;
      typeId = legacyData.typeId;
      attachedId = legacyData.attachedId;
      id = legacyData.id;
      upgrades = legacyData.upgrades;
      siloAmmo = legacyData.siloAmmo;
    }

    public void UpgradeFromLegacy(LandPlotV04 legacyData) => UpgradeFrom(legacyData);

    public static List<LandPlotV05> UpgradeList(List<LandPlotV04> legacyList)
    {
      List<LandPlotV05> landPlotV05List = new List<LandPlotV05>();
      foreach (LandPlotV04 legacy in legacyList)
      {
        LandPlotV05 landPlotV05 = new LandPlotV05();
        landPlotV05.UpgradeFrom(legacy);
        landPlotV05List.Add(landPlotV05);
      }
      return landPlotV05List;
    }

    public static List<LandPlotV05> UpgradeList(List<LandPlotV03> legacyList)
    {
      List<LandPlotV05> landPlotV05List = new List<LandPlotV05>();
      foreach (LandPlotV03 legacy in legacyList)
      {
        LandPlotV04 legacyData = new LandPlotV04();
        legacyData.UpgradeFromLegacy(legacy);
        LandPlotV05 landPlotV05 = new LandPlotV05();
        landPlotV05.UpgradeFrom(legacyData);
        landPlotV05List.Add(landPlotV05);
      }
      return landPlotV05List;
    }

    public static void AssertAreEqual(LandPlotV05 expected, LandPlotV05 actual)
    {
      TestUtil.AssertAreEqual(expected.upgrades, actual.upgrades, "upgrades");
      TestUtil.AssertAreEqual(expected.siloAmmo, actual.siloAmmo, (e, a) => AssertAreEqual(e, a), "siloAmmo");
    }

    public static void AssertAreEqual(LandPlotV04 expected, LandPlotV05 actual)
    {
      TestUtil.AssertAreEqual(expected.upgrades, actual.upgrades, "upgrades");
      TestUtil.AssertAreEqual(expected.siloAmmo, actual.siloAmmo, (e, a) => AssertAreEqual(e, a), "siloAmmo");
    }

    public static void AssertAreEqual(LandPlotV03 expected, LandPlotV05 actual)
    {
      TestUtil.AssertAreEqual(expected.upgrades, actual.upgrades, "upgrades");
      TestUtil.AssertAreEqual(expected.siloAmmo, actual.siloAmmo, (e, a) => AssertAreEqual(e, a), "siloAmmo");
    }

    private static void AssertAreEqual(List<AmmoDataV02> expected, List<AmmoDataV02> actual)
    {
      for (int index = 0; index < expected.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected[index], actual[index]);
    }

    private static string GetIdFromPos(Vector3V02 pos)
    {
      foreach (KeyValuePair<Vector3, string> keyValuePair in new Dictionary<Vector3, string>()
      {
        [new Vector3(249.4f, 8.1f, -110.5f)] = "plot1997557426",
        [new Vector3(113.8f, 14.2f, -152f)] = "plot0359741256",
        [new Vector3(130.8f, 14.2f, -152f)] = "plot1671111819",
        [new Vector3(113.8f, 12.3f, -116f)] = "plot1796182480",
        [new Vector3(-74.5f, 13.5f, -171.6f)] = "plot1396469869",
        [new Vector3(268.1f, 8.1f, -121.2f)] = "plot2072834083",
        [new Vector3(266.4f, 8.5f, -152.4f)] = "plot0129758657",
        [new Vector3(-55.4f, 12.3f, -130.9f)] = "plot1511468234",
        [new Vector3(113.8f, 12.3f, -99f)] = "plot1924356083",
        [new Vector3(130.8f, 12.3f, -99f)] = "plot0701059634",
        [new Vector3(130.8f, 12.3f, -116f)] = "plot1585829687",
        [new Vector3(221.2f, 12.1f, -115.2f)] = "plot0242645904",
        [new Vector3(227.5f, 12.2f, -144.7f)] = "plot0713110432",
        [new Vector3(64.3f, 12.3f, -116f)] = "plot0206229813",
        [new Vector3(64.3f, 12.3f, -98.9f)] = "plot1516838724",
        [new Vector3(-28.6f, 12.3f, -132.3f)] = "plot0958727463",
        [new Vector3(-25.4f, 12.3f, -169.9f)] = "plot0587970576"
      })
      {
        if ((pos.value - keyValuePair.Key).sqrMagnitude < 100.0)
          return keyValuePair.Value;
      }
      Log.Warning("Unknown land plot position, cannot find ID", nameof (pos), pos.value);
      return "Unknown";
    }
  }
}
