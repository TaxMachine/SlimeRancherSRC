// Decompiled with JetBrains decompiler
// Type: rail.IRailGlobalStatsImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailGlobalStatsImpl : RailObject, IRailGlobalStats, IRailComponent
  {
    internal IRailGlobalStatsImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailGlobalStatsImpl()
    {
    }

    public virtual RailResult AsyncRequestGlobalStats(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailGlobalStats_AsyncRequestGlobalStats(swigCPtr_, user_data);

    public virtual RailResult GetGlobalStatValue(string name, out long data) => (RailResult) RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValue__SWIG_0(swigCPtr_, name, out data);

    public virtual RailResult GetGlobalStatValue(string name, out double data) => (RailResult) RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValue__SWIG_1(swigCPtr_, name, out data);

    public virtual RailResult GetGlobalStatValueHistory(
      string name,
      long[] global_stats_data,
      uint data_size,
      out int num_global_stats)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValueHistory__SWIG_0(swigCPtr_, name, global_stats_data, data_size, out num_global_stats);
    }

    public virtual RailResult GetGlobalStatValueHistory(
      string name,
      double[] global_stats_data,
      uint data_size,
      out int num_global_stats)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailGlobalStats_GetGlobalStatValueHistory__SWIG_1(swigCPtr_, name, global_stats_data, data_size, out num_global_stats);
    }

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
