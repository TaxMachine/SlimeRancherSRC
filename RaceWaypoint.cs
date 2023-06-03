// Decompiled with JetBrains decompiler
// Type: RaceWaypoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class RaceWaypoint : MonoBehaviour
{
  [Tooltip("Radius which a slime has to reach to trigger having reached this checkpoint")]
  public float checkpointRad = 5f;
  [Tooltip("Factor by which an approaching slime will scale their forward force")]
  public float approachForceFactor = 1f;
  [Tooltip("The waypoints a slime which reaches this waypoint should travel to next.")]
  public RaceWaypoint[] next;
  private static List<RaceWaypoint> allWaypoints = new List<RaceWaypoint>();
  public const float TRIGGER_RAD = 15f;
  public const float SQR_TRIGGER_RAD = 225f;

  public void Awake() => allWaypoints.Add(this);

  public void OnDestroy() => allWaypoints.Remove(this);

  public RaceWaypoint GetNext() => Randoms.SHARED.Pick(next, null);

  public static RaceWaypoint GetNearest(Vector3 position, float maxDistSqr)
  {
    RaceWaypoint nearest = null;
    float num = maxDistSqr;
    foreach (RaceWaypoint allWaypoint in allWaypoints)
    {
      float sqrMagnitude = (allWaypoint.transform.position - position).sqrMagnitude;
      if (sqrMagnitude < (double) num)
      {
        num = sqrMagnitude;
        nearest = allWaypoint;
      }
    }
    return nearest;
  }

  public bool HasHitCheckpoint(Vector3 checkPos) => (checkPos - transform.position).sqrMagnitude <= checkpointRad * (double) checkpointRad;

  public void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, checkpointRad);
    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(transform.position, 15f);
  }
}
