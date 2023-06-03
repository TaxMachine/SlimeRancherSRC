// Decompiled with JetBrains decompiler
// Type: VacAccelerationHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class VacAccelerationHelper
{
  public readonly float minFactor;
  public readonly float maxFactor;
  public readonly float speed;
  public readonly float duration;
  private float timeBegin;
  private float timeEnd;

  public static VacAccelerationHelper CreateInput() => new VacAccelerationHelper(1f, 3f, 0.5f, 1f);

  public static VacAccelerationHelper CreateOutput() => new VacAccelerationHelper(1f, 1.75f, 0.5f, 1f);

  public VacAccelerationHelper(float minFactor, float maxFactor, float speed, float duration)
  {
    this.minFactor = minFactor;
    this.maxFactor = maxFactor;
    this.speed = speed;
    this.duration = duration;
  }

  public float Factor => Time.time < (double) timeEnd ? Math.Min(minFactor + (Time.time - timeBegin) * speed, maxFactor) : minFactor;

  public void OnTriggered()
  {
    timeBegin = Time.time >= (double) timeEnd ? Time.time : timeBegin;
    timeEnd = Time.time + duration;
  }
}
