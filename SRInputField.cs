// Decompiled with JetBrains decompiler
// Type: SRInputField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SRInputField : InputField
{
  public string descKey;
  private bool requestingKeyboard;
  private bool keyboardActive;
  private MessageBundle uiBundle;

  protected override void Start()
  {
    base.Start();
    uiBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui");
  }

  public override void OnUpdateSelected(BaseEventData data)
  {
    bool flag = InputDirector.UsingGamepad();
    if (flag && (bool) (OneAxisInputControl) SRInput.PauseActions.submit)
    {
      requestingKeyboard = true;
      data.Use();
    }
    else if (flag && (bool) (OneAxisInputControl) SRInput.PauseActions.cancel)
    {
      requestingKeyboard = false;
      data.Use();
    }
    else
    {
      if (flag && ((bool) (OneAxisInputControl) SRInput.PauseActions.menuDown || (bool) (OneAxisInputControl) SRInput.PauseActions.menuUp))
        return;
      base.OnUpdateSelected(data);
    }
  }

  protected override void LateUpdate()
  {
    base.LateUpdate();
    if (requestingKeyboard && !keyboardActive)
    {
      ActivateKeyboard();
    }
    else
    {
      if (requestingKeyboard || !keyboardActive)
        return;
      DeactivateKeyboard();
    }
  }

  private void ActivateKeyboard()
  {
    keyboardActive = true;
    SteamDirector.ActivateKeyboard(multiLine, uiBundle.Xlate(descKey), text, ProcessVirtualKeyboardInput);
  }

  private void ProcessVirtualKeyboardInput(string inputString)
  {
    if (inputString != null)
      text = inputString;
    DeactivateInputField();
    requestingKeyboard = false;
  }

  private void DeactivateKeyboard() => keyboardActive = false;
}
