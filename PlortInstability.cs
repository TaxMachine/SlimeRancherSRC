// Decompiled with JetBrains decompiler
// Type: PlortInstability
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlortInstability : SRBehaviour
{
  public float lifetimeHours = 0.5f;
  public float explodePower = 400f;
  public float explodeRadius = 7f;
  public float minPlayerDamage = 10f;
  public float maxPlayerDamage = 30f;
  public GameObject explodeFX;
  private double destroyTime;
  private TimeDirector timeDir;

  public void Awake()
  {
    if (!SRSingleton<SceneContext>.Instance.ModDirector.PlortsUnstable())
      Destroyer.Destroy(this, "PlortInstability.Awake");
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    destroyTime = timeDir.HoursFromNowOrStart(lifetimeHours);
  }

  public void Update()
  {
    if (!timeDir.HasReached(destroyTime))
      return;
    Instantiate(explodeFX, transform.position, transform.rotation);
    Destroyer.DestroyActor(gameObject, "PlortInstability.Update");
    PhysicsUtil.Explode(gameObject, explodeRadius, explodePower, minPlayerDamage, maxPlayerDamage);
  }
}
