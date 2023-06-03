// Decompiled with JetBrains decompiler
// Type: LandPlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using DG.Tweening;
using MonomiPark.SlimeRancher.DataModel;
using System.Collections.Generic;
using UnityEngine;

public class LandPlot : SRBehaviour, LandPlotModel.Participant
{
  public static IdComparer idComparer = new IdComparer();
  public static UpgradeComparer upgradeComparer = new UpgradeComparer();
  public Id typeId;
  public const float attachScaleUpTime = 5f;
  private LandPlotModel model;
  private GameObject attached;
  private DroneNetwork droneNetwork;
  private AchievementsDirector achievementsDirector;

  public void InitModel(LandPlotModel model) => model.typeId = typeId;

  public void SetModel(LandPlotModel model)
  {
    this.model = model;
    ApplyUpgrades(model.upgrades);
    if (model.attachedId == SpawnResource.Id.NONE)
      return;
    Attach(Instantiate(SRSingleton<GameContext>.Instance.LookupDirector.GetResourcePrefab(model.attachedId), transform.position, transform.rotation), true, false);
  }

  public void Awake() => achievementsDirector = SRSingleton<SceneContext>.Instance.AchievementsDirector;

  public void Start()
  {
    droneNetwork = GetComponentInParent<DroneNetwork>();
    droneNetwork.Register(this);
  }

  public void OnDestroy()
  {
    if (!(droneNetwork != null))
      return;
    droneNetwork.Deregister(this);
    droneNetwork = null;
  }

  public void Attach(
    GameObject toAttach,
    bool immediate,
    bool isReplacement,
    SECTR_AudioCue scaleUpCue = null)
  {
    toAttach.transform.SetParent(transform, true);
    attached = toAttach;
    SpawnResource component = attached.GetComponent<SpawnResource>();
    model.attachedId = component == null ? SpawnResource.Id.NONE : component.id;
    model.attachedResourceId = component.GetPrimarySpawnId();
    if (!immediate)
    {
      SECTR_AudioSystem.Play(scaleUpCue, toAttach.transform, Vector3.zero, false);
      TweenScaleUpItem(toAttach, isReplacement);
    }
    if (typeId != Id.GARDEN)
      return;
    Identifiable.Id attachedCropId = GetAttachedCropId();
    if (Identifiable.IsFruit(attachedCropId))
    {
      achievementsDirector.CheckAchievement(AchievementsDirector.Achievement.FRUIT_TREE_TYPES);
    }
    else
    {
      if (!Identifiable.IsVeggie(attachedCropId))
        return;
      achievementsDirector.CheckAchievement(AchievementsDirector.Achievement.VEGGIE_PATCH_TYPES);
    }
  }

  private void TweenScaleUpItem(GameObject toAttach, bool isReplacement)
  {
    SpawnResource[] spawners = toAttach.GetComponentsInChildren<SpawnResource>(true);
    foreach (ScaleMarker componentsInChild in toAttach.GetComponentsInChildren<ScaleMarker>())
    {
      if (!isReplacement || !componentsInChild.doNotScaleAsReplacement)
      {
        foreach (SpawnResource spawnResource in spawners)
          spawnResource.RegisterSpawnBlocker();
        TweenUtil.ScaleIn(componentsInChild.gameObject, 5f).OnComplete(() => TweenScaleUpItem_OnTweenComplete(spawners));
      }
    }
  }

  private void TweenScaleUpItem_OnTweenComplete(SpawnResource[] spawners)
  {
    foreach (SpawnResource spawner in spawners)
      spawner.DeregisterSpawnBlocker();
  }

  public bool HasAttached() => attached != null;

  public void DestroyAttached()
  {
    Destroyer.Destroy(attached, "LandPlot.DestroyAttached");
    attached = null;
    model.attachedId = SpawnResource.Id.NONE;
  }

  public void AddUpgrade(Upgrade upgrade)
  {
    model.upgrades.Add(upgrade);
    achievementsDirector.CheckAchievement(AchievementsDirector.Achievement.RANCH_UPGRADED_STORAGE);
    achievementsDirector.AddToStat(AchievementsDirector.GameIntStat.UPGRADES_PURCHASED, 1);
    ApplyUpgrades(upgrade.ToEnumerable());
  }

  public bool HasUpgrade(Upgrade upgrade) => model.HasUpgrade(upgrade);

  private void ApplyUpgrades(IEnumerable<Upgrade> upgrades)
  {
    foreach (PlotUpgrader component in GetComponents<PlotUpgrader>())
    {
      foreach (Upgrade upgrade in upgrades)
        component.Apply(upgrade);
    }
    if (!(droneNetwork != null))
      return;
    droneNetwork.OnUpgradesChanged(this);
  }

  public Identifiable.Id GetAttachedCropId() => attached != null ? attached.GetComponent<SpawnResource>().GetPrimarySpawnId() : Identifiable.Id.NONE;

  public double GetAttachedDeathTime()
  {
    if (attached != null)
    {
      DestroyAfterTime component = attached.GetComponent<DestroyAfterTime>();
      if (component != null)
        return component.GetDeathTime();
    }
    return 0.0;
  }

  public enum Id
  {
    NONE,
    EMPTY,
    CORRAL,
    COOP,
    GARDEN,
    SILO,
    POND,
    INCINERATOR,
  }

  public class IdComparer : IEqualityComparer<Id>
  {
    public bool Equals(Id id1, Id id2) => id1 == id2;

    public int GetHashCode(Id obj) => (int) obj;
  }

  public enum Upgrade
  {
    NONE,
    WALLS,
    MUSIC_BOX,
    STORAGE2,
    STORAGE3,
    STORAGE4,
    SOIL,
    SPRINKLER,
    SCARESLIME,
    FEEDER,
    VITAMIZER,
    AIR_NET,
    PLORT_COLLECTOR,
    SOLAR_SHIELD,
    ASH_TROUGH,
    MIRACLE_MIX,
    DELUXE_GARDEN,
    DELUXE_COOP,
  }

  public class UpgradeComparer : IEqualityComparer<Upgrade>
  {
    public bool Equals(Upgrade a, Upgrade b) => a == b;

    public int GetHashCode(Upgrade obj) => (int) obj;
  }
}
