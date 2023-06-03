// Decompiled with JetBrains decompiler
// Type: DynamicBoneCollider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Dynamic Bone/Dynamic Bone Collider")]
public class DynamicBoneCollider : MonoBehaviour
{
  public Vector3 m_Center = Vector3.zero;
  public float m_Radius = 0.5f;
  public float m_Height;
  public Direction m_Direction;
  public Bound m_Bound;

  private void OnValidate()
  {
    m_Radius = Mathf.Max(m_Radius, 0.0f);
    m_Height = Mathf.Max(m_Height, 0.0f);
  }

  public void Collide(ref Vector3 particlePosition, float particleRadius)
  {
    float num1 = m_Radius * Mathf.Abs(transform.lossyScale.x);
    float num2 = m_Height * 0.5f - num1;
    if (num2 <= 0.0)
    {
      if (m_Bound == Bound.Outside)
        OutsideSphere(ref particlePosition, particleRadius, transform.TransformPoint(m_Center), num1);
      else
        InsideSphere(ref particlePosition, particleRadius, transform.TransformPoint(m_Center), num1);
    }
    else
    {
      Vector3 center1 = m_Center;
      Vector3 center2 = m_Center;
      switch (m_Direction)
      {
        case Direction.X:
          center1.x -= num2;
          center2.x += num2;
          break;
        case Direction.Y:
          center1.y -= num2;
          center2.y += num2;
          break;
        case Direction.Z:
          center1.z -= num2;
          center2.z += num2;
          break;
      }
      if (m_Bound == Bound.Outside)
        OutsideCapsule(ref particlePosition, particleRadius, transform.TransformPoint(center1), transform.TransformPoint(center2), num1);
      else
        InsideCapsule(ref particlePosition, particleRadius, transform.TransformPoint(center1), transform.TransformPoint(center2), num1);
    }
  }

  private static void OutsideSphere(
    ref Vector3 particlePosition,
    float particleRadius,
    Vector3 sphereCenter,
    float sphereRadius)
  {
    float num1 = sphereRadius + particleRadius;
    float num2 = num1 * num1;
    Vector3 vector3 = particlePosition - sphereCenter;
    float sqrMagnitude = vector3.sqrMagnitude;
    if (sqrMagnitude <= 0.0 || sqrMagnitude >= (double) num2)
      return;
    float num3 = Mathf.Sqrt(sqrMagnitude);
    particlePosition = sphereCenter + vector3 * (num1 / num3);
  }

  private static void InsideSphere(
    ref Vector3 particlePosition,
    float particleRadius,
    Vector3 sphereCenter,
    float sphereRadius)
  {
    float num1 = sphereRadius + particleRadius;
    float num2 = num1 * num1;
    Vector3 vector3 = particlePosition - sphereCenter;
    float sqrMagnitude = vector3.sqrMagnitude;
    if (sqrMagnitude <= (double) num2)
      return;
    float num3 = Mathf.Sqrt(sqrMagnitude);
    particlePosition = sphereCenter + vector3 * (num1 / num3);
  }

  private static void OutsideCapsule(
    ref Vector3 particlePosition,
    float particleRadius,
    Vector3 capsuleP0,
    Vector3 capsuleP1,
    float capsuleRadius)
  {
    float num1 = capsuleRadius + particleRadius;
    float num2 = num1 * num1;
    Vector3 rhs = capsuleP1 - capsuleP0;
    Vector3 lhs = particlePosition - capsuleP0;
    float num3 = Vector3.Dot(lhs, rhs);
    if (num3 <= 0.0)
    {
      float sqrMagnitude = lhs.sqrMagnitude;
      if (sqrMagnitude <= 0.0 || sqrMagnitude >= (double) num2)
        return;
      float num4 = Mathf.Sqrt(sqrMagnitude);
      particlePosition = capsuleP0 + lhs * (num1 / num4);
    }
    else
    {
      float sqrMagnitude1 = rhs.sqrMagnitude;
      if (num3 >= (double) sqrMagnitude1)
      {
        Vector3 vector3 = particlePosition - capsuleP1;
        float sqrMagnitude2 = vector3.sqrMagnitude;
        if (sqrMagnitude2 <= 0.0 || sqrMagnitude2 >= (double) num2)
          return;
        float num5 = Mathf.Sqrt(sqrMagnitude2);
        particlePosition = capsuleP1 + vector3 * (num1 / num5);
      }
      else
      {
        if (sqrMagnitude1 <= 0.0)
          return;
        float num6 = num3 / sqrMagnitude1;
        Vector3 vector3 = lhs - rhs * num6;
        float sqrMagnitude3 = vector3.sqrMagnitude;
        if (sqrMagnitude3 <= 0.0 || sqrMagnitude3 >= (double) num2)
          return;
        float num7 = Mathf.Sqrt(sqrMagnitude3);
        particlePosition += vector3 * ((num1 - num7) / num7);
      }
    }
  }

  private static void InsideCapsule(
    ref Vector3 particlePosition,
    float particleRadius,
    Vector3 capsuleP0,
    Vector3 capsuleP1,
    float capsuleRadius)
  {
    float num1 = capsuleRadius + particleRadius;
    float num2 = num1 * num1;
    Vector3 rhs = capsuleP1 - capsuleP0;
    Vector3 lhs = particlePosition - capsuleP0;
    float num3 = Vector3.Dot(lhs, rhs);
    if (num3 <= 0.0)
    {
      float sqrMagnitude = lhs.sqrMagnitude;
      if (sqrMagnitude <= (double) num2)
        return;
      float num4 = Mathf.Sqrt(sqrMagnitude);
      particlePosition = capsuleP0 + lhs * (num1 / num4);
    }
    else
    {
      float sqrMagnitude1 = rhs.sqrMagnitude;
      if (num3 >= (double) sqrMagnitude1)
      {
        Vector3 vector3 = particlePosition - capsuleP1;
        float sqrMagnitude2 = vector3.sqrMagnitude;
        if (sqrMagnitude2 <= (double) num2)
          return;
        float num5 = Mathf.Sqrt(sqrMagnitude2);
        particlePosition = capsuleP1 + vector3 * (num1 / num5);
      }
      else
      {
        if (sqrMagnitude1 <= 0.0)
          return;
        float num6 = num3 / sqrMagnitude1;
        Vector3 vector3 = lhs - rhs * num6;
        float sqrMagnitude3 = vector3.sqrMagnitude;
        if (sqrMagnitude3 <= (double) num2)
          return;
        float num7 = Mathf.Sqrt(sqrMagnitude3);
        particlePosition += vector3 * ((num1 - num7) / num7);
      }
    }
  }

  private void OnDrawGizmosSelected()
  {
    if (!enabled)
      return;
    Gizmos.color = m_Bound != Bound.Outside ? Color.magenta : Color.yellow;
    float radius = m_Radius * Mathf.Abs(transform.lossyScale.x);
    float num = m_Height * 0.5f - radius;
    if (num <= 0.0)
    {
      Gizmos.DrawWireSphere(transform.TransformPoint(m_Center), radius);
    }
    else
    {
      Vector3 center1 = m_Center;
      Vector3 center2 = m_Center;
      switch (m_Direction)
      {
        case Direction.X:
          center1.x -= num;
          center2.x += num;
          break;
        case Direction.Y:
          center1.y -= num;
          center2.y += num;
          break;
        case Direction.Z:
          center1.z -= num;
          center2.z += num;
          break;
      }
      Gizmos.DrawWireSphere(transform.TransformPoint(center1), radius);
      Gizmos.DrawWireSphere(transform.TransformPoint(center2), radius);
    }
  }

  public enum Direction
  {
    X,
    Y,
    Z,
  }

  public enum Bound
  {
    Outside,
    Inside,
  }
}
