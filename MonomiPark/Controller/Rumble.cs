// Decompiled with JetBrains decompiler
// Type: MonomiPark.Controller.Rumble
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace MonomiPark.Controller
{
  public abstract class Rumble
  {
    public static IEqualityComparer<Motor> motorComparer = new MotorComparer();
    private Motor motor;

    public Rumble(Motor motor) => this.motor = motor;

    public Motor GetMotor() => motor;

    public abstract int CurrentPower();

    public abstract bool IsFinished();

    public enum Motor
    {
      LARGE,
      SMALL,
      LEFT,
      RIGHT,
    }

    public class MotorComparer : IEqualityComparer<Motor>
    {
      public bool Equals(Motor motor1, Motor motor2) => motor1 == motor2;

      public int GetHashCode(Motor motor) => (int) motor;
    }
  }
}
