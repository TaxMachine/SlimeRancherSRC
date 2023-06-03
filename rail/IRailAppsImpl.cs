// Decompiled with JetBrains decompiler
// Type: rail.IRailAppsImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailAppsImpl : RailObject, IRailApps
  {
    internal IRailAppsImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailAppsImpl()
    {
    }

    public virtual RailResult MarkGameContentDamaged(EnumRailGameContentDamageFlag flag) => (RailResult) RAIL_API_PINVOKE.IRailApps_MarkGameContentDamaged(swigCPtr_, (int) flag);

    public virtual RailResult GetGameInstallPath(out string app_path)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailApps_GetGameInstallPath(swigCPtr_, num);
      }
      finally
      {
        app_path = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult GetGameLanguageCode(out string language_code)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailApps_GetGameLanguageCode(swigCPtr_, num);
      }
      finally
      {
        language_code = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult SetGameState(EnumRailGamePlayingState game_state_flag) => (RailResult) RAIL_API_PINVOKE.IRailApps_SetGameState(swigCPtr_, (int) game_state_flag);

    public virtual RailResult GetGameState(out EnumRailGamePlayingState game_state_flag)
    {
      IntPtr num = RAIL_API_PINVOKE.NewInt();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailApps_GetGameState(swigCPtr_, num);
      }
      finally
      {
        game_state_flag = (EnumRailGamePlayingState) RAIL_API_PINVOKE.GetInt(num);
        RAIL_API_PINVOKE.DeleteInt(num);
      }
    }

    public virtual uint GetGameEarliestPurchaseTime() => RAIL_API_PINVOKE.IRailApps_GetGameEarliestPurchaseTime(swigCPtr_);

    public virtual RailResult GetCurrentBranchInfo(RailBranchInfo branch_info)
    {
      IntPtr num = branch_info == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailBranchInfo__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailApps_GetCurrentBranchInfo(swigCPtr_, num);
      }
      finally
      {
        if (branch_info != null)
          RailConverter.Cpp2Csharp(num, branch_info);
        RAIL_API_PINVOKE.delete_RailBranchInfo(num);
      }
    }

    public virtual RailResult AsyncQuerySubscribeWishPlayState(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailApps_AsyncQuerySubscribeWishPlayState(swigCPtr_, user_data);
  }
}
