// Decompiled with JetBrains decompiler
// Type: vp_TargetEvent`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public static class vp_TargetEvent<T>
{
  public static void Register(object target, string eventName, Action<T> callback) => vp_TargetEventHandler.Register(target, eventName, callback, 1);

  public static void Unregister(object target, string eventName, Action<T> callback) => vp_TargetEventHandler.Unregister(target, eventName, callback);

  public static void Unregister(object target) => vp_TargetEventHandler.Unregister(target);

  public static void Send(object target, string eventName, T arg, vp_TargetEventOptions options = vp_TargetEventOptions.DontRequireReceiver)
  {
    while (true)
    {
      Delegate callback = vp_TargetEventHandler.GetCallback(target, eventName, false, 1, options);
      if ((object) callback != null)
      {
        try
        {
          ((Action<T>) callback)(arg);
          return;
        }
        catch
        {
          eventName += "_";
        }
      }
      else
        break;
    }
    vp_TargetEventHandler.OnNoReceiver(eventName, options);
  }

  public static void SendUpwards(
    Component target,
    string eventName,
    T arg,
    vp_TargetEventOptions options = vp_TargetEventOptions.DontRequireReceiver)
  {
    while (true)
    {
      Delegate callback = vp_TargetEventHandler.GetCallback(target, eventName, true, 1, options);
      if ((object) callback != null)
      {
        try
        {
          ((Action<T>) callback)(arg);
          return;
        }
        catch
        {
          eventName += "_";
        }
      }
      else
        break;
    }
    vp_TargetEventHandler.OnNoReceiver(eventName, options);
  }
}
