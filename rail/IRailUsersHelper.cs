// Decompiled with JetBrains decompiler
// Type: rail.IRailUsersHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailUsersHelper
  {
    RailResult AsyncGetUsersInfo(List<RailID> rail_ids, string user_data);

    RailResult AsyncInviteUsers(
      string command_line,
      List<RailID> users,
      RailInviteOptions options,
      string user_data);

    RailResult AsyncGetInviteDetail(
      RailID inviter,
      EnumRailUsersInviteType invite_type,
      string user_data);

    RailResult AsyncCancelInvite(EnumRailUsersInviteType invite_type, string user_data);

    RailResult AsyncCancelAllInvites(string user_data);
  }
}
