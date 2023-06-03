// Decompiled with JetBrains decompiler
// Type: PhysicsUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsUtil
{
  private const float PLAYER_FORCE_FACTOR = 0.001f;

  public static void Explode(
    GameObject source,
    float radius,
    float power,
    float minPlayerDamage,
    float maxPlayerDamage,
    bool ignites = false)
  {
    HashSet<GameObject> gameObjectSet = new HashSet<GameObject>();
    Vector3 position = source.transform.position;
    LayerMask layerMask = LayerMask -65537;
    foreach (Collider collider in Physics.OverlapSphere(position, radius, layerMask))
    {
      if ((bool) (Object) collider && !collider.isTrigger && collider.GetComponent<Rigidbody>() != null && collider.gameObject != source && !gameObjectSet.Contains(collider.gameObject))
      {
        vp_FPController component = collider.gameObject.GetComponent<vp_FPController>();
        if (component != null)
        {
          Vector3 vector3 = SlimeSubbehaviour.GetGotoPos(component.gameObject) - position;
          float magnitude = vector3.magnitude;
          vector3.Normalize();
          float num = (float) ((1.0 - magnitude / (double) radius) * (1.0 / 1000.0));
          component.AddForce(vector3 * (power * num));
          Damageable interfaceComponent = collider.gameObject.GetInterfaceComponent<Damageable>();
          if (interfaceComponent != null)
          {
            int healthLoss = Mathf.RoundToInt(Mathf.Lerp(minPlayerDamage, maxPlayerDamage, (float) (1.0 - magnitude / (double) radius)));
            if (interfaceComponent.Damage(healthLoss, source))
              DeathHandler.Kill(collider.gameObject, DeathHandler.Source.SLIME_EXPLODE, source, "PhysicsUtil.Explode");
          }
          if (ignites)
          {
            foreach (Ignitable componentsInChild in collider.gameObject.GetComponentsInChildren<Ignitable>())
              componentsInChild.Ignite(source);
          }
        }
        else
          SoftExplosionForce(power, position, radius, collider.GetComponent<Rigidbody>());
        gameObjectSet.Add(collider.gameObject);
      }
    }
  }

  public static int LayerMask { get; }

  public static void SoftExplosionForce(float power, Vector3 pos, float radius, Rigidbody body)
  {
    Vector3 vector3 = body.position - pos;
    float magnitude = vector3.magnitude;
    vector3.Normalize();
    float num = (float) (1.0 - Mathf.Max(2f, magnitude) / (double) radius);
    body.AddForce(vector3 * (power * num * num));
  }

  public static float RadiusOfObject(GameObject obj)
  {
    float a = 0.0f;
    foreach (Collider component in obj.GetComponents<Collider>())
    {
      if (!component.isTrigger)
      {
        if (obj.activeInHierarchy)
        {
          Bounds bounds = component.bounds;
          a = Mathf.Max(a, (float) (0.5 * Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z)));
        }
        else
          a = Mathf.Max(a, CalcRad(component));
      }
    }
    return a;
  }

  private static float CalcRad(Collider col)
  {
    Vector3 lossyScale = col.transform.lossyScale;
    switch (col)
    {
      case SphereCollider _:
        return (float) (((SphereCollider) col).radius * (double) Mathf.Max(lossyScale.x, lossyScale.y, lossyScale.z));
      case BoxCollider _:
        BoxCollider boxCollider = (BoxCollider) col;
        return Mathf.Max(boxCollider.size.x * lossyScale.x, boxCollider.size.y * lossyScale.y, boxCollider.size.z * lossyScale.z) * 0.5f;
      case CapsuleCollider _:
        CapsuleCollider capsuleCollider = (CapsuleCollider) col;
        float num1 = capsuleCollider.direction == 0 ? capsuleCollider.height * 0.5f : capsuleCollider.radius;
        float num2 = capsuleCollider.direction == 1 ? capsuleCollider.height * 0.5f : capsuleCollider.radius;
        float num3 = capsuleCollider.direction == 2 ? capsuleCollider.height * 0.5f : capsuleCollider.radius;
        return Mathf.Max(num1 * lossyScale.x, num2 * lossyScale.y, num3 * lossyScale.z);
      default:
        return 0.0f;
    }
  }

  public static bool IsPlayerMainCollider(Collider collider) => collider.gameObject == SRSingleton<SceneContext>.Instance.Player && collider is CharacterController;

  public static void RestoreFreezeRotationConstraints(GameObject gameObject)
  {
    Rigidbody component = gameObject.GetComponent<Rigidbody>();
    if (!(component != null) || component.constraints == RigidbodyConstraints.None)
      return;
    Vector3 eulerAngles = component.transform.rotation.eulerAngles;
    if ((component.constraints & RigidbodyConstraints.FreezeRotationX) != RigidbodyConstraints.None)
      eulerAngles.x = 0.0f;
    if ((component.constraints & RigidbodyConstraints.FreezeRotationY) != RigidbodyConstraints.None)
      eulerAngles.y = 0.0f;
    if ((component.constraints & RigidbodyConstraints.FreezeRotationZ) != RigidbodyConstraints.None)
      eulerAngles.z = 0.0f;
    component.transform.rotation = Quaternion.Euler(eulerAngles);
  }

  public static void IgnoreCollision(GameObject a, GameObject b, bool ignored = true)
  {
    Collider[] componentsInChildren1 = a.GetComponentsInChildren<Collider>();
    Collider[] componentsInChildren2 = b.GetComponentsInChildren<Collider>();
    foreach (Collider collider1 in componentsInChildren1)
    {
      foreach (Collider collider2 in componentsInChildren2)
        Physics.IgnoreCollision(collider1, collider2, ignored);
    }
  }

  public static void IgnoreCollision(GameObject a, GameObject b, float enableAfter)
  {
    IgnoreCollision(a, b);
    SRSingleton<GameContext>.Instance.StartCoroutine(RestoreCollision(a, b, enableAfter));
  }

  private static IEnumerator RestoreCollision(GameObject a, GameObject b, float enableAfter)
  {
    yield return new WaitForSeconds(enableAfter);
    if (a != null && b != null)
      IgnoreCollision(a, b, false);
  }
}
