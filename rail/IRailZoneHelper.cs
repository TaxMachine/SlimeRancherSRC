// Decompiled with JetBrains decompiler
// Type: rail.IRailZoneHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailZoneHelper
  {
    RailResult AsyncGetZoneList(string user_data);

    RailResult AsyncGetRoomListInZone(
      ulong zone_id,
      uint start_index,
      uint end_index,
      List<RoomInfoListSorter> sorter,
      List<RoomInfoListFilter> filter,
      string user_data);
  }
}
