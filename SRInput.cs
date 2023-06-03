// Decompiled with JetBrains decompiler
// Type: SRInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using MonomiPark.SlimeRancher.Persist;
using MonomiPark.SlimeRancher.Utility;
using System.Collections.ObjectModel;
using UnityEngine;

public class SRInput
{
  public PlayerActions actions;
  public PlayerPauseActions pauseActions;
  public PlayerEngageActions engageActions;
  public PlayerLookActions lookActions;
  public readonly Key[] PROTECTED_KEYS;
  public readonly InputControlType[] PROTECTED_CONTROLS;
  public readonly PlayerAction[] PROTECTED_ACTIONS;
  protected static SRInput instance;
  private InputModeStack inputModeStack = new InputModeStack();

  private SRInput()
  {
    PROTECTED_KEYS = new Key[2]
    {
      Key.Pause,
      GetDefaultMenuKey()
    };
    PROTECTED_CONTROLS = new InputControlType[2]
    {
      InputControlType.Start,
      InputControlType.Menu
    };
    actions = new PlayerActions(this);
    pauseActions = new PlayerPauseActions();
    engageActions = new PlayerEngageActions();
    lookActions = new PlayerLookActions();
    SetInputMode(InputMode.DEFAULT, 0);
    PROTECTED_ACTIONS = new PlayerAction[2]
    {
      actions.menu,
      pauseActions.unmenu
    };
  }

  public static SRInput Instance
  {
    get
    {
      if (instance == null && Application.isPlaying)
        instance = new SRInput();
      return instance;
    }
  }

  public static PlayerActions Actions => Instance.actions;

  public static PlayerPauseActions PauseActions => Instance.pauseActions;

  public static PlayerEngageActions EngageActions => Instance.engageActions;

  public static PlayerLookActions LookActions => Instance.lookActions;

  public static BindingSource GetPrimKeyBinding(PlayerAction action)
  {
    ReadOnlyCollection<BindingSource> readOnlyCollection = action.BindingsOfTypes(BindingSourceType.KeyBindingSource, BindingSourceType.MouseBindingSource);
    return readOnlyCollection.Count >= 1 ? readOnlyCollection[0] : null;
  }

  public static BindingSource GetSecKeyBinding(PlayerAction action)
  {
    ReadOnlyCollection<BindingSource> readOnlyCollection = action.BindingsOfTypes(BindingSourceType.KeyBindingSource, BindingSourceType.MouseBindingSource);
    return readOnlyCollection.Count >= 2 ? readOnlyCollection[1] : null;
  }

  public static BindingSource GetPrimGamepadBinding(PlayerAction action)
  {
    ReadOnlyCollection<BindingSource> readOnlyCollection = action.BindingsOfTypes(BindingSourceType.DeviceBindingSource);
    return readOnlyCollection.Count >= 1 ? readOnlyCollection[0] : null;
  }

  public static BindingSource GetSecGamepadBinding(PlayerAction action)
  {
    ReadOnlyCollection<BindingSource> readOnlyCollection = action.BindingsOfTypes(BindingSourceType.DeviceBindingSource);
    return readOnlyCollection.Count >= 2 ? readOnlyCollection[1] : null;
  }

  public static BindingSource GetBinding(PlayerAction action, ButtonType type)
  {
    switch (type)
    {
      case ButtonType.PRIMARY:
        return GetPrimKeyBinding(action);
      case ButtonType.SECONDARY:
        return GetSecKeyBinding(action);
      case ButtonType.GAMEPAD:
        return GetPrimGamepadBinding(action);
      case ButtonType.GAMEPAD_SEC:
        return GetSecGamepadBinding(action);
      default:
        return null;
    }
  }

  public static string GetButtonKey(PlayerAction action, ButtonType type)
  {
    BindingSource binding = GetBinding(action, type);
    switch (binding)
    {
      case KeyBindingSource _:
        return (binding as KeyBindingSource).Control.GetInclude(0).ToString();
      case MouseBindingSource _:
        return (binding as MouseBindingSource).Control.ToString();
      case DeviceBindingSource _:
        return (binding as DeviceBindingSource).Control.ToString();
      default:
        return null;
    }
  }

  public static bool IsProtected(PlayerAction action)
  {
    foreach (PlayerAction playerAction in Instance.PROTECTED_ACTIONS)
    {
      if (playerAction == action)
        return true;
    }
    return false;
  }

  public static bool IsProtected(params Key[] keys)
  {
    foreach (Key key1 in keys)
    {
      foreach (Key key2 in Instance.PROTECTED_KEYS)
      {
        if (key2 == key1)
          return true;
      }
    }
    return false;
  }

  public static PlayerAction GetAction(string actionName) => Actions.Get(actionName) ?? PauseActions.Get(actionName);

  public static Key GetDefaultMenuKey() => !Application.isEditor ? Key.Escape : Key.Backquote;

  public void SetInputMode(InputMode mode, int handle)
  {
    if (inputModeStack.Push(mode, handle))
      SetInputMode(mode);
    else
      Log.Error("Failed to set input mode!", nameof (mode), mode, nameof (handle), handle);
  }

  public void ClearInputMode(int handle)
  {
    inputModeStack.Pop(handle);
    InputMode mode = inputModeStack.Peek();
    if (mode == GetInputMode())
      return;
    SetInputMode(mode);
  }

  private void SetInputMode(InputMode mode)
  {
    actions.Enabled = mode == InputMode.DEFAULT;
    pauseActions.Enabled = mode == InputMode.PAUSE;
    engageActions.Enabled = mode == InputMode.ENGAGEMENT;
    lookActions.Enabled = mode == InputMode.LOOK_ONLY;
    Log.Debug("Setting input mode", nameof (mode), mode);
  }

  public InputMode GetInputMode()
  {
    if (actions.Enabled)
      return InputMode.DEFAULT;
    if (pauseActions.Enabled)
      return InputMode.PAUSE;
    if (engageActions.Enabled)
      return InputMode.ENGAGEMENT;
    return !lookActions.Enabled ? InputMode.NONE : InputMode.LOOK_ONLY;
  }

  public Vector2 GetMouseLook() => Actions.GetMouseLook() + LookActions.GetMouseLook();

  public Vector2 GetMouseLookRaw() => Actions.GetMouseLookRaw() + LookActions.GetMouseLookRaw();

  public static BindingV01 ToBinding(PlayerAction action)
  {
    BindingV01 binding = new BindingV01();
    binding.action = action.Name;
    BindingSource primKeyBinding = GetPrimKeyBinding(action);
    KeyCombo control;
    switch (primKeyBinding)
    {
      case KeyBindingSource _:
        BindingV01 bindingV01_1 = binding;
        control = ((KeyBindingSource) primKeyBinding).Control;
        int include1 = (int) control.GetInclude(0);
        bindingV01_1.primKey = include1;
        break;
      case MouseBindingSource _:
        binding.primMouse = (int) ((MouseBindingSource) primKeyBinding).Control;
        break;
    }
    BindingSource secKeyBinding = GetSecKeyBinding(action);
    switch (secKeyBinding)
    {
      case KeyBindingSource _:
        BindingV01 bindingV01_2 = binding;
        control = ((KeyBindingSource) secKeyBinding).Control;
        int include2 = (int) control.GetInclude(0);
        bindingV01_2.secKey = include2;
        break;
      case MouseBindingSource _:
        binding.secMouse = (int) ((MouseBindingSource) secKeyBinding).Control;
        break;
    }
    BindingSource primGamepadBinding = GetPrimGamepadBinding(action);
    if (primGamepadBinding is DeviceBindingSource)
      binding.gamepad = (int) ((DeviceBindingSource) primGamepadBinding).Control;
    return binding;
  }

  public static bool AddOrReplaceBinding(PlayerAction action, PlayerAction source) => AddOrReplaceBinding(action, ToBinding(source));

  public static bool AddOrReplaceBinding(PlayerAction action, BindingV01 binding)
  {
    if (IsProtected(action))
    {
      Log.Warning("Ignoring key override for protected binding, using defaults.", nameof (binding), binding.action);
      return false;
    }
    if (IsProtected((Key) binding.primKey, (Key) binding.secKey))
    {
      Log.Warning("Ignoring key override for protected key, using defaults.", nameof (binding), binding.action, "binding.primKey", binding.primKey, "binding.secKey", binding.secKey);
      return false;
    }
    if (binding.primKey != 0)
      action.AddOrReplaceBinding(GetPrimKeyBinding(action), new KeyBindingSource(new Key[1]
      {
        (Key) binding.primKey
      }));
    else if (binding.primMouse != 0)
      action.AddOrReplaceBinding(GetPrimKeyBinding(action), new MouseBindingSource((Mouse) binding.primMouse));
    else
      action.AddOrReplaceBinding(GetPrimKeyBinding(action), null);
    if (binding.secKey != 0)
      action.AddOrReplaceBinding(GetSecKeyBinding(action), new KeyBindingSource(new Key[1]
      {
        (Key) binding.secKey
      }));
    else if (binding.secMouse != 0)
      action.AddOrReplaceBinding(GetSecKeyBinding(action), new MouseBindingSource((Mouse) binding.secMouse));
    else
      action.AddOrReplaceBinding(GetSecKeyBinding(action), null);
    if (binding.gamepad != 0)
      action.AddOrReplaceBinding(GetPrimGamepadBinding(action), new DeviceBindingSource((InputControlType) binding.gamepad));
    else
      action.AddOrReplaceBinding(GetPrimGamepadBinding(action), null);
    return true;
  }

  public enum ButtonType
  {
    PRIMARY,
    SECONDARY,
    GAMEPAD,
    GAMEPAD_SEC,
  }

  public class PlayerLookActions : PlayerActionSet
  {
    public PlayerAction lookXPos;
    public PlayerAction lookXNeg;
    public PlayerOneAxisAction lookX;
    public PlayerAction lookYPos;
    public PlayerAction lookYNeg;
    public PlayerOneAxisAction lookY;

    public PlayerLookActions()
    {
      lookXNeg = CreatePlayerAction("LookXNeg");
      lookXPos = CreatePlayerAction("LookXPos");
      lookX = CreateOneAxisPlayerAction(lookXNeg, lookXPos);
      lookYNeg = CreatePlayerAction("LookYNeg");
      lookYPos = CreatePlayerAction("LookYPos");
      lookY = CreateOneAxisPlayerAction(lookYNeg, lookYPos);
    }

    public Vector2 GetMouseLook() => new Vector2((float) (OneAxisInputControl) lookX, (float) (OneAxisInputControl) lookY);

    public Vector2 GetMouseLookRaw() => new Vector2(lookX.RawValue, lookY.RawValue);
  }

  public class PlayerActions : PlayerLookActions
  {
    public PlayerAction attack;
    public PlayerAction vac;
    public PlayerAction slimeFilter;
    public PlayerAction jump;
    public PlayerAction run;
    public PlayerAction interact;
    public PlayerAction accept;
    public PlayerAction menu;
    public PlayerAction radarToggle;
    public PlayerAction openMap;
    public PlayerAction pedia;
    public PlayerAction reportIssue;
    public PlayerAction screenshot;
    public PlayerAction recordGif;
    public PlayerAction verticalNeg;
    public PlayerAction verticalPos;
    public PlayerOneAxisAction vertical;
    public PlayerAction horizontalNeg;
    public PlayerAction horizontalPos;
    public PlayerOneAxisAction horizontal;
    public PlayerAction slot1;
    public PlayerAction slot2;
    public PlayerAction slot3;
    public PlayerAction slot4;
    public PlayerAction slot5;
    public PlayerAction prevSlot;
    public PlayerAction nextSlot;
    public PlayerAction light;
    public PlayerAction burst;
    public PlayerAction toggleGadgetMode;

    public PlayerActions(SRInput srInput)
    {
      attack = CreatePlayerAction("Attack");
      vac = CreatePlayerAction("Vac");
      slimeFilter = CreatePlayerAction("SlimeFilter");
      jump = CreatePlayerAction("Jump");
      run = CreatePlayerAction("Run");
      interact = CreatePlayerAction("Interact");
      accept = CreatePlayerAction("Accept1");
      menu = CreatePlayerAction("Menu");
      radarToggle = CreatePlayerAction("RadarToggle");
      openMap = CreatePlayerAction("OpenMap");
      pedia = CreatePlayerAction("Pedia");
      reportIssue = CreatePlayerAction("ReportIssue");
      screenshot = CreatePlayerAction("Screenshot");
      recordGif = CreatePlayerAction("RecordGif");
      verticalNeg = CreatePlayerAction("VerticalNeg");
      verticalPos = CreatePlayerAction("VerticalPos");
      vertical = CreateOneAxisPlayerAction(verticalNeg, verticalPos);
      horizontalNeg = CreatePlayerAction("HorizontalNeg");
      horizontalPos = CreatePlayerAction("HorizontalPos");
      horizontal = CreateOneAxisPlayerAction(horizontalNeg, horizontalPos);
      slot1 = CreatePlayerAction("Slot1");
      slot2 = CreatePlayerAction("Slot2");
      slot3 = CreatePlayerAction("Slot3");
      slot4 = CreatePlayerAction("Slot4");
      slot5 = CreatePlayerAction("Slot5");
      prevSlot = CreatePlayerAction("PrevSlot");
      nextSlot = CreatePlayerAction("NextSlot");
      light = CreatePlayerAction("Light");
      burst = CreatePlayerAction("Burst");
      toggleGadgetMode = CreatePlayerAction("ToggleGadgetMode");
      ListenOptions.IncludeMouseButtons = true;
      ListenOptions.IncludeModifiersAsFirstClassKeys = true;
      ListenOptions.UnsetDuplicateBindingsOnSet = true;
      ListenOptions.DisallowBindingKeys = srInput.PROTECTED_KEYS;
      ListenOptions.DisallowBindingControls = srInput.PROTECTED_CONTROLS;
    }
  }

  public class PlayerPauseActions : PlayerActionSet
  {
    public PlayerAction submit;
    public PlayerAction altSubmit;
    public PlayerAction cancel;
    public PlayerAction menuUp;
    public PlayerAction menuDown;
    public PlayerAction menuLeft;
    public PlayerAction menuRight;
    public PlayerAction menuTabLeft;
    public PlayerAction menuTabRight;
    public PlayerAction menuScrollUp;
    public PlayerAction menuScrollDown;
    public PlayerAction unmenu;
    public PlayerAction closeMap;
    public PlayerAction switchUser;

    public PlayerPauseActions()
    {
      submit = CreatePlayerAction("Submit");
      altSubmit = CreatePlayerAction("AltSubmit");
      cancel = CreatePlayerAction("Cancel");
      unmenu = CreatePlayerAction("Unmenu");
      menuUp = CreatePlayerAction("MenuUp");
      menuDown = CreatePlayerAction("MenuDown");
      menuLeft = CreatePlayerAction("MenuLeft");
      menuRight = CreatePlayerAction("MenuRight");
      menuTabLeft = CreatePlayerAction("MenuTabLeft");
      menuTabRight = CreatePlayerAction("MenuTabRight");
      menuScrollUp = CreatePlayerAction("MenuScrollUp");
      menuScrollDown = CreatePlayerAction("MenuScrollDown");
      closeMap = CreatePlayerAction("CloseMap");
      switchUser = CreatePlayerAction("SwitchUser");
    }
  }

  public class PlayerEngageActions : PlayerActionSet
  {
    public PlayerAction engage;

    public PlayerEngageActions() => engage = CreatePlayerAction("Engage");
  }

  public enum InputMode
  {
    NONE,
    DEFAULT,
    PAUSE,
    ENGAGEMENT,
    LOOK_ONLY,
  }
}
