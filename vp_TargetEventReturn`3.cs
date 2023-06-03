// Decompiled with JetBrains decompiler
// Type: vp_TargetEventReturn`3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public static class vp_TargetEventReturn<T, U, R>
{
  public static void Register(object target, string eventName, Func<T, U, R> callback) => vp_TargetEventHandler.Register(target, eventName, callback, 6);

  public static void Unregister(object target, string eventName, Func<T, U, R> callback) => vp_TargetEventHandler.Unregister(target, eventName, callback);

  public static void Unregister(object target) => vp_TargetEventHandler.Unregister(target);

  public static R Send(
    object target,
    string eventName,
    T arg1,
    U arg2,
    vp_TargetEventOptions options = vp_TargetEventOptions.DontRequireReceiver)
  {
    while (true)
    {
      Delegate callback = vp_TargetEventHandler.GetCallback(target, eventName, false, 6, options);
      if ((object) callback != null)
      {
        try
        {
          return ((Func<T, U, R>) callback)(arg1, arg2);
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
    return default (R);
  }

  public static R SendUpwards(
    Component target,
    string eventName,
    T arg1,
    U arg2,
    vp_TargetEventOptions options = vp_TargetEventOptions.DontRequireReceiver)
  {
    while (true)
    {
      Delegate callback = vp_TargetEventHandler.GetCallback(target, eventName, true, 6, options);
      if ((object) callback != null)
      {
        try
        {
          return ((Func<T, U, R>) callback)(arg1, arg2);
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
    return default (R);
  }
}
