// Decompiled with JetBrains decompiler
// Type: rail.IRailFriendsImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailFriendsImpl : RailObject, IRailFriends
  {
    internal IRailFriendsImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailFriendsImpl()
    {
    }

    public virtual RailResult AsyncGetPersonalInfo(List<RailID> rail_ids, string user_data)
    {
      IntPtr num = rail_ids == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      if (rail_ids != null)
        RailConverter.Csharp2Cpp(rail_ids, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailFriends_AsyncGetPersonalInfo(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailID(num);
      }
    }

    public virtual RailResult AsyncGetFriendMetadata(
      RailID rail_id,
      List<string> keys,
      string user_data)
    {
      IntPtr num1 = rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (rail_id != null)
        RailConverter.Csharp2Cpp(rail_id, num1);
      IntPtr num2 = keys == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      if (keys != null)
        RailConverter.Csharp2Cpp(keys, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailFriends_AsyncGetFriendMetadata(swigCPtr_, num1, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailArrayRailString(num2);
      }
    }

    public virtual RailResult AsyncSetMyMetadata(List<RailKeyValue> key_values, string user_data)
    {
      IntPtr num = key_values == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0();
      if (key_values != null)
        RailConverter.Csharp2Cpp(key_values, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailFriends_AsyncSetMyMetadata(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(num);
      }
    }

    public virtual RailResult AsyncClearAllMyMetadata(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailFriends_AsyncClearAllMyMetadata(swigCPtr_, user_data);

    public virtual RailResult AsyncSetInviteCommandLine(string command_line, string user_data) => (RailResult) RAIL_API_PINVOKE.IRailFriends_AsyncSetInviteCommandLine(swigCPtr_, command_line, user_data);

    public virtual RailResult AsyncGetInviteCommandLine(RailID rail_id, string user_data)
    {
      IntPtr num = rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (rail_id != null)
        RailConverter.Csharp2Cpp(rail_id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailFriends_AsyncGetInviteCommandLine(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult AsyncReportPlayedWithUserList(
      List<RailUserPlayedWith> player_list,
      string user_data)
    {
      IntPtr num = player_list == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailUserPlayedWith__SWIG_0();
      if (player_list != null)
        RailConverter.Csharp2Cpp(player_list, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailFriends_AsyncReportPlayedWithUserList(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailUserPlayedWith(num);
      }
    }

    public virtual EnumRailFriendType GetFriendType(RailID rail_id)
    {
      IntPtr num = rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (rail_id != null)
        RailConverter.Csharp2Cpp(rail_id, num);
      try
      {
        return (EnumRailFriendType) RAIL_API_PINVOKE.IRailFriends_GetFriendType(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult GetFriendsList(List<RailID> friends_list)
    {
      IntPtr num = friends_list == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailFriends_GetFriendsList(swigCPtr_, num);
      }
      finally
      {
        if (friends_list != null)
          RailConverter.Cpp2Csharp(num, friends_list);
        RAIL_API_PINVOKE.delete_RailArrayRailID(num);
      }
    }
  }
}
