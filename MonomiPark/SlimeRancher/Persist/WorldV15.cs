// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.WorldV15
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class WorldV15 : VersionedPersistedDataSet<WorldV14>
  {
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
    public Dictionary<string, PlacedGadgetV04> placedGadgets;
    public Dictionary<string, TreasurePod.State> treasurePods;
    public Dictionary<string, SwitchHandler.State> switches;
    public Dictionary<string, bool> puzzleSlotsFilled;
    public Dictionary<string, bool> occupiedPhaseSites;
    public FirestormV01 firestorm = new FirestormV01();
    public Dictionary<string, bool> oasisStates;

    public override string Identifier => "SRW";

    public override uint Version => 15;

    public WorldV15()
    {
    }

    public WorldV15(WorldV14 legacyData) => UpgradeFrom(legacyData);

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
      placedGadgets = LoadDictionary(reader, r => r.ReadString(), r => PlacedGadgetV04.Load(r));
      treasurePods = LoadDictionary(reader, r => r.ReadString(), r => (TreasurePod.State) r.ReadInt32());
      switches = LoadDictionary(reader, r => r.ReadString(), r => (SwitchHandler.State) r.ReadInt32());
      puzzleSlotsFilled = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      occupiedPhaseSites = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      firestorm = FirestormV01.Load(reader);
      oasisStates = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
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
      WriteDictionary(writer, switches, (w, s) => w.Write(s), (w, val) => w.Write((int) val));
      WriteDictionary(writer, puzzleSlotsFilled, (w, s) => w.Write(s), (w, val) => w.Write(val));
      WriteDictionary(writer, occupiedPhaseSites, (w, s) => w.Write(s), (w, val) => w.Write(val));
      firestorm.Write(writer.BaseStream);
      WriteDictionary(writer, oasisStates, (w, s) => w.Write(s), (w, val) => w.Write(val));
    }

    public static WorldV15 Load(BinaryReader reader)
    {
      WorldV15 worldV15 = new WorldV15();
      worldV15.Load(reader.BaseStream);
      return worldV15;
    }

    public static void AssertAreEqual(WorldV15 expected, WorldV15 actual)
    {
      ExchangeOfferV03.AssertAreEqual(expected.offer, actual.offer);
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
    }

    public static void AssertAreEqual(WorldV14 expected, WorldV15 actual)
    {
      ExchangeOfferV03.AssertAreEqual(expected.offer, actual.offer);
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
      FirestormV01.AssertAreEqual(new FirestormV01(), actual.firestorm);
    }

    protected override void UpgradeFrom(WorldV14 legacyData)
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
      gordos = legacyData.gordos;
      resourceSpawnerWater = legacyData.resourceSpawnerWater;
      placedGadgets = UpgradeGadgets(legacyData.placedGadgets);
      treasurePods = legacyData.treasurePods;
      switches = legacyData.switches;
      puzzleSlotsFilled = legacyData.puzzleSlotsFilled;
      occupiedPhaseSites = legacyData.occupiedPhaseSites;
      firestorm = new FirestormV01();
      oasisStates = new Dictionary<string, bool>();
    }

    protected Dictionary<string, PlacedGadgetV04> UpgradeGadgets(
      Dictionary<string, PlacedGadgetV03> legacyDict)
    {
      if (legacyDict == null)
        return null;
      Dictionary<string, PlacedGadgetV04> dictionary = new Dictionary<string, PlacedGadgetV04>();
      foreach (KeyValuePair<string, PlacedGadgetV03> keyValuePair in legacyDict)
        dictionary[keyValuePair.Key] = new PlacedGadgetV04(keyValuePair.Value);
      return dictionary;
    }
  }
}
