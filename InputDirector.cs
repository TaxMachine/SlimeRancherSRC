// Decompiled with JetBrains decompiler
// Type: InputDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InputDirector : SRBehaviour
{
  public OnKeysChanged onKeysChanged;
  public GameObject bugReportPrefab;
  private float mouseLookSensitivity;
  private float gamepadLookSensitivityX;
  private float gamepadLookSensitivityY = -0.2f;
  private float gamepadLookSensitivityXFactor = 1f;
  private float gamepadLookSensitivityYFactor = 1f;
  private float mouseLookSensitivityFactor = 1f;
  private float controllerStickDeadZone;
  private bool oldUsingGamepad;
  private bool swapSticks;
  private bool invertGamepadLookY;
  private bool invertMouseLookY;
  private bool disableMouseLookSmooth;
  private SRInput input;
  private HashSet<KeyCode> protectedKeyCodes = new HashSet<KeyCode>();
  private HashSet<string> protectedButtons = new HashSet<string>();
  private static DefaultBinding[] DEFAULTS;
  private static DefaultBinding[] EDITOR_DEFAULTS;

  public void Awake()
  {
    SRInput.Actions.ListenOptions.OnBindingAdded += OnBindingAdded;
    input = SRInput.Instance;
    InitBindings();
    vp_Utility.SetUsingGamepad(UsingGamepad());
  }

  public void OnDestroy() => SRInput.Actions.ListenOptions.OnBindingAdded -= OnBindingAdded;

  private void OnBindingAdded(PlayerAction action, BindingSource binding) => NoteKeysChanged();

  public void Update()
  {
    if (SRInput.Actions.reportIssue.WasPressed)
      Instantiate(bugReportPrefab);
    else if (SRInput.Actions.screenshot.WasPressed)
      SRSingleton<GameContext>.Instance.TakeScreenshot();
    else if (SRInput.Actions.recordGif.WasPressed)
      SRSingleton<GameContext>.Instance.TakeGifScreenshot();
    if (Mathf.Abs(Input.GetAxisRaw("mouse x")) > (double) Mathf.Epsilon || Mathf.Abs(Input.GetAxisRaw("mouse y")) > (double) Mathf.Epsilon)
    {
      SRInput.Actions.LastInputType = BindingSourceType.MouseBindingSource;
      SRInput.PauseActions.LastInputType = BindingSourceType.MouseBindingSource;
      SRInput.LookActions.LastInputType = BindingSourceType.MouseBindingSource;
    }
    bool usingGamepad = UsingGamepad();
    if (oldUsingGamepad != usingGamepad)
    {
      NoteKeysChanged();
      vp_Utility.SetUsingGamepad(usingGamepad);
    }
    oldUsingGamepad = usingGamepad;
    switch (input.GetInputMode())
    {
      case SRInput.InputMode.DEFAULT:
        if (Time.timeScale != 0.0 && !Levels.isSpecialNonAlloc())
          break;
        input.SetInputMode(SRInput.InputMode.PAUSE, gameObject.GetInstanceID());
        break;
      case SRInput.InputMode.PAUSE:
        if (Time.timeScale == 0.0 || Levels.isSpecialNonAlloc())
          break;
        input.ClearInputMode(gameObject.GetInstanceID());
        break;
    }
  }

  public bool IsProtected(string button) => protectedButtons.Contains(button);

  public bool IsProtected(KeyCode key) => protectedKeyCodes.Contains(key);

  private void InitBindings() => SetDefaultBindings();

  private void InitializeDefaultBindings()
  {
    if (DEFAULTS == null)
      DEFAULTS = new DefaultBinding[51]
      {
        new DefaultBinding(SRInput.Actions.verticalPos, Key.W, Key.UpArrow, InputControlType.LeftStickUp),
        new DefaultBinding(SRInput.Actions.verticalNeg, Key.S, Key.DownArrow, InputControlType.LeftStickDown),
        new DefaultBinding(SRInput.Actions.horizontalPos, Key.D, Key.RightArrow, InputControlType.LeftStickRight),
        new DefaultBinding(SRInput.Actions.horizontalNeg, Key.A, Key.LeftArrow, InputControlType.LeftStickLeft),
        new DefaultBinding(SRInput.Actions.lookYPos, Mouse.PositiveY, Key.None, InputControlType.RightStickDown),
        new DefaultBinding(SRInput.Actions.lookYNeg, Mouse.NegativeY, Key.None, InputControlType.RightStickUp),
        new DefaultBinding(SRInput.Actions.lookXPos, Mouse.PositiveX, Key.None, InputControlType.RightStickRight),
        new DefaultBinding(SRInput.Actions.lookXNeg, Mouse.NegativeX, Key.None, InputControlType.RightStickLeft),
        new DefaultBinding(SRInput.Actions.attack, Mouse.LeftButton, Key.None, InputControlType.RightTrigger),
        new DefaultBinding(SRInput.Actions.vac, Mouse.RightButton, Key.None, InputControlType.LeftTrigger),
        new DefaultBinding(SRInput.Actions.slimeFilter, Key.H, Key.None, InputControlType.None),
        new DefaultBinding(SRInput.Actions.jump, Key.Space, Key.None, InputControlType.Action1),
        new DefaultBinding(SRInput.Actions.run, Key.LeftShift, Key.None, InputControlType.LeftStickButton),
        new DefaultBinding(SRInput.Actions.interact, Key.E, Key.None, InputControlType.Action3),
        new DefaultBinding(SRInput.Actions.accept, Key.Return, Key.PadEnter, InputControlType.None),
        new DefaultBinding(SRInput.Actions.menu, Key.Pause, SRInput.GetDefaultMenuKey(), InputControlType.Start, InputControlType.Options, InputControlType.Menu),
        new DefaultBinding(SRInput.Actions.radarToggle, Key.R, Key.None, InputControlType.RightStickButton),
        new DefaultBinding(SRInput.Actions.openMap, Key.M, Key.None, InputControlType.DPadRight),
        new DefaultBinding(SRInput.Actions.pedia, Key.F1, Key.Slash, InputControlType.DPadUp),
        new DefaultBinding(SRInput.Actions.reportIssue, Key.F2, Key.None, InputControlType.None),
        new DefaultBinding(SRInput.Actions.screenshot, Key.Backslash, Key.None, InputControlType.None),
        new DefaultBinding(SRInput.Actions.recordGif, Key.G, Key.None, InputControlType.None),
        new DefaultBinding(SRInput.Actions.slot1, Key.Key1, Key.None, InputControlType.None),
        new DefaultBinding(SRInput.Actions.slot2, Key.Key2, Key.None, InputControlType.None),
        new DefaultBinding(SRInput.Actions.slot3, Key.Key3, Key.None, InputControlType.None),
        new DefaultBinding(SRInput.Actions.slot4, Key.Key4, Key.None, InputControlType.None),
        new DefaultBinding(SRInput.Actions.slot5, Key.Key5, Key.None, InputControlType.None),
        new DefaultBinding(SRInput.Actions.light, Key.F, Key.None, InputControlType.Action4),
        new DefaultBinding(SRInput.Actions.burst, Mouse.MiddleButton, Key.Q, InputControlType.Action2),
        new DefaultBinding(SRInput.Actions.prevSlot, Mouse.PositiveScrollWheel, Key.None, InputControlType.LeftBumper),
        new DefaultBinding(SRInput.Actions.nextSlot, Mouse.NegativeScrollWheel, Key.None, InputControlType.RightBumper),
        new DefaultBinding(SRInput.Actions.toggleGadgetMode, Key.T, Key.None, InputControlType.DPadDown),
        new DefaultBinding(SRInput.PauseActions.submit, Key.Space, Key.None, InputControlType.Action1),
        new DefaultBinding(SRInput.PauseActions.altSubmit, Key.Space, Key.None, InputControlType.Action3),
        new DefaultBinding(SRInput.PauseActions.cancel, Key.Escape, Key.None, InputControlType.Action2),
        new DefaultBinding(SRInput.PauseActions.menuUp, Key.W, Key.UpArrow, InputControlType.DPadUp, InputControlType.LeftStickUp),
        new DefaultBinding(SRInput.PauseActions.menuDown, Key.S, Key.DownArrow, InputControlType.DPadDown, InputControlType.LeftStickDown),
        new DefaultBinding(SRInput.PauseActions.menuLeft, Key.A, Key.LeftArrow, InputControlType.DPadLeft, InputControlType.LeftStickLeft),
        new DefaultBinding(SRInput.PauseActions.menuRight, Key.D, Key.RightArrow, InputControlType.DPadRight, InputControlType.LeftStickRight),
        new DefaultBinding(SRInput.PauseActions.menuTabLeft, Key.Minus, Key.None, InputControlType.LeftBumper),
        new DefaultBinding(SRInput.PauseActions.menuTabRight, Key.Equals, Key.None, InputControlType.RightBumper),
        new DefaultBinding(SRInput.PauseActions.menuScrollUp, Key.PageUp, Key.None, InputControlType.RightStickUp),
        new DefaultBinding(SRInput.PauseActions.menuScrollDown, Key.PageDown, Key.None, InputControlType.RightStickDown),
        new DefaultBinding(SRInput.PauseActions.unmenu, Key.Pause, SRInput.GetDefaultMenuKey(), InputControlType.Start, InputControlType.Options, InputControlType.Menu),
        new DefaultBinding(SRInput.PauseActions.closeMap, Key.M, Key.None, InputControlType.None),
        new DefaultBinding(SRInput.EngageActions.engage, Key.None, Key.None, InputControlType.Menu),
        new DefaultBinding(SRInput.PauseActions.switchUser, Key.Space, Key.None, InputControlType.Action4),
        new DefaultBinding(SRInput.LookActions.lookYPos, Mouse.PositiveY, Key.None, InputControlType.RightStickDown),
        new DefaultBinding(SRInput.LookActions.lookYNeg, Mouse.NegativeY, Key.None, InputControlType.RightStickUp),
        new DefaultBinding(SRInput.LookActions.lookXPos, Mouse.PositiveX, Key.None, InputControlType.RightStickRight),
        new DefaultBinding(SRInput.LookActions.lookXNeg, Mouse.NegativeX, Key.None, InputControlType.RightStickLeft)
      };
    if (EDITOR_DEFAULTS != null)
      return;
    EDITOR_DEFAULTS = new DefaultBinding[1]
    {
      new DefaultBinding(SRInput.PauseActions.unmenu, Key.Pause, Key.Backquote, InputControlType.Start, InputControlType.Options, InputControlType.Menu)
    };
  }

  private void SetDefaultBindings()
  {
    InitializeDefaultBindings();
    SRInput.Actions.ListenOptions.OnBindingAdded += (action, bindingSource) => SRSingleton<GameContext>.Instance.InputDirector.NoteKeysChanged();
    foreach (DefaultBinding defaultBinding in DEFAULTS)
      defaultBinding.ApplyDefaultBinding();
    if (Application.isEditor)
    {
      foreach (DefaultBinding defaultBinding in EDITOR_DEFAULTS)
        defaultBinding.ApplyDefaultBinding();
    }
    UpdateGamepadStickBindings();
    NoteKeysChanged();
  }

  public void ResetProfile()
  {
    mouseLookSensitivity = 0.0f;
    gamepadLookSensitivityX = 0.0f;
    gamepadLookSensitivityY = -0.2f;
    invertGamepadLookY = false;
    swapSticks = false;
    SetDefaultBindings();
  }

  public void ResetKeyMouseDefaults() => SRInput.Actions.ResetForTypes(BindingSourceType.MouseBindingSource, BindingSourceType.KeyBindingSource);

  public void ResetGamepadDefaults() => SRInput.Actions.ResetForTypes(BindingSourceType.DeviceBindingSource);

  public void NoteKeysChanged()
  {
    if (onKeysChanged == null)
      return;
    onKeysChanged();
  }

  public static bool UsingGamepad() => SRInput.Actions.LastInputType == BindingSourceType.DeviceBindingSource;

  public string GetActiveDeviceString(string actionStr, bool isPauseAction)
  {
    int num = UsingGamepad() ? 1 : 0;
    PlayerAction action = SRInput.GetAction(actionStr);
    return num == 0 ? GetKeyStringForMouseKeyboard(action) : GetKeyStringForGamepad(action, actionStr);
  }

  public Sprite GetActiveDeviceIcon(string actionStr, bool isPauseAction, out bool iconFound) => SRSingleton<GameContext>.Instance.InputDirector.UsingSteamController() ? GetSteamDeviceIcon(actionStr, isPauseAction, out iconFound) : GetDefaultDeviceIcon(actionStr, isPauseAction, out iconFound);

  private Sprite GetDefaultDeviceIcon(string actionStr, bool isPauseAction, out bool iconFound) => SRSingleton<GameContext>.Instance.UITemplates.GetButtonIcon(SRInput.Actions.LastDeviceStyle, GetActiveDeviceString(actionStr, isPauseAction), out iconFound);

  private string GetKeyStringForGamepad(PlayerAction action, string actionStr)
  {
    if (action != null)
    {
      BindingSource primGamepadBinding = SRInput.GetPrimGamepadBinding(action);
      if (primGamepadBinding != null && primGamepadBinding is DeviceBindingSource)
        return ((DeviceBindingSource) primGamepadBinding).Control.ToString();
    }
    else
    {
      if (actionStr == (swapSticks ? "Look" : "Move"))
        return "LeftStickMove";
      if (actionStr == (!swapSticks ? "Look" : "Move"))
        return "RightStickMove";
    }
    return null;
  }

  private string GetKeyStringForMouseKeyboard(PlayerAction action)
  {
    if (action != null)
    {
      BindingSource bindingSource = SRInput.GetPrimKeyBinding(action);
      if (bindingSource == null)
        bindingSource = SRInput.GetSecKeyBinding(action);
      if (bindingSource != null && bindingSource is MouseBindingSource)
        return ((MouseBindingSource) bindingSource).Control.ToString();
      if (bindingSource != null && bindingSource is KeyBindingSource)
        return ((KeyBindingSource) bindingSource).Control.ToString();
    }
    return null;
  }

  private Sprite GetSteamDeviceIcon(string actionStr, bool isPauseAction, out bool iconFound) => SRSingleton<GameContext>.Instance.UITemplates.GetSteamButtonIcon(((SteamInputDevice) InputManager.ActiveDevice).GetOrigin(actionStr, isPauseAction), out iconFound);

  public bool UsingSteamController() => InputManager.ActiveDevice is SteamInputDevice;

  public void ShowSteamControllerConfig()
  {
    if (!(InputManager.ActiveDevice is SteamInputDevice activeDevice))
      return;
    SteamController.ShowBindingPanel(activeDevice.GetController());
  }

  public bool GetSwapSticks() => swapSticks;

  public bool GetDisableGamepad() => DeviceBindingSource.DevicesDisabled;

  public void SetDisableGamepad(bool disable) => DeviceBindingSource.DevicesDisabled = disable;

  public void SetSwapSticks(bool swap)
  {
    swapSticks = swap;
    UpdateGamepadStickBindings();
  }

  public bool GetInvertGamepadLookY() => invertGamepadLookY;

  public void SetInvertGamepadLookY(bool invert)
  {
    invertGamepadLookY = invert;
    UpdateGamepadStickBindings();
  }

  public bool GetInvertMouseLookY() => invertMouseLookY;

  public void SetInvertMouseLookY(bool invert)
  {
    invertMouseLookY = invert;
    UpdateMouseYAxis();
  }

  public bool GetDisableMouseLookSmooth() => disableMouseLookSmooth;

  public void SetDisableMouseLookSmooth(bool smooth) => disableMouseLookSmooth = smooth;

  public float ControllerStickDeadZone
  {
    get => controllerStickDeadZone;
    set => controllerStickDeadZone = value;
  }

  private void UpdateMouseYAxis()
  {
    UpdateMouseYAxis(SRInput.Actions);
    UpdateMouseYAxis(SRInput.LookActions);
  }

  private void UpdateMouseYAxis(SRInput.PlayerLookActions actions)
  {
    actions.lookXNeg.ClearBindingsOfTypes(BindingSourceType.MouseBindingSource);
    actions.lookXPos.ClearBindingsOfTypes(BindingSourceType.MouseBindingSource);
    actions.lookYNeg.ClearBindingsOfTypes(BindingSourceType.MouseBindingSource);
    actions.lookYPos.ClearBindingsOfTypes(BindingSourceType.MouseBindingSource);
    actions.lookXNeg.AddBinding(new ScalableMouseBindingSource(Mouse.NegativeX, mouseLookSensitivityFactor));
    actions.lookXPos.AddBinding(new ScalableMouseBindingSource(Mouse.PositiveX, mouseLookSensitivityFactor));
    actions.lookYNeg.AddBinding(new ScalableMouseBindingSource(invertMouseLookY ? Mouse.PositiveY : Mouse.NegativeY, mouseLookSensitivityFactor));
    actions.lookYPos.AddBinding(new ScalableMouseBindingSource(invertMouseLookY ? Mouse.NegativeY : Mouse.PositiveY, mouseLookSensitivityFactor));
  }

  private void UpdateGamepadStickBindings()
  {
    UpdateGamepadStickBindings(SRInput.Actions);
    UpdateGamepadStickBindings(SRInput.LookActions);
    SRInput.Actions.horizontalNeg.ClearBindingsOfTypes(BindingSourceType.DeviceBindingSource);
    SRInput.Actions.horizontalPos.ClearBindingsOfTypes(BindingSourceType.DeviceBindingSource);
    SRInput.Actions.verticalNeg.ClearBindingsOfTypes(BindingSourceType.DeviceBindingSource);
    SRInput.Actions.verticalPos.ClearBindingsOfTypes(BindingSourceType.DeviceBindingSource);
    if (swapSticks)
    {
      SRInput.Actions.horizontalNeg.AddBinding(new DeviceBindingSource(InputControlType.RightStickLeft));
      SRInput.Actions.horizontalPos.AddBinding(new DeviceBindingSource(InputControlType.RightStickRight));
      SRInput.Actions.verticalNeg.AddBinding(new DeviceBindingSource(InputControlType.RightStickDown));
      SRInput.Actions.verticalPos.AddBinding(new DeviceBindingSource(InputControlType.RightStickUp));
    }
    else
    {
      SRInput.Actions.horizontalNeg.AddBinding(new DeviceBindingSource(InputControlType.LeftStickLeft));
      SRInput.Actions.horizontalPos.AddBinding(new DeviceBindingSource(InputControlType.LeftStickRight));
      SRInput.Actions.verticalNeg.AddBinding(new DeviceBindingSource(InputControlType.LeftStickDown));
      SRInput.Actions.verticalPos.AddBinding(new DeviceBindingSource(InputControlType.LeftStickUp));
    }
  }

  private void UpdateGamepadStickBindings(SRInput.PlayerLookActions actions)
  {
    actions.lookXNeg.ClearBindingsOfTypes(BindingSourceType.DeviceBindingSource);
    actions.lookXPos.ClearBindingsOfTypes(BindingSourceType.DeviceBindingSource);
    actions.lookYNeg.ClearBindingsOfTypes(BindingSourceType.DeviceBindingSource);
    actions.lookYPos.ClearBindingsOfTypes(BindingSourceType.DeviceBindingSource);
    if (swapSticks)
    {
      actions.lookXNeg.AddBinding(new DeviceBindingSource(InputControlType.LeftStickLeft));
      actions.lookXPos.AddBinding(new DeviceBindingSource(InputControlType.LeftStickRight));
      actions.lookYNeg.AddBinding(new DeviceBindingSource(invertGamepadLookY ? InputControlType.LeftStickUp : InputControlType.LeftStickDown));
      actions.lookYPos.AddBinding(new DeviceBindingSource(invertGamepadLookY ? InputControlType.LeftStickDown : InputControlType.LeftStickUp));
    }
    else
    {
      actions.lookXNeg.AddBinding(new DeviceBindingSource(InputControlType.RightStickLeft));
      actions.lookXPos.AddBinding(new DeviceBindingSource(InputControlType.RightStickRight));
      actions.lookYNeg.AddBinding(new DeviceBindingSource(invertGamepadLookY ? InputControlType.RightStickUp : InputControlType.RightStickDown));
      actions.lookYPos.AddBinding(new DeviceBindingSource(invertGamepadLookY ? InputControlType.RightStickDown : InputControlType.RightStickUp));
    }
  }

  public float MouseLookSensitivity
  {
    get => mouseLookSensitivity;
    set
    {
      mouseLookSensitivity = value;
      NoteNewPlayer(SRSingleton<SceneContext>.Instance.Player);
    }
  }

  public float GamepadLookSensitivityX
  {
    get => gamepadLookSensitivityX;
    set
    {
      gamepadLookSensitivityX = value;
      NoteNewPlayer(SRSingleton<SceneContext>.Instance.Player);
    }
  }

  public float GamepadLookSensitivityY
  {
    get => gamepadLookSensitivityY;
    set
    {
      gamepadLookSensitivityY = value;
      NoteNewPlayer(SRSingleton<SceneContext>.Instance.Player);
    }
  }

  public void NoteNewPlayer(GameObject player)
  {
    SetPlayerMouseSensitivity(player);
    SetPlayerGamepadXSensitivity(player);
    SetPlayerGamepadYSensitivity(player);
  }

  public float GetGamepadLookSensitivityXFactor() => gamepadLookSensitivityXFactor;

  public float GetGamepadLookSensitivityYFactor() => gamepadLookSensitivityYFactor;

  public void SetGamepadLookSensitivityXFactor(float factor)
  {
    gamepadLookSensitivityXFactor = factor;
    UpdateGamepadStickBindings();
  }

  public void SetGamepadLookSensitivityYFactor(float factor)
  {
    gamepadLookSensitivityYFactor = factor;
    UpdateGamepadStickBindings();
  }

  public void SetMouseLookSensitivityFactor(float factor)
  {
    mouseLookSensitivityFactor = factor;
    UpdateMouseYAxis();
  }

  private void SetPlayerMouseSensitivity(GameObject player)
  {
    if (!(player != null))
      return;
    SetMouseLookSensitivityFactor(Mathf.Pow(3f, mouseLookSensitivity));
  }

  private void SetPlayerGamepadXSensitivity(GameObject player)
  {
    if (!(player != null))
      return;
    SetGamepadLookSensitivityXFactor(Mathf.Pow(3f, gamepadLookSensitivityX));
  }

  private void SetPlayerGamepadYSensitivity(GameObject player)
  {
    if (!(player != null))
      return;
    SetGamepadLookSensitivityYFactor(Mathf.Pow(3f, gamepadLookSensitivityY));
  }

  public class ScalableMouseBindingSource : MouseBindingSource
  {
    private float sensitivity;

    public ScalableMouseBindingSource(Mouse control, float sensitivity)
      : base(control)
    {
      this.sensitivity = sensitivity;
    }

    public override float GetValue(InputDevice device) => base.GetValue(device) * sensitivity;
  }

  public delegate void OnKeysChanged();

  private class DefaultBinding
  {
    public PlayerAction bindTo;
    public Mouse primMouse;
    public Key primKey;
    public Key secKey;
    public InputControlType primBtn;
    public InputControlType secBtn;
    public InputControlType tertBtn;

    public DefaultBinding(
      PlayerAction bindTo,
      Key primKey,
      Key secKey,
      InputControlType primBtn,
      InputControlType secBtn = InputControlType.None,
      InputControlType tertBtn = InputControlType.None)
    {
      this.bindTo = bindTo;
      this.primKey = primKey;
      this.secKey = secKey;
      this.primBtn = primBtn;
      this.secBtn = secBtn;
      this.tertBtn = tertBtn;
    }

    public DefaultBinding(
      PlayerAction bindTo,
      Mouse primMouse,
      Key secKey,
      InputControlType primBtn,
      InputControlType secBtn = InputControlType.None,
      InputControlType tertBtn = InputControlType.None)
    {
      this.bindTo = bindTo;
      this.primMouse = primMouse;
      this.secKey = secKey;
      this.primBtn = primBtn;
      this.secBtn = secBtn;
      this.tertBtn = tertBtn;
    }

    public void ApplyDefaultBinding()
    {
      bindTo.ClearBindings();
      if (primKey != Key.None)
        bindTo.AddDefaultBinding(primKey);
      if (primMouse != Mouse.None)
        bindTo.AddDefaultBinding(primMouse);
      if (secKey != Key.None)
        bindTo.AddDefaultBinding(secKey);
      if (primBtn != InputControlType.None)
        bindTo.AddDefaultBinding(primBtn);
      if (secBtn != InputControlType.None)
        bindTo.AddDefaultBinding(secBtn);
      if (tertBtn == InputControlType.None)
        return;
      bindTo.AddDefaultBinding(tertBtn);
    }
  }
}
