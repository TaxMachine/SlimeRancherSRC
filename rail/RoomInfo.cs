// Decompiled with JetBrains decompiler
// Type: rail.RoomInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public class RoomInfo
  {
    public ulong zone_id;
    public bool has_password;
    public uint create_time;
    public uint max_members;
    public string room_name;
    public ulong game_server_rail_id;
    public ulong room_id;
    public uint current_members;
    public bool is_joinable;
    public EnumRoomStatus room_state;
    public List<RailKeyValue> room_kvs = new List<RailKeyValue>();
    public EnumRoomType type;
    public ulong game_server_channel_id;
    public RailID owner_id = new RailID();
  }
}
