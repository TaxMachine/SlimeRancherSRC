// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModuleAudio
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Audio;

namespace DG.Tweening
{
  public static class DOTweenModuleAudio
  {
    public static TweenerCore<float, float, FloatOptions> DOFade(
      this AudioSource target,
      float endValue,
      float duration)
    {
      if (endValue < 0.0)
        endValue = 0.0f;
      else if (endValue > 1.0)
        endValue = 1f;
      TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.volume, x => target.volume = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<float, float, FloatOptions> DOPitch(
      this AudioSource target,
      float endValue,
      float duration)
    {
      TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.pitch, x => target.pitch = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<float, float, FloatOptions> DOSetFloat(
      this AudioMixer target,
      string floatName,
      float endValue,
      float duration)
    {
      TweenerCore<float, float, FloatOptions> t = DOTween.To(() =>
      {
        float num;
        target.GetFloat(floatName, out num);
        return num;
      }, x => target.SetFloat(floatName, x), endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static int DOComplete(this AudioMixer target, bool withCallbacks = false) => DOTween.Complete(target, withCallbacks);

    public static int DOKill(this AudioMixer target, bool complete = false) => DOTween.Kill(target, complete);

    public static int DOFlip(this AudioMixer target) => DOTween.Flip(target);

    public static int DOGoto(this AudioMixer target, float to, bool andPlay = false) => DOTween.Goto(target, to, andPlay);

    public static int DOPause(this AudioMixer target) => DOTween.Pause(target);

    public static int DOPlay(this AudioMixer target) => DOTween.Play(target);

    public static int DOPlayBackwards(this AudioMixer target) => DOTween.PlayBackwards(target);

    public static int DOPlayForward(this AudioMixer target) => DOTween.PlayForward(target);

    public static int DORestart(this AudioMixer target) => DOTween.Restart(target);

    public static int DORewind(this AudioMixer target) => DOTween.Rewind(target);

    public static int DOSmoothRewind(this AudioMixer target) => DOTween.SmoothRewind(target);

    public static int DOTogglePause(this AudioMixer target) => DOTween.TogglePause(target);
  }
}
