// Decompiled with JetBrains decompiler
// Type: QuicksilverSlowField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class QuicksilverSlowField : SRBehaviour
{
  [Tooltip("Duration, in game minutes, that the slow field is active.")]
  public float activeMinutes;
  [Tooltip("Duration, in game minutes, that the slow is applied to the slime.")]
  public float slowMinutes;
  [Tooltip("SFX played when a slime is slowed by this field.")]
  public SECTR_AudioCue onSlowAppliedCue;
  private double deathTime;

  public void Awake() => deathTime = SRSingleton<SceneContext>.Instance.TimeDirector.HoursFromNow(activeMinutes * 0.0166666675f);

  public void Update()
  {
    if (!SRSingleton<SceneContext>.Instance.TimeDirector.HasReached(deathTime))
      return;
    Destroyer.Destroy(gameObject, "QuicksilverSlowField.Update");
  }

  public void OnTriggerEnter(Collider collider)
  {
    FollowWaypoints component = collider.GetComponent<FollowWaypoints>();
    if (!(component != null))
      return;
    SECTR_AudioSystem.Play(onSlowAppliedCue, collider.transform.position, false);
    component.ApplySlow(slowMinutes * 0.0166666675f);
  }
}
