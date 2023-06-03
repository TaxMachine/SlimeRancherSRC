// Decompiled with JetBrains decompiler
// Type: EchoNet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EchoNet : SRBehaviour, GadgetModel.Participant
{
  public float minSpawnsPerHour = 0.2f;
  public float maxSpawnsPerHour = 0.3333f;
  public Transform[] spawnNodes;
  public GameObject activeVersion;
  public GameObject inactiveVersion;
  private CellDirector cellDir;
  private ZoneDirector zoneDir;
  private LookupDirector lookupDir;
  private TimeDirector timeDir;
  private EchoNetModel model;
  private const float PRESENT_DIST = 0.001f;
  private const float SQR_PRESENT_DIST = 1.00000011E-06f;

  public void Awake()
  {
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
  }

  public void Start()
  {
    zoneDir = GetComponentInParent<ZoneDirector>();
    cellDir = GetComponentInParent<CellDirector>();
    MaybeSpawnEchoes();
  }

  public void InitModel(GadgetModel model) => ResetSpawnTime((EchoNetModel) model);

  public void SetModel(GadgetModel model)
  {
    this.model = (EchoNetModel) model;
    if (!(zoneDir != null))
      return;
    MaybeSpawnEchoes();
  }

  public void OnEnable()
  {
    if (zoneDir == null)
      return;
    MaybeSpawnEchoes();
  }

  private void MaybeSpawnEchoes()
  {
    bool flag = zoneDir.GetAllAuxItems().Count > 0;
    activeVersion.SetActive(flag);
    inactiveVersion.SetActive(!flag);
    double num1 = timeDir.WorldTime() - model.lastSpawnTime;
    ResetSpawnTime(model);
    float num2 = (int) Math.Round(Randoms.SHARED.GetInRange(minSpawnsPerHour, maxSpawnsPerHour) * num1 * 0.00027777778450399637);
    ICollection<Identifiable.Id> allAuxItems = zoneDir.GetAllAuxItems();
    if (num2 <= 0.0 || allAuxItems.Count <= 0)
      return;
    List<GameObject> result = new List<GameObject>();
    foreach (Identifiable.Id id in allAuxItems)
      cellDir.Get(id, ref result);
    List<Transform> iterable = new List<Transform>(spawnNodes);
    foreach (Transform spawnNode in spawnNodes)
    {
      foreach (GameObject gameObject in result)
      {
        if ((gameObject.transform.position - spawnNode.position).sqrMagnitude < 1.0000001111620804E-06)
        {
          iterable.Remove(spawnNode);
          break;
        }
      }
    }
    for (int index = 0; index < (double) num2 && iterable.Count > 0; ++index)
      SpawnAt(Randoms.SHARED.Pluck(iterable, null));
  }

  private void SpawnAt(Transform node) => InstantiateActor(lookupDir.GetPrefab(zoneDir.PickAuxItem()), zoneDir.regionSetId, node.position, node.rotation);

  private void ResetSpawnTime(EchoNetModel model) => model.lastSpawnTime = timeDir.WorldTime();
}
