// Decompiled with JetBrains decompiler
// Type: GlitchRegionHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlitchRegionHelper : 
  SRSingleton<GlitchRegionHelper>,
  AmbianceDirector.TimeOfDay,
  PlayerModel.Participant
{
  [Tooltip("Renderer to update the material on death in SLIMULATIONS region.")]
  public Renderer seaRenderer;
  private GameObject exitHudInstance;

  public GlitchImpostoDirector[] impostoDirectors { get; private set; }

  public CellDirector[] cellDirectors { get; private set; }

  public GlitchLiquidSource[] stations { get; private set; }

  public GlitchTarrNode[] nodes { get; private set; }

  public GlitchBreadcrumbNetwork breadcrumbs { get; private set; }

  public Dictionary<string, GlitchTeleportDestination> destinationsDict { get; private set; }

  public IEnumerable<GlitchTeleportDestination> destinations => destinationsDict.Values;

  public override void Awake()
  {
    base.Awake();
    SRSingleton<SceneContext>.Instance.GameModel.RegisterPlayerParticipant(this);
    SRSingleton<SceneContext>.Instance.RegionRegistry.ManageWithRegionSet(gameObject, RegionRegistry.RegionSetId.SLIMULATIONS);
    ZoneDirector componentInParent = GetComponentInParent<ZoneDirector>();
    impostoDirectors = componentInParent.GetComponentsInChildren<GlitchImpostoDirector>(true);
    cellDirectors = componentInParent.GetComponentsInChildren<CellDirector>(true);
    stations = componentInParent.GetComponentsInChildren<GlitchLiquidSource>(true);
    nodes = componentInParent.GetComponentsInChildren<GlitchTarrNode>(true);
    breadcrumbs = componentInParent.GetRequiredComponentInChildren<GlitchBreadcrumbNetwork>(true);
    destinationsDict = componentInParent.GetComponentsInChildren<GlitchTeleportDestination>(true).ToDictionary(d => d.id, d => d);
    breadcrumbs.OnGlitchRegionLoaded();
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.RegionRegistry != null))
      return;
    SRSingleton<SceneContext>.Instance.RegionRegistry.ReleaseFromRegionSet(gameObject, RegionRegistry.RegionSetId.SLIMULATIONS);
  }

  public void OnEnable() => SRSingleton<SceneContext>.Instance.AmbianceDirector.Register(this);

  public void OnDisable()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.AmbianceDirector != null))
      return;
    SRSingleton<SceneContext>.Instance.AmbianceDirector.Deregister(this);
  }

  public void RegionSetChanged(
    RegionRegistry.RegionSetId previous,
    RegionRegistry.RegionSetId current)
  {
    if (current == RegionRegistry.RegionSetId.SLIMULATIONS)
    {
      TimeDirector timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
      GlitchMetadata glitch = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
      PlayerState playerState = SRSingleton<SceneContext>.Instance.PlayerState;
      SRSingleton<DynamicObjectContainer>.Instance.DestroyChildren(go =>
      {
        RegionMember component = go.GetComponent<RegionMember>();
        return component != null && component.IsInRegion(RegionRegistry.RegionSetId.SLIMULATIONS);
      }, "GlitchRegionHelper.RegionSetChanged");
      foreach (GlitchImpostoDirector impostoDirector in impostoDirectors)
        impostoDirector.ResetImpostos();
      foreach (CellDirector cellDirector in cellDirectors)
        cellDirector.ForceCheckSpawn();
      foreach (GlitchLiquidSource station in stations)
        station.ResetLiquidState();
      List<GlitchTarrNode.Group> list = Enum.GetValues(typeof (GlitchTarrNode.Group)).Cast<GlitchTarrNode.Group>().ToList();
      list.Sort(new GlitchTarrNodeGroupComparer().OrderBy(it => Randoms.SHARED.GetInt()));
      foreach (GlitchTarrNode node in nodes)
        node.ResetNode(timeDirector.WorldTime() + (glitch.tarrNodeActivationDelay + (list.IndexOf(node.activationGroup) + list.Count * node.activationIndex) * (double) glitch.tarrNodeActivationDelayPerNode) * 3600.0);
      foreach (GlitchTeleportDestination destination in destinations)
        destination.Reset(new double?());
      Randoms.SHARED.Pick(destinations.Where(e => e.isPotentialExitDestination), null).Reset(new double?(timeDirector.HoursFromNow(glitch.teleportActivationDelay.GetRandom())));
      playerState.Ammo.Replace(Identifiable.Id.GLITCH_BUG_REPORT, Identifiable.Id.GLITCH_SLIME);
    }
    if (previous != RegionRegistry.RegionSetId.SLIMULATIONS)
      return;
    PlayerState player = SRSingleton<SceneContext>.Instance.PlayerState;
    foreach (GlitchTeleportDestination destination in destinations)
      destination.Reset(new double?(0.0));
    SRSingleton<DynamicObjectContainer>.Instance.DestroyChildren(go =>
    {
      RegionMember component = go.GetComponent<RegionMember>();
      return component != null && component.IsInRegion(RegionRegistry.RegionSetId.SLIMULATIONS);
    }, "GlitchRegionHelper.RegionSetChanged");
    Destroyer.Destroy(exitHudInstance, "GlitchRegionHelper.RegionSetChanged");
    player.Ammo.Replace(Identifiable.Id.GLITCH_SLIME, Identifiable.Id.GLITCH_BUG_REPORT);
    player.Ammo.Clear(ii => player.Ammo.GetSlotName(ii) != Identifiable.Id.GLITCH_BUG_REPORT);
  }

  public void OnExitTeleporterBecameActive()
  {
    GlitchMetadata glitch = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    Destroyer.Destroy(exitHudInstance, "GlitchRegionHelper.OnExitTeleporterBecameActive");
    exitHudInstance = Instantiate(glitch.teleportHudPrefab, SRSingleton<HudUI>.Instance.uiContainer.transform);
    exitHudInstance.StartCoroutine(OnExitTeleporterBecameActive_Coroutine(exitHudInstance));
  }

  private static IEnumerator OnExitTeleporterBecameActive_Coroutine(GameObject instance)
  {
    yield return new WaitForSeconds(SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.teleportHudLifetime);
    instance.GetRequiredComponent<Animator>().SetBool("state_active", false);
  }

  public float GetCurrentDayFraction_Position() => SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.ambianceTimeOfDay;

  public float GetCurrentDayFraction_Color() => SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.ambianceTimeOfDay;

  public void InitModel(PlayerModel model)
  {
  }

  public void SetModel(PlayerModel model)
  {
  }

  public void TransformChanged(Vector3 position, Quaternion rotation)
  {
  }

  public void RegisteredPotentialAmmoChanged(
    Dictionary<PlayerState.AmmoMode, List<GameObject>> ammo)
  {
  }

  public void KeyAdded()
  {
  }

  private class GlitchTarrNodeGroupComparer : SRComparer<GlitchTarrNode.Group>
  {
  }
}
