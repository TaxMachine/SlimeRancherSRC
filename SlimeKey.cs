// Decompiled with JetBrains decompiler
// Type: SlimeKey
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKey : SRBehaviour
{
  public static List<SlimeKey> allKeys = new List<SlimeKey>();
  public GameObject pickupFX;
  private RegionMember regionMember;

  public void Awake()
  {
    allKeys.Add(this);
    regionMember = GetComponent<RegionMember>();
  }

  public bool IsKeyInZone(ZoneDirector.Zone zoneId) => regionMember.IsInZone(zoneId);

  public void OnDestroy()
  {
    allKeys.Remove(this);
    regionMember = null;
  }

  public void OnTriggerEnter(Collider col)
  {
    if (!(col.gameObject == SRSingleton<SceneContext>.Instance.Player))
      return;
    SRSingleton<SceneContext>.Instance.PlayerState.AddKey();
    if (pickupFX != null)
      SpawnAndPlayFX(pickupFX, transform.position, transform.rotation);
    Destroyer.DestroyActor(gameObject, "SlimeKey.OnTriggerEnter");
  }
}
