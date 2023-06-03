// Decompiled with JetBrains decompiler
// Type: BaseStateManager`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateManager<TState, VComp> : StateManager
  where TState : BaseState
  where VComp : vp_Component
{
  protected VComp managedComponent;
  protected TState[] states;
  protected Dictionary<string, int> stateNameIndex = new Dictionary<string, int>();

  public BaseStateManager(VComp managedComponent) => this.managedComponent = managedComponent;

  protected void AddState(TState state, int index)
  {
    states[index] = state;
    stateNameIndex.Add(state.name, index);
  }

  public void SetState(string stateName, bool setEnabled = true)
  {
    int index = -1;
    if (!stateNameIndex.TryGetValue(stateName, out index))
      return;
    if (index == states.Length - 1 && !setEnabled)
    {
      Debug.LogWarning("Warning: The 'Default' state cannot be disabled.");
    }
    else
    {
      states[index].Enabled = setEnabled;
      ApplyStates();
    }
  }

  public void Reset()
  {
    for (int index = 0; index < states.Length - 1; ++index)
      states[index].Enabled = false;
    states[states.Length - 1].Enabled = true;
    ApplyStates();
  }

  public void ApplyStates()
  {
    int num = states.Length - 1;
    for (int index = states.Length - 1; index >= 0; --index)
    {
      TState state = states[index];
      if (state.Enabled || index == num)
        ApplyState(state);
    }
  }

  public void CombineStates() => ApplyStates();

  public bool IsEnabled(string stateName)
  {
    int index = -1;
    return stateNameIndex.TryGetValue(stateName, out index) && states[index].Enabled;
  }

  public abstract void ApplyState(TState state);
}
