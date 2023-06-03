// Decompiled with JetBrains decompiler
// Type: rail.IRailUsersHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailUsersHelperImpl : RailObject, IRailUsersHelper
  {
    internal IRailUsersHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailUsersHelperImpl()
    {
    }

    public virtual RailResult AsyncGetUsersInfo(List<RailID> rail_ids, string user_data)
    {
      IntPtr num = rail_ids == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      if (rail_ids != null)
        RailConverter.Csharp2Cpp(rail_ids, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUsersHelper_AsyncGetUsersInfo(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailID(num);
      }
    }

    public virtual RailResult AsyncInviteUsers(
      string command_line,
      List<RailID> users,
      RailInviteOptions options,
      string user_data)
    {
      IntPtr num1 = users == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      if (users != null)
        RailConverter.Csharp2Cpp(users, num1);
      IntPtr num2 = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailInviteOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUsersHelper_AsyncInviteUsers(swigCPtr_, command_line, num1, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailID(num1);
        RAIL_API_PINVOKE.delete_RailInviteOptions(num2);
      }
    }

    public virtual RailResult AsyncGetInviteDetail(
      RailID inviter,
      EnumRailUsersInviteType invite_type,
      string user_data)
    {
      IntPtr num = inviter == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (inviter != null)
        RailConverter.Csharp2Cpp(inviter, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailUsersHelper_AsyncGetInviteDetail(swigCPtr_, num, (int) invite_type, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult AsyncCancelInvite(
      EnumRailUsersInviteType invite_type,
      string user_data)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailUsersHelper_AsyncCancelInvite(swigCPtr_, (int) invite_type, user_data);
    }

    public virtual RailResult AsyncCancelAllInvites(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailUsersHelper_AsyncCancelAllInvites(swigCPtr_, user_data);
  }
}
