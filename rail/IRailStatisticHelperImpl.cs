// Decompiled with JetBrains decompiler
// Type: rail.IRailStatisticHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailStatisticHelperImpl : RailObject, IRailStatisticHelper
  {
    internal IRailStatisticHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailStatisticHelperImpl()
    {
    }

    public virtual IRailPlayerStats CreatePlayerStats(RailID player)
    {
      IntPtr num = player == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (player != null)
        RailConverter.Csharp2Cpp(player, num);
      try
      {
        IntPtr playerStats = RAIL_API_PINVOKE.IRailStatisticHelper_CreatePlayerStats(swigCPtr_, num);
        return playerStats == IntPtr.Zero ? null : (IRailPlayerStats) new IRailPlayerStatsImpl(playerStats);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual IRailGlobalStats GetGlobalStats()
    {
      IntPtr globalStats = RAIL_API_PINVOKE.IRailStatisticHelper_GetGlobalStats(swigCPtr_);
      return !(globalStats == IntPtr.Zero) ? new IRailGlobalStatsImpl(globalStats) : (IRailGlobalStats) null;
    }

    public virtual RailResult AsyncGetNumberOfPlayer(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailStatisticHelper_AsyncGetNumberOfPlayer(swigCPtr_, user_data);
  }
}
