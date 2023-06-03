﻿// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.OptionsV08
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class OptionsV08 : VersionedPersistedDataSet<OptionsV07>
  {
    public int screenWidth = 800;
    public int screenHeight = 600;
    public bool fullScreen = true;
    public float fieldOfView = 75f;
    public QualitySettingsV02 qualitySettings = new QualitySettingsV02();
    public int qualityLevel;
    public float masterVolume = 1f;
    public float musicVolume = 0.5f;
    public float sfxVolume = 1f;
    public bool disableCameraBob = true;
    public string bugReportEmail = "";
    public bool bufferForGif;
    public bool disableTutorials;
    public bool vacLockOnHold = true;
    public bool enableVsync;
    public bool swapSticks;
    public bool invertGamepadLookY;
    public bool invertMouseLookY;
    public bool disableGamepad;
    public float lookSensitivityX;
    public float lookSensitivityY = -0.2f;
    public float mouseSensitivity;
    public BindingsV04 bindings = new BindingsV04();
    public int language;
    public bool sprintHold;

    public override string Identifier => "SROPTIONS";

    public override uint Version => 8;

    protected override void LoadData(BinaryReader reader)
    {
      screenWidth = reader.ReadInt32();
      screenHeight = reader.ReadInt32();
      fullScreen = reader.ReadBoolean();
      fieldOfView = reader.ReadSingle();
      qualityLevel = reader.ReadInt32();
      qualitySettings = new QualitySettingsV02();
      qualitySettings.Load(reader.BaseStream);
      masterVolume = reader.ReadSingle();
      musicVolume = reader.ReadSingle();
      sfxVolume = reader.ReadSingle();
      swapSticks = reader.ReadBoolean();
      invertGamepadLookY = reader.ReadBoolean();
      invertMouseLookY = reader.ReadBoolean();
      disableGamepad = reader.ReadBoolean();
      lookSensitivityX = reader.ReadSingle();
      lookSensitivityY = reader.ReadSingle();
      mouseSensitivity = reader.ReadSingle();
      bindings = new BindingsV04();
      bindings.Load(reader.BaseStream);
      disableCameraBob = reader.ReadBoolean();
      bugReportEmail = reader.ReadString();
      bufferForGif = reader.ReadBoolean();
      disableTutorials = reader.ReadBoolean();
      vacLockOnHold = reader.ReadBoolean();
      language = reader.ReadInt32();
      sprintHold = reader.ReadBoolean();
      enableVsync = reader.ReadBoolean();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(screenWidth);
      writer.Write(screenHeight);
      writer.Write(fullScreen);
      writer.Write(fieldOfView);
      writer.Write(qualityLevel);
      qualitySettings.Write(writer.BaseStream);
      writer.Write(masterVolume);
      writer.Write(musicVolume);
      writer.Write(sfxVolume);
      writer.Write(swapSticks);
      writer.Write(invertGamepadLookY);
      writer.Write(invertMouseLookY);
      writer.Write(disableGamepad);
      writer.Write(lookSensitivityX);
      writer.Write(lookSensitivityY);
      writer.Write(mouseSensitivity);
      bindings.Write(writer.BaseStream);
      writer.Write(disableCameraBob);
      writer.Write(string.IsNullOrEmpty(bugReportEmail) ? "" : bugReportEmail);
      writer.Write(bufferForGif);
      writer.Write(disableTutorials);
      writer.Write(vacLockOnHold);
      writer.Write(language);
      writer.Write(sprintHold);
      writer.Write(enableVsync);
    }

    protected override void UpgradeFrom(OptionsV07 legacyData)
    {
      screenWidth = legacyData.screenWidth;
      screenHeight = legacyData.screenHeight;
      fullScreen = legacyData.fullScreen;
      qualitySettings = legacyData.qualitySettings;
      qualityLevel = legacyData.qualityLevel;
      masterVolume = legacyData.masterVolume;
      musicVolume = legacyData.musicVolume;
      sfxVolume = legacyData.sfxVolume;
      disableCameraBob = legacyData.disableCameraBob;
      bugReportEmail = legacyData.bugReportEmail;
      bufferForGif = legacyData.bufferForGif;
      disableTutorials = legacyData.disableTutorials;
      vacLockOnHold = legacyData.vacLockOnHold;
      swapSticks = legacyData.swapSticks;
      invertGamepadLookY = legacyData.invertGamepadLookY;
      invertMouseLookY = legacyData.invertMouseLookY;
      disableGamepad = legacyData.disableGamepad;
      lookSensitivityX = legacyData.lookSensitivityX;
      lookSensitivityY = legacyData.lookSensitivityY;
      mouseSensitivity = legacyData.mouseSensitivity;
      bindings = new BindingsV04();
      language = legacyData.language;
      sprintHold = legacyData.sprintHold;
      fieldOfView = legacyData.fieldOfView;
      enableVsync = true;
    }
  }
}
