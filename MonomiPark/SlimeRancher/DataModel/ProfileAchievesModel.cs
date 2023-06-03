// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.ProfileAchievesModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class ProfileAchievesModel
  {
    public Dictionary<AchievementsDirector.BoolStat, bool> boolStatDict = new Dictionary<AchievementsDirector.BoolStat, bool>();
    public Dictionary<AchievementsDirector.IntStat, int> intStatDict = new Dictionary<AchievementsDirector.IntStat, int>();
    public Dictionary<AchievementsDirector.EnumStat, HashSet<Enum>> enumStatDict = new Dictionary<AchievementsDirector.EnumStat, HashSet<Enum>>();
    public HashSet<AchievementsDirector.Achievement> earnedAchievements = new HashSet<AchievementsDirector.Achievement>();
    private Participant participant;

    public void SetParticipant(Participant participant) => this.participant = participant;

    public void Init()
    {
      if (participant == null)
        return;
      participant.InitModel(this);
    }

    public void NotifyParticipants()
    {
      if (participant == null)
        return;
      participant.SetModel(this);
    }

    public void Reset()
    {
      boolStatDict = new Dictionary<AchievementsDirector.BoolStat, bool>();
      intStatDict = new Dictionary<AchievementsDirector.IntStat, int>();
      enumStatDict = new Dictionary<AchievementsDirector.EnumStat, HashSet<Enum>>();
      earnedAchievements = new HashSet<AchievementsDirector.Achievement>();
    }

    public void Push(
      Dictionary<AchievementsDirector.BoolStat, bool> boolStatDict,
      Dictionary<AchievementsDirector.IntStat, int> intStatDict,
      Dictionary<AchievementsDirector.EnumStat, List<Enum>> enumStatDict,
      List<AchievementsDirector.Achievement> earnedAchievements)
    {
      this.boolStatDict = new Dictionary<AchievementsDirector.BoolStat, bool>(boolStatDict);
      this.intStatDict = new Dictionary<AchievementsDirector.IntStat, int>(intStatDict);
      this.enumStatDict = new Dictionary<AchievementsDirector.EnumStat, HashSet<Enum>>();
      foreach (KeyValuePair<AchievementsDirector.EnumStat, List<Enum>> keyValuePair in enumStatDict)
        this.enumStatDict[keyValuePair.Key] = new HashSet<Enum>(keyValuePair.Value);
      this.earnedAchievements = new HashSet<AchievementsDirector.Achievement>(earnedAchievements);
      AchievementsDirector.SyncAchievements(this);
    }

    public void Pull(
      out Dictionary<AchievementsDirector.BoolStat, bool> boolStatDict,
      out Dictionary<AchievementsDirector.IntStat, int> intStatDict,
      out Dictionary<AchievementsDirector.EnumStat, List<Enum>> enumStatDict,
      out List<AchievementsDirector.Achievement> earnedAchievements)
    {
      boolStatDict = new Dictionary<AchievementsDirector.BoolStat, bool>(this.boolStatDict);
      intStatDict = new Dictionary<AchievementsDirector.IntStat, int>(this.intStatDict);
      enumStatDict = new Dictionary<AchievementsDirector.EnumStat, List<Enum>>();
      foreach (KeyValuePair<AchievementsDirector.EnumStat, HashSet<Enum>> keyValuePair in this.enumStatDict)
        enumStatDict[keyValuePair.Key] = new List<Enum>(keyValuePair.Value);
      earnedAchievements = new List<AchievementsDirector.Achievement>(this.earnedAchievements);
    }

    public interface Participant
    {
      void InitModel(ProfileAchievesModel model);

      void SetModel(ProfileAchievesModel model);
    }
  }
}
