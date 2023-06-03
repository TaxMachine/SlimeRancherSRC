// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GadgetsModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class GadgetsModel
  {
    public static IEnumerable<Gadget.Id> AVAILABLE_BLUEPRINTS = new List<Gadget.Id>()
    {
      Gadget.Id.EXTRACTOR_DRILL_NOVICE,
      Gadget.Id.EXTRACTOR_PUMP_NOVICE,
      Gadget.Id.EXTRACTOR_APIARY_NOVICE,
      Gadget.Id.MED_STATION,
      Gadget.Id.HYDRO_TURRET,
      Gadget.Id.TELEPORTER_PINK,
      Gadget.Id.WARP_DEPOT_PINK,
      Gadget.Id.LAMP_PINK,
      Gadget.Id.SLIME_HOOP,
      Gadget.Id.SLIME_STAGE,
      Gadget.Id.GORDO_SNARE_NOVICE,
      Gadget.Id.DRONE
    };
    public HashSet<Gadget.Id> blueprints = new HashSet<Gadget.Id>();
    public Dictionary<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLockData = new Dictionary<Gadget.Id, GadgetDirector.BlueprintLockData>(Gadget.idComparer);
    public HashSet<Gadget.Id> availBlueprints = new HashSet<Gadget.Id>();
    public HashSet<Gadget.Id> registeredBlueprints = new HashSet<Gadget.Id>();
    public Dictionary<Gadget.Id, int> gadgets = new Dictionary<Gadget.Id, int>();
    public Dictionary<Identifiable.Id, int> craftMatCounts = new Dictionary<Identifiable.Id, int>(Identifiable.idComparer);
    public Dictionary<Gadget.Id, int> placedGadgetCounts = new Dictionary<Gadget.Id, int>(Gadget.idComparer);
    private Participant participant;

    public void SetParticipant(Participant participant) => this.participant = participant;

    public void Init()
    {
      if (participant == null)
        return;
      participant.InitModel(this);
    }

    public void NotifyParticipants()
    {
      if (participant == null)
        return;
      participant.SetModel(this);
    }

    public void Reset()
    {
      blueprints.Clear();
      blueprintLockData.Clear();
      availBlueprints.Clear();
      gadgets.Clear();
      craftMatCounts.Clear();
      placedGadgetCounts.Clear();
      InitInitialBlueprints();
    }

    private void InitInitialBlueprints()
    {
      blueprints.Add(Gadget.Id.EXTRACTOR_DRILL_NOVICE);
      availBlueprints.UnionWith(AVAILABLE_BLUEPRINTS);
      blueprints.Remove(Gadget.Id.HYDRO_SHOWER);
      availBlueprints.Remove(Gadget.Id.HYDRO_SHOWER);
    }

    public bool IsTimedLock(Gadget.Id id) => blueprintLockData.ContainsKey(id) && blueprintLockData[id].timedLock;

    public double GetLockedUntil(Gadget.Id id) => blueprintLockData.ContainsKey(id) ? blueprintLockData[id].lockedUntil : double.PositiveInfinity;

    public void UnlockAt(Gadget.Id id, double lockedUntil) => blueprintLockData[id] = new GadgetDirector.BlueprintLockData(true, lockedUntil);

    public void RegisterBlueprint(Gadget.Id id)
    {
      Gadget.RegisterFashion(id);
      registeredBlueprints.Add(id);
    }

    public void Push(
      List<Gadget.Id> blueprints,
      List<Gadget.Id> availBlueprints,
      Dictionary<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLocks,
      Dictionary<Gadget.Id, int> gadgets,
      Dictionary<Identifiable.Id, int> craftMatCounts)
    {
      this.blueprints = new HashSet<Gadget.Id>(blueprints);
      this.availBlueprints = new HashSet<Gadget.Id>(availBlueprints);
      InitInitialBlueprints();
      this.gadgets = new Dictionary<Gadget.Id, int>(gadgets);
      this.craftMatCounts = new Dictionary<Identifiable.Id, int>(craftMatCounts, Identifiable.idComparer);
      foreach (KeyValuePair<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLock in blueprintLocks)
      {
        if (blueprintLockData.ContainsKey(blueprintLock.Key))
          blueprintLockData[blueprintLock.Key] = blueprintLock.Value;
        else
          Log.Debug("Skipping unknown blueprint lock key", "key", blueprintLock.Key);
      }
    }

    public void Pull(
      out List<Gadget.Id> blueprints,
      out List<Gadget.Id> availBlueprints,
      out Dictionary<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLocks,
      out Dictionary<Gadget.Id, int> gadgets,
      out Dictionary<Identifiable.Id, int> craftMatCounts)
    {
      blueprints = new List<Gadget.Id>(this.blueprints);
      availBlueprints = new List<Gadget.Id>(this.availBlueprints);
      gadgets = new Dictionary<Gadget.Id, int>(this.gadgets);
      craftMatCounts = new Dictionary<Identifiable.Id, int>(this.craftMatCounts, Identifiable.idComparer);
      blueprintLocks = new Dictionary<Gadget.Id, GadgetDirector.BlueprintLockData>(blueprintLockData);
    }

    public void OnNewGameLoaded(PlayerState.GameMode currGameMode)
    {
      if (currGameMode != PlayerState.GameMode.TIME_LIMIT_V2)
        return;
      gadgets = new Dictionary<Gadget.Id, int>()
      {
        {
          Gadget.Id.HYDRO_TURRET,
          3
        },
        {
          Gadget.Id.MARKET_LINK,
          1
        },
        {
          Gadget.Id.MED_STATION,
          2
        },
        {
          Gadget.Id.TELEPORTER_BLUE,
          2
        },
        {
          Gadget.Id.TELEPORTER_GREY,
          2
        },
        {
          Gadget.Id.TELEPORTER_PINK,
          2
        },
        {
          Gadget.Id.WARP_DEPOT_BLUE,
          2
        },
        {
          Gadget.Id.WARP_DEPOT_GREY,
          2
        },
        {
          Gadget.Id.WARP_DEPOT_PINK,
          2
        },
        {
          Gadget.Id.DRONE,
          10
        }
      };
    }

    public interface Participant
    {
      void InitModel(GadgetsModel model);

      void SetModel(GadgetsModel model);
    }
  }
}
