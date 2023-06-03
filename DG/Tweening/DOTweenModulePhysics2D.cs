// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModulePhysics2D
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
  public static class DOTweenModulePhysics2D
  {
    public static TweenerCore<Vector2, Vector2, VectorOptions> DOMove(
      this Rigidbody2D target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOMoveX(
      this Rigidbody2D target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, new Vector2(endValue, 0.0f), duration);
      t.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector2, Vector2, VectorOptions> DOMoveY(
      this Rigidbody2D target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, new Vector2(0.0f, endValue), duration);
      t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<float, float, FloatOptions> DORotate(
      this Rigidbody2D target,
      float endValue,
      float duration)
    {
      TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.rotation, target.MoveRotation, endValue, duration);
      t.SetTarget(target);
      return t;
    }

    public static Sequence DOJump(
      this Rigidbody2D target,
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
      Tween yTween = DOTween.To(() => target.position, x => target.position = x, new Vector2(0.0f, jumpPower), duration / (numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative().SetLoops(numJumps * 2, LoopType.Yoyo).OnStart(() => startPosY = target.position.y);
      s.Append(DOTween.To(() => target.position, x => target.position = x, new Vector2(endValue.x, 0.0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(yTween).SetTarget(target).SetEase(DOTween.defaultEaseType);
      yTween.OnUpdate(() =>
      {
        if (!offsetYSet)
        {
          offsetYSet = true;
          offsetY = s.isRelative ? endValue.y : endValue.y - startPosY;
        }
        Vector3 position = target.position;
        position.y += DOVirtual.EasedValue(0.0f, offsetY, yTween.ElapsedPercentage(), Ease.OutQuad);
        target.MovePosition(position);
      });
      return s;
    }
  }
}
