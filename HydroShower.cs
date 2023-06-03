// Decompiled with JetBrains decompiler
// Type: HydroShower
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class HydroShower : SRBehaviour, GadgetInteractor
{
  public Identifiable.Id liquidId = Identifiable.Id.WATER_LIQUID;
  public FilteredTrackCollisions tracker;
  public GameObject showerFX;
  [Tooltip("How long we should keep the shower going in game-minutes")]
  public float waterDuration = 5f;
  [Tooltip("How long between activations in game-minutes")]
  public float timeBetweenShowers = 60f;
  public float wateringUnitsPerPulse = 0.5f;
  [Tooltip("How long between pulses in game-minutes")]
  public float pulseDelay = 0.5f;
  private double waterUntil;
  private double nextWaterPulse;
  private double nextShower;
  private TimeDirector timeDir;
  private WaitForChargeup waiter;

  public void Awake()
  {
    waiter = GetComponentInParent<WaitForChargeup>();
    tracker.SetFilter(IsTarr);
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
  }

  public void Update()
  {
    if (waiter.IsWaiting())
      return;
    if (timeDir.HasReached(waterUntil))
    {
      showerFX.SetActive(false);
      nextWaterPulse = double.PositiveInfinity;
    }
    else
    {
      if (timeDir.HasReached(nextWaterPulse))
      {
        DoWaterPulse();
        nextWaterPulse = timeDir.HoursFromNow(pulseDelay * 0.0166666675f);
      }
      showerFX.SetActive(true);
    }
  }

  public void OnInteract()
  {
    if (!timeDir.HasReached(nextShower))
      return;
    waterUntil = timeDir.HoursFromNow(waterDuration * 0.0166666675f);
    nextWaterPulse = 0.0;
    nextShower = timeDir.HoursFromNow(timeBetweenShowers * 0.0166666675f);
  }

  public bool CanInteract() => true;

  private bool IsTarr(GameObject gameObj) => Identifiable.IsTarr(Identifiable.GetId(gameObj));

  private void DoWaterPulse()
  {
    HashSet<GameObject> gameObjectSet = tracker.CurrColliders();
    if (gameObjectSet.Count <= 0)
      return;
    HashSet<LiquidConsumer> liquidConsumerSet = new HashSet<LiquidConsumer>();
    foreach (GameObject gameObject in gameObjectSet)
    {
      if (gameObject != null)
      {
        foreach (LiquidConsumer liquidConsumer in gameObject.GetComponentsInParent<LiquidConsumer>())
          liquidConsumerSet.Add(liquidConsumer);
      }
    }
    foreach (LiquidConsumer liquidConsumer in liquidConsumerSet)
      liquidConsumer.AddLiquid(liquidId, wateringUnitsPerPulse);
  }
}
