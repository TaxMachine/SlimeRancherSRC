// Decompiled with JetBrains decompiler
// Type: RanchLargoTypesTracker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class RanchLargoTypesTracker : MonoBehaviour
{
  private AchievementsDirector achieveDir;
  private Dictionary<Identifiable.Id, int> largoTypes = new Dictionary<Identifiable.Id, int>(Identifiable.idComparer);

  public void Awake()
  {
    foreach (CellDirector componentsInChild in GetComponentsInChildren<CellDirector>())
    {
      componentsInChild.onSlimeAdded += OnSlimeAdded;
      componentsInChild.onSlimeRemoved += OnSlimeRemoved;
    }
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
  }

  public void OnDestroy()
  {
    foreach (CellDirector componentsInChild in GetComponentsInChildren<CellDirector>())
      componentsInChild.onSlimeAdded -= OnSlimeAdded;
  }

  public void OnSlimeAdded(Identifiable.Id slimeId)
  {
    if (!Identifiable.IsLargo(slimeId))
      return;
    if (largoTypes.ContainsKey(slimeId))
      ++largoTypes[slimeId];
    else
      largoTypes[slimeId] = 1;
    achieveDir.MaybeUpdateMaxStat(AchievementsDirector.IntStat.RANCH_LARGO_TYPES, largoTypes.Count);
  }

  public void OnSlimeRemoved(Identifiable.Id slimeId)
  {
    if (!Identifiable.IsLargo(slimeId))
      return;
    if (largoTypes.ContainsKey(slimeId))
    {
      int num = largoTypes[slimeId] - 1;
      if (num > 0)
        largoTypes[slimeId] = num;
      else
        largoTypes.Remove(slimeId);
    }
    else
      Log.Warning("Tried to remove non-registered largo ID: " + slimeId);
  }
}
