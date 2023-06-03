// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenCYInstruction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace DG.Tweening
{
  public static class DOTweenCYInstruction
  {
    public class WaitForCompletion : CustomYieldInstruction
    {
      private readonly Tween t;

      public override bool keepWaiting => t.active && !t.IsComplete();

      public WaitForCompletion(Tween tween) => t = tween;
    }

    public class WaitForRewind : CustomYieldInstruction
    {
      private readonly Tween t;

      public override bool keepWaiting
      {
        get
        {
          if (!t.active)
            return false;
          return !t.playedOnce || t.position * (double) (t.CompletedLoops() + 1) > 0.0;
        }
      }

      public WaitForRewind(Tween tween) => t = tween;
    }

    public class WaitForKill : CustomYieldInstruction
    {
      private readonly Tween t;

      public override bool keepWaiting => t.active;

      public WaitForKill(Tween tween) => t = tween;
    }

    public class WaitForElapsedLoops : CustomYieldInstruction
    {
      private readonly Tween t;
      private readonly int elapsedLoops;

      public override bool keepWaiting => t.active && t.CompletedLoops() < elapsedLoops;

      public WaitForElapsedLoops(Tween tween, int elapsedLoops)
      {
        t = tween;
        this.elapsedLoops = elapsedLoops;
      }
    }

    public class WaitForPosition : CustomYieldInstruction
    {
      private readonly Tween t;
      private readonly float position;

      public override bool keepWaiting => t.active && t.position * (double) (t.CompletedLoops() + 1) < position;

      public WaitForPosition(Tween tween, float position)
      {
        t = tween;
        this.position = position;
      }
    }

    public class WaitForStart : CustomYieldInstruction
    {
      private readonly Tween t;

      public override bool keepWaiting => t.active && !t.playedOnce;

      public WaitForStart(Tween tween) => t = tween;
    }
  }
}
