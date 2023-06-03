// Decompiled with JetBrains decompiler
// Type: XlateText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XlateText : MonoBehaviour
{
  private const string XBOX_SUFFIX = "_xbox";
  private const string XBOX_GAME_PREVIEW_SUFFIX = "_xboxgp";
  public string bundlePath = "ui";
  public string key;
  public string[] args;
  public bool addPlatformSuffix;
  private MessageBundle bundle;
  private Text text;
  private TMP_Text meshText;

  public void Awake()
  {
    text = GetComponent<Text>();
    meshText = GetComponent<TMP_Text>();
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(InitBundles);
  }

  public void OnDestroy()
  {
    if (!(SRSingleton<GameContext>.Instance != null))
      return;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(InitBundles);
  }

  public void SetKey(string key)
  {
    this.key = key;
    UpdateText();
  }

  private void UpdateText()
  {
    string[] strArray = new string[args.Length];
    for (int index = 0; index < args.Length; ++index)
      strArray[index] = bundle.Xlate(args[index]);
    string key = this.key;
    if (text != null)
      text.text = bundle.Get(key, strArray);
    if (!(meshText != null))
      return;
    meshText.text = bundle.Get(key, strArray);
  }

  public void InitBundles(MessageDirector msgDir)
  {
    bundle = msgDir.GetBundle(bundlePath);
    UpdateText();
  }
}
