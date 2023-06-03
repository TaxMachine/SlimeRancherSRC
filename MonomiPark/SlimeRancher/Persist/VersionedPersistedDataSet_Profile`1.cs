// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.VersionedPersistedDataSet_Profile`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace MonomiPark.SlimeRancher.Persist
{
  public abstract class VersionedPersistedDataSet_Profile<T> : VersionedPersistedDataSet<T> where T : PersistedDataSet, new()
  {
    private List<uint> upgrades;

    public override string Identifier => "SRPF";

    public void RunUpgradeActions(AutoSaveDirector director)
    {
      if (upgrades == null)
        return;
      if (!director.SaveProfile())
        throw new Exception("Failed to persist profile before running upgrade actions.");
      foreach (VersionedPersistedDataSet_Profile.UpgradeAction upgradeAction in upgrades.Where(v => VersionedPersistedDataSet_Profile.UpgradeActions.ContainsKey(v)).Select(v => VersionedPersistedDataSet_Profile.UpgradeActions[v]))
        upgradeAction(director);
    }

    protected override void UpgradeFrom(T previous)
    {
      upgrades = upgrades ?? new List<uint>();
      upgrades.Add(previous.Version);
    }
  }
}
