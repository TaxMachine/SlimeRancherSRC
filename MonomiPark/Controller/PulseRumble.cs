// Decompiled with JetBrains decompiler
// Type: MonomiPark.Controller.PulseRumble
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.Controller
{
  public class PulseRumble : Rumble
  {
    private int maxPower;
    private float stopTime;

    public PulseRumble(Motor motor, int maxPower, float duration)
      : base(motor)
    {
      stopTime = Time.time + duration;
      this.maxPower = maxPower;
    }

    public override int CurrentPower() => maxPower;

    public override bool IsFinished() => Time.time >= (double) stopTime;
  }
}
