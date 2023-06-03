// Decompiled with JetBrains decompiler
// Type: DLCProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class DLCProvider
{
  private HashSet<DLCPackage.Id> supported;
  private Dictionary<DLCPackage.Id, DLCPackage.State> cache;

  public DLCProvider(IEnumerable<DLCPackage.Id> supported)
  {
    this.supported = new HashSet<DLCPackage.Id>(supported, DLCPackage.IdComparer.Instance);
    cache = this.supported.ToDictionary(id => id, id => DLCPackage.State.AVAILABLE, DLCPackage.IdComparer.Instance);
  }

  protected void ResetAllPackageStates() => cache = supported.ToDictionary(id => id, id => DLCPackage.State.AVAILABLE, DLCPackage.IdComparer.Instance);

  public abstract IEnumerator Refresh();

  public abstract void ShowInStore(DLCPackage.Id id);

  public IEnumerable<DLCPackage.Id> GetSupported() => supported;

  public DLCPackage.State GetState(DLCPackage.Id id)
  {
    DLCPackage.State state;
    cache.TryGetValue(id, out state);
    return state;
  }

  protected bool SetState(DLCPackage.Id id, DLCPackage.State state)
  {
    DLCPackage.State state1 = GetState(id);
    if (state1 > state)
    {
      Log.Error("Attempting to downgrade DLC state.", nameof (id), id, "current", state1, "updated", state);
      return false;
    }
    cache[id] = state;
    return true;
  }
}
