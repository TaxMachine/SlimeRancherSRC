// Decompiled with JetBrains decompiler
// Type: GlitchCellDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GlitchCellDirector : CellDirector
{
  [Header("Tarr Properties")]
  [Tooltip("Target number of tarr slimes to be in the cell.")]
  public int targetTarrCount;
  [Tooltip("Number of tarr slime to spawn per spawn. (random range)")]
  public Vector2 tarrSpawnCount;
  [Tooltip("Tarr activation major group.")]
  public GlitchTarrNode.Group tarrActivationGroup;
  private GlitchMetadata metadata;
  private List<GlitchTarrNodeSpawner> tarrSpawners;
  private double tarrNextSpawn;

  public override void Awake()
  {
    base.Awake();
    metadata = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    tarrSpawners = new List<GlitchTarrNodeSpawner>();
  }

  protected override void UpdateToTime(double worldTime)
  {
    base.UpdateToTime(worldTime);
    if (!TimeUtil.HasReached(worldTime, tarrNextSpawn))
      return;
    if (tarrSpawners.Count > 0 && NeedsMoreTarrs())
    {
      GlitchTarrNodeSpawner glitchTarrNodeSpawner = rand.Pick(tarrSpawners, it => !it.CanSpawn(new float?()) ? 0.0f : it.directedSpawnWeight, null);
      if (glitchTarrNodeSpawner != null)
        StartCoroutine(glitchTarrNodeSpawner.Spawn(Mathf.RoundToInt(tarrSpawnCount.GetRandom(rand)), rand));
    }
    tarrNextSpawn = worldTime + metadata.tarrSpawnerThrottleTime * 60.0;
  }

  public override void ForceCheckSpawn()
  {
    base.ForceCheckSpawn();
    tarrNextSpawn = 0.0;
  }

  public void Register(GlitchTarrNodeSpawner spawner) => tarrSpawners.Add(spawner);

  protected override bool CanSpawnSlimes() => true;

  private bool NeedsMoreTarrs() => tarrSlimeCount < targetTarrCount;
}
