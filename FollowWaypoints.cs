// Decompiled with JetBrains decompiler
// Type: FollowWaypoints
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FollowWaypoints : SlimeSubbehaviour
{
  public float straightlineForceFactor = 1f;
  public float facingStability = 1f;
  public float facingSpeed = 5f;
  public float slowSpeedLimit = 0.35f;
  [Tooltip("Factor multiplied instantly to the slime's velocity when slow is applied.")]
  public float slowSpeedInstantFactor = 0.6f;
  [Tooltip("Maximum slime starting velocity when the slow is applied.")]
  public float slowSpeedInstantMaxVelocity = 18f;
  [Tooltip("Delay, in game seconds, between rotation changes. Helps reduce jitter.")]
  public float rotationDelay = 10f;
  private RaceWaypoint nextWaypoint;
  private double resetAfter = double.PositiveInfinity;
  private double disableUntil;
  private double slowUntil;
  private double rotateTime;
  private TimeDirector timeDir;
  private const float TRY_TO_FOLLOW_TIME = 0.166666672f;
  private const float RESET_DISABLE_TIME = 0.166666672f;

  public override void Awake()
  {
    base.Awake();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
  }

  public override float Relevancy(bool isGrounded)
  {
    if (!timeDir.HasReached(disableUntil))
      return 0.0f;
    if (nextWaypoint == null)
    {
      nextWaypoint = RaceWaypoint.GetNearest(transform.position, 225f);
      if (nextWaypoint != null)
        resetAfter = timeDir.HoursFromNow(0.166666672f);
    }
    return !(nextWaypoint == null) ? 0.8f : 0.0f;
  }

  public override void Selected()
  {
  }

  public override void Deselected()
  {
    base.Deselected();
    nextWaypoint = null;
    resetAfter = double.PositiveInfinity;
    disableUntil = 0.0;
    rotateTime = 0.0;
  }

  public override void Action()
  {
    if (timeDir.HasReached(resetAfter))
    {
      disableUntil = timeDir.HoursFromNow(0.166666672f);
      nextWaypoint = null;
      resetAfter = double.PositiveInfinity;
    }
    else
    {
      if (!(nextWaypoint != null))
        return;
      if (nextWaypoint.HasHitCheckpoint(transform.position))
      {
        nextWaypoint = nextWaypoint.GetNext();
        if (nextWaypoint != null)
          resetAfter = timeDir.HoursFromNow(0.166666672f);
      }
      if (!(nextWaypoint != null))
        return;
      Vector3 normalized = (nextWaypoint.transform.position - transform.position).normalized;
      if (timeDir.HasReached(rotateTime))
      {
        RotateTowards(normalized, facingSpeed, facingStability);
        rotateTime = timeDir.HoursFromNow(rotationDelay * 0.000277777785f);
      }
      if (!IsGrounded())
        return;
      MoveTowards(normalized, Mathf.Min(timeDir.HasReached(slowUntil) ? 1f : slowSpeedLimit, nextWaypoint.approachForceFactor));
    }
  }

  public void ApplySlow(float durationGameHrs)
  {
    if (timeDir.HasReached(slowUntil))
    {
      if (slimeBody.velocity.sqrMagnitude > slowSpeedInstantMaxVelocity * (double) slowSpeedInstantMaxVelocity)
        slimeBody.velocity = slimeBody.velocity.normalized * slowSpeedInstantMaxVelocity;
      slimeBody.velocity = slimeBody.velocity * slowSpeedInstantFactor;
    }
    slowUntil = timeDir.HoursFromNow(durationGameHrs);
  }

  private void MoveTowards(Vector3 direction, float approachForceFactor)
  {
    slimeBody.AddForce(direction * ((float) (straightlineForceFactor * (double) approachForceFactor * 80.0) * slimeBody.mass * Time.fixedDeltaTime));
    Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
    slimeBody.AddForceAtPosition(direction * ((float) (straightlineForceFactor * (double) approachForceFactor * 240.0) * slimeBody.mass * Time.fixedDeltaTime), position);
  }
}
