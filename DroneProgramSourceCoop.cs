// Decompiled with JetBrains decompiler
// Type: DroneProgramSourceCoop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneProgramSourceCoop : DroneProgramSourceLandPlot
{
  private const int SKIP_ROOSTER = 2;
  private const int SKIP_HEN_RARE = 1;
  private const int SKIP_HEN = 4;

  protected override LandPlot.Id GetLandPlotID() => LandPlot.Id.COOP;

  protected override IEnumerable<Identifiable> GetSources(
    Predicate<Identifiable.Id> predicate,
    DroneNetwork.LandPlotMetadata metadata)
  {
    TrackContainedIdentifiables container = metadata.trackers.First();
    return container.GetAllTracked().Where(pair => predicate(pair.Key)).SelectMany(pair => pair.Value.Skip(GetSkipCount(pair.Key, container))).Where(s => SourcePredicate(metadata, s));
  }

  protected override int GetMaxPickup(Identifiable.Id id)
  {
    int b = int.MaxValue;
    if (currentLandPlot != null && Identifiable.MEAT_CLASS.Contains(id))
    {
      TrackContainedIdentifiables container = currentLandPlot.trackers.First();
      b = container.Count(id) - GetSkipCount(id, container);
    }
    return Mathf.Min(base.GetMaxPickup(id), b);
  }

  private static int GetSkipCount(Identifiable.Id id, TrackContainedIdentifiables container)
  {
    switch (id)
    {
      case Identifiable.Id.HEN:
        return Mathf.Max(0, 4 - container.Count(Identifiable.Id.PAINTED_HEN) - container.Count(Identifiable.Id.BRIAR_HEN) - container.Count(Identifiable.Id.STONY_HEN));
      case Identifiable.Id.ROOSTER:
        return 2;
      case Identifiable.Id.STONY_HEN:
        return Math.Max(1, 4 - container.Count(Identifiable.Id.PAINTED_HEN) - container.Count(Identifiable.Id.BRIAR_HEN));
      case Identifiable.Id.BRIAR_HEN:
        return Math.Max(1, 4 - container.Count(Identifiable.Id.PAINTED_HEN) - Mathf.Min(1, container.Count(Identifiable.Id.STONY_HEN)));
      case Identifiable.Id.PAINTED_HEN:
        return Math.Max(1, 4 - Mathf.Min(1, container.Count(Identifiable.Id.BRIAR_HEN)) - Mathf.Min(1, container.Count(Identifiable.Id.STONY_HEN)));
      default:
        return 0;
    }
  }
}
