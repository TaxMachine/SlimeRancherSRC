// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ProfileV07
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ProfileV07 : VersionedPersistedDataSet_Profile<ProfileV06>
  {
    public PlayerAchievementsV03 achievements = new PlayerAchievementsV03();
    public string continueGameName = string.Empty;
    public DLCV02 DLC = new DLCV02();

    public override uint Version => 7;

    protected override void LoadData(BinaryReader reader)
    {
      achievements = new PlayerAchievementsV03();
      achievements.Load(reader.BaseStream);
      continueGameName = reader.ReadString();
      DLC = LoadPersistable<DLCV02>(reader);
    }

    protected override void WriteData(BinaryWriter writer)
    {
      achievements.Write(writer.BaseStream);
      writer.Write(continueGameName);
      WritePersistable(writer, DLC);
    }

    protected override void UpgradeFrom(ProfileV06 previous)
    {
      base.UpgradeFrom(previous);
      achievements = previous.achievements;
      continueGameName = previous.continueGameName;
      DLC = previous.DLC;
    }
  }
}
