// Decompiled with JetBrains decompiler
// Type: DroneAnimatorStateLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DroneAnimatorStateLock : StateMachineBehaviour
{
  private int parameter;

  public void Awake() => parameter = Animator.StringToHash("TRANSITION_LOCK");

  public override void OnStateEnter(Animator animator, AnimatorStateInfo state, int layerIndex) => animator.SetBool(parameter, false);

  public override void OnStateExit(Animator animator, AnimatorStateInfo state, int layerIndex) => animator.SetBool(parameter, true);
}
