// Decompiled with JetBrains decompiler
// Type: AppearanceSelections
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AppearanceSelections
{
  private readonly Dictionary<Identifiable.Id, HashSet<SlimeAppearance>> unlocks = new Dictionary<Identifiable.Id, HashSet<SlimeAppearance>>();
  private readonly Dictionary<Identifiable.Id, SlimeAppearance> selections = new Dictionary<Identifiable.Id, SlimeAppearance>();
  private readonly HashSet<SlimeAppearance> allSelectedAppearances = new HashSet<SlimeAppearance>(SlimeAppearanceEqualityComparer.Default);

  public event OnAppearanceUnlockedForSlimeDelegate onAppearanceUnlocked = (_param1, _param2) => { };

  public event OnAppearanceSelectedForSlimeDelegate onAppearanceSelected = (_param1, _param2) => { };

  public event OnAppearanceLockedForSlimeDelegate onAppearanceLocked = (_param1, _param2) => { };

  public void UnlockAppearanceForSlime(SlimeDefinition slime, SlimeAppearance appearance)
  {
    if (!slime.Appearances.Contains(appearance))
    {
      Log.Error(string.Format("Trying to unlock appearance {0} not attached to a slime definition {1}.", appearance.name, slime.Name));
    }
    else
    {
      GetOrCreateUnlockSetForSlime(slime).Add(appearance);
      onAppearanceUnlocked(slime, appearance);
    }
  }

  public void LockAppearanceForSlime(SlimeDefinition slime, SlimeAppearance appearance)
  {
    HashSet<SlimeAppearance> slimeAppearanceSet;
    if (unlocks.TryGetValue(slime.IdentifiableId, out slimeAppearanceSet))
      slimeAppearanceSet.Remove(appearance);
    onAppearanceLocked(slime, appearance);
  }

  public void SelectAppearanceForSlime(SlimeDefinition slime, SlimeAppearance appearance)
  {
    SlimeAppearance selectedAppearance = GetSelectedAppearance(slime);
    if (selectedAppearance != null)
      allSelectedAppearances.Remove(selectedAppearance);
    allSelectedAppearances.Add(appearance);
    selections[slime.IdentifiableId] = appearance;
    onAppearanceSelected(slime, appearance);
  }

  public SlimeAppearance GetSelectedAppearance(SlimeDefinition slime)
  {
    SlimeAppearance slimeAppearance;
    return !selections.TryGetValue(slime.IdentifiableId, out slimeAppearance) ? null : slimeAppearance;
  }

  public List<SlimeAppearance> GetUnlockedAppearances(SlimeDefinition slime) => GetOrCreateUnlockSetForSlime(slime).ToList();

  public HashSet<SlimeAppearance> GetAllSelectedAppearances() => allSelectedAppearances;

  private HashSet<SlimeAppearance> GetOrCreateUnlockSetForSlime(SlimeDefinition slime)
  {
    HashSet<SlimeAppearance> unlockSetForSlime;
    if (!unlocks.TryGetValue(slime.IdentifiableId, out unlockSetForSlime))
    {
      unlockSetForSlime = new HashSet<SlimeAppearance>();
      unlocks[slime.IdentifiableId] = unlockSetForSlime;
    }
    return unlockSetForSlime;
  }

  public delegate void OnAppearanceUnlockedForSlimeDelegate(
    SlimeDefinition slime,
    SlimeAppearance appearance);

  public delegate void OnAppearanceSelectedForSlimeDelegate(
    SlimeDefinition slime,
    SlimeAppearance appearance);

  public delegate void OnAppearanceLockedForSlimeDelegate(
    SlimeDefinition slime,
    SlimeAppearance appearance);
}
