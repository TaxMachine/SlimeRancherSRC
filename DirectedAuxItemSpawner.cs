// Decompiled with JetBrains decompiler
// Type: DirectedAuxItemSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class DirectedAuxItemSpawner : SRBehaviour
{
  private CellDirector cellDir;
  private ZoneDirector zoneDirector;
  private const float PRESENT_DIST = 0.001f;
  private const float SQR_PRESENT_DIST = 1.00000011E-06f;

  public void Start()
  {
    cellDir = GetComponentInParent<CellDirector>();
    zoneDirector = GetComponentInParent<ZoneDirector>();
    zoneDirector.Register(this);
  }

  public void Spawn(GameObject prefab) => InstantiateActor(prefab, zoneDirector.regionSetId, transform.position, transform.rotation);

  public void UnspawnIfPresent(IEnumerable<Identifiable.Id> ids)
  {
    List<GameObject> result = new List<GameObject>();
    foreach (Identifiable.Id id in ids)
      cellDir.Get(id, ref result);
    foreach (GameObject actorObj in result)
    {
      if ((actorObj.transform.position - transform.position).sqrMagnitude < 1.0000001111620804E-06)
        Destroyer.DestroyActor(actorObj, "DirectedAuxItemSpawner.UnspawnIfPresent");
    }
  }
}
