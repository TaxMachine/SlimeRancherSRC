// Decompiled with JetBrains decompiler
// Type: TimeLimitV2IntroUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class TimeLimitV2IntroUI : MonoBehaviour
{
  [Tooltip("Countdown text.")]
  public TMP_Text text;
  [Tooltip("Duration, in real-time seconds, to countdown.")]
  public float countdown = 3f;
  private float time;

  public void Awake() => time = Time.unscaledTime + 3f;

  public void Update()
  {
    text.text = string.Format("{0}", Mathf.CeilToInt(time - Time.unscaledTime));
    if (Time.unscaledTime < (double) time)
      return;
    Destroyer.Destroy(gameObject, "TimeLimitV2IntroUI.Update");
  }
}
