// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.WorldV22
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonomiPark.SlimeRancher.Persist
{
  public class WorldV22 : VersionedPersistedDataSet<WorldV21>
  {
    public double worldTime;
    public float econSeed;
    public double dailyOfferCreateTime;
    public List<string> lastOfferRancherIds;
    public List<string> pendingOfferRancherIds;
    public double weatherUntil;
    public AmbianceDirector.Weather weather;
    public Dictionary<ExchangeDirector.OfferType, ExchangeOfferV04> offers;
    public Dictionary<Identifiable.Id, float> econSaturations;
    public Dictionary<string, bool> teleportNodeActivations;
    public Dictionary<Vector3V02, double> animalSpawnerTimes;
    public Dictionary<string, float> liquidSourceUnits;
    public Dictionary<Vector3V02, double> spawnerTriggerTimes;
    public Dictionary<string, GordoV01> gordos;
    public Dictionary<Vector3V02, ResourceWaterV03> resourceSpawnerWater;
    public Dictionary<string, PlacedGadgetV08> placedGadgets;
    public Dictionary<string, TreasurePodV01> treasurePods;
    public Dictionary<string, SwitchHandler.State> switches;
    public Dictionary<string, bool> puzzleSlotsFilled;
    public Dictionary<string, bool> occupiedPhaseSites;
    public Dictionary<string, QuicksilverEnergyGeneratorV02> quicksilverEnergyGenerators;
    public FirestormV01 firestorm = new FirestormV01();
    public Dictionary<string, bool> oasisStates;
    public List<string> activeGingerPatches;
    public Dictionary<string, EchoNoteGordoV01> echoNoteGordos;
    public GlitchSlimulationV02 glitch;

    public override string Identifier => "SRW";

    public override uint Version => 22;

    public WorldV22()
    {
    }

    public WorldV22(WorldV21 legacyData) => UpgradeFrom(legacyData);

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
      liquidSourceUnits = LoadDictionary(reader, r => r.ReadString(), r => r.ReadSingle());
      spawnerTriggerTimes = LoadDictionary(reader, r => Vector3V02.Load(r), r => r.ReadDouble());
      gordos = LoadDictionary(reader, r => r.ReadString(), r => GordoV01.Load(r));
      resourceSpawnerWater = LoadDictionary(reader, r => Vector3V02.Load(r), r => ResourceWaterV03.Load(r));
      placedGadgets = LoadDictionary(reader, r => r.ReadString(), r => PlacedGadgetV08.Load(r));
      treasurePods = LoadDictionary(reader, r => r.ReadString(), r => TreasurePodV01.Load(r));
      switches = LoadDictionary(reader, r => r.ReadString(), r => (SwitchHandler.State) r.ReadInt32());
      puzzleSlotsFilled = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      occupiedPhaseSites = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      firestorm = FirestormV01.Load(reader);
      oasisStates = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      activeGingerPatches = LoadList(reader, (Func<string, string>) (s => s));
      quicksilverEnergyGenerators = LoadDictionary(reader, r => r.ReadString(), r => QuicksilverEnergyGeneratorV02.Load(r));
      echoNoteGordos = LoadDictionary(reader, r => r.ReadString(), LoadPersistable<EchoNoteGordoV01>);
      glitch = LoadPersistable<GlitchSlimulationV02>(reader);
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
      WriteDictionary(writer, liquidSourceUnits, (w, key) => w.Write(key), (w, val) => w.Write(val));
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
      WriteDictionary(writer, echoNoteGordos, (w, k) => w.Write(k), WritePersistable);
      WritePersistable(writer, glitch);
    }

    public static WorldV22 Load(BinaryReader reader)
    {
      WorldV22 worldV22 = new WorldV22();
      worldV22.Load(reader.BaseStream);
      return worldV22;
    }

    public static void AssertAreEqual(WorldV22 expected, WorldV22 actual)
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
      TestUtil.AssertAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV08.AssertAreEqual(e, a), "placedGadgets");
      TestUtil.AssertAreEqual(expected.treasurePods, actual.treasurePods, (e, a) => TreasurePodV01.AssertAreEqual(e, a), "treasurePods");
      TestUtil.AssertAreEqual(expected.switches, actual.switches, (e, a) => { }, "switches");
      TestUtil.AssertAreEqual(expected.puzzleSlotsFilled, actual.puzzleSlotsFilled, (e, a) => { }, "puzzleSlotsFilled");
      TestUtil.AssertAreEqual(expected.occupiedPhaseSites, actual.occupiedPhaseSites, (e, a) => { }, "occupiedPhaseSites");
      FirestormV01.AssertAreEqual(expected.firestorm, actual.firestorm);
      TestUtil.AssertAreEqual(expected.oasisStates, actual.oasisStates, (e, a) => { }, "oasisStates");
      TestUtil.AssertAreEqual(expected.activeGingerPatches, actual.activeGingerPatches, "activeGingerPatches");
      TestUtil.AssertAreEqual(expected.quicksilverEnergyGenerators, actual.quicksilverEnergyGenerators, (e, a) => QuicksilverEnergyGeneratorV02.AssertAreEqual(e, a), "quicksilverEnergyGenerators");
      TestUtil.AssertAreEqual(expected.echoNoteGordos, actual.echoNoteGordos, EchoNoteGordoV01.AssertAreEqual, "echoNoteGordos");
      GlitchSlimulationV02.AssertAreEqual(expected.glitch, actual.glitch);
    }

    public static void AssertAreEqual(WorldV21 expected, WorldV22 actual)
    {
      TestUtil.AssertAreEqual(expected.offers, actual.offers, (e, a) => ExchangeOfferV04.AssertAreEqual(e, a), "offers");
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(expected.animalSpawnerTimes, actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.spawnerTriggerTimes, actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(expected.gordos, actual.gordos, (e, a) => GordoV01.AssertAreEqual(e, a), "gordos");
      TestUtil.AssertAreEqual(expected.resourceSpawnerWater, actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertVersionedAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV08.AssertAreEqual(e, a), "placedGadgets");
      TestUtil.AssertAreEqual(expected.treasurePods, actual.treasurePods, (e, a) => TreasurePodV01.AssertAreEqual(e, a), "treasurePods");
      TestUtil.AssertAreEqual(expected.switches, actual.switches, (e, a) => { }, "switches");
      TestUtil.AssertAreEqual(expected.puzzleSlotsFilled, actual.puzzleSlotsFilled, (e, a) => { }, "puzzleSlotsFilled");
      TestUtil.AssertAreEqual(expected.occupiedPhaseSites, actual.occupiedPhaseSites, (e, a) => { }, "occupiedPhaseSites");
      FirestormV01.AssertAreEqual(expected.firestorm, actual.firestorm);
      TestUtil.AssertAreEqual(expected.oasisStates, actual.oasisStates, (e, a) => { }, "oasisStates");
      TestUtil.AssertAreEqual(expected.activeGingerPatches, actual.activeGingerPatches, "activeGingerPatches");
      TestUtil.AssertVersionedAreEqual(expected.quicksilverEnergyGenerators, actual.quicksilverEnergyGenerators, (e, a) => QuicksilverEnergyGeneratorV02.AssertAreEqual(e, a), "quicksilverEnergyGenerators");
      TestUtil.AssertAreEqual(expected.liquidSourceUnits, actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
      TestUtil.AssertAreEqual(expected.echoNoteGordos, actual.echoNoteGordos, (e, a) => EchoNoteGordoV01.AssertAreEqual(e, a), "echoNoteGordos");
      GlitchSlimulationV02.AssertAreEqual(new GlitchSlimulationV02(), actual.glitch);
    }

    protected override void UpgradeFrom(WorldV21 legacyData)
    {
      worldTime = legacyData.worldTime;
      econSeed = legacyData.econSeed;
      dailyOfferCreateTime = legacyData.dailyOfferCreateTime;
      lastOfferRancherIds = legacyData.lastOfferRancherIds;
      pendingOfferRancherIds = legacyData.pendingOfferRancherIds;
      weatherUntil = legacyData.weatherUntil;
      weather = legacyData.weather;
      offers = legacyData.offers;
      econSaturations = legacyData.econSaturations;
      teleportNodeActivations = legacyData.teleportNodeActivations;
      animalSpawnerTimes = legacyData.animalSpawnerTimes;
      spawnerTriggerTimes = legacyData.spawnerTriggerTimes;
      gordos = legacyData.gordos;
      resourceSpawnerWater = legacyData.resourceSpawnerWater;
      switches = legacyData.switches;
      puzzleSlotsFilled = legacyData.puzzleSlotsFilled;
      occupiedPhaseSites = legacyData.occupiedPhaseSites;
      firestorm = legacyData.firestorm;
      oasisStates = legacyData.oasisStates;
      activeGingerPatches = legacyData.activeGingerPatches;
      treasurePods = legacyData.treasurePods;
      quicksilverEnergyGenerators = legacyData.quicksilverEnergyGenerators;
      liquidSourceUnits = legacyData.liquidSourceUnits;
      echoNoteGordos = legacyData.echoNoteGordos;
      placedGadgets = legacyData.placedGadgets != null ? legacyData.placedGadgets.ToDictionary(p => p.Key, p => new PlacedGadgetV08(p.Value)) : null;
      glitch = new GlitchSlimulationV02();
    }
  }
}
