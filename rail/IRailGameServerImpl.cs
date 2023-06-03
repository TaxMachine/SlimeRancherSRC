// Decompiled with JetBrains decompiler
// Type: rail.IRailGameServerImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailGameServerImpl : RailObject, IRailGameServer, IRailComponent
  {
    internal IRailGameServerImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailGameServerImpl()
    {
    }

    public virtual RailID GetGameServerRailID()
    {
      IntPtr gameServerRailId1 = RAIL_API_PINVOKE.IRailGameServer_GetGameServerRailID(swigCPtr_);
      RailID gameServerRailId2 = new RailID();
      RailID ret = gameServerRailId2;
      RailConverter.Cpp2Csharp(gameServerRailId1, ret);
      return gameServerRailId2;
    }

    public virtual RailResult GetGameServerName(out string name)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServer_GetGameServerName(swigCPtr_, num);
      }
      finally
      {
        name = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult GetGameServerFullname(out string fullname)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServer_GetGameServerFullname(swigCPtr_, num);
      }
      finally
      {
        fullname = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailID GetOwnerRailID()
    {
      IntPtr ownerRailId1 = RAIL_API_PINVOKE.IRailGameServer_GetOwnerRailID(swigCPtr_);
      RailID ownerRailId2 = new RailID();
      RailID ret = ownerRailId2;
      RailConverter.Cpp2Csharp(ownerRailId1, ret);
      return ownerRailId2;
    }

    public virtual bool SetZoneID(ulong zone_id) => RAIL_API_PINVOKE.IRailGameServer_SetZoneID(swigCPtr_, zone_id);

    public virtual ulong GetZoneID() => RAIL_API_PINVOKE.IRailGameServer_GetZoneID(swigCPtr_);

    public virtual bool SetChannelID(ulong channel_id) => RAIL_API_PINVOKE.IRailGameServer_SetChannelID(swigCPtr_, channel_id);

    public virtual ulong GetChannelID() => RAIL_API_PINVOKE.IRailGameServer_GetChannelID(swigCPtr_);

    public virtual bool SetHost(string game_server_host) => RAIL_API_PINVOKE.IRailGameServer_SetHost(swigCPtr_, game_server_host);

    public virtual bool GetHost(out string game_server_host)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailGameServer_GetHost(swigCPtr_, num);
      }
      finally
      {
        game_server_host = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual bool SetMapName(string game_server_map) => RAIL_API_PINVOKE.IRailGameServer_SetMapName(swigCPtr_, game_server_map);

    public virtual bool GetMapName(out string game_server_map)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailGameServer_GetMapName(swigCPtr_, num);
      }
      finally
      {
        game_server_map = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual bool SetPasswordProtect(bool has_password) => RAIL_API_PINVOKE.IRailGameServer_SetPasswordProtect(swigCPtr_, has_password);

    public virtual bool GetPasswordProtect() => RAIL_API_PINVOKE.IRailGameServer_GetPasswordProtect(swigCPtr_);

    public virtual bool SetMaxPlayers(uint max_player_count) => RAIL_API_PINVOKE.IRailGameServer_SetMaxPlayers(swigCPtr_, max_player_count);

    public virtual uint GetMaxPlayers() => RAIL_API_PINVOKE.IRailGameServer_GetMaxPlayers(swigCPtr_);

    public virtual bool SetBotPlayers(uint bot_player_count) => RAIL_API_PINVOKE.IRailGameServer_SetBotPlayers(swigCPtr_, bot_player_count);

    public virtual uint GetBotPlayers() => RAIL_API_PINVOKE.IRailGameServer_GetBotPlayers(swigCPtr_);

    public virtual bool SetGameServerDescription(string game_server_description) => RAIL_API_PINVOKE.IRailGameServer_SetGameServerDescription(swigCPtr_, game_server_description);

    public virtual bool GetGameServerDescription(out string game_server_description)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailGameServer_GetGameServerDescription(swigCPtr_, num);
      }
      finally
      {
        game_server_description = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual bool SetGameServerTags(string game_server_tags) => RAIL_API_PINVOKE.IRailGameServer_SetGameServerTags(swigCPtr_, game_server_tags);

    public virtual bool GetGameServerTags(out string game_server_tags)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailGameServer_GetGameServerTags(swigCPtr_, num);
      }
      finally
      {
        game_server_tags = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual bool SetMods(List<string> server_mods)
    {
      IntPtr num = server_mods == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      if (server_mods != null)
        RailConverter.Csharp2Cpp(server_mods, num);
      try
      {
        return RAIL_API_PINVOKE.IRailGameServer_SetMods(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual bool GetMods(List<string> server_mods)
    {
      IntPtr num = server_mods == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailGameServer_GetMods(swigCPtr_, num);
      }
      finally
      {
        if (server_mods != null)
          RailConverter.Cpp2Csharp(num, server_mods);
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual bool SetSpectatorHost(string specator_host) => RAIL_API_PINVOKE.IRailGameServer_SetSpectatorHost(swigCPtr_, specator_host);

    public virtual bool GetSpectatorHost(out string specator_host)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailGameServer_GetSpectatorHost(swigCPtr_, num);
      }
      finally
      {
        specator_host = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual bool SetGameServerVersion(string version) => RAIL_API_PINVOKE.IRailGameServer_SetGameServerVersion(swigCPtr_, version);

    public virtual bool GetGameServerVersion(out string version)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailGameServer_GetGameServerVersion(swigCPtr_, num);
      }
      finally
      {
        version = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual bool SetIsFriendOnly(bool is_friend_only) => RAIL_API_PINVOKE.IRailGameServer_SetIsFriendOnly(swigCPtr_, is_friend_only);

    public virtual bool GetIsFriendOnly() => RAIL_API_PINVOKE.IRailGameServer_GetIsFriendOnly(swigCPtr_);

    public virtual bool ClearAllMetadata() => RAIL_API_PINVOKE.IRailGameServer_ClearAllMetadata(swigCPtr_);

    public virtual RailResult GetMetadata(string key, out string value)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServer_GetMetadata(swigCPtr_, key, num);
      }
      finally
      {
        value = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult SetMetadata(string key, string value) => (RailResult) RAIL_API_PINVOKE.IRailGameServer_SetMetadata(swigCPtr_, key, value);

    public virtual RailResult AsyncSetMetadata(List<RailKeyValue> key_values, string user_data)
    {
      IntPtr num = key_values == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0();
      if (key_values != null)
        RailConverter.Csharp2Cpp(key_values, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServer_AsyncSetMetadata(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(num);
      }
    }

    public virtual RailResult AsyncGetMetadata(List<string> keys, string user_data)
    {
      IntPtr num = keys == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      if (keys != null)
        RailConverter.Csharp2Cpp(keys, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServer_AsyncGetMetadata(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }

    public virtual RailResult AsyncGetAllMetadata(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailGameServer_AsyncGetAllMetadata(swigCPtr_, user_data);

    public virtual RailResult AsyncAcquireGameServerSessionTicket(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailGameServer_AsyncAcquireGameServerSessionTicket(swigCPtr_, user_data);

    public virtual RailResult AsyncStartSessionWithPlayer(
      RailSessionTicket player_ticket,
      RailID player_rail_id,
      string user_data)
    {
      IntPtr num1 = player_ticket == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket();
      if (player_ticket != null)
        RailConverter.Csharp2Cpp(player_ticket, num1);
      IntPtr num2 = player_rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (player_rail_id != null)
        RailConverter.Csharp2Cpp(player_rail_id, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServer_AsyncStartSessionWithPlayer(swigCPtr_, num1, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSessionTicket(num1);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual void TerminateSessionOfPlayer(RailID player_rail_id)
    {
      IntPtr num = player_rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (player_rail_id != null)
        RailConverter.Csharp2Cpp(player_rail_id, num);
      try
      {
        RAIL_API_PINVOKE.IRailGameServer_TerminateSessionOfPlayer(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual void AbandonGameServerSessionTicket(RailSessionTicket session_ticket)
    {
      IntPtr num = session_ticket == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket();
      if (session_ticket != null)
        RailConverter.Csharp2Cpp(session_ticket, num);
      try
      {
        RAIL_API_PINVOKE.IRailGameServer_AbandonGameServerSessionTicket(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSessionTicket(num);
      }
    }

    public virtual RailResult ReportPlayerJoinGameServer(List<GameServerPlayerInfo> player_infos)
    {
      IntPtr num = player_infos == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerPlayerInfo__SWIG_0();
      if (player_infos != null)
        RailConverter.Csharp2Cpp(player_infos, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServer_ReportPlayerJoinGameServer(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayGameServerPlayerInfo(num);
      }
    }

    public virtual RailResult ReportPlayerQuitGameServer(List<GameServerPlayerInfo> player_infos)
    {
      IntPtr num = player_infos == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerPlayerInfo__SWIG_0();
      if (player_infos != null)
        RailConverter.Csharp2Cpp(player_infos, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServer_ReportPlayerQuitGameServer(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayGameServerPlayerInfo(num);
      }
    }

    public virtual RailResult UpdateGameServerPlayerList(List<GameServerPlayerInfo> player_infos)
    {
      IntPtr num = player_infos == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerPlayerInfo__SWIG_0();
      if (player_infos != null)
        RailConverter.Csharp2Cpp(player_infos, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServer_UpdateGameServerPlayerList(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayGameServerPlayerInfo(num);
      }
    }

    public virtual uint GetCurrentPlayers() => RAIL_API_PINVOKE.IRailGameServer_GetCurrentPlayers(swigCPtr_);

    public virtual void RemoveAllPlayers() => RAIL_API_PINVOKE.IRailGameServer_RemoveAllPlayers(swigCPtr_);

    public virtual RailResult RegisterToGameServerList() => (RailResult) RAIL_API_PINVOKE.IRailGameServer_RegisterToGameServerList(swigCPtr_);

    public virtual RailResult UnregisterFromGameServerList() => (RailResult) RAIL_API_PINVOKE.IRailGameServer_UnregisterFromGameServerList(swigCPtr_);

    public virtual RailResult CloseGameServer() => (RailResult) RAIL_API_PINVOKE.IRailGameServer_CloseGameServer(swigCPtr_);

    public virtual RailResult GetFriendsInGameServer(List<RailID> friend_ids)
    {
      IntPtr num = friend_ids == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServer_GetFriendsInGameServer(swigCPtr_, num);
      }
      finally
      {
        if (friend_ids != null)
          RailConverter.Cpp2Csharp(num, friend_ids);
        RAIL_API_PINVOKE.delete_RailArrayRailID(num);
      }
    }

    public virtual bool IsUserInGameServer(RailID user_rail_id)
    {
      IntPtr num = user_rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (user_rail_id != null)
        RailConverter.Csharp2Cpp(user_rail_id, num);
      try
      {
        return RAIL_API_PINVOKE.IRailGameServer_IsUserInGameServer(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual bool SetServerInfo(string server_info) => RAIL_API_PINVOKE.IRailGameServer_SetServerInfo(swigCPtr_, server_info);

    public virtual bool GetServerInfo(out string server_info)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailGameServer_GetServerInfo(swigCPtr_, num);
      }
      finally
      {
        server_info = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult EnableTeamVoice(bool enable) => (RailResult) RAIL_API_PINVOKE.IRailGameServer_EnableTeamVoice(swigCPtr_, enable);

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
