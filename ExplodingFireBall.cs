// Decompiled with JetBrains decompiler
// Type: ExplodingFireBall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ExplodingFireBall : FireBall
{
  public float explodePower = 600f;
  public float explodeRadius = 7f;
  public float minPlayerDamage = 15f;
  public float maxPlayerDamage = 45f;
  public GameObject explodeFX;

  protected override void OnExpire() => Explode();

  public void Explode()
  {
    if (defused)
      return;
    PhysicsUtil.Explode(gameObject, explodeRadius, explodePower, minPlayerDamage, maxPlayerDamage, true);
    if (!(explodeFX != null))
      return;
    SpawnAndPlayFX(explodeFX, transform.position, transform.rotation);
  }
}
