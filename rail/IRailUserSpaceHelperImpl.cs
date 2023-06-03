// Decompiled with JetBrains decompiler
// Type: rail.IRailUserSpaceHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailUserSpaceHelperImpl : RailObject, IRailUserSpaceHelper
  {
    internal IRailUserSpaceHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailUserSpaceHelperImpl()
    {
    }

    public virtual RailResult AsyncGetMySubscribedWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type,
      RailQueryWorkFileOptions options,
      string user_data)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_0(swigCPtr_, offset, max_works, (int) type, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(num);
      }
    }

    public virtual RailResult AsyncGetMySubscribedWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type,
      RailQueryWorkFileOptions options)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_1(swigCPtr_, offset, max_works, (int) type, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(num);
      }
    }

    public virtual RailResult AsyncGetMySubscribedWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_2(swigCPtr_, offset, max_works, (int) type);
    }

    public virtual RailResult AsyncGetMyFavoritesWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type,
      RailQueryWorkFileOptions options,
      string user_data)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_0(swigCPtr_, offset, max_works, (int) type, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(num);
      }
    }

    public virtual RailResult AsyncGetMyFavoritesWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type,
      RailQueryWorkFileOptions options)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_1(swigCPtr_, offset, max_works, (int) type, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(num);
      }
    }

    public virtual RailResult AsyncGetMyFavoritesWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_2(swigCPtr_, offset, max_works, (int) type);
    }

    public virtual RailResult AsyncQuerySpaceWorks(
      RailSpaceWorkFilter filter,
      uint offset,
      uint max_works,
      EnumRailSpaceWorkOrderBy order_by,
      RailQueryWorkFileOptions options,
      string user_data)
    {
      IntPtr num1 = filter == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0();
      if (filter != null)
        RailConverter.Csharp2Cpp(filter, num1);
      IntPtr num2 = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_0(swigCPtr_, num1, offset, max_works, (int) order_by, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(num1);
        RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(num2);
      }
    }

    public virtual RailResult AsyncQuerySpaceWorks(
      RailSpaceWorkFilter filter,
      uint offset,
      uint max_works,
      EnumRailSpaceWorkOrderBy order_by,
      RailQueryWorkFileOptions options)
    {
      IntPtr num1 = filter == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0();
      if (filter != null)
        RailConverter.Csharp2Cpp(filter, num1);
      IntPtr num2 = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_1(swigCPtr_, num1, offset, max_works, (int) order_by, num2);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(num1);
        RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(num2);
      }
    }

    public virtual RailResult AsyncQuerySpaceWorks(
      RailSpaceWorkFilter filter,
      uint offset,
      uint max_works,
      EnumRailSpaceWorkOrderBy order_by)
    {
      IntPtr num = filter == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0();
      if (filter != null)
        RailConverter.Csharp2Cpp(filter, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_2(swigCPtr_, num, offset, max_works, (int) order_by);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(num);
      }
    }

    public virtual RailResult AsyncQuerySpaceWorks(
      RailSpaceWorkFilter filter,
      uint offset,
      uint max_works)
    {
      IntPtr num = filter == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0();
      if (filter != null)
        RailConverter.Csharp2Cpp(filter, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_3(swigCPtr_, num, offset, max_works);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(num);
      }
    }

    public virtual RailResult AsyncSubscribeSpaceWorks(
      List<SpaceWorkID> ids,
      bool subscribe,
      string user_data)
    {
      IntPtr num = ids == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0();
      if (ids != null)
        RailConverter.Csharp2Cpp(ids, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncSubscribeSpaceWorks(swigCPtr_, num, subscribe, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(num);
      }
    }

    public virtual IRailSpaceWork OpenSpaceWork(SpaceWorkID id)
    {
      IntPtr num = id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0();
      if (id != null)
        RailConverter.Csharp2Cpp(id, num);
      try
      {
        IntPtr cPtr = RAIL_API_PINVOKE.IRailUserSpaceHelper_OpenSpaceWork(swigCPtr_, num);
        return cPtr == IntPtr.Zero ? null : (IRailSpaceWork) new IRailSpaceWorkImpl(cPtr);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_SpaceWorkID(num);
      }
    }

    public virtual IRailSpaceWork CreateSpaceWork(EnumRailSpaceWorkType type)
    {
      IntPtr spaceWork = RAIL_API_PINVOKE.IRailUserSpaceHelper_CreateSpaceWork(swigCPtr_, (int) type);
      return !(spaceWork == IntPtr.Zero) ? new IRailSpaceWorkImpl(spaceWork) : (IRailSpaceWork) null;
    }

    public virtual RailResult GetMySubscribedWorks(
      uint offset,
      uint max_works,
      EnumRailSpaceWorkType type,
      QueryMySubscribedSpaceWorksResult result)
    {
      IntPtr num = result == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_QueryMySubscribedSpaceWorksResult__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_GetMySubscribedWorks(swigCPtr_, offset, max_works, (int) type, num);
      }
      finally
      {
        if (result != null)
          RailConverter.Cpp2Csharp(num, result);
        RAIL_API_PINVOKE.delete_QueryMySubscribedSpaceWorksResult(num);
      }
    }

    public virtual uint GetMySubscribedWorksCount(EnumRailSpaceWorkType type, out int result) => RAIL_API_PINVOKE.IRailUserSpaceHelper_GetMySubscribedWorksCount(swigCPtr_, (int) type, out result);

    public virtual RailResult AsyncRemoveSpaceWork(SpaceWorkID id, string user_data)
    {
      IntPtr num = id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0();
      if (id != null)
        RailConverter.Csharp2Cpp(id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncRemoveSpaceWork(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_SpaceWorkID(num);
      }
    }

    public virtual RailResult AsyncModifyFavoritesWorks(
      List<SpaceWorkID> ids,
      EnumRailModifyFavoritesSpaceWorkType modify_flag,
      string user_data)
    {
      IntPtr num = ids == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0();
      if (ids != null)
        RailConverter.Csharp2Cpp(ids, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncModifyFavoritesWorks(swigCPtr_, num, (int) modify_flag, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(num);
      }
    }

    public virtual RailResult AsyncVoteSpaceWork(
      SpaceWorkID id,
      EnumRailSpaceWorkVoteValue vote,
      string user_data)
    {
      IntPtr num = id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0();
      if (id != null)
        RailConverter.Csharp2Cpp(id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncVoteSpaceWork(swigCPtr_, num, (int) vote, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_SpaceWorkID(num);
      }
    }

    public virtual RailResult AsyncSearchSpaceWork(
      RailSpaceWorkSearchFilter filter,
      RailQueryWorkFileOptions options,
      List<EnumRailSpaceWorkType> types,
      uint offset,
      uint max_works,
      string user_data)
    {
      IntPtr num1 = filter == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkSearchFilter__SWIG_0();
      if (filter != null)
        RailConverter.Csharp2Cpp(filter, num1);
      IntPtr num2 = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num2);
      IntPtr num3 = types == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayEnumRailSpaceWorkType__SWIG_0();
      if (types != null)
        RailConverter.Csharp2Cpp(types, num3);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncSearchSpaceWork(swigCPtr_, num1, num2, num3, offset, max_works, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSpaceWorkSearchFilter(num1);
        RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(num2);
        RAIL_API_PINVOKE.delete_RailArrayEnumRailSpaceWorkType(num3);
      }
    }
  }
}
