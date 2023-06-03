// Decompiled with JetBrains decompiler
// Type: PauseMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : SRSingleton<PauseMenu>
{
  public GameObject pauseUI;
  public GameObject optionsUI;
  public GameObject bugReportUI;
  public Button resumeButton;
  public Button pediaButton;
  public Button achievementsButton;
  public Button optionsButton;
  public Button bugReportButton;
  public Button screenshotButton;
  public Button emergencyResetButton;
  public Button quitButton;
  public Toggle invertViewYAxisToggle;
  public GameObject invertViewYAxisPanel;
  private TimeDirector timeDir;
  private bool suppressUnpause;
  private SRInput.InputMode? previousInputMode;

  public override void Awake()
  {
    base.Awake();
    bugReportButton.gameObject.SetActive(true);
    screenshotButton.gameObject.SetActive(true);
    achievementsButton.gameObject.SetActive(true);
    XlateText[] componentsInChildren = quitButton.GetComponentsInChildren<XlateText>(true);
    if (componentsInChildren != null && componentsInChildren.Length != 0)
      componentsInChildren[0].key = "b.save_and_quit";
    invertViewYAxisPanel.SetActive(false);
    InputManager.OnDeviceDetached += PauseOnDeviceDetach;
  }

  private void Start()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    pauseUI.SetActive(false);
  }

  public void Update()
  {
    if ((SRInput.Actions.menu.WasPressed || SRInput.PauseActions.unmenu.WasPressed) && !timeDir.IsFastForwarding())
    {
      if (pauseUI.activeSelf)
      {
        if (!timeDir.ExactlyOnePauser() || suppressUnpause)
          return;
        UnPauseGame();
      }
      else
      {
        if (Time.timeScale <= 0.0)
          return;
        PauseGame();
      }
    }
    else
    {
      if (!SRInput.PauseActions.cancel.WasPressed || suppressUnpause || !pauseUI.activeSelf || !timeDir.ExactlyOnePauser())
        return;
      UnPauseGame();
    }
  }

  public bool IsOnlyPauser() => pauseUI.activeSelf && timeDir.ExactlyOnePauser();

  public void PauseGame()
  {
    SRInput.Instance.SetInputMode(SRInput.InputMode.PAUSE, gameObject.GetInstanceID());
    pauseUI.SetActive(true);
  }

  public void UnPauseGame()
  {
    pauseUI.SetActive(false);
    SRInput.Instance.ClearInputMode(gameObject.GetInstanceID());
  }

  public void Resume() => UnPauseGame();

  public void Pedia() => InstantiateAndWait(SRSingleton<SceneContext>.Instance.PediaDirector.pediaPanelPrefab);

  public void Achievements() => InstantiateAndWait(SRSingleton<SceneContext>.Instance.AchievementsDirector.achievementsPanelPrefab);

  public void EmergencyReturn() => WaitFor(SRSingleton<GameContext>.Instance.UITemplates.CreateConfirmDialog("m.emergency_return", () =>
  {
    DeathHandler.Kill(SRSingleton<SceneContext>.Instance.Player, DeathHandler.Source.EMERGENCY_RETURN, null, "PauseGame.EmergencyReturn");
    UnPauseGame();
  }));

  public void Quit()
  {
    if (!SRSingleton<GameContext>.Instance.AutoSaveDirector.SaveAllNow())
      return;
    SRSingleton<SceneContext>.Instance.OnSessionEnded();
    SceneManager.LoadScene("MainMenu");
  }

  public void Options() => InstantiateAndWait(optionsUI);

  public void ReportIssue() => InstantiateAndWait(bugReportUI);

  public GameObject InstantiateAndWait(GameObject prefab)
  {
    GameObject uiObj = Instantiate(prefab);
    WaitFor(uiObj);
    return uiObj;
  }

  public void WaitFor(GameObject uiObj)
  {
    BaseUI component = uiObj.GetComponent<BaseUI>();
    gameObject.SetActive(false);
    component.onDestroy += () =>
    {
      if (!(this != null) || !(gameObject != null))
        return;
      gameObject.SetActive(true);
    };
  }

  public void Screenshot() => SRSingleton<GameContext>.Instance.TakeScreenshot();

  public void OnEnable() => invertViewYAxisToggle.isOn = SRSingleton<GameContext>.Instance.InputDirector.GetInvertGamepadLookY();

  public void OnDisable()
  {
  }

  public void OnToggleYAxis() => SRSingleton<GameContext>.Instance.InputDirector.SetInvertGamepadLookY(invertViewYAxisToggle.isOn);

  public override void OnDestroy()
  {
    base.OnDestroy();
    SRInput.Instance.ClearInputMode(gameObject.GetInstanceID());
    InputManager.OnDeviceDetached -= PauseOnDeviceDetach;
  }

  private void PauseOnDeviceDetach(InputDevice device)
  {
    if (Time.timeScale <= 0.0 || timeDir.IsFastForwarding() || SRInput.Instance.GetInputMode() != SRInput.InputMode.DEFAULT)
      return;
    PauseGame();
  }
}
