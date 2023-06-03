// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModuleUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace DG.Tweening
{
  public static class DOTweenModuleUtils
  {
    private static bool _initialized;

    [Preserve]
    public static void Init()
    {
      if (_initialized)
        return;
      _initialized = true;
      DOTweenExternalCommand.SetOrientationOnPath += Physics.SetOrientationOnPath;
    }

    [Preserve]
    private static void Preserver()
    {
      AppDomain.CurrentDomain.GetAssemblies();
      typeof (MonoBehaviour).GetMethod("Stub");
    }

    public static class Physics
    {
      public static void SetOrientationOnPath(
        PathOptions options,
        Tween t,
        Quaternion newRot,
        Transform trans)
      {
        if (options.isRigidbody)
          ((Rigidbody) t.target).rotation = newRot;
        else
          trans.rotation = newRot;
      }

      public static bool HasRigidbody2D(Component target) => target.GetComponent<Rigidbody2D>() != null;

      [Preserve]
      public static bool HasRigidbody(Component target) => target.GetComponent<Rigidbody>() != null;

      [Preserve]
      public static TweenerCore<Vector3, Path, PathOptions> CreateDOTweenPathTween(
        MonoBehaviour target,
        bool tweenRigidbody,
        bool isLocal,
        Path path,
        float duration,
        PathMode pathMode)
      {
        Rigidbody component = tweenRigidbody ? target.GetComponent<Rigidbody>() : null;
        return !tweenRigidbody || !(component != null) ? (isLocal ? target.transform.DOLocalPath(path, duration, pathMode) : target.transform.DOPath(path, duration, pathMode)) : (isLocal ? component.DOLocalPath(path, duration, pathMode) : component.DOPath(path, duration, pathMode));
      }
    }
  }
}
