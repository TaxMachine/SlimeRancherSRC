// Decompiled with JetBrains decompiler
// Type: TimerText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class TimerText : Text
{
  private int? priorMinutes;

  protected override void Start()
  {
    base.Start();
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    text = SRSingleton<SceneContext>.Instance.TimeDirector.FormatTimeMinutes(new int?());
  }

  public void UpdateTimeRemaining(double? secondsRemaining)
  {
    int? minutes = RoundTimeToMinutes(secondsRemaining);
    int? nullable = minutes;
    int? priorMinutes = this.priorMinutes;
    if (nullable.GetValueOrDefault() == priorMinutes.GetValueOrDefault() & nullable.HasValue == priorMinutes.HasValue)
      return;
    text = SRSingleton<SceneContext>.Instance.TimeDirector.FormatTimeMinutes(minutes);
    this.priorMinutes = minutes;
  }

  private int? RoundTimeToMinutes(double? timeInSeconds) => !timeInSeconds.HasValue ? new int?() : new int?(Mathf.CeilToInt((float) timeInSeconds.Value * 0.0166666675f));
}
