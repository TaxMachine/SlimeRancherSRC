// Decompiled with JetBrains decompiler
// Type: SlimeFeeder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeFeeder : SRBehaviour, LandPlotModel.Participant
{
  public Dictionary<FeedSpeed, float> hoursByFeedSpeed = new Dictionary<FeedSpeed, float>()
  {
    {
      FeedSpeed.Slow,
      9f
    },
    {
      FeedSpeed.Normal,
      6f
    },
    {
      FeedSpeed.Fast,
      3f
    }
  };
  public int itemsPerFeeding = 6;
  public GameObject spawnFX;
  public GameObject feederSpeedUI;
  public Image feederIcon;
  public Sprite slowIcon;
  public Sprite normalIcon;
  public Sprite fastIcon;
  private SiloStorage storage;
  private TimeDirector timeDir;
  private float nextEject;
  private Region region;
  private LandPlotModel model;
  private const float EJECT_DIST = 0.5f;
  private const float EJECT_FORCE = 500f;
  private const float EJECT_NOISE = 400f;
  private const float EJECT_RATE = 0.5f;

  public void Awake()
  {
    storage = GetComponentInParent<SiloStorage>();
    region = GetComponentInParent<Region>();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    SetFeederSpeedIcon(FeedSpeed.Normal);
  }

  public void InitModel(LandPlotModel model)
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    model.nextFeedingTime = timeDir.HoursFromNowOrStart(CalcFeedingCycleHours(model.feederCycleSpeed));
  }

  public void SetModel(LandPlotModel model)
  {
    this.model = model;
    SetFeederSpeedIcon(model.feederCycleSpeed);
  }

  public void Update()
  {
    UpdateToTime(timeDir.WorldTime());
    if (!ShouldFeed())
      return;
    ProcessFeedOperation(true);
  }

  public void UpdateToTime(double worldTime)
  {
    for (; TimeUtil.HasReached(worldTime, model.nextFeedingTime); model.nextFeedingTime += 3600.0 * CalcFeedingCycleHours())
      model.remainingFeedOperations += itemsPerFeeding;
  }

  private bool ShouldFeed() => model.remainingFeedOperations > 0 && Time.time > (double) nextEject && !region.Hibernated;

  private void ProcessFeedOperation(bool ejectFood)
  {
    Ammo relevantAmmo = storage.GetRelevantAmmo();
    relevantAmmo.SetAmmoSlot(0);
    if (relevantAmmo.HasSelectedAmmo())
    {
      if (ejectFood)
        EjectFood(relevantAmmo);
      relevantAmmo.DecrementSelectedAmmo();
    }
    model.remainingFeedOperations = Math.Max(0, model.remainingFeedOperations - 1);
  }

  private void EjectFood(Ammo storageAmmo)
  {
    GameObject gameObject = InstantiateActor(storageAmmo.GetSelectedStored(), region.setId, transform.position + transform.forward * 0.5f, transform.rotation);
    Rigidbody component = gameObject.GetComponent<Rigidbody>();
    component.AddForce((transform.forward * 500f + UnityEngine.Random.insideUnitSphere * 400f) * component.mass);
    nextEject = Time.time + 0.5f;
    if (!(spawnFX != null))
      return;
    SpawnAndPlayFX(spawnFX, gameObject.transform.position, transform.rotation);
  }

  public Identifiable.Id GetFoodId() => storage.GetRelevantAmmo().GetSelectedId();

  public int GetFoodCount() => storage.GetRelevantAmmo().GetSlotCount(0);

  public int RemainingFeedOperationsFastForward() => Mathf.Min(model.remainingFeedOperations, GetFoodCount());

  public void ProcessFeedOperationFastForward() => ProcessFeedOperation(false);

  public void SetFeederSpeed(FeedSpeed speed)
  {
    model.feederCycleSpeed = speed;
    SetFeederSpeedIcon(speed);
  }

  private void SetFeederSpeedIcon(FeedSpeed speed)
  {
    switch (speed)
    {
      case FeedSpeed.Normal:
        feederIcon.sprite = normalIcon;
        break;
      case FeedSpeed.Slow:
        feederIcon.sprite = slowIcon;
        break;
      case FeedSpeed.Fast:
        feederIcon.sprite = fastIcon;
        break;
    }
  }

  public void StepFeederSpeed()
  {
    switch (model.feederCycleSpeed)
    {
      case FeedSpeed.Normal:
        SetFeederSpeed(FeedSpeed.Fast);
        break;
      case FeedSpeed.Slow:
        SetFeederSpeed(FeedSpeed.Normal);
        break;
      case FeedSpeed.Fast:
        SetFeederSpeed(FeedSpeed.Slow);
        break;
    }
  }

  public FeedSpeed GetFeedingCycleSpeed() => model.feederCycleSpeed;

  private float CalcFeedingCycleHours() => CalcFeedingCycleHours(model.feederCycleSpeed);

  private float CalcFeedingCycleHours(FeedSpeed speed) => hoursByFeedSpeed[speed];

  public enum FeedSpeed
  {
    Normal,
    Slow,
    Fast,
  }
}
