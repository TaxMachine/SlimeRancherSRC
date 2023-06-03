// Decompiled with JetBrains decompiler
// Type: QuicksilverElectricField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class QuicksilverElectricField : SRBehaviour
{
  [Tooltip("Duration, in game minutes, that the electric field is active.")]
  public float activeMinutes;
  [Tooltip("SFX played when a slime is electrified by the field and produces a plort.")]
  public SECTR_AudioCue onElectrifiedCue;
  private double deathTime;

  public void Awake() => ResetDeathTime();

  public void Update()
  {
    if (!SRSingleton<SceneContext>.Instance.TimeDirector.HasReached(deathTime))
      return;
    Destroyer.Destroy(gameObject, "QuicksilverElectricField.Update");
  }

  public void OnTriggerEnter(Collider collider)
  {
    ReactToShock component = collider.GetComponent<ReactToShock>();
    if (!(component != null) || !component.MaybeCreatePlorts(1, ReactToShock.PlortSounds.SUCCESS))
      return;
    SECTR_AudioSystem.Play(onElectrifiedCue, collider.transform.position, false);
  }

  public void ResetDeathTime() => deathTime = SRSingleton<SceneContext>.Instance.TimeDirector.HoursFromNow(activeMinutes * 0.0166666675f);
}
