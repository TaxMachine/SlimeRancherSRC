// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ProfileV04
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ProfileV04 : VersionedPersistedDataSet<ProfileData>
  {
    public OptionsV09 options = new OptionsV09();
    public PlayerAchievementsV03 achievements = new PlayerAchievementsV03();
    public string continueGameName = "";

    public override string Identifier => "SRPF";

    public override uint Version => 4;

    protected override void LoadData(BinaryReader reader)
    {
      options = new OptionsV09();
      options.Load(reader.BaseStream);
      ReadSectionSeparator(reader);
      achievements = new PlayerAchievementsV03();
      achievements.Load(reader.BaseStream);
      continueGameName = reader.ReadString();
    }

    protected override void UpgradeFrom(ProfileData legacyData)
    {
      UpgradeOptions(legacyData.options);
      UpgradeAchievements(legacyData.achieve);
      continueGameName = string.IsNullOrEmpty(legacyData.continueGameName) ? string.Empty : legacyData.continueGameName;
    }

    private void UpgradeOptions(OptionsData legacyOptions)
    {
      options = new OptionsV09();
      options.screenWidth = legacyOptions.screenWidth;
      options.screenHeight = legacyOptions.screenHeight;
      options.fullScreen = legacyOptions.fullScreen;
      options.qualitySettings = new QualitySettingsV02();
      options.qualitySettings.lighting = (int) legacyOptions.qualitySettings.lighting;
      options.qualitySettings.textures = (int) legacyOptions.qualitySettings.textures;
      options.qualitySettings.antialiasing = (int) legacyOptions.qualitySettings.antialiasing;
      options.qualitySettings.shadows = (int) legacyOptions.qualitySettings.shadows;
      options.qualitySettings.particles = (int) legacyOptions.qualitySettings.particles;
      options.qualitySettings.modelDetail = (int) legacyOptions.qualitySettings.modelDetail;
      options.qualitySettings.waterDetail = (int) legacyOptions.qualitySettings.waterDetail;
      options.qualitySettings.ambientOcclusion = legacyOptions.qualitySettings.ambientOcclusion;
      options.qualitySettings.bloom = legacyOptions.qualitySettings.bloom;
      options.qualityLevel = (int) legacyOptions.qualityLevel;
      options.masterVolume = legacyOptions.masterVolume;
      options.musicVolume = legacyOptions.musicVolume;
      options.sfxVolume = legacyOptions.sfxVolume;
      options.disableCameraBob = legacyOptions.disableCameraBob;
      options.bugReportEmail = string.IsNullOrEmpty(legacyOptions.bugReportEmail) ? "" : legacyOptions.bugReportEmail;
      options.bufferForGif = legacyOptions.bufferForGif;
      options.disableTutorials = legacyOptions.disableTutorials;
      options.vacLockOnHold = legacyOptions.vacLockOnHold;
      options.swapSticks = legacyOptions.swapSticks;
      options.invertGamepadLookY = legacyOptions.invertGamepadLookY;
      options.invertMouseLookY = legacyOptions.invertMouseLookY;
      options.disableGamepad = legacyOptions.disableGamepad;
      options.lookSensitivityX = legacyOptions.lookSensitivityX;
      options.lookSensitivityY = legacyOptions.lookSensitivityY;
      options.mouseSensitivity = legacyOptions.mouseSensitivity;
      options.bindings = UpgradeBindings(legacyOptions.bindings);
      options.language = 0;
      options.sprintHold = true;
      options.enableVsync = true;
      options.overscanAdjustment = 0.0f;
    }

    private BindingsV04 UpgradeBindings(List<OptionsData.BindingData> legacyBindings)
    {
      BindingsV04 bindingsV04 = new BindingsV04();
      bindingsV04.bindings = new List<BindingsV04.Binding>();
      if (legacyBindings != null)
      {
        foreach (OptionsData.BindingData legacyBinding in legacyBindings)
          bindingsV04.bindings.Add(UpgradeBinding(legacyBinding));
      }
      return bindingsV04;
    }

    private BindingsV04.Binding UpgradeBinding(OptionsData.BindingData legacyBinding) => new BindingsV04.Binding()
    {
      action = legacyBinding.action,
      gamepad = (int) legacyBinding.gamepad,
      primKey = (int) legacyBinding.primKey,
      secKey = (int) legacyBinding.secondary,
      secMouse = 0,
      primMouse = (int) legacyBinding.primMouse
    };

    private void UpgradeAchievements(AchieveData legacyAchievements)
    {
      achievements = new PlayerAchievementsV03();
      achievements.earnedAchievements = new List<int>();
      foreach (int earnedAchievement in legacyAchievements.earnedAchievements)
        achievements.earnedAchievements.Add(earnedAchievement);
      PlayerAchievementProgressV01 achievementProgressV01 = new PlayerAchievementProgressV01();
      achievementProgressV01.counts = new Dictionary<int, int>();
      achievementProgressV01.events = new Dictionary<int, bool>();
      achievementProgressV01.lists = new Dictionary<int, List<System.Enum>>();
      foreach (KeyValuePair<AchievementsDirector.IntStat, int> keyValuePair in legacyAchievements.intStatDict)
        achievementProgressV01.counts.Add((int) keyValuePair.Key, keyValuePair.Value);
      foreach (KeyValuePair<AchievementsDirector.BoolStat, bool> keyValuePair in legacyAchievements.boolStatDict)
        achievementProgressV01.events.Add((int) keyValuePair.Key, keyValuePair.Value);
      foreach (KeyValuePair<AchievementsDirector.EnumStat, List<System.Enum>> keyValuePair in legacyAchievements.enumStatDict)
      {
        List<System.Enum> enumList = new List<System.Enum>();
        foreach (System.Enum @enum in keyValuePair.Value)
          enumList.Add(@enum);
        achievementProgressV01.lists.Add((int) keyValuePair.Key, enumList);
      }
      achievements.progress = achievementProgressV01;
    }

    protected override void WriteData(BinaryWriter writer)
    {
      options.Write(writer.BaseStream);
      WriteSectionSeparator(writer);
      achievements.Write(writer.BaseStream);
      writer.Write(continueGameName);
    }
  }
}
