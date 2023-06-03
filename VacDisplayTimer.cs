// Decompiled with JetBrains decompiler
// Type: VacDisplayTimer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class VacDisplayTimer : MonoBehaviour
{
  [Tooltip("Text object to update with the time.")]
  public TimerText timer;
  [Tooltip("Renderer containing the color spiral.")]
  public Renderer renderer;
  private QuicksilverEnergyGenerator generator;
  private TimerSpiral spiral;
  private TimeSource source;

  public void Awake()
  {
    spiral = renderer.gameObject.AddComponent<TimerSpiral>();
    timer.UpdateTimeRemaining(new double?());
  }

  public void Update()
  {
    double? secondsRemaining = source == null ? new double?() : source.GetTimeRemaining();
    double? nullable = source == null ? new double?() : source.GetWarningTimeSeconds();
    timer.UpdateTimeRemaining(secondsRemaining);
    spiral.SetWarningThreshold(source != null ? (!secondsRemaining.HasValue || !nullable.HasValue ? 0.2f : (secondsRemaining.Value >= nullable.Value ? 0.0f : 1f)) : 0.0f);
  }

  public void OnDestroy() => SetTimeSource(null);

  public void SetQuicksilverEnergyGenerator(QuicksilverEnergyGenerator generator)
  {
    if (this.generator != null)
      this.generator.onStateChanged -= OnQuicksilverEnergyGeneratorStateChanged;
    SetTimeSource(generator);
    this.generator = generator;
    if (!(this.generator != null))
      return;
    this.generator.onStateChanged += OnQuicksilverEnergyGeneratorStateChanged;
    OnQuicksilverEnergyGeneratorStateChanged();
  }

  private void OnQuicksilverEnergyGeneratorStateChanged() => SetTimeSource(generator.GetState() == QuicksilverEnergyGenerator.State.ACTIVE || generator.GetState() == QuicksilverEnergyGenerator.State.COUNTDOWN ? generator : (TimeSource) null);

  public void SetTimeSource(TimeSource source)
  {
    this.source = source;
    spiral.SetTimeSource(source);
  }

  public interface TimeSource
  {
    double? GetTimeRemaining();

    double? GetMaxTimeRemaining();

    double? GetWarningTimeSeconds();
  }
}
