// Decompiled with JetBrains decompiler
// Type: rail.RoomInfoList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public class RoomInfoList : EventBase
  {
    public uint total_room_num_in_zone;
    public List<RoomInfo> room_info = new List<RoomInfo>();
    public uint end_index;
    public ulong zone_id;
    public uint begin_index;
  }
}
