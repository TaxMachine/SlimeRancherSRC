// Decompiled with JetBrains decompiler
// Type: DestroyOnTouching
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouching : SRBehaviour
{
  [Tooltip("How long, in hours, we can contact one or more non-water objects before poofing.")]
  public float hoursOfContactAllowed;
  [Tooltip("When we poof, how large an area is watered.")]
  public float wateringRadius;
  [Tooltip("Amount to water each thing in radius when we poof.")]
  public float wateringUnits = 3f;
  [Tooltip("The effect to play when we poof.")]
  public GameObject destroyFX;
  [Tooltip("Should we destroy only if touching a non-water object?")]
  public bool touchingWaterOkay = true;
  [Tooltip("Should we destroy only if touching a non-ash object? Note: Does not include toys.")]
  public bool touchingAshOkay;
  [Tooltip("Should we destroy if touching an actor even when in the water. Note: Does not include toys.")]
  public bool reactToActors;
  [Tooltip("The type of liquid we should spread on destruction.")]
  public Identifiable.Id liquidType = Identifiable.Id.WATER_LIQUID;
  private double destroyAt = double.PositiveInfinity;
  private HashSet<GameObject> destructiveContacts = new HashSet<GameObject>();
  private HashSet<LiquidSource> waterSources = new HashSet<LiquidSource>();
  private HashSet<AshSafetyZone> ashSources = new HashSet<AshSafetyZone>();
  private TimeDirector timeDir;
  private bool destroying;
  private SlimeSubbehaviourPlexer plexer;
  private bool hasPlexer;
  private SlimeAppearanceApplicator slimeAppearanceApplicator;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    plexer = gameObject.GetComponent<SlimeSubbehaviourPlexer>();
    hasPlexer = plexer != null;
    slimeAppearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    if (!(slimeAppearanceApplicator != null))
      return;
    slimeAppearanceApplicator.OnAppearanceChanged += UpdateDestroyFX;
    if (!(slimeAppearanceApplicator.Appearance != null))
      return;
    UpdateDestroyFX(slimeAppearanceApplicator.Appearance);
  }

  public void FixedUpdate()
  {
    if (!timeDir.HasReached(destroyAt))
      return;
    DestroyAndWater();
  }

  public void NoteDestroying() => destroying = true;

  private void DestroyAndWater()
  {
    if (destroying)
      return;
    destroying = true;
    if (destroyFX != null)
      SpawnAndPlayFX(destroyFX, transform.position, transform.rotation);
    if (wateringRadius > 0.0)
      SphereOverlapTrigger.CreateGameObject(transform.position, wateringRadius, colliders =>
      {
        HashSet<LiquidConsumer> liquidConsumerSet = new HashSet<LiquidConsumer>();
        foreach (Collider collider in colliders)
        {
          foreach (LiquidConsumer liquidConsumer in !collider.isTrigger ? collider.gameObject.GetComponentsInParent<LiquidConsumer>() : collider.gameObject.GetComponents<LiquidConsumer>())
            liquidConsumerSet.Add(liquidConsumer);
        }
        foreach (LiquidConsumer liquidConsumer in liquidConsumerSet)
          liquidConsumer.AddLiquid(liquidType, wateringUnits);
      }, 4);
    Destroyer.DestroyActor(gameObject, "DestroyOnTouching.DestroyAndWater");
  }

  public void OnCollisionEnter(Collision col)
  {
    if (!IsDestructiveContact(col) || !destructiveContacts.Add(col.gameObject))
      return;
    UpdateDestroyTime();
  }

  public void OnCollisionExit(Collision col)
  {
    if (!IsDestructiveContact(col) || !destructiveContacts.Remove(col.gameObject))
      return;
    UpdateDestroyTime();
  }

  public void OnTriggerEnter(Collider col)
  {
    LiquidSource component1 = col.gameObject.GetComponent<LiquidSource>();
    if (component1 != null && Identifiable.IsWater(component1.liquidId) && waterSources.Add(component1))
      UpdateDestroyTime();
    AshSafetyZone component2 = col.gameObject.GetComponent<AshSafetyZone>();
    if (!(component2 != null) || !ashSources.Add(component2))
      return;
    UpdateDestroyTime();
  }

  public void OnTriggerExit(Collider col)
  {
    LiquidSource component1 = col.gameObject.GetComponent<LiquidSource>();
    if (component1 != null && Identifiable.IsWater(component1.liquidId) && waterSources.Remove(component1))
      UpdateDestroyTime();
    AshSafetyZone component2 = col.gameObject.GetComponent<AshSafetyZone>();
    if (!(component2 != null) || !ashSources.Remove(component2))
      return;
    UpdateDestroyTime();
  }

  private void UpdateDestroyFX(SlimeAppearance appearance)
  {
    if (!(appearance.DeathAppearance != null))
      return;
    destroyFX = appearance.DeathAppearance.deathFX;
  }

  private void UpdateDestroyTime()
  {
    destructiveContacts.RemoveWhere(c => c == null);
    waterSources.RemoveWhere(w => w == null || w.gameObject == null);
    ashSources.RemoveWhere(a => a == null || a.gameObject == null);
    int num1 = (hasPlexer ? (!plexer.IsGrounded() ? 1 : (destructiveContacts.Count == 0 ? 1 : 0)) : (destructiveContacts.Count == 0 ? 1 : 0)) == 0 ? 0 : (touchingWaterOkay ? 1 : (waterSources.Count <= 0 ? 1 : 0));
    bool flag1 = touchingAshOkay && ashSources.Count > 0;
    bool flag2 = touchingWaterOkay && waterSources.Count > 0;
    int num2 = flag1 ? 1 : 0;
    bool flag3 = (num1 | num2 | (flag2 ? 1 : 0)) == 0;
    if (double.IsPositiveInfinity(destroyAt) & flag3)
    {
      if (hoursOfContactAllowed <= 0.0)
        StartCoroutine(DestroyAndWaterAtEndOfFrame());
      else
        destroyAt = timeDir.HoursFromNowOrStart(hoursOfContactAllowed);
    }
    else
    {
      if (double.IsPositiveInfinity(destroyAt) || flag3)
        return;
      destroyAt = double.PositiveInfinity;
    }
  }

  private IEnumerator DestroyAndWaterAtEndOfFrame()
  {
    yield return new WaitForEndOfFrame();
    DestroyAndWater();
  }

  public float PctTimeToDestruct() => Mathf.Clamp01((float) ((destroyAt - timeDir.WorldTime()) / (3600.0 * hoursOfContactAllowed)));

  private bool IsDestructiveContact(Collision col) => (!touchingWaterOkay || IsNonWater(col)) && (!touchingAshOkay || IsNonAsh(col));

  private static bool IsNonWater(Collision col)
  {
    LiquidSource component1 = col.gameObject.GetComponent<LiquidSource>();
    Identifiable component2 = col.gameObject.GetComponent<Identifiable>();
    int num = !(component1 != null) ? 0 : (Identifiable.IsWater(component1.liquidId) ? 1 : 0);
    bool flag = component2 != null && (component2.id == Identifiable.Id.PUDDLE_PLORT || component2.id == Identifiable.Id.PUDDLE_SLIME || Identifiable.IsWater(component2.id));
    return num == 0 && !flag;
  }

  private static bool IsNonAsh(Collision col)
  {
    AshSource component1 = col.gameObject.GetComponent<AshSource>();
    Identifiable component2 = col.gameObject.GetComponent<Identifiable>();
    int num = component1 != null ? 1 : 0;
    bool flag = component2 != null && (component2.id == Identifiable.Id.FIRE_PLORT || component2.id == Identifiable.Id.FIRE_SLIME);
    return num == 0 && !flag;
  }
}
