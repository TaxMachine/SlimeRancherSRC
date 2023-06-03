// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModuleSprite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
  public static class DOTweenModuleSprite
  {
    public static TweenerCore<Color, Color, ColorOptions> DOColor(
      this SpriteRenderer target,
      Color endValue,
      float duration)
    {
      TweenerCore<Color, Color, ColorOptions> t = DOTween.To(() => target.color, x => target.color = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<Color, Color, ColorOptions> DOFade(
      this SpriteRenderer target,
      float endValue,
      float duration)
    {
      TweenerCore<Color, Color, ColorOptions> alpha = DOTween.ToAlpha(() => target.color, x => target.color = x, endValue, duration);
      alpha.SetTarget(target);
      return alpha;
    }

    public static Sequence DOGradientColor(
      this SpriteRenderer target,
      Gradient gradient,
      float duration)
    {
      Sequence sequence = DOTween.Sequence();
      GradientColorKey[] colorKeys = gradient.colorKeys;
      int length = colorKeys.Length;
      for (int index = 0; index < length; ++index)
      {
        GradientColorKey gradientColorKey = colorKeys[index];
        if (index == 0 && gradientColorKey.time <= 0.0)
        {
          target.color = gradientColorKey.color;
        }
        else
        {
          float duration1 = index == length - 1 ? duration - sequence.Duration(false) : duration * (index == 0 ? gradientColorKey.time : gradientColorKey.time - colorKeys[index - 1].time);
          sequence.Append(target.DOColor(gradientColorKey.color, duration1).SetEase(Ease.Linear));
        }
      }
      return sequence;
    }

    public static Tweener DOBlendableColor(
      this SpriteRenderer target,
      Color endValue,
      float duration)
    {
      endValue -= target.color;
      Color to = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      return DOTween.To(() => to, x =>
      {
        Color color = x - to;
        to = x;
        target.color += color;
      }, endValue, duration).Blendable().SetTarget(target);
    }
  }
}
