// Decompiled with JetBrains decompiler
// Type: CompanyLogoScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class CompanyLogoScene : MonoBehaviour
{
  public CanvasGroup logo;
  public CanvasGroup splashBackground;
  public float logoFadeInTime;
  public float logoHoldTime;
  public float logoFadeOutTime;
  public float splashFadeInWaitTime;
  public float splashFadeInTime;
  private float timeAcc;
  private LogoSceneState currentState;

  public void Update()
  {
    if (currentState == LogoSceneState.None)
      currentState = LogoSceneState.FirstFrameProcessed;
    else if (currentState == LogoSceneState.FirstFrameProcessed)
    {
      currentState = LogoSceneState.StartedLogoFadeIn;
      timeAcc = 0.0f;
    }
    else if (currentState == LogoSceneState.StartedLogoFadeIn)
    {
      timeAcc += Time.deltaTime / logoFadeInTime;
      if (timeAcc > 1.0)
      {
        currentState = LogoSceneState.LogoWaitTime;
        logo.alpha = 1f;
        timeAcc = 0.0f;
      }
      else
        logo.alpha = Mathf.Lerp(0.0f, 1f, timeAcc);
    }
    else if (currentState == LogoSceneState.LogoWaitTime)
    {
      timeAcc += Time.deltaTime / logoHoldTime;
      if (timeAcc <= 1.0)
        return;
      currentState = LogoSceneState.StartedLogoFadeOut;
      timeAcc = 0.0f;
    }
    else if (currentState == LogoSceneState.StartedLogoFadeOut)
    {
      timeAcc += Time.deltaTime / logoFadeOutTime;
      if (timeAcc > 1.0)
      {
        currentState = LogoSceneState.SplashPreFadeInWait;
        logo.alpha = 0.0f;
        timeAcc = 0.0f;
      }
      else
        logo.alpha = Mathf.Lerp(1f, 0.0f, timeAcc);
    }
    else if (currentState == LogoSceneState.SplashPreFadeInWait)
    {
      timeAcc += Time.deltaTime / splashFadeInWaitTime;
      if (timeAcc <= 1.0)
        return;
      currentState = LogoSceneState.StartedSplashFadeIn;
      timeAcc = 0.0f;
    }
    else if (currentState == LogoSceneState.StartedSplashFadeIn)
    {
      timeAcc += Time.deltaTime / splashFadeInTime;
      if (timeAcc > 1.0)
      {
        currentState = LogoSceneState.ReadyToStartLoad;
        splashBackground.alpha = 1f;
        timeAcc = 0.0f;
      }
      else
        splashBackground.alpha = Mathf.Lerp(0.0f, 1f, timeAcc);
    }
    else
    {
      if (currentState != LogoSceneState.ReadyToStartLoad)
        return;
      currentState = LogoSceneState.Loading;
      SceneManager.LoadScene("StandaloneStart", LoadSceneMode.Single);
    }
  }

  private enum LogoSceneState
  {
    None,
    FirstFrameProcessed,
    StartedLogoFadeIn,
    LogoWaitTime,
    StartedLogoFadeOut,
    SplashPreFadeInWait,
    StartedSplashFadeIn,
    ReadyToStartLoad,
    Loading,
  }
}
