// Decompiled with JetBrains decompiler
// Type: GardenCatcher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class GardenCatcher : SRBehaviour
{
  public PlantSlot[] plantable;
  public LandPlot activator;
  public GameObject acceptFX;
  public SECTR_AudioCue treeScaleUpCue;
  public SECTR_AudioCue treeScaleDownCue;
  private Dictionary<Identifiable.Id, GameObject> plantableDict = new Dictionary<Identifiable.Id, GameObject>(Identifiable.idComparer);
  private Dictionary<Identifiable.Id, GameObject> deluxeDict = new Dictionary<Identifiable.Id, GameObject>(Identifiable.idComparer);
  private TutorialDirector tutDir;

  public void Awake()
  {
    foreach (PlantSlot plantSlot in plantable)
    {
      plantableDict[plantSlot.id] = plantSlot.plantedPrefab;
      deluxeDict[plantSlot.id] = plantSlot.deluxePlantedPrefab;
    }
    tutDir = SRSingleton<SceneContext>.Instance.TutorialDirector;
  }

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger)
      return;
    Identifiable.Id id = Identifiable.GetId(col.gameObject);
    if (!CanAccept(id))
      return;
    Plant(id, false);
    tutDir.OnPlanted();
    if (acceptFX != null)
      SpawnAndPlayFX(acceptFX, transform.position, transform.rotation);
    Destroyer.DestroyActor(col.gameObject, "GardenCatcher.OnTriggerEnter");
  }

  public GameObject Plant(Identifiable.Id cropId, bool isReplacement)
  {
    GameObject toAttach = Instantiate(activator.HasUpgrade(LandPlot.Upgrade.DELUXE_GARDEN) ? deluxeDict[cropId] : plantableDict[cropId], activator.transform.position, activator.transform.rotation);
    if (Identifiable.FRUIT_CLASS.Contains(cropId))
      activator.Attach(toAttach, false, isReplacement, treeScaleUpCue);
    else
      activator.Attach(toAttach, false, isReplacement);
    return toAttach;
  }

  public bool CanAccept(Identifiable.Id id) => !activator.HasAttached() && plantableDict.ContainsKey(id);

  [Serializable]
  public class PlantSlot
  {
    public Identifiable.Id id;
    public GameObject plantedPrefab;
    public GameObject deluxePlantedPrefab;
  }
}
