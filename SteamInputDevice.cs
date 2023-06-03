// Decompiled with JetBrains decompiler
// Type: SteamInputDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using Steamworks;
using System.Collections.Generic;
using UnityEngine;

public class SteamInputDevice : InputDevice
{
  private ControllerHandle_t controller;
  private ControllerActionSetHandle_t gameActions;
  private ControllerActionSetHandle_t menuActions;
  private Dictionary<string, DigitalControl> digitalControls = new Dictionary<string, DigitalControl>();
  private Dictionary<string, AnalogControl> analogControls = new Dictionary<string, AnalogControl>();
  private byte startBtnState;
  private EControllerActionOrigin[] tmpOrigins = new EControllerActionOrigin[8];

  public SteamInputDevice(
    ControllerHandle_t controller,
    ControllerActionSetHandle_t gameActions,
    ControllerActionSetHandle_t menuActions)
    : base("SteamController")
  {
    this.controller = controller;
    this.gameActions = gameActions;
    this.menuActions = menuActions;
    AddNewDigitalControl("Vac");
    AddNewDigitalControl("Attack");
    AddNewDigitalControl("Jump");
    AddNewDigitalControl("Run");
    AddNewDigitalControl("Interact");
    AddNewDigitalControl("Menu");
    AddNewDigitalControl("RadarToggle");
    AddNewDigitalControl("OpenMap");
    AddNewDigitalControl("Pedia");
    AddNewDigitalControl("ReportIssue");
    AddNewDigitalControl("Screenshot");
    AddNewDigitalControl("RecordGif");
    AddNewDigitalControl("Slot1");
    AddNewDigitalControl("Slot2");
    AddNewDigitalControl("Slot3");
    AddNewDigitalControl("Slot4");
    AddNewDigitalControl("Slot5");
    AddNewDigitalControl("Light");
    AddNewDigitalControl("Burst");
    AddNewDigitalControl("PrevSlot");
    AddNewDigitalControl("NextSlot");
    AddNewDigitalControl("ToggleGadgetMode");
    AddNewAnalogControl("Move", "VerticalPos", "VerticalNeg", "HorizontalNeg", "HorizontalPos", 1f);
    AddNewAnalogControl("Look", "LookYNeg", "LookYPos", "LookXNeg", "LookXPos", 0.02f);
    AddNewDigitalControl("MenuUp", true);
    AddNewDigitalControl("MenuDown", true);
    AddNewDigitalControl("MenuLeft", true);
    AddNewDigitalControl("MenuRight", true);
    AddNewDigitalControl("MenuTabLeft", true);
    AddNewDigitalControl("MenuTabRight", true);
    AddNewDigitalControl("MenuScrollUp", true);
    AddNewDigitalControl("MenuScrollDown", true);
    AddNewDigitalControl("Submit", true);
    AddNewDigitalControl("AltSubmit", true);
    AddNewDigitalControl("Cancel", true);
    AddNewDigitalControl("Unmenu", true);
  }

  private void AddNewDigitalControl(string name, bool isMenu = false) => digitalControls[name] = new DigitalControl(name, isMenu);

  private void AddNewAnalogControl(
    string name,
    string upName,
    string downName,
    string leftName,
    string rightName,
    float scale)
  {
    analogControls[name] = new AnalogControl(name, upName, downName, leftName, rightName, scale);
  }

  private bool EnsureNotLeftoverStartButtonChange(
    DigitalControl control,
    InputDigitalActionData_t data)
  {
    if (!(control.control.Name == "Menu") && !(control.control.Name == "Unmenu"))
      return true;
    if (data.bState == startBtnState)
      return false;
    startBtnState = data.bState;
    return true;
  }

  public override void Update(ulong updateTick, float deltaTime)
  {
    SetPauseMode(Time.timeScale == 0.0 || Levels.isSpecial());
    SteamController.RunFrame();
    base.Update(updateTick, deltaTime);
    if (controller.m_ControllerHandle == 0UL)
      return;
    foreach (DigitalControl control in digitalControls.Values)
    {
      InputDigitalActionData_t digitalActionData = SteamController.GetDigitalActionData(controller, control.action);
      if (control.control.Enabled && digitalActionData.bActive != 0 && EnsureNotLeftoverStartButtonChange(control, digitalActionData) && control.control.UpdateWithState(digitalActionData.bState > 0, updateTick, deltaTime))
      {
        control.control.LastInputTypeChangedTick = updateTick;
        control.control.LastInputType = BindingSourceType.DeviceBindingSource;
        RequestActivation();
      }
    }
    foreach (AnalogControl analogControl in analogControls.Values)
    {
      InputAnalogActionData_t analogActionData = SteamController.GetAnalogActionData(controller, analogControl.action);
      if (analogControl.upControl.Enabled && analogControl.upControl.UpdateWithRawValue(Mathf.Max(0.0f, analogActionData.y * analogControl.scale), updateTick, deltaTime))
      {
        analogControl.upControl.LastInputTypeChangedTick = updateTick;
        analogControl.upControl.LastInputType = BindingSourceType.DeviceBindingSource;
        RequestActivation();
      }
      if (analogControl.downControl.Enabled && analogControl.downControl.UpdateWithRawValue(Mathf.Max(0.0f, -analogActionData.y * analogControl.scale), updateTick, deltaTime))
      {
        analogControl.downControl.LastInputTypeChangedTick = updateTick;
        analogControl.downControl.LastInputType = BindingSourceType.DeviceBindingSource;
        RequestActivation();
      }
      if (analogControl.leftControl.Enabled && analogControl.leftControl.UpdateWithRawValue(Mathf.Max(0.0f, -analogActionData.x * analogControl.scale), updateTick, deltaTime))
      {
        analogControl.leftControl.LastInputTypeChangedTick = updateTick;
        analogControl.leftControl.LastInputType = BindingSourceType.DeviceBindingSource;
        RequestActivation();
      }
      if (analogControl.rightControl.Enabled && analogControl.rightControl.UpdateWithRawValue(Mathf.Max(0.0f, analogActionData.x * analogControl.scale), updateTick, deltaTime))
      {
        analogControl.rightControl.LastInputTypeChangedTick = updateTick;
        analogControl.rightControl.LastInputType = BindingSourceType.DeviceBindingSource;
        RequestActivation();
      }
    }
  }

  public void SetPauseMode(bool pauseMode)
  {
    if (pauseMode)
      SteamController.ActivateActionSet(controller, menuActions);
    else
      SteamController.ActivateActionSet(controller, gameActions);
  }

  public ControllerHandle_t GetController() => controller;

  public int GetOrigin(string actionName, bool isMenuAction)
  {
    ControllerActionSetHandle_t actionSetHandle = isMenuAction ? menuActions : gameActions;
    if (digitalControls.ContainsKey(actionName))
    {
      SteamController.GetDigitalActionOrigins(controller, actionSetHandle, digitalControls[actionName].action, tmpOrigins);
    }
    else
    {
      if (!analogControls.ContainsKey(actionName))
        return 0;
      SteamController.GetAnalogActionOrigins(controller, actionSetHandle, analogControls[actionName].action, tmpOrigins);
    }
    return (int) tmpOrigins[0];
  }

  private class DigitalControl
  {
    public PlayerAction control;
    public ControllerDigitalActionHandle_t action;

    public DigitalControl(string name, bool isMenu = false)
    {
      control = isMenu ? SRInput.PauseActions.Get(name) : SRInput.Actions.Get(name);
      action = SteamController.GetDigitalActionHandle(name);
    }
  }

  private class AnalogControl
  {
    public PlayerAction upControl;
    public PlayerAction downControl;
    public PlayerAction leftControl;
    public PlayerAction rightControl;
    public ControllerAnalogActionHandle_t action;
    public float scale;

    public AnalogControl(
      string name,
      string upName,
      string downName,
      string leftName,
      string rightName,
      float scale)
    {
      upControl = SRInput.Actions.Get(upName);
      downControl = SRInput.Actions.Get(downName);
      leftControl = SRInput.Actions.Get(leftName);
      rightControl = SRInput.Actions.Get(rightName);
      action = SteamController.GetAnalogActionHandle(name);
      this.scale = scale;
    }
  }
}
