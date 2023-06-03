// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.SavedGame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Persist;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MonomiPark.SlimeRancher
{
  public class SavedGame
  {
    private const float MAX_DIST_MATCH = 5f;
    private const float MAX_DIST_MATCH_SQR = 25f;
    private const float MAX_DIST_CLOSE_MATCH = 0.1f;
    public const float MAX_DIST_CLOSE_MATCH_SQR = 0.0100000007f;
    private const float MAX_FAR_DIST_MATCH = 10f;
    public const float MAX_FAR_DIST_MATCH_SQR = 100f;
    private const float MAX_ACTOR_POSITION_COORD = 1E+07f;
    private GameV12 gameState;
    private PrefabInstantiator prefabInstantiator;
    private SavedGameInfoProvider savedGameInfoProvider;
    private const string EXTENSION = ".sav";
    private const string TEMP_EXTENSION = ".tmp";

    public SavedGame(
      PrefabInstantiator prefabInstantiator,
      SavedGameInfoProvider gameModeEndTimeProvider)
    {
      this.prefabInstantiator = prefabInstantiator;
      savedGameInfoProvider = gameModeEndTimeProvider;
    }

    public GameV12 GameState => gameState;

    public string GetDisplayName() => gameState != null ? gameState.displayName : "";

    public string GetName() => gameState != null ? gameState.gameName : "";

    public void ClearName()
    {
      if (gameState == null)
        return;
      gameState.gameName = null;
    }

    public GameData.Summary LoadSummary(string saveName, Stream gameData)
    {
      try
      {
        GameV12 gameV12 = new GameV12();
        gameV12.LoadSummary(gameData);
        if (string.IsNullOrEmpty(gameV12.displayName))
          gameV12.displayName = saveName;
        return new GameData.Summary(gameV12.gameName, gameV12.displayName, gameV12.summary.iconId, gameV12.summary.gameMode, gameV12.summary.version, (int) Math.Floor(gameV12.summary.worldTime * 1.1574074051168282E-05), gameV12.summary.currency, gameV12.summary.pediaCount, gameV12.summary.isGameOver, gameV12.summary.saveTimestamp, saveName, gameV12.summary.saveNumber);
      }
      catch (Exception ex)
      {
        Log.Warning("Error while loading saved game summary.", "name", saveName, "Exception", ex.Message, "Stack Trace", ex.StackTrace);
        return new GameData.Summary(saveName);
      }
    }

    public void CreateNew(string name, string displayName)
    {
      gameState = new GameV12();
      gameState.gameName = name;
      gameState.displayName = displayName;
    }

    public void Load(Stream stream)
    {
      GameV12 gameV12 = new GameV12();
      gameV12.Load(stream);
      gameState = gameV12;
    }

    public void Save(Stream stream) => gameState.Write(stream);

    public void Push(GameModel gameModel)
    {
      if (gameState == null)
        throw new InvalidOperationException("There is no game state to restore.");
      PushBase(gameModel, gameState.player);
      PushWorldGlobal(gameModel, gameState.world);
      Push(gameModel, gameState.achieve);
      Push(gameModel, gameState.player);
      Push(gameModel, gameState.pedia);
      Push(gameModel, gameState.ranch);
      PushWorldItems(gameModel, gameState.world);
      Push(gameModel, gameState.actors, gameState.world);
      Push(gameModel, gameState.holiday);
      Push(gameModel, gameState.appearances);
      Push(gameModel, gameState.instrument);
    }

    public void Push(GameModel gameModel, RanchV07 ranch)
    {
      Dictionary<string, LandPlotModel> scenePlots = new Dictionary<string, LandPlotModel>(gameModel.AllLandPlots());
      foreach (LandPlotV08 plot in ranch.plots)
        LandPlotToGameObject(plot, scenePlots);
      if (scenePlots.Count > 0)
      {
        Log.Warning("Remaining unreplaced plots: " + scenePlots.Count);
        foreach (LandPlotModel landPlotModel in scenePlots.Values)
        {
          landPlotModel.Init();
          landPlotModel.NotifyParticipants();
        }
      }
      Dictionary<string, AccessDoorModel> dictionary = new Dictionary<string, AccessDoorModel>(gameModel.AllDoors());
      foreach (KeyValuePair<string, AccessDoor.State> accessDoorState in ranch.accessDoorStates)
      {
        AccessDoorModel accessDoorModel;
        dictionary.TryGetValue(accessDoorState.Key, out accessDoorModel);
        if (accessDoorModel != null)
        {
          dictionary.Remove(accessDoorState.Key);
          accessDoorModel.Init();
          accessDoorModel.Push(accessDoorState.Value);
        }
        else
          Log.Debug("Skipping deserializing door, as it's missing.", "id", accessDoorState.Key);
      }
      if (dictionary.Count > 0)
      {
        Log.Warning("Remaining unreplaced doors: " + dictionary.Count);
        foreach (AccessDoorModel accessDoorModel in dictionary.Values)
          accessDoorModel.Init();
      }
      foreach (AccessDoorModel accessDoorModel in gameModel.AllDoors().Values)
        accessDoorModel.NotifyParticipants();
      RanchModel ranchModel = gameModel.GetRanchModel();
      ranchModel.Init();
      ranchModel.Push(ranch.palettes, ranch.ranchFastForward);
      ranchModel.NotifyParticipants();
    }

    private void LandPlotToGameObject(
      LandPlotV08 plotData,
      Dictionary<string, LandPlotModel> scenePlots)
    {
      LandPlotModel plotModel;
      scenePlots.TryGetValue(plotData.id, out plotModel);
      if (plotModel == null)
      {
        Log.Warning("Did not find plot: " + plotData.id);
      }
      else
      {
        scenePlots.Remove(plotData.id);
        prefabInstantiator.InstantiatePlot(plotData.typeId, plotModel, true);
        Dictionary<SiloStorage.StorageType, Ammo.Slot[]> siloAmmo = new Dictionary<SiloStorage.StorageType, Ammo.Slot[]>();
        foreach (KeyValuePair<SiloStorage.StorageType, List<AmmoDataV02>> keyValuePair in plotData.siloAmmo)
          siloAmmo[keyValuePair.Key] = AmmoDataToSlots(keyValuePair.Value);
        plotModel.Init();
        plotModel.Push(plotData.feederNextTime, plotData.feederPendingCount, plotData.feederCycleSpeed, plotData.collectorNextTime, plotData.typeId, plotData.attachedId, plotData.upgrades, siloAmmo, plotData.siloActivatorIndices, plotData.ashUnits);
        plotModel.NotifyParticipants();
      }
    }

    private void PushWorldGlobal(GameModel gameModel, WorldV22 world)
    {
      Dictionary<ExchangeDirector.OfferType, ExchangeDirector.Offer> offers = new Dictionary<ExchangeDirector.OfferType, ExchangeDirector.Offer>();
      foreach (KeyValuePair<ExchangeDirector.OfferType, ExchangeOfferV04> offer in world.offers)
        offers[offer.Key] = FromOfferData(offer.Value);
      WorldModel worldModel = gameModel.GetWorldModel();
      worldModel.Init();
      worldModel.Push(world.econSeed, world.econSaturations, world.worldTime, offers, world.dailyOfferCreateTime, world.lastOfferRancherIds, world.pendingOfferRancherIds, world.weather, world.weatherUntil, world.firestorm.endStormTime, world.firestorm.nextStormTime, world.firestorm.stormPreparing, world.activeGingerPatches, world.occupiedPhaseSites);
      worldModel.NotifyParticipants();
    }

    private void PushWorldItems(GameModel gameModel, WorldV22 world)
    {
      SetSpawnTimes(gameModel, world.resourceSpawnerWater);
      SetKookadobaNodes(gameModel);
      SetTriggerTimes(gameModel, world.spawnerTriggerTimes);
      SetAnimalSpawnTimes(gameModel, world.animalSpawnerTimes);
      SetLiquidUnits(gameModel, world.liquidSourceUnits);
      SetGordos(gameModel, world.gordos);
      SetEchoNoteGordos(gameModel, world.echoNoteGordos);
      SetPlacedGadgets(gameModel, world.placedGadgets);
      SetTreasurePods(gameModel, world.treasurePods);
      SetSwitches(gameModel, world.switches);
      SetPuzzleSlotsFilled(gameModel, world.puzzleSlotsFilled);
      SetOasisStates(gameModel, world.oasisStates);
      SetQuicksilverEnergyGenerators(gameModel, world.quicksilverEnergyGenerators);
      GlitchSlimulationModel glitch = gameModel.Glitch;
      glitch.Init();
      glitch.Push(world.glitch);
      glitch.NotifyParticipants();
    }

    private void SetQuicksilverEnergyGenerators(
      GameModel gameModel,
      Dictionary<string, QuicksilverEnergyGeneratorV02> generators)
    {
      foreach (KeyValuePair<string, QuicksilverEnergyGeneratorModel> allGenerator in gameModel.AllGenerators())
      {
        allGenerator.Value.Init();
        if (generators.ContainsKey(allGenerator.Key))
        {
          QuicksilverEnergyGeneratorV02 generator = generators[allGenerator.Key];
          allGenerator.Value.Push(generator.state, generator.timer);
        }
        allGenerator.Value.NotifyParticipants();
      }
    }

    private void SetSpawnTimes(
      GameModel gameModel,
      Dictionary<Vector3V02, ResourceWaterV03> resourceSpawnerWater)
    {
      float num = 0.0100000007f;
      foreach (SpawnResourceModel allResourceSpawner in gameModel.AllResourceSpawners())
      {
        allResourceSpawner.Init();
        Vector3 pos = allResourceSpawner.pos;
        bool flag = false;
        foreach (KeyValuePair<Vector3V02, ResourceWaterV03> keyValuePair in resourceSpawnerWater)
        {
          if ((keyValuePair.Key.value - pos).sqrMagnitude < (double) num)
          {
            allResourceSpawner.Push(keyValuePair.Value.water, keyValuePair.Value.spawn);
            flag = true;
            break;
          }
        }
        if (!flag)
          Debug.Log("Skipping deserializing spawn time, as it's missing.");
        allResourceSpawner.NotifyParticipants();
      }
    }

    private void SetKookadobaNodes(GameModel gameModel)
    {
      foreach (KookadobaNodeModel allKookadobaNode in gameModel.AllKookadobaNodes())
      {
        allKookadobaNode.Init();
        allKookadobaNode.NotifyParticipants();
      }
    }

    private void SetTriggerTimes(
      GameModel gameModel,
      Dictionary<Vector3V02, double> spawnerTriggerTimes)
    {
      float num = 0.0100000007f;
      foreach (SpawnerTriggerModel allSpawnerTrigger in gameModel.AllSpawnerTriggers())
      {
        allSpawnerTrigger.Init();
        Vector3 pos = allSpawnerTrigger.pos;
        bool flag = false;
        foreach (KeyValuePair<Vector3V02, double> spawnerTriggerTime in spawnerTriggerTimes)
        {
          if ((spawnerTriggerTime.Key.value - pos).sqrMagnitude < (double) num)
          {
            allSpawnerTrigger.Push(spawnerTriggerTime.Value);
            flag = true;
            break;
          }
        }
        if (!flag)
          Debug.Log("Skipping deserializing spawn time, as it's missing.");
        allSpawnerTrigger.NotifyParticipants();
      }
    }

    private void SetAnimalSpawnTimes(
      GameModel gameModel,
      Dictionary<Vector3V02, double> animalSpawnerTimes)
    {
      float num = 0.0100000007f;
      foreach (DirectedAnimalSpawnerModel allAnimalSpawner in gameModel.AllAnimalSpawners())
      {
        allAnimalSpawner.Init();
        Vector3 pos = allAnimalSpawner.pos;
        bool flag = false;
        foreach (KeyValuePair<Vector3V02, double> animalSpawnerTime in animalSpawnerTimes)
        {
          if ((animalSpawnerTime.Key.value - pos).sqrMagnitude < (double) num)
          {
            allAnimalSpawner.Push(animalSpawnerTime.Value);
            flag = true;
            break;
          }
        }
        if (!flag)
          Debug.Log("Skipping deserializing spawn time, as it's missing.");
        allAnimalSpawner.NotifyParticipants();
      }
    }

    private void SetLiquidUnits(GameModel gameModel, Dictionary<string, float> data)
    {
      Dictionary<string, LiquidSourceModel> dictionary = gameModel.LiquidSources.StaticInstances.ToDictionary(kv => kv.Key, kv => kv.Value);
      foreach (KeyValuePair<string, float> keyValuePair in data)
      {
        LiquidSourceModel liquidSourceModel;
        dictionary.TryGetValue(keyValuePair.Key, out liquidSourceModel);
        if (liquidSourceModel != null)
        {
          dictionary.Remove(keyValuePair.Key);
          liquidSourceModel.Init();
          liquidSourceModel.Push(keyValuePair.Value);
          liquidSourceModel.NotifyParticipants();
        }
        else
          Log.Debug("Skipping deserializing LiquidSource, as it's missing.", "id", keyValuePair.Key);
      }
      if (dictionary.Count <= 0)
        return;
      Log.Warning("Remaining unreplaced LiquidSources: " + dictionary.Count);
      foreach (LiquidSourceModel liquidSourceModel in dictionary.Values)
      {
        liquidSourceModel.Init();
        liquidSourceModel.NotifyParticipants();
      }
    }

    private void SetGordos(GameModel gameModel, Dictionary<string, GordoV01> gordos)
    {
      Dictionary<string, GordoModel> dictionary = new Dictionary<string, GordoModel>(gameModel.AllGordos());
      foreach (KeyValuePair<string, GordoV01> gordo in gordos)
      {
        GordoModel gordoModel;
        dictionary.TryGetValue(gordo.Key, out gordoModel);
        if (gordoModel != null)
        {
          dictionary.Remove(gordo.Key);
          gordoModel.Init();
          gordoModel.Push(gordo.Value.eatenCount, gordo.Value.fashions);
          gordoModel.NotifyParticipants();
        }
        else
          Log.Debug("Skipping deserializing gordo, as it's missing.", "id", gordo.Key);
      }
      if (dictionary.Count <= 0)
        return;
      Log.Warning("Remaining unreplaced gordos: " + dictionary.Count);
      foreach (GordoModel gordoModel in dictionary.Values)
      {
        gordoModel.Init();
        gordoModel.NotifyParticipants();
      }
    }

    private void SetEchoNoteGordos(GameModel gameModel, Dictionary<string, EchoNoteGordoV01> gordos)
    {
      Dictionary<string, EchoNoteGordoModel> dictionary = new Dictionary<string, EchoNoteGordoModel>(gameModel.AllEchoNoteGordos());
      foreach (KeyValuePair<string, EchoNoteGordoV01> gordo in gordos)
      {
        EchoNoteGordoModel echoNoteGordoModel;
        dictionary.TryGetValue(gordo.Key, out echoNoteGordoModel);
        if (echoNoteGordoModel != null)
        {
          dictionary.Remove(gordo.Key);
          echoNoteGordoModel.Init();
          echoNoteGordoModel.Push(gordo.Value);
          echoNoteGordoModel.NotifyParticipants();
        }
        else
          Log.Debug("Skipping deserializing EchoNoteGordo, as it's missing.", "id", gordo.Key);
      }
      if (dictionary.Count <= 0)
        return;
      Log.Warning("Remaining unreplaced EchoNoteGordo: " + dictionary.Count);
      foreach (EchoNoteGordoModel echoNoteGordoModel in dictionary.Values)
      {
        echoNoteGordoModel.Init();
        echoNoteGordoModel.NotifyParticipants();
      }
    }

    private void SetPlacedGadgets(
      GameModel gameModel,
      Dictionary<string, PlacedGadgetV08> placedGadgets)
    {
      Dictionary<string, PlacedGadgetV08> dictionary = new Dictionary<string, PlacedGadgetV08>(placedGadgets);
      foreach (KeyValuePair<string, GadgetSiteModel> allGadgetSite in gameModel.AllGadgetSites())
      {
        allGadgetSite.Value.Init();
        if (dictionary.ContainsKey(allGadgetSite.Key))
        {
          PlacedGadgetV08 gadget = dictionary[allGadgetSite.Key];
          Push(gameModel, gadget, allGadgetSite.Value);
          dictionary.Remove(allGadgetSite.Key);
        }
        allGadgetSite.Value.NotifyParticipants();
      }
    }

    private void SetTreasurePods(
      GameModel gameModel,
      Dictionary<string, TreasurePodV01> treasurePods)
    {
      Dictionary<string, TreasurePodModel> dictionary = new Dictionary<string, TreasurePodModel>(gameModel.AllPods());
      foreach (KeyValuePair<string, TreasurePodV01> treasurePod in treasurePods)
      {
        TreasurePodModel treasurePodModel;
        dictionary.TryGetValue(treasurePod.Key, out treasurePodModel);
        if (treasurePodModel != null)
        {
          dictionary.Remove(treasurePod.Key);
          treasurePodModel.Init();
          treasurePodModel.Push(treasurePod.Value.state, treasurePod.Value.spawnQueue);
          treasurePodModel.NotifyParticipants();
        }
        else
          Log.Debug("Skipping deserializing treasure pod, as it's missing.", "id", treasurePod.Key);
      }
      if (dictionary.Count <= 0)
        return;
      Log.Warning("Remaining unreplaced pods: " + dictionary.Count);
      foreach (TreasurePodModel treasurePodModel in dictionary.Values)
      {
        treasurePodModel.Init();
        treasurePodModel.NotifyParticipants();
      }
    }

    private void SetSwitches(GameModel gameModel, Dictionary<string, SwitchHandler.State> switches)
    {
      Dictionary<string, MasterSwitchModel> dictionary = new Dictionary<string, MasterSwitchModel>(gameModel.AllSwitches());
      foreach (KeyValuePair<string, SwitchHandler.State> keyValuePair in switches)
      {
        MasterSwitchModel masterSwitchModel;
        dictionary.TryGetValue(keyValuePair.Key, out masterSwitchModel);
        if (masterSwitchModel != null)
        {
          dictionary.Remove(keyValuePair.Key);
          masterSwitchModel.Init();
          masterSwitchModel.Push(keyValuePair.Value);
          masterSwitchModel.NotifyParticipants();
        }
        else
          Log.Debug("Skipping deserializing master switch, as it's missing.", "id", keyValuePair.Key);
      }
      if (dictionary.Count <= 0)
        return;
      Log.Warning("Remaining unreplaced switches: " + dictionary.Count);
      foreach (MasterSwitchModel masterSwitchModel in dictionary.Values)
      {
        masterSwitchModel.Init();
        masterSwitchModel.NotifyParticipants();
      }
    }

    private void SetPuzzleSlotsFilled(
      GameModel gameModel,
      Dictionary<string, bool> puzzleSlotsFilled)
    {
      Dictionary<string, PuzzleSlotModel> dictionary = new Dictionary<string, PuzzleSlotModel>(gameModel.AllSlots());
      foreach (KeyValuePair<string, bool> keyValuePair in puzzleSlotsFilled)
      {
        PuzzleSlotModel puzzleSlotModel;
        dictionary.TryGetValue(keyValuePair.Key, out puzzleSlotModel);
        if (puzzleSlotModel != null)
        {
          dictionary.Remove(keyValuePair.Key);
          puzzleSlotModel.Init();
          puzzleSlotModel.Push(keyValuePair.Value);
          puzzleSlotModel.NotifyParticipants();
        }
        else
          Log.Debug("Skipping deserializing puzzle slot, as it's missing.", "id", keyValuePair.Key);
      }
      if (dictionary.Count <= 0)
        return;
      Log.Warning("Remaining unreplaced pods: " + dictionary.Count);
      foreach (PuzzleSlotModel puzzleSlotModel in dictionary.Values)
      {
        puzzleSlotModel.Init();
        puzzleSlotModel.NotifyParticipants();
      }
    }

    private void SetOasisStates(GameModel gameModel, Dictionary<string, bool> oases)
    {
      Oasis.oasisSpheres.Clear();
      Dictionary<string, OasisModel> dictionary = new Dictionary<string, OasisModel>(gameModel.AllOases());
      foreach (KeyValuePair<string, bool> oasis in oases)
      {
        OasisModel oasisModel;
        dictionary.TryGetValue(oasis.Key, out oasisModel);
        if (oasisModel != null)
        {
          dictionary.Remove(oasis.Key);
          oasisModel.Init();
          oasisModel.Push(oasis.Value);
          oasisModel.NotifyParticipants();
        }
        else
          Log.Debug("Skipping deserializing master switch, as it's missing.", "id", oasis.Key);
      }
      if (dictionary.Count <= 0)
        return;
      Log.Warning("Remaining unreplaced switches: " + dictionary.Count);
      foreach (OasisModel oasisModel in dictionary.Values)
      {
        oasisModel.Init();
        oasisModel.NotifyParticipants();
      }
    }

    private void Push(GameModel gameModel, PlacedGadgetV08 gadget, GadgetSiteModel siteModel)
    {
      GameObject gameObj = prefabInstantiator.InstantiateGadget(gadget.gadgetId, siteModel, gameModel);
      GadgetModel attached = siteModel.attached;
      attached.PushBase(gadget.waitForChargeupTime, gadget.yRotation);
      switch (attached)
      {
        case ExtractorModel _:
          ((ExtractorModel) attached).Push(gadget.extractorCyclesRemaining, gadget.extractorQueuedToProduce, gadget.extractorCycleEndTime, gadget.extractorNextProduceTime);
          break;
        case WarpDepotModel _:
          ((WarpDepotModel) attached).Push(gadget.isPrimaryInLink, AmmoDataToSlots(gadget.ammo));
          break;
        case SnareModel _:
          ((SnareModel) attached).Push(gadget.baitTypeId, gadget.gordoTypeId, gadget.gordoEatenCount, gadget.fashions);
          break;
        case EchoNetModel _:
          ((EchoNetModel) attached).Push(gadget.lastSpawnTime);
          break;
        case DroneModel _:
          ((DroneModel) attached).Push(gadget.drone.drone.position.value, gadget.drone.drone.rotation.value, AmmoDataToSlots(new List<AmmoDataV02>()
          {
            gadget.drone.drone.ammo
          }), gadget.drone.drone.fashions, gadget.drone.drone.noClip, gadget.drone.station.battery.time, gadget.drone.programs);
          break;
        default:
          BasicGadgetModel basicGadgetModel = attached as BasicGadgetModel;
          break;
      }
      attached.NotifyParticipants(gameObj);
    }

    private Dictionary<PlayerState.AmmoMode, Ammo.Slot[]> AmmoDataToSlots(
      Dictionary<PlayerState.AmmoMode, List<AmmoDataV02>> ammo)
    {
      if (ammo == null)
        return null;
      Dictionary<PlayerState.AmmoMode, Ammo.Slot[]> slots = new Dictionary<PlayerState.AmmoMode, Ammo.Slot[]>();
      foreach (KeyValuePair<PlayerState.AmmoMode, List<AmmoDataV02>> keyValuePair in ammo)
        slots[keyValuePair.Key] = AmmoDataToSlots(keyValuePair.Value);
      return slots;
    }

    private Ammo.Slot[] AmmoDataToSlots(List<AmmoDataV02> ammo)
    {
      Ammo.Slot[] slots = new Ammo.Slot[ammo.Count];
      for (int index = 0; index < ammo.Count; ++index)
      {
        if (ammo[index] == null)
        {
          slots[index] = new Ammo.Slot(Identifiable.Id.NONE, 0);
        }
        else
        {
          slots[index] = new Ammo.Slot(ammo[index].id, ammo[index].count);
          slots[index].emotions = new SlimeEmotionData();
          foreach (KeyValuePair<SlimeEmotions.Emotion, float> keyValuePair in ammo[index].emotionData.emotionData)
            slots[index].emotions[keyValuePair.Key] = keyValuePair.Value;
        }
      }
      return slots;
    }

    private void PushBase(GameModel gameModel, PlayerV14 player)
    {
      gameModel.Init();
      gameModel.Push(player.gameMode, player.gameIconId);
      gameModel.NotifyParticipants();
    }

    private void Push(GameModel gameModel, PlayerV14 player)
    {
      PlayerModel playerModel = gameModel.GetPlayerModel();
      playerModel.Init();
      playerModel.Push(player.health, player.energy, player.radiation, player.currency, player.currencyEverCollected, player.keys, AmmoDataToSlots(player.ammo), player.upgrades, player.availUpgrades, player.upgradeLocks, player.unlockedZoneMaps, player.regionSetId, player.playerPos.value, player.playerRotEuler.value, player.endGameTime);
      playerModel.NotifyParticipants();
      List<MailDirector.Mail> mail = new List<MailDirector.Mail>();
      foreach (MailV02 mailV02 in player.mail)
      {
        if (mailV02.mailType != MailDirector.Type.UPGRADE)
          mail.Add(new MailDirector.Mail()
          {
            key = mailV02.messageKey,
            read = mailV02.isRead,
            type = mailV02.mailType
          });
      }
      MailModel mailModel = gameModel.GetMailModel();
      mailModel.Init();
      mailModel.Push(mail);
      mailModel.NotifyParticipants();
      ProgressModel progressModel = gameModel.GetProgressModel();
      progressModel.Init();
      progressModel.Push(player.progress, player.delayedProgress);
      progressModel.NotifyParticipants();
      GadgetsModel gadgetsModel = gameModel.GetGadgetsModel();
      gadgetsModel.Init();
      gadgetsModel.Push(player.blueprints, player.availBlueprints, player.blueprintLocks, player.gadgets, player.craftMatCounts);
      gadgetsModel.NotifyParticipants();
      DecorizerModel decorizerModel = gameModel.GetDecorizerModel();
      decorizerModel.Init();
      decorizerModel.Push(player.decorizer);
      decorizerModel.NotifyParticipants();
    }

    private void Push(GameModel gameModel, List<ActorDataV09> actors, WorldV22 world)
    {
      foreach (ActorDataV09 actor in actors)
        PushActorData(gameModel, actor, world);
    }

    private bool IsInvalidActorPosition(Vector3 pos) => float.IsNaN(pos.x) || pos.x > 10000000.0 || float.IsNaN(pos.y) || pos.y > 10000000.0 || float.IsNaN(pos.z) || pos.z > 10000000.0;

    private void PushActorData(GameModel gameModel, ActorDataV09 actorData, WorldV22 world)
    {
      if (IsInvalidActorPosition(actorData.pos.value))
      {
        Log.Warning("Actor has invalid position during load. Skipping. id: " + (Identifiable.Id) actorData.typeId);
      }
      else
      {
        GameObject gameObj = prefabInstantiator.InstantiateActor(actorData.actorId, (Identifiable.Id) actorData.typeId, actorData.regionSetId, actorData.pos.value, actorData.rot.value, gameModel);
        ActorModel actorModel = gameModel.GetActorModel(actorData.actorId);
        switch (actorModel)
        {
          case SlimeModel _:
            ((SlimeModel) actorModel).Push(actorData);
            if (actorModel is GlitchSlimeModel)
            {
              ((GlitchSlimeModel) actorModel).Push(world.glitch.slimes[actorData.actorId]);
              break;
            }
            break;
          case PlortModel _:
            ((PlortModel) actorModel).Push(actorData.destroyTime);
            break;
          case AnimalModel _:
            ((AnimalModel) actorModel).Push(actorData);
            break;
          case ProduceModel _:
            ((ProduceModel) actorModel).Push(actorData.cycleData.state, actorData.cycleData.progressTime);
            break;
          default:
            ScienceMatModel scienceMatModel = actorModel as ScienceMatModel;
            break;
        }
        actorModel.NotifyParticipants(gameObj);
      }
    }

    public void Push(GameModel gameModel, PediaV03 pedia)
    {
      List<PediaDirector.Id> enums1 = StringsToEnums<PediaDirector.Id>(pedia.unlockedIds);
      PediaModel pediaModel = gameModel.GetPediaModel();
      pediaModel.Init();
      pediaModel.Push(pedia.progressGivenForPediaCount, enums1);
      pediaModel.NotifyParticipants();
      if (pedia.completedTuts == null)
      {
        Debug.Log("Had to create non-null completed tuts ids list.");
        pedia.completedTuts = new List<string>();
      }
      List<TutorialDirector.Id> enums2 = StringsToEnums<TutorialDirector.Id>(pedia.completedTuts);
      List<TutorialDirector.Id> enums3 = StringsToEnums<TutorialDirector.Id>(pedia.popupQueue);
      TutorialsModel tutorialsModel = gameModel.GetTutorialsModel();
      tutorialsModel.Init();
      tutorialsModel.Push(enums2, enums3);
      tutorialsModel.NotifyParticipants();
    }

    private static List<T> StringsToEnums<T>(IEnumerable<string> strings)
    {
      List<T> enums = new List<T>();
      if (strings != null)
      {
        foreach (string str in strings)
        {
          try
          {
            enums.Add((T) Enum.Parse(typeof (T), str));
          }
          catch (Exception ex)
          {
          }
        }
      }
      return enums;
    }

    public void Push(GameModel gameModel, GameAchieveV03 achieve)
    {
      GameAchievesModel gameAchievesModel = gameModel.GetGameAchievesModel();
      gameAchievesModel.Init();
      gameAchievesModel.Push(achieve.gameFloatStatDict, achieve.gameDoubleStatDict, achieve.gameIntStatDict, achieve.gameIdDictStatDict);
      gameAchievesModel.NotifyParticipants();
    }

    private void Push(GameModel game, HolidayDirectorV02 persistence)
    {
      HolidayModel holidayModel = game.GetHolidayModel();
      holidayModel.Init();
      holidayModel.Push(persistence);
      holidayModel.NotifyParticipants();
    }

    private void Push(GameModel game, AppearancesV01 persistence)
    {
      AppearancesModel appearancesModel = game.GetAppearancesModel();
      appearancesModel.Init();
      appearancesModel.Push(persistence);
      appearancesModel.NotifyParticipants();
    }

    private void Push(GameModel game, InstrumentV01 persistence)
    {
      InstrumentModel instrumentModel = game.GetInstrumentModel();
      instrumentModel.Init();
      instrumentModel.Push(persistence);
      instrumentModel.NotifyParticipants();
    }

    public void Pull(GameModel gameModel)
    {
      GameV12 gameV12 = new GameV12();
      if (gameState != null)
      {
        gameV12.gameName = gameState.gameName;
        gameV12.displayName = gameState.displayName;
      }
      Pull(gameModel, gameV12.world);
      Pull(gameModel, gameV12.player);
      Pull(gameModel, gameV12.ranch);
      Pull(gameModel, gameV12.actors, gameV12.world);
      Pull(gameModel, gameV12.pedia);
      Pull(gameModel, gameV12.achieve);
      gameV12.holiday = gameModel.GetHolidayModel().Pull();
      gameV12.appearances = gameModel.GetAppearancesModel().Pull();
      gameV12.instrument = gameModel.GetInstrumentModel().Pull();
      gameV12.summary = new GameSummaryV04();
      gameV12.summary.version = gameV12.player.version;
      gameV12.summary.gameMode = gameV12.player.gameMode;
      gameV12.summary.iconId = gameV12.player.gameIconId;
      gameV12.summary.worldTime = gameV12.world.worldTime;
      gameV12.summary.currency = gameV12.player.currency;
      gameV12.summary.pediaCount = gameV12.pedia.unlockedIds.Count;
      gameV12.summary.saveTimestamp = DateTimeOffset.UtcNow;
      gameV12.summary.isGameOver = gameModel.IsGameOver();
      gameV12.summary.saveNumber = gameState == null || gameState.summary == null ? 0UL : gameState.summary.saveNumber + 1UL;
      gameState = gameV12;
    }

    private void Pull(GameModel gameModel, WorldV22 world)
    {
      Dictionary<ExchangeDirector.OfferType, ExchangeDirector.Offer> offers;
      gameModel.GetWorldModel().Pull(out world.econSeed, out world.econSaturations, out world.worldTime, out offers, out world.dailyOfferCreateTime, out world.lastOfferRancherIds, out world.pendingOfferRancherIds, out world.weather, out world.weatherUntil, out world.firestorm.endStormTime, out world.firestorm.nextStormTime, out world.firestorm.stormPreparing, out world.activeGingerPatches, out world.occupiedPhaseSites);
      gameModel.Glitch.Pull(out world.glitch);
      world.offers = new Dictionary<ExchangeDirector.OfferType, ExchangeOfferV04>();
      foreach (KeyValuePair<ExchangeDirector.OfferType, ExchangeDirector.Offer> keyValuePair in offers)
        world.offers[keyValuePair.Key] = ToOfferData(keyValuePair.Value);
      world.resourceSpawnerWater = new Dictionary<Vector3V02, ResourceWaterV03>();
      foreach (SpawnResourceModel allResourceSpawner in gameModel.AllResourceSpawners())
      {
        Vector3V02 key = new Vector3V02()
        {
          value = allResourceSpawner.pos
        };
        ResourceWaterV03 resourceWaterV03 = new ResourceWaterV03();
        allResourceSpawner.Pull(out resourceWaterV03.water, out resourceWaterV03.spawn);
        world.resourceSpawnerWater[key] = resourceWaterV03;
      }
      world.spawnerTriggerTimes = new Dictionary<Vector3V02, double>();
      foreach (SpawnerTriggerModel allSpawnerTrigger in gameModel.AllSpawnerTriggers())
      {
        Vector3V02 key = new Vector3V02()
        {
          value = allSpawnerTrigger.pos
        };
        double nextTriggerTime;
        allSpawnerTrigger.Pull(out nextTriggerTime);
        world.spawnerTriggerTimes[key] = nextTriggerTime;
      }
      world.animalSpawnerTimes = new Dictionary<Vector3V02, double>();
      foreach (DirectedAnimalSpawnerModel allAnimalSpawner in gameModel.AllAnimalSpawners())
      {
        Vector3V02 key = new Vector3V02()
        {
          value = allAnimalSpawner.pos
        };
        double nextSpawnTime;
        allAnimalSpawner.Pull(out nextSpawnTime);
        world.animalSpawnerTimes[key] = nextSpawnTime;
      }
      world.liquidSourceUnits = new Dictionary<string, float>();
      foreach (KeyValuePair<string, LiquidSourceModel> staticInstance in gameModel.LiquidSources.StaticInstances)
      {
        float unitsFilled;
        staticInstance.Value.Pull(out unitsFilled);
        world.liquidSourceUnits[staticInstance.Key] = unitsFilled;
      }
      world.gordos = new Dictionary<string, GordoV01>();
      foreach (KeyValuePair<string, GordoModel> allGordo in gameModel.AllGordos())
      {
        GordoV01 gordoV01 = new GordoV01();
        allGordo.Value.Pull(out gordoV01.eatenCount, out gordoV01.fashions);
        world.gordos[allGordo.Key] = gordoV01;
      }
      world.echoNoteGordos = gameModel.AllEchoNoteGordos().ToDictionary(p => p.Key, p => p.Value.Pull());
      world.placedGadgets = new Dictionary<string, PlacedGadgetV08>();
      foreach (KeyValuePair<string, GadgetSiteModel> allGadgetSite in gameModel.AllGadgetSites())
      {
        if (allGadgetSite.Value.HasAttached())
        {
          PlacedGadgetV08 gadget = new PlacedGadgetV08();
          Pull(gameModel, gadget, allGadgetSite.Value.attached);
          world.placedGadgets[allGadgetSite.Key] = gadget;
        }
      }
      world.treasurePods = new Dictionary<string, TreasurePodV01>();
      foreach (KeyValuePair<string, TreasurePodModel> allPod in gameModel.AllPods())
      {
        TreasurePodV01 treasurePodV01 = new TreasurePodV01();
        allPod.Value.Pull(out treasurePodV01.state, out treasurePodV01.spawnQueue);
        world.treasurePods[allPod.Key] = treasurePodV01;
      }
      world.switches = new Dictionary<string, SwitchHandler.State>();
      foreach (KeyValuePair<string, MasterSwitchModel> allSwitch in gameModel.AllSwitches())
      {
        SwitchHandler.State state;
        allSwitch.Value.Pull(out state);
        world.switches[allSwitch.Key] = state;
      }
      world.puzzleSlotsFilled = new Dictionary<string, bool>();
      foreach (KeyValuePair<string, PuzzleSlotModel> allSlot in gameModel.AllSlots())
      {
        bool filled;
        allSlot.Value.Pull(out filled);
        world.puzzleSlotsFilled[allSlot.Key] = filled;
      }
      world.oasisStates = new Dictionary<string, bool>();
      foreach (KeyValuePair<string, OasisModel> allOasis in gameModel.AllOases())
      {
        bool isLive;
        allOasis.Value.Pull(out isLive);
        world.oasisStates[allOasis.Key] = isLive;
      }
      world.quicksilverEnergyGenerators = new Dictionary<string, QuicksilverEnergyGeneratorV02>();
      foreach (KeyValuePair<string, QuicksilverEnergyGeneratorModel> allGenerator in gameModel.AllGenerators())
      {
        QuicksilverEnergyGeneratorV02 energyGeneratorV02 = new QuicksilverEnergyGeneratorV02();
        allGenerator.Value.Pull(out energyGeneratorV02.state, out energyGeneratorV02.timer);
        world.quicksilverEnergyGenerators[allGenerator.Key] = energyGeneratorV02;
      }
      world.teleportNodeActivations = new Dictionary<string, bool>();
    }

    public ExchangeDirector.Offer FromOfferData(ExchangeOfferV04 offer)
    {
      if (!offer.hasOffer)
        return null;
      List<ExchangeDirector.ItemEntry> rewards = new List<ExchangeDirector.ItemEntry>();
      if (offer.rewards != null)
      {
        foreach (ItemEntryV03 reward in offer.rewards)
          rewards.Add(new ExchangeDirector.ItemEntry(reward.id, reward.count, reward.nonIdentReward));
      }
      List<ExchangeDirector.RequestedItemEntry> requests = new List<ExchangeDirector.RequestedItemEntry>();
      if (offer.requests != null)
      {
        foreach (RequestedItemEntryV03 request in offer.requests)
          requests.Add(new ExchangeDirector.RequestedItemEntry(request.id, request.count, request.progress, request.nonIdentReward));
      }
      return new ExchangeDirector.Offer(offer.offerId, offer.rancherId, offer.expireTime, offer.earlyExchangeTime, requests, rewards);
    }

    public ExchangeOfferV04 ToOfferData(ExchangeDirector.Offer o)
    {
      ExchangeOfferV04 offerData = new ExchangeOfferV04();
      offerData.rewards = new List<ItemEntryV03>();
      offerData.requests = new List<RequestedItemEntryV03>();
      if (o == null)
      {
        offerData.hasOffer = false;
        return offerData;
      }
      offerData.hasOffer = true;
      offerData.expireTime = o.expireTime;
      offerData.earlyExchangeTime = o.earlyExchangeTime;
      offerData.offerId = o.offerId;
      offerData.rancherId = o.rancherId;
      foreach (ExchangeDirector.ItemEntry reward in o.rewards)
        offerData.rewards.Add(new ItemEntryV03()
        {
          id = reward.id,
          count = reward.count,
          nonIdentReward = reward.specReward
        });
      foreach (ExchangeDirector.RequestedItemEntry request in o.requests)
        offerData.requests.Add(new RequestedItemEntryV03()
        {
          id = request.id,
          count = request.count,
          progress = request.progress,
          nonIdentReward = request.specReward
        });
      return offerData;
    }

    private Dictionary<PlayerState.AmmoMode, List<AmmoDataV02>> AmmoDataFromSlots(
      Dictionary<PlayerState.AmmoMode, Ammo.Slot[]> slots)
    {
      if (slots == null)
        return null;
      Dictionary<PlayerState.AmmoMode, List<AmmoDataV02>> dictionary = new Dictionary<PlayerState.AmmoMode, List<AmmoDataV02>>();
      foreach (KeyValuePair<PlayerState.AmmoMode, Ammo.Slot[]> slot in slots)
        dictionary[slot.Key] = AmmoDataFromSlots(slot.Value);
      return dictionary;
    }

    private List<AmmoDataV02> AmmoDataFromSlots(Ammo.Slot[] slots)
    {
      List<AmmoDataV02> ammoDataV02List = new List<AmmoDataV02>(slots.Length);
      for (int index = 0; index < slots.Length; ++index)
      {
        if (slots[index] != null)
          ammoDataV02List.Add(new AmmoDataV02()
          {
            id = slots[index].id,
            count = slots[index].count,
            emotionData = new SlimeEmotionDataV02()
            {
              emotionData = slots[index].emotions
            }
          });
        else
          ammoDataV02List.Add(new AmmoDataV02()
          {
            id = Identifiable.Id.NONE,
            count = 0,
            emotionData = new SlimeEmotionDataV02()
            {
              emotionData = new SlimeEmotionData()
            }
          });
      }
      return ammoDataV02List;
    }

    private void Pull(GameModel gameModel, PlacedGadgetV08 gadget, GadgetModel model)
    {
      gadget.gadgetId = model.ident;
      model.PullBase(out gadget.waitForChargeupTime, out gadget.yRotation);
      switch (model)
      {
        case ExtractorModel _:
          ((ExtractorModel) model).Pull(out gadget.extractorCyclesRemaining, out gadget.extractorQueuedToProduce, out gadget.extractorCycleEndTime, out gadget.extractorNextProduceTime);
          break;
        case WarpDepotModel _:
          Ammo.Slot[] slots;
          ((WarpDepotModel) model).Pull(out gadget.isPrimaryInLink, out slots);
          gadget.ammo = AmmoDataFromSlots(slots);
          break;
        case SnareModel _:
          ((SnareModel) model).Pull(out gadget.baitTypeId, out gadget.gordoTypeId, out gadget.gordoEatenCount, out gadget.fashions);
          break;
        case EchoNetModel _:
          ((EchoNetModel) model).Pull(out gadget.lastSpawnTime);
          break;
        case DroneModel _:
          gadget.drone = new DroneGadgetV01();
          Ammo.Slot[] ammoSlots;
          ((DroneModel) model).Pull(out gadget.drone.drone.position.value, out gadget.drone.drone.rotation.value, out ammoSlots, out gadget.drone.drone.fashions, out gadget.drone.drone.noClip, out gadget.drone.station.battery.time, out gadget.drone.programs);
          List<AmmoDataV02> ammoDataV02List = AmmoDataFromSlots(ammoSlots);
          gadget.drone.drone.ammo = ammoDataV02List.Count >= 1 ? ammoDataV02List[0] : null;
          break;
        default:
          BasicGadgetModel basicGadgetModel = model as BasicGadgetModel;
          break;
      }
    }

    private void Pull(GameModel gameModel, PlayerV14 player)
    {
      Dictionary<PlayerState.AmmoMode, Ammo.Slot[]> ammoSlots;
      Vector3 position;
      Vector3 rotation;
      gameModel.GetPlayerModel().Pull(out player.health, out player.energy, out player.radiation, out player.currency, out player.currencyEverCollected, out player.keys, out ammoSlots, out player.upgrades, out player.availUpgrades, out player.upgradeLocks, out player.unlockedZoneMaps, out player.regionSetId, out position, out rotation, out player.endGameTime);
      if (float.IsNaN(position.x) || float.IsNaN(position.y) || float.IsNaN(position.z))
      {
        Debug.Log("Player position was set to NaN on serialization! ZOMG!");
        position = savedGameInfoProvider.GetWakeUpDestination();
      }
      player.playerPos = new Vector3V02()
      {
        value = position
      };
      player.playerRotEuler = new Vector3V02()
      {
        value = rotation
      };
      player.ammo = AmmoDataFromSlots(ammoSlots);
      gameModel.Pull(out player.gameMode, out player.gameIconId);
      player.mail = new List<MailV02>();
      List<MailDirector.Mail> mail1;
      gameModel.GetMailModel().Pull(out mail1);
      foreach (MailDirector.Mail mail2 in mail1)
        player.mail.Add(new MailV02()
        {
          isRead = mail2.read,
          mailType = mail2.type,
          messageKey = mail2.key
        });
      gameModel.GetProgressModel().Pull(out player.progress, out player.delayedProgress);
      gameModel.GetGadgetsModel().Pull(out player.blueprints, out player.availBlueprints, out player.blueprintLocks, out player.gadgets, out player.craftMatCounts);
      gameModel.GetDecorizerModel().Pull(out player.decorizer);
      player.version = savedGameInfoProvider.GetVersion();
    }

    private void Pull(GameModel gameModel, List<ActorDataV09> actors, WorldV22 world)
    {
      foreach (KeyValuePair<long, ActorModel> allActor in gameModel.AllActors())
      {
        Identifiable.Id ident = allActor.Value.ident;
        if (!Identifiable.SCENE_OBJECTS.Contains(ident) && ident != Identifiable.Id.QUICKSILVER_SLIME)
          actors.Add(BuildActorData(gameModel, (int) ident, allActor.Value.actorId, allActor.Value, world));
      }
    }

    private ActorDataV09 BuildActorData(
      GameModel gameModel,
      int typeId,
      long actorId,
      ActorModel actorModel,
      WorldV22 world)
    {
      ActorDataV09 persistence = new ActorDataV09();
      persistence.typeId = typeId;
      persistence.actorId = actorId;
      persistence.pos = new Vector3V02()
      {
        value = actorModel.GetPos()
      };
      persistence.rot = new Vector3V02()
      {
        value = actorModel.GetRot().eulerAngles
      };
      persistence.regionSetId = actorModel.currRegionSetId;
      persistence.cycleData = new ResourceCycleDataV03();
      persistence.emotions = new SlimeEmotionDataV02();
      switch (actorModel)
      {
        case SlimeModel _:
          ((SlimeModel) actorModel).Pull(ref persistence);
          if (actorModel is GlitchSlimeModel)
          {
            world.glitch.slimes[persistence.actorId] = ((GlitchSlimeModel) actorModel).Pull();
            break;
          }
          break;
        case PlortModel _:
          ((PlortModel) actorModel).Pull(out persistence.destroyTime);
          break;
        case AnimalModel _:
          ((AnimalModel) actorModel).Pull(ref persistence);
          break;
        case ProduceModel _:
          ((ProduceModel) actorModel).Pull(out persistence.cycleData.state, out persistence.cycleData.progressTime);
          break;
        default:
          ScienceMatModel scienceMatModel = actorModel as ScienceMatModel;
          break;
      }
      return persistence;
    }

    private void Pull(GameModel gameModel, RanchV07 ranch)
    {
      ranch.accessDoorStates = new Dictionary<string, AccessDoor.State>();
      foreach (KeyValuePair<string, AccessDoorModel> allDoor in gameModel.AllDoors())
      {
        AccessDoor.State state;
        allDoor.Value.Pull(out state);
        ranch.accessDoorStates[allDoor.Key] = state;
      }
      ranch.plots = new List<LandPlotV08>();
      foreach (KeyValuePair<string, LandPlotModel> allLandPlot in gameModel.AllLandPlots())
      {
        LandPlotV08 landPlotV08 = new LandPlotV08();
        landPlotV08.id = allLandPlot.Key;
        Dictionary<SiloStorage.StorageType, Ammo.Slot[]> siloAmmo;
        allLandPlot.Value.Pull(out landPlotV08.feederNextTime, out landPlotV08.feederPendingCount, out landPlotV08.feederCycleSpeed, out landPlotV08.collectorNextTime, out landPlotV08.typeId, out landPlotV08.attachedId, out landPlotV08.upgrades, out siloAmmo, out landPlotV08.siloActivatorIndices, out landPlotV08.ashUnits);
        landPlotV08.siloAmmo = new Dictionary<SiloStorage.StorageType, List<AmmoDataV02>>();
        foreach (KeyValuePair<SiloStorage.StorageType, Ammo.Slot[]> keyValuePair in siloAmmo)
          landPlotV08.siloAmmo[keyValuePair.Key] = AmmoDataFromSlots(keyValuePair.Value);
        ranch.plots.Add(landPlotV08);
      }
      gameModel.GetRanchModel().Pull(out ranch.palettes, out ranch.ranchFastForward);
    }

    private void Pull(GameModel gameModel, PediaV03 pedia)
    {
      IEnumerable<PediaDirector.Id> unlocked;
      gameModel.GetPediaModel().Pull(out pedia.progressGivenForPediaCount, out unlocked);
      pedia.unlockedIds = new List<string>();
      foreach (PediaDirector.Id id in unlocked)
        pedia.unlockedIds.Add(Enum.GetName(typeof (PediaDirector.Id), id));
      pedia.completedTuts = new List<string>();
      List<TutorialDirector.Id> completedIds;
      List<TutorialDirector.Id> popupQueue;
      gameModel.GetTutorialsModel().Pull(out completedIds, out popupQueue);
      foreach (TutorialDirector.Id id in completedIds)
        pedia.completedTuts.Add(Enum.GetName(typeof (TutorialDirector.Id), id));
      foreach (TutorialDirector.Id id in popupQueue)
        pedia.popupQueue.Add(Enum.GetName(typeof (TutorialDirector.Id), id));
    }

    private void Pull(GameModel gameModel, GameAchieveV03 achieve) => gameModel.GetGameAchievesModel().Pull(out achieve.gameFloatStatDict, out achieve.gameDoubleStatDict, out achieve.gameIntStatDict, out achieve.gameIdDictStatDict);
  }
}
