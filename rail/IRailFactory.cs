// Decompiled with JetBrains decompiler
// Type: rail.IRailFactory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailFactory
  {
    IRailPlayer RailPlayer();

    IRailUsersHelper RailUsersHelper();

    IRailFriends RailFriends();

    IRailFloatingWindow RailFloatingWindow();

    IRailBrowserHelper RailBrowserHelper();

    IRailInGamePurchase RailInGamePurchase();

    IRailZoneHelper RailZoneHelper();

    IRailRoomHelper RailRoomHelper();

    IRailGameServerHelper RailGameServerHelper();

    IRailStorageHelper RailStorageHelper();

    IRailUserSpaceHelper RailUserSpaceHelper();

    IRailStatisticHelper RailStatisticHelper();

    IRailLeaderboardHelper RailLeaderboardHelper();

    IRailAchievementHelper RailAchievementHelper();

    IRailNetChannel RailNetChannelHelper();

    IRailNetwork RailNetworkHelper();

    IRailApps RailApps();

    IRailUtils RailUtils();

    IRailAssetsHelper RailAssetsHelper();

    IRailDlcHelper RailDlcHelper();

    IRailScreenshotHelper RailScreenshotHelper();

    IRailVoiceHelper RailVoiceHelper();

    IRailSystemHelper RailSystemHelper();
  }
}
