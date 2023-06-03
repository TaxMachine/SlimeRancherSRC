// Decompiled with JetBrains decompiler
// Type: DroneProgramDestination
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

public abstract class DroneProgramDestination : DroneProgram
{
  public Predicate<Identifiable.Id> predicate = id => false;

  public abstract int GetAvailableSpace(Identifiable.Id id);

  public abstract FastForward_Response FastForward(
    Identifiable.Id id,
    bool overflow,
    double endTime,
    int maxFastForward);

  public virtual bool HasAvailableSpace(Identifiable.Id id) => GetAvailableSpace(id) > 0;

  public override sealed bool Relevancy() => throw new InvalidOperationException();

  public abstract bool Relevancy(bool overflow);

  protected override DroneAnimator.Id animation => DroneAnimator.Id.DEPOSIT;

  protected override DroneAnimatorState.Id animationStateBegin => DroneAnimatorState.Id.DEPOSIT_BEGIN;

  protected override DroneAnimatorState.Id animationStateEnd => DroneAnimatorState.Id.DEPOSIT_END;

  public class FastForward_Response
  {
    public int deposits;
    public int currency;
  }
}
