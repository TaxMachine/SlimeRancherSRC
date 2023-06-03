// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.SavedProfile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Persist;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MonomiPark.SlimeRancher
{
  public class SavedProfile
  {
    private SettingsV01 currentSettings;
    private ProfileV07 currentProfile;
    private string continueGameName = "";

    public SettingsV01 Settings => currentSettings;

    public ProfileV07 Profile => currentProfile;

    public string ContinueGameName
    {
      get => continueGameName;
      set => continueGameName = value;
    }

    public void LoadProfile(Stream stream)
    {
      Log.Debug("Loading profile.");
      ProfileV07 profileV07 = new ProfileV07();
      try
      {
        profileV07.Load(stream);
        currentProfile = profileV07;
        Log.Debug("Loaded profile.", "achievement count", profileV07.achievements.earnedAchievements.Count, "continue game", profileV07.continueGameName);
      }
      catch (Exception ex)
      {
        Log.Warning("Failed to load profile.", "Error Message", ex.Message, "Error", ex.ToString());
      }
    }

    public void SaveProfile(Stream stream)
    {
      Log.Debug("Saving profile.", "achievement count", currentProfile.achievements.earnedAchievements.Count, "continue game", currentProfile.continueGameName);
      currentProfile.Write(stream);
    }

    public void LoadSettings(Stream stream)
    {
      Log.Debug("Loading settings.");
      SettingsV01 settingsV01 = new SettingsV01();
      try
      {
        settingsV01.Load(stream);
        currentSettings = settingsV01;
      }
      catch (Exception ex)
      {
        Log.Warning("Failed to load settings.", "Error Message", ex.Message, "Error", ex.ToString());
      }
    }

    public void SaveSettings(Stream stream)
    {
      Log.Debug("Saving settings.");
      currentSettings.Write(stream);
    }

    public void LoadLegacySettings(Stream stream)
    {
      Log.Debug("Attempting to load settings from legacy profile file.");
      ProfileV04 legacyProfile = new ProfileV04();
      SettingsV01 settingsV01 = new SettingsV01();
      try
      {
        legacyProfile.Load(stream);
        settingsV01.SetLegacyProfileOptions(legacyProfile);
        currentSettings = settingsV01;
        Log.Debug("Loaded legacy settings.");
      }
      catch (Exception ex)
      {
        Log.Warning("Failed to load legacy profile.");
      }
    }

    public void Push()
    {
      if (currentProfile == null)
      {
        Log.Debug("No profile was set. Using default profile.");
        currentProfile = new ProfileV07();
      }
      if (currentSettings == null)
      {
        Log.Debug("No settings were set. Using default settings.");
        currentSettings = new SettingsV01();
        if ((bool) (UnityEngine.Object) SRSingleton<GameContext>.Instance && (bool) (UnityEngine.Object) SRSingleton<GameContext>.Instance.MessageDirector)
          currentSettings.options.language = (int) SRSingleton<GameContext>.Instance.MessageDirector.GetCultureLang();
      }
      PushOptions(currentSettings.options);
      PushAchievements(currentProfile.achievements);
      continueGameName = currentProfile.continueGameName;
    }

    private void PushOptions(OptionsV12 options)
    {
      if (options == null)
      {
        Log.Warning("Options data was null.");
      }
      else
      {
        InputDirector inputDirector = SRSingleton<GameContext>.Instance.InputDirector;
        OptionsDirector optionsDirector = SRSingleton<GameContext>.Instance.OptionsDirector;
        PushBindings(options.bindings, inputDirector);
        inputDirector.SetDisableGamepad(options.disableGamepad);
        inputDirector.SetSwapSticks(options.swapSticks);
        inputDirector.SetInvertGamepadLookY(options.invertGamepadLookY);
        inputDirector.SetInvertMouseLookY(options.invertMouseLookY);
        inputDirector.SetDisableMouseLookSmooth(options.disableMouseLookSmooth);
        inputDirector.MouseLookSensitivity = options.mouseSensitivity;
        inputDirector.GamepadLookSensitivityX = options.lookSensitivityX;
        inputDirector.GamepadLookSensitivityY = options.lookSensitivityY;
        inputDirector.ControllerStickDeadZone = options.controllerStickDeadZone;
        optionsDirector.disableCameraBob = options.disableCameraBob;
        optionsDirector.enabledTutorials = options.enabledTutorials;
        optionsDirector.bugReportEmail = options.bugReportEmail;
        optionsDirector.bufferForGif = options.bufferForGif;
        optionsDirector.vacLockOnHold = options.vacLockOnHold;
        optionsDirector.SetFOV(options.fieldOfView);
        optionsDirector.sprintHold = options.sprintHold;
        optionsDirector.enableVsync = options.enableVsync;
        optionsDirector.UpdateVsync();
        optionsDirector.SetOverscanAdjustment(options.overscanAdjustment);
        optionsDirector.SetShowMinimalHUD(options.showMinimalHUD);
        Log.Debug("Restoring volume.", "Music", options.musicVolume, "SFX", options.sfxVolume, "Master", options.masterVolume);
        SECTR_AudioBus masterBus = SECTR_AudioSystem.System.MasterBus;
        foreach (SECTR_AudioBus child in masterBus.Children)
        {
          if (child.name == "Music")
            child.UserVolume = options.musicVolume;
          else if (child.name == "SFX")
            child.UserVolume = options.sfxVolume;
          else
            Log.Warning("Unknown top-level bus name: " + child.name);
        }
        masterBus.UserVolume = options.masterVolume;
        if (options.screenWidth < 800 || options.screenHeight < 600)
          optionsDirector.SetScreenResolution(800, 600, options.fullScreen);
        else
          optionsDirector.SetScreenResolution(options.screenWidth, options.screenHeight, options.fullScreen);
        PushQualitySettings(options.qualitySettings, options.qualityLevel);
        SRSingleton<GameContext>.Instance.MessageDirector.SetCulture((MessageDirector.Lang) options.language);
      }
    }

    private void PushQualitySettings(QualitySettingsV02 qualitySettings, int qualityLevel) => SRQualitySettings.Push(new SRQualitySettings.Settings((SRQualitySettings.LightingLevel) qualitySettings.lighting, (SRQualitySettings.TextureLevel) qualitySettings.textures, (SRQualitySettings.AntialiasingMode) qualitySettings.antialiasing, (SRQualitySettings.ShadowsLevel) qualitySettings.shadows, (SRQualitySettings.ParticlesLevel) qualitySettings.particles, (SRQualitySettings.ModelDetailLevel) qualitySettings.modelDetail, (SRQualitySettings.WaterDetailLevel) qualitySettings.waterDetail, qualitySettings.ambientOcclusion, qualitySettings.bloom), (SRQualitySettings.Level) qualityLevel);

    private void PushBindings(BindingsV05 bindingsData, InputDirector inputDir)
    {
      inputDir.ResetKeyMouseDefaults();
      inputDir.ResetGamepadDefaults();
      if (bindingsData == null || bindingsData.bindings == null)
        return;
      foreach (BindingV01 binding in bindingsData.bindings)
      {
        PlayerAction action = SRInput.Actions.Get(binding.action) ?? SRInput.PauseActions.Get(binding.action);
        if (action == null)
          Log.Warning("Ignoring the binding for unknown action: " + binding.action);
        else
          SRInput.AddOrReplaceBinding(action, binding);
      }
    }

    private void PushAchievements(PlayerAchievementsV03 achievements)
    {
      if (achievements == null)
      {
        Log.Warning("Achievements data was null.");
      }
      else
      {
        Dictionary<AchievementsDirector.BoolStat, bool> boolStatDict = new Dictionary<AchievementsDirector.BoolStat, bool>();
        foreach (KeyValuePair<int, bool> keyValuePair in achievements.progress.events)
          boolStatDict.Add((AchievementsDirector.BoolStat) keyValuePair.Key, keyValuePair.Value);
        Dictionary<AchievementsDirector.IntStat, int> intStatDict = new Dictionary<AchievementsDirector.IntStat, int>();
        foreach (KeyValuePair<int, int> count in achievements.progress.counts)
          intStatDict.Add((AchievementsDirector.IntStat) count.Key, count.Value);
        Dictionary<AchievementsDirector.EnumStat, List<Enum>> enumStatDict = new Dictionary<AchievementsDirector.EnumStat, List<Enum>>();
        foreach (KeyValuePair<int, List<Enum>> list in achievements.progress.lists)
        {
          List<Enum> enumList = new List<Enum>();
          foreach (Enum @enum in list.Value)
            enumList.Add(@enum);
          enumStatDict.Add((AchievementsDirector.EnumStat) list.Key, enumList);
        }
        List<AchievementsDirector.Achievement> earnedAchievements = new List<AchievementsDirector.Achievement>();
        foreach (int earnedAchievement in achievements.earnedAchievements)
          earnedAchievements.Add((AchievementsDirector.Achievement) earnedAchievement);
        ProfileAchievesModel profileAchievesModel = SRSingleton<SceneContext>.Instance.GameModel.GetProfileAchievesModel();
        SRSingleton<SceneContext>.Instance.AchievementsDirector.InitModel(profileAchievesModel);
        profileAchievesModel.Push(boolStatDict, intStatDict, enumStatDict, earnedAchievements);
        SRSingleton<SceneContext>.Instance.AchievementsDirector.SetModel(profileAchievesModel);
      }
    }

    public void Pull()
    {
      ProfileV07 profileV07 = new ProfileV07();
      profileV07.achievements = new PlayerAchievementsV03();
      PullAchievements(profileV07.achievements);
      profileV07.continueGameName = continueGameName;
      profileV07.DLC = new DLCV02()
      {
        installed = new List<DLCPackage.Id>(SRSingleton<GameContext>.Instance.DLCDirector.Installed)
      };
      Log.Debug("Profile pulled", "achievement count", profileV07.achievements.earnedAchievements.Count, "continue game", profileV07.continueGameName);
      SettingsV01 settingsV01 = new SettingsV01();
      settingsV01.options = new OptionsV12();
      PullOptions(settingsV01.options);
      currentProfile = profileV07;
      currentSettings = settingsV01;
    }

    private void PullOptions(OptionsV12 options)
    {
      InputDirector inputDirector = SRSingleton<GameContext>.Instance.InputDirector;
      OptionsDirector optionsDirector = SRSingleton<GameContext>.Instance.OptionsDirector;
      options.swapSticks = inputDirector.GetSwapSticks();
      options.invertGamepadLookY = inputDirector.GetInvertGamepadLookY();
      options.invertMouseLookY = inputDirector.GetInvertMouseLookY();
      options.disableMouseLookSmooth = inputDirector.GetDisableMouseLookSmooth();
      options.mouseSensitivity = inputDirector.MouseLookSensitivity;
      options.lookSensitivityX = inputDirector.GamepadLookSensitivityX;
      options.lookSensitivityY = inputDirector.GamepadLookSensitivityY;
      options.controllerStickDeadZone = inputDirector.ControllerStickDeadZone;
      options.disableGamepad = inputDirector.GetDisableGamepad();
      options.disableCameraBob = optionsDirector.disableCameraBob;
      options.bufferForGif = optionsDirector.bufferForGif;
      options.vacLockOnHold = optionsDirector.vacLockOnHold;
      options.fieldOfView = optionsDirector.GetFOV();
      options.sprintHold = optionsDirector.sprintHold;
      options.enableVsync = optionsDirector.enableVsync;
      options.overscanAdjustment = optionsDirector.GetOverscanAdjustment();
      options.bugReportEmail = optionsDirector.bugReportEmail;
      options.enabledTutorials = optionsDirector.enabledTutorials;
      options.showMinimalHUD = optionsDirector.GetShowMinimalHUD();
      SECTR_AudioBus masterBus = SECTR_AudioSystem.System.MasterBus;
      foreach (SECTR_AudioBus child in masterBus.Children)
      {
        if (child.name == "Music")
          options.musicVolume = child.UserVolume;
        else if (child.name == "SFX")
          options.sfxVolume = child.UserVolume;
        else
          Log.Warning("Unknown top-level bus name: " + child.name);
        Log.Debug("Retrieving volume from master bus", "name", child.name, "value", child.UserVolume);
      }
      options.masterVolume = masterBus.UserVolume;
      Log.Debug("Updating volume.", "Music", options.musicVolume, "SFX", options.sfxVolume, "Master", options.masterVolume);
      QualitySettingsV02 qualitySettingsV02 = new QualitySettingsV02();
      SRQualitySettings.Settings settings = null;
      SRQualitySettings.Level overallLevel = SRQualitySettings.Level.DEFAULT;
      SRQualitySettings.Pull(out settings, out overallLevel);
      options.qualityLevel = (int) overallLevel;
      qualitySettingsV02.lighting = (int) settings.lighting;
      qualitySettingsV02.antialiasing = (int) settings.antialiasing;
      qualitySettingsV02.shadows = (int) settings.shadows;
      qualitySettingsV02.particles = (int) settings.particles;
      qualitySettingsV02.textures = (int) settings.textures;
      qualitySettingsV02.modelDetail = (int) settings.modelDetail;
      qualitySettingsV02.waterDetail = (int) settings.waterDetail;
      qualitySettingsV02.ambientOcclusion = settings.ambientOcclusion;
      qualitySettingsV02.bloom = settings.bloom;
      options.qualitySettings = qualitySettingsV02;
      if (!Application.isEditor)
      {
        options.fullScreen = Screen.fullScreen;
        options.screenWidth = Screen.width;
        options.screenHeight = Screen.height;
        options.refreshRate = Screen.currentResolution.refreshRate;
      }
      options.bindings = new BindingsV05();
      PullBindings(options.bindings, SRInput.Actions.Actions);
      PullBindings(options.bindings, SRInput.PauseActions.Actions);
      options.language = (int) SRSingleton<GameContext>.Instance.MessageDirector.GetCultureLang();
    }

    private void PullBindings(BindingsV05 bindings, IEnumerable<PlayerAction> actions)
    {
      foreach (PlayerAction action in actions)
      {
        if (!SRInput.IsProtected(action))
        {
          BindingV01 binding = SRInput.ToBinding(action);
          if (!SRInput.IsProtected((Key) binding.primKey, (Key) binding.secKey))
            bindings.bindings.Add(binding);
        }
      }
    }

    private void PullAchievements(PlayerAchievementsV03 achievements)
    {
      Dictionary<AchievementsDirector.BoolStat, bool> boolStatDict;
      Dictionary<AchievementsDirector.IntStat, int> intStatDict;
      Dictionary<AchievementsDirector.EnumStat, List<Enum>> enumStatDict;
      List<AchievementsDirector.Achievement> earnedAchievements;
      SRSingleton<SceneContext>.Instance.GameModel.GetProfileAchievesModel().Pull(out boolStatDict, out intStatDict, out enumStatDict, out earnedAchievements);
      achievements.earnedAchievements = new List<int>();
      foreach (AchievementsDirector.Achievement achievement in earnedAchievements)
        achievements.earnedAchievements.Add((int) achievement);
      PlayerAchievementProgressV01 achievementProgressV01 = new PlayerAchievementProgressV01();
      achievementProgressV01.counts = new Dictionary<int, int>();
      achievementProgressV01.events = new Dictionary<int, bool>();
      achievementProgressV01.lists = new Dictionary<int, List<Enum>>();
      foreach (KeyValuePair<AchievementsDirector.IntStat, int> keyValuePair in intStatDict)
        achievementProgressV01.counts.Add((int) keyValuePair.Key, keyValuePair.Value);
      foreach (KeyValuePair<AchievementsDirector.BoolStat, bool> keyValuePair in boolStatDict)
        achievementProgressV01.events.Add((int) keyValuePair.Key, keyValuePair.Value);
      foreach (KeyValuePair<AchievementsDirector.EnumStat, List<Enum>> keyValuePair in enumStatDict)
      {
        List<Enum> enumList = new List<Enum>();
        foreach (Enum @enum in keyValuePair.Value)
          enumList.Add(@enum);
        achievementProgressV01.lists.Add((int) keyValuePair.Key, enumList);
      }
      achievements.progress = achievementProgressV01;
    }
  }
}
