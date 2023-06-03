// Decompiled with JetBrains decompiler
// Type: BoomGordoEat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class BoomGordoEat : GordoEat, BoomMaterialAnimator.BoomMaterialInformer
{
  public float explodePower = 600f;
  public float explodeRadius = 7f;
  public float minPlayerDamage = 15f;
  public float maxPlayerDamage = 45f;

  protected override void WillStartBurst()
  {
    base.WillStartBurst();
    GetComponentsInChildren<ExplodeIndicatorMarker>(true)[0].SetActive(true);
  }

  protected override void DidCompleteBurst()
  {
    base.DidCompleteBurst();
    PhysicsUtil.Explode(gameObject, explodeRadius, explodePower, minPlayerDamage, maxPlayerDamage, (bool) (Object) gameObject);
    GetComponentsInChildren<ExplodeIndicatorMarker>(true)[0].SetActive(false);
  }

  public float GetReadiness() => Mathf.Lerp(0.2f, 1f, GetPercentageFed());

  public float GetRecoveriness() => 0.0f;
}
