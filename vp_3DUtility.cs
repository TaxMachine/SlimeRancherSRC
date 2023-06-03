// Decompiled with JetBrains decompiler
// Type: vp_3DUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public static class vp_3DUtility
{
  public static Vector3 HorizontalVector(Vector3 value)
  {
    value.y = 0.0f;
    return value;
  }

  public static Vector3 RandomHorizontalDirection() => (Random.rotation * Vector3.up).normalized;

  public static bool OnScreen(
    Camera camera,
    Renderer renderer,
    Vector3 worldPosition,
    out Vector3 screenPosition)
  {
    screenPosition = Vector2.zero;
    if (camera == null || renderer == null || !renderer.isVisible)
      return false;
    screenPosition = camera.WorldToScreenPoint(worldPosition);
    return screenPosition.z >= 0.0;
  }

  public static bool InLineOfSight(
    Vector3 from,
    Transform target,
    Vector3 targetOffset,
    int layerMask)
  {
    RaycastHit hitInfo;
    Physics.Linecast(from, target.position + targetOffset, out hitInfo, layerMask);
    return hitInfo.collider == null || hitInfo.collider.transform.root == target;
  }

  public static bool WithinRange(Vector3 from, Vector3 to, float range, out float distance)
  {
    distance = Vector3.Distance(from, to);
    return distance <= (double) range;
  }

  public static float DistanceToRay(Ray ray, Vector3 point) => Vector3.Cross(ray.direction, point - ray.origin).magnitude;

  public static float LookAtAngle(Vector3 fromPosition, Vector3 fromForward, Vector3 toPosition) => Vector3.Cross(fromForward, (toPosition - fromPosition).normalized).y >= 0.0 ? Vector3.Angle(fromForward, (toPosition - fromPosition).normalized) : -Vector3.Angle(fromForward, (toPosition - fromPosition).normalized);

  public static float LookAtAngleHorizontal(
    Vector3 fromPosition,
    Vector3 fromForward,
    Vector3 toPosition)
  {
    return LookAtAngle(HorizontalVector(fromPosition), HorizontalVector(fromForward), HorizontalVector(toPosition));
  }

  public static float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis)
  {
    dirA -= Vector3.Project(dirA, axis);
    dirB -= Vector3.Project(dirB, axis);
    return Vector3.Angle(dirA, dirB) * (Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) < 0.0 ? -1f : 1f);
  }

  public static Quaternion GetBoneLookRotationInWorldSpace(
    Quaternion originalRotation,
    Quaternion parentRotation,
    Vector3 worldLookDir,
    float amount,
    Vector3 referenceLookDir,
    Vector3 referenceUpDir,
    Quaternion relativeWorldSpaceDifference)
  {
    Vector3 vector3 = Quaternion.Inverse(parentRotation) * worldLookDir.normalized;
    Vector3 normal1 = Quaternion.AngleAxis(AngleAroundAxis(referenceLookDir, vector3, referenceUpDir), referenceUpDir) * Quaternion.AngleAxis(AngleAroundAxis(vector3 - Vector3.Project(vector3, referenceUpDir), vector3, Vector3.Cross(referenceUpDir, vector3)), Vector3.Cross(referenceUpDir, referenceLookDir)) * referenceLookDir;
    Vector3 tangent1 = referenceUpDir;
    Vector3.OrthoNormalize(ref normal1, ref tangent1);
    Vector3 normal2 = normal1;
    Vector3 tangent2 = tangent1;
    Vector3.OrthoNormalize(ref normal2, ref tangent2);
    return Quaternion.Lerp(Quaternion.identity, parentRotation * Quaternion.LookRotation(normal2, tangent2) * Quaternion.Inverse(parentRotation * Quaternion.LookRotation(referenceLookDir, referenceUpDir)), amount) * originalRotation * relativeWorldSpaceDifference;
  }

  public static GameObject DebugPrimitive(
    PrimitiveType primitiveType,
    Vector3 scale,
    Color color,
    Vector3 pivotOffset,
    Transform parent = null)
  {
    GameObject gameObject = null;
    Material material = new Material(Shader.Find("Transparent/Diffuse"));
    material.color = color;
    GameObject primitive = GameObject.CreatePrimitive(primitiveType);
    primitive.GetComponent<Collider>().enabled = false;
    primitive.GetComponent<Renderer>().material = material;
    primitive.transform.localScale = scale;
    primitive.name = "Debug" + primitive.name;
    if (pivotOffset != Vector3.zero)
    {
      gameObject = new GameObject(primitive.name);
      primitive.name = primitive.name.Replace("Debug", "");
      primitive.transform.parent = gameObject.transform;
      primitive.transform.localPosition = pivotOffset;
    }
    if (parent != null)
    {
      if (gameObject == null)
      {
        primitive.transform.parent = parent;
        primitive.transform.localPosition = Vector3.zero;
      }
      else
      {
        gameObject.transform.parent = parent;
        gameObject.transform.localPosition = Vector3.zero;
      }
    }
    return !(gameObject != null) ? primitive : gameObject;
  }

  public static GameObject DebugPointer(Transform parent = null) => DebugPrimitive(PrimitiveType.Sphere, new Vector3(0.01f, 0.01f, 3f), new Color(1f, 1f, 0.0f, 0.75f), Vector3.forward, parent);

  public static GameObject DebugBall(Transform parent = null) => DebugPrimitive(PrimitiveType.Sphere, Vector3.one * 0.25f, new Color(1f, 0.0f, 0.0f, 0.5f), Vector3.zero, parent);
}
