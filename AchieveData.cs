// Decompiled with JetBrains decompiler
// Type: AchieveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

[Serializable]
public class AchieveData : DataModule<AchieveData>
{
  public const int CURR_FORMAT_ID = 2;
  public Dictionary<AchievementsDirector.BoolStat, bool> boolStatDict = new Dictionary<AchievementsDirector.BoolStat, bool>();
  public Dictionary<AchievementsDirector.IntStat, int> intStatDict = new Dictionary<AchievementsDirector.IntStat, int>();
  public Dictionary<AchievementsDirector.EnumStat, List<Enum>> enumStatDict = new Dictionary<AchievementsDirector.EnumStat, List<Enum>>();
  public List<AchievementsDirector.Achievement> earnedAchievements = new List<AchievementsDirector.Achievement>();
}
