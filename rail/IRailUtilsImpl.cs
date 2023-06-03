// Decompiled with JetBrains decompiler
// Type: rail.IRailUtilsImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailUtilsImpl : RailObject, IRailUtils
  {
    internal IRailUtilsImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailUtilsImpl()
    {
    }

    public virtual uint GetTimeCountSinceGameLaunch() => RAIL_API_PINVOKE.IRailUtils_GetTimeCountSinceGameLaunch(swigCPtr_);

    public virtual uint GetTimeCountSinceComputerLaunch() => RAIL_API_PINVOKE.IRailUtils_GetTimeCountSinceComputerLaunch(swigCPtr_);

    public virtual uint GetTimeFromServer() => RAIL_API_PINVOKE.IRailUtils_GetTimeFromServer(swigCPtr_);

    public virtual RailGameID GetGameID()
    {
      IntPtr gameId1 = RAIL_API_PINVOKE.IRailUtils_GetGameID(swigCPtr_);
      RailGameID gameId2 = new RailGameID();
      RailGameID ret = gameId2;
      RailConverter.Cpp2Csharp(gameId1, ret);
      return gameId2;
    }

    public virtual RailResult AsyncGetImageData(
      string image_path,
      uint scale_to_width,
      uint scale_to_height,
      string user_data)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailUtils_AsyncGetImageData(swigCPtr_, image_path, scale_to_width, scale_to_height, user_data);
    }

    public virtual void GetErrorString(RailResult result, out string error_string)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        RAIL_API_PINVOKE.IRailUtils_GetErrorString(swigCPtr_, (int) result, num);
      }
      finally
      {
        error_string = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult DirtyWordsFilter(
      string words,
      bool replace_sensitive,
      RailDirtyWordsCheckResult check_result)
    {
      IntPtr num = check_result == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDirtyWordsCheckResult__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUtils_DirtyWordsFilter(swigCPtr_, words, replace_sensitive, num);
      }
      finally
      {
        if (check_result != null)
          RailConverter.Cpp2Csharp(num, check_result);
        RAIL_API_PINVOKE.delete_RailDirtyWordsCheckResult(num);
      }
    }

    public virtual EnumRailPlatformType GetRailPlatformType() => (EnumRailPlatformType) RAIL_API_PINVOKE.IRailUtils_GetRailPlatformType(swigCPtr_);

    public virtual RailResult GetLaunchAppParameters(
      EnumRailLaunchAppType app_type,
      out string parameter)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUtils_GetLaunchAppParameters(swigCPtr_, (int) app_type, num);
      }
      finally
      {
        parameter = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult GetPlatformLanguageCode(out string language_code)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUtils_GetPlatformLanguageCode(swigCPtr_, num);
      }
      finally
      {
        language_code = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult SetWarningMessageCallback(RailWarningMessageCallbackFunction callback) => (RailResult) RAIL_API_PINVOKE.IRailUtils_SetWarningMessageCallback(swigCPtr_, callback);
  }
}
