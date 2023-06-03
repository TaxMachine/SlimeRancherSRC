// Decompiled with JetBrains decompiler
// Type: RadSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class RadSource : MonoBehaviour
{
  [Tooltip("Radiation to apply to the player per second while in this rad source. Stacks with other rad sources.")]
  public float radPerSecond = 1f;
  private PlayerRadAbsorber absorber;
  private double startTime;
  private TimeDirector timeDir;

  public void Awake() => timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;

  public void OnDisable()
  {
    if (!(absorber != null))
      return;
    ClearAbsorber();
  }

  public void OnTriggerEnter(Collider collider)
  {
    if (collider.isTrigger)
      return;
    PlayerRadAbsorber component = collider.gameObject.GetComponent<PlayerRadAbsorber>();
    if (!(component != null))
      return;
    absorber = component;
    startTime = timeDir.HoursFromNowOrStart(0.0f);
  }

  public void OnTriggerExit(Collider collider)
  {
    if (collider.isTrigger)
      return;
    PlayerRadAbsorber component = collider.gameObject.GetComponent<PlayerRadAbsorber>();
    if (!(component != null) || !(absorber == component))
      return;
    ClearAbsorber();
  }

  private void ClearAbsorber()
  {
    absorber = null;
    int val = (int) Math.Floor((timeDir.WorldTime() - startTime) * 0.01666666753590107);
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.AchievementsDirector.MaybeUpdateMaxStat(AchievementsDirector.IntStat.EXTENDED_RAD_EXPOSURE, val);
  }

  public void FixedUpdate()
  {
    if (!(absorber != null))
      return;
    absorber.Absorb(gameObject, radPerSecond * Time.fixedDeltaTime);
  }
}
