// Decompiled with JetBrains decompiler
// Type: rail.IRailSystemHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailSystemHelperImpl : RailObject, IRailSystemHelper
  {
    internal IRailSystemHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailSystemHelperImpl()
    {
    }

    public virtual float GetRateOfGameRevenue() => RAIL_API_PINVOKE.IRailSystemHelper_GetRateOfGameRevenue(swigCPtr_);

    public virtual RailResult SetTerminationTimeoutOwnershipExpired(int timeout_seconds) => (RailResult) RAIL_API_PINVOKE.IRailSystemHelper_SetTerminationTimeoutOwnershipExpired(swigCPtr_, timeout_seconds);

    public virtual RailSystemState GetPlatformSystemState() => (RailSystemState) RAIL_API_PINVOKE.IRailSystemHelper_GetPlatformSystemState(swigCPtr_);
  }
}
