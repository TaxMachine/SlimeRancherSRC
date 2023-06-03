// Decompiled with JetBrains decompiler
// Type: TransformAfterTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TransformAfterTime : SRBehaviour, ActorModel.Participant
{
  public float delayGameHours = 6f;
  public GameObject transformFX;
  public List<TransformOpt> options;
  private TimeDirector timeDir;
  private List<FeederRegion> feeders = new List<FeederRegion>();
  private double lastWorldTime;
  private AnimalModel model;
  private RegionMember regionMember;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    regionMember = GetComponent<RegionMember>();
    lastWorldTime = timeDir.HoursFromNowOrStart(0.0f);
  }

  public void InitModel(ActorModel model) => ((AnimalModel) model).transformTime = timeDir.HoursFromNowOrStart(delayGameHours);

  public void SetModel(ActorModel model) => this.model = (AnimalModel) model;

  public void Update()
  {
    if (feeders.Count > 0)
      model.transformTime -= (timeDir.WorldTime() - lastWorldTime) * 1.0;
    if (timeDir.HasReached(model.transformTime) && options.Count > 0)
    {
      Dictionary<GameObject, float> weightMap = new Dictionary<GameObject, float>();
      foreach (TransformOpt option in options)
        weightMap[option.targetPrefab] = option.weight;
      SpawnAndPlayFX(transformFX, transform.position, transform.rotation);
      Destroyer.DestroyActor(gameObject, "TransformAfterTime.Update");
      InstantiateActor(Randoms.SHARED.Pick(weightMap, null), regionMember.setId, transform.position, transform.rotation);
    }
    lastWorldTime = timeDir.WorldTime();
  }

  public void AddFeeder(FeederRegion feeder) => feeders.Add(feeder);

  public void RemoveFeeder(FeederRegion feeder) => feeders.Remove(feeder);

  [Serializable]
  public class TransformOpt
  {
    public GameObject targetPrefab;
    public float weight;
  }
}
