// Decompiled with JetBrains decompiler
// Type: DroneAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneAnimator : SRAnimator<Drone>
{
  private static Dictionary<Id, int> ANIMATION_DICT;
  private static readonly int HAS_BATTERY = Animator.StringToHash(nameof (HAS_BATTERY));
  private Dictionary<DroneAnimatorState.Id, Action> onStateExit = new Dictionary<DroneAnimatorState.Id, Action>(DroneAnimatorState.IdComparer.Instance);

  public override void Awake()
  {
    base.Awake();
    SetAnimation(Id.IDLE);
  }

  public void Start()
  {
    battery.onHasAnyChanged -= OnBatteryHasAnyChanged;
    battery.onHasAnyChanged += OnBatteryHasAnyChanged;
    OnBatteryHasAnyChanged();
  }

  public override void OnEnable()
  {
    base.OnEnable();
    if (!(battery != null))
      return;
    battery.onHasAnyChanged -= OnBatteryHasAnyChanged;
    battery.onHasAnyChanged += OnBatteryHasAnyChanged;
    OnBatteryHasAnyChanged();
  }

  public override void OnDisable()
  {
    base.OnDisable();
    battery.onHasAnyChanged -= OnBatteryHasAnyChanged;
  }

  static DroneAnimator()
  {
    ANIMATION_DICT = new Dictionary<Id, int>(IdComparer.Instance);
    foreach (Id key in Enum.GetValues(typeof (Id)).Cast<Id>())
      ANIMATION_DICT.Add(key, Animator.StringToHash(Enum.GetName(typeof (Id), key)));
  }

  public void SetAnimation(Id id)
  {
    onStateExit.Clear();
    if (!animator.isInitialized)
      return;
    foreach (KeyValuePair<Id, int> keyValuePair in ANIMATION_DICT)
      animator.SetBool(keyValuePair.Value, keyValuePair.Key == id);
  }

  private void OnBatteryHasAnyChanged() => animator.SetBool(HAS_BATTERY, battery.HasAny());

  private DroneStationBattery battery => parent.station.battery;

  public void OnStateExit(DroneAnimatorState.Id id, Action callback)
  {
    if (onStateExit.ContainsKey(id))
      onStateExit[id] += callback;
    else
      onStateExit[id] = callback;
  }

  public void OnStateExit(DroneAnimatorState.Id id)
  {
    Action action;
    if (!onStateExit.TryGetValue(id, out action) || action == null)
      return;
    action();
    onStateExit[id] = null;
  }

  public enum Id
  {
    IDLE,
    MOVE,
    GATHER,
    DEPOSIT,
    REST,
    IDLE_CELEBRATE,
    IDLE_GRUMP,
  }

  public class IdComparer : IEqualityComparer<Id>
  {
    public static IdComparer Instance = new IdComparer();

    public bool Equals(Id a, Id b) => a == b;

    public int GetHashCode(Id a) => (int) a;
  }
}
