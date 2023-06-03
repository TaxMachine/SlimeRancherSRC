// Decompiled with JetBrains decompiler
// Type: vp_TargetEvent`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public static class vp_TargetEvent<T, U>
{
  public static void Register(object target, string eventName, Action<T, U> callback) => vp_TargetEventHandler.Register(target, eventName, callback, 2);

  public static void Unregister(object target, string eventName, Action<T, U> callback) => vp_TargetEventHandler.Unregister(target, eventName, callback);

  public static void Unregister(object target) => vp_TargetEventHandler.Unregister(target);

  public static void Send(
    object target,
    string eventName,
    T arg1,
    U arg2,
    vp_TargetEventOptions options = vp_TargetEventOptions.DontRequireReceiver)
  {
    while (true)
    {
      Delegate callback = vp_TargetEventHandler.GetCallback(target, eventName, false, 2, options);
      if ((object) callback != null)
      {
        try
        {
          ((Action<T, U>) callback)(arg1, arg2);
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
    T arg1,
    U arg2,
    vp_TargetEventOptions options = vp_TargetEventOptions.DontRequireReceiver)
  {
    while (true)
    {
      Delegate callback = vp_TargetEventHandler.GetCallback(target, eventName, true, 2, options);
      if ((object) callback != null)
      {
        try
        {
          ((Action<T, U>) callback)(arg1, arg2);
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
