// Decompiled with JetBrains decompiler
// Type: FireSlimeIgnition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class FireSlimeIgnition : 
  RegisteredActorBehaviour,
  Ignitable,
  LiquidConsumer,
  RegistryUpdateable,
  ControllerCollisionListener
{
  private bool isIgnited;
  private GameObject fireFXObj;
  private double reigniteAtTime = double.PositiveInfinity;
  private TimeDirector timeDir;
  private List<LiquidSource> waterSources = new List<LiquidSource>();
  private const float EXTINGUISH_HRS = 0.5f;

  public void Awake() => timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;

  public override void Start()
  {
    base.Start();
    ExtractFire();
    GetComponent<SlimeAppearanceApplicator>().OnAppearanceChanged += appearance => ExtractFire();
    Ignite(gameObject);
  }

  private void ExtractFire()
  {
    FireIndicatorMarker componentInChildren = GetComponentInChildren<FireIndicatorMarker>();
    if (!(componentInChildren != null))
      return;
    fireFXObj = componentInChildren.gameObject;
    fireFXObj.SetActive(isIgnited);
  }

  public void OnCollisionEnter(Collision col)
  {
    if (!isIgnited)
      return;
    col.gameObject.GetComponent<Ignitable>()?.Ignite(gameObject);
  }

  public void OnControllerCollision(GameObject gameObj)
  {
    if (!isIgnited)
      return;
    gameObj.GetComponent<Ignitable>()?.Ignite(gameObject);
  }

  public void OnTriggerEnter(Collider col)
  {
    LiquidSource component = col.gameObject.GetComponent<LiquidSource>();
    if (!(component != null) || !Identifiable.IsWater(component.liquidId))
      return;
    waterSources.Add(component);
    Extinguish();
  }

  public void OnTriggerExit(Collider col)
  {
    LiquidSource component = col.gameObject.GetComponent<LiquidSource>();
    if (!(component != null) || !Identifiable.IsWater(component.liquidId))
      return;
    waterSources.Remove(component);
  }

  public void RegistryUpdate()
  {
    if (isIgnited || !timeDir.HasReached(reigniteAtTime))
      return;
    Ignite(gameObject);
  }

  public void Ignite(GameObject igniter)
  {
    waterSources.RemoveAll(w => w == null || w.gameObject == null);
    if (waterSources.Count > 0)
      return;
    isIgnited = true;
    if (!(fireFXObj != null))
      return;
    fireFXObj.SetActive(true);
  }

  public void Extinguish()
  {
    isIgnited = false;
    if (fireFXObj != null)
      fireFXObj.SetActive(false);
    reigniteAtTime = timeDir.HoursFromNow(0.5f);
  }

  public void AddLiquid(Identifiable.Id liquidId, float units)
  {
    if (!Identifiable.IsWater(liquidId))
      return;
    Extinguish();
  }
}
