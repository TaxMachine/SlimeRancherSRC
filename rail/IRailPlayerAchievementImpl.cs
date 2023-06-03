// Decompiled with JetBrains decompiler
// Type: rail.IRailPlayerAchievementImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailPlayerAchievementImpl : RailObject, IRailPlayerAchievement, IRailComponent
  {
    internal IRailPlayerAchievementImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailPlayerAchievementImpl()
    {
    }

    public virtual RailID GetRailID()
    {
      IntPtr railId1 = RAIL_API_PINVOKE.IRailPlayerAchievement_GetRailID(swigCPtr_);
      RailID railId2 = new RailID();
      RailID ret = railId2;
      RailConverter.Cpp2Csharp(railId1, ret);
      return railId2;
    }

    public virtual RailResult AsyncRequestAchievement(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncRequestAchievement(swigCPtr_, user_data);

    public virtual RailResult HasAchieved(string name, out bool achieved) => (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_HasAchieved(swigCPtr_, name, out achieved);

    public virtual RailResult GetAchievementInfo(string name, out string achievement_info)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_GetAchievementInfo(swigCPtr_, name, num);
      }
      finally
      {
        achievement_info = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult AsyncTriggerAchievementProgress(
      string name,
      uint current_value,
      uint max_value,
      string user_data)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_0(swigCPtr_, name, current_value, max_value, user_data);
    }

    public virtual RailResult AsyncTriggerAchievementProgress(
      string name,
      uint current_value,
      uint max_value)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_1(swigCPtr_, name, current_value, max_value);
    }

    public virtual RailResult AsyncTriggerAchievementProgress(string name, uint current_value) => (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_2(swigCPtr_, name, current_value);

    public virtual RailResult MakeAchievement(string name) => (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_MakeAchievement(swigCPtr_, name);

    public virtual RailResult CancelAchievement(string name) => (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_CancelAchievement(swigCPtr_, name);

    public virtual RailResult AsyncStoreAchievement(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_AsyncStoreAchievement(swigCPtr_, user_data);

    public virtual RailResult ResetAllAchievements() => (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_ResetAllAchievements(swigCPtr_);

    public virtual RailResult GetAllAchievementsName(List<string> names)
    {
      IntPtr num = names == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailPlayerAchievement_GetAllAchievementsName(swigCPtr_, num);
      }
      finally
      {
        if (names != null)
          RailConverter.Cpp2Csharp(num, names);
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
