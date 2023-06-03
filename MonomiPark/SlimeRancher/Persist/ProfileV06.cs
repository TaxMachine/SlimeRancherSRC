// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ProfileV06
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ProfileV06 : VersionedPersistedDataSet<ProfileV05>
  {
    public PlayerAchievementsV03 achievements = new PlayerAchievementsV03();
    public string continueGameName = string.Empty;
    public DLCV02 DLC = new DLCV02();

    public override string Identifier => "SRPF";

    public override uint Version => 6;

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

    protected override void UpgradeFrom(ProfileV05 legacyData)
    {
      achievements = legacyData.achievements;
      continueGameName = legacyData.continueGameName;
      DLC = new DLCV02();
    }
  }
}
