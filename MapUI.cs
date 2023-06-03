// Decompiled with JetBrains decompiler
// Type: MapUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : BaseUI
{
  public MapScrollRect scrollRect;
  public GameObject mapContentArea;
  public GameObject mapMarkerSection;
  public GameObject disruptionOverlay;
  public List<ZoneFogEntry> zoneFogEntries;
  public TMP_Text currentLocationNameText;
  public TMP_Text keyCount;
  public TMP_Text treasurePodCount;
  public GameObject keyCountLine;
  public GameObject treasurePodCountLine;
  public SECTR_AudioCue openCue;
  public SECTR_AudioCue closeCue;
  public SECTR_AudioCue fogClearCue;
  public SECTR_AudioCue disruptionCue;
  private List<DisplayOnMap> mappableObjects = new List<DisplayOnMap>();
  private PlayerState playerState;
  private MessageBundle pediaBundle;
  private RectTransform mapUIRectTransform;
  private SECTR_AudioCueInstance disruptionCuePlaying;
  private Vector2 mainMapPoint1 = new Vector2(2468f, -2532f);
  private Vector2 mainMapPoint2 = new Vector2(2655f, -2741f);
  private Vector2 mainWorldPoint1 = new Vector2(89.3f, -144.5f);
  private Vector2 mainWorldPoint2 = new Vector2(193.8f, -260.8f);
  private Vector2 desertMapPoint1 = new Vector2(4219f, -2497f);
  private Vector2 desertMapPoint2 = new Vector2(4433f, -1685f);
  private Vector2 desertWorldPoint1 = new Vector2(119.4345f, 918.0937f);
  private Vector2 desertWorldPoint2 = new Vector2(-12.69382f, 416.4283f);
  private Vector4 mainCoefficients;
  private Vector4 desertCoefficients;
  private float mainRotationAdjustment;
  private float desertRotationAdjustment = 180f;
  private Vector2 markerZoomInToSizePoint = new Vector2(2f, 30f);
  private Vector2 markerZoomOutToSizePoint = new Vector2(0.55f, 50f);
  private Vector2 markerZoomSlopeOffset;
  private Vector2 playerMarkerZoomInToSizePoint = new Vector2(2f, 50f);
  private Vector2 playerMarkerZoomOutToSizePoint = new Vector2(0.55f, 70f);
  private Vector2 playerMarkerZoomSlopeOffset;
  private Vector2 worldMarkerPositionMin = new Vector2(1000f, -180f);
  private Vector2 worldMarkerPositionMax = new Vector2(3548f, -3050f);
  private Vector2 desertMarkerPositionMin = new Vector2(2236f, -372f);
  private Vector2 desertMarkerPositionMax = new Vector2(4960f, -2680f);
  private int keysInZone;
  private int keysCollectedInZone;
  private int treasurePodsInZone;
  private int treasurePodsOpenedInZone;
  private HashSet<ZoneDirector.Zone> zonesToReveal = new HashSet<ZoneDirector.Zone>();
  private const float ZONE_REVEAL_TIME = 2f;
  private const float SCROLL_SPEED = 240f;

  public override void Awake()
  {
    base.Awake();
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    PrepareCoefficients();
    scrollRect.onZoom = ScaleMarkersOnZoom;
    mapUIRectTransform = GetComponent<RectTransform>();
  }

  public override void OnBundlesAvailable(MessageDirector msgDir)
  {
    base.OnBundlesAvailable(msgDir);
    pediaBundle = msgDir.GetBundle("pedia");
  }

  public void AddZoneToReveal(ZoneDirector.Zone zoneId) => zonesToReveal.Add(zoneId);

  public void OpenMap()
  {
    Play(openCue);
    RefreshMap();
  }

  public override void Update()
  {
    base.Update();
    if (SRInput.PauseActions.menuTabLeft.IsPressed)
      scrollRect.ZoomOut();
    else if (SRInput.PauseActions.menuTabRight.IsPressed)
      scrollRect.ZoomIn();
    scrollRect.Scroll(new Vector2(SRInput.PauseActions.menuLeft.Value - SRInput.PauseActions.menuRight.Value, SRInput.PauseActions.menuUp.Value - SRInput.PauseActions.menuDown.Value) * 240f * Time.unscaledDeltaTime);
  }

  public override void OnDestroy()
  {
    scrollRect.onZoom = null;
    base.OnDestroy();
  }

  public void RegisterObject(DisplayOnMap displayOnMap)
  {
    if (mappableObjects.Contains(displayOnMap))
      return;
    mappableObjects.Add(displayOnMap);
  }

  public void DeregisterObject(DisplayOnMap displayOnMap) => mappableObjects.Remove(displayOnMap);

  private void RefreshMap()
  {
    LayoutRebuilder.ForceRebuildLayoutImmediate(mapUIRectTransform);
    float currentZoom = scrollRect.GetCurrentZoom();
    scrollRect.ResetToDefaultZoom();
    UpdateZoneFog();
    MapMarker mapMarker = null;
    foreach (DisplayOnMap mappableObject in mappableObjects)
    {
      mappableObject.Refresh();
      if (mappableObject.ShowOnMap())
      {
        MapMarker marker = mappableObject.GetMarker();
        marker.transform.SetParent(mapMarkerSection.gameObject.transform, false);
        marker.transform.localPosition = Vector3.zero;
        marker.gameObject.SetActive(true);
        RegionRegistry.RegionSetId regionSetId = mappableObject.GetRegionSetId();
        Vector4 coefficients;
        Vector2 markerPositionMin;
        Vector2 markerPositionMax;
        if (regionSetId == RegionRegistry.RegionSetId.DESERT)
        {
          coefficients = desertCoefficients;
          markerPositionMin = desertMarkerPositionMin;
          markerPositionMax = desertMarkerPositionMax;
        }
        else
        {
          coefficients = mainCoefficients;
          markerPositionMin = worldMarkerPositionMin;
          markerPositionMax = worldMarkerPositionMax;
        }
        marker.SetAnchoredPosition(GetMapPosClamped(mappableObject.GetCurrentPosition(), coefficients, markerPositionMin, markerPositionMax));
        if (mappableObject is PlayerDisplayOnMap)
        {
          UpdatePlayerMarker(mappableObject as PlayerDisplayOnMap, marker, regionSetId);
          mapMarker = marker;
        }
      }
      else
        mappableObject.GetMarker().gameObject.SetActive(false);
    }
    scrollRect.ZoomTo(currentZoom);
    Canvas.ForceUpdateCanvases();
    CenterMapOnMarker(mapMarker.GetLocalPosition());
  }

  private void UpdatePlayerMarker(
    PlayerDisplayOnMap mappableObject,
    MapMarker marker,
    RegionRegistry.RegionSetId regionId)
  {
    ZoneDirector.Zone zoneId = mappableObject.GetZoneId();
    UpdateZoneItemCounts(zoneId);
    float num = regionId != RegionRegistry.RegionSetId.DESERT ? mainRotationAdjustment : desertRotationAdjustment;
    if (mappableObject.IsInUnknownArea() || zoneId == ZoneDirector.Zone.NONE || !ZoneDirector.zonePediaIdLookup.ContainsKey(zoneId))
    {
      currentLocationNameText.text = uiBundle.Xlate("t.unknown_location");
      if (!(disruptionOverlay != null))
        return;
      disruptionOverlay.SetActive(true);
      disruptionCuePlaying = SECTR_AudioSystem.Play(disruptionCue, transform.position, true);
    }
    else
    {
      Vector3 eulerAngles = mappableObject.GetCurrentRotation().eulerAngles;
      marker.Rotate(Quaternion.Euler(eulerAngles.x + num, eulerAngles.y, eulerAngles.z));
      currentLocationNameText.text = pediaBundle.Xlate(string.Format("t.{0}", ZoneDirector.zonePediaIdLookup[zoneId].ToString().ToLowerInvariant()));
      disruptionOverlay.SetActive(false);
    }
  }

  private void UpdateZoneFog()
  {
    foreach (ZoneFogEntry zoneFogEntry in zoneFogEntries)
    {
      if (zonesToReveal.Contains(zoneFogEntry.zoneId))
        RevealZone(zoneFogEntry);
      else if (playerState.HasUnlockedMap(zoneFogEntry.zoneId))
        zoneFogEntry.fogObject.gameObject.SetActive(false);
      else
        zoneFogEntry.fogObject.gameObject.SetActive(true);
    }
  }

  private void RevealZone(ZoneFogEntry zoneFogEntry)
  {
    Play(fogClearCue);
    zoneFogEntry.fogObject.DOFade(0.0f, 2f).SetUpdate(true);
  }

  private void UpdateZoneItemCounts(ZoneDirector.Zone zoneId)
  {
    treasurePodsInZone = 0;
    treasurePodsOpenedInZone = 0;
    keysInZone = 0;
    keysCollectedInZone = 0;
    bool flag1 = SRSingleton<GameContext>.Instance.DLCDirector.IsPackageInstalledAndEnabled(DLCPackage.Id.SECRET_STYLE);
    foreach (KeyValuePair<string, TreasurePodModel> allPod in SRSingleton<SceneContext>.Instance.GameModel.AllPods())
    {
      if (!DLCDirector.SECRET_STYLE_TREASURE_PODS.Contains(allPod.Key) | flag1)
      {
        TreasurePodModel treasurePodModel = allPod.Value;
        if (treasurePodModel.GetZoneId() == zoneId)
        {
          ++treasurePodsInZone;
          if (treasurePodModel.state == TreasurePod.State.OPEN)
            ++treasurePodsOpenedInZone;
        }
      }
    }
    foreach (GordoModel gordoModel in SRSingleton<SceneContext>.Instance.GameModel.AllGordos().Values)
    {
      if (gordoModel.GetZoneId() == zoneId && gordoModel.DropsKey())
      {
        ++keysInZone;
        if (gordoModel.HasPopped())
          ++keysCollectedInZone;
      }
    }
    foreach (SlimeKey allKey in SlimeKey.allKeys)
    {
      if (allKey.IsKeyInZone(zoneId))
        --keysCollectedInZone;
    }
    bool flag2 = playerState.HasUnlockedMap(zoneId);
    if (keysInZone > 0 & flag2)
    {
      keyCountLine.SetActive(true);
      keyCount.text = string.Format("{0}/{1}", keysCollectedInZone, keysInZone);
    }
    else
      keyCountLine.SetActive(false);
    if (treasurePodsInZone > 0 & flag2)
    {
      treasurePodCountLine.SetActive(true);
      treasurePodCount.text = string.Format("{0}/{1}", treasurePodsOpenedInZone, treasurePodsInZone);
    }
    else
      treasurePodCountLine.SetActive(false);
  }

  private void CenterMapOnMarker(Vector3 position)
  {
    scrollRect.content.localPosition = (Vector2) (-position * scrollRect.content.localScale.x);
    scrollRect.Scroll(Vector2.zero);
  }

  private float ApplyCoefficients(Vector2 coefficients, float xVal) => coefficients.x * xVal + coefficients.y;

  public override void Close()
  {
    Play(closeCue);
    zonesToReveal.Clear();
    disruptionCuePlaying.Stop(true);
    gameObject.SetActive(false);
  }

  public void ScaleMarkersOnZoom(float zoomLevel)
  {
    float num1 = zoomLevel * markerZoomSlopeOffset.x + markerZoomSlopeOffset.y;
    float num2 = zoomLevel * playerMarkerZoomSlopeOffset.x + playerMarkerZoomSlopeOffset.y;
    foreach (DisplayOnMap mappableObject in mappableObjects)
    {
      if (mappableObject is PlayerDisplayOnMap)
        mappableObject.GetMarker().SetSize(num2, num2);
      else
        mappableObject.GetMarker().SetSize(num1, num1);
    }
  }

  private void PrepareCoefficients()
  {
    mainCoefficients = GetWorldToMapCoefficients(mainWorldPoint2, mainWorldPoint1, mainMapPoint2, mainMapPoint1);
    desertCoefficients = GetWorldToMapCoefficients(desertWorldPoint1, desertWorldPoint2, desertMapPoint1, desertMapPoint2);
    markerZoomSlopeOffset = GetSlopeAndOffset(markerZoomInToSizePoint, markerZoomOutToSizePoint);
    playerMarkerZoomSlopeOffset = GetSlopeAndOffset(playerMarkerZoomInToSizePoint, playerMarkerZoomOutToSizePoint);
  }

  private Vector4 GetWorldToMapCoefficients(
    Vector2 worldPoint1,
    Vector2 worldPoint2,
    Vector2 mapPoint1,
    Vector2 mapPoint2)
  {
    Vector2 slopeAndOffset1 = GetSlopeAndOffset(new Vector2(worldPoint2.x, mapPoint2.x), new Vector2(worldPoint1.x, mapPoint1.x));
    Vector2 slopeAndOffset2 = GetSlopeAndOffset(new Vector2(worldPoint2.y, mapPoint2.y), new Vector2(worldPoint1.y, mapPoint1.y));
    return new Vector4(slopeAndOffset1.x, slopeAndOffset1.y, slopeAndOffset2.x, slopeAndOffset2.y);
  }

  private Vector2 GetSlopeAndOffset(Vector2 point1, Vector2 point2)
  {
    float x = (float) ((point2.y - (double) point1.y) / (point2.x - (double) point1.x));
    float y = point2.y - point2.x * x;
    return new Vector2(x, y);
  }

  private Vector2 GetMapPos(Vector3 playerPosition, Vector4 coefficients) => new Vector2(playerPosition.x * coefficients.x + coefficients.y, playerPosition.z * coefficients.z + coefficients.w);

  private Vector2 GetMapPosClamped(
    Vector3 playerPosition,
    Vector4 coefficients,
    Vector2 minPoint,
    Vector2 maxPoint)
  {
    Vector2 mapPos = GetMapPos(playerPosition, coefficients);
    return new Vector2(Mathf.Clamp(mapPos.x, minPoint.x, maxPoint.x), Mathf.Clamp(mapPos.y, maxPoint.y, minPoint.y));
  }

  [Serializable]
  public class ZoneFogEntry
  {
    public ZoneDirector.Zone zoneId;
    public CanvasGroup fogObject;
  }
}
