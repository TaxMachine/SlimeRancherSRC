// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.WorldV09
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class WorldV09 : VersionedPersistedDataSet<WorldV08>
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
    public Dictionary<Vector3V02, int> gordoEatenCounts;
    public Dictionary<Vector3V02, ResourceWaterV03> resourceSpawnerWater;
    public Dictionary<string, PlacedGadgetV01> placedGadgets;

    public override string Identifier => "SRW";

    public override uint Version => 9;

    public WorldV09()
    {
    }

    public WorldV09(WorldV08 legacyData) => UpgradeFrom(legacyData);

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
      gordoEatenCounts = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadInt32());
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
      WriteDictionary(writer, gordoEatenCounts, (w, key) => key.Write(w.BaseStream), (w, val) => w.Write(val));
      WriteDictionary(writer, resourceSpawnerWater, (w, key) => key.Write(w.BaseStream), (w, val) => val.Write(w.BaseStream));
      WriteDictionary(writer, placedGadgets, (w, s) => w.Write(s), (w, pg) => pg.Write(w.BaseStream));
    }

    public static WorldV09 Load(BinaryReader reader)
    {
      WorldV09 worldV09 = new WorldV09();
      worldV09.Load(reader.BaseStream);
      return worldV09;
    }

    public static void AssertAreEqual(WorldV09 expected, WorldV09 actual)
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

    public static void AssertAreEqual(WorldV08 expected, WorldV09 actual)
    {
      ExchangeOfferV03.AssertAreEqual(expected.offer, actual.offer);
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(UpgradeFrom(expected.animalSpawnerTimes), actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.liquidSourceUnits, actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
      TestUtil.AssertAreEqual(UpgradeFrom(expected.spawnerTriggerTimes), actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(expected.gordoEatenCounts, actual.gordoEatenCounts, (e, a) => { }, "gordoEatenCounts");
      TestUtil.AssertAreEqual(UpgradeFrom(expected.resourceSpawnerWater), actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV01.AssertAreEqual(e, a), "placedGadgets");
    }

    protected override void UpgradeFrom(WorldV08 legacyData)
    {
      worldTime = legacyData.worldTime;
      econSeed = legacyData.econSeed;
      dailyOfferCreateTime = legacyData.dailyOfferCreateTime;
      lastRancherOfferId = legacyData.lastRancherOfferId;
      weatherUntil = legacyData.weatherUntil;
      weather = legacyData.weather;
      offer = new ExchangeOfferV03(legacyData.offer);
      econSaturations = legacyData.econSaturations;
      teleportNodeActivations = legacyData.teleportNodeActivations;
      animalSpawnerTimes = UpgradeFrom(legacyData.animalSpawnerTimes);
      liquidSourceUnits = legacyData.liquidSourceUnits;
      spawnerTriggerTimes = UpgradeFrom(legacyData.spawnerTriggerTimes);
      gordoEatenCounts = legacyData.gordoEatenCounts;
      resourceSpawnerWater = UpgradeFrom(legacyData.resourceSpawnerWater);
      placedGadgets = legacyData.placedGadgets;
    }

    private static Dictionary<Vector3V02, double> UpgradeFrom(
      Dictionary<Vector3V02, float> singleDict)
    {
      Dictionary<Vector3V02, double> dictionary = new Dictionary<Vector3V02, double>();
      foreach (KeyValuePair<Vector3V02, float> keyValuePair in singleDict)
        dictionary[keyValuePair.Key] = keyValuePair.Value;
      return dictionary;
    }

    private static Dictionary<Vector3V02, ResourceWaterV03> UpgradeFrom(
      Dictionary<Vector3V02, ResourceWaterV02> legacyDict)
    {
      Dictionary<Vector3V02, ResourceWaterV03> dictionary = new Dictionary<Vector3V02, ResourceWaterV03>();
      foreach (KeyValuePair<Vector3V02, ResourceWaterV02> keyValuePair in legacyDict)
        dictionary[keyValuePair.Key] = new ResourceWaterV03(keyValuePair.Value);
      return dictionary;
    }
  }
}
