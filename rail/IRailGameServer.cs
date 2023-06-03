// Decompiled with JetBrains decompiler
// Type: rail.IRailGameServer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailGameServer : IRailComponent
  {
    RailID GetGameServerRailID();

    RailResult GetGameServerName(out string name);

    RailResult GetGameServerFullname(out string fullname);

    RailID GetOwnerRailID();

    bool SetZoneID(ulong zone_id);

    ulong GetZoneID();

    bool SetChannelID(ulong channel_id);

    ulong GetChannelID();

    bool SetHost(string game_server_host);

    bool GetHost(out string game_server_host);

    bool SetMapName(string game_server_map);

    bool GetMapName(out string game_server_map);

    bool SetPasswordProtect(bool has_password);

    bool GetPasswordProtect();

    bool SetMaxPlayers(uint max_player_count);

    uint GetMaxPlayers();

    bool SetBotPlayers(uint bot_player_count);

    uint GetBotPlayers();

    bool SetGameServerDescription(string game_server_description);

    bool GetGameServerDescription(out string game_server_description);

    bool SetGameServerTags(string game_server_tags);

    bool GetGameServerTags(out string game_server_tags);

    bool SetMods(List<string> server_mods);

    bool GetMods(List<string> server_mods);

    bool SetSpectatorHost(string specator_host);

    bool GetSpectatorHost(out string specator_host);

    bool SetGameServerVersion(string version);

    bool GetGameServerVersion(out string version);

    bool SetIsFriendOnly(bool is_friend_only);

    bool GetIsFriendOnly();

    bool ClearAllMetadata();

    RailResult GetMetadata(string key, out string value);

    RailResult SetMetadata(string key, string value);

    RailResult AsyncSetMetadata(List<RailKeyValue> key_values, string user_data);

    RailResult AsyncGetMetadata(List<string> keys, string user_data);

    RailResult AsyncGetAllMetadata(string user_data);

    RailResult AsyncAcquireGameServerSessionTicket(string user_data);

    RailResult AsyncStartSessionWithPlayer(
      RailSessionTicket player_ticket,
      RailID player_rail_id,
      string user_data);

    void TerminateSessionOfPlayer(RailID player_rail_id);

    void AbandonGameServerSessionTicket(RailSessionTicket session_ticket);

    RailResult ReportPlayerJoinGameServer(List<GameServerPlayerInfo> player_infos);

    RailResult ReportPlayerQuitGameServer(List<GameServerPlayerInfo> player_infos);

    RailResult UpdateGameServerPlayerList(List<GameServerPlayerInfo> player_infos);

    uint GetCurrentPlayers();

    void RemoveAllPlayers();

    RailResult RegisterToGameServerList();

    RailResult UnregisterFromGameServerList();

    RailResult CloseGameServer();

    RailResult GetFriendsInGameServer(List<RailID> friend_ids);

    bool IsUserInGameServer(RailID user_rail_id);

    bool SetServerInfo(string server_info);

    bool GetServerInfo(out string server_info);

    RailResult EnableTeamVoice(bool enable);
  }
}
