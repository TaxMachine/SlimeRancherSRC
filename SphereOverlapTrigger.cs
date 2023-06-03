// Decompiled with JetBrains decompiler
// Type: SphereOverlapTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SphereOverlapTrigger : MonoBehaviour
{
  public OnSphereOverlap onSphereOverlap;
  private List<Collider> colliders = new List<Collider>();
  private bool hasDoneOneFixedUpdate;

  public void OnTriggerEnter(Collider col) => colliders.Add(col);

  public void FixedUpdate() => hasDoneOneFixedUpdate = true;

  public void LateUpdate()
  {
    if (!hasDoneOneFixedUpdate)
      return;
    try
    {
      if (onSphereOverlap == null)
        return;
      onSphereOverlap(colliders.Where(c => c != null));
    }
    finally
    {
      onSphereOverlap = null;
      Destroyer.Destroy(gameObject, "SphereOverlapTrigger.LateUpdate");
    }
  }

  public static GameObject CreateGameObject(
    Vector3 center,
    float radius,
    OnSphereOverlap onOverlap,
    int layer = 0)
  {
    GameObject gameObject = new GameObject(nameof (SphereOverlapTrigger));
    gameObject.transform.position = center;
    gameObject.layer = layer;
    gameObject.AddComponent<SphereOverlapTrigger>().onSphereOverlap += onOverlap;
    SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
    sphereCollider.radius = radius;
    sphereCollider.isTrigger = true;
    gameObject.AddComponent<Rigidbody>().isKinematic = true;
    return gameObject;
  }

  public delegate void OnSphereOverlap(IEnumerable<Collider> colliders);
}
