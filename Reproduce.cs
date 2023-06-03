// Decompiled with JetBrains decompiler
// Type: Reproduce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Reproduce : RegisteredActorBehaviour, RegistryUpdateable, ActorModel.Participant
{
  public Identifiable nearMateId;
  public float maxDistToMate = 10f;
  public Identifiable[] densityIds;
  public float densityDist = 10f;
  public int maxDensity = 10;
  public GameObject childPrefab;
  public GameObject produceFX;
  public float minReproduceGameHours = 6f;
  public float maxReproduceGameHours = 12f;
  public float deluxeDensityFactor = 2f;
  private TimeDirector timeDir;
  private RegionMember regionMember;
  private AnimalModel model;
  private const float NON_COOP_REPRO_PROB = 0.5f;
  private List<GameObject> nearbyGameObjects = new List<GameObject>();

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    regionMember = GetComponent<RegionMember>();
  }

  public void InitModel(ActorModel model) => ((AnimalModel) model).nextReproduceTime = timeDir.HoursFromNowOrStart(ReproducePeriod());

  public void SetModel(ActorModel model) => this.model = (AnimalModel) model;

  public void RegistryUpdate()
  {
    if (!timeDir.HasReached(model.nextReproduceTime))
      return;
    GameObject gameObject = NearMateAndSparseEnough();
    if (ShouldReproduce() && gameObject != null)
    {
      CreateChick();
      if (WithinVitamizer() && Randoms.SHARED.GetProbability(0.5f))
        CreateChick();
      TransformChanceOnReproduce component1 = GetComponent<TransformChanceOnReproduce>();
      TransformChanceOnReproduce component2 = gameObject.GetComponent<TransformChanceOnReproduce>();
      if (component1 != null)
        component1.MaybeTransform();
      if (component2 != null)
        component2.MaybeTransform();
    }
    model.nextReproduceTime = timeDir.HoursFromNow(ReproducePeriod());
  }

  private GameObject CreateChick()
  {
    GameObject chick = InstantiateActor(childPrefab, regionMember.setId, transform.position, transform.rotation);
    EggActivator component = chick.GetComponent<EggActivator>();
    if (!(component != null))
      return chick;
    component.AddEgg();
    return chick;
  }

  private bool ShouldReproduce() => WithinCoop() || Randoms.SHARED.GetProbability(0.5f);

  private bool WithinCoop() => CoopRegion.IsWithin(transform.position);

  private bool WithinDeluxeCoop() => CoopRegion.IsWithinDeluxe(transform.position);

  private bool WithinVitamizer() => VitamizerRegion.IsWithin(transform.position);

  private float ReproducePeriod()
  {
    if (maxReproduceGameHours < (double) minReproduceGameHours)
      throw new InvalidOperationException("Invalid reproduce periods. min:" + minReproduceGameHours + " max: " + maxReproduceGameHours);
    return Randoms.SHARED.GetInRange(minReproduceGameHours, maxReproduceGameHours);
  }

  private int MaxDensity() => !WithinDeluxeCoop() ? maxDensity : Mathf.RoundToInt(maxDensity * deluxeDensityFactor);

  private GameObject NearMateAndSparseEnough()
  {
    Vector3 position = transform.position;
    float num1 = maxDistToMate * maxDistToMate;
    nearbyGameObjects.Clear();
    CellDirector.Get(nearMateId.id, regionMember, nearbyGameObjects);
    GameObject gameObject = null;
    for (int index = 0; index < nearbyGameObjects.Count; ++index)
    {
      GameObject nearbyGameObject = nearbyGameObjects[index];
      if ((nearbyGameObject.transform.position - position).sqrMagnitude <= (double) num1)
      {
        gameObject = nearbyGameObject;
        break;
      }
    }
    nearbyGameObjects.Clear();
    if (gameObject == null)
      return gameObject;
    int num2 = 0;
    float num3 = densityDist * densityDist;
    int num4 = MaxDensity();
    for (int index1 = 0; index1 < densityIds.Length && num2 <= num4; ++index1)
    {
      Identifiable densityId = densityIds[index1];
      nearbyGameObjects.Clear();
      CellDirector.Get(densityId.id, regionMember, nearbyGameObjects);
      for (int index2 = 0; index2 < nearbyGameObjects.Count && num2 <= num4; ++index2)
      {
        if ((nearbyGameObjects[index2].transform.position - position).sqrMagnitude <= (double) num3)
          ++num2;
      }
    }
    nearbyGameObjects.Clear();
    return num2 > num4 ? null : gameObject;
  }
}
