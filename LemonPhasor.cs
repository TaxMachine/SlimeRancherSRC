// Decompiled with JetBrains decompiler
// Type: LemonPhasor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class LemonPhasor : SRBehaviour
{
  private SpawnResource spawnResource;
  private Region region;
  public GameObject lemonPrefab;
  public GameObject spawnLemonFx;
  public GameObject phaseoutFruitFx;
  private HashSet<GameObject> handledFruit = new HashSet<GameObject>();

  public void Awake()
  {
    region = GetComponentInParent<Region>();
    spawnResource = GetComponent<SpawnResource>();
  }

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger)
      return;
    Identifiable component1 = col.gameObject.GetComponent<Identifiable>();
    if (component1 == null || !Identifiable.FRUIT_CLASS.Contains(component1.id) || component1.id == Identifiable.Id.LEMON_FRUIT || handledFruit.Contains(col.gameObject))
      return;
    Joint joint = spawnResource.PickRipeResourceJoint();
    if (joint == null)
      return;
    handledFruit.Add(col.gameObject);
    ((ProduceModel) SRSingleton<SceneContext>.Instance.GameModel.GetActorModel(component1.GetActorId())).state = ResourceCycle.State.ROTTEN;
    Destroyer.DestroyActor(joint.connectedBody.gameObject, "LemonPhasor.OnTriggerEnter#1");
    GameObject gameObject = InstantiateActor(lemonPrefab, region.setId, joint.transform.position, joint.transform.rotation);
    ProduceModel actorModel = (ProduceModel) SRSingleton<SceneContext>.Instance.GameModel.GetActorModel(component1.GetActorId());
    ResourceCycle component2 = gameObject.GetComponent<ResourceCycle>();
    actorModel.state = ResourceCycle.State.EDIBLE;
    component2.Eject(gameObject.GetComponent<Rigidbody>());
    if (spawnLemonFx != null)
      SpawnAndPlayFX(spawnLemonFx, joint.transform.position, joint.transform.rotation);
    if (phaseoutFruitFx != null)
      SpawnAndPlayFX(phaseoutFruitFx, col.transform.position, col.transform.rotation);
    Destroyer.DestroyActor(col.gameObject, "LemonPhasor.OnTriggerEnter#2");
  }

  public void LateUpdate() => handledFruit.Clear();
}
