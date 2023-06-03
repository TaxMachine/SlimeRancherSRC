// Decompiled with JetBrains decompiler
// Type: GlitchTerminalAnimator_PlayerState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GlitchTerminalAnimator_PlayerState : SRAnimatorState<GlitchTerminalAnimator_Player>
{
  [Tooltip("State identifier.")]
  public Id id;

  public override void OnStateExit(Animator animator, AnimatorStateInfo state, int layerIndex)
  {
    base.OnStateExit(animator, state, layerIndex);
    GetAnimatorWrapper(animator).OnStateExit(id);
  }

  public enum Id
  {
    NONE,
    ENTERING,
    EXITING,
  }
}
