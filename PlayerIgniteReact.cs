// Decompiled with JetBrains decompiler
// Type: PlayerIgniteReact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlayerIgniteReact : MonoBehaviour, Ignitable
{
  public int damagePerIgnite = 10;
  public float repeatTime = 1f;
  private PlayerDamageable damageable;
  private double nextTime;

  public void Awake() => damageable = GetComponent<PlayerDamageable>();

  public void Ignite(GameObject igniter)
  {
    if (Time.time < nextTime)
      return;
    TryToDamage(igniter);
  }

  private void TryToDamage(GameObject igniter)
  {
    if (damageable.Damage(damagePerIgnite, gameObject))
      DeathHandler.Kill(gameObject, DeathHandler.Source.SLIME_IGNITE, igniter, "PlayerIgniteReact.TryToDamage");
    nextTime = Time.time + (double) repeatTime;
  }
}
