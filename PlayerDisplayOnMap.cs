// Decompiled with JetBrains decompiler
// Type: PlayerDisplayOnMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Linq;
using UnityEngine;

public class PlayerDisplayOnMap : DisplayOnMap
{
  public MapMarker playerUnknownLocationMarkerPrefab;
  private MapMarker playerUnknownLocationMarker;
  private bool isInHiddenCell;
  private PlayerZoneTracker playerZoneTracker;

  public override void Awake()
  {
    base.Awake();
    playerUnknownLocationMarker = Instantiate(playerUnknownLocationMarkerPrefab);
    playerZoneTracker = GetComponent<PlayerZoneTracker>();
  }

  public override ZoneDirector.Zone GetZoneId() => playerZoneTracker.GetCurrentZone();

  public bool IsInUnknownArea() => IsInHiddenCell();

  public override RegionRegistry.RegionSetId GetRegionSetId() => !isInHiddenCell ? SRSingleton<SceneContext>.Instance.RegionRegistry.GetCurrentRegionSetId() : RegionRegistry.RegionSetId.HOME;

  private bool IsInHiddenCell() => GetComponent<RegionMember>().regions.Where(r => r.cellDir.notShownOnMap).Count() > 0;

  public override void OnDestroy()
  {
    base.OnDestroy();
    Destroyer.Destroy(playerUnknownLocationMarker, "PlayerDisplayOnMap.OnDestroy");
  }

  public override Vector3 GetCurrentPosition() => !isInHiddenCell ? base.GetCurrentPosition() : Vector3.zero;

  public override void Refresh()
  {
    base.Refresh();
    isInHiddenCell = IsInHiddenCell();
    if (isInHiddenCell)
    {
      playerUnknownLocationMarker.gameObject.SetActive(true);
      base.GetMarker().gameObject.SetActive(false);
    }
    else
    {
      playerUnknownLocationMarker.gameObject.SetActive(false);
      base.GetMarker().gameObject.SetActive(true);
    }
  }

  public override bool ShowOnMap() => true;

  public override MapMarker GetMarker() => !isInHiddenCell ? base.GetMarker() : playerUnknownLocationMarker;

  public override Quaternion GetCurrentRotation() => !isInHiddenCell ? gameObject.transform.rotation : Quaternion.identity;
}
