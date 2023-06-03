// Decompiled with JetBrains decompiler
// Type: DestroyOutsideHoursOfDay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class DestroyOutsideHoursOfDay : SRBehaviour, CaveTrigger.Listener
{
  public float startHour = 18f;
  public float endHour = 6f;
  public float minEndureHoursOutsideWindow = 0.5f;
  public float maxEndureHoursOutsideWindow = 1.5f;
  public bool cavesPreventShutdown = true;
  public GameObject destroyFX;
  private TimeDirector timeDir;
  private double shutdownAt;
  private HashSet<GameObject> caves = new HashSet<GameObject>();
  private bool waitForPhysicsUpdate;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    StartShutdownClock();
  }

  public void OnEnable() => waitForPhysicsUpdate = true;

  public void FixedUpdate() => waitForPhysicsUpdate = false;

  public void Update()
  {
    if (waitForPhysicsUpdate)
      return;
    if (timeDir.HasReached(shutdownAt))
    {
      if (destroyFX != null)
        SpawnAndPlayFX(destroyFX, transform.position, transform.rotation);
      Destroyer.DestroyActor(gameObject, "DestroyOutsideHoursOfDay.Update");
    }
    if (caves.Count <= 0)
      return;
    UnityWorkarounds.SafeRemoveAllNulls(caves);
    if (caves.Count != 0)
      return;
    StartShutdownClock();
  }

  public void OnCaveEnter(GameObject caveObj, bool affectLighting, AmbianceDirector.Zone caveZone)
  {
    if (caves.Count == 0)
      StopShutdownClock();
    caves.Add(caveObj);
  }

  public void OnCaveExit(GameObject caveObj, bool affectLighting, AmbianceDirector.Zone caveZone)
  {
    caves.Remove(caveObj);
    if (caves.Count != 0)
      return;
    StartShutdownClock();
  }

  private void StartShutdownClock()
  {
    float num1 = timeDir.CurrHourOrStart();
    float inRange = Randoms.SHARED.GetInRange(minEndureHoursOutsideWindow, maxEndureHoursOutsideWindow);
    float num2 = num1 + inRange;
    if (num2 > 24.0)
      num2 %= 24f;
    if (endHour >= (double) startHour)
    {
      if ((num1 < (double) startHour || num1 > (double) endHour) && (num2 < (double) startHour || num2 > (double) endHour))
        shutdownAt = timeDir.HoursFromNowOrStart(inRange);
      else
        shutdownAt = timeDir.GetNextHour(endHour) + inRange * 3600.0;
    }
    else if (num1 > (double) endHour && num1 < (double) startHour && num2 > (double) endHour && num2 < (double) startHour)
      shutdownAt = timeDir.HoursFromNowOrStart(inRange);
    else
      shutdownAt = timeDir.GetNextHour(endHour) + inRange * 3600.0;
  }

  private void StopShutdownClock() => shutdownAt = double.PositiveInfinity;
}
