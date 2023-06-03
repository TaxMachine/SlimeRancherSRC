﻿// Decompiled with JetBrains decompiler
// Type: vp_GlobalEventReturn`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

public static class vp_GlobalEventReturn<T, R>
{
  private static Hashtable m_Callbacks = vp_GlobalEventInternal.Callbacks;

  public static void Register(string name, vp_GlobalCallbackReturn<T, R> callback)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof (name));
    if (callback == null)
      throw new ArgumentNullException(nameof (callback));
    List<vp_GlobalCallbackReturn<T, R>> globalCallbackReturnList = (List<vp_GlobalCallbackReturn<T, R>>) m_Callbacks[name];
    if (globalCallbackReturnList == null)
    {
      globalCallbackReturnList = new List<vp_GlobalCallbackReturn<T, R>>();
      m_Callbacks.Add(name, globalCallbackReturnList);
    }
    globalCallbackReturnList.Add(callback);
  }

  public static void Unregister(string name, vp_GlobalCallbackReturn<T, R> callback)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof (name));
    if (callback == null)
      throw new ArgumentNullException(nameof (callback));
    ((List<vp_GlobalCallbackReturn<T, R>>) m_Callbacks[name] ?? throw vp_GlobalEventInternal.ShowUnregisterException(name)).Remove(callback);
  }

  public static R Send(string name, T arg1) => Send(name, arg1, vp_GlobalEventMode.DONT_REQUIRE_LISTENER);

  public static R Send(string name, T arg1, vp_GlobalEventMode mode)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof (name));
    if (arg1 == null)
      throw new ArgumentNullException(nameof (arg1));
    List<vp_GlobalCallbackReturn<T, R>> callback = (List<vp_GlobalCallbackReturn<T, R>>) m_Callbacks[name];
    if (callback != null)
    {
      R r = default (R);
      foreach (vp_GlobalCallbackReturn<T, R> globalCallbackReturn in callback)
        r = globalCallbackReturn(arg1);
      return r;
    }
    if (mode == vp_GlobalEventMode.REQUIRE_LISTENER)
      throw vp_GlobalEventInternal.ShowSendException(name);
    return default (R);
  }
}
