// Decompiled with JetBrains decompiler
// Type: DroneProgramSource`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DroneProgramSource<T> : DroneProgramSource where T : class
{
  protected T source;
  private GameObject sourceGameObject;

  public override bool Relevancy()
  {
    if (drone.ammo.IsFull() || !drone.station.battery.HasAny() || drone.ammo.Any() && !HasAvailableDestinationSpace(drone.ammo.GetSlotName(), drone.ammo.GetSlotCount() + 1))
      return false;
    foreach (T source in GetSources(id =>
             {
               if (!predicate(id) || !drone.ammo.CouldAddToSlot(id))
                 return false;
               return drone.ammo.Any() || HasAvailableDestinationSpace(id);
             }))
    {
      sourceGameObject = GetTargetGameObject(source);
      this.source = source;
      if (BLACKLIST.Add(sourceGameObject) && GeneratePath(GetSubnetwork(), GetTargetOrientations(), GetTargetPosition()))
        return true;
    }
    sourceGameObject = null;
    this.source = default (T);
    return false;
  }

  public override void Selected() => base.Selected();

  public override void Deselected()
  {
    base.Deselected();
    BLACKLIST.Remove(sourceGameObject);
    sourceGameObject = null;
  }

  protected override void OnPathGenerationFailed()
  {
    base.OnPathGenerationFailed();
    if (!(sourceGameObject != null))
      return;
    sourceGameObject.AddComponent<DroneProgramSource_PathGenerationFailure>();
    sourceGameObject = null;
  }

  protected override sealed IEnumerable<Orientation> GetTargetOrientations() => GetTargetOrientations(source);

  protected override sealed Vector3 GetTargetPosition() => GetTargetPosition(source);

  protected abstract IEnumerable<T> GetSources(Predicate<Identifiable.Id> predicate);

  protected abstract IEnumerable<Orientation> GetTargetOrientations(T source);

  protected abstract Vector3 GetTargetPosition(T source);

  protected abstract GameObject GetTargetGameObject(T source);
}
