// Decompiled with JetBrains decompiler
// Type: rail.IRailApps
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface IRailApps
  {
    RailResult MarkGameContentDamaged(EnumRailGameContentDamageFlag flag);

    RailResult GetGameInstallPath(out string app_path);

    RailResult GetGameLanguageCode(out string language_code);

    RailResult SetGameState(EnumRailGamePlayingState game_state_flag);

    RailResult GetGameState(out EnumRailGamePlayingState game_state_flag);

    uint GetGameEarliestPurchaseTime();

    RailResult GetCurrentBranchInfo(RailBranchInfo branch_info);

    RailResult AsyncQuerySubscribeWishPlayState(string user_data);
  }
}
