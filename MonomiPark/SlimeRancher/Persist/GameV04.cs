// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GameV04
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GameV04 : VersionedPersistedDataSet<GameData>
  {
    public string gameName;
    public WorldV13 world = new WorldV13();
    public PlayerV07 player = new PlayerV07();
    public RanchV05 ranch = new RanchV05();
    public List<ActorDataV05> actors = new List<ActorDataV05>();
    public PediaV02 pedia = new PediaV02();
    public GameAchieveV03 achieve = new GameAchieveV03();

    public override string Identifier => "SRGAME";

    public override uint Version => 4;

    protected override void LoadData(BinaryReader reader)
    {
      gameName = reader.ReadString();
      world = WorldV13.Load(reader);
      player = PlayerV07.Load(reader);
      ranch = RanchV05.Load(reader);
      ReadSectionSeparator(reader);
      actors = LoadList<ActorDataV05>(reader);
      ReadSectionSeparator(reader);
      pedia = PediaV02.Load(reader);
      achieve = GameAchieveV03.Load(reader);
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(gameName);
      world.Write(writer.BaseStream);
      player.Write(writer.BaseStream);
      ranch.Write(writer.BaseStream);
      WriteSectionSeparator(writer);
      WriteList(writer, actors);
      WriteSectionSeparator(writer);
      pedia.Write(writer.BaseStream);
      achieve.Write(writer.BaseStream);
    }

    protected override void UpgradeFrom(GameData legacyData)
    {
      gameName = legacyData.gameName;
      world = UpgradeFrom(legacyData.world);
      achieve = UpgradeFrom(legacyData.achieve);
      player = UpgradeFrom(legacyData.player);
      ranch = UpgradeFrom(legacyData.ranch);
      actors = UpgradeFrom(legacyData.actors);
      pedia = UpgradeFrom(legacyData.pedia);
    }

    private WorldV13 UpgradeFrom(WorldData legacyData)
    {
      WorldV13 worldV13 = new WorldV13();
      worldV13.worldTime = legacyData.worldTime;
      worldV13.econSeed = legacyData.econSeed;
      worldV13.econSaturations = legacyData.econSaturations == null ? new Dictionary<Identifiable.Id, float>() : new Dictionary<Identifiable.Id, float>(legacyData.econSaturations);
      worldV13.resourceSpawnerWater = new Dictionary<Vector3V02, ResourceWaterV03>();
      if (legacyData.resourceSpawnerWater != null)
      {
        foreach (KeyValuePair<Vector3, WorldData.ResourceWater> keyValuePair in legacyData.resourceSpawnerWater)
          worldV13.resourceSpawnerWater.Add(UpgradeFrom(keyValuePair.Key), UpgradeFrom(keyValuePair.Value));
      }
      worldV13.spawnerTriggerTimes = new Dictionary<Vector3V02, double>();
      if (legacyData.spawnerTriggerTimes != null)
      {
        foreach (KeyValuePair<Vector3, float> spawnerTriggerTime in legacyData.spawnerTriggerTimes)
          worldV13.spawnerTriggerTimes.Add(UpgradeFrom(spawnerTriggerTime.Key), spawnerTriggerTime.Value);
      }
      worldV13.teleportNodeActivations = legacyData.teleportNodeActivations == null ? new Dictionary<string, bool>() : new Dictionary<string, bool>(legacyData.teleportNodeActivations);
      worldV13.animalSpawnerTimes = new Dictionary<Vector3V02, double>();
      if (legacyData.animalSpawnerTimes != null)
      {
        foreach (KeyValuePair<Vector3, float> animalSpawnerTime in legacyData.animalSpawnerTimes)
          worldV13.animalSpawnerTimes.Add(UpgradeFrom(animalSpawnerTime.Key), animalSpawnerTime.Value);
      }
      worldV13.offer = UpgradeFrom(legacyData.offer);
      worldV13.dailyOfferCreateTime = legacyData.dailyOfferCreateTime;
      worldV13.lastRancherOfferId = legacyData.lastRancherOfferId;
      worldV13.liquidSourceUnits = new Dictionary<Vector3V02, float>();
      if (legacyData.liquidSourceUnits != null)
      {
        foreach (KeyValuePair<Vector3, float> liquidSourceUnit in legacyData.liquidSourceUnits)
          worldV13.liquidSourceUnits.Add(UpgradeFrom(liquidSourceUnit.Key), liquidSourceUnit.Value);
      }
      worldV13.weather = legacyData.weather;
      worldV13.weatherUntil = legacyData.weatherUntil;
      Dictionary<Vector3V02, int> legacyData1 = new Dictionary<Vector3V02, int>();
      if (legacyData.gordoEatenCounts != null)
      {
        foreach (KeyValuePair<Vector3, int> gordoEatenCount in legacyData.gordoEatenCounts)
          legacyData1.Add(UpgradeFrom(gordoEatenCount.Key), gordoEatenCount.Value);
      }
      worldV13.gordos = WorldV12.UpgradeGordoEatsFrom(WorldV10.UpgradeGordoEatsFrom(legacyData1));
      worldV13.placedGadgets = new Dictionary<string, PlacedGadgetV02>();
      worldV13.treasurePods = new Dictionary<string, TreasurePod.State>();
      worldV13.switches = new Dictionary<string, SwitchHandler.State>();
      worldV13.puzzleSlotsFilled = new Dictionary<string, bool>();
      return worldV13;
    }

    private ExchangeOfferV03 UpgradeFrom(ExchangeDirector.Offer legacyData)
    {
      ExchangeOfferV03 exchangeOfferV03 = new ExchangeOfferV03();
      exchangeOfferV03.requests = new List<RequestedItemEntryV03>();
      exchangeOfferV03.rewards = new List<ItemEntryV03>();
      if (legacyData == null)
      {
        exchangeOfferV03.hasOffer = false;
        return exchangeOfferV03;
      }
      exchangeOfferV03.hasOffer = true;
      exchangeOfferV03.expireTime = legacyData.expireTime;
      exchangeOfferV03.offerId = legacyData.offerId;
      exchangeOfferV03.rancherId = legacyData.rancherId;
      foreach (ExchangeDirector.RequestedItemEntry request in legacyData.requests)
        exchangeOfferV03.requests.Add(UpgradeFrom(request));
      foreach (ExchangeDirector.ItemEntry reward in legacyData.rewards)
        exchangeOfferV03.rewards.Add(UpgradeFrom(reward));
      return exchangeOfferV03;
    }

    private RequestedItemEntryV03 UpgradeFrom(ExchangeDirector.RequestedItemEntry legacyData)
    {
      RequestedItemEntryV02 legacyData1 = new RequestedItemEntryV02();
      if (legacyData != null)
      {
        legacyData1.count = legacyData.count;
        legacyData1.id = legacyData.id;
        legacyData1.progress = legacyData.progress;
      }
      return new RequestedItemEntryV03(legacyData1);
    }

    private ItemEntryV03 UpgradeFrom(ExchangeDirector.ItemEntry legacyData)
    {
      ItemEntryV02 legacyData1 = new ItemEntryV02();
      if (legacyData != null)
      {
        legacyData1.id = legacyData.id;
        legacyData1.count = legacyData.count;
      }
      return new ItemEntryV03(legacyData1);
    }

    private ResourceWaterV03 UpgradeFrom(WorldData.ResourceWater legacyData)
    {
      ResourceWaterV03 resourceWaterV03 = new ResourceWaterV03();
      if (legacyData == null)
        return resourceWaterV03;
      resourceWaterV03.spawn = legacyData.spawn;
      resourceWaterV03.water = legacyData.water;
      return resourceWaterV03;
    }

    private GameAchieveV03 UpgradeFrom(GameAchieveData legacyData)
    {
      GameAchieveV03 gameAchieveV03 = new GameAchieveV03();
      if (legacyData == null)
        return gameAchieveV03;
      gameAchieveV03.gameFloatStatDict = new Dictionary<AchievementsDirector.GameFloatStat, float>();
      if (legacyData.gameFloatStatDict != null)
      {
        foreach (KeyValuePair<AchievementsDirector.GameFloatStat, float> keyValuePair in legacyData.gameFloatStatDict)
          gameAchieveV03.gameFloatStatDict.Add(keyValuePair.Key, keyValuePair.Value);
      }
      gameAchieveV03.gameIntStatDict = new Dictionary<AchievementsDirector.GameIntStat, int>();
      if (legacyData.gameIntStatDict != null)
      {
        foreach (KeyValuePair<AchievementsDirector.GameIntStat, int> keyValuePair in legacyData.gameIntStatDict)
          gameAchieveV03.gameIntStatDict.Add(keyValuePair.Key, keyValuePair.Value);
      }
      gameAchieveV03.gameIdDictStatDict = new Dictionary<AchievementsDirector.GameIdDictStat, Dictionary<Identifiable.Id, int>>();
      if (legacyData.gameIdDictStatDict != null)
      {
        foreach (KeyValuePair<AchievementsDirector.GameIdDictStat, Dictionary<Identifiable.Id, int>> keyValuePair in legacyData.gameIdDictStatDict)
          gameAchieveV03.gameIdDictStatDict.Add(keyValuePair.Key, keyValuePair.Value);
      }
      gameAchieveV03.gameDoubleStatDict = new Dictionary<AchievementsDirector.GameDoubleStat, double>();
      return gameAchieveV03;
    }

    private PlayerV07 UpgradeFrom(PlayerData legacyData)
    {
      PlayerV07 playerV07 = new PlayerV07();
      if (legacyData == null)
        return playerV07;
      playerV07.playerPos = UpgradeFrom(legacyData.playerPos);
      playerV07.playerRotEuler = UpgradeFrom(legacyData.playerRotEuler);
      playerV07.health = legacyData.health;
      playerV07.energy = legacyData.energy;
      playerV07.radiation = legacyData.rad;
      playerV07.currency = legacyData.currency;
      playerV07.ammo = new List<AmmoDataV02>();
      if (legacyData.ammo != null)
      {
        foreach (Ammo.AmmoData legacyData1 in legacyData.ammo)
          playerV07.ammo.Add(UpgradeFrom(legacyData1));
      }
      playerV07.upgrades = legacyData.upgrades == null ? new List<PlayerState.Upgrade>() : new List<PlayerState.Upgrade>(legacyData.upgrades);
      playerV07.upgradeLocks = legacyData.upgradeLocks == null ? new Dictionary<PlayerState.Upgrade, PlayerState.UpgradeLockData>() : new Dictionary<PlayerState.Upgrade, PlayerState.UpgradeLockData>(PlayerV07.UpgradeFrom(PlayerV06.UpgradeFrom(legacyData.upgradeLocks)));
      playerV07.mail = new List<MailV02>();
      if (legacyData.mail != null)
      {
        foreach (MailDirector.Mail legacyData2 in legacyData.mail)
          playerV07.mail.Add(UpgradeFrom(legacyData2));
      }
      playerV07.keys = legacyData.keys;
      playerV07.progress = legacyData.progress == null ? new Dictionary<ProgressDirector.ProgressType, int>() : new Dictionary<ProgressDirector.ProgressType, int>(legacyData.progress);
      playerV07.delayedProgress = legacyData.delayedProgress == null ? new Dictionary<ProgressDirector.ProgressType, List<double>>() : new Dictionary<ProgressDirector.ProgressType, List<double>>(PlayerV06.UpgradeFrom(legacyData.delayedProgress));
      playerV07.currencyEverCollected = legacyData.currencyEverCollected;
      playerV07.gameMode = legacyData.gameMode;
      playerV07.gameIconId = legacyData.gameIconId;
      playerV07.version = legacyData.version;
      playerV07.blueprints = new List<Gadget.Id>();
      playerV07.availBlueprints = new List<Gadget.Id>();
      playerV07.blueprintLocks = new Dictionary<Gadget.Id, GadgetDirector.BlueprintLockData>();
      playerV07.gadgets = new Dictionary<Gadget.Id, int>();
      playerV07.craftMatCounts = new Dictionary<Identifiable.Id, int>();
      playerV07.availUpgrades = new List<PlayerState.Upgrade>();
      foreach (PlayerState.Upgrade key in System.Enum.GetValues(typeof (PlayerState.Upgrade)))
      {
        if (!playerV07.upgradeLocks.ContainsKey(key))
          playerV07.availUpgrades.Add(key);
      }
      return playerV07;
    }

    private MailV02 UpgradeFrom(MailDirector.Mail legacyData)
    {
      MailV02 mailV02 = new MailV02();
      if (legacyData == null)
        return mailV02;
      mailV02.isRead = legacyData.read;
      mailV02.mailType = legacyData.type;
      mailV02.messageKey = legacyData.key;
      return mailV02;
    }

    private RanchV05 UpgradeFrom(RanchData legacyData)
    {
      RanchV05 ranchV05 = new RanchV05();
      if (legacyData == null)
        return ranchV05;
      Dictionary<Vector3V02, AccessDoor.State> legacyData1 = new Dictionary<Vector3V02, AccessDoor.State>();
      if (legacyData.GetAccessDoorStates() != null)
      {
        foreach (KeyValuePair<Vector3, AccessDoor.State> accessDoorState in legacyData.GetAccessDoorStates())
          legacyData1.Add(UpgradeFrom(accessDoorState.Key), accessDoorState.Value);
      }
      ranchV05.accessDoorStates = RanchV05.UpgradeDoorsFrom(legacyData1);
      ranchV05.plots = new List<LandPlotV04>();
      if (legacyData.GetPlots() != null)
      {
        foreach (RanchData.LandPlotData plot in legacyData.GetPlots())
          ranchV05.plots.Add(UpgradeFrom(plot));
      }
      return ranchV05;
    }

    private LandPlotV04 UpgradeFrom(RanchData.LandPlotData legacyData)
    {
      LandPlotV04 landPlotV04 = new LandPlotV04();
      if (legacyData == null)
        return landPlotV04;
      landPlotV04.id = LandPlotV04.GetIdFromPos(UpgradeFrom(legacyData.pos));
      landPlotV04.typeId = legacyData.id;
      landPlotV04.upgrades = new List<LandPlot.Upgrade>();
      if (legacyData.upgrades != null)
      {
        foreach (LandPlot.Upgrade upgrade in legacyData.upgrades)
          landPlotV04.upgrades.Add(upgrade);
      }
      landPlotV04.attachedId = legacyData.attachedId;
      landPlotV04.attachedDeathTime = legacyData.attachedDeathTime;
      landPlotV04.siloAmmo = new Dictionary<SiloStorage.StorageType, List<AmmoDataV02>>();
      if (legacyData.siloAmmo != null)
      {
        foreach (KeyValuePair<SiloStorage.StorageType, Ammo.AmmoData[]> keyValuePair in legacyData.siloAmmo)
        {
          List<AmmoDataV02> ammoDataV02List = new List<AmmoDataV02>();
          foreach (Ammo.AmmoData legacyData1 in keyValuePair.Value)
            ammoDataV02List.Add(UpgradeFrom(legacyData1));
          landPlotV04.siloAmmo.Add(keyValuePair.Key, ammoDataV02List);
        }
      }
      landPlotV04.feederNextTime = legacyData.feederNextTime;
      landPlotV04.feederPendingCount = legacyData.feederPendingCount;
      landPlotV04.collectorNextTime = legacyData.collectorNextTime;
      landPlotV04.fastforwarderDisableTime = legacyData.fastforwarderDisableTime;
      return landPlotV04;
    }

    private AmmoDataV02 UpgradeFrom(Ammo.AmmoData legacyData)
    {
      AmmoDataV02 ammoDataV02 = new AmmoDataV02();
      if (legacyData == null)
        return ammoDataV02;
      ammoDataV02.count = legacyData.count;
      ammoDataV02.emotionData = UpgradeFrom(legacyData.emotionData);
      ammoDataV02.id = legacyData.id;
      return ammoDataV02;
    }

    private List<ActorDataV05> UpgradeFrom(ActorsData legacyData)
    {
      List<ActorDataV05> actorDataV05List = new List<ActorDataV05>();
      if (legacyData == null || legacyData.GetActors() == null)
        return actorDataV05List;
      foreach (ActorsData.ActorData actor in legacyData.GetActors())
        actorDataV05List.Add(UpgradeFrom(actor));
      return actorDataV05List;
    }

    private ActorDataV05 UpgradeFrom(ActorsData.ActorData legacyData)
    {
      ActorDataV05 actorDataV05_1 = new ActorDataV05();
      if (legacyData == null)
        return actorDataV05_1;
      actorDataV05_1.pos = UpgradeFrom(legacyData.pos);
      actorDataV05_1.rot = UpgradeFrom(legacyData.rot);
      actorDataV05_1.id = (int) legacyData.id;
      actorDataV05_1.emotions = UpgradeFrom(legacyData.emotions);
      actorDataV05_1.transformTime = legacyData.transformTime;
      actorDataV05_1.reproduceTime = legacyData.reproduceTime;
      actorDataV05_1.cycleData = UpgradeFrom(legacyData.cycleData);
      ActorDataV05 actorDataV05_2 = actorDataV05_1;
      float? disabledAtTime = legacyData.disabledAtTime;
      double? nullable = disabledAtTime.HasValue ? new double?(disabledAtTime.GetValueOrDefault()) : new double?();
      actorDataV05_2.disabledAtTime = nullable;
      actorDataV05_1.isFeral = false;
      actorDataV05_1.fashions = new List<Identifiable.Id>();
      return actorDataV05_1;
    }

    private SlimeEmotionDataV02 UpgradeFrom(SlimeEmotionData legacyData)
    {
      SlimeEmotionDataV02 slimeEmotionDataV02 = new SlimeEmotionDataV02();
      slimeEmotionDataV02.emotionData = new Dictionary<SlimeEmotions.Emotion, float>();
      if (legacyData == null)
        return slimeEmotionDataV02;
      foreach (KeyValuePair<SlimeEmotions.Emotion, float> keyValuePair in legacyData)
        slimeEmotionDataV02.emotionData.Add(keyValuePair.Key, keyValuePair.Value);
      return slimeEmotionDataV02;
    }

    private ResourceCycleDataV03 UpgradeFrom(ResourceCycle.CycleData legacyData)
    {
      ResourceCycleDataV03 resourceCycleDataV03 = new ResourceCycleDataV03();
      if (legacyData == null)
        return resourceCycleDataV03;
      resourceCycleDataV03.progressTime = legacyData.progressTime;
      resourceCycleDataV03.state = legacyData.state;
      return resourceCycleDataV03;
    }

    private PediaV02 UpgradeFrom(PediaData legacyData)
    {
      PediaV02 pediaV02 = new PediaV02();
      if (legacyData == null)
        return pediaV02;
      pediaV02.progressGivenForPediaCount = legacyData.progressGivenForPediaCount;
      pediaV02.unlockedIds = new List<string>();
      if (legacyData.unlockedIds != null)
      {
        foreach (string unlockedId in legacyData.unlockedIds)
          pediaV02.unlockedIds.Add(unlockedId);
      }
      pediaV02.completedTuts = new List<string>();
      if (legacyData.completedTuts != null)
      {
        foreach (string completedTut in pediaV02.completedTuts)
          pediaV02.completedTuts.Add(completedTut);
      }
      return pediaV02;
    }

    private Vector3V02 UpgradeFrom(Vector3 legacyData) => new Vector3V02()
    {
      value = new Vector3(legacyData.x, legacyData.y, legacyData.z)
    };

    public static void AssertAreEqual(GameV04 expected, GameV04 actual)
    {
      WorldV13.AssertAreEqual(expected.world, actual.world);
      GameAchieveV03.AssertAreEqual(expected.achieve, actual.achieve);
      PediaV02.AssertAreEqual(expected.pedia, actual.pedia);
      PlayerV07.AssertAreEqual(expected.player, actual.player);
      RanchV05.AssertAreEqual(expected.ranch, actual.ranch);
      for (int index = 0; index < expected.actors.Count; ++index)
        ActorDataV05.AssertAreEqual(expected.actors[index], actual.actors[index]);
    }
  }
}
