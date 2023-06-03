// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.LandPlotModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class LandPlotModel
  {
    public double nextFeedingTime;
    public int remainingFeedOperations;
    public SlimeFeeder.FeedSpeed feederCycleSpeed;
    public double collectorNextTime;
    public double attachedDeathTime;
    public LandPlot.Id typeId;
    public SpawnResource.Id attachedId;
    public Identifiable.Id attachedResourceId;
    public HashSet<LandPlot.Upgrade> upgrades = new HashSet<LandPlot.Upgrade>(LandPlot.upgradeComparer);
    public Dictionary<SiloStorage.StorageType, AmmoModel> siloAmmo = new Dictionary<SiloStorage.StorageType, AmmoModel>();
    public int[] siloStorageIndices = new int[4];
    public float ashUnits;
    private GameObject gameObj;

    public void SetGameObject(GameObject gameObj) => this.gameObj = gameObj;

    public void Init()
    {
      foreach (Participant componentsInChild in gameObj.GetComponentsInChildren<Participant>(true))
        componentsInChild.InitModel(this);
    }

    public void NotifyParticipants()
    {
      foreach (Participant componentsInChild in gameObj.GetComponentsInChildren<Participant>(true))
        componentsInChild.SetModel(this);
    }

    public void InstantiatePlot(GameObject prefab, bool expectingPush)
    {
      LandPlot componentInChildren = gameObj.GetComponentInChildren<LandPlot>();
      if (componentInChildren != null)
      {
        componentInChildren.gameObject.transform.SetParent(SRSingleton<DynamicObjectContainer>.Instance.transform);
        Destroyer.Destroy(componentInChildren.gameObject, "LandPlotModel.InstantiatePlot");
      }
      Object.Instantiate(prefab, gameObj.transform);
      Clear();
      if (expectingPush)
        return;
      Init();
      NotifyParticipants();
    }

    private void Clear()
    {
      nextFeedingTime = 0.0;
      remainingFeedOperations = 0;
      feederCycleSpeed = SlimeFeeder.FeedSpeed.Normal;
      collectorNextTime = 0.0;
      attachedDeathTime = 0.0;
      typeId = LandPlot.Id.NONE;
      attachedId = SpawnResource.Id.NONE;
      attachedResourceId = Identifiable.Id.NONE;
      upgrades.Clear();
      siloAmmo.Clear();
    }

    public bool HasUpgrade(LandPlot.Upgrade upgrade) => upgrades.Contains(upgrade);

    public static bool IncludesFullyUpgradedCorralCoopAndSilo(ICollection<LandPlotModel> allPlots)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      foreach (LandPlotModel allPlot in allPlots)
      {
        if (allPlot.typeId == LandPlot.Id.COOP)
          flag1 = ((flag1 ? 1 : 0) | (!allPlot.HasUpgrade(LandPlot.Upgrade.WALLS) || !allPlot.HasUpgrade(LandPlot.Upgrade.VITAMIZER) ? 0 : (allPlot.HasUpgrade(LandPlot.Upgrade.FEEDER) ? 1 : 0))) != 0;
        else if (allPlot.typeId == LandPlot.Id.CORRAL)
          flag2 = ((flag2 ? 1 : 0) | (!allPlot.HasUpgrade(LandPlot.Upgrade.WALLS) || !allPlot.HasUpgrade(LandPlot.Upgrade.AIR_NET) || !allPlot.HasUpgrade(LandPlot.Upgrade.SOLAR_SHIELD) || !allPlot.HasUpgrade(LandPlot.Upgrade.FEEDER) || !allPlot.HasUpgrade(LandPlot.Upgrade.PLORT_COLLECTOR) ? 0 : (allPlot.HasUpgrade(LandPlot.Upgrade.MUSIC_BOX) ? 1 : 0))) != 0;
        else if (allPlot.typeId == LandPlot.Id.SILO)
          flag3 = ((flag3 ? 1 : 0) | (!allPlot.HasUpgrade(LandPlot.Upgrade.STORAGE2) || !allPlot.HasUpgrade(LandPlot.Upgrade.STORAGE3) ? 0 : (allPlot.HasUpgrade(LandPlot.Upgrade.STORAGE4) ? 1 : 0))) != 0;
      }
      return flag1 & flag2 & flag3;
    }

    public static HashSet<Identifiable.Id> GetRanchResourceTypes(
      ICollection<LandPlotModel> allPlots,
      HashSet<Identifiable.Id> resourceClass)
    {
      HashSet<Identifiable.Id> ranchResourceTypes = new HashSet<Identifiable.Id>();
      foreach (LandPlotModel allPlot in allPlots)
      {
        if (resourceClass.Contains(allPlot.attachedResourceId))
          ranchResourceTypes.Add(allPlot.attachedResourceId);
      }
      return ranchResourceTypes;
    }

    public bool HasAttached() => attachedId != 0;

    public void Push(
      double feederNextTime,
      int feederPendingCount,
      SlimeFeeder.FeedSpeed feederCycleSpeed,
      double collectorNextTime,
      LandPlot.Id typeId,
      SpawnResource.Id attachedId,
      List<LandPlot.Upgrade> upgrades,
      Dictionary<SiloStorage.StorageType, Ammo.Slot[]> siloAmmo,
      List<int> siloStorageIndices,
      float ashUnits)
    {
      nextFeedingTime = feederNextTime;
      remainingFeedOperations = feederPendingCount;
      this.feederCycleSpeed = feederCycleSpeed;
      this.collectorNextTime = collectorNextTime;
      this.typeId = typeId;
      this.attachedId = attachedId;
      this.upgrades = upgrades.ToHashSet(LandPlot.upgradeComparer);
      foreach (KeyValuePair<SiloStorage.StorageType, Ammo.Slot[]> keyValuePair in siloAmmo)
        this.siloAmmo[keyValuePair.Key].Push(keyValuePair.Value);
      this.siloStorageIndices = siloStorageIndices.ToArray();
      this.ashUnits = ashUnits;
    }

    public void Pull(
      out double feederNextTime,
      out int feederPendingCount,
      out SlimeFeeder.FeedSpeed feederCycleSpeed,
      out double collectorNextTime,
      out LandPlot.Id typeId,
      out SpawnResource.Id attachedId,
      out List<LandPlot.Upgrade> upgrades,
      out Dictionary<SiloStorage.StorageType, Ammo.Slot[]> siloAmmo,
      out List<int> siloStorageIndices,
      out float ashUnits)
    {
      feederNextTime = nextFeedingTime;
      feederPendingCount = remainingFeedOperations;
      feederCycleSpeed = this.feederCycleSpeed;
      collectorNextTime = this.collectorNextTime;
      typeId = this.typeId;
      attachedId = this.attachedId;
      upgrades = this.upgrades.ToList();
      siloAmmo = new Dictionary<SiloStorage.StorageType, Ammo.Slot[]>();
      foreach (KeyValuePair<SiloStorage.StorageType, AmmoModel> keyValuePair in this.siloAmmo)
      {
        Ammo.Slot[] slots;
        keyValuePair.Value.Pull(out slots);
        siloAmmo[keyValuePair.Key] = slots;
      }
      siloStorageIndices = new List<int>(this.siloStorageIndices);
      ashUnits = this.ashUnits;
    }

    public interface Participant
    {
      void InitModel(LandPlotModel model);

      void SetModel(LandPlotModel model);
    }
  }
}
