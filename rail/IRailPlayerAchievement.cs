// Decompiled with JetBrains decompiler
// Type: rail.IRailPlayerAchievement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailPlayerAchievement : IRailComponent
  {
    RailID GetRailID();

    RailResult AsyncRequestAchievement(string user_data);

    RailResult HasAchieved(string name, out bool achieved);

    RailResult GetAchievementInfo(string name, out string achievement_info);

    RailResult AsyncTriggerAchievementProgress(
      string name,
      uint current_value,
      uint max_value,
      string user_data);

    RailResult AsyncTriggerAchievementProgress(string name, uint current_value, uint max_value);

    RailResult AsyncTriggerAchievementProgress(string name, uint current_value);

    RailResult MakeAchievement(string name);

    RailResult CancelAchievement(string name);

    RailResult AsyncStoreAchievement(string user_data);

    RailResult ResetAllAchievements();

    RailResult GetAllAchievementsName(List<string> names);
  }
}
