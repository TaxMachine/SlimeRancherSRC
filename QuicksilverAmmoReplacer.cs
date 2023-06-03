// Decompiled with JetBrains decompiler
// Type: QuicksilverAmmoReplacer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class QuicksilverAmmoReplacer : MonoBehaviour
{
  [Tooltip("Energy generator required to be active to replace ammo.")]
  public QuicksilverEnergyGenerator generator;
  [Tooltip("Weighted list of ammo to replace with.")]
  public List<WeightedAmmo> ammo;
  [Tooltip("Time in game hours between ability to trigger this.")]
  public float cooldownHours = 1f;
  [Tooltip("FX to display when the ammo replacer is available. (optional)")]
  public GameObject activeFX;
  private WeightedAmmo picked;
  private TimeDirector timeDir;
  private TutorialDirector tutDir;
  private PlayerState playerState;
  private double unavailUntil;
  private bool? wasReady;
  private Component[] padRenderer;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    tutDir = SRSingleton<SceneContext>.Instance.TutorialDirector;
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    padRenderer = transform.parent.gameObject.GetComponentsInChildren<Renderer>();
    picked = PickNextAmmo();
  }

  public void OnTriggerEnter(Collider col)
  {
    if (!IsReady() || !PhysicsUtil.IsPlayerMainCollider(col) || !playerState.Ammo.ReplaceWithQuicksilverAmmo(picked.id, picked.count))
      return;
    if (picked.id == Identifiable.Id.VALLEY_AMMO_1)
      tutDir.MaybeShowPopup(TutorialDirector.Id.RACE_PULSESHOT);
    else
      tutDir.MaybeShowPopup(TutorialDirector.Id.RACE_POWERUP);
    SECTR_AudioSystem.Play(picked.onPickupCue, transform.position, false);
    unavailUntil = timeDir.HoursFromNow(cooldownHours);
    picked = PickNextAmmo();
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
    float num = flag1 ? 0.5f : 0.0f;
    foreach (Renderer renderer in padRenderer)
      renderer.material.SetFloat("_SpiralColor", num);
    this.wasReady = new bool?(flag1);
  }

  private WeightedAmmo PickNextAmmo() => Randoms.SHARED.Pick(ammo, a => a.weight, null);

  private bool IsReady() => generator.GetState() == QuicksilverEnergyGenerator.State.ACTIVE && timeDir.HasReached(unavailUntil);

  [Serializable]
  public class WeightedAmmo
  {
    [Tooltip("Identifiable of the ammo to replace with.")]
    public Identifiable.Id id;
    [Tooltip("Weight used when picking a weighted random ammo.")]
    public float weight;
    [Tooltip("Amount of ammo that should be added/replaced with on pickup.")]
    public int count;
    [Tooltip("SFX played when this ammo is picked up by the player.")]
    public SECTR_AudioCue onPickupCue;
  }
}
