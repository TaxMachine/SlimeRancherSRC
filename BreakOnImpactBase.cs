// Decompiled with JetBrains decompiler
// Type: BreakOnImpactBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public abstract class BreakOnImpactBase : SRBehaviour
{
  public GameObject breakFX;
  private const float COLLISION_THRESHOLD = 14f;
  private Rigidbody body;
  private bool breaking;

  public virtual void Awake() => body = GetComponent<Rigidbody>();

  public void OnCollisionEnter(Collision col)
  {
    if (col.collider.isTrigger || body.isKinematic)
      return;
    float a = 0.0f;
    foreach (ContactPoint contact in col.contacts)
      a = Mathf.Max(a, Vector3.Dot(contact.normal, col.relativeVelocity));
    if (a <= 14.0)
      return;
    BreakOpen();
  }

  private void BreakOpen()
  {
    if (breaking)
      return;
    breaking = true;
    SpawnAndPlayFX(breakFX, gameObject.transform.position, gameObject.transform.rotation);
    Destroyer.DestroyActor(gameObject, "BreakOnImpact.BreakOpen");
    RegionRegistry.RegionSetId setId = GetComponent<RegionMember>().setId;
    foreach (GameObject rewardPrefab in GetRewardPrefabs())
    {
      Vector3 vector3 = transform.position + Random.insideUnitSphere;
      int num = (int) setId;
      Vector3 position = vector3;
      Quaternion identity = Quaternion.identity;
      Rigidbody component = InstantiateActor(rewardPrefab, (RegionRegistry.RegionSetId) num, position, identity, true).GetComponent<Rigidbody>();
      if (component != null)
      {
        component.AddTorque(Random.insideUnitSphere);
        component.AddForce(Random.insideUnitSphere);
      }
    }
  }

  protected abstract IEnumerable<GameObject> GetRewardPrefabs();
}
