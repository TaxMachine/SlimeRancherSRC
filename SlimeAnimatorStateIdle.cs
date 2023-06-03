// Decompiled with JetBrains decompiler
// Type: SlimeAnimatorStateIdle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlimeAnimatorStateIdle : StateMachineBehaviour
{
  private bool? isCurrentState;

  public bool IsCurrentState
  {
    get
    {
      bool? isCurrentState = this.isCurrentState;
      bool flag = true;
      return isCurrentState.GetValueOrDefault() == flag & isCurrentState.HasValue;
    }
  }

  public bool IsInitialized => isCurrentState.HasValue;

  public override void OnStateEnter(Animator animator, AnimatorStateInfo state, int layerIndex)
  {
    base.OnStateEnter(animator, state, layerIndex);
    isCurrentState = new bool?(true);
  }

  public override void OnStateExit(Animator animator, AnimatorStateInfo state, int layerIndex)
  {
    base.OnStateExit(animator, state, layerIndex);
    isCurrentState = new bool?(false);
  }
}
