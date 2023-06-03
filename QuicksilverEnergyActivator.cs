// Decompiled with JetBrains decompiler
// Type: QuicksilverEnergyActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class QuicksilverEnergyActivator : MonoBehaviour, TechActivator
{
  [Tooltip("Energy generator to activate.")]
  public QuicksilverEnergyGenerator generator;
  public SECTR_AudioCue pressButtonCue;
  public GameObject pressButtonFX;
  [Tooltip("SFX played when the button is pressed and the generator cannot be activated. (optional)")]
  public SECTR_AudioCue onPressButtonFailureCue;
  private Component[] buttonRenderer;
  private Animator buttonAnimator;
  private int buttonPressedTriggerId;
  public Animator generatorAnimator;

  public void Awake()
  {
    buttonAnimator = GetComponentInParent<Animator>();
    buttonPressedTriggerId = Animator.StringToHash("ButtonPressed");
    buttonRenderer = transform.parent.gameObject.GetComponentsInChildren<Renderer>();
    generator.onStateChanged += OnGeneratorStateChanged;
  }

  public void OnDestroy() => generator.onStateChanged -= OnGeneratorStateChanged;

  private void OnGeneratorStateChanged()
  {
    if (pressButtonFX != null)
      pressButtonFX.SetActive(generator.GetState() == QuicksilverEnergyGenerator.State.ACTIVE || generator.GetState() == QuicksilverEnergyGenerator.State.COUNTDOWN);
    if (generatorAnimator != null)
      generatorAnimator.SetBool("generatorState", generator.GetState() != QuicksilverEnergyGenerator.State.ACTIVE && generator.GetState() != QuicksilverEnergyGenerator.State.COUNTDOWN);
    float num = IsReady() ? 0.5f : 0.0f;
    foreach (Renderer renderer in buttonRenderer)
      renderer.material.SetFloat("_SpiralColor", num);
  }

  public void Activate()
  {
    if (buttonAnimator != null)
      buttonAnimator.SetTrigger(buttonPressedTriggerId);
    if (IsReady())
    {
      SECTR_AudioSystem.Play(pressButtonCue, transform.position, false);
      generator.Activate();
    }
    else
      SECTR_AudioSystem.Play(onPressButtonFailureCue, transform.position, false);
  }

  public GameObject GetCustomGuiPrefab()
  {
    if (IsReady())
      return null;
    GameObject instance = new GameObject("EmptyGameObject");
    Destroyer.Destroy(instance, "QuicksilverEnergyActivator.GetCustomGuiPrefab");
    return instance;
  }

  private bool IsReady() => generator.GetState() == QuicksilverEnergyGenerator.State.INACTIVE;
}
