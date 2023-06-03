// Decompiled with JetBrains decompiler
// Type: GlitchTerminalAnimator_BootupCollider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GlitchTerminalAnimator_BootupCollider : SRBehaviour
{
  private GlitchTerminalAnimator animator;

  public void Awake() => animator = GetRequiredComponentInParent<GlitchTerminalAnimator>();

  public void OnTriggerEnter(Collider collider)
  {
    if (!PhysicsUtil.IsPlayerMainCollider(collider) || animator.activator.GetLinkState() <= GlitchTerminalActivator.LinkState.INACTIVE_PROGRESS)
      return;
    animator.animator.SetBool("state_sleeping", false);
    Destroyer.Destroy(gameObject, "GlitchTerminalAnimator_BootupCollider.OnTriggerEnter");
  }
}
