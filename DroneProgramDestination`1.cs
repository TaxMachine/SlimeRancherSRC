// Decompiled with JetBrains decompiler
// Type: DroneProgramDestination`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

public abstract class DroneProgramDestination<T> : DroneProgramDestination where T : class
{
  protected int MAX_AVAIL_TO_REPORT = 1000000;
  protected T destination;
  private bool overflow;

  public override sealed bool Relevancy(bool overflow)
  {
    if (drone.ammo.IsEmpty())
      return false;
    Identifiable.Id slotName = drone.ammo.GetSlotName();
    if (!predicate(slotName))
      return false;
    destination = Prioritize(GetDestinations(slotName, overflow)).FirstOrDefault();
    return !IsNull(destination);
  }

  public override void Selected()
  {
    base.Selected();
    overflow = false;
  }

  protected override sealed bool OnAction()
  {
    if (!OnAction_Deposit(overflow))
      return false;
    if (drone.ammo.IsEmpty())
      return true;
    if (overflow)
    {
      Log.Error("Failed to complete overflow deposit.", "destination", destination);
      return true;
    }
    if (GetDestinations(drone.ammo.GetSlotName(), false).Any(d => d != destination))
      return true;
    overflow = true;
    return false;
  }

  protected override bool CanCancel() => drone.ammo.IsEmpty() || IsNull(destination);

  private static bool IsNull(T destination) => destination == null || destination.Equals(null);

  protected abstract bool OnAction_Deposit(bool overflow);

  protected abstract IEnumerable<T> GetDestinations(Identifiable.Id id, bool overflow);

  protected abstract IEnumerable<T> Prioritize(IEnumerable<T> destinations);
}
