// Decompiled with JetBrains decompiler
// Type: rail.IRailRoomHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailRoomHelper
  {
    void set_current_zone_id(ulong zone_id);

    ulong get_current_zone_id();

    IRailRoom CreateRoom(RoomOptions options, string room_name, out int result);

    IRailRoom AsyncCreateRoom(RoomOptions options, string room_name, string user_data);

    IRailRoom OpenRoom(ulong zone_id, ulong room_id, out int result);

    RailResult AsyncGetUserRoomList(string user_data);
  }
}
