// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.WorldV20
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MonomiPark.SlimeRancher.Persist
{
  public class WorldV20 : VersionedPersistedDataSet<WorldV19>
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
    public Dictionary<string, PlacedGadgetV06> placedGadgets;
    public Dictionary<string, TreasurePodV01> treasurePods;
    public Dictionary<string, SwitchHandler.State> switches;
    public Dictionary<string, bool> puzzleSlotsFilled;
    public Dictionary<string, bool> occupiedPhaseSites;
    public Dictionary<string, QuicksilverEnergyGeneratorV02> quicksilverEnergyGenerators;
    public FirestormV01 firestorm = new FirestormV01();
    public Dictionary<string, bool> oasisStates;
    public List<string> activeGingerPatches;

    public override string Identifier => "SRW";

    public override uint Version => 20;

    public WorldV20()
    {
    }

    public WorldV20(WorldV19 legacyData) => UpgradeFrom(legacyData);

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
      placedGadgets = LoadDictionary(reader, r => r.ReadString(), r => PlacedGadgetV06.Load(r));
      treasurePods = LoadDictionary(reader, r => r.ReadString(), r => TreasurePodV01.Load(r));
      switches = LoadDictionary(reader, r => r.ReadString(), r => (SwitchHandler.State) r.ReadInt32());
      puzzleSlotsFilled = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      occupiedPhaseSites = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      firestorm = FirestormV01.Load(reader);
      oasisStates = LoadDictionary(reader, r => r.ReadString(), r => r.ReadBoolean());
      activeGingerPatches = LoadList(reader, (Func<string, string>) (s => s));
      quicksilverEnergyGenerators = LoadDictionary(reader, r => r.ReadString(), r => QuicksilverEnergyGeneratorV02.Load(r));
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
    }

    public static WorldV20 Load(BinaryReader reader)
    {
      WorldV20 worldV20 = new WorldV20();
      worldV20.Load(reader.BaseStream);
      return worldV20;
    }

    public static void AssertAreEqual(WorldV20 expected, WorldV20 actual)
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
      TestUtil.AssertAreEqual(expected.quicksilverEnergyGenerators, actual.quicksilverEnergyGenerators, (e, a) => QuicksilverEnergyGeneratorV02.AssertAreEqual(e, a), "quicksilverEnergyGenerators");
    }

    public static void AssertAreEqual(WorldV19 expected, WorldV20 actual)
    {
      TestUtil.AssertAreEqual(expected.offers, actual.offers, (e, a) => ExchangeOfferV04.AssertAreEqual(e, a), "offers");
      TestUtil.AssertAreEqual(expected.econSaturations, actual.econSaturations, (e, a) => { }, "econSaturations");
      TestUtil.AssertAreEqual(expected.teleportNodeActivations, actual.teleportNodeActivations, (e, a) => { }, "teleportNodeActivations");
      TestUtil.AssertAreEqual(expected.animalSpawnerTimes, actual.animalSpawnerTimes, (e, a) => { }, "animalSpawnerTimes");
      TestUtil.AssertAreEqual(expected.spawnerTriggerTimes, actual.spawnerTriggerTimes, (e, a) => { }, "spawnerTriggerTimes");
      TestUtil.AssertAreEqual(expected.gordos, actual.gordos, (e, a) => GordoV01.AssertAreEqual(e, a), "gordos");
      TestUtil.AssertAreEqual(expected.resourceSpawnerWater, actual.resourceSpawnerWater, (e, a) => ResourceWaterV03.AssertAreEqual(e, a), "resourceSpawnerWater");
      TestUtil.AssertVersionedAreEqual(expected.placedGadgets, actual.placedGadgets, (e, a) => PlacedGadgetV06.AssertAreEqual(e, a), "placedGadgets");
      TestUtil.AssertAreEqual(expected.treasurePods, actual.treasurePods, (e, a) => TreasurePodV01.AssertAreEqual(e, a), "treasurePods");
      TestUtil.AssertAreEqual(expected.switches, actual.switches, (e, a) => { }, "switches");
      TestUtil.AssertAreEqual(expected.puzzleSlotsFilled, actual.puzzleSlotsFilled, (e, a) => { }, "puzzleSlotsFilled");
      TestUtil.AssertAreEqual(expected.occupiedPhaseSites, actual.occupiedPhaseSites, (e, a) => { }, "occupiedPhaseSites");
      FirestormV01.AssertAreEqual(expected.firestorm, actual.firestorm);
      TestUtil.AssertAreEqual(expected.oasisStates, actual.oasisStates, (e, a) => { }, "oasisStates");
      TestUtil.AssertAreEqual(expected.activeGingerPatches, actual.activeGingerPatches, "activeGingerPatches");
      TestUtil.AssertVersionedAreEqual(expected.quicksilverEnergyGenerators, actual.quicksilverEnergyGenerators, (e, a) => QuicksilverEnergyGeneratorV02.AssertAreEqual(e, a), "quicksilverEnergyGenerators");
      TestUtil.AssertAreEqual(UpgradeLiquidSourceFrom(expected.liquidSourceUnits), actual.liquidSourceUnits, (e, a) => { }, "liquidSourceUnits");
    }

    protected override void UpgradeFrom(WorldV19 legacyData)
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
      placedGadgets = legacyData.placedGadgets;
      switches = legacyData.switches;
      puzzleSlotsFilled = legacyData.puzzleSlotsFilled;
      occupiedPhaseSites = legacyData.occupiedPhaseSites;
      firestorm = legacyData.firestorm;
      oasisStates = legacyData.oasisStates;
      activeGingerPatches = legacyData.activeGingerPatches;
      treasurePods = legacyData.treasurePods;
      quicksilverEnergyGenerators = legacyData.quicksilverEnergyGenerators != null ? legacyData.quicksilverEnergyGenerators.ToDictionary(kv => kv.Key, kv => new QuicksilverEnergyGeneratorV02(kv.Value)) : null;
      liquidSourceUnits = UpgradeLiquidSourceFrom(legacyData.liquidSourceUnits);
    }

    private static Dictionary<string, float> UpgradeLiquidSourceFrom(
      Dictionary<Vector3V02, float> legacy)
    {
      return PersistedDataSet.UpgradeFrom(legacy, new Dictionary<Vector3, string>()
      {
        {
          new Vector3(-107.3299f, -2.181591f, -256.5173f),
          "LiquidSource1821848142"
        },
        {
          new Vector3(-100.6733f, 21.74f, -248.7744f),
          "LiquidSource1649312552"
        },
        {
          new Vector3(4.998472f, -3.441f, -24.41549f),
          "LiquidSource1990662812"
        },
        {
          new Vector3(-50.79f, -5.195f, 219.98f),
          "LiquidSource2028576483"
        },
        {
          new Vector3(-72.82455f, 5.768999f, 151.6755f),
          "LiquidSource1055205995"
        },
        {
          new Vector3(-284.0245f, 0.3189994f, 132.9355f),
          "LiquidSource1309435585"
        },
        {
          new Vector3(-571.6572f, -6.335f, -196.6816f),
          "LiquidSource0175964606"
        },
        {
          new Vector3(-582.0848f, 5.239653f, -225.9712f),
          "LiquidSource1303576363"
        },
        {
          new Vector3(245.7014f, 0.9990001f, 62.35651f),
          "LiquidSource2092797854"
        },
        {
          new Vector3(381.4633f, -1.641001f, 236.5295f),
          "LiquidSource0236081948"
        },
        {
          new Vector3(370.2054f, -1.641001f, 229.9455f),
          "LiquidSource0685004436"
        },
        {
          new Vector3(370.1454f, -1.641001f, 246.1255f),
          "LiquidSource0337399841"
        },
        {
          new Vector3(396.2454f, -1.641001f, 236.9255f),
          "LiquidSource0768522297"
        },
        {
          new Vector3(385.69f, -2.053186f, 241.08f),
          "LiquidSource0339157684"
        },
        {
          new Vector3(224.54f, -2.176034f, 318.29f),
          "LiquidSource0257277014"
        },
        {
          new Vector3(-148.23f, -2.451262f, 432.14f),
          "LiquidSource1908317765"
        },
        {
          new Vector3(-259.7745f, 9.979f, 366.1155f),
          "LiquidSource1659774516"
        },
        {
          new Vector3(-281.7145f, -2.171001f, 560.4755f),
          "LiquidSource1564089217"
        },
        {
          new Vector3(-405.4545f, 1.019001f, 396.1355f),
          "LiquidSource1196070009"
        },
        {
          new Vector3(-444.4701f, 1.449112f, 583.4445f),
          "LiquidSource0484905579"
        },
        {
          new Vector3(-233f, 3.317609f, 674.6f),
          "LiquidSource1446494385"
        },
        {
          new Vector3(-366.51f, -8.424999f, 792.41f),
          "LiquidSource0521513736"
        },
        {
          new Vector3(-336.4f, -8.424999f, 809.1f),
          "LiquidSource2084655384"
        },
        {
          new Vector3(927.16f, 16.80813f, 137.8052f),
          "LiquidSource2090097584"
        },
        {
          new Vector3(927.16f, 16.80813f, 73.44484f),
          "LiquidSource2078333005"
        },
        {
          new Vector3(983.59f, 16.80813f, 73.44484f),
          "LiquidSource1731324186"
        },
        {
          new Vector3(983.59f, 16.80813f, 137.8052f),
          "LiquidSource1361637309"
        },
        {
          new Vector3(882.9582f, 18.21f, 198.0948f),
          "LiquidSource1886489902"
        },
        {
          new Vector3(902.2848f, 18.21f, 198.0948f),
          "LiquidSource2083073731"
        },
        {
          new Vector3(895.87f, 18.21f, 215.0329f),
          "LiquidSource0990738422"
        },
        {
          new Vector3(91.99127f, 11.8f, 582.0352f),
          "LiquidSource0400228104"
        },
        {
          new Vector3(109.8313f, 11.8f, 582.0352f),
          "LiquidSource0880552317"
        },
        {
          new Vector3(-8.300479f, 20.49221f, 685.18f),
          "LiquidSource1040146946"
        },
        {
          new Vector3(134.503f, 8.108602f, 639.4841f),
          "LiquidSource1646922388"
        },
        {
          new Vector3(131.1023f, -0.5250006f, 640.1f),
          "LiquidSource2101203491"
        },
        {
          new Vector3(138.0148f, 27.71f, 775f),
          "LiquidSource1719935935"
        },
        {
          new Vector3(137.4748f, 11.63f, 787.78f),
          "LiquidSource0840671444"
        },
        {
          new Vector3(49.52f, 3.137276f, 739.93f),
          "LiquidSource0200098857"
        },
        {
          new Vector3(113.1148f, 11.54f, 693.9375f),
          "LiquidSource0496126148"
        },
        {
          new Vector3(40.6f, 3.65f, 842.77f),
          "LiquidSource1862051859"
        },
        {
          new Vector3(40.6f, 3.65f, 872.65f),
          "LiquidSource0322878943"
        },
        {
          new Vector3(56.87574f, -7.885f, 857.22f),
          "LiquidSource1237502513"
        },
        {
          new Vector3(53.12559f, 1059.791f, 631.0752f),
          "LiquidSource1051609151"
        },
        {
          new Vector3(102.5854f, 1025.699f, 640.9355f),
          "LiquidSource1989825024"
        },
        {
          new Vector3(32.17712f, 1025.848f, 573.2024f),
          "LiquidSource0101071077"
        },
        {
          new Vector3(-65.06472f, 1026.003f, 619.2498f),
          "LiquidSource1152944911"
        },
        {
          new Vector3(-4.935879f, 1034.568f, 503.1194f),
          "LiquidSource1816694074"
        },
        {
          new Vector3(-148.9949f, 1005.582f, 490.9409f),
          "LiquidSource1148397045"
        },
        {
          new Vector3(-125.1141f, 1016.156f, 513.7898f),
          "LiquidSource1026723854"
        },
        {
          new Vector3(-138.9546f, 1012.899f, 192.5857f),
          "LiquidSource0094681716"
        },
        {
          new Vector3(-41.63993f, 1013.84f, 253.9668f),
          "LiquidSource0903559647"
        },
        {
          new Vector3(49.33876f, 996.2458f, 268.2415f),
          "LiquidSource0035106881"
        },
        {
          new Vector3(-47.12584f, 1041.439f, 121.6924f),
          "LiquidSource0825490879"
        },
        {
          new Vector3(-77.46f, 1011.416f, 159.0901f),
          "LiquidSource0821809482"
        },
        {
          new Vector3(-49.0852f, 1011.342f, 147.0918f),
          "LiquidSource0562668862"
        },
        {
          new Vector3(124.825f, 1010.906f, 424.9546f),
          "LiquidSource0005154068"
        },
        {
          new Vector3(81.57545f, 1025.919f, 435.3254f),
          "LiquidSource1264606705"
        },
        {
          new Vector3(-886.3604f, 15.7f, 860.5355f),
          "LiquidSource2076051771"
        },
        {
          new Vector3(-881.0118f, 12.71f, 854.1743f),
          "LiquidSource1168655324"
        },
        {
          new Vector3(-857.8259f, 35.00747f, 901.8265f),
          "LiquidSource0907929535"
        },
        {
          new Vector3(-848.823f, 38.52246f, 899.7509f),
          "LiquidSource1782345945"
        },
        {
          new Vector3(-874.7886f, 19.12f, 868.0653f),
          "LiquidSource1179514084"
        },
        {
          new Vector3(-856.1986f, 20.4f, 891.7453f),
          "LiquidSource1851383021"
        },
        {
          new Vector3(-864.4118f, 19.79f, 880.7467f),
          "LiquidSource1266955593"
        },
        {
          new Vector3(262.3019f, 6.46f, -868.92f),
          "LiquidSource0923726985"
        },
        {
          new Vector3(299.3942f, 0.75f, -884.7032f),
          "LiquidSource0667264529"
        },
        {
          new Vector3(253.5085f, 10.459f, -869.1275f),
          "LiquidSource1424305194"
        },
        {
          new Vector3(210.6465f, 26.76244f, -966.0587f),
          "LiquidSource1729739017"
        },
        {
          new Vector3(212.5739f, 52.44f, -954.4037f),
          "LiquidSource1883417022"
        },
        {
          new Vector3(275.32f, -0.7850003f, -868.0813f),
          "LiquidSource1262245631"
        },
        {
          new Vector3(294.4326f, 1.712443f, -868.5961f),
          "LiquidSource0183782712"
        },
        {
          new Vector3(299.0847f, 3.343443f, -896.576f),
          "LiquidSource1081513035"
        }
      });
    }
  }
}
