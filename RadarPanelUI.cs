// Decompiled with JetBrains decompiler
// Type: RadarPanelUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarPanelUI : SRSingleton<RadarPanelUI>
{
  private List<TrackedEntry> registered = new List<TrackedEntry>();
  private List<Image> optional = new List<Image>();
  private bool radarVisible;
  private Camera mainCamera;
  private RectTransform rectTransform;
  private RegionRegistry regionReg;

  public override void Awake()
  {
    base.Awake();
    regionReg = SRSingleton<SceneContext>.Instance.RegionRegistry;
    rectTransform = GetComponent<RectTransform>();
    UpdateOptionalActiveness();
  }

  public void Start() => mainCamera = Camera.main;

  public void Update()
  {
    if (Time.timeScale == 0.0 || !SRInput.Actions.radarToggle.WasPressed)
      return;
    radarVisible = !radarVisible;
    AnalyticsUtil.CustomEvent("RadarToggled", new Dictionary<string, object>()
    {
      {
        "RadarState",
        radarVisible
      }
    });
    UpdateOptionalActiveness();
  }

  private void UpdateOptionalActiveness()
  {
    foreach (Component component in optional)
      component.gameObject.SetActive(radarVisible);
  }

  public void LateUpdate()
  {
    RegionRegistry.RegionSetId currentRegionSetId = regionReg.GetCurrentRegionSetId();
    foreach (TrackedEntry trackedEntry in registered)
    {
      if (trackedEntry.setId == currentRegionSetId)
      {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(trackedEntry.obj.transform.position);
        Transform transform = trackedEntry.img.transform;
        Rect rect = rectTransform.rect;
        double x = rect.width * (viewportPoint.x - 0.5);
        rect = rectTransform.rect;
        double y = rect.height * (viewportPoint.y - 0.5);
        Vector3 vector3 = new Vector3((float) x, (float) y);
        transform.localPosition = vector3;
        trackedEntry.img.enabled = viewportPoint.z >= 0.0;
      }
      else
        trackedEntry.img.enabled = false;
    }
  }

  public void RegisterTracked(
    GameObject obj,
    RegionRegistry.RegionSetId regionSetId,
    Image img,
    bool isOptional)
  {
    Image img1 = Instantiate(img);
    registered.Add(new TrackedEntry(obj, img1, regionSetId));
    img1.transform.SetParent(transform);
    if (!isOptional)
      return;
    optional.Add(img1);
    img1.gameObject.SetActive(radarVisible);
  }

  public void UnregisterTracked(GameObject obj)
  {
    TrackedEntry trackedEntry1 = null;
    foreach (TrackedEntry trackedEntry2 in registered)
    {
      if (trackedEntry2.obj == obj)
      {
        trackedEntry1 = trackedEntry2;
        break;
      }
    }
    if (trackedEntry1 == null)
      return;
    Destroyer.Destroy(trackedEntry1.img.gameObject, "RadarPanelUI.UnregisterTracked");
    optional.Remove(trackedEntry1.img);
    registered.Remove(trackedEntry1);
  }

  private class TrackedEntry
  {
    public GameObject obj;
    public Image img;
    public RegionRegistry.RegionSetId setId;

    public TrackedEntry(GameObject obj, Image img, RegionRegistry.RegionSetId setId)
    {
      this.obj = obj;
      this.img = img;
      this.setId = setId;
    }
  }
}
