// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.WorldV19
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonomiPark.SlimeRancher.Persist
{
  public class WorldV19 : VersionedPersistedDataSet<WorldV18>
  {
    public double worldTime;
    public float econSeed;
    public double dailyOfferCreateTime;
    public List<string> lastOfferRancherIds;
    public List<string> pendingOfferRancherIds;
    public double weatherUntil;
    public AmbianceDirector.Weather weather;
    public Dictionary<ExchangeDirector.OfferType, ExchangeOfferV04> offers = new Dictionary<ExchangeDirector.OfferType, ExchangeOfferV04>();
    public Dictionary<Identifiable.Id, float> econSaturations;
    public Dictionary<string, bool> teleportNodeActivations;
    public Dictionary<Vector3V02, double> animalSpawnerTimes;
    public Dictionary<Vector3V02, float> liquidSourceUnits;
    public Dictionary<Vector3V02, double> spawnerTriggerTimes;
    public Dictionary<string, GordoV01> gordos;
    public Dictionary<Vector3V02, ResourceWaterV03> resourceSpawnerWater;
    public Dictionary<string, PlacedGadgetV06> placedGadgets;
    public Dictionary<string, TreasurePodV01> treasurePods;
    public Dictionary<string, SwitchHandler.State> switches;
    public Dictionary<string, bool> puzzleSlotsFilled;
    public Dictionary<string, bool> occupiedPhaseSites;
    public Dictionary<string, QuicksilverEnergyGeneratorV01> quicksilverEnergyGenerators;
    public FirestormV01 firestorm = new FirestormV01();
    public Dictionary<string, bool> oasisStates;
    public List<string> activeGingerPatches;

    public override string Identifier => "SRW";

    public override uint Version => 19;

    public WorldV19()
    {
    }

    public WorldV19(WorldV18 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      worldTime = reader.ReadDouble();
      econSeed = reader.ReadSingle();
      dailyOfferCreateTime = reader.ReadDouble();
      lastOfferRancherIds = LoadList(reader, (Func<string, string>) (s => s));
      pendingOfferRancherIds = LoadList(reader, (Func<string, string>) (s => s));
      weatherUntil = reader.ReadDouble();
      weather = (AmbianceDirector.Weather) reader.ReadInt32();
      offers = LoadDictionary(reader, r => (ExchangeDirector.OfferType) r.ReadInt32(), r => ExchangeOfferV04.Load(reader));
      econSaturations = LoadDictionary(reader, r => (Identifiable.Id) r.ReadInt32(), r => r.ReadSingle());
      teleportNodeActivations = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      animalSpawnerTimes = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadDouble());
      liquidSourceUnits = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadSingle());
      spawnerTriggerTimes = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadDouble());
      gordos = LoadDictionary(reader, r => r.ReadString(), r => GordoV01.Load(r));
      resourceSpawnerWater = LoadDictionary(reader, r => Vector3V02.Load(r), r => ResourceWaterV03.Load(r));
      placedGadgets = LoadDictionary(reader, r => r.ReadString(), r => PlacedGadgetV06.Load(r));
      treasurePods = LoadDictionary(reader, r => r.ReadString(), r => TreasurePodV01.Load(r));
      switches = LoadDictionary(reader, r => r.ReadString(), r => (SwitchHandler.State) r.ReadInt32());
      puzzleSlotsFilled = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      occupiedPhaseSites = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      firestorm = FirestormV01.Load(reader);
      oasisStates = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      activeGingerPatches = LoadList(reader, (Func<string, string>) (s => s));
      quicksilverEnergyGenerators = LoadDictionary(reader, r => r.ReadString(), r => QuicksilverEnergyGeneratorV01.Load(r));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(worldTime);
      writer.Write(econSeed);
      writer.Write(dailyOfferCreateTime);
      WriteList(writer, lastOfferRancherIds, s => s);
      WriteList(writer, pendingOfferRancherIds, s => s);
      writer.Write(weatherUntil);
      writer.Write((int) weather);
      WriteDictionary(writer, offers, (w, ot) => w.Write((int) ot), (w, off) => off.Write(w.BaseStream));
      WriteDictionary(writer, econSaturations, (w, k) => w.Write((int) k), (w, v) => w.Write(v));
      WriteDictionary(writer, teleportNodeActivations, (w, k) => w.Write(k), (w, v) => w.Write(v));
      WriteDictionary(writer, animalSpawnerTimes, (w, key) => key.Write(w.BaseStream), (w, val) => w.Write(val));
      WriteDictionary(writer, liquidSourceUnits, (w, key) => key.Write(w.BaseStream), (w, val) => w.Write(val));
      WriteDictionary(writer, spawnerTriggerTimes, (w, key) => key.Write(w.BaseStream), (w, val) => w.Write(val));
      WriteDictionary(writer, gordos, (w, key) => w.Write(key), (w, val) => val.Write(w.BaseStream));
      WriteDictionary(writer, resourceSpawnerWater, (w, key) => key.Write(w.BaseStream), (w, val) => val.Write(w.BaseStream));
      WriteDictionary(writer, placedGadgets, (w, s) => w.Write(s), (w, pg) => pg.Write(w.BaseStream));
      WriteDictionary(writer, treasurePods, (w, s) => w.Write(s), (w, tp) => tp.Write(w.BaseStream));
      WriteDictionary(writer, switches, (w, s) => w.Write(s), (w, val) => w.Write((int) val));
      WriteDictionary(writer, puzzleSlotsFilled, (w, s) => w.Write(s), (w, val) => w.Write(val));
      WriteDictionary(writer, occupiedPhaseSites, (w, s) => w.Write(s), (w, val) => w.Write(val));
      firestorm.Write(writer.BaseStream);
      WriteDictionary(writer, oasisStates, (w, s) => w.Write(s), (w, val) => w.Write(val));
      WriteList(writer, activeGingerPatches, s => s);
      WriteDictionary(writer, quicksilverEnergyGenerators, (w, s) => w.Write(s), (w, p) => p.Write(w.BaseStream));
    }

    public static WorldV19 Load(BinaryReader reader)
    {
      WorldV19 worldV19 = new WorldV19();
      worldV19.Load(reader.BaseStream);
      return worldV19;
    }

    public static void AssertAreEqual(WorldV19 expected, WorldV19 actual)
    {
      TestUtil.AssertAreEqual(expected.lastOfferRancherIds, actual.lastOfferRancherIds, "lastOfferRancherIds");
      TestUtil.AssertAreEqual(expected.pendingOfferRancherIds, actual.pendingOfferRancherIds, "pendingOfferRancherIds");
      TestUtil.AssertAreEqual(expected.offers, actual.offers, (e, a) => ExchangeOfferV04.AssertAreEqual(e, a), "offers");
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(expected.animalSpawnerTimes, actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.liquidSourceUnits, actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
      TestUtil.AssertAreEqual(expected.spawnerTriggerTimes, actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(expected.gordos, actual.gordos, (e, a) => GordoV01.AssertAreEqual(e, a), "gordos");
      TestUtil.AssertAreEqual(expected.resourceSpawnerWater, actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV06.AssertAreEqual(e, a), "placedGadgets");
      TestUtil.AssertAreEqual(expected.treasurePods, actual.treasurePods, (e, a) => TreasurePodV01.AssertAreEqual(e, a), "treasurePods");
      TestUtil.AssertAreEqual(expected.switches, actual.switches, (e, a) => { }, "switches");
      TestUtil.AssertAreEqual(expected.puzzleSlotsFilled, actual.puzzleSlotsFilled, (e, a) => { }, "puzzleSlotsFilled");
      TestUtil.AssertAreEqual(expected.occupiedPhaseSites, actual.occupiedPhaseSites, (e, a) => { }, "occupiedPhaseSites");
      FirestormV01.AssertAreEqual(expected.firestorm, actual.firestorm);
      TestUtil.AssertAreEqual(expected.oasisStates, actual.oasisStates, (e, a) => { }, "oasisStates");
      TestUtil.AssertAreEqual(expected.activeGingerPatches, actual.activeGingerPatches, "activeGingerPatches");
      TestUtil.AssertAreEqual(expected.quicksilverEnergyGenerators, actual.quicksilverEnergyGenerators, (e, a) => QuicksilverEnergyGeneratorV01.AssertAreEqual(e, a), "quicksilverEnergyGenerators");
    }

    public static void AssertAreEqual(WorldV18 expected, WorldV19 actual)
    {
      TestUtil.AssertAreEqual(UpgradeFrom(expected.offers), actual.offers, (e, a) => ExchangeOfferV04.AssertAreEqual(e, a), "offers");
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(expected.animalSpawnerTimes, actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.liquidSourceUnits, actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
      TestUtil.AssertAreEqual(expected.spawnerTriggerTimes, actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(expected.gordos, actual.gordos, (e, a) => GordoV01.AssertAreEqual(e, a), "gordos");
      TestUtil.AssertAreEqual(expected.resourceSpawnerWater, actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertVersionedAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV06.AssertAreEqual(new PlacedGadgetV05(e), a), "placedGadgets");
      TestUtil.AssertAreEqual(expected.treasurePods, actual.treasurePods, (e, a) => TreasurePodV01.AssertAreEqual(e, a), "treasurePods");
      TestUtil.AssertAreEqual(expected.switches, actual.switches, (e, a) => { }, "switches");
      TestUtil.AssertAreEqual(expected.puzzleSlotsFilled, actual.puzzleSlotsFilled, (e, a) => { }, "puzzleSlotsFilled");
      TestUtil.AssertAreEqual(expected.occupiedPhaseSites, actual.occupiedPhaseSites, (e, a) => { }, "occupiedPhaseSites");
      FirestormV01.AssertAreEqual(expected.firestorm, actual.firestorm);
      TestUtil.AssertAreEqual(expected.oasisStates, actual.oasisStates, (e, a) => { }, "oasisStates");
      TestUtil.AssertAreEqual(expected.activeGingerPatches, actual.activeGingerPatches, "activeGingerPatches");
    }

    protected override void UpgradeFrom(WorldV18 legacyData)
    {
      worldTime = legacyData.worldTime;
      econSeed = legacyData.econSeed;
      dailyOfferCreateTime = legacyData.dailyOfferCreateTime;
      lastOfferRancherIds = legacyData.lastOfferRancherIds;
      pendingOfferRancherIds = legacyData.pendingOfferRancherIds;
      weatherUntil = legacyData.weatherUntil;
      weather = legacyData.weather;
      offers = UpgradeFrom(legacyData.offers);
      econSaturations = legacyData.econSaturations;
      teleportNodeActivations = legacyData.teleportNodeActivations;
      animalSpawnerTimes = legacyData.animalSpawnerTimes;
      liquidSourceUnits = legacyData.liquidSourceUnits;
      spawnerTriggerTimes = legacyData.spawnerTriggerTimes;
      gordos = legacyData.gordos;
      resourceSpawnerWater = legacyData.resourceSpawnerWater;
      placedGadgets = legacyData.placedGadgets.ToDictionary(kv => kv.Key, kv => new PlacedGadgetV06(new PlacedGadgetV05(kv.Value)));
      switches = legacyData.switches;
      puzzleSlotsFilled = legacyData.puzzleSlotsFilled;
      occupiedPhaseSites = legacyData.occupiedPhaseSites;
      firestorm = legacyData.firestorm;
      oasisStates = legacyData.oasisStates;
      activeGingerPatches = legacyData.activeGingerPatches;
      treasurePods = legacyData.treasurePods;
      quicksilverEnergyGenerators = new Dictionary<string, QuicksilverEnergyGeneratorV01>();
    }

    private static Dictionary<ExchangeDirector.OfferType, ExchangeOfferV04> UpgradeFrom(
      Dictionary<ExchangeDirector.OfferType, ExchangeOfferV03> legacyData)
    {
      Dictionary<ExchangeDirector.OfferType, ExchangeOfferV04> dictionary = new Dictionary<ExchangeDirector.OfferType, ExchangeOfferV04>();
      foreach (KeyValuePair<ExchangeDirector.OfferType, ExchangeOfferV03> keyValuePair in legacyData)
        dictionary[keyValuePair.Key] = new ExchangeOfferV04(keyValuePair.Value);
      return dictionary;
    }
  }
}
