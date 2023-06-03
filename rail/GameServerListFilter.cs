// Decompiled with JetBrains decompiler
// Type: rail.GameServerListFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public class GameServerListFilter
  {
    public string tags_not_contained;
    public string filter_game_server_name;
    public EnumRailOptionalValue filter_delicated_server;
    public ulong filter_zone_id;
    public string tags_contained;
    public EnumRailOptionalValue filter_password;
    public List<GameServerListFilterKey> filters = new List<GameServerListFilterKey>();
    public string filter_game_server_map;
    public EnumRailOptionalValue filter_friends_created;
    public RailID owner_id = new RailID();
  }
}
