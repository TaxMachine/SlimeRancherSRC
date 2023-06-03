// Decompiled with JetBrains decompiler
// Type: FilteredTrackCollisions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class FilteredTrackCollisions : TrackCollisions
{
  private Predicate<GameObject> filter;

  public void SetFilter(Predicate<GameObject> filter) => this.filter = filter;

  protected override void OnTriggerEnter(Collider other)
  {
    if (filter != null && !filter(other.gameObject))
      return;
    base.OnTriggerEnter(other);
  }

  protected override void OnTriggerExit(Collider other)
  {
    if (filter != null && !filter(other.gameObject))
      return;
    base.OnTriggerExit(other);
  }
}
