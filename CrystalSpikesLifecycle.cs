// Decompiled with JetBrains decompiler
// Type: CrystalSpikesLifecycle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CrystalSpikesLifecycle : SRBehaviour, LiquidConsumer
{
  [Tooltip("Lifetime of spikes in hours")]
  public float lifetime = 0.5f;
  public int damagePerHit = 10;
  public GameObject spawnFX;
  public GameObject destroyFX;
  private double destroyAt;
  private TimeDirector timeDir;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    destroyAt = timeDir.HoursFromNowOrStart(lifetime);
    if (!(spawnFX != null))
      return;
    SpawnAndPlayFX(spawnFX, transform.position, transform.rotation);
  }

  public void Update()
  {
    if (!timeDir.HasReached(destroyAt))
      return;
    bool flag = timeDir.HasReached(destroyAt + 3600.0);
    if (destroyFX != null && !flag)
      SpawnAndPlayFX(destroyFX, transform.position, transform.rotation);
    Destroyer.Destroy(gameObject, "CrystalSpikesLifecycle.Update");
  }

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger)
      return;
    Identifiable component = col.gameObject.GetComponent<Identifiable>();
    if (!(component != null) || component.id != Identifiable.Id.PLAYER || !col.gameObject.GetComponent<Damageable>().Damage(damagePerHit, gameObject))
      return;
    DeathHandler.Kill(col.gameObject, DeathHandler.Source.SLIME_CRYSTAL_SPIKES, gameObject, "CrystalSpikesLifecycle.OnTriggerEnter");
  }

  public void AddLiquid(Identifiable.Id liquidId, float units)
  {
    if (!Identifiable.IsWater(liquidId))
      return;
    SpawnAndPlayFX(destroyFX, transform.position, transform.rotation);
    Destroyer.Destroy(gameObject, "CrystalSpikesLifecycle.AddLiquid");
  }
}
