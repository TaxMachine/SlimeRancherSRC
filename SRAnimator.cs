// Decompiled with JetBrains decompiler
// Type: SRAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SRAnimator : SRBehaviour
{
  private List<HibernatingParameter> hibernatingParameters;
  private List<AnimatorStateInfo> hibernatingStates;

  public Animator animator { get; private set; }

  public virtual void Awake() => animator = GetRequiredComponent<Animator>();

  public virtual void OnEnable()
  {
    if (hibernatingParameters != null)
    {
      hibernatingParameters.ForEach(p => p.Restore());
      hibernatingParameters = null;
    }
    if (hibernatingStates == null)
      return;
    for (int index = 0; index < hibernatingStates.Count; ++index)
    {
      AnimatorStateInfo hibernatingState = hibernatingStates[index];
      animator.Play(hibernatingState.shortNameHash, index, hibernatingState.normalizedTime);
    }
    hibernatingStates = null;
  }

  public virtual void OnDisable()
  {
    hibernatingParameters = animator.parameters.Select(p => new HibernatingParameter(animator, p)).ToList();
    hibernatingStates = Enumerable.Range(0, animator.layerCount).Select(ii => animator.GetCurrentAnimatorStateInfo(ii)).ToList();
  }

  private class HibernatingParameter
  {
    public readonly Action Restore;

    public HibernatingParameter(Animator animator, AnimatorControllerParameter parameter)
    {
      switch (parameter.type)
      {
        case AnimatorControllerParameterType.Float:
          float current1 = animator.GetFloat(parameter.nameHash);
          Restore = () => animator.SetFloat(parameter.nameHash, current1);
          break;
        case AnimatorControllerParameterType.Int:
          int current2 = animator.GetInteger(parameter.nameHash);
          Restore = () => animator.SetInteger(parameter.nameHash, current2);
          break;
        case AnimatorControllerParameterType.Bool:
          bool current3 = animator.GetBool(parameter.nameHash);
          Restore = () => animator.SetBool(parameter.nameHash, current3);
          break;
        case AnimatorControllerParameterType.Trigger:
          Restore = () => { };
          break;
        default:
          throw new NotImplementedException(string.Format("Failed to hibernate SRAnimator parameter. [animator={0}; parameter={1}; type={2}]", animator.name, parameter.name, parameter.type));
      }
    }
  }
}
