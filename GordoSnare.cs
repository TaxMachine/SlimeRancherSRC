// Decompiled with JetBrains decompiler
// Type: GordoSnare
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class GordoSnare : SRBehaviour, GadgetModel.Participant
{
  public static List<GordoSnare> AllGordoSnares = new List<GordoSnare>();
  private bool isSnared;
  public GameObject baitPosition;
  public GameObject bait;
  public GameObject baitAttachedFx;
  public int pinkSnareWeight;
  public int foodTypeSnareWeight;
  public int favoredFoodSnareWeight;
  private SnareModel model;

  public void Awake() => AllGordoSnares.Add(this);

  public void OnDestroy() => AllGordoSnares.Remove(this);

  public void InitModel(GadgetModel model)
  {
  }

  public void SetModel(GadgetModel model)
  {
    this.model = (SnareModel) model;
    if (this.model.baitTypeId != Identifiable.Id.NONE)
    {
      AttachBait(this.model.baitTypeId);
    }
    else
    {
      if (this.model.gordoTypeId == Identifiable.Id.NONE)
        return;
      SnareGordo(this.model.gordoTypeId);
    }
  }

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger || !(bait == null) || isSnared)
      return;
    Identifiable component = col.GetComponent<Identifiable>();
    if (!(component != null) || !Identifiable.IsFood(component.id))
      return;
    if (baitAttachedFx != null)
      SpawnAndPlayFX(baitAttachedFx, gameObject);
    Destroyer.DestroyActor(col.gameObject, "GordoSnare.OnTriggerEnter");
    AttachBait(component.id);
  }

  public bool HasSnaredGordo() => isSnared;

  public bool IsBaited() => model.baitTypeId != 0;

  public bool SnareGordo()
  {
    if (!IsBaited() || HasSnaredGordo())
      return false;
    Identifiable.Id gordoIdForBait = GetGordoIdForBait();
    SnareGordo(gordoIdForBait);
    if (gordoIdForBait == Identifiable.Id.HUNTER_GORDO)
      SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.SNARED_HUNTER_GORDOS, 1);
    return true;
  }

  private void SnareGordo(Identifiable.Id id)
  {
    model.gordoTypeId = id;
    GameObject gameObject = Instantiate(SRSingleton<GameContext>.Instance.LookupDirector.GetGordo(id), this.gameObject.transform);
    gameObject.transform.localPosition = Vector3.zero;
    gameObject.transform.localRotation = Quaternion.identity;
    GadgetModel.Participant[] components = gameObject.GetComponents<GadgetModel.Participant>();
    foreach (GadgetModel.Participant participant in components)
      participant.InitModel(model);
    foreach (GadgetModel.Participant participant in components)
      participant.SetModel(model);
    isSnared = true;
    ClearBait();
  }

  private Identifiable.Id GetGordoIdForBait()
  {
    Dictionary<Identifiable.Id, float> weightMap = new Dictionary<Identifiable.Id, float>(Identifiable.idComparer);
    Identifiable.Id key = Identifiable.Id.NONE;
    List<Identifiable.Id> idList = new List<Identifiable.Id>();
    foreach (GameObject gordoEntry in SRSingleton<GameContext>.Instance.LookupDirector.GordoEntries)
    {
      GordoEat component1 = gordoEntry.GetComponent<GordoEat>();
      GordoIdentifiable component2 = component1.GetComponent<GordoIdentifiable>();
      if (component2.id != Identifiable.Id.PINK_GORDO)
      {
        SlimeDiet diet = component1.slimeDefinition.Diet;
        List<SlimeDiet.EatMapEntry> eatMapEntryList = new List<SlimeDiet.EatMapEntry>();
        int baitTypeId = (int) model.baitTypeId;
        List<SlimeDiet.EatMapEntry> targetEntries = eatMapEntryList;
        diet.AddEatMapEntries((Identifiable.Id) baitTypeId, targetEntries);
        SlimeDiet.EatMapEntry eatMapEntry = eatMapEntryList.Count > 0 ? eatMapEntryList[0] : null;
        bool flag = false;
        for (int index = 0; index < component2.nativeZones.Length; ++index)
        {
          if (ZoneDirector.HasAccessToZone(component2.nativeZones[index]))
            flag = true;
        }
        if (flag && eatMapEntry != null)
        {
          if (eatMapEntry.isFavorite)
          {
            Log.Debug("Found favorite", "gordo", component2.id, "hasAccess", flag);
            key = component2.id;
          }
          else
          {
            Log.Debug("Adding potential", "gordo", component2.id, "hasAccess", flag);
            idList.Add(component2.id);
          }
        }
      }
    }
    if (idList.Count > 0)
    {
      float num = foodTypeSnareWeight / idList.Count;
      for (int index = 0; index < idList.Count; ++index)
        weightMap.Add(idList[index], num);
    }
    if (key != Identifiable.Id.NONE)
      weightMap.Add(key, favoredFoodSnareWeight);
    weightMap.Add(Identifiable.Id.PINK_GORDO, pinkSnareWeight);
    return Randoms.SHARED.Pick(weightMap, Identifiable.Id.PINK_GORDO);
  }

  private void AttachBait(Identifiable.Id id)
  {
    ClearBait();
    model.baitTypeId = id;
    bait = Instantiate(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(id), transform);
    bait.transform.position = baitPosition.transform.position;
    bait.transform.rotation = Quaternion.identity;
    RemoveComponents<Collider>(bait);
    RemoveComponent<DragFloatReactor>(bait);
    RemoveComponent<Rigidbody>(bait);
    RemoveComponent<KeepUpright>(bait);
    RemoveComponent<DontGoThroughThings>(bait);
    RemoveComponent<SECTR_PointSource>(bait);
    RemoveComponent<RegionMember>(bait);
    RemoveComponent<ChickenRandomMove>(bait);
    RemoveComponent<ChickenVampirism>(bait);
    RemoveComponent<PlaySoundOnHit>(bait);
    RemoveComponent<ResourceCycle>(bait);
    RemoveComponent<Reproduce>(bait);
    RemoveComponent<SlimeSubbehaviourPlexer>(bait);
    Animator componentInChildren = bait.GetComponentInChildren<Animator>();
    if (!(componentInChildren != null))
      return;
    componentInChildren.SetBool("grounded", true);
  }

  public void Destroy()
  {
    Gadget componentInParent = GetComponentInParent<Gadget>();
    if (!(componentInParent != null))
      return;
    componentInParent.DestroyGadget();
  }

  private void ClearBait()
  {
    if (!(bait != null))
      return;
    model.baitTypeId = Identifiable.Id.NONE;
    Destroyer.Destroy(bait, 0.0f, "GordoSnare.ClearBait", true);
  }

  private void RemoveComponent<T>(GameObject gameObject) where T : Component
  {
    T component = gameObject.GetComponent<T>();
    if (!(component != null))
      return;
    Destroyer.Destroy(component, "GordoSnare.RemoveComponent");
  }

  private void RemoveComponents<T>(GameObject gameObject) where T : Component
  {
    foreach (T component in gameObject.GetComponents<T>())
      Destroyer.Destroy(component, "GordoSnare.RemoveComponents");
  }

  private void RemoveComponentInChildren<T>(GameObject gameObject) where T : Component
  {
    T componentInChildren = gameObject.GetComponentInChildren<T>();
    if (!(componentInChildren != null))
      return;
    Destroyer.Destroy(componentInChildren, "GordoSnare.RemoveComponentInChildren");
  }

  private void RemoveComponentsInChildren<T>(GameObject gameObject) where T : Component
  {
    foreach (T componentsInChild in gameObject.GetComponentsInChildren<T>())
      Destroyer.Destroy(componentsInChild, "GordoSnare.RemoveComponentsInChildren");
  }

  public static bool HasSnaredGordo(GadgetSite site)
  {
    if (site.HasAttached())
    {
      switch (site.GetAttachedId())
      {
        case Gadget.Id.GORDO_SNARE_NOVICE:
        case Gadget.Id.GORDO_SNARE_ADVANCED:
        case Gadget.Id.GORDO_SNARE_MASTER:
          GordoSnare componentInChildren = site.GetAttached().GetComponentInChildren<GordoSnare>();
          if (componentInChildren != null)
            return componentInChildren.HasSnaredGordo();
          break;
      }
    }
    return false;
  }
}
