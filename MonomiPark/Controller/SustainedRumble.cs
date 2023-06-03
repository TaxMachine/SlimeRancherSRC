// Decompiled with JetBrains decompiler
// Type: MonomiPark.Controller.SustainedRumble
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace MonomiPark.Controller
{
  public class SustainedRumble : Rumble
  {
    private int power;
    private bool isFinished;

    public SustainedRumble(Motor motor, int power)
      : base(motor)
    {
      this.power = power;
    }

    public override int CurrentPower() => power;

    public void UpdatePower(int power) => this.power = power;

    public void FinishRumble() => isFinished = true;

    public override bool IsFinished() => isFinished;
  }
}
