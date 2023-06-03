// Decompiled with JetBrains decompiler
// Type: QuicksilverEnergyGeneratorClockUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuicksilverEnergyGeneratorClockUI : MonoBehaviour
{
  [Tooltip("Energy generator to display.")]
  public QuicksilverEnergyGenerator generator;
  [Tooltip("Text to display the remaining countdown time. (optional)")]
  public Text countdownText;
  [Tooltip("Text to display the remaining active time. (optional)")]
  public Text activeText;
  [Tooltip("Text to display the remaining cooldown time. (optional)")]
  public Text cooldownText;
  [Tooltip("Renderer containing the color spiral.")]
  public Renderer renderer;
  private TimerSpiral spiral;
  private TimeDirector timeDirector;
  private Dictionary<QuicksilverEnergyGenerator.State, Text> stateText = new Dictionary<QuicksilverEnergyGenerator.State, Text>(QuicksilverEnergyGenerator.StateComparer.Instance);

  public void Awake()
  {
    if (countdownText != null)
      stateText[QuicksilverEnergyGenerator.State.COUNTDOWN] = countdownText;
    if (activeText != null)
      stateText[QuicksilverEnergyGenerator.State.ACTIVE] = activeText;
    if (cooldownText != null)
      stateText[QuicksilverEnergyGenerator.State.COOLDOWN] = cooldownText;
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    spiral = renderer.gameObject.AddComponent<TimerSpiral>();
    generator.onStateChanged += OnQuicksilverEnergyGeneratorStateChanged;
  }

  public void OnDestroy() => generator.onStateChanged -= OnQuicksilverEnergyGeneratorStateChanged;

  public void Update()
  {
    Text text;
    if (!stateText.TryGetValue(generator.GetState(), out text))
      return;
    text.text = timeDirector.FormatTimeSeconds(generator.GetTimeRemaining());
  }

  private void OnQuicksilverEnergyGeneratorStateChanged()
  {
    foreach (KeyValuePair<QuicksilverEnergyGenerator.State, Text> keyValuePair in stateText)
      keyValuePair.Value.gameObject.SetActive(generator.GetState() == keyValuePair.Key);
    spiral.SetTimeSource(generator.GetState() == QuicksilverEnergyGenerator.State.ACTIVE || generator.GetState() == QuicksilverEnergyGenerator.State.COUNTDOWN ? generator : (VacDisplayTimer.TimeSource) null);
  }
}
