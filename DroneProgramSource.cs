// Decompiled with JetBrains decompiler
// Type: DroneProgramSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DroneProgramSource : DroneProgram
{
  public static HashSet<GameObject> BLACKLIST = new HashSet<GameObject>();
  public Predicate<Identifiable.Id> predicate = id => false;
  public IEnumerable<DroneProgramDestination> destinations = Enumerable.Empty<DroneProgramDestination>();

  public abstract IEnumerable<DroneFastForwarder.GatherGroup> GetFastForwardGroups(double endTime);

  protected override DroneAnimator.Id animation => DroneAnimator.Id.GATHER;

  protected override DroneAnimatorState.Id animationStateBegin => DroneAnimatorState.Id.GATHER_BEGIN;

  protected override DroneAnimatorState.Id animationStateEnd => DroneAnimatorState.Id.GATHER_END;

  protected int GetAvailableDestinationSpace(Identifiable.Id id) => destinations.Aggregate(0, (c, d) => c + d.GetAvailableSpace(id));

  protected bool HasAvailableDestinationSpace(Identifiable.Id id) => destinations.Any(d => d.HasAvailableSpace(id));

  protected bool HasAvailableDestinationSpace(Identifiable.Id id, int minimum)
  {
    foreach (DroneProgramDestination destination in destinations)
    {
      int availableSpace = destination.GetAvailableSpace(id);
      if (availableSpace >= minimum)
        return true;
      minimum -= availableSpace;
    }
    return false;
  }
}
