// Decompiled with JetBrains decompiler
// Type: vp_State
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class vp_State
{
  public vp_StateManager StateManager;
  public string TypeName;
  public string Name;
  public TextAsset TextAsset;
  public vp_ComponentPreset Preset;
  public List<int> StatesToBlock;
  [NonSerialized]
  protected bool m_Enabled;
  [NonSerialized]
  protected List<vp_State> m_CurrentlyBlockedBy;

  public vp_State(string typeName, string name = "Untitled", string path = null, TextAsset asset = null)
  {
    TypeName = typeName;
    Name = name;
    TextAsset = asset;
  }

  public bool Enabled
  {
    get => m_Enabled;
    set
    {
      m_Enabled = value;
      if (StateManager == null)
        return;
      if (m_Enabled)
        StateManager.ImposeBlockingList(this);
      else
        StateManager.RelaxBlockingList(this);
    }
  }

  public bool Blocked => CurrentlyBlockedBy.Count > 0;

  public int BlockCount => CurrentlyBlockedBy.Count;

  protected List<vp_State> CurrentlyBlockedBy
  {
    get
    {
      if (m_CurrentlyBlockedBy == null)
        m_CurrentlyBlockedBy = new List<vp_State>();
      return m_CurrentlyBlockedBy;
    }
  }

  public void AddBlocker(vp_State blocker)
  {
    if (CurrentlyBlockedBy.Contains(blocker))
      return;
    CurrentlyBlockedBy.Add(blocker);
  }

  public void RemoveBlocker(vp_State blocker)
  {
    if (!CurrentlyBlockedBy.Contains(blocker))
      return;
    CurrentlyBlockedBy.Remove(blocker);
  }
}
