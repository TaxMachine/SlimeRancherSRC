// Decompiled with JetBrains decompiler
// Type: DroneProgramDestinationSiloStorage`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DroneProgramDestinationSiloStorage<T> : DroneProgramDestination<T> where T : DroneNetwork.StorageMetadata
{
  private double time;
  private CatcherOrientation orientation;

  public override void Deselected()
  {
    base.Deselected();
    if (orientation == null)
      return;
    orientation.Dispose();
    orientation = null;
  }

  public override int GetAvailableSpace(Identifiable.Id id) => GetDestinations(id, false).Cast<DroneNetwork.StorageMetadata>().Aggregate(0, (accum, d) => accum + d.GetAvailableSpace(id));

  public override bool HasAvailableSpace(Identifiable.Id id) => GetDestinations(id, false).Any();

  protected override bool CanCancel() => base.CanCancel() || destination.CanCancel();

  protected override IEnumerable<Orientation> GetTargetOrientations()
  {
    // ISSUE: reference to a compiler-generated field
    /*int num = this.\u003C\u003E1__state;
    DroneProgramDestinationSiloStorage<T> destinationSiloStorage = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    destinationSiloStorage.orientation = destinationSiloStorage.GetTargetOrientation_Catcher(destinationSiloStorage.destination.catcher.gameObject);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = destinationSiloStorage.orientation.orientation;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;*/
    return null;
  }

  protected override Vector3 GetTargetPosition() => destination.catcher.transform.position;

  protected override bool OnAction_Deposit(bool overflow)
  {
    if (!timeDirector.HasReached(time))
      return false;
    Identifiable.Id slotName = drone.ammo.GetSlotName();
    if (!destination.Increment(slotName, overflow, 1))
      return true;
    drone.ammo.Decrement(slotName);
    time = timeDirector.HoursFromNow(0.00166666682f);
    return !overflow && destination.IsFull();
  }

  public override FastForward_Response FastForward(
    Identifiable.Id id,
    bool overflow,
    double endTime,
    int maxFastForward)
  {
    DroneNetwork.StorageMetadata storageMetadata = Prioritize(GetDestinations(id, overflow)).First();
    maxFastForward = overflow ? maxFastForward : Mathf.Min(maxFastForward, storageMetadata.GetAvailableSpace(id));
    storageMetadata.Increment(id, overflow, maxFastForward);
    return new FastForward_Response()
    {
      deposits = maxFastForward
    };
  }
}
