// Decompiled with JetBrains decompiler
// Type: QuicksilverEnergyCheckpoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class QuicksilverEnergyCheckpoint : MonoBehaviour
{
  [Tooltip("Energy generator to extend.")]
  public QuicksilverEnergyGenerator generator;
  [Tooltip("Time, in game hours, this checkpoint will extend the energy timer.")]
  public float extensionHours;
  [Tooltip("Time, in game hours, this checkpoint is on cooldown after use.")]
  public float cooldownHours;
  [Tooltip("FX to display when the checkpoint is available. (optional)")]
  public GameObject activeFX;
  [Tooltip("SFX played when the checkpoint is triggered by the player.")]
  public SECTR_AudioCue onPickupCue;
  [Tooltip("FX played when the checkpoint is triggered by the player. (optional)")]
  public GameObject onPickupFX;
  private Renderer padRenderer;
  public GameObject padObject;
  private double cooldown;
  private bool? wasReady;
  private TimeDirector timeDirector;

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    generator.onStateChanged += OnGeneratorStateChanged;
    if (!(padObject != null))
      return;
    padRenderer = padObject.GetComponent<Renderer>();
  }

  public void OnDestroy() => generator.onStateChanged -= OnGeneratorStateChanged;

  public void OnTriggerEnter(Collider collider)
  {
    if (!PhysicsUtil.IsPlayerMainCollider(collider) || !timeDirector.HasReached(cooldown) || !generator.ExtendActiveDuration(extensionHours))
      return;
    SECTR_AudioSystem.Play(onPickupCue, transform.position, false);
    cooldown = timeDirector.HoursFromNow(cooldownHours);
    onPickupFX.SetActive(true);
  }

  public void Update()
  {
    bool flag1 = IsReady();
    if (this.wasReady.HasValue)
    {
      bool? wasReady = this.wasReady;
      bool flag2 = flag1;
      if (wasReady.GetValueOrDefault() == flag2 & wasReady.HasValue)
        return;
    }
    if (activeFX != null)
      activeFX.SetActive(flag1);
    if (padRenderer != null)
      padRenderer.material.SetFloat("_SpiralColor", flag1 ? 0.5f : 0.0f);
    this.wasReady = new bool?(flag1);
  }

  private void OnGeneratorStateChanged()
  {
    if (generator.GetState() != QuicksilverEnergyGenerator.State.ACTIVE)
      return;
    onPickupFX.SetActive(false);
    cooldown = 0.0;
  }

  private bool IsReady() => generator.GetState() == QuicksilverEnergyGenerator.State.ACTIVE && timeDirector.HasReached(cooldown);
}
