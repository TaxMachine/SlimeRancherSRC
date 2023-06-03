// Decompiled with JetBrains decompiler
// Type: EchoNoteGordoAnimatorState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EchoNoteGordoAnimatorState : SRAnimatorState<EchoNoteGordoAnimator>
{
  [Tooltip("Animation state identifier.")]
  public Id id;

  public override void OnStateEnter(Animator animator, AnimatorStateInfo state, int layerIndex)
  {
    base.OnStateEnter(animator, state, layerIndex);
    GetAnimatorWrapper(animator).parent.OnAnimationEvent_StateEnter(id);
  }

  public override void OnStateExit(Animator animator, AnimatorStateInfo state, int layerIndex)
  {
    base.OnStateExit(animator, state, layerIndex);
    GetAnimatorWrapper(animator).parent.OnAnimationEvent_StateExit(id);
  }

  public enum Id
  {
    NONE,
    PRE_ACTIVATION,
    ACTIVATION,
  }
}
