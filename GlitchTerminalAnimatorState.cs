// Decompiled with JetBrains decompiler
// Type: GlitchTerminalAnimatorState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GlitchTerminalAnimatorState : SRAnimatorState<GlitchTerminalAnimator>
{
  [Tooltip("State identifier.")]
  public Id id;

  public override void OnStateEnter(Animator animator, AnimatorStateInfo state, int layerIndex)
  {
    base.OnStateEnter(animator, state, layerIndex);
    GetAnimatorWrapper(animator).OnStateEnter(id);
  }

  public enum Id
  {
    NONE,
    SLEEP,
    BOOT_UP,
    IDLE,
  }
}
