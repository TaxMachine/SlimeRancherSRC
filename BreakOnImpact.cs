// Decompiled with JetBrains decompiler
// Type: BreakOnImpact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnImpact : BreakOnImpactBase
{
  public int minSpawns = 2;
  public int maxSpawns = 4;
  public List<SpawnOption> spawnOptions = new List<SpawnOption>();
  private Dictionary<SpawnOption, float> spawnWeights = new Dictionary<SpawnOption, float>();

  public override void Awake()
  {
    base.Awake();
    foreach (SpawnOption spawnOption in spawnOptions)
      spawnWeights[spawnOption] = spawnOption.weight;
  }

  protected override IEnumerable<GameObject> GetRewardPrefabs()
  {
    int numSpawns = Randoms.SHARED.GetInRange(minSpawns, maxSpawns);
    for (int ii = 0; ii < numSpawns; ++ii)
    {
      SpawnOption spawnOption = Randoms.SHARED.Pick(spawnWeights, null);
      if (spawnOption != null)
        yield return spawnOption.spawn;
    }
    foreach (Identifiable.Id id in SRSingleton<SceneContext>.Instance.HolidayDirector.GetCurrOrnament())
      yield return SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(id);
  }

  [Serializable]
  public class SpawnOption
  {
    public GameObject spawn;
    public float weight;
  }
}
