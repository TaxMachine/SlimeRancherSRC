// Decompiled with JetBrains decompiler
// Type: vp_StateManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class vp_StateManager
{
  private vp_Component m_Component;
  [NonSerialized]
  private List<vp_State> m_States;
  private Dictionary<string, int> m_StateIds;
  private int m_DefaultId;
  private int m_TargetId;

  public vp_StateManager(vp_Component component, List<vp_State> states)
  {
    m_States = states;
    m_Component = component;
    m_Component.RefreshDefaultState();
    m_StateIds = new Dictionary<string, int>();
    foreach (vp_State state in m_States)
    {
      state.StateManager = this;
      if (!m_StateIds.ContainsKey(state.Name))
      {
        m_StateIds.Add(state.Name, m_States.IndexOf(state));
      }
      else
      {
        Debug.LogWarning("Warning: " + m_Component.GetType() + " on '" + m_Component.name + "' has more than one state named: '" + state.Name + "'. Only the topmost one will be used.");
        m_States[m_DefaultId].StatesToBlock.Add(m_States.IndexOf(state));
      }
      if (state.Preset == null)
        state.Preset = new vp_ComponentPreset();
      if (state.TextAsset != null)
        state.Preset.LoadFromTextAsset(state.TextAsset);
    }
    m_DefaultId = m_States.Count - 1;
  }

  public void ImposeBlockingList(vp_State blocker)
  {
    if (blocker == null || blocker.StatesToBlock == null || m_States == null)
      return;
    foreach (int index in blocker.StatesToBlock)
      m_States[index].AddBlocker(blocker);
  }

  public void RelaxBlockingList(vp_State blocker)
  {
    if (blocker == null || blocker.StatesToBlock == null || m_States == null)
      return;
    foreach (int index in blocker.StatesToBlock)
      m_States[index].RemoveBlocker(blocker);
  }

  public void SetState(string state, bool setEnabled = true)
  {
    if (!AppPlaying() || !m_StateIds.TryGetValue(state, out m_TargetId))
      return;
    if (m_TargetId == m_DefaultId && !setEnabled)
    {
      Debug.LogWarning("Warning: The 'Default' state cannot be disabled.");
    }
    else
    {
      m_States[m_TargetId].Enabled = setEnabled;
      CombineStates();
      m_Component.Refresh();
    }
  }

  public void Reset()
  {
    if (!AppPlaying())
      return;
    foreach (vp_State state in m_States)
      state.Enabled = false;
    m_States[m_DefaultId].Enabled = true;
    m_TargetId = m_DefaultId;
    CombineStates();
  }

  public void CombineStates()
  {
    for (int index = m_States.Count - 1; index > -1; --index)
    {
      if ((index == m_DefaultId || m_States[index].Enabled && !m_States[index].Blocked && !(m_States[index].TextAsset == null)) && m_States[index].Preset != null && !(m_States[index].Preset.ComponentType == null))
        vp_ComponentPreset.Apply(m_Component, m_States[index].Preset);
    }
  }

  public bool IsEnabled(string state) => AppPlaying() && m_StateIds.TryGetValue(state, out m_TargetId) && m_States[m_TargetId].Enabled;

  private static bool AppPlaying() => true;
}
