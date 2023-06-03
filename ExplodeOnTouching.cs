// Decompiled with JetBrains decompiler
// Type: ExplodeOnTouching
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ExplodeOnTouching : SRBehaviour
{
  public float explodePower = 600f;
  public float explodeRadius = 7f;
  public float minPlayerDamage = 15f;
  public float maxPlayerDamage = 45f;
  public bool ignites;
  public GameObject explodeFX;

  public void OnCollisionEnter(Collision col)
  {
    DestroyOnTouching component = col.gameObject.GetComponent<DestroyOnTouching>();
    if (!(component == null) && component.wateringUnits > 0.0)
      return;
    Explode();
  }

  public void Explode()
  {
    PhysicsUtil.Explode(gameObject, explodeRadius, explodePower, minPlayerDamage, maxPlayerDamage, ignites);
    if (explodeFX != null)
      SpawnAndPlayFX(explodeFX, transform.position, transform.rotation);
    RequestDestroy("ExplodeOnTouching.Explode");
  }
}
