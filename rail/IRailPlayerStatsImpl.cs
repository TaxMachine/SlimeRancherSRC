// Decompiled with JetBrains decompiler
// Type: rail.IRailPlayerStatsImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailPlayerStatsImpl : RailObject, IRailPlayerStats, IRailComponent
  {
    internal IRailPlayerStatsImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailPlayerStatsImpl()
    {
    }

    public virtual RailID GetRailID()
    {
      IntPtr railId1 = RAIL_API_PINVOKE.IRailPlayerStats_GetRailID(swigCPtr_);
      RailID railId2 = new RailID();
      RailID ret = railId2;
      RailConverter.Cpp2Csharp(railId1, ret);
      return railId2;
    }

    public virtual RailResult AsyncRequestStats(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailPlayerStats_AsyncRequestStats(swigCPtr_, user_data);

    public virtual RailResult GetStatValue(string name, out int data) => (RailResult) RAIL_API_PINVOKE.IRailPlayerStats_GetStatValue__SWIG_0(swigCPtr_, name, out data);

    public virtual RailResult GetStatValue(string name, out double data) => (RailResult) RAIL_API_PINVOKE.IRailPlayerStats_GetStatValue__SWIG_1(swigCPtr_, name, out data);

    public virtual RailResult SetStatValue(string name, int data) => (RailResult) RAIL_API_PINVOKE.IRailPlayerStats_SetStatValue__SWIG_0(swigCPtr_, name, data);

    public virtual RailResult SetStatValue(string name, double data) => (RailResult) RAIL_API_PINVOKE.IRailPlayerStats_SetStatValue__SWIG_1(swigCPtr_, name, data);

    public virtual RailResult UpdateAverageStatValue(string name, double data) => (RailResult) RAIL_API_PINVOKE.IRailPlayerStats_UpdateAverageStatValue(swigCPtr_, name, data);

    public virtual RailResult AsyncStoreStats(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailPlayerStats_AsyncStoreStats(swigCPtr_, user_data);

    public virtual RailResult ResetAllStats() => (RailResult) RAIL_API_PINVOKE.IRailPlayerStats_ResetAllStats(swigCPtr_);

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
