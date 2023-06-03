// Decompiled with JetBrains decompiler
// Type: rail.IRailLeaderboardImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailLeaderboardImpl : RailObject, IRailLeaderboard, IRailComponent
  {
    internal IRailLeaderboardImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailLeaderboardImpl()
    {
    }

    public virtual string GetLeaderboardName() => RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardName(swigCPtr_);

    public virtual int GetTotalEntriesCount() => RAIL_API_PINVOKE.IRailLeaderboard_GetTotalEntriesCount(swigCPtr_);

    public virtual RailResult AsyncGetLeaderboard(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailLeaderboard_AsyncGetLeaderboard(swigCPtr_, user_data);

    public virtual RailResult GetLeaderboardParameters(LeaderboardParameters param)
    {
      IntPtr num = param == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_LeaderboardParameters__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardParameters(swigCPtr_, num);
      }
      finally
      {
        if (param != null)
          RailConverter.Cpp2Csharp(num, param);
        RAIL_API_PINVOKE.delete_LeaderboardParameters(num);
      }
    }

    public virtual IRailLeaderboardEntries CreateLeaderboardEntries()
    {
      IntPtr leaderboardEntries = RAIL_API_PINVOKE.IRailLeaderboard_CreateLeaderboardEntries(swigCPtr_);
      return !(leaderboardEntries == IntPtr.Zero) ? new IRailLeaderboardEntriesImpl(leaderboardEntries) : (IRailLeaderboardEntries) null;
    }

    public virtual RailResult AsyncUploadLeaderboard(
      UploadLeaderboardParam update_param,
      string user_data)
    {
      IntPtr num = update_param == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_UploadLeaderboardParam__SWIG_0();
      if (update_param != null)
        RailConverter.Csharp2Cpp(update_param, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailLeaderboard_AsyncUploadLeaderboard(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_UploadLeaderboardParam(num);
      }
    }

    public virtual RailResult GetLeaderboardSortType(out int sort_type) => (RailResult) RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardSortType(swigCPtr_, out sort_type);

    public virtual RailResult GetLeaderboardDisplayType(out int display_type) => (RailResult) RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardDisplayType(swigCPtr_, out display_type);

    public virtual RailResult AsyncAttachSpaceWork(SpaceWorkID spacework_id, string user_data)
    {
      IntPtr num = spacework_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0();
      if (spacework_id != null)
        RailConverter.Csharp2Cpp(spacework_id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailLeaderboard_AsyncAttachSpaceWork(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_SpaceWorkID(num);
      }
    }

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
