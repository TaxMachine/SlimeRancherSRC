// Decompiled with JetBrains decompiler
// Type: DronePather
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DronePather : Pather
{
  private float sqrMaxConnectionDist;

  public DronePather(float maxConnDist) => sqrMaxConnectionDist = maxConnDist * maxConnDist;

  private static bool PathIsBlocked(Vector3 start, Vector3 end)
  {
    Vector3 vector3 = end - start;
    return Physics.SphereCast(start, 0.5f, vector3.normalized, out RaycastHit _, vector3.magnitude, -537968901);
  }

  protected override bool PathPredicate(Vector3 start, Vector3 end) => (start - end).sqrMagnitude <= (double) sqrMaxConnectionDist && !PathIsBlocked(start, end);

  protected override bool NearestAccessibleNodePredicate(Vector3 start, Vector3 end) => PathPredicate(start, end);
}
