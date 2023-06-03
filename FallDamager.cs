// Decompiled with JetBrains decompiler
// Type: FallDamager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FallDamager : SRBehaviour, EventHandlerRegistrable
{
  public float minImpactForDamage = 0.2f;
  public float damagePerImpact = 300f;
  private vp_FPPlayerEventHandler playerEvents;
  private Damageable damageable;

  public void Awake()
  {
    playerEvents = GetComponentInChildren<vp_FPPlayerEventHandler>();
    damageable = GetInterfaceComponent<Damageable>();
  }

  protected virtual void OnEnable()
  {
    if (!(playerEvents != null))
      return;
    Register(playerEvents);
  }

  protected virtual void OnDisable()
  {
    if (!(playerEvents != null))
      return;
    Unregister(playerEvents);
  }

  public virtual void OnMessage_FallImpact(float val)
  {
    if (val <= (double) minImpactForDamage || !damageable.Damage(Mathf.RoundToInt((val - minImpactForDamage) * damagePerImpact), null))
      return;
    DeathHandler.Kill(gameObject, DeathHandler.Source.FALL_DAMAGE, null, "FallDamager.OnMessage_FallImpact");
  }

  public void Register(vp_EventHandler eventHandler) => eventHandler.RegisterMessage<float>("FallImpact", OnMessage_FallImpact);

  public void Unregister(vp_EventHandler eventHandler) => eventHandler.UnregisterMessage<float>("FallImpact", OnMessage_FallImpact);
}
