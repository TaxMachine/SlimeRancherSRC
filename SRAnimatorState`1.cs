// Decompiled with JetBrains decompiler
// Type: SRAnimatorState`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class SRAnimatorState<T> : StateMachineBehaviour where T : SRAnimator
{
  private T wrapper;

  protected T GetAnimatorWrapper(Animator animator)
  {
    if (wrapper == null)
      wrapper = animator.gameObject.GetComponent<T>();
    return wrapper;
  }
}
