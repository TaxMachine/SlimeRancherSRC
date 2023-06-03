// Decompiled with JetBrains decompiler
// Type: SteamDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class SteamDirector
{
  private static readonly Dictionary<string, string> LANG_LOOKUP = new Dictionary<string, string>()
  {
    {
      "english",
      "en"
    },
    {
      "german",
      "de"
    },
    {
      "spanish",
      "es"
    },
    {
      "french",
      "fr"
    },
    {
      "russian",
      "ru"
    },
    {
      "swedish",
      "sv"
    },
    {
      "schinese",
      "zh"
    },
    {
      "tchinese",
      "zh"
    },
    {
      "japanese",
      "ja"
    },
    {
      "portuguese",
      "pt"
    },
    {
      "brazilian",
      "pt"
    }
  };
  private static UnityAction<string> s_GamepadTextInputDismissed_onResult;

  static SteamDirector()
  {
    if (!SteamManager.Initialized)
      return;
    SteamUtils.SetOverlayNotificationPosition(ENotificationPosition.k_EPositionTopRight);
    SRSingleton<GameContext>.Instance.gameObject.AddComponent<UnityEventListener_SteamController>();
    SteamManager.s_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(c_GameOverlayActivated);
    SteamManager.s_GamepadTextInputDismissed = Callback<GamepadTextInputDismissed_t>.Create(c_GamepadTextInputDismissed);
    string fallbackLang;
    if (!LANG_LOOKUP.TryGetValue(SteamApps.GetCurrentGameLanguage(), out fallbackLang))
    {
      Log.Error("Failed to get language code from current Steam language.", "steam_language", SteamApps.GetCurrentGameLanguage());
      fallbackLang = SRSingleton<GameContext>.Instance.MessageDirector.fallbackLang;
    }
    SteamManager.s_UserStatsReceived = Callback<UserStatsReceived_t>.Create(SteamManager.c_UserStatsReceived);
    SteamUserStats.RequestCurrentStats();
  }

  public static void SyncAchievements(
    HashSet<AchievementsDirector.Achievement> achievements)
  {
  }

  public static void ClearAchievements()
  {
  }

  public static void AddAchievement(AchievementsDirector.Achievement achievement) => SteamManager.AddAchievement(achievement);

  private static void c_GameOverlayActivated(GameOverlayActivated_t response)
  {
    UnityEventListener_MainThreadHelper mainThreadHelper = SRSingleton<GameContext>.Instance.gameObject.AddComponent<UnityEventListener_MainThreadHelper>();
    if (response.m_bActive != 0)
      mainThreadHelper.onUpdate += () => SRSingleton<SceneContext>.Instance.TimeDirector.Pause();
    else
      mainThreadHelper.onUpdate += () => SRSingleton<SceneContext>.Instance.TimeDirector.Unpause();
  }

  public static bool ActivateKeyboard(
    bool multiline,
    string desc,
    string existingVal,
    UnityAction<string> onResult)
  {
    if (!SteamUtils.IsSteamInBigPictureMode())
    {
      Log.Info("Steam virtual keyboard requires the game to be started in Big Picture mode.");
      return false;
    }
    if (!SteamUtils.ShowGamepadTextInput(EGamepadTextInputMode.k_EGamepadTextInputModeNormal, multiline ? EGamepadTextInputLineMode.k_EGamepadTextInputLineModeMultipleLines : EGamepadTextInputLineMode.k_EGamepadTextInputLineModeSingleLine, desc, multiline ? 1000U : 100U, existingVal))
    {
      Log.Error("Failed to show Steam virtual keyboard.");
      return false;
    }
    s_GamepadTextInputDismissed_onResult = onResult;
    return true;
  }

  private static void c_GamepadTextInputDismissed(GamepadTextInputDismissed_t response)
  {
    if (s_GamepadTextInputDismissed_onResult == null)
      return;
    if (response.m_bSubmitted)
    {
      uint gamepadTextLength = SteamUtils.GetEnteredGamepadTextLength();
      string pchText;
      SteamUtils.GetEnteredGamepadTextInput(out pchText, gamepadTextLength);
      s_GamepadTextInputDismissed_onResult(pchText);
    }
    else
      s_GamepadTextInputDismissed_onResult(null);
    s_GamepadTextInputDismissed_onResult = null;
  }

  private class UnityEventListener_SteamController : 
    SRSingleton<UnityEventListener_SteamController>
  {
    private SteamInputDevice currentSteamInputDevice;
    private ControllerHandle_t[] controllers = new ControllerHandle_t[16];

    public override void Awake()
    {
      base.Awake();
      SteamController.Init();
      SteamController.RunFrame();
      SteamManager.AddDestroyCallback(() => Destroyer.Destroy(this, "SteamDirector.UnityEventListener_SteamController.OnSteamManagerDestroyed"));
    }

    public void OnDisable() => SteamController.Shutdown();

    public override void OnDestroy()
    {
      base.OnDestroy();
      currentSteamInputDevice = null;
    }

    public void Update()
    {
      SteamController.GetConnectedControllers(controllers);
      if ((ulong) controllers[0] == 0UL && currentSteamInputDevice != null)
      {
        InputManager.DetachDevice(currentSteamInputDevice);
        currentSteamInputDevice = null;
      }
      if ((ulong) controllers[0] == 0UL || currentSteamInputDevice != null)
        return;
      ControllerActionSetHandle_t actionSetHandle1 = SteamController.GetActionSetHandle("GameControls");
      ControllerActionSetHandle_t actionSetHandle2 = SteamController.GetActionSetHandle("MenuControls");
      SteamController.ActivateActionSet(controllers[0], actionSetHandle1);
      currentSteamInputDevice = new SteamInputDevice(controllers[0], actionSetHandle1, actionSetHandle2);
      InputManager.AttachDevice(currentSteamInputDevice);
    }
  }

  private class UnityEventListener_MainThreadHelper : MonoBehaviour
  {
    public event Action onUpdate;

    public void Update()
    {
      onUpdate();
      Destroyer.Destroy(this, "SteamDirector.UnityEventListener_MainThreadHelper.Update");
    }
  }
}
