// Decompiled with JetBrains decompiler
// Type: ActivateUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class ActivateUI : MonoBehaviour
{
  public string key = "m.press_to_activate";
  public GameObject normalPrompt;
  public GameObject gamepadPrompt;
  public TMP_Text normalPromptText;
  public TMP_Text preGamepadText;
  public TMP_Text postGamepadText;
  public bool useGamepadAlt;
  private MessageDirector messageDir;
  private InputDirector inputDir;

  public void Awake()
  {
    messageDir = SRSingleton<GameContext>.Instance.MessageDirector;
    messageDir.RegisterBundlesListener(OnBundlesAvailable);
    inputDir = SRSingleton<GameContext>.Instance.InputDirector;
    inputDir.onKeysChanged += OnKeysChanged;
    ResetActivePrompt();
  }

  public void Update() => ResetActivePrompt();

  public void OnDestroy()
  {
    messageDir.UnregisterBundlesListener(OnBundlesAvailable);
    inputDir.onKeysChanged -= OnKeysChanged;
  }

  private void OnBundlesAvailable(MessageDirector messageDir) => OnKeysChanged();

  private void OnKeysChanged()
  {
    MessageBundle bundle = messageDir.GetBundle("ui");
    normalPromptText.text = bundle.Get(key, new string[1]
    {
      XlateKeyText.GetKeyString(bundle, SRInput.Actions.interact, true)
    });
    if (preGamepadText != null)
      preGamepadText.text = bundle.Get(key + ".pre_gamepad");
    if (!(postGamepadText != null))
      return;
    postGamepadText.text = bundle.Get(key + ".post_gamepad");
  }

  private void ResetActivePrompt()
  {
    bool flag = useGamepadAlt && InputDirector.UsingGamepad();
    normalPrompt.SetActive(!flag);
    if (!(gamepadPrompt != null))
      return;
    gamepadPrompt.SetActive(flag);
  }
}
