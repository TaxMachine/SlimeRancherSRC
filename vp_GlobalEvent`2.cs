// Decompiled with JetBrains decompiler
// Type: vp_GlobalEvent`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

public static class vp_GlobalEvent<T, U>
{
  private static Hashtable m_Callbacks = vp_GlobalEventInternal.Callbacks;

  public static void Register(string name, vp_GlobalCallback<T, U> callback)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof (name));
    if (callback == null)
      throw new ArgumentNullException(nameof (callback));
    List<vp_GlobalCallback<T, U>> vpGlobalCallbackList = (List<vp_GlobalCallback<T, U>>) m_Callbacks[name];
    if (vpGlobalCallbackList == null)
    {
      vpGlobalCallbackList = new List<vp_GlobalCallback<T, U>>();
      m_Callbacks.Add(name, vpGlobalCallbackList);
    }
    vpGlobalCallbackList.Add(callback);
  }

  public static void Unregister(string name, vp_GlobalCallback<T, U> callback)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof (name));
    if (callback == null)
      throw new ArgumentNullException(nameof (callback));
    ((List<vp_GlobalCallback<T, U>>) m_Callbacks[name] ?? throw vp_GlobalEventInternal.ShowUnregisterException(name)).Remove(callback);
  }

  public static void Send(string name, T arg1, U arg2) => Send(name, arg1, arg2, vp_GlobalEventMode.DONT_REQUIRE_LISTENER);

  public static void Send(string name, T arg1, U arg2, vp_GlobalEventMode mode)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof (name));
    if (arg1 == null)
      throw new ArgumentNullException(nameof (arg1));
    if (arg2 == null)
      throw new ArgumentNullException(nameof (arg2));
    List<vp_GlobalCallback<T, U>> callback = (List<vp_GlobalCallback<T, U>>) m_Callbacks[name];
    if (callback != null)
    {
      foreach (vp_GlobalCallback<T, U> vpGlobalCallback in callback)
        vpGlobalCallback(arg1, arg2);
    }
    else if (mode == vp_GlobalEventMode.REQUIRE_LISTENER)
      throw vp_GlobalEventInternal.ShowSendException(name);
  }
}
