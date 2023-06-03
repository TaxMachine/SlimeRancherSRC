// Decompiled with JetBrains decompiler
// Type: rail.IRailDlcHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailDlcHelperImpl : RailObject, IRailDlcHelper
  {
    internal IRailDlcHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailDlcHelperImpl()
    {
    }

    public virtual RailResult AsyncQueryIsOwnedDlcsOnServer(
      List<RailDlcID> dlc_ids,
      string user_data)
    {
      IntPtr num = dlc_ids == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailDlcID__SWIG_0();
      if (dlc_ids != null)
        RailConverter.Csharp2Cpp(dlc_ids, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailDlcHelper_AsyncQueryIsOwnedDlcsOnServer(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailDlcID(num);
      }
    }

    public virtual RailResult AsyncCheckAllDlcsStateReady(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailDlcHelper_AsyncCheckAllDlcsStateReady(swigCPtr_, user_data);

    public virtual bool IsDlcInstalled(RailDlcID dlc_id, out string installed_path)
    {
      IntPtr num1 = dlc_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0();
      if (dlc_id != null)
        RailConverter.Csharp2Cpp(dlc_id, num1);
      IntPtr num2 = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailDlcHelper_IsDlcInstalled__SWIG_0(swigCPtr_, num1, num2);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailDlcID(num1);
        installed_path = RAIL_API_PINVOKE.RailString_c_str(num2);
        RAIL_API_PINVOKE.delete_RailString(num2);
      }
    }

    public virtual bool IsDlcInstalled(RailDlcID dlc_id)
    {
      IntPtr num = dlc_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0();
      if (dlc_id != null)
        RailConverter.Csharp2Cpp(dlc_id, num);
      try
      {
        return RAIL_API_PINVOKE.IRailDlcHelper_IsDlcInstalled__SWIG_1(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailDlcID(num);
      }
    }

    public virtual bool IsOwnedDlc(RailDlcID dlc_id)
    {
      IntPtr num = dlc_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0();
      if (dlc_id != null)
        RailConverter.Csharp2Cpp(dlc_id, num);
      try
      {
        return RAIL_API_PINVOKE.IRailDlcHelper_IsOwnedDlc(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailDlcID(num);
      }
    }

    public virtual uint GetDlcCount() => RAIL_API_PINVOKE.IRailDlcHelper_GetDlcCount(swigCPtr_);

    public virtual bool GetDlcInfo(uint index, RailDlcInfo dlc_info)
    {
      IntPtr num = dlc_info == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcInfo__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailDlcHelper_GetDlcInfo(swigCPtr_, index, num);
      }
      finally
      {
        if (dlc_info != null)
          RailConverter.Cpp2Csharp(num, dlc_info);
        RAIL_API_PINVOKE.delete_RailDlcInfo(num);
      }
    }

    public virtual bool AsyncInstallDlc(RailDlcID dlc_id, string user_data)
    {
      IntPtr num = dlc_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0();
      if (dlc_id != null)
        RailConverter.Csharp2Cpp(dlc_id, num);
      try
      {
        return RAIL_API_PINVOKE.IRailDlcHelper_AsyncInstallDlc(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailDlcID(num);
      }
    }

    public virtual bool AsyncRemoveDlc(RailDlcID dlc_id, string user_data)
    {
      IntPtr num = dlc_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0();
      if (dlc_id != null)
        RailConverter.Csharp2Cpp(dlc_id, num);
      try
      {
        return RAIL_API_PINVOKE.IRailDlcHelper_AsyncRemoveDlc(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailDlcID(num);
      }
    }
  }
}
