// Decompiled with JetBrains decompiler
// Type: vp_GlobalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

public static class vp_GlobalEvent
{
  private static Hashtable m_Callbacks = vp_GlobalEventInternal.Callbacks;

  public static void Register(string name, vp_GlobalCallback callback)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof (name));
    if (callback == null)
      throw new ArgumentNullException(nameof (callback));
    List<vp_GlobalCallback> vpGlobalCallbackList = (List<vp_GlobalCallback>) m_Callbacks[name];
    if (vpGlobalCallbackList == null)
    {
      vpGlobalCallbackList = new List<vp_GlobalCallback>();
      m_Callbacks.Add(name, vpGlobalCallbackList);
    }
    vpGlobalCallbackList.Add(callback);
  }

  public static void Unregister(string name, vp_GlobalCallback callback)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof (name));
    if (callback == null)
      throw new ArgumentNullException(nameof (callback));
    ((List<vp_GlobalCallback>) m_Callbacks[name] ?? throw vp_GlobalEventInternal.ShowUnregisterException(name)).Remove(callback);
  }

  public static void Send(string name) => Send(name, vp_GlobalEventMode.DONT_REQUIRE_LISTENER);

  public static void Send(string name, vp_GlobalEventMode mode)
  {
    List<vp_GlobalCallback> vpGlobalCallbackList = !string.IsNullOrEmpty(name) ? (List<vp_GlobalCallback>) m_Callbacks[name] : throw new ArgumentNullException(nameof (name));
    if (vpGlobalCallbackList != null)
    {
      foreach (vp_GlobalCallback vpGlobalCallback in vpGlobalCallbackList)
        vpGlobalCallback();
    }
    else if (mode == vp_GlobalEventMode.REQUIRE_LISTENER)
      throw vp_GlobalEventInternal.ShowSendException(name);
  }
}
