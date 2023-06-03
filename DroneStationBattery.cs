// Decompiled with JetBrains decompiler
// Type: DroneStationBattery
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using UnityEngine;

[RequireComponent(typeof (DroneStation))]
public class DroneStationBattery : SRBehaviour, LiquidConsumer, GadgetModel.Participant
{
  [Tooltip("Battery meter transform.")]
  public Transform meter;
  private const float DURATION_HOURS = 28f;
  private const float DURATION_SECONDS = 100800f;
  private TimeDirector timeDirector;
  private bool? previousHasAny;
  private double fxCooldownTime;
  private DroneModel droneModel;

  public event OnReset onReset;

  public event OnHasAnyChanged onHasAnyChanged;

  public DroneStation station { get; private set; }

  public double Time => droneModel.batteryDepleteTime;

  private float percentage
  {
    get => meter.localScale.y;
    set => meter.localScale = new Vector3(1f, value, 1f);
  }

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    station = GetComponent<DroneStation>();
  }

  public void InitModel(GadgetModel droneModel) => Reset((DroneModel) droneModel);

  public void SetModel(GadgetModel droneModel) => this.droneModel = (DroneModel) droneModel;

  public void AddLiquid(Identifiable.Id id, float units)
  {
    if (percentage < 1.0 && timeDirector.HasReached(fxCooldownTime))
    {
      SECTR_AudioSystem.Play(station.gadget.metadata.onBatteryFilledCue, transform.position, false);
      fxCooldownTime = timeDirector.HoursFromNow(71f / (452f * Math.PI));
      if (station.gadget.metadata.onBatteryFilledFX != null)
        SpawnAndPlayFX(station.gadget.metadata.onBatteryFilledFX, gameObject);
    }
    Reset(droneModel);
  }

  public void Update()
  {
    percentage = Mathf.Clamp01((float) ((Time - timeDirector.WorldTime()) / 100800.0));
    bool? previousHasAny = this.previousHasAny;
    bool flag = HasAny();
    if (previousHasAny.GetValueOrDefault() == flag & previousHasAny.HasValue)
      return;
    this.previousHasAny = new bool?(HasAny());
    if (onHasAnyChanged == null)
      return;
    onHasAnyChanged();
  }

  public bool HasAny() => percentage > 0.0;

  private void Reset(DroneModel droneModel)
  {
    percentage = 1f;
    droneModel.batteryDepleteTime = timeDirector.HoursFromNow(28f);
    if (onReset == null)
      return;
    onReset();
  }

  public delegate void OnReset();

  public delegate void OnHasAnyChanged();
}
