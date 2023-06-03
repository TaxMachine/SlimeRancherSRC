// Decompiled with JetBrains decompiler
// Type: DirectedAnimalSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.Collections;
using UnityEngine;

public class DirectedAnimalSpawner : DirectedActorSpawner, DirectedAnimalSpawnerModel.Participant
{
  public float minSpawnIntervalGameHours = 12f;
  public float maxSpawnIntervalGameHours = 18f;
  private DirectedAnimalSpawnerModel model;
  private Oasis oasis;
  private int timeoutDelay;
  private GameObject timeout;

  public override void Awake()
  {
    base.Awake();
    oasis = GetComponentInParent<Oasis>();
    SRSingleton<SceneContext>.Instance.GameModel.RegisterAnimalSpawner(this);
  }

  public void InitModel(DirectedAnimalSpawnerModel model) => model.pos = transform.position;

  public void SetModel(DirectedAnimalSpawnerModel model) => this.model = model;

  public override bool CanSpawn(float? forHour = null) => base.CanSpawn(forHour) && timeDir.HasReached(model.nextSpawnTime) && !IsOasisFull();

  public override IEnumerator Spawn(int count, Randoms rand)
  {
    // ISSUE: reference to a compiler-generated field
    int num = timeoutDelay;
    DirectedAnimalSpawner directedAnimalSpawner = this;
    if (num != 0)
    {
      if (num != 1)
        yield return false;
      // ISSUE: reference to a compiler-generated field
      timeoutDelay = -1;
      yield return false;
    }
    // ISSUE: reference to a compiler-generated field
    timeoutDelay = -1;
    directedAnimalSpawner.model.nextSpawnTime = directedAnimalSpawner.timeDir.HoursFromNowOrStart(Randoms.SHARED.GetInRange(directedAnimalSpawner.minSpawnIntervalGameHours, directedAnimalSpawner.maxSpawnIntervalGameHours));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    //this.timeout = (object) directedAnimalSpawner.\u003C\u003En__0(count, rand);
    // ISSUE: reference to a compiler-generated field
    timeoutDelay = 1;
    yield return true;
  }

  public double GetNextSpawnTime() => model.nextSpawnTime;

  public void SetNextSpawnTime(double time) => model.nextSpawnTime = time;

  protected override void Register(CellDirector cellDir) => cellDir.Register(this);

  private bool IsOasisFull() => oasis != null && oasis.NeedsMoreAnimals();
}
