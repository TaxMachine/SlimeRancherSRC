// Decompiled with JetBrains decompiler
// Type: TimerSpiral
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TimerSpiral : MonoBehaviour
{
  private const float MAX_CHANGE_PER_FRAME = 0.004f;
  private VacDisplayTimer.TimeSource source;
  private Renderer renderer;
  private double ratio;

  public void Awake()
  {
    renderer = GetComponent<Renderer>();
    renderer.material.SetFloat("_Timer", 0.0f);
    ratio = 0.0;
  }

  public void SetTimeSource(VacDisplayTimer.TimeSource source)
  {
    this.source = source;
    double? timeRemaining = source?.GetTimeRemaining();
    double? maxTimeRemaining = source?.GetMaxTimeRemaining();
    ratio = !maxTimeRemaining.HasValue ? 0.0 : 0.5 / maxTimeRemaining.Value;
    renderer.material.SetFloat("_Timer", timeRemaining.HasValue ? (float) (ratio * timeRemaining.Value) : 0.0f);
  }

  public void SetWarningThreshold(float percentage) => renderer.material.SetFloat("_WarningThreshold", percentage);

  public void Update()
  {
    if (ratio <= 0.0 || source == null)
      return;
    float num1 = renderer.material.GetFloat("_Timer");
    float num2 = Mathf.Clamp01((float) (ratio * source.GetTimeRemaining().Value));
    renderer.material.SetFloat("_Timer", num1 + Mathf.Max(-0.004f, Mathf.Min(0.004f, num2 - num1)));
  }

  public void OnDestroy() => renderer.material.SetFloat("_Timer", 0.0f);
}
