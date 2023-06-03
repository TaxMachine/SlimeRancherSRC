// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModulePhysics
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
  public static class DOTweenModulePhysics
  {
    public static TweenerCore<Vector3, Vector3, VectorOptions> DOMove(
      this Rigidbody target,
      Vector3 endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, endValue, duration);
      t.SetOptions(snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveX(
      this Rigidbody target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, new Vector3(endValue, 0.0f, 0.0f), duration);
      t.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveY(
      this Rigidbody target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, new Vector3(0.0f, endValue, 0.0f), duration);
      t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveZ(
      this Rigidbody target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      TweenerCore<Vector3, Vector3, VectorOptions> t = DOTween.To(() => target.position, target.MovePosition, new Vector3(0.0f, 0.0f, endValue), duration);
      t.SetOptions(AxisConstraint.Z, snapping).SetTarget(target);
      return t;
    }

    public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotate(
      this Rigidbody target,
      Vector3 endValue,
      float duration,
      RotateMode mode = RotateMode.Fast)
    {
      TweenerCore<Quaternion, Vector3, QuaternionOptions> t = DOTween.To(() => target.rotation, target.MoveRotation, endValue, duration);
      t.SetTarget(target);
      t.plugOptions.rotateMode = mode;
      return t;
    }

    public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOLookAt(
      this Rigidbody target,
      Vector3 towards,
      float duration,
      AxisConstraint axisConstraint = AxisConstraint.None,
      Vector3? up = null)
    {
      TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = DOTween.To(() => target.rotation, target.MoveRotation, towards, duration).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetLookAt);
      tweenerCore.plugOptions.axisConstraint = axisConstraint;
      tweenerCore.plugOptions.up = !up.HasValue ? Vector3.up : up.Value;
      return tweenerCore;
    }

    public static Sequence DOJump(
      this Rigidbody target,
      Vector3 endValue,
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
      Tween yTween = DOTween.To(() => target.position, target.MovePosition, new Vector3(0.0f, jumpPower, 0.0f), duration / (numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative().SetLoops(numJumps * 2, LoopType.Yoyo).OnStart(() => startPosY = target.position.y);
      s.Append(DOTween.To(() => target.position, target.MovePosition, new Vector3(endValue.x, 0.0f, 0.0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(DOTween.To(() => target.position, target.MovePosition, new Vector3(0.0f, 0.0f, endValue.z), duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear)).Join(yTween).SetTarget(target).SetEase(DOTween.defaultEaseType);
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

    public static TweenerCore<Vector3, Path, PathOptions> DOPath(
      this Rigidbody target,
      Vector3[] path,
      float duration,
      PathType pathType = PathType.Linear,
      PathMode pathMode = PathMode.Full3D,
      int resolution = 10,
      Color? gizmoColor = null)
    {
      if (resolution < 1)
        resolution = 1;
      TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To(PathPlugin.Get(), () => target.position, target.MovePosition, new Path(pathType, path, resolution, gizmoColor), duration).SetTarget(target).SetUpdate(UpdateType.Fixed);
      tweenerCore.plugOptions.isRigidbody = true;
      tweenerCore.plugOptions.mode = pathMode;
      return tweenerCore;
    }

    public static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(
      this Rigidbody target,
      Vector3[] path,
      float duration,
      PathType pathType = PathType.Linear,
      PathMode pathMode = PathMode.Full3D,
      int resolution = 10,
      Color? gizmoColor = null)
    {
      if (resolution < 1)
        resolution = 1;
      Transform trans = target.transform;
      TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To(PathPlugin.Get(), () => trans.localPosition, x => target.MovePosition(trans.parent == null ? x : trans.parent.TransformPoint(x)), new Path(pathType, path, resolution, gizmoColor), duration).SetTarget(target).SetUpdate(UpdateType.Fixed);
      tweenerCore.plugOptions.isRigidbody = true;
      tweenerCore.plugOptions.mode = pathMode;
      tweenerCore.plugOptions.useLocalPosition = true;
      return tweenerCore;
    }

    internal static TweenerCore<Vector3, Path, PathOptions> DOPath(
      this Rigidbody target,
      Path path,
      float duration,
      PathMode pathMode = PathMode.Full3D)
    {
      TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To(PathPlugin.Get(), () => target.position, target.MovePosition, path, duration).SetTarget(target);
      tweenerCore.plugOptions.isRigidbody = true;
      tweenerCore.plugOptions.mode = pathMode;
      return tweenerCore;
    }

    internal static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(
      this Rigidbody target,
      Path path,
      float duration,
      PathMode pathMode = PathMode.Full3D)
    {
      Transform trans = target.transform;
      TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To(PathPlugin.Get(), () => trans.localPosition, x => target.MovePosition(trans.parent == null ? x : trans.parent.TransformPoint(x)), path, duration).SetTarget(target);
      tweenerCore.plugOptions.isRigidbody = true;
      tweenerCore.plugOptions.mode = pathMode;
      tweenerCore.plugOptions.useLocalPosition = true;
      return tweenerCore;
    }
  }
}
