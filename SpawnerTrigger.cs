// Decompiled with JetBrains decompiler
// Type: SpawnerTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class SpawnerTrigger : MonoBehaviour, SpawnerTriggerModel.Participant
{
  public DirectedActorSpawner spawner;
  [Tooltip("Minimum number of items/slimes to spawn at a time.")]
  public int minSpawn = 3;
  [Tooltip("Maximum number of items/slimes to spawn at a time.")]
  public int maxSpawn = 5;
  [Tooltip("Average cooldown between triggers.")]
  public float avgGameHoursBetweenTrigger = 2f;
  [Tooltip("Chance the trigger will spawn slimes. Even if it doesn't, cooldown will reset.")]
  public float chanceOfTrigger = 1f;
  private TimeDirector timeDir;
  private SpawnerTriggerModel model;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterSpawnerTrigger(this);
  }

  public void InitModel(SpawnerTriggerModel model) => model.pos = transform.position;

  public void SetModel(SpawnerTriggerModel model) => this.model = model;

  public void OnTriggerEnter(Collider collider)
  {
    if (collider.isTrigger || !timeDir.HasReached(model.nextTriggerTime))
      return;
    Identifiable componentInParent = collider.gameObject.GetComponentInParent<Identifiable>();
    if (!(componentInParent != null) || componentInParent.id != Identifiable.Id.PLAYER || !spawner.CanSpawnSomething())
      return;
    if (Randoms.SHARED.GetProbability(chanceOfTrigger))
    {
      float num = spawner is DirectedSlimeSpawner ? SRSingleton<SceneContext>.Instance.ModDirector.SlimeCountFactor() : 1f;
      StartCoroutine(spawner.Spawn(Mathf.RoundToInt(Randoms.SHARED.GetInRange(minSpawn, maxSpawn + 1) * num), Randoms.SHARED));
    }
    model.nextTriggerTime = timeDir.HoursFromNow(Randoms.SHARED.GetInRange(0.5f, 1.5f) * avgGameHoursBetweenTrigger);
  }
}
