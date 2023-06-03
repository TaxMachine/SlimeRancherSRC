// Decompiled with JetBrains decompiler
// Type: QuicksilverEnergyReplacer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class QuicksilverEnergyReplacer : MonoBehaviour
{
  [Tooltip("Energy generator required to be active to replace ammo.")]
  public QuicksilverEnergyGenerator generator;
  [Tooltip("Time in game hours between ability to trigger this.")]
  public float cooldownHours = 1f;
  [Tooltip("FX to display when the ammo replacer is available. (optional)")]
  public GameObject activeFX;
  [Tooltip("SFX played when the player picks up the energy.")]
  public SECTR_AudioCue onPickupCue;
  private TimeDirector timeDirector;
  private TutorialDirector tutDirector;
  private PlayerState playerState;
  private double activeTime;

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    tutDirector = SRSingleton<SceneContext>.Instance.TutorialDirector;
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
  }

  public void Update()
  {
    if (!(activeFX != null))
      return;
    activeFX.SetActive(IsReady());
  }

  public void OnTriggerEnter(Collider col)
  {
    if (!IsReady() || !PhysicsUtil.IsPlayerMainCollider(col))
      return;
    tutDirector.MaybeShowPopup(TutorialDirector.Id.RACE_ENERGYBOOST);
    if (playerState.GetCurrEnergy() >= playerState.GetMaxEnergy())
      return;
    SECTR_AudioSystem.Play(onPickupCue, transform.position, false);
    activeTime = timeDirector.HoursFromNow(cooldownHours);
    playerState.SetEnergy(playerState.GetMaxEnergy());
  }

  private bool IsReady() => generator.GetState() == QuicksilverEnergyGenerator.State.ACTIVE && timeDirector.HasReached(activeTime);
}
