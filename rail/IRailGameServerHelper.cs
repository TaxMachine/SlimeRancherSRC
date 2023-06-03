// Decompiled with JetBrains decompiler
// Type: rail.IRailGameServerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailGameServerHelper
  {
    RailResult AsyncGetGameServerPlayerList(RailID gameserver_rail_id, string user_data);

    RailResult AsyncGetGameServerList(
      uint start_index,
      uint end_index,
      List<GameServerListFilter> alternative_filters,
      List<GameServerListSorter> sorter,
      string user_data);

    IRailGameServer AsyncCreateGameServer(
      CreateGameServerOptions options,
      string game_server_name,
      string user_data);

    IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options, string game_server_name);

    IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options);

    IRailGameServer AsyncCreateGameServer();

    RailResult AsyncGetFavoriteGameServers(string user_data);

    RailResult AsyncGetFavoriteGameServers();

    RailResult AsyncAddFavoriteGameServer(RailID game_server_id, string user_data);

    RailResult AsyncAddFavoriteGameServer(RailID game_server_id);

    RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id, string user_Data);

    RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id);
  }
}
