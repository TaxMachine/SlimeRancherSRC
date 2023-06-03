// Decompiled with JetBrains decompiler
// Type: DamagePlayerOnTouch_Trigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DamagePlayerOnTouch_Trigger : RegisteredActorBehaviour, RegistryUpdateable
{
  [Tooltip("Amount of damage applied each tick.")]
  public int damagePerTick;
  [Tooltip("Amount of time in between ticks. (in-game minutes)")]
  public float cooldownPerTick;
  private TimeDirector timeDirector;
  protected double nextTime;
  protected GameObject damageGameObject;

  public void Awake() => timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;

  public void OnTriggerEnter(Collider collider)
  {
    if (!(damageGameObject == null) || !PhysicsUtil.IsPlayerMainCollider(collider))
      return;
    damageGameObject = collider.gameObject;
  }

  public void OnTriggerExit(Collider collider)
  {
    if (!(damageGameObject == collider.gameObject))
      return;
    damageGameObject = null;
  }

  public virtual void RegistryUpdate()
  {
    if (!(damageGameObject != null) || !timeDirector.HasReached(nextTime))
      return;
    if (damageGameObject.GetInterfaceComponent<Damageable>().Damage(damagePerTick, gameObject))
      DeathHandler.Kill(damageGameObject, DeathHandler.Source.SLIME_DAMAGE_PLAYER_ON_TOUCH, gameObject, "DamagePlayerOnTouch_Trigger.RegistryUpdate");
    nextTime = timeDirector.HoursFromNow(cooldownPerTick * 0.0166666675f);
  }
}
