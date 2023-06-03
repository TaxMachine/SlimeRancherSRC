// Decompiled with JetBrains decompiler
// Type: rail.IRailRoomImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailRoomImpl : RailObject, IRailRoom, IRailComponent
  {
    internal IRailRoomImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailRoomImpl()
    {
    }

    public virtual ulong GetRoomId() => RAIL_API_PINVOKE.IRailRoom_GetRoomId(swigCPtr_);

    public virtual RailResult GetRoomName(out string name)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailRoom_GetRoomName(swigCPtr_, num);
      }
      finally
      {
        name = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual ulong GetZoneId() => RAIL_API_PINVOKE.IRailRoom_GetZoneId(swigCPtr_);

    public virtual RailID GetOwnerId()
    {
      IntPtr ownerId1 = RAIL_API_PINVOKE.IRailRoom_GetOwnerId(swigCPtr_);
      RailID ownerId2 = new RailID();
      RailID ret = ownerId2;
      RailConverter.Cpp2Csharp(ownerId1, ret);
      return ownerId2;
    }

    public virtual RailResult GetHasPassword(out bool has_password) => (RailResult) RAIL_API_PINVOKE.IRailRoom_GetHasPassword(swigCPtr_, out has_password);

    public virtual EnumRoomType GetRoomType() => (EnumRoomType) RAIL_API_PINVOKE.IRailRoom_GetRoomType(swigCPtr_);

    public virtual bool SetNewOwner(RailID new_owner_id)
    {
      IntPtr num = new_owner_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (new_owner_id != null)
        RailConverter.Csharp2Cpp(new_owner_id, num);
      try
      {
        return RAIL_API_PINVOKE.IRailRoom_SetNewOwner(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult AsyncGetRoomMembers(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailRoom_AsyncGetRoomMembers(swigCPtr_, user_data);

    public virtual void Leave() => RAIL_API_PINVOKE.IRailRoom_Leave(swigCPtr_);

    public virtual RailResult AsyncJoinRoom(string password, string user_data) => (RailResult) RAIL_API_PINVOKE.IRailRoom_AsyncJoinRoom(swigCPtr_, password, user_data);

    public virtual RailResult AsyncGetAllRoomData(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailRoom_AsyncGetAllRoomData(swigCPtr_, user_data);

    public virtual RailResult AsyncKickOffMember(RailID member_id, string user_data)
    {
      IntPtr num = member_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (member_id != null)
        RailConverter.Csharp2Cpp(member_id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailRoom_AsyncKickOffMember(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual bool GetRoomMetadata(string key, out string value)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailRoom_GetRoomMetadata(swigCPtr_, key, num);
      }
      finally
      {
        value = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual bool SetRoomMetadata(string key, string value) => RAIL_API_PINVOKE.IRailRoom_SetRoomMetadata(swigCPtr_, key, value);

    public virtual RailResult AsyncSetRoomMetadata(List<RailKeyValue> key_values, string user_data)
    {
      IntPtr num = key_values == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0();
      if (key_values != null)
        RailConverter.Csharp2Cpp(key_values, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailRoom_AsyncSetRoomMetadata(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(num);
      }
    }

    public virtual RailResult AsyncGetRoomMetadata(List<string> keys, string user_data)
    {
      IntPtr num = keys == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      if (keys != null)
        RailConverter.Csharp2Cpp(keys, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailRoom_AsyncGetRoomMetadata(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual RailResult AsyncClearRoomMetadata(List<string> keys, string user_data)
    {
      IntPtr num = keys == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      if (keys != null)
        RailConverter.Csharp2Cpp(keys, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailRoom_AsyncClearRoomMetadata(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual bool GetMemberMetadata(RailID member_id, string key, out string value)
    {
      IntPtr num1 = member_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (member_id != null)
        RailConverter.Csharp2Cpp(member_id, num1);
      IntPtr num2 = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailRoom_GetMemberMetadata(swigCPtr_, num1, key, num2);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        value = RAIL_API_PINVOKE.RailString_c_str(num2);
        RAIL_API_PINVOKE.delete_RailString(num2);
      }
    }

    public virtual bool SetMemberMetadata(RailID member_id, string key, string value)
    {
      IntPtr num = member_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (member_id != null)
        RailConverter.Csharp2Cpp(member_id, num);
      try
      {
        return RAIL_API_PINVOKE.IRailRoom_SetMemberMetadata(swigCPtr_, num, key, value);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult AsyncGetMemberMetadata(
      RailID member_id,
      List<string> keys,
      string user_data)
    {
      IntPtr num1 = member_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (member_id != null)
        RailConverter.Csharp2Cpp(member_id, num1);
      IntPtr num2 = keys == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      if (keys != null)
        RailConverter.Csharp2Cpp(keys, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailRoom_AsyncGetMemberMetadata(swigCPtr_, num1, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailArrayRailString(num2);
      }
    }

    public virtual RailResult AsyncSetMemberMetadata(
      RailID member_id,
      List<RailKeyValue> key_values,
      string user_data)
    {
      IntPtr num1 = member_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (member_id != null)
        RailConverter.Csharp2Cpp(member_id, num1);
      IntPtr num2 = key_values == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0();
      if (key_values != null)
        RailConverter.Csharp2Cpp(key_values, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailRoom_AsyncSetMemberMetadata(swigCPtr_, num1, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(num2);
      }
    }

    public virtual RailResult SendDataToMember(
      byte[] data,
      uint data_len,
      uint message_type,
      RailID remote_peer)
    {
      IntPtr num = remote_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (remote_peer != null)
        RailConverter.Csharp2Cpp(remote_peer, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailRoom_SendDataToMember__SWIG_0(swigCPtr_, data, data_len, message_type, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult SendDataToMember(byte[] data, uint data_len, uint message_type) => (RailResult) RAIL_API_PINVOKE.IRailRoom_SendDataToMember__SWIG_1(swigCPtr_, data, data_len, message_type);

    public virtual RailResult SendDataToMember(byte[] data, uint data_len) => (RailResult) RAIL_API_PINVOKE.IRailRoom_SendDataToMember__SWIG_2(swigCPtr_, data, data_len);

    public virtual uint GetNumOfMembers() => RAIL_API_PINVOKE.IRailRoom_GetNumOfMembers(swigCPtr_);

    public virtual RailID GetMemberByIndex(uint index)
    {
      IntPtr memberByIndex1 = RAIL_API_PINVOKE.IRailRoom_GetMemberByIndex(swigCPtr_, index);
      RailID memberByIndex2 = new RailID();
      RailID ret = memberByIndex2;
      RailConverter.Cpp2Csharp(memberByIndex1, ret);
      return memberByIndex2;
    }

    public virtual RailResult GetMemberNameByIndex(uint index, out string name)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailRoom_GetMemberNameByIndex(swigCPtr_, index, num);
      }
      finally
      {
        name = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual uint GetMaxMembers() => RAIL_API_PINVOKE.IRailRoom_GetMaxMembers(swigCPtr_);

    public virtual bool SetGameServerID(ulong game_server_rail_id) => RAIL_API_PINVOKE.IRailRoom_SetGameServerID(swigCPtr_, game_server_rail_id);

    public virtual bool SetGameServerChannelID(ulong game_server_channel_id) => RAIL_API_PINVOKE.IRailRoom_SetGameServerChannelID(swigCPtr_, game_server_channel_id);

    public virtual bool GetGameServerID(out ulong game_server_rail_id) => RAIL_API_PINVOKE.IRailRoom_GetGameServerID(swigCPtr_, out game_server_rail_id);

    public virtual bool GetGameServerChannelID(out ulong game_server_channel_id) => RAIL_API_PINVOKE.IRailRoom_GetGameServerChannelID(swigCPtr_, out game_server_channel_id);

    public virtual bool SetRoomJoinable(bool is_joinable) => RAIL_API_PINVOKE.IRailRoom_SetRoomJoinable(swigCPtr_, is_joinable);

    public virtual bool GetRoomJoinable() => RAIL_API_PINVOKE.IRailRoom_GetRoomJoinable(swigCPtr_);

    public virtual RailResult GetFriendsInRoom(List<RailID> friend_ids)
    {
      IntPtr num = friend_ids == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailRoom_GetFriendsInRoom(swigCPtr_, num);
      }
      finally
      {
        if (friend_ids != null)
          RailConverter.Cpp2Csharp(num, friend_ids);
        RAIL_API_PINVOKE.delete_RailArrayRailID(num);
      }
    }

    public virtual bool IsUserInRoom(RailID user_rail_id)
    {
      IntPtr num = user_rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (user_rail_id != null)
        RailConverter.Csharp2Cpp(user_rail_id, num);
      try
      {
        return RAIL_API_PINVOKE.IRailRoom_IsUserInRoom(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult EnableTeamVoice(bool enable) => (RailResult) RAIL_API_PINVOKE.IRailRoom_EnableTeamVoice(swigCPtr_, enable);

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
