// Decompiled with JetBrains decompiler
// Type: rail.IRailFriends
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailFriends
  {
    RailResult AsyncGetPersonalInfo(List<RailID> rail_ids, string user_data);

    RailResult AsyncGetFriendMetadata(RailID rail_id, List<string> keys, string user_data);

    RailResult AsyncSetMyMetadata(List<RailKeyValue> key_values, string user_data);

    RailResult AsyncClearAllMyMetadata(string user_data);

    RailResult AsyncSetInviteCommandLine(string command_line, string user_data);

    RailResult AsyncGetInviteCommandLine(RailID rail_id, string user_data);

    RailResult AsyncReportPlayedWithUserList(List<RailUserPlayedWith> player_list, string user_data);

    EnumRailFriendType GetFriendType(RailID rail_id);

    RailResult GetFriendsList(List<RailID> friends_list);
  }
}
