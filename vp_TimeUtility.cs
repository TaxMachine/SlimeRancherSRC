// Decompiled with JetBrains decompiler
// Type: vp_TimeUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public static class vp_TimeUtility
{
  private static float m_MinTimeScale = 0.1f;
  private static float m_MaxTimeScale = 1f;
  private static bool m_Paused = false;
  private static float m_TimeScaleOnPause = 1f;
  public static float InitialFixedTimeStep = Time.fixedDeltaTime;

  public static float TimeScale
  {
    get => Time.timeScale;
    set
    {
      value = ClampTimeScale(value);
      Time.timeScale = value;
      Time.fixedDeltaTime = InitialFixedTimeStep * Time.timeScale;
    }
  }

  public static float AdjustedTimeScale => (float) (1.0 / (Time.timeScale * (0.019999999552965164 / Time.fixedDeltaTime)));

  public static void FadeTimeScale(float targetTimeScale, float fadeSpeed)
  {
    if (TimeScale == (double) targetTimeScale)
      return;
    targetTimeScale = ClampTimeScale(targetTimeScale);
    TimeScale = Mathf.Lerp(TimeScale, targetTimeScale, Time.deltaTime * 60f * fadeSpeed);
    if (Mathf.Abs(TimeScale - targetTimeScale) >= 0.0099999997764825821)
      return;
    TimeScale = targetTimeScale;
  }

  private static float ClampTimeScale(float t)
  {
    if (t < (double) m_MinTimeScale || t > (double) m_MaxTimeScale)
    {
      t = Mathf.Clamp(t, m_MinTimeScale, m_MaxTimeScale);
      Debug.LogWarning("Warning: (vp_TimeUtility) TimeScale was clamped to within the supported range (" + m_MinTimeScale + " - " + m_MaxTimeScale + ").");
    }
    return t;
  }

  public static bool Paused
  {
    get => m_Paused;
    set
    {
      if (value)
      {
        if (m_Paused)
          return;
        m_Paused = true;
        m_TimeScaleOnPause = Time.timeScale;
        Time.timeScale = 0.0f;
      }
      else
      {
        if (!m_Paused)
          return;
        m_Paused = false;
        Time.timeScale = m_TimeScaleOnPause;
        m_TimeScaleOnPause = 1f;
      }
    }
  }
}
