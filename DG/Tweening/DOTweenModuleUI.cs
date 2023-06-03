// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModuleUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace DG.Tweening
{
  public static class DOTweenModuleUI
  {
    public static TweenerCore<float, float, FloatOptions> DOFade(
      this CanvasGroup target,
      float endValue,
      float duration)
    {
      TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.alpha, x => target.alpha = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<Color, Color, ColorOptions> DOColor(
      this Graphic target,
      Color endValue,
      float duration)
    {
      TweenerCore<Color, Color, ColorOptions> t = DOTween.To(() => target.color, x => target.color = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<Color, Color, ColorOptions> DOFade(
      this Graphic target,
      float endValue,
      float duration)
    {
      TweenerCore<Color, Color, ColorOptions> alpha = DOTween.ToAlpha(() => target.color, x => target.color = x, endValue, duration);
      alpha.SetTarget(target);
      return alpha;
    }

    public static TweenerCore<Color, Color, ColorOptions> DOColor(
      this Image target,
      Color endValue,
      float duration)
    {
      TweenerCore<Color, Color, ColorOptions> t = DOTween.To(() => target.color, x => target.color = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<Color, Color, ColorOptions> DOFade(
      this Image target,
      float endValue,
      float duration)
    {
      TweenerCore<Color, Color, ColorOptions> alpha = DOTween.ToAlpha(() => target.color, x => target.color = x, endValue, duration);
      alpha.SetTarget(target);
      return alpha;
    }

    public static TweenerCore<float, float, FloatOptions> DOFillAmount(
      this Image target,
      float endValue,
      float duration)
    {
      if (endValue > 1.0)
        endValue = 1f;
      else if (endValue < 0.0)
        endValue = 0.0f;
      TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.fillAmount, x => target.fillAmount = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static Sequence DOGradientColor(this Image target, Gradient gradient, float duration)
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
          sequence.Append(DOColor(target, gradientColorKey.color, duration1).SetEase(Ease.Linear));
        }
      }
      return sequence;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOFlexibleSize(
      this LayoutElement target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => new Vector2(target.flexibleWidth, target.flexibleHeight), x =>
      {
        target.flexibleWidth = x.x;
        target.flexibleHeight = x.y;
      }, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOMinSize(
      this LayoutElement target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => new Vector2(target.minWidth, target.minHeight), x =>
      {
        target.minWidth = x.x;
        target.minHeight = x.y;
      }, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOPreferredSize(
      this LayoutElement target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => new Vector2(target.preferredWidth, target.preferredHeight), x =>
      {
        target.preferredWidth = x.x;
        target.preferredHeight = x.y;
      }, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Color, Color, ColorOptions> DOColor(
      this Outline target,
      Color endValue,
      float duration)
    {
      TweenerCore<Color, Color, ColorOptions> t = DOTween.To(() => target.effectColor, x => target.effectColor = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<Color, Color, ColorOptions> DOFade(
      this Outline target,
      float endValue,
      float duration)
    {
      TweenerCore<Color, Color, ColorOptions> alpha = DOTween.ToAlpha(() => target.effectColor, x => target.effectColor = x, endValue, duration);
      alpha.SetTarget(target);
      return alpha;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOScale(
      this Outline target,
      Vector2 endValue,
      float duration)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.effectDistance, x => target.effectDistance = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPos(
      this RectTransform target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPosX(
      this RectTransform target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(endValue, 0.0f), duration);
      t.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPosY(
      this RectTransform target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(0.0f, endValue), duration);
      t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3D(
      this RectTransform target,
      Vector3 endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.anchoredPosition3D, x => target.anchoredPosition3D = x, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3DX(
      this RectTransform target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.anchoredPosition3D, x => target.anchoredPosition3D = x, new Vector3(endValue, 0.0f, 0.0f), duration);
      t.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3DY(
      this RectTransform target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.anchoredPosition3D, x => target.anchoredPosition3D = x, new Vector3(0.0f, endValue, 0.0f), duration);
      t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector3, Vector3, VectorOptions> DOAnchorPos3DZ(
      this RectTransform target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.anchoredPosition3D, x => target.anchoredPosition3D = x, new Vector3(0.0f, 0.0f, endValue), duration);
      t.SetOptions(AxisConstraint.Z, snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorMax(
      this RectTransform target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchorMax, x => target.anchorMax = x, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorMin(
      this RectTransform target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchorMin, x => target.anchorMin = x, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOPivot(
      this RectTransform target,
      Vector2 endValue,
      float duration)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.pivot, x => target.pivot = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOPivotX(
      this RectTransform target,
      float endValue,
      float duration)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.pivot, x => target.pivot = x, new Vector2(endValue, 0.0f), duration);
      t.SetOptions(AxisConstraint.X).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOPivotY(
      this RectTransform target,
      float endValue,
      float duration)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.pivot, x => target.pivot = x, new Vector2(0.0f, endValue), duration);
      t.SetOptions(AxisConstraint.Y).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOSizeDelta(
      this RectTransform target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.sizeDelta, x => target.sizeDelta = x, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static Tweener DOPunchAnchorPos(
      this RectTransform target,
      Vector2 punch,
      float duration,
      int vibrato = 10,
      float elasticity = 1f,
      bool snapping = false)
    {
      return DOTween.Punch(() => target.anchoredPosition, x => target.anchoredPosition = x, punch, duration, vibrato, elasticity).SetTarget(target).SetOptions(snapping);
    }

    public static Tweener DOShakeAnchorPos(
      this RectTransform target,
      float duration,
      float strength = 100f,
      int vibrato = 10,
      float randomness = 90f,
      bool snapping = false,
      bool fadeOut = true)
    {
      return DOTween.Shake(() => target.anchoredPosition, x => target.anchoredPosition = x, duration, strength, vibrato, randomness, fadeOut: fadeOut).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake).SetOptions(snapping);
    }

    public static Tweener DOShakeAnchorPos(
      this RectTransform target,
      float duration,
      Vector2 strength,
      int vibrato = 10,
      float randomness = 90f,
      bool snapping = false,
      bool fadeOut = true)
    {
      return DOTween.Shake(() => target.anchoredPosition, x => target.anchoredPosition = x, duration, strength, vibrato, randomness, fadeOut).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake).SetOptions(snapping);
    }

    public static Sequence DOJumpAnchorPos(
      this RectTransform target,
      Vector2 endValue,
      float jumpPower,
      int numJumps,
      float duration,
      bool snapping = false)
    {
      if (numJumps < 1)
        numJumps = 1;
      float startPosY = 0.0f;
      float offsetY = -1f;
      bool offsetYSet = false;
      Sequence s = DOTween.Sequence();
      Tween t = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(0.0f, jumpPower), duration / (numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative().SetLoops(numJumps * 2, LoopType.Yoyo).OnStart(() => startPosY = target.anchoredPosition.y);
      s.Append(DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(endValue.x, 0.0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(t).SetTarget(target).SetEase(DOTween.defaultEaseType);
      s.OnUpdate(() =>
      {
        if (!offsetYSet)
        {
          offsetYSet = true;
          offsetY = s.isRelative ? endValue.y : endValue.y - startPosY;
        }
        Vector2 anchoredPosition = target.anchoredPosition;
        anchoredPosition.y += DOVirtual.EasedValue(0.0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
        target.anchoredPosition = anchoredPosition;
      });
      return s;
    }

    public static Tweener DONormalizedPos(
      this ScrollRect target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To(() => new Vector2(target.horizontalNormalizedPosition, target.verticalNormalizedPosition), x =>
      {
        target.horizontalNormalizedPosition = x.x;
        target.verticalNormalizedPosition = x.y;
      }, endValue, duration).SetOptions(snapping).SetTarget(target);
    }

    public static Tweener DOHorizontalNormalizedPos(
      this ScrollRect target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To(() => target.horizontalNormalizedPosition, x => target.horizontalNormalizedPosition = x, endValue, duration).SetOptions(snapping).SetTarget(target);
    }

    public static Tweener DOVerticalNormalizedPos(
      this ScrollRect target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To(() => target.verticalNormalizedPosition, x => target.verticalNormalizedPosition = x, endValue, duration).SetOptions(snapping).SetTarget(target);
    }

    public static TweenerCore<float, float, FloatOptions> DOValue(
      this Slider target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.value, x => target.value = x, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Color, Color, ColorOptions> DOColor(
      this Text target,
      Color endValue,
      float duration)
    {
      TweenerCore<Color, Color, ColorOptions> t = DOTween.To(() => target.color, x => target.color = x, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static TweenerCore<Color, Color, ColorOptions> DOFade(
      this Text target,
      float endValue,
      float duration)
    {
      TweenerCore<Color, Color, ColorOptions> alpha = DOTween.ToAlpha(() => target.color, x => target.color = x, endValue, duration);
      alpha.SetTarget(target);
      return alpha;
    }

    public static TweenerCore<string, string, StringOptions> DOText(
      this Text target,
      string endValue,
      float duration,
      bool richTextEnabled = true,
      ScrambleMode scrambleMode = ScrambleMode.None,
      string scrambleChars = null)
    {
      TweenerCore<string, string, StringOptions> t = DOTween.To(() => target.text, x => target.text = x, endValue, duration);
      t.SetOptions(richTextEnabled, scrambleMode, scrambleChars).SetTarget(target);
      return t;
    }

    public static Tweener DOBlendableColor(this Graphic target, Color endValue, float duration)
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

    public static Tweener DOBlendableColor(this Image target, Color endValue, float duration)
    {
      endValue -= target.color;
      Color to = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      return DOTween.To(() => to, x =>
      {
        Color color = x - to;
        to = x;
        Image image = target;
        image.color = image.color + color;
      }, endValue, duration).Blendable().SetTarget(target);
    }

    public static Tweener DOBlendableColor(this Text target, Color endValue, float duration)
    {
      endValue -= target.color;
      Color to = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      return DOTween.To(() => to, x =>
      {
        Color color = x - to;
        to = x;
        Text text = target;
        text.color = text.color + color;
      }, endValue, duration).Blendable().SetTarget(target);
    }

    public static class Utils
    {
      public static Vector2 SwitchToRectTransform(RectTransform from, RectTransform to)
      {
        Vector2 vector21 = default;
        ref Vector2 local1 = ref vector21;
        var rect = from.rect;
        Rect rect1 = rect;
        double num1 = rect1.width * 0.5;
        rect1 = rect;
        double xMin1 = rect1.xMin;
        double x1 = num1 + xMin1;
        rect1 = rect;
        double num2 = rect1.height * 0.5;
        rect1 = rect;
        double yMin1 = rect1.yMin;
        double y1 = num2 + yMin1;
        local1 = new Vector2((float) x1, (float) y1);
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, from.position) + vector21;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(to, screenPoint, null, out localPoint);
        Vector2 vector22 = default;
        ref Vector2 local2 = ref vector22;
        var rect3 = to.rect;
        Rect rect2 = rect3;
        double num3 = rect2.width * 0.5;
        rect2 = rect3;
        double xMin2 = rect2.xMin;
        double x2 = num3 + xMin2;
        rect2 = rect3;
        double num4 = rect2.height * 0.5;
        rect2 = rect3;
        double yMin2 = rect2.yMin;
        double y2 = num4 + yMin2;
        local2 = new Vector2((float) x2, (float) y2);
        return to.anchoredPosition + localPoint - vector22;
      }
    }
  }
}
