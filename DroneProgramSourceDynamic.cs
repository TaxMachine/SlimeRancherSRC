// Decompiled with JetBrains decompiler
// Type: DroneProgramSourceDynamic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DroneProgramSourceDynamic : DroneProgramSource<Identifiable>
{
  private float? pickupDelay;
  private int? maxPickup;
  private const int PICKUP_MASK = 34816;
  protected const float PICKUP_DURATION = 0.3f;

  public override void Selected()
  {
    base.Selected();
    pickupDelay = new float?();
    maxPickup = new int?();
    if (!(source != null))
      return;
    source.gameObject.GetComponent<RegionMember>().regionsChanged += OnRegionsChanged;
  }

  public override void Deselected()
  {
    base.Deselected();
    if (!(source != null))
      return;
    source.gameObject.GetComponent<RegionMember>().regionsChanged -= OnRegionsChanged;
  }

  protected override bool CanCancel()
  {
    if (source == null || !source.gameObject.activeInHierarchy)
      return true;
    if (!this.maxPickup.HasValue)
      return false;
    int? maxPickup = this.maxPickup;
    int num = 0;
    return maxPickup.GetValueOrDefault() <= num & maxPickup.HasValue;
  }

  protected override IEnumerable<Orientation> GetTargetOrientations(Identifiable source) => GetTargetOrientations_Gather(source.gameObject);

  protected override Vector3 GetTargetPosition(Identifiable source) => source.transform.position;

  protected override GameObject GetTargetGameObject(Identifiable source) => source.gameObject;

  protected override void OnReachedDestination()
  {
    base.OnReachedDestination();
    maxPickup = new int?(GetMaxPickup(source.id));
  }

  protected override bool OnAction()
  {
    if (!pickupDelay.HasValue)
      pickupDelay = new float?(Time.fixedTime + (SphereCastPickup(source, maxPickup.Value, GetPickupRadius(), SourcePredicate) ? 0.3f : 0.0f));
    return pickupDelay.Value <= (double) Time.fixedTime;
  }

  protected override IEnumerable<Identifiable> GetSources(Predicate<Identifiable.Id> predicate) => drone.network.gameObject.GetComponent<CellDirector>().identifiableIndex.GetAllRegistered().Select(e => e.GameObject.GetComponent<Identifiable>()).Where(s => predicate(s.id) && SourcePredicate(s)).OrderBy(s => (s.transform.position - drone.transform.position).sqrMagnitude);

  public override IEnumerable<DroneFastForwarder.GatherGroup> GetFastForwardGroups(double endTime) => GetSources(predicate).GroupBy(source => source.id).Select(group => new DroneFastForwarder.GatherGroup.Dynamic(group)).Where(group => group.Any()).Cast<DroneFastForwarder.GatherGroup>();

  protected bool SourcePredicate(Identifiable source) => SourcePredicate(drone.network.GetContaining(source), source);

  protected virtual bool SourcePredicate(
    DroneNetwork.LandPlotMetadata metadata,
    Identifiable source)
  {
    return source != null && source.transform.parent == null && source.gameObject.activeInHierarchy && DroneNetwork.IsResourceReady(source.gameObject);
  }

  protected virtual float GetPickupRadius() => 1f;

  protected virtual int GetMaxPickup(Identifiable.Id id) => Mathf.Min(drone.ammo.GetSlotMaxCount(), GetAvailableDestinationSpace(id)) - drone.ammo.GetSlotCount();

  private void OnRegionsChanged(List<Region> left, List<Region> joined)
  {
    if (!(source != null))
      return;
    source.gameObject.GetComponent<RegionMember>().regionsChanged -= OnRegionsChanged;
    source = null;
  }

  protected bool SphereCastPickup(
    Identifiable source,
    int maxPickup,
    float radius,
    Predicate<Identifiable> predicate)
  {
    if (maxPickup < 1 || !predicate(source) || !drone.ammo.CouldAddToSlot(source.id))
      return false;
    SphereCastPickupTween(source);
    int num = maxPickup - 1;
    if (num <= 0)
      return true;
    HashSet<GameObject> gameObjectSet = new HashSet<GameObject>()
    {
      source.gameObject
    };
    Vector3 direction = source.transform.position - drone.transform.position;
    foreach (RaycastHit raycastHit in Physics.SphereCastAll(drone.transform.position, radius, direction, direction.magnitude, 34816, QueryTriggerInteraction.Ignore))
    {
      Identifiable component = raycastHit.collider.gameObject.GetComponent<Identifiable>();
      if (component != null && component.id == source.id && gameObjectSet.Add(component.gameObject) && predicate(component))
      {
        SphereCastPickupTween(component);
        --num;
        if (num <= 0)
          break;
      }
    }
    return true;
  }

  protected void SphereCastPickupTween(Identifiable source)
  {
    List<Collider> disabledColliders = new List<Collider>();
    foreach (Collider component in source.gameObject.GetComponents<Collider>())
    {
      if (component.enabled)
      {
        disabledColliders.Add(component);
        component.enabled = false;
      }
    }
    float endValue = source.transform.localScale.x * 0.2f;
    float x = source.transform.localScale.x;
    DOTween.Sequence().Append(source.transform.DOScale(endValue, 0.3f).SetEase(Ease.Linear)).Append(source.transform.DOScale(x, 0.3f).SetEase(Ease.Linear));
    source.transform.DOMove(drone.transform.position, 0.375f).SetEase(Ease.Linear);
    source.transform.DORotate(Quaternion.LookRotation(drone.transform.position - source.transform.position).eulerAngles, 0.450000018f).SetEase(Ease.Linear);
    SRSingleton<SceneContext>.Instance.StartCoroutine(SphereCastPickupCoroutine(source, disabledColliders));
  }

  protected IEnumerator SphereCastPickupCoroutine(
    Identifiable identifiable,
    List<Collider> disabledColliders)
  {
    DroneProgramSourceDynamic programSourceDynamic = this;
    yield return new WaitForSeconds(0.3f);
    if (identifiable != null)
    {
      if (programSourceDynamic.drone.ammo.MaybeAddToSlot(identifiable.id))
      {
        Destroyer.DestroyActor(identifiable.gameObject, "DroneSubbehaviour.SphereCastPickupCoroutine");
      }
      else
      {
        foreach (Collider disabledCollider in disabledColliders)
          disabledCollider.enabled = true;
      }
    }
  }
}
