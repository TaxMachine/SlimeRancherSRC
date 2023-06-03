// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.WorldV10
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MonomiPark.SlimeRancher.Persist
{
  public class WorldV10 : VersionedPersistedDataSet<WorldV09>
  {
    private const float MAX_DIST_MATCH = 5f;
    private const float MAX_DIST_MATCH_SQR = 25f;
    private const float MAX_DIST_CLOSE_MATCH = 0.1f;
    private const float MAX_DIST_CLOSE_MATCH_SQR = 0.0100000007f;
    public double worldTime;
    public float econSeed;
    public double dailyOfferCreateTime;
    public string lastRancherOfferId;
    public double weatherUntil;
    public AmbianceDirector.Weather weather;
    public ExchangeOfferV03 offer = new ExchangeOfferV03();
    public Dictionary<Identifiable.Id, float> econSaturations;
    public Dictionary<string, bool> teleportNodeActivations;
    public Dictionary<Vector3V02, double> animalSpawnerTimes;
    public Dictionary<Vector3V02, float> liquidSourceUnits;
    public Dictionary<Vector3V02, double> spawnerTriggerTimes;
    public Dictionary<string, int> gordoEatenCounts;
    public Dictionary<Vector3V02, ResourceWaterV03> resourceSpawnerWater;
    public Dictionary<string, PlacedGadgetV01> placedGadgets;

    public override string Identifier => "SRW";

    public override uint Version => 10;

    public WorldV10()
    {
    }

    public WorldV10(WorldV09 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      worldTime = reader.ReadDouble();
      econSeed = reader.ReadSingle();
      dailyOfferCreateTime = reader.ReadDouble();
      lastRancherOfferId = !reader.ReadBoolean() ? null : reader.ReadString();
      weatherUntil = reader.ReadDouble();
      weather = (AmbianceDirector.Weather) reader.ReadInt32();
      offer = new ExchangeOfferV03();
      offer.Load(reader.BaseStream);
      econSaturations = LoadDictionary(reader, r => (Identifiable.Id) r.ReadInt32(), r => r.ReadSingle());
      teleportNodeActivations = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      animalSpawnerTimes = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadDouble());
      liquidSourceUnits = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadSingle());
      spawnerTriggerTimes = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadDouble());
      gordoEatenCounts = LoadDictionary(reader, r => r.ReadString(), r => r.ReadInt32());
      resourceSpawnerWater = LoadDictionary(reader, r => Vector3V02.Load(r), r => ResourceWaterV03.Load(r));
      placedGadgets = LoadDictionary(reader, r => r.ReadString(), r => PlacedGadgetV01.Load(r));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(worldTime);
      writer.Write(econSeed);
      writer.Write(dailyOfferCreateTime);
      bool flag = lastRancherOfferId != null;
      writer.Write(flag);
      if (flag)
        writer.Write(lastRancherOfferId);
      writer.Write(weatherUntil);
      writer.Write((int) weather);
      offer.Write(writer.BaseStream);
      WriteDictionary(writer, econSaturations, (w, k) => w.Write((int) k), (w, v) => w.Write(v));
      WriteDictionary(writer, teleportNodeActivations, (w, k) => w.Write(k), (w, v) => w.Write(v));
      WriteDictionary(writer, animalSpawnerTimes, (w, key) => key.Write(w.BaseStream), (w, val) => w.Write(val));
      WriteDictionary(writer, liquidSourceUnits, (w, key) => key.Write(w.BaseStream), (w, val) => w.Write(val));
      WriteDictionary(writer, spawnerTriggerTimes, (w, key) => key.Write(w.BaseStream), (w, val) => w.Write(val));
      WriteDictionary(writer, gordoEatenCounts, (w, key) => w.Write(key), (w, val) => w.Write(val));
      WriteDictionary(writer, resourceSpawnerWater, (w, key) => key.Write(w.BaseStream), (w, val) => val.Write(w.BaseStream));
      WriteDictionary(writer, placedGadgets, (w, s) => w.Write(s), (w, pg) => pg.Write(w.BaseStream));
    }

    public static WorldV10 Load(BinaryReader reader)
    {
      WorldV10 worldV10 = new WorldV10();
      worldV10.Load(reader.BaseStream);
      return worldV10;
    }

    public static void AssertAreEqual(WorldV10 expected, WorldV10 actual)
    {
      ExchangeOfferV03.AssertAreEqual(expected.offer, actual.offer);
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(expected.animalSpawnerTimes, actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.liquidSourceUnits, actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
      TestUtil.AssertAreEqual(expected.spawnerTriggerTimes, actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(expected.gordoEatenCounts, actual.gordoEatenCounts, (e, a) => { }, "gordoEatenCounts");
      TestUtil.AssertAreEqual(expected.resourceSpawnerWater, actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV01.AssertAreEqual(e, a), "placedGadgets");
    }

    public static void AssertAreEqual(WorldV09 expected, WorldV10 actual)
    {
      ExchangeOfferV03.AssertAreEqual(expected.offer, actual.offer);
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(expected.animalSpawnerTimes, actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.liquidSourceUnits, actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
      TestUtil.AssertAreEqual(expected.spawnerTriggerTimes, actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(UpgradeGordoEatsFrom(expected.gordoEatenCounts), actual.gordoEatenCounts, (e, a) => { }, "gordoEatenCounts");
      TestUtil.AssertAreEqual(expected.resourceSpawnerWater, actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV01.AssertAreEqual(e, a), "placedGadgets");
    }

    protected override void UpgradeFrom(WorldV09 legacyData)
    {
      worldTime = legacyData.worldTime;
      econSeed = legacyData.econSeed;
      dailyOfferCreateTime = legacyData.dailyOfferCreateTime;
      lastRancherOfferId = legacyData.lastRancherOfferId;
      weatherUntil = legacyData.weatherUntil;
      weather = legacyData.weather;
      offer = legacyData.offer;
      econSaturations = legacyData.econSaturations;
      teleportNodeActivations = legacyData.teleportNodeActivations;
      animalSpawnerTimes = legacyData.animalSpawnerTimes;
      liquidSourceUnits = legacyData.liquidSourceUnits;
      spawnerTriggerTimes = legacyData.spawnerTriggerTimes;
      gordoEatenCounts = UpgradeGordoEatsFrom(legacyData.gordoEatenCounts);
      resourceSpawnerWater = legacyData.resourceSpawnerWater;
      placedGadgets = legacyData.placedGadgets;
    }

    public static Dictionary<string, int> UpgradeGordoEatsFrom(
      Dictionary<Vector3V02, int> legacyData)
    {
      Dictionary<Vector3, string> dictionary1 = new Dictionary<Vector3, string>();
      dictionary1[new Vector3(286.8f, -4.6f, 219.2f)] = "gordo0769818715";
      dictionary1[new Vector3(-172.2f, -4.6f, 124.4f)] = "gordo1173217994";
      dictionary1[new Vector3(-235.4f, -1.4f, -178.5f)] = "gordo0806598363";
      dictionary1[new Vector3(-108.9f, -0.6f, 14.6f)] = "gordo1686430858";
      dictionary1[new Vector3(228.2f, 5f, 147f)] = "gordo1831887310";
      dictionary1[new Vector3(414.7f, 4.4f, 395.7f)] = "gordo0983207065";
      dictionary1[new Vector3(-253.9f, -1.2f, 163.7f)] = "gordo0966530436";
      dictionary1[new Vector3(-445f, 9f, 392f)] = "gordo2083992877";
      Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
      foreach (KeyValuePair<Vector3V02, int> keyValuePair1 in legacyData)
      {
        Vector3 vector3 = keyValuePair1.Key.value;
        bool flag = false;
        foreach (KeyValuePair<Vector3, string> keyValuePair2 in dictionary1)
        {
          if ((vector3 - keyValuePair2.Key).sqrMagnitude < 100.0)
          {
            dictionary2[keyValuePair2.Value] = keyValuePair1.Value;
            flag = true;
            break;
          }
        }
        if (!flag)
          Log.Warning("Failed to find gordo match during upgrade: " + vector3);
      }
      return dictionary2;
    }
  }
}
