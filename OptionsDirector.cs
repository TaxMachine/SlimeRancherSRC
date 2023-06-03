// Decompiled with JetBrains decompiler
// Type: OptionsDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OptionsDirector : SRBehaviour
{
  public bool disableCameraBob = true;
  public EnabledTutorials enabledTutorials;
  public bool bufferForGif = true;
  public string bugReportEmail = "";
  public bool vacLockOnHold = true;
  public bool sprintHold;
  public bool enableVsync = true;
  private bool showMinimalHud;
  private float fov = 75f;
  private float overscanAdjustment;
  private const bool DEFAULT_ENABLED_VSYNC = true;
  public int screenWidth;
  public int screenHeight;
  public bool isFullScreen;

  public void Awake() => SceneManager.sceneLoaded += OnSceneLoaded;

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    UpdateCameraFOVs();
    UpdateVsync();
  }

  public void OnEnable()
  {
    SetDefaultVolumes();
    UpdateCameraFOVs();
    UpdateVsync();
  }

  public void ResetProfile()
  {
    disableCameraBob = true;
    enabledTutorials = EnabledTutorials.ALL;
    bufferForGif = true;
    bugReportEmail = "";
    vacLockOnHold = true;
    sprintHold = false;
    SetFOV(75f);
    SetDefaultVolumes();
    SetVsync(true);
    SetShowMinimalHUD(false);
  }

  public void SetOverscanAdjustment(float value) => overscanAdjustment = value;

  public float GetOverscanAdjustment() => overscanAdjustment;

  public void SetFOV(float value)
  {
    fov = value;
    foreach (CameraFOVAdjuster instance in CameraFOVAdjuster.Instances)
      instance.SetFOV(fov);
  }

  public void UpdateCameraFOVs() => SetFOV(fov);

  public float GetFOV() => fov;

  public void SetShowMinimalHUD(bool showMinimalHud) => this.showMinimalHud = showMinimalHud;

  public bool GetShowMinimalHUD() => showMinimalHud;

  public void SetVsync(bool enabled)
  {
    enableVsync = enabled;
    QualitySettings.vSyncCount = enableVsync ? 1 : 0;
  }

  public void UpdateVsync() => SetVsync(enableVsync);

  private void SetDefaultVolumes()
  {
    SECTR_AudioBus sectrAudioBus = null;
    if (SECTR_AudioSystem.System != null)
      sectrAudioBus = SECTR_AudioSystem.System.MasterBus;
    if (!(sectrAudioBus != null))
      return;
    sectrAudioBus.UserVolume = 0.8f;
    foreach (SECTR_AudioBus child in sectrAudioBus.Children)
    {
      if (child.name == "Music")
        child.UserVolume = 0.5f;
      else if (child.name == "SFX")
        child.UserVolume = 0.9f;
      else
        Log.Warning("Unknown top-level bus name: " + child.name);
    }
  }

  public void SetScreenResolution(int width, int height, bool fullScreen)
  {
    if (width == 0 || height == 0)
    {
      Log.Debug("Attempted to set resolution to an invalid value.", nameof (width), width, nameof (height), height);
    }
    else
    {
      screenWidth = width;
      screenHeight = height;
      isFullScreen = fullScreen;
      Log.Debug("Setting screen res", nameof (width), width, nameof (height), height, nameof (fullScreen), fullScreen);
      Screen.SetResolution(width, height, fullScreen);
    }
  }

  public void ResetScreenResolution() => SetScreenResolution(screenWidth, screenHeight, isFullScreen);

  public void OnApplicationFocus(bool focus)
  {
    if (!focus)
      return;
    ResetScreenResolution();
  }

  public enum EnabledTutorials
  {
    ALL,
    ADVANCED_ONLY,
    NONE,
  }
}
