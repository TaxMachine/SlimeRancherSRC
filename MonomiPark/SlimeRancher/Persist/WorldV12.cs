// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.WorldV12
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class WorldV12 : VersionedPersistedDataSet<WorldV11>
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
    public Dictionary<string, GordoV01> gordos;
    public Dictionary<Vector3V02, ResourceWaterV03> resourceSpawnerWater;
    public Dictionary<string, PlacedGadgetV02> placedGadgets;
    public Dictionary<string, TreasurePod.State> treasurePods;

    public override string Identifier => "SRW";

    public override uint Version => 12;

    public WorldV12()
    {
    }

    public WorldV12(WorldV11 legacyData) => UpgradeFrom(legacyData);

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
      gordos = LoadDictionary(reader, r => r.ReadString(), r => GordoV01.Load(r));
      resourceSpawnerWater = LoadDictionary(reader, r => Vector3V02.Load(r), r => ResourceWaterV03.Load(r));
      placedGadgets = LoadDictionary(reader, r => r.ReadString(), r => PlacedGadgetV02.Load(r));
      treasurePods = LoadDictionary(reader, r => r.ReadString(), r => (TreasurePod.State) r.ReadInt32());
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
      WriteDictionary(writer, gordos, (w, key) => w.Write(key), (w, val) => val.Write(w.BaseStream));
      WriteDictionary(writer, resourceSpawnerWater, (w, key) => key.Write(w.BaseStream), (w, val) => val.Write(w.BaseStream));
      WriteDictionary(writer, placedGadgets, (w, s) => w.Write(s), (w, pg) => pg.Write(w.BaseStream));
      WriteDictionary(writer, treasurePods, (w, s) => w.Write(s), (w, val) => w.Write((int) val));
    }

    public static WorldV12 Load(BinaryReader reader)
    {
      WorldV12 worldV12 = new WorldV12();
      worldV12.Load(reader.BaseStream);
      return worldV12;
    }

    public static void AssertAreEqual(WorldV12 expected, WorldV12 actual)
    {
      ExchangeOfferV03.AssertAreEqual(expected.offer, actual.offer);
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(expected.animalSpawnerTimes, actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.liquidSourceUnits, actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
      TestUtil.AssertAreEqual(expected.spawnerTriggerTimes, actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(expected.gordos, actual.gordos, (e, a) => GordoV01.AssertAreEqual(e, a), "gordos");
      TestUtil.AssertAreEqual(expected.resourceSpawnerWater, actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV02.AssertAreEqual(e, a), "placedGadgets");
      TestUtil.AssertAreEqual(expected.treasurePods, actual.treasurePods, (e, a) => { }, "treasurePods");
    }

    public static void AssertAreEqual(WorldV11 expected, WorldV12 actual)
    {
      ExchangeOfferV03.AssertAreEqual(expected.offer, actual.offer);
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(expected.animalSpawnerTimes, actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.liquidSourceUnits, actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
      TestUtil.AssertAreEqual(expected.spawnerTriggerTimes, actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(UpgradeGordoEatsFrom(expected.gordoEatenCounts), actual.gordos, (e, a) => GordoV01.AssertAreEqual(e, a), "gordos");
      TestUtil.AssertAreEqual(expected.resourceSpawnerWater, actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV02.AssertAreEqual(e, a), "placedGadgets");
      TestUtil.AssertAreEqual(expected.treasurePods, actual.treasurePods, (e, a) => { }, "treasurePods");
    }

    protected override void UpgradeFrom(WorldV11 legacyData)
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
      gordos = UpgradeGordoEatsFrom(legacyData.gordoEatenCounts);
      resourceSpawnerWater = legacyData.resourceSpawnerWater;
      placedGadgets = legacyData.placedGadgets;
      treasurePods = legacyData.treasurePods;
    }

    public static Dictionary<string, GordoV01> UpgradeGordoEatsFrom(
      Dictionary<string, int> legacyData)
    {
      if (legacyData == null)
        return null;
      Dictionary<string, GordoV01> dictionary = new Dictionary<string, GordoV01>();
      foreach (KeyValuePair<string, int> keyValuePair in legacyData)
        dictionary[keyValuePair.Key] = new GordoV01()
        {
          eatenCount = keyValuePair.Value
        };
      return dictionary;
    }
  }
}
