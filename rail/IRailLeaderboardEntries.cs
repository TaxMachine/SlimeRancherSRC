// Decompiled with JetBrains decompiler
// Type: rail.IRailLeaderboardEntries
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailLeaderboardEntries : IRailComponent
  {
    RailID GetRailID();

    string GetLeaderboardName();

    RailResult AsyncRequestLeaderboardEntries(
      RailID player,
      RequestLeaderboardEntryParam param,
      string user_data);

    RequestLeaderboardEntryParam GetEntriesParam();

    int GetEntriesCount();

    RailResult GetLeaderboardEntry(int index, LeaderboardEntry leaderboard_entry);
  }
}
