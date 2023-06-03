// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PlayerAchievementsV03
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PlayerAchievementsV03 : PersistedDataSet
  {
    public List<int> earnedAchievements = new List<int>();
    public PlayerAchievementProgressV01 progress = new PlayerAchievementProgressV01();

    public override string Identifier => "SRPA";

    public override uint Version => 3;

    protected override void LoadData(BinaryReader reader)
    {
      ReadAchievements(reader);
      ReadSectionSeparator(reader);
      progress = new PlayerAchievementProgressV01();
      progress.Load(reader.BaseStream);
    }

    private void ReadAchievements(BinaryReader reader)
    {
      int num = reader.ReadInt32();
      earnedAchievements.Clear();
      for (; num > 0; --num)
        earnedAchievements.Add(reader.ReadInt32());
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteAchievements(writer);
      WriteSectionSeparator(writer);
      progress.Write(writer.BaseStream);
    }

    private void WriteAchievements(BinaryWriter writer)
    {
      writer.Write(earnedAchievements.Count);
      foreach (int earnedAchievement in earnedAchievements)
        writer.Write(earnedAchievement);
    }
  }
}
