// Decompiled with JetBrains decompiler
// Type: XlateKeyText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XlateKeyText : MonoBehaviour
{
  public string bundlePath = "ui";
  public string key;
  public string inputKey;
  private MessageBundle bundle;
  private Text text;
  private TMP_Text meshText;

  public void Awake()
  {
    text = GetComponent<Text>();
    meshText = GetComponent<TMP_Text>();
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(InitBundles);
  }

  public void InitBundles(MessageDirector msgDir)
  {
    bundle = msgDir.GetBundle(bundlePath);
    SRSingleton<GameContext>.Instance.InputDirector.onKeysChanged -= OnKeysChanged;
    SRSingleton<GameContext>.Instance.InputDirector.onKeysChanged += OnKeysChanged;
    OnKeysChanged();
  }

  public void OnDestroy()
  {
    if (!(SRSingleton<GameContext>.Instance != null))
      return;
    SRSingleton<GameContext>.Instance.InputDirector.onKeysChanged -= OnKeysChanged;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(InitBundles);
  }

  public void OnKeysChanged()
  {
    if (text != null)
      text.text = bundle.Get(key, new string[1]
      {
        GetKeyString(bundle, inputKey, false)
      });
    if (!(meshText != null))
      return;
    meshText.text = bundle.Get(key, new string[1]
    {
      GetKeyString(bundle, inputKey, false)
    });
  }

  public static string GetKeyString(
    MessageBundle bundle,
    string inputKey,
    bool primaryOnly,
    bool ignoreGamepad = false)
  {
    return GetKeyString(bundle, SRInput.Actions.Get(inputKey), primaryOnly, ignoreGamepad);
  }

  public static string GetKeyString(
    MessageBundle bundle,
    PlayerAction inputKey,
    bool primaryOnly,
    bool ignoreGamepad = false)
  {
    string buttonKey1 = SRInput.GetButtonKey(inputKey, SRInput.ButtonType.PRIMARY);
    string buttonKey2 = SRInput.GetButtonKey(inputKey, SRInput.ButtonType.SECONDARY);
    string buttonKey3 = SRInput.GetButtonKey(inputKey, SRInput.ButtonType.GAMEPAD);
    string compoundKey = "m.keys.0";
    if (InputDirector.UsingGamepad() && !ignoreGamepad && buttonKey3 != null)
      compoundKey = MessageUtil.Tcompose("m.keys.1", new string[1]
      {
        XlateKey(buttonKey3)
      });
    else if (buttonKey1 != null && buttonKey2 != null && !primaryOnly)
      compoundKey = MessageUtil.Tcompose("m.keys.2", new string[2]
      {
        XlateKey(buttonKey1),
        XlateKey(buttonKey2)
      });
    else if (buttonKey1 != null)
      compoundKey = MessageUtil.Tcompose("m.keys.1", new string[1]
      {
        XlateKey(buttonKey1)
      });
    else if (buttonKey2 != null)
      compoundKey = MessageUtil.Tcompose("m.keys.1", new string[1]
      {
        XlateKey(buttonKey2)
      });
    return bundle.Xlate(compoundKey);
  }

  public static string XlateKey(KeyCode key) => XlateKey(key.ToString());

  public static string XlateKey(string keyStr)
  {
    MessageBundle bundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("keys");
    if (bundle.Exists("m." + GetPlatformStr() + "." + keyStr))
      return bundle.Get("m." + GetPlatformStr() + "." + keyStr);
    return bundle.Exists("m." + keyStr) ? bundle.Get("m." + keyStr) : keyStr;
  }

  public static string XlateKey(InputControlType key) => XlateKey(key.ToString());

  public static string XlateKey(Key key) => XlateKey(key.ToString());

  public static string XlateKey(Mouse key) => XlateKey(key.ToString());

  private static string GetPlatformStr() => "win";
}
