// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModuleUnityVersion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
  public static class DOTweenModuleUnityVersion
  {
    public static Sequence DOGradientColor(this Material target, Gradient gradient, float duration)
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

    public static Sequence DOGradientColor(
      this Material target,
      Gradient gradient,
      string property,
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
          target.SetColor(property, gradientColorKey.color);
        }
        else
        {
          float duration1 = index == length - 1 ? duration - sequence.Duration(false) : duration * (index == 0 ? gradientColorKey.time : gradientColorKey.time - colorKeys[index - 1].time);
          sequence.Append(target.DOColor(gradientColorKey.color, property, duration1).SetEase(Ease.Linear));
        }
      }
      return sequence;
    }

    public static CustomYieldInstruction WaitForCompletion(
      this Tween t,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return new DOTweenCYInstruction.WaitForCompletion(t);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return null;
    }

    public static CustomYieldInstruction WaitForRewind(
      this Tween t,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return new DOTweenCYInstruction.WaitForRewind(t);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return null;
    }

    public static CustomYieldInstruction WaitForKill(
      this Tween t,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return new DOTweenCYInstruction.WaitForKill(t);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return null;
    }

    public static CustomYieldInstruction WaitForElapsedLoops(
      this Tween t,
      int elapsedLoops,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return new DOTweenCYInstruction.WaitForElapsedLoops(t, elapsedLoops);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return null;
    }

    public static CustomYieldInstruction WaitForPosition(
      this Tween t,
      float position,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return new DOTweenCYInstruction.WaitForPosition(t, position);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return null;
    }

    public static CustomYieldInstruction WaitForStart(
      this Tween t,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return new DOTweenCYInstruction.WaitForStart(t);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return null;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffset(
      this Material target,
      Vector2 endValue,
      int propertyID,
      float duration)
    {
      if (!target.HasProperty(propertyID))
      {
        if (Debugger.logPriority > 0)
          Debugger.LogMissingMaterialProperty(propertyID);
        return null;
      }
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.GetTextureOffset(propertyID), x => target.SetTextureOffset(propertyID, x), endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOTiling(
      this Material target,
      Vector2 endValue,
      int propertyID,
      float duration)
    {
      if (!target.HasProperty(propertyID))
      {
        if (Debugger.logPriority > 0)
          Debugger.LogMissingMaterialProperty(propertyID);
        return null;
      }
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.GetTextureScale(propertyID), x => target.SetTextureScale(propertyID, x), endValue, duration);
      t.SetTarget(target);
      return t;
    }
  }
}
