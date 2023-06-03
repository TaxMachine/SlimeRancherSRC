// Decompiled with JetBrains decompiler
// Type: DroneSubbehaviourDumpAmmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DroneSubbehaviourDumpAmmo : DroneSubbehaviour
{
  [HideInInspector]
  public bool destructive;
  private State state;
  private double time;

  public override bool Relevancy() => !drone.ammo.IsEmpty();

  public override void Selected()
  {
    base.Selected();
    drone.movement.rigidbody.isKinematic = true;
    drone.movement.rigidbody.velocity = Vector3.zero;
    drone.movement.rigidbody.angularVelocity = Vector3.zero;
    state = State.ANIMATE;
    drone.animator.SetAnimation(DroneAnimator.Id.IDLE_GRUMP);
    drone.animator.OnStateExit(DroneAnimatorState.Id.IDLE_GRUMP, () =>
    {
      drone.animator.SetAnimation(DroneAnimator.Id.IDLE);
      state = State.ELEVATE;
    });
  }

  public override void Deselected()
  {
    base.Deselected();
    drone.animator.SetAnimation(DroneAnimator.Id.IDLE);
    drone.movement.rigidbody.isKinematic = false;
  }

  public override void Action()
  {
    if (state == State.ELEVATE)
    {
      if (Physics.Raycast(drone.transform.position, Vector3.down, 3f))
      {
        drone.movement.MoveTowards(drone.transform.position + Vector3.up);
      }
      else
      {
        state = State.DUMP;
        time = 0.0;
      }
    }
    if (state != State.DUMP || !OnAction_DumpAmmo(ref time) || !drone.ammo.IsEmpty())
      return;
    plexer.ForceRethink();
    if (!destructive)
      return;
    if (drone.metadata.onTeleportFX != null)
      SpawnAndPlayFX(drone.metadata.onTeleportFX, drone.transform.position, drone.transform.rotation);
    Destroyer.Destroy(drone.gameObject, "DroneSubbehaviourDumpAmmo.Destructive.Action");
  }

  private enum State
  {
    ANIMATE,
    ELEVATE,
    DUMP,
  }
}
