// Decompiled with JetBrains decompiler
// Type: DamagePlayerOnTouch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DamagePlayerOnTouch : SRBehaviour, ControllerCollisionListener
{
  public int damagePerTouch = 10;
  public float repeatTime = 1f;
  private bool blocked;
  private float nextTime;
  private const float INIT_NO_DAMAGE_WINDOW = 0.1f;

  public void Awake() => ResetDamageAmnesty();

  public void ResetDamageAmnesty() => nextTime = Time.time + 0.1f;

  public void OnControllerCollision(GameObject gameObj)
  {
    if (Time.time < (double) nextTime)
      return;
    TryToDamage(gameObj);
  }

  public void OnCollisionEnter(Collision col)
  {
    if (Time.time < (double) nextTime || !(col.gameObject == SRSingleton<SceneContext>.Instance.Player))
      return;
    TryToDamage(col.gameObject);
  }

  public void SetBlocked(bool blocked) => this.blocked = blocked;

  private void TryToDamage(GameObject gameObj)
  {
    if (!blocked && gameObj.GetInterfaceComponent<Damageable>().Damage(damagePerTouch, gameObject))
      DeathHandler.Kill(gameObj, DeathHandler.Source.SLIME_DAMAGE_PLAYER_ON_TOUCH, gameObject, "DamagePlayerOnTouch.TryToDamage");
    nextTime = Time.time + repeatTime;
  }
}
