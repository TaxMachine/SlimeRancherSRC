// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.WorldV17
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class WorldV17 : VersionedPersistedDataSet<WorldV16>
  {
    public double worldTime;
    public float econSeed;
    public double dailyOfferCreateTime;
    public List<string> lastOfferRancherIds;
    public List<string> pendingOfferRancherIds;
    public double weatherUntil;
    public AmbianceDirector.Weather weather;
    public Dictionary<ExchangeDirector.OfferType, ExchangeOfferV03> offers = new Dictionary<ExchangeDirector.OfferType, ExchangeOfferV03>();
    public Dictionary<Identifiable.Id, float> econSaturations;
    public Dictionary<string, bool> teleportNodeActivations;
    public Dictionary<Vector3V02, double> animalSpawnerTimes;
    public Dictionary<Vector3V02, float> liquidSourceUnits;
    public Dictionary<Vector3V02, double> spawnerTriggerTimes;
    public Dictionary<string, GordoV01> gordos;
    public Dictionary<Vector3V02, ResourceWaterV03> resourceSpawnerWater;
    public Dictionary<string, PlacedGadgetV04> placedGadgets;
    public Dictionary<string, TreasurePod.State> treasurePods;
    public Dictionary<string, SwitchHandler.State> switches;
    public Dictionary<string, bool> puzzleSlotsFilled;
    public Dictionary<string, bool> occupiedPhaseSites;
    public FirestormV01 firestorm = new FirestormV01();
    public Dictionary<string, bool> oasisStates;
    public List<string> activeGingerPatches;

    public override string Identifier => "SRW";

    public override uint Version => 17;

    public WorldV17()
    {
    }

    public WorldV17(WorldV16 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      worldTime = reader.ReadDouble();
      econSeed = reader.ReadSingle();
      dailyOfferCreateTime = reader.ReadDouble();
      lastOfferRancherIds = LoadList(reader, (Func<string, string>) (s => s));
      pendingOfferRancherIds = LoadList(reader, (Func<string, string>) (s => s));
      weatherUntil = reader.ReadDouble();
      weather = (AmbianceDirector.Weather) reader.ReadInt32();
      offers = LoadDictionary(reader, r => (ExchangeDirector.OfferType) r.ReadInt32(), r => ExchangeOfferV03.Load(reader));
      econSaturations = LoadDictionary(reader, r => (Identifiable.Id) r.ReadInt32(), r => r.ReadSingle());
      teleportNodeActivations = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      animalSpawnerTimes = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadDouble());
      liquidSourceUnits = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadSingle());
      spawnerTriggerTimes = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadDouble());
      gordos = LoadDictionary(reader, r => r.ReadString(), r => GordoV01.Load(r));
      resourceSpawnerWater = LoadDictionary(reader, r => Vector3V02.Load(r), r => ResourceWaterV03.Load(r));
      placedGadgets = LoadDictionary(reader, r => r.ReadString(), r => PlacedGadgetV04.Load(r));
      treasurePods = LoadDictionary(reader, r => r.ReadString(), r => (TreasurePod.State) r.ReadInt32());
      switches = LoadDictionary(reader, r => r.ReadString(), r => (SwitchHandler.State) r.ReadInt32());
      puzzleSlotsFilled = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      occupiedPhaseSites = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      firestorm = FirestormV01.Load(reader);
      oasisStates = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      activeGingerPatches = LoadList(reader, (Func<string, string>) (s => s));
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
      WriteDictionary(writer, treasurePods, (w, s) => w.Write(s), (w, val) => w.Write((int) val));
      WriteDictionary(writer, switches, (w, s) => w.Write(s), (w, val) => w.Write((int) val));
      WriteDictionary(writer, puzzleSlotsFilled, (w, s) => w.Write(s), (w, val) => w.Write(val));
      WriteDictionary(writer, occupiedPhaseSites, (w, s) => w.Write(s), (w, val) => w.Write(val));
      firestorm.Write(writer.BaseStream);
      WriteDictionary(writer, oasisStates, (w, s) => w.Write(s), (w, val) => w.Write(val));
      WriteList(writer, activeGingerPatches, s => s);
    }

    public static WorldV17 Load(BinaryReader reader)
    {
      WorldV17 worldV17 = new WorldV17();
      worldV17.Load(reader.BaseStream);
      return worldV17;
    }

    public static void AssertAreEqual(WorldV17 expected, WorldV17 actual)
    {
      TestUtil.AssertAreEqual(expected.lastOfferRancherIds, actual.lastOfferRancherIds, "lastOfferRancherIds");
      TestUtil.AssertAreEqual(expected.pendingOfferRancherIds, actual.pendingOfferRancherIds, "pendingOfferRancherIds");
      TestUtil.AssertAreEqual(expected.offers, actual.offers, (e, a) => ExchangeOfferV03.AssertAreEqual(e, a), "offers");
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(expected.animalSpawnerTimes, actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.liquidSourceUnits, actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
      TestUtil.AssertAreEqual(expected.spawnerTriggerTimes, actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(expected.gordos, actual.gordos, (e, a) => GordoV01.AssertAreEqual(e, a), "gordos");
      TestUtil.AssertAreEqual(expected.resourceSpawnerWater, actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV04.AssertAreEqual(e, a), "placedGadgets");
      TestUtil.AssertAreEqual(expected.treasurePods, actual.treasurePods, (e, a) => { }, "treasurePods");
      TestUtil.AssertAreEqual(expected.switches, actual.switches, (e, a) => { }, "switches");
      TestUtil.AssertAreEqual(expected.puzzleSlotsFilled, actual.puzzleSlotsFilled, (e, a) => { }, "puzzleSlotsFilled");
      TestUtil.AssertAreEqual(expected.occupiedPhaseSites, actual.occupiedPhaseSites, (e, a) => { }, "occupiedPhaseSites");
      FirestormV01.AssertAreEqual(expected.firestorm, actual.firestorm);
      TestUtil.AssertAreEqual(expected.oasisStates, actual.oasisStates, (e, a) => { }, "oasisStates");
      TestUtil.AssertAreEqual(expected.activeGingerPatches, actual.activeGingerPatches, "activeGingerPatches");
    }

    public static void AssertAreEqual(WorldV16 expected, WorldV17 actual)
    {
      Dictionary<ExchangeDirector.OfferType, ExchangeOfferV03> expected1 = new Dictionary<ExchangeDirector.OfferType, ExchangeOfferV03>();
      if (expected.offer != null && expected.offer.hasOffer)
        expected1[ExchangeDirector.OfferType.GENERAL] = expected.offer;
      TestUtil.AssertAreEqual(expected1, actual.offers, (e, a) => ExchangeOfferV03.AssertAreEqual(e, a), "offers");
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(expected.animalSpawnerTimes, actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.liquidSourceUnits, actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
      TestUtil.AssertAreEqual(expected.spawnerTriggerTimes, actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(expected.gordos, actual.gordos, (e, a) => GordoV01.AssertAreEqual(e, a), "gordos");
      TestUtil.AssertAreEqual(expected.resourceSpawnerWater, actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertVersionedAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV04.AssertAreEqual(e, a), "placedGadgets");
      TestUtil.AssertAreEqual(expected.treasurePods, actual.treasurePods, (e, a) => { }, "treasurePods");
      TestUtil.AssertAreEqual(expected.switches, actual.switches, (e, a) => { }, "switches");
      TestUtil.AssertAreEqual(expected.puzzleSlotsFilled, actual.puzzleSlotsFilled, (e, a) => { }, "puzzleSlotsFilled");
      TestUtil.AssertAreEqual(expected.occupiedPhaseSites, actual.occupiedPhaseSites, (e, a) => { }, "occupiedPhaseSites");
      FirestormV01.AssertAreEqual(expected.firestorm, actual.firestorm);
      TestUtil.AssertAreEqual(expected.oasisStates, actual.oasisStates, (e, a) => { }, "oasisStates");
      TestUtil.AssertAreEqual(expected.activeGingerPatches, actual.activeGingerPatches, "activeGingerPatches");
    }

    protected override void UpgradeFrom(WorldV16 legacyData)
    {
      worldTime = legacyData.worldTime;
      econSeed = legacyData.econSeed;
      dailyOfferCreateTime = legacyData.dailyOfferCreateTime;
      lastOfferRancherIds = new List<string>();
      if (legacyData.lastRancherOfferId != null)
        lastOfferRancherIds.Add(legacyData.lastRancherOfferId);
      pendingOfferRancherIds = new List<string>();
      weatherUntil = legacyData.weatherUntil;
      weather = legacyData.weather;
      offers = new Dictionary<ExchangeDirector.OfferType, ExchangeOfferV03>();
      if (legacyData.offer != null && legacyData.offer.hasOffer)
        offers[ExchangeDirector.OfferType.GENERAL] = legacyData.offer;
      econSaturations = legacyData.econSaturations;
      teleportNodeActivations = legacyData.teleportNodeActivations;
      animalSpawnerTimes = legacyData.animalSpawnerTimes;
      liquidSourceUnits = legacyData.liquidSourceUnits;
      spawnerTriggerTimes = legacyData.spawnerTriggerTimes;
      gordos = legacyData.gordos;
      resourceSpawnerWater = legacyData.resourceSpawnerWater;
      placedGadgets = legacyData.placedGadgets;
      treasurePods = legacyData.treasurePods;
      switches = legacyData.switches;
      puzzleSlotsFilled = legacyData.puzzleSlotsFilled;
      occupiedPhaseSites = legacyData.occupiedPhaseSites;
      firestorm = legacyData.firestorm;
      oasisStates = legacyData.oasisStates;
      activeGingerPatches = legacyData.activeGingerPatches;
    }
  }
}
