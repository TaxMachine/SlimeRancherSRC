// Decompiled with JetBrains decompiler
// Type: AmmoModeTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AmmoModeTrigger : MonoBehaviour
{
  [Tooltip("Ammo mode to set on entering the trigger.")]
  public PlayerState.AmmoMode onEnter;
  [Tooltip("Ammo mode to set on exiting the trigger.")]
  public PlayerState.AmmoMode onExit;
  private PlayerState playerState;

  public void Awake() => playerState = SRSingleton<SceneContext>.Instance.PlayerState;

  public void OnTriggerEnter(Collider collider)
  {
    if (!PhysicsUtil.IsPlayerMainCollider(collider))
      return;
    playerState.SetAmmoMode(onEnter);
  }

  public void OnTriggerExit(Collider collider)
  {
    if (!PhysicsUtil.IsPlayerMainCollider(collider))
      return;
    playerState.SetAmmoMode(onExit);
  }
}
