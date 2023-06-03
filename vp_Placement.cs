// Decompiled with JetBrains decompiler
// Type: vp_Placement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_Placement
{
  public Vector3 Position = Vector3.zero;
  public Quaternion Rotation = Quaternion.identity;

  public static bool AdjustPosition(vp_Placement p, float physicsRadius, int attempts = 1000)
  {
    --attempts;
    if (attempts > 0)
    {
      if (p.IsObstructed(physicsRadius))
      {
        Vector3 insideUnitSphere = Random.insideUnitSphere;
        p.Position.x += insideUnitSphere.x;
        p.Position.z += insideUnitSphere.z;
        AdjustPosition(p, physicsRadius, attempts);
      }
      return true;
    }
    Debug.LogWarning("(vp_Placement.AdjustPosition) Failed to find valid placement.");
    return false;
  }

  public virtual bool IsObstructed(float physicsRadius = 1f) => Physics.CheckSphere(Position, physicsRadius, 270532864);

  public static void SnapToGround(vp_Placement p, float radius, float snapDistance)
  {
    if (snapDistance == 0.0)
      return;
    RaycastHit hitInfo;
    Physics.SphereCast(new Ray(p.Position + Vector3.up * snapDistance, Vector3.down), radius, out hitInfo, snapDistance * 2f, -675375893);
    if (!(hitInfo.collider != null))
      return;
    p.Position.y = hitInfo.point.y + 0.05f;
  }
}
