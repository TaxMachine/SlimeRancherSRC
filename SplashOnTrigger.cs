// Decompiled with JetBrains decompiler
// Type: SplashOnTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SplashOnTrigger : SRBehaviour
{
  public GameObject splashFX;
  public GameObject playerSplashFX;
  private Collider[] splashColliders;
  private const float SPLASH_THRESHOLD = 4f;

  public void Awake() => splashColliders = GetComponents<Collider>();

  private void OnTriggerEnter(Collider collider)
  {
    if (PhysicsUtil.IsPlayerMainCollider(collider))
    {
      SpawnAndPlayFX(playerSplashFX, collider);
    }
    else
    {
      if (collider.isTrigger)
        return;
      Identifiable component1 = collider.gameObject.GetComponent<Identifiable>();
      if (!(component1 == null) && !component1.isPhysicsInitialized)
        return;
      Rigidbody component2 = collider.GetComponent<Rigidbody>();
      if (!(component2 != null) || component2.isKinematic || Mathf.Abs(component2.velocity.y) < 4.0)
        return;
      SpawnAndPlayFX(splashFX, collider);
    }
  }

  private void SpawnAndPlayFX(GameObject prefab, Collider collider)
  {
    Ray ray = new Ray(collider.gameObject.transform.position, Vector3.down);
    float num = float.PositiveInfinity;
    Vector3 position = collider.gameObject.transform.position;
    foreach (Collider splashCollider in splashColliders)
    {
      RaycastHit hitInfo;
      if (splashCollider.Raycast(ray, out hitInfo, 2f) && hitInfo.distance < (double) num)
      {
        num = hitInfo.distance;
        position = hitInfo.point;
      }
    }
    SRBehaviour.SpawnAndPlayFX(prefab, position, Quaternion.identity);
  }
}
