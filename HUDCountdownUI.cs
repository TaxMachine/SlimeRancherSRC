// Decompiled with JetBrains decompiler
// Type: HUDCountdownUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class HUDCountdownUI : MonoBehaviour
{
  [Tooltip("Countdown text.")]
  public TMP_Text text;
  private TimeDirector timeDirector;
  private double time;

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    gameObject.SetActive(false);
  }

  public void Update()
  {
    int num = Mathf.CeilToInt((float) ((time - timeDirector.WorldTime()) % 3600.0 / 60.0));
    text.text = string.Format("{0}", num);
    gameObject.SetActive(num >= 0);
  }

  public void SetCountdownTime(double minutes)
  {
    time = timeDirector.HoursFromNow((float) minutes * 0.0166666675f);
    gameObject.SetActive(true);
  }
}
