// Decompiled with JetBrains decompiler
// Type: GamepadSettingsUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine.UI;

public class GamepadSettingsUI : BaseUI
{
  public Toggle swapSticksToggle;
  public Toggle invertGamepadLookYToggle;
  public Toggle sprintHoldToggle;
  public Slider lookSensitivityXSlider;
  public Slider lookSensitivityYSlider;

  public override void Awake()
  {
    base.Awake();
    swapSticksToggle.isOn = SRSingleton<GameContext>.Instance.InputDirector.GetSwapSticks();
    invertGamepadLookYToggle.isOn = SRSingleton<GameContext>.Instance.InputDirector.GetInvertGamepadLookY();
    sprintHoldToggle.isOn = SRSingleton<GameContext>.Instance.OptionsDirector.sprintHold;
    lookSensitivityXSlider.value = SRSingleton<GameContext>.Instance.InputDirector.GamepadLookSensitivityX;
    lookSensitivityYSlider.value = SRSingleton<GameContext>.Instance.InputDirector.GamepadLookSensitivityY;
  }

  public override void Close() => base.Close();

  public void ToggleInvertGamepadLookY() => SRSingleton<GameContext>.Instance.InputDirector.SetInvertGamepadLookY(invertGamepadLookYToggle.isOn);

  public void ToggleSwapSticks() => SRSingleton<GameContext>.Instance.InputDirector.SetSwapSticks(swapSticksToggle.isOn);

  public void ToggleSprintHold() => SRSingleton<GameContext>.Instance.OptionsDirector.sprintHold = sprintHoldToggle.isOn;

  public void OnLookSensitivityXChanged() => SRSingleton<GameContext>.Instance.InputDirector.GamepadLookSensitivityX = lookSensitivityXSlider.value;

  public void OnLookSensitivityYChanged() => SRSingleton<GameContext>.Instance.InputDirector.GamepadLookSensitivityY = lookSensitivityYSlider.value;
}
