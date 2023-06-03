// Decompiled with JetBrains decompiler
// Type: SlimeStealth
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class SlimeStealth : RegisteredActorBehaviour, RegistryUpdateable, SpawnListener
{
  private const float STEALTH_INIT_TIME = 5f;
  private const float OPACITY_CHANGE_PER_SEC = 2f;
  private const float STEALTH_OPACITY = 0.0f;
  private const float OPACITY_CHANGE_TOLERANCE = 0.001f;
  private SlimeAppearanceApplicator slimeAppearanceApplicator;
  private Vacuumable vacuumable;
  private SlimeAudio slimeAudio;
  private MaterialStealthController stealthController;
  private float initStealthUntil;
  private float currentOpacity = 1f;
  private float targetOpacity = 1f;
  private float lastOpacity = 1f;

  public bool IsStealthed => currentOpacity < 1.0;

  public void Awake()
  {
    stealthController = new MaterialStealthController(gameObject);
    slimeAppearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    vacuumable = GetComponent<Vacuumable>();
    slimeAudio = GetComponent<SlimeAudio>();
    if (slimeAppearanceApplicator.Appearance != null)
      UpdateMaterialStealthController();
    if (!(slimeAppearanceApplicator != null))
      return;
    slimeAppearanceApplicator.OnAppearanceChanged += appearance => UpdateMaterialStealthController();
  }

  public void RegistryUpdate() => UpdateStealthOpacity();

  public void DidSpawn()
  {
    currentOpacity = 0.0f;
    initStealthUntil = Time.time + 5f;
  }

  public void SetStealth(bool stealth)
  {
    targetOpacity = stealth ? 0.0f : 1f;
    slimeAudio.Play(stealth ? slimeAudio.slimeSounds.cloakCue : slimeAudio.slimeSounds.decloakCue);
  }

  private void SetOpacity(float opacity)
  {
    stealthController.SetOpacity(opacity);
    lastOpacity = opacity;
  }

  private void UpdateMaterialStealthController()
  {
    stealthController.UpdateMaterials(gameObject);
    lastOpacity = 1f;
  }

  private void UpdateStealthOpacity()
  {
    float a = Time.time < (double) initStealthUntil ? 0.0f : targetOpacity;
    if (a > (double) currentOpacity)
      currentOpacity = Mathf.Min(a, currentOpacity + 2f * Time.deltaTime);
    else if (targetOpacity < (double) currentOpacity)
      currentOpacity = Mathf.Max(a, currentOpacity - 2f * Time.deltaTime);
    float opacity = vacuumable.isHeld() ? 1f : currentOpacity;
    if (Math.Abs(opacity - lastOpacity) <= 1.0 / 1000.0)
      return;
    SetOpacity(opacity);
  }
}
