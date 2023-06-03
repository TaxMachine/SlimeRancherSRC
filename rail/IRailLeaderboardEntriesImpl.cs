// Decompiled with JetBrains decompiler
// Type: rail.IRailLeaderboardEntriesImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailLeaderboardEntriesImpl : RailObject, IRailLeaderboardEntries, IRailComponent
  {
    internal IRailLeaderboardEntriesImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailLeaderboardEntriesImpl()
    {
    }

    public virtual RailID GetRailID()
    {
      IntPtr railId1 = RAIL_API_PINVOKE.IRailLeaderboardEntries_GetRailID(swigCPtr_);
      RailID railId2 = new RailID();
      RailID ret = railId2;
      RailConverter.Cpp2Csharp(railId1, ret);
      return railId2;
    }

    public virtual string GetLeaderboardName() => RAIL_API_PINVOKE.IRailLeaderboardEntries_GetLeaderboardName(swigCPtr_);

    public virtual RailResult AsyncRequestLeaderboardEntries(
      RailID player,
      RequestLeaderboardEntryParam param,
      string user_data)
    {
      IntPtr num1 = player == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (player != null)
        RailConverter.Csharp2Cpp(player, num1);
      IntPtr num2 = param == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RequestLeaderboardEntryParam__SWIG_0();
      if (param != null)
        RailConverter.Csharp2Cpp(param, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailLeaderboardEntries_AsyncRequestLeaderboardEntries(swigCPtr_, num1, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RequestLeaderboardEntryParam(num2);
      }
    }

    public virtual RequestLeaderboardEntryParam GetEntriesParam()
    {
      IntPtr entriesParam1 = RAIL_API_PINVOKE.IRailLeaderboardEntries_GetEntriesParam(swigCPtr_);
      RequestLeaderboardEntryParam entriesParam2 = new RequestLeaderboardEntryParam();
      RequestLeaderboardEntryParam ret = entriesParam2;
      RailConverter.Cpp2Csharp(entriesParam1, ret);
      return entriesParam2;
    }

    public virtual int GetEntriesCount() => RAIL_API_PINVOKE.IRailLeaderboardEntries_GetEntriesCount(swigCPtr_);

    public virtual RailResult GetLeaderboardEntry(int index, LeaderboardEntry leaderboard_entry)
    {
      IntPtr num = leaderboard_entry == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_LeaderboardEntry__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailLeaderboardEntries_GetLeaderboardEntry(swigCPtr_, index, num);
      }
      finally
      {
        if (leaderboard_entry != null)
          RailConverter.Cpp2Csharp(num, leaderboard_entry);
        RAIL_API_PINVOKE.delete_LeaderboardEntry(num);
      }
    }

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
