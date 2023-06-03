// Decompiled with JetBrains decompiler
// Type: vp_StateEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public abstract class vp_StateEventHandler : vp_EventHandler
{
  private List<vp_Component> m_StateTargets = new List<vp_Component>();

  protected override void Awake()
  {
    EventHandlerType = vp_EventHandlerType.StateEventHandler;
    base.Awake();
    foreach (vp_Component componentsInChild in transform.root.GetComponentsInChildren<vp_Component>(true))
    {
      if (componentsInChild.Parent == null || componentsInChild.Parent.GetComponent<vp_Component>() == null)
        m_StateTargets.Add(componentsInChild);
    }
  }

  protected void BindStateToActivity(vp_Activity a)
  {
    BindStateToActivityOnStart(a);
    BindStateToActivityOnStop(a);
  }

  protected void BindStateToActivityOnStart(vp_Activity a)
  {
    if (!ActivityInitialized(a))
      return;
    string s = a.EventName;
    a.StartCallbacks += () =>
    {
      foreach (vp_Component stateTarget in m_StateTargets)
        stateTarget.SetState(s, recursive: true);
    };
  }

  protected void BindStateToActivityOnStop(vp_Activity a)
  {
    if (!ActivityInitialized(a))
      return;
    string s = a.EventName;
    a.StopCallbacks += () =>
    {
      foreach (vp_Component stateTarget in m_StateTargets)
        stateTarget.SetState(s, false, true);
    };
  }

  public void RefreshActivityStates()
  {
    foreach (vp_Event vpEvent in m_HandlerEvents.Values)
    {
      if (vpEvent.EventType == vp_EventType.Activity)
      {
        foreach (vp_Component stateTarget in m_StateTargets)
          stateTarget.SetState(vpEvent.EventName, ((vp_Activity) vpEvent).Active, true);
      }
    }
  }

  public void ResetActivityStates()
  {
    foreach (vp_Component stateTarget in m_StateTargets)
      stateTarget.ResetState();
  }

  public void SetState(string state, bool setActive = true, bool recursive = true, bool includeDisabled = false)
  {
    foreach (vp_Component stateTarget in m_StateTargets)
      stateTarget.SetState(state, setActive, recursive, includeDisabled);
  }

  private bool ActivityInitialized(vp_Activity a)
  {
    if (a == null)
    {
      Debug.LogError("Error: (" + this + ") Activity is null.");
      return false;
    }
    if (!string.IsNullOrEmpty(a.EventName))
      return true;
    Debug.LogError("Error: (" + this + ") Activity not initialized. Make sure the event handler has run its Awake call before binding layers.");
    return false;
  }
}
