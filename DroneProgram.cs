// Decompiled with JetBrains decompiler
// Type: DroneProgram
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public abstract class DroneProgram : DroneSubbehaviour
{
  private const float ARRIVE_RAD = 1f;
  private const float NO_CLIP_PERIOD = 10f;
  private const float ARRIVAL_NO_CLIP_PERIOD = 2f;
  private State state;
  private Queue<Vector3> existingPath;
  private Orientation arrivalOrient;
  private Vector3 previousTargetPosition;
  private Vector3 noClipPreviousPosition;
  private float noClipTime;

  public override void Selected()
  {
    base.Selected();
    drone.animator.SetAnimation(DroneAnimator.Id.MOVE);
    noClipPreviousPosition = drone.transform.position;
    noClipTime = 10f;
    state = State.PATHING;
  }

  public override void Deselected()
  {
    base.Deselected();
    drone.animator.SetAnimation(DroneAnimator.Id.IDLE);
    drone.movement.rigidbody.isKinematic = false;
    drone.upright.enabled = true;
    existingPath = null;
  }

  public void OnDrawGizmos()
  {
    if (existingPath == null)
      return;
    Gizmos.color = Color.blue;
    Vector3 from = drone.transform.position;
    foreach (Vector3 to in existingPath)
    {
      Gizmos.DrawLine(from, to);
      from = to;
    }
  }

  public override sealed void Action()
  {
    if (state == State.COMPLETE || CanCancel())
    {
      plexer.ForceRethink();
    }
    else
    {
      if ((state == State.PATHING || state == State.PATHING_ARRIVAL) && !drone.noClip.enabled)
      {
        noClipTime -= Time.fixedDeltaTime;
        if (noClipTime <= 0.0)
        {
          float sqrMagnitude = (noClipPreviousPosition - drone.transform.position).sqrMagnitude;
          noClipPreviousPosition = drone.transform.position;
          noClipTime = state == State.PATHING_ARRIVAL ? 2f : 10f;
          drone.noClip.enabled = sqrMagnitude <= 0.10000000149011612;
        }
      }
      if (state == State.PATHING)
      {
        if (existingPath == null)
        {
          if (!timeDirector.HasReached(drone.network.pathingThrottleUntil))
            return;
          drone.network.pathingThrottleUntil = timeDirector.HoursFromNow(0.0333333351f);
          GeneratePath(GetSubnetwork(), GetTargetOrientations(), GetTargetPosition());
        }
        if (existingPath == null)
        {
          plexer.ForceRethink();
          return;
        }
        Vector3? nullable = existingPath.Count > 0 ? new Vector3?(existingPath.Peek()) : new Vector3?();
        if (!nullable.HasValue || (nullable.Value - drone.transform.position).sqrMagnitude <= 1.0)
        {
          if (existingPath.Count <= 1)
          {
            if ((GetTargetPosition() - previousTargetPosition).sqrMagnitude >= 1.0)
            {
              existingPath = null;
              return;
            }
            state = State.PATHING_ARRIVAL;
            noClipTime = Mathf.Min(noClipTime, 2f);
            drone.movement.rigidbody.velocity = Vector3.zero;
            drone.movement.rigidbody.angularVelocity = Vector3.zero;
            drone.upright.enabled = false;
            existingPath = null;
          }
          else
          {
            drone.noClip.enabled = false;
            existingPath.Dequeue();
          }
        }
        else
          drone.movement.PathTowards(nullable.Value);
      }
      if (state == State.PATHING_ARRIVAL && drone.movement.MoveTowards(arrivalOrient.pos) && drone.movement.RotateTowards(arrivalOrient.rot))
      {
        drone.movement.rigidbody.isKinematic = true;
        OnReachedDestination();
        state = State.PATHING_ARRIVED;
      }
      else
      {
        if (state == State.PATHING_ARRIVED)
        {
          System.Action callback = () => state = State.ACTION_LOOP_FIRST;
          if (animationStateBegin != DroneAnimatorState.Id.NONE)
          {
            state = State.ACTION_PRE;
            drone.animator.SetAnimation(animation);
            drone.animator.OnStateExit(animationStateBegin, callback);
          }
          else
          {
            drone.animator.SetAnimation(animation);
            callback();
          }
        }
        if (state == State.ACTION_LOOP_FIRST)
        {
          state = State.ACTION_LOOP;
          OnFirstAction();
        }
        if (state != State.ACTION_LOOP || !OnAction())
          return;
        System.Action callback1 = () => state = State.COMPLETE;
        if (animationStateEnd != DroneAnimatorState.Id.NONE)
        {
          state = State.ACTION_POST;
          drone.animator.SetAnimation(DroneAnimator.Id.IDLE);
          drone.animator.OnStateExit(animationStateEnd, callback1);
        }
        else
        {
          drone.animator.SetAnimation(DroneAnimator.Id.IDLE);
          callback1();
        }
      }
    }
  }

  protected bool GeneratePath(
    GardenDroneSubnetwork subnetwork,
    IEnumerable<Orientation> orientations,
    Vector3 position)
  {
    Vector3 position1 = drone.transform.position;
    List<PathingNetwork> pathingNetworkList = new List<PathingNetwork>();
    if (subnetwork != null)
      pathingNetworkList.Add(subnetwork);
    pathingNetworkList.Add(drone.network);
    foreach (Orientation orientation in orientations)
    {
      foreach (PathingNetwork pathingNetwork in pathingNetworkList)
      {
        if ((existingPath = pathingNetwork.GeneratePath(position1, orientation.pos)) != null)
        {
          arrivalOrient = orientation;
          previousTargetPosition = position;
          return true;
        }
      }
    }
    OnPathGenerationFailed();
    return false;
  }

  protected virtual GardenDroneSubnetwork GetSubnetwork() => null;

  protected abstract DroneAnimator.Id animation { get; }

  protected abstract DroneAnimatorState.Id animationStateBegin { get; }

  protected abstract DroneAnimatorState.Id animationStateEnd { get; }

  protected abstract IEnumerable<Orientation> GetTargetOrientations();

  protected abstract Vector3 GetTargetPosition();

  protected virtual bool CanCancel() => false;

  protected virtual void OnReachedDestination()
  {
  }

  protected virtual void OnFirstAction()
  {
  }

  protected virtual bool OnAction() => true;

  protected virtual void OnPathGenerationFailed()
  {
  }

  public class Orientation
  {
    public Vector3 pos;
    public Quaternion rot;

    public Orientation()
    {
    }

    public Orientation(Vector3 pos, Quaternion rot)
    {
      this.pos = pos;
      this.rot = rot;
    }
  }

  private enum State
  {
    PATHING,
    PATHING_ARRIVAL,
    PATHING_ARRIVED,
    ACTION_PRE,
    ACTION_LOOP_FIRST,
    ACTION_LOOP,
    ACTION_POST,
    COMPLETE,
  }
}
