// Decompiled with JetBrains decompiler
// Type: GamepadVisualPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using TMPro;
using UnityEngine;

public class GamepadVisualPanel : MonoBehaviour
{
  public TMP_Text aText;
  public TMP_Text bText;
  public TMP_Text xText;
  public TMP_Text yText;
  public TMP_Text lbText;
  public TMP_Text ltText;
  public TMP_Text rbText;
  public TMP_Text rtText;
  public TMP_Text upText;
  public TMP_Text rightText;
  public TMP_Text downText;
  public TMP_Text leftText;
  public TMP_Text backText;
  public TMP_Text startText;
  public TMP_Text leftStickText;
  public TMP_Text rightStickText;

  public void ClearAllGamepadText(MessageBundle uiBundle)
  {
    ClearGamepadText(uiBundle, aText, false, InputControlType.Action1);
    ClearGamepadText(uiBundle, bText, false, InputControlType.Action2);
    ClearGamepadText(uiBundle, xText, false, InputControlType.Action3);
    ClearGamepadText(uiBundle, yText, false, InputControlType.Action4);
    ClearGamepadText(uiBundle, ltText, false, InputControlType.LeftTrigger);
    ClearGamepadText(uiBundle, lbText, false, InputControlType.LeftBumper);
    ClearGamepadText(uiBundle, rtText, false, InputControlType.RightTrigger);
    ClearGamepadText(uiBundle, rbText, false, InputControlType.RightBumper);
    ClearGamepadText(uiBundle, upText, false, InputControlType.DPadUp);
    ClearGamepadText(uiBundle, rightText, false, InputControlType.DPadRight);
    ClearGamepadText(uiBundle, downText, false, InputControlType.DPadDown);
    ClearGamepadText(uiBundle, leftText, false, InputControlType.DPadLeft);
    ClearGamepadText(uiBundle, backText, false, InputControlType.Back);
    ClearGamepadText(uiBundle, startText, false, InputControlType.Start);
    ClearGamepadText(uiBundle, leftStickText, true, InputControlType.LeftStickButton);
    ClearGamepadText(uiBundle, rightStickText, true, InputControlType.RightStickButton);
  }

  private void ClearGamepadText(
    MessageBundle uiBundle,
    TMP_Text text,
    bool isStick,
    InputControlType key)
  {
    if (isStick)
    {
      text.text = uiBundle.Get("m.gamepad_stick", new string[4]
      {
        XlateKeyText.XlateKey(key == InputControlType.LeftStickButton ? "LeftStick" : "RightStick"),
        uiBundle.Get(key == InputControlType.LeftStickButton ^ SRSingleton<GameContext>.Instance.InputDirector.GetSwapSticks() ? "l.move" : "l.view"),
        uiBundle.Get("l.none"),
        uiBundle.Get(string.Format("l.gamepad_{0}_stick_press_action", key == InputControlType.LeftStickButton ? "left" : (object) "right"))
      });
    }
    else
    {
      if (text == null)
        Log.Error("Test was null!");
      if (uiBundle == null)
        Log.Error("uiBundle was null!");
      text.text = uiBundle.Get("m.gamepad_button", new string[2]
      {
        XlateKeyText.XlateKey(key),
        uiBundle.Get("l.none")
      });
    }
  }

  public TMP_Text GetTextForGamepadKey(InputControlType btn)
  {
    switch (btn)
    {
      case InputControlType.DPadUp:
        return upText;
      case InputControlType.DPadDown:
        return downText;
      case InputControlType.DPadLeft:
        return leftText;
      case InputControlType.DPadRight:
        return rightText;
      case InputControlType.LeftTrigger:
        return ltText;
      case InputControlType.RightTrigger:
        return rtText;
      case InputControlType.LeftBumper:
        return lbText;
      case InputControlType.RightBumper:
        return rbText;
      case InputControlType.Action1:
        return aText;
      case InputControlType.Action2:
        return bText;
      case InputControlType.Action3:
        return xText;
      case InputControlType.Action4:
        return yText;
      case InputControlType.Back:
      case InputControlType.Share:
      case InputControlType.View:
        return backText;
      case InputControlType.Start:
      case InputControlType.Options:
      case InputControlType.Menu:
        return startText;
      default:
        return null;
    }
  }

  public TMP_Text GetTextForGamepadStickKey(InputControlType key)
  {
    if (key == InputControlType.LeftStickButton)
      return leftStickText;
    return key == InputControlType.RightStickButton ? rightStickText : null;
  }
}
