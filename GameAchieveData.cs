// Decompiled with JetBrains decompiler
// Type: GameAchieveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

[Serializable]
public class GameAchieveData : DataModule<GameAchieveData>
{
  public const int CURR_FORMAT_ID = 1;
  public Dictionary<AchievementsDirector.GameFloatStat, float> gameFloatStatDict = new Dictionary<AchievementsDirector.GameFloatStat, float>();
  public Dictionary<AchievementsDirector.GameIntStat, int> gameIntStatDict = new Dictionary<AchievementsDirector.GameIntStat, int>();
  public Dictionary<AchievementsDirector.GameIdDictStat, Dictionary<Identifiable.Id, int>> gameIdDictStatDict = new Dictionary<AchievementsDirector.GameIdDictStat, Dictionary<Identifiable.Id, int>>();

  public static void AssertEquals(GameAchieveData dataA, GameAchieveData dataB)
  {
    foreach (int key in dataA.gameIdDictStatDict.Keys)
      ;
  }
}
