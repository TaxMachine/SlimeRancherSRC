// Decompiled with JetBrains decompiler
// Type: XlateTextEllipsize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XlateTextEllipsize : MonoBehaviour
{
  public string bundlePath = "ui";
  public string key;
  public string[] args;
  [Tooltip("Time between ellipsis steps (in real-world clock time).")]
  public float timePerChange = 1f;
  private MessageBundle bundle;
  private Text text;
  private TMP_Text meshText;
  private float ellipsisChangeTime;
  private int ellipsisCount;
  private string unellipsizedText;

  public void Awake()
  {
    text = GetComponent<Text>();
    meshText = GetComponent<TMP_Text>();
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(InitBundles);
  }

  public void OnDestroy()
  {
    if (!(SRSingleton<GameContext>.Instance != null) || !(SRSingleton<GameContext>.Instance.MessageDirector != null))
      return;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(InitBundles);
  }

  public void InitBundles(MessageDirector msgDir)
  {
    bundle = msgDir.GetBundle(bundlePath);
    string[] strArray = new string[args.Length];
    for (int index = 0; index < args.Length; ++index)
      strArray[index] = bundle.Xlate(args[index]);
    unellipsizedText = bundle.Get(key, strArray);
    if (text != null)
      text.text = unellipsizedText;
    if (!(meshText != null))
      return;
    meshText.text = unellipsizedText;
  }

  public void Update()
  {
    if (Time.unscaledTime <= (double) ellipsisChangeTime)
      return;
    if (text != null)
      text.text = bundle.Xlate(MessageUtil.Compose("m.ellipsize" + ellipsisCount, new string[1]
      {
        MessageUtil.Taint(unellipsizedText)
      }));
    if (meshText != null)
      meshText.text = bundle.Xlate(MessageUtil.Compose("m.ellipsize" + ellipsisCount, new string[1]
      {
        MessageUtil.Taint(unellipsizedText)
      }));
    ellipsisCount = (ellipsisCount + 1) % 4;
    ellipsisChangeTime = Time.unscaledTime + timePerChange;
  }
}
