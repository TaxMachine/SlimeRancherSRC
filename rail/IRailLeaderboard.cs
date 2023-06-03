// Decompiled with JetBrains decompiler
// Type: rail.IRailLeaderboard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailLeaderboard : IRailComponent
  {
    string GetLeaderboardName();

    int GetTotalEntriesCount();

    RailResult AsyncGetLeaderboard(string user_data);

    RailResult GetLeaderboardParameters(LeaderboardParameters param);

    IRailLeaderboardEntries CreateLeaderboardEntries();

    RailResult AsyncUploadLeaderboard(UploadLeaderboardParam update_param, string user_data);

    RailResult GetLeaderboardSortType(out int sort_type);

    RailResult GetLeaderboardDisplayType(out int display_type);

    RailResult AsyncAttachSpaceWork(SpaceWorkID spacework_id, string user_data);
  }
}
