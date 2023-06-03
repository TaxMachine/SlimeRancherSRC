// Decompiled with JetBrains decompiler
// Type: SlimeHealth
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlimeHealth : SRBehaviour, Damageable
{
  public int maxHealth = 100;
  private int currHealth;
  public OnDamage onDamage;

  public virtual void Start() => currHealth = maxHealth;

  public int GetCurrHealth() => currHealth;

  public bool Damage(int healthLoss, GameObject source)
  {
    currHealth -= healthLoss;
    if (onDamage != null)
      onDamage(source);
    if (currHealth <= 0)
    {
      currHealth = 0;
      return true;
    }
    SlimeFaceAnimator component = GetComponent<SlimeFaceAnimator>();
    if (component != null)
      component.SetTrigger("triggerWince");
    return false;
  }

  public delegate void OnDamage(GameObject damageSource);
}
