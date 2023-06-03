// Decompiled with JetBrains decompiler
// Type: DroneProgramSourceSiloStorage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DroneProgramSourceSiloStorage : 
  DroneProgramSource<DroneNetwork.StorageMetadata>
{
  private int remaining;
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

  public override IEnumerable<DroneFastForwarder.GatherGroup> GetFastForwardGroups(double endTime) => GetSources(predicate).Select(s => new DroneFastForwarder.GatherGroup.Storage(s)).Cast<DroneFastForwarder.GatherGroup>();

  protected override bool CanCancel() => source.CanCancel();

  protected override IEnumerable<Orientation> GetTargetOrientations(
    DroneNetwork.StorageMetadata source)
  {
    /*// ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    DroneProgramSourceSiloStorage sourceSiloStorage = this;
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
    sourceSiloStorage.orientation = sourceSiloStorage.GetTargetOrientation_Catcher(source.catcher.gameObject);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = sourceSiloStorage.orientation.orientation;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;*/
    yield return null;
  }

  protected override Vector3 GetTargetPosition(DroneNetwork.StorageMetadata source) => source.catcher.transform.position;

  protected override GameObject GetTargetGameObject(DroneNetwork.StorageMetadata source) => source.catcher.gameObject;

  protected override void OnFirstAction()
  {
    base.OnFirstAction();
    remaining = Mathf.Min(new int[3]
    {
      drone.ammo.GetSlotMaxCount() - drone.ammo.GetSlotCount(),
      source.count,
      GetAvailableDestinationSpace(source.id)
    });
  }

  protected override bool OnAction()
  {
    if (!timeDirector.HasReached(time))
      return false;
    if (remaining <= 0 || !drone.ammo.MaybeAddToSlot(source.id))
      return true;
    time = timeDirector.HoursFromNow(0.00166666682f);
    source.ammo.Decrement(source.id);
    return --remaining <= 0;
  }

  protected override void OnPathGenerationFailed()
  {
    base.OnPathGenerationFailed();
    if (orientation == null)
      return;
    orientation.Dispose();
    orientation = null;
  }
}
