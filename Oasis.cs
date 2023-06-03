// Decompiled with JetBrains decompiler
// Type: Oasis
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oasis : IdHandler, OasisModel.Participant
{
  public float scaleUpTime = 8f;
  public int targetSlimeCount = 20;
  public int targetAnimalCount = 5;
  public FireColumn[] suppressColumns;
  private Transform[] children;
  private SphereCollider oasisCollider;
  private List<Identifiable> slimeIdents = new List<Identifiable>();
  private List<Identifiable> animalIdents = new List<Identifiable>();
  private BoundingSphere boundingSphere;
  private OasisModel model;
  public static ExposedArrayList<BoundingSphere> oasisSpheres = new ExposedArrayList<BoundingSphere>(20);

  public void Awake()
  {
    oasisCollider = GetComponent<SphereCollider>();
    if (oasisCollider != null)
      oasisCollider.enabled = false;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterOasis(id, gameObject);
    CreateBoundingSphere();
  }

  public void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterOasis(id);
  }

  private void CreateBoundingSphere()
  {
    Vector3 position = transform.position;
    boundingSphere = new BoundingSphere(new Vector4(position.x + oasisCollider.center.x, position.y + oasisCollider.center.y, position.z + oasisCollider.center.z, oasisCollider.radius));
  }

  public void InitModel(OasisModel model)
  {
  }

  public void SetModel(OasisModel model)
  {
    this.model = model;
    int childCount = transform.childCount;
    children = new Transform[childCount];
    for (int index = 0; index < childCount; ++index)
    {
      children[index] = transform.GetChild(index);
      if (!model.isLive)
        children[index].gameObject.SetActive(false);
    }
    if (!model.isLive)
      return;
    OnSetLive(true);
  }

  public void SetLive(bool immediate)
  {
    if (model.isLive)
      return;
    OnSetLive(immediate);
  }

  private void OnSetLive(bool immediate)
  {
    if (oasisCollider != null)
    {
      oasisCollider.enabled = true;
      oasisSpheres.Add(boundingSphere);
    }
    model.isLive = true;
    TweenScaleChildren(immediate);
    if (!immediate)
    {
      StartCoroutine(DelayedTriggerAllSpawners());
      SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.ACTIVATED_OASES, 1);
    }
    foreach (FireColumn suppressColumn in suppressColumns)
    {
      if (suppressColumn != null)
        suppressColumn.NoteInOasis();
    }
  }

  public bool IsLive() => model.isLive;

  private IEnumerator DelayedTriggerAllSpawners()
  {
    yield return new WaitForSeconds(scaleUpTime);
    TriggerAllSpawners();
  }

  private void TriggerAllSpawners()
  {
    DirectedSlimeSpawner[] componentsInChildren1 = GetComponentsInChildren<DirectedSlimeSpawner>();
    List<DirectedSlimeSpawner> directedSlimeSpawnerList = new List<DirectedSlimeSpawner>();
    foreach (DirectedSlimeSpawner directedSlimeSpawner in componentsInChildren1)
    {
      if (directedSlimeSpawner.allowDirectedSpawns)
        directedSlimeSpawnerList.Add(directedSlimeSpawner);
    }
    foreach (DirectedActorSpawner directedActorSpawner in directedSlimeSpawnerList)
      StartCoroutine(directedActorSpawner.Spawn(targetSlimeCount / directedSlimeSpawnerList.Count, Randoms.SHARED));
    DirectedAnimalSpawner[] componentsInChildren2 = GetComponentsInChildren<DirectedAnimalSpawner>();
    List<DirectedAnimalSpawner> directedAnimalSpawnerList = new List<DirectedAnimalSpawner>();
    foreach (DirectedAnimalSpawner directedAnimalSpawner in componentsInChildren2)
    {
      if (directedAnimalSpawner.allowDirectedSpawns)
        directedAnimalSpawnerList.Add(directedAnimalSpawner);
    }
    foreach (DirectedActorSpawner directedActorSpawner in directedAnimalSpawnerList)
      StartCoroutine(directedActorSpawner.Spawn(targetAnimalCount / directedAnimalSpawnerList.Count, Randoms.SHARED));
  }

  public void OnTriggerEnter(Collider col)
  {
    FireBall component1 = col.GetComponent<FireBall>();
    if (component1 != null)
    {
      component1.Vaporize();
    }
    else
    {
      Identifiable component2 = col.GetComponent<Identifiable>();
      if (!(component2 != null))
        return;
      if (Identifiable.IsSlime(component2.id))
      {
        slimeIdents.Add(component2);
      }
      else
      {
        if (!Identifiable.IsAnimal(component2.id))
          return;
        animalIdents.Add(component2);
      }
    }
  }

  public void OnTriggerExit(Collider col)
  {
    Identifiable component = col.GetComponent<Identifiable>();
    if (!(component != null))
      return;
    if (Identifiable.IsSlime(component.id))
    {
      slimeIdents.Remove(component);
    }
    else
    {
      if (!Identifiable.IsAnimal(component.id))
        return;
      animalIdents.Remove(component);
    }
  }

  public bool NeedsMoreSlimes()
  {
    EnsureNoDestroyedIdents();
    return slimeIdents.Count < targetSlimeCount;
  }

  public bool NeedsMoreAnimals()
  {
    EnsureNoDestroyedIdents();
    return animalIdents.Count < targetAnimalCount;
  }

  private void TweenScaleChildren(bool immediate)
  {
    foreach (Transform child in children)
    {
      child.gameObject.SetActive(true);
      if (!immediate)
      {
        SpawnResource[] spawners = child.GetComponentsInChildren<SpawnResource>(true);
        foreach (SpawnResource spawnResource in spawners)
          spawnResource.RegisterSpawnBlocker();
        TweenUtil.ScaleIn(child.gameObject, scaleUpTime).OnComplete(() => TweenScaleChildren_OnTweenComplete(spawners));
      }
    }
  }

  private void TweenScaleChildren_OnTweenComplete(SpawnResource[] spawners)
  {
    foreach (SpawnResource spawner in spawners)
      spawner.DeregisterSpawnBlocker();
  }

  private void EnsureNoDestroyedIdents()
  {
    slimeIdents.RemoveAll(ident => ident == null);
    animalIdents.RemoveAll(ident => ident == null);
  }

  protected override string IdPrefix() => "oasis";
}
