// Decompiled with JetBrains decompiler
// Type: rail.IRailStorageHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailStorageHelperImpl : RailObject, IRailStorageHelper
  {
    internal IRailStorageHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailStorageHelperImpl()
    {
    }

    public virtual IRailFile OpenFile(string filename, out int result)
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailStorageHelper_OpenFile__SWIG_0(swigCPtr_, filename, out result);
      return !(cPtr == IntPtr.Zero) ? new IRailFileImpl(cPtr) : (IRailFile) null;
    }

    public virtual IRailFile OpenFile(string filename)
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailStorageHelper_OpenFile__SWIG_1(swigCPtr_, filename);
      return !(cPtr == IntPtr.Zero) ? new IRailFileImpl(cPtr) : (IRailFile) null;
    }

    public virtual IRailFile CreateFile(string filename, out int result)
    {
      IntPtr fileSwig0 = RAIL_API_PINVOKE.IRailStorageHelper_CreateFile__SWIG_0(swigCPtr_, filename, out result);
      return !(fileSwig0 == IntPtr.Zero) ? new IRailFileImpl(fileSwig0) : (IRailFile) null;
    }

    public virtual IRailFile CreateFile(string filename)
    {
      IntPtr fileSwig1 = RAIL_API_PINVOKE.IRailStorageHelper_CreateFile__SWIG_1(swigCPtr_, filename);
      return !(fileSwig1 == IntPtr.Zero) ? new IRailFileImpl(fileSwig1) : (IRailFile) null;
    }

    public virtual bool IsFileExist(string filename) => RAIL_API_PINVOKE.IRailStorageHelper_IsFileExist(swigCPtr_, filename);

    public virtual bool ListFiles(List<string> filelist)
    {
      IntPtr num = filelist == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailStorageHelper_ListFiles(swigCPtr_, num);
      }
      finally
      {
        if (filelist != null)
          RailConverter.Cpp2Csharp(num, filelist);
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual RailResult RemoveFile(string filename) => (RailResult) RAIL_API_PINVOKE.IRailStorageHelper_RemoveFile(swigCPtr_, filename);

    public virtual bool IsFileSyncedToCloud(string filename) => RAIL_API_PINVOKE.IRailStorageHelper_IsFileSyncedToCloud(swigCPtr_, filename);

    public virtual RailResult GetFileTimestamp(string filename, out ulong time_stamp) => (RailResult) RAIL_API_PINVOKE.IRailStorageHelper_GetFileTimestamp(swigCPtr_, filename, out time_stamp);

    public virtual uint GetFileCount() => RAIL_API_PINVOKE.IRailStorageHelper_GetFileCount(swigCPtr_);

    public virtual RailResult GetFileNameAndSize(
      uint file_index,
      out string filename,
      out ulong file_size)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailStorageHelper_GetFileNameAndSize(swigCPtr_, file_index, num, out file_size);
      }
      finally
      {
        filename = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult AsyncQueryQuota() => (RailResult) RAIL_API_PINVOKE.IRailStorageHelper_AsyncQueryQuota(swigCPtr_);

    public virtual RailResult SetSyncFileOption(string filename, RailSyncFileOption option)
    {
      IntPtr num = option == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSyncFileOption__SWIG_0();
      if (option != null)
        RailConverter.Csharp2Cpp(option, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailStorageHelper_SetSyncFileOption(swigCPtr_, filename, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSyncFileOption(num);
      }
    }

    public virtual bool IsCloudStorageEnabledForApp() => RAIL_API_PINVOKE.IRailStorageHelper_IsCloudStorageEnabledForApp(swigCPtr_);

    public virtual bool IsCloudStorageEnabledForPlayer() => RAIL_API_PINVOKE.IRailStorageHelper_IsCloudStorageEnabledForPlayer(swigCPtr_);

    public virtual RailResult AsyncPublishFileToUserSpace(
      RailPublishFileToUserSpaceOption option,
      string user_data)
    {
      IntPtr num = option == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailPublishFileToUserSpaceOption__SWIG_0();
      if (option != null)
        RailConverter.Csharp2Cpp(option, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailStorageHelper_AsyncPublishFileToUserSpace(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailPublishFileToUserSpaceOption(num);
      }
    }

    public virtual IRailStreamFile OpenStreamFile(
      string filename,
      RailStreamFileOption option,
      out int result)
    {
      IntPtr num = option == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailStreamFileOption__SWIG_0();
      if (option != null)
        RailConverter.Csharp2Cpp(option, num);
      try
      {
        IntPtr cPtr = RAIL_API_PINVOKE.IRailStorageHelper_OpenStreamFile__SWIG_0(swigCPtr_, filename, num, out result);
        return cPtr == IntPtr.Zero ? null : (IRailStreamFile) new IRailStreamFileImpl(cPtr);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailStreamFileOption(num);
      }
    }

    public virtual IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option)
    {
      IntPtr num = option == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailStreamFileOption__SWIG_0();
      if (option != null)
        RailConverter.Csharp2Cpp(option, num);
      try
      {
        IntPtr cPtr = RAIL_API_PINVOKE.IRailStorageHelper_OpenStreamFile__SWIG_1(swigCPtr_, filename, num);
        return cPtr == IntPtr.Zero ? null : (IRailStreamFile) new IRailStreamFileImpl(cPtr);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailStreamFileOption(num);
      }
    }

    public virtual RailResult AsyncListStreamFiles(
      string contents,
      RailListStreamFileOption option,
      string user_data)
    {
      IntPtr num = option == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailListStreamFileOption__SWIG_0();
      if (option != null)
        RailConverter.Csharp2Cpp(option, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailStorageHelper_AsyncListStreamFiles(swigCPtr_, contents, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailListStreamFileOption(num);
      }
    }

    public virtual RailResult AsyncRenameStreamFile(
      string old_filename,
      string new_filename,
      string user_data)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailStorageHelper_AsyncRenameStreamFile(swigCPtr_, old_filename, new_filename, user_data);
    }

    public virtual RailResult AsyncDeleteStreamFile(string filename, string user_data) => (RailResult) RAIL_API_PINVOKE.IRailStorageHelper_AsyncDeleteStreamFile(swigCPtr_, filename, user_data);
  }
}
