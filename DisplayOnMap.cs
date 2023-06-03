// Decompiled with JetBrains decompiler
// Type: DisplayOnMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using UnityEngine;

public class DisplayOnMap : MonoBehaviour
{
  public MapMarker markerPrefab;
  public bool HideInFog;
  private MapMarker marker;
  private PlayerState playerState;
  private RegionRegistry.RegionSetId? regionSetId;

  public virtual void Awake()
  {
    SRSingleton<Map>.Instance.RegisterMarker(this);
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    marker = Instantiate(markerPrefab, transform);
    marker.gameObject.SetActive(false);
  }

  public virtual void Refresh()
  {
  }

  public virtual Vector3 GetCurrentPosition() => gameObject.transform.position;

  public virtual MapMarker GetMarker() => marker;

  public virtual ZoneDirector.Zone GetZoneId() => GetComponentInParent<ZoneDirector>().zone;

  public virtual bool ShowOnMap()
  {
    CellDirector parentCellDirector = GetParentCellDirector();
    return (!(parentCellDirector != null) || !parentCellDirector.notShownOnMap) && (!HideInFog || playerState.HasUnlockedMap(GetZoneId()));
  }

  public virtual Quaternion GetCurrentRotation() => Quaternion.identity;

  public virtual RegionRegistry.RegionSetId GetRegionSetId()
  {
    if (regionSetId.HasValue)
      return regionSetId.Value;
    RegionMember component = GetComponent<RegionMember>();
    if (component != null)
    {
      regionSetId = new RegionRegistry.RegionSetId?(component.setId);
      return regionSetId.Value;
    }
    Region componentInParent = GetComponentInParent<Region>();
    regionSetId = componentInParent != null ? new RegionRegistry.RegionSetId?(componentInParent.setId) : throw new Exception(string.Format("Failed to get RegionSetId for DisplayOnMap. [name={0}]", gameObject.name));
    return regionSetId.Value;
  }

  protected CellDirector GetParentCellDirector() => gameObject.GetComponentInParent<CellDirector>();

  public virtual void OnDestroy()
  {
    if (SRSingleton<Map>.Instance != null)
      SRSingleton<Map>.Instance.DeregisterMarker(this);
    if (!(marker != null))
      return;
    Destroyer.Destroy(marker.gameObject, "DisplayOnMap.OnDestroy");
  }
}
