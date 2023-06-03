// Decompiled with JetBrains decompiler
// Type: DroneSubbehaviourRest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class DroneSubbehaviourRest : DroneProgram
{
  private const float RETHINK_BASE_HOURS = 0.333333343f;
  private const float RETHINK_PERIOD_HOURS = 0.166666672f;
  private double rethinkTime;

  public override bool Relevancy() => true;

  public override void Selected()
  {
    base.Selected();
    rethinkTime = double.MaxValue;
  }

  public override void Deselected()
  {
    base.Deselected();
    drone.station.battery.onReset -= ForceRethink;
    drone.station.battery.onHasAnyChanged -= OnBatteryHasAnyChanged;
  }

  public void ForceRethink() => rethinkTime = 0.0;

  protected override IEnumerable<Orientation> GetTargetOrientations()
  {
    /*// ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    DroneSubbehaviourRest subbehaviourRest = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = subbehaviourRest.drone.GetRestingOrientation();
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;*/
    yield return drone.GetRestingOrientation();
  }

  protected override Vector3 GetTargetPosition() => drone.station.guideRest.position;

  protected override bool CanCancel() => false;

  protected override void OnFirstAction()
  {
    base.OnFirstAction();
    rethinkTime = timeDirector.HoursFromNow(0.333333343f);
    drone.noClip.enabled = false;
    drone.onActiveCue.enabled = false;
    drone.station.battery.onReset += ForceRethink;
    drone.station.battery.onHasAnyChanged += OnBatteryHasAnyChanged;
    OnBatteryHasAnyChanged();
  }

  protected override bool OnAction()
  {
    if (timeDirector.HasReached(rethinkTime))
    {
      if (plexer.PickNextGatherBehaviour())
      {
        drone.onActiveCue.enabled = true;
        drone.station.animator.SetEnabled(true);
        return true;
      }
      rethinkTime = timeDirector.HoursFromNow(0.166666672f);
    }
    return false;
  }

  private void OnBatteryHasAnyChanged() => drone.station.animator.SetEnabled(drone.station.battery.HasAny());

  protected override DroneAnimator.Id animation => DroneAnimator.Id.REST;

  protected override DroneAnimatorState.Id animationStateBegin => DroneAnimatorState.Id.REST_BEGIN;

  protected override DroneAnimatorState.Id animationStateEnd => DroneAnimatorState.Id.REST_END;
}
