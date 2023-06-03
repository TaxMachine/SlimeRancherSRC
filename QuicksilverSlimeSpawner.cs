// Decompiled with JetBrains decompiler
// Type: QuicksilverSlimeSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class QuicksilverSlimeSpawner : DirectedSlimeSpawner
{
  [Tooltip("Energy generator to control spawning.")]
  public QuicksilverEnergyGenerator generator;
  [Tooltip("Duration, in game minutes, that the slimes will flee before being destroyed. Prevents stuck slimes from never despawning.")]
  public float maxFleeMinutes = 30f;
  private CellDirector cellDirector;
  private List<GameObject> spawned = new List<GameObject>();

  public override void Awake()
  {
    base.Awake();
    generator.onStateChanged += OnGeneratorStateChanged;
  }

  public void OnDestroy() => generator.onStateChanged -= OnGeneratorStateChanged;

  protected override void Register(CellDirector cellDirector)
  {
    base.Register(cellDirector);
    this.cellDirector = cellDirector;
  }

  protected override void OnActorSpawned(GameObject slime)
  {
    base.OnActorSpawned(slime);
    spawned.Add(slime);
  }

  private void OnGeneratorStateChanged()
  {
    if (generator.GetState() == QuicksilverEnergyGenerator.State.ACTIVE)
    {
      if (!(cellDirector != null))
        return;
      cellDirector.ForceCheckSpawn();
    }
    else
    {
      for (int index = 0; index < spawned.Count; ++index)
      {
        GameObject instance = spawned[index];
        if (instance != null)
        {
          if (SRSingleton<SceneContext>.Instance.Player != null)
          {
            instance.GetComponent<SlimeFlee>().StartFleeing(SRSingleton<SceneContext>.Instance.Player);
            DestroyAfterTime destroyAfterTime = instance.AddComponent<DestroyAfterTime>();
            destroyAfterTime.SetDeathTime(SRSingleton<SceneContext>.Instance.TimeDirector.HoursFromNow(maxFleeMinutes * 0.0166666675f));
            destroyAfterTime.ScaleDownOnDestroy();
          }
          else
            Destroyer.Destroy(instance, "QuicksilverSlimeSpawner.OnGeneratorStateChanged");
        }
      }
      spawned.Clear();
    }
  }

  public override bool CanSpawn(float? forHour = null) => base.CanSpawn(forHour) && generator.GetState() == QuicksilverEnergyGenerator.State.ACTIVE;

  protected override bool CanContinueSpawning() => base.CanContinueSpawning() && generator.GetState() == QuicksilverEnergyGenerator.State.ACTIVE;
}
