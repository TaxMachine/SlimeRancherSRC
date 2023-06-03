// Decompiled with JetBrains decompiler
// Type: GamepadPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamepadPanel : MonoBehaviour
{
  private bool initializing;
  public Toggle disableGamepadToggle;
  public Toggle swapSticksToggle;
  public Toggle invertGamepadLookYToggle;
  public GameObject swapStickToggleLabel;
  public GameObject ps4SwapSticksToggleLabel;
  public Slider lookSensitivityXSlider;
  public Slider lookSensitivityYSlider;
  public Slider deadZoneSlider;
  public Button defaultGamepadBtn;
  public GameObject openChangeGamepadSettingsButton;
  private Dictionary<BindingLineUI, string> labelKeyDict = new Dictionary<BindingLineUI, string>();
  public GameObject bindingGamepadLinePrefab;
  public GameObject bindingsGamepadPanel;
  public GameObject rightPanel;
  public GameObject gamepadSettingsPrefab;
  public OptionsUI optionsUi;
  public GamepadVisualPanel defaultGamepadVisualPanel;
  public GamepadVisualPanel ps4GamepadVisualPanel;
  public GameObject standardPanel;
  public GameObject steamPanel;
  private MessageBundle uiBundle;
  private InputDirector inputDir;

  public void Awake()
  {
    inputDir = SRSingleton<GameContext>.Instance.InputDirector;
    ps4SwapSticksToggleLabel.SetActive(false);
    swapStickToggleLabel.SetActive(true);
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(OnBundlesAvailable);
  }

  public void OnDestroy()
  {
    if (!(SRSingleton<GameContext>.Instance != null))
      return;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(OnBundlesAvailable);
  }

  private GamepadVisualPanel GetVisualPanel() => defaultGamepadVisualPanel;

  private void OnBundlesAvailable(MessageDirector msgDir)
  {
    initializing = true;
    uiBundle = msgDir.GetBundle("ui");
    SetupBindings();
    disableGamepadToggle.isOn = SRSingleton<GameContext>.Instance.InputDirector.GetDisableGamepad();
    SetGamepadControlsInteractable(!disableGamepadToggle.isOn);
    swapSticksToggle.isOn = SRSingleton<GameContext>.Instance.InputDirector.GetSwapSticks();
    invertGamepadLookYToggle.isOn = SRSingleton<GameContext>.Instance.InputDirector.GetInvertGamepadLookY();
    lookSensitivityXSlider.value = SRSingleton<GameContext>.Instance.InputDirector.GamepadLookSensitivityX;
    lookSensitivityYSlider.value = SRSingleton<GameContext>.Instance.InputDirector.GamepadLookSensitivityY;
    deadZoneSlider.value = SRSingleton<GameContext>.Instance.InputDirector.ControllerStickDeadZone * 10f;
    disableGamepadToggle.gameObject.SetActive(true);
    defaultGamepadBtn.gameObject.SetActive(true);
    rightPanel.SetActive(true);
    openChangeGamepadSettingsButton.SetActive(false);
    initializing = false;
    RefreshBindings();
    Update();
  }

  public void Update()
  {
    bool flag = inputDir.UsingSteamController();
    standardPanel.SetActive(!flag);
    steamPanel.SetActive(flag);
    defaultGamepadVisualPanel.gameObject.SetActive(true);
    ps4GamepadVisualPanel.gameObject.SetActive(false);
  }

  private void SetupBindings()
  {
    Selectable deadZoneSlider = this.deadZoneSlider;
    for (int index = 0; index < bindingsGamepadPanel.transform.childCount; ++index)
      Destroyer.Destroy(bindingsGamepadPanel.transform.GetChild(index).gameObject, "GamepadPanel.SetupBindings");
    while (bindingsGamepadPanel.transform.childCount > 0)
      bindingsGamepadPanel.transform.GetChild(0).SetParent(null, false);
    CreateGamepadBindingLine("key.shoot", SRInput.Actions.attack);
    CreateGamepadBindingLine("key.vac", SRInput.Actions.vac);
    CreateGamepadBindingLine("key.burst", SRInput.Actions.burst);
    CreateGamepadBindingLine("key.jump", SRInput.Actions.jump);
    CreateGamepadBindingLine("key.run", SRInput.Actions.run);
    CreateGamepadBindingLine("key.interact", SRInput.Actions.interact);
    CreateGamepadBindingLine("key.gadgetMode", SRInput.Actions.toggleGadgetMode);
    CreateGamepadBindingLine("key.flashlight", SRInput.Actions.light);
    CreateGamepadBindingLine("key.radar", SRInput.Actions.radarToggle);
    CreateGamepadBindingLine("key.map", SRInput.Actions.openMap);
    CreateGamepadBindingLine("key.slot_1", SRInput.Actions.slot1);
    CreateGamepadBindingLine("key.slot_2", SRInput.Actions.slot2);
    CreateGamepadBindingLine("key.slot_3", SRInput.Actions.slot3);
    CreateGamepadBindingLine("key.slot_4", SRInput.Actions.slot4);
    CreateGamepadBindingLine("key.slot_5", SRInput.Actions.slot5);
    CreateGamepadBindingLine("key.prev_slot", SRInput.Actions.prevSlot);
    CreateGamepadBindingLine("key.next_slot", SRInput.Actions.nextSlot);
    CreateGamepadBindingLine("key.reportissue", SRInput.Actions.reportIssue);
    CreateGamepadBindingLine("key.screenshot", SRInput.Actions.screenshot);
    CreateGamepadBindingLine("key.recordgif", SRInput.Actions.recordGif);
    CreateGamepadBindingLine("key.pedia", SRInput.Actions.pedia);
    Button[] componentsInChildren = bindingsGamepadPanel.GetComponentsInChildren<Button>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      Navigation navigation1 = new Navigation();
      navigation1.mode = Navigation.Mode.Explicit;
      if (index < componentsInChildren.Length - 1)
      {
        navigation1.selectOnDown = componentsInChildren[index + 1];
      }
      else
      {
        navigation1.selectOnDown = defaultGamepadBtn;
        defaultGamepadBtn.navigation = defaultGamepadBtn.navigation with
        {
          mode = Navigation.Mode.Explicit,
          selectOnUp = componentsInChildren[index]
        };
      }
      if (index > 0)
      {
        navigation1.selectOnUp = componentsInChildren[index - 1];
      }
      else
      {
        navigation1.selectOnUp = deadZoneSlider;
        Navigation navigation2 = deadZoneSlider.navigation with
        {
          mode = Navigation.Mode.Explicit,
          selectOnDown = componentsInChildren[index]
        };
        deadZoneSlider.navigation = navigation2;
      }
      navigation1.selectOnLeft = disableGamepadToggle;
      componentsInChildren[index].navigation = navigation1;
    }
  }

  public void ToggleDisableGamepad()
  {
    SRSingleton<GameContext>.Instance.InputDirector.SetDisableGamepad(disableGamepadToggle.isOn);
    SetGamepadControlsInteractable(!disableGamepadToggle.isOn);
  }

  public void ToggleInvertGamepadLookY() => SRSingleton<GameContext>.Instance.InputDirector.SetInvertGamepadLookY(invertGamepadLookYToggle.isOn);

  public void ToggleSwapSticks()
  {
    SRSingleton<GameContext>.Instance.InputDirector.SetSwapSticks(swapSticksToggle.isOn);
    RefreshBindings();
  }

  public void OnLookSensitivityXChanged() => SRSingleton<GameContext>.Instance.InputDirector.GamepadLookSensitivityX = lookSensitivityXSlider.value;

  public void OnLookSensitivityYChanged() => SRSingleton<GameContext>.Instance.InputDirector.GamepadLookSensitivityY = lookSensitivityYSlider.value;

  public void OnDeadZoneChanged() => SRSingleton<GameContext>.Instance.InputDirector.ControllerStickDeadZone = Mathf.Clamp(deadZoneSlider.value / 10f, 0.0f, 0.95f);

  public void OnOpenChangeSettingsClicked()
  {
    BaseUI component = Instantiate(gamepadSettingsPrefab).GetComponent<BaseUI>();
    optionsUi.PreventClosing(true);
    gameObject.SetActive(false);
    component.onDestroy += () =>
    {
      gameObject.SetActive(true);
      optionsUi.PreventClosing(false);
    };
  }

  public void RefreshBindings()
  {
    if (initializing || uiBundle == null)
      return;
    GetVisualPanel().ClearAllGamepadText(uiBundle);
    foreach (BindingLineUI componentsInChild in GetComponentsInChildren<BindingLineUI>(true))
    {
      componentsInChild.Refresh();
      if (componentsInChild.leftBtnMode == SRInput.ButtonType.GAMEPAD)
        UpdateGamepadBindingText(SRInput.GetBinding(componentsInChild.action, componentsInChild.leftBtnMode), componentsInChild);
    }
    GetVisualPanel().GetTextForGamepadKey(InputControlType.Start).text = uiBundle.Get("m.gamepad_button_pause", InputControlType.Start);
  }

  private GameObject CreateGamepadBindingLine(string label, PlayerAction action)
  {
    GameObject bindingLineObj = Instantiate(bindingGamepadLinePrefab);
    bindingLineObj.transform.SetParent(bindingsGamepadPanel.transform, false);
    BindingPanel.CreateBindingLine(label, action, bindingLineObj, uiBundle, labelKeyDict, DisableGamepads);
    BindingLineUI component = bindingLineObj.GetComponent<BindingLineUI>();
    if (component.leftBtnMode == SRInput.ButtonType.GAMEPAD)
      UpdateGamepadBindingText(SRInput.GetBinding(component.action, SRInput.ButtonType.GAMEPAD), component);
    return bindingLineObj;
  }

  private void SetGamepadControlsInteractable(bool interactable)
  {
    foreach (Selectable componentsInChild in bindingsGamepadPanel.GetComponentsInChildren<Button>(true))
      componentsInChild.interactable = interactable;
    swapSticksToggle.interactable = interactable;
    invertGamepadLookYToggle.interactable = interactable;
    lookSensitivityXSlider.interactable = interactable;
    lookSensitivityYSlider.interactable = interactable;
    deadZoneSlider.interactable = interactable;
  }

  private void UpdateGamepadBindingText(BindingSource bindingSource, BindingLineUI binding)
  {
    InputControlType inputControlType = bindingSource == null ? InputControlType.None : (bindingSource as DeviceBindingSource).Control;
    GamepadVisualPanel visualPanel = GetVisualPanel();
    TMP_Text textForGamepadKey = visualPanel.GetTextForGamepadKey(inputControlType);
    TMP_Text forGamepadStickKey = visualPanel.GetTextForGamepadStickKey(inputControlType);
    if (textForGamepadKey != null)
    {
      textForGamepadKey.text = uiBundle.Get("m.gamepad_button", new string[2]
      {
        XlateKeyText.XlateKey(inputControlType),
        uiBundle.Get(labelKeyDict[binding])
      });
    }
    else
    {
      if (!(forGamepadStickKey != null))
        return;
      forGamepadStickKey.text = uiBundle.Get("m.gamepad_stick", new string[4]
      {
        XlateKeyText.XlateKey(inputControlType == InputControlType.LeftStickButton ? "LeftStick" : "RightStick"),
        uiBundle.Get(inputControlType == InputControlType.LeftStickButton ^ SRSingleton<GameContext>.Instance.InputDirector.GetSwapSticks() ? "l.move" : "l.view"),
        uiBundle.Get(labelKeyDict[binding]),
        uiBundle.Get(string.Format("l.gamepad_{0}_stick_press_action", inputControlType == InputControlType.LeftStickButton ? "left" : (object) "right"))
      });
    }
  }

  private int InputControlTypeToSpriteId(InputControlType btn)
  {
    switch (btn)
    {
      case InputControlType.Action1:
        return 0;
      case InputControlType.Action2:
        return 1;
      case InputControlType.Action3:
        return 2;
      case InputControlType.Action4:
        return 3;
      default:
        return 0;
    }
  }

  private bool DisableGamepads() => disableGamepadToggle.isOn;

  public void ResetGamepadDefaults()
  {
    SRSingleton<GameContext>.Instance.InputDirector.ResetGamepadDefaults();
    SRSingleton<GameContext>.Instance.InputDirector.SetInvertGamepadLookY(invertGamepadLookYToggle.isOn);
    RefreshBindings();
  }

  public void ShowSteamControllerConfig() => inputDir.ShowSteamControllerConfig();
}
