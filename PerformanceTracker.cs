// Decompiled with JetBrains decompiler
// Type: PerformanceTracker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class PerformanceTracker : MonoBehaviour
{
  private float frameCount;
  private float dt;
  private float fps;
  private float updateRate = 1f;
  private double fpsSum;
  private int fpsCount;
  private long maxHeapSize;
  private const int BYTES_PER_MEG = 1048576;

  public void Update()
  {
    ++frameCount;
    dt += Time.deltaTime;
    if (dt > (double) updateRate)
    {
      fps = frameCount / dt;
      frameCount = 0.0f;
      dt -= updateRate;
      fpsSum += fps;
      ++fpsCount;
    }
    maxHeapSize = Math.Max(maxHeapSize, Profiler.usedHeapSizeLong);
  }

  public void OnApplicationQuit()
  {
    if (fpsCount <= 0)
      return;
    AnalyticsUtil.CustomEvent("PerfSummary", new Dictionary<string, object>()
    {
      {
        "meanFps",
        (int) Math.Round(fpsSum / fpsCount)
      },
      {
        "maxMem",
        maxHeapSize / 1048576L
      }
    }, false);
  }
}
