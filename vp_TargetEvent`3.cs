// Decompiled with JetBrains decompiler
// Type: vp_TargetEvent`3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public static class vp_TargetEvent<T, U, V>
{
  public static void Register(object target, string eventName, Action<T, U, V> callback) => vp_TargetEventHandler.Register(target, eventName, callback, 3);

  public static void Unregister(object target, string eventName, Action<T, U, V> callback) => vp_TargetEventHandler.Unregister(target, eventName, callback);

  public static void Unregister(object target) => vp_TargetEventHandler.Unregister(target);

  public static void Send(
    object target,
    string eventName,
    T arg1,
    U arg2,
    V arg3,
    vp_TargetEventOptions options = vp_TargetEventOptions.DontRequireReceiver)
  {
    while (true)
    {
      Delegate callback = vp_TargetEventHandler.GetCallback(target, eventName, false, 3, options);
      if ((object) callback != null)
      {
        try
        {
          ((Action<T, U, V>) callback)(arg1, arg2, arg3);
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
    V arg3,
    vp_TargetEventOptions options = vp_TargetEventOptions.DontRequireReceiver)
  {
    while (true)
    {
      Delegate callback = vp_TargetEventHandler.GetCallback(target, eventName, true, 3, options);
      if ((object) callback != null)
      {
        try
        {
          ((Action<T, U, V>) callback)(arg1, arg2, arg3);
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
