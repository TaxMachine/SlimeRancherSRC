// Decompiled with JetBrains decompiler
// Type: RadarTrackedObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RadarTrackedObject : MonoBehaviour
{
  public Image radarImage;
  public bool isOptional;
  [NonSerialized]
  public RegionRegistry.RegionSetId regionSetId = RegionRegistry.RegionSetId.UNSET;

  public void Start()
  {
    if (regionSetId == RegionRegistry.RegionSetId.UNSET)
    {
      regionSetId = GetComponentInParent<Region>().setId;
      SRSingleton<RadarPanelUI>.Instance.RegisterTracked(gameObject, regionSetId, radarImage, isOptional);
    }
    transform.SetParent(SRSingleton<DynamicObjectContainer>.Instance.transform, true);
  }

  public void OnEnable()
  {
    if (regionSetId == RegionRegistry.RegionSetId.UNSET)
      return;
    SRSingleton<RadarPanelUI>.Instance.RegisterTracked(gameObject, regionSetId, radarImage, isOptional);
  }

  public void OnDisable()
  {
    if (!(SRSingleton<RadarPanelUI>.Instance != null))
      return;
    SRSingleton<RadarPanelUI>.Instance.UnregisterTracked(gameObject);
  }
}
