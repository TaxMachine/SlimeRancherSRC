// Decompiled with JetBrains decompiler
// Type: DroneSubbehaviourIdle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DroneSubbehaviourIdle : DroneSubbehaviour
{
  private double cooldown;
  private Quaternion? rotation;

  public override bool Relevancy() => drone.station.battery.HasAny() && timeDirector.HasReached(cooldown) && Randoms.SHARED.GetProbability(0.1f);

  public override void Selected()
  {
    base.Selected();
    drone.movement.rigidbody.isKinematic = true;
    rotation = new Quaternion?(Quaternion.LookRotation(SRSingleton<SceneContext>.Instance.Player.transform.position - drone.transform.position));
    cooldown = timeDirector.HoursFromNow(3f);
  }

  public override void Deselected()
  {
    base.Deselected();
    drone.animator.SetAnimation(DroneAnimator.Id.IDLE);
    drone.movement.rigidbody.isKinematic = false;
  }

  public override void Action()
  {
    if (!rotation.HasValue || !drone.movement.RotateTowards(rotation.Value))
      return;
    drone.animator.SetAnimation(DroneAnimator.Id.IDLE_CELEBRATE);
    drone.animator.OnStateExit(DroneAnimatorState.Id.IDLE_CELEBRATE, () => plexer.ForceRethink());
    rotation = new Quaternion?();
  }
}
