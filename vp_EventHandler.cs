// Decompiled with JetBrains decompiler
// Type: vp_EventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class vp_EventHandler : MonoBehaviour
{
  protected bool m_Initialized;
  protected Dictionary<string, vp_Event> m_HandlerEvents = new Dictionary<string, vp_Event>();
  protected Queue<EventHandlerRegistrable> m_PendingRegistrants = new Queue<EventHandlerRegistrable>();
  public vp_EventHandlerType EventHandlerType;

  protected virtual void Awake()
  {
    EventHandlerType = vp_EventHandlerType.EventHandler;
    m_Initialized = true;
    while (m_PendingRegistrants.Count > 0)
      m_PendingRegistrants.Dequeue()?.Register(this);
  }

  private T GetEvent<T>(string name) where T : vp_Event
  {
    vp_Event vpEvent = null;
    if (!m_HandlerEvents.TryGetValue(name, out vpEvent))
      throw new Exception("Failed to find event " + name);
    return vpEvent is T obj ? obj : throw new Exception(string.Format("Expected event {0} to be of type {1} but was {2}", name, typeof (T), vpEvent.GetType()));
  }

  public void RegisterMessage(string name, vp_Message.Sender onMessage)
  {
    vp_Message vpMessage = GetEvent<vp_Message>(name);
    if (onMessage == null)
      return;
    vpMessage.Send += onMessage;
  }

  public void RegisterMessage<T>(string name, vp_Message<T>.Sender<T> onMessage)
  {
    vp_Message<T> vpMessage = GetEvent<vp_Message<T>>(name);
    if (onMessage == null)
      return;
    vpMessage.Send += onMessage;
  }

  public void RegisterMessage<T, V>(string name, vp_Message<T, V>.Sender<T, V> onMessage)
  {
    vp_Message<T, V> vpMessage = GetEvent<vp_Message<T, V>>(name);
    if (onMessage == null)
      return;
    vpMessage.Send += onMessage;
  }

  public void UnregisterMessage(string name, vp_Message.Sender onMessage)
  {
    vp_Message vpMessage = GetEvent<vp_Message>(name);
    if (onMessage == null)
      return;
    vpMessage.Send -= onMessage;
  }

  public void UnregisterMessage<T>(string name, vp_Message<T>.Sender<T> onMessage)
  {
    vp_Message<T> vpMessage = GetEvent<vp_Message<T>>(name);
    if (onMessage == null)
      return;
    vpMessage.Send -= onMessage;
  }

  public void UnregisterMessage<T, V>(string name, vp_Message<T, V>.Sender<T, V> onMessage)
  {
    vp_Message<T, V> vpMessage = GetEvent<vp_Message<T, V>>(name);
    if (onMessage == null)
      return;
    vpMessage.Send -= onMessage;
  }

  public void RegisterActivity(
    string name,
    vp_Activity.Callback onStart,
    vp_Activity.Callback onStop,
    vp_Activity.Condition canStart,
    vp_Activity.Condition canStop,
    vp_Activity.Callback onFailStart,
    vp_Activity.Callback onFailStop)
  {
    vp_Activity vpActivity = GetEvent<vp_Activity>(name);
    if (onStart != null)
      vpActivity.StartCallbacks += onStart;
    if (onStop != null)
      vpActivity.StopCallbacks += onStop;
    if (canStart != null)
      vpActivity.StartConditions += canStart;
    if (canStop != null)
      vpActivity.StopConditions += canStop;
    if (onFailStart != null)
      vpActivity.FailStartCallbacks += onFailStart;
    if (onFailStop == null)
      return;
    vpActivity.FailStopCallbacks += onFailStart;
  }

  public void RegisterActivity<T>(
    string name,
    vp_Activity.Callback onStart,
    vp_Activity.Callback onStop,
    vp_Activity.Condition canStart,
    vp_Activity.Condition canStop,
    vp_Activity.Callback onFailStart,
    vp_Activity.Callback onFailStop)
  {
    vp_Activity<T> vpActivity1 = GetEvent<vp_Activity<T>>(name);
    if (onStart != null)
    {
      vp_Activity<T> vpActivity2 = vpActivity1;
      vpActivity2.StartCallbacks = vpActivity2.StartCallbacks + onStart;
    }
    if (onStop != null)
    {
      vp_Activity<T> vpActivity3 = vpActivity1;
      vpActivity3.StopCallbacks = vpActivity3.StopCallbacks + onStop;
    }
    if (canStart != null)
    {
      vp_Activity<T> vpActivity4 = vpActivity1;
      vpActivity4.StartConditions = vpActivity4.StartConditions + canStart;
    }
    if (canStop != null)
    {
      vp_Activity<T> vpActivity5 = vpActivity1;
      vpActivity5.StopConditions = vpActivity5.StopConditions + canStop;
    }
    if (onFailStart != null)
    {
      vp_Activity<T> vpActivity6 = vpActivity1;
      vpActivity6.FailStartCallbacks = vpActivity6.FailStartCallbacks + onFailStart;
    }
    if (onFailStop == null)
      return;
    vp_Activity<T> vpActivity7 = vpActivity1;
    vpActivity7.FailStopCallbacks = vpActivity7.FailStopCallbacks + onFailStart;
  }

  public void UnregisterActivity(
    string name,
    vp_Activity.Callback onStart,
    vp_Activity.Callback onStop,
    vp_Activity.Condition canStart,
    vp_Activity.Condition canStop,
    vp_Activity.Callback onFailStart,
    vp_Activity.Callback onFailStop)
  {
    vp_Activity vpActivity = GetEvent<vp_Activity>(name);
    if (onStart != null)
      vpActivity.StartCallbacks -= onStart;
    if (onStop != null)
      vpActivity.StopCallbacks -= onStop;
    if (canStart != null)
      vpActivity.StartConditions -= canStart;
    if (canStop != null)
      vpActivity.StopConditions -= canStop;
    if (onFailStart != null)
      vpActivity.FailStartCallbacks -= onFailStart;
    if (onFailStop == null)
      return;
    vpActivity.FailStopCallbacks -= onFailStart;
  }

  public void UnregisterActivity<T>(
    string name,
    vp_Activity.Callback onStart,
    vp_Activity.Callback onStop,
    vp_Activity.Condition canStart,
    vp_Activity.Condition canStop,
    vp_Activity.Callback onFailStart,
    vp_Activity.Callback onFailStop)
  {
    vp_Activity<T> vpActivity1 = GetEvent<vp_Activity<T>>(name);
    if (onStart != null)
    {
      vp_Activity<T> vpActivity2 = vpActivity1;
      vpActivity2.StartCallbacks = vpActivity2.StartCallbacks - onStart;
    }
    if (onStop != null)
    {
      vp_Activity<T> vpActivity3 = vpActivity1;
      vpActivity3.StopCallbacks = vpActivity3.StopCallbacks - onStop;
    }
    if (canStart != null)
    {
      vp_Activity<T> vpActivity4 = vpActivity1;
      vpActivity4.StartConditions = vpActivity4.StartConditions - canStart;
    }
    if (canStop != null)
    {
      vp_Activity<T> vpActivity5 = vpActivity1;
      vpActivity5.StopConditions = vpActivity5.StopConditions - canStop;
    }
    if (onFailStart != null)
    {
      vp_Activity<T> vpActivity6 = vpActivity1;
      vpActivity6.FailStartCallbacks = vpActivity6.FailStartCallbacks - onFailStart;
    }
    if (onFailStop == null)
      return;
    vp_Activity<T> vpActivity7 = vpActivity1;
    vpActivity7.FailStopCallbacks = vpActivity7.FailStopCallbacks - onFailStart;
  }

  public void RegisterAttempt(string name, vp_Attempt.Tryer onAttempt)
  {
    vp_Attempt vpAttempt = GetEvent<vp_Attempt>(name);
    if (onAttempt == null)
      return;
    vpAttempt.Try = onAttempt;
  }

  public void RegisterAttempt<T>(string name, vp_Attempt<T>.Tryer<T> onAttempt)
  {
    vp_Attempt<T> vpAttempt = GetEvent<vp_Attempt<T>>(name);
    if (onAttempt == null)
      return;
    vpAttempt.Try = onAttempt;
  }

  public void UnregisterAttempt<T>(string name, vp_Attempt<T>.Tryer<T> onAttempt)
  {
    vp_Attempt<T> vpAttempt = GetEvent<vp_Attempt<T>>(name);
    if (onAttempt == null)
      return;
    vpAttempt.Try = vpAttempt.AttemptAlwaysOK;
  }

  public void UnregisterAttempt(string name, vp_Attempt.Tryer onAttempt)
  {
    vp_Attempt vpAttempt = GetEvent<vp_Attempt>(name);
    if (onAttempt == null)
      return;
    vpAttempt.Try = vp_Attempt.AlwaysOK;
  }

  public void RegisterValue<T>(
    string name,
    vp_Value<T>.Getter<T> onValueGet,
    vp_Value<T>.Setter<T> onValueSet)
  {
    vp_Value<T> vpValue = GetEvent<vp_Value<T>>(name);
    if (onValueGet != null)
      vpValue.Get = onValueGet;
    if (onValueSet == null)
      return;
    vpValue.Set = onValueSet;
  }

  public void UnregisterValue<T>(
    string name,
    vp_Value<T>.Getter<T> onValueGet,
    vp_Value<T>.Setter<T> onValueSet)
  {
    vp_Value<T> vpValue = GetEvent<vp_Value<T>>(name);
    if (onValueGet != null)
      vpValue.Get = vpValue.GetEmpty;
    if (onValueSet == null)
      return;
    vpValue.Set = vpValue.SetEmpty;
  }
}
