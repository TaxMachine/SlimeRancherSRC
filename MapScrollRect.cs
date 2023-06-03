// Decompiled with JetBrains decompiler
// Type: MapScrollRect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapScrollRect : ScrollRect
{
  private const float DEFAULT_ZOOM = 1f;
  private const float ZOOM_CHANGE_PER_FRAME = 0.04f;
  private float currentZoom = 1f;
  public static float MinZoom = 0.55f;
  public static float MaxZoom = 2f;
  public OnZoomEvent onZoom;

  public float GetCurrentZoom() => currentZoom;

  public override void OnScroll(PointerEventData data)
  {
    if (data.scrollDelta.y < 0.0)
    {
      ZoomOut();
    }
    else
    {
      if (data.scrollDelta.y <= 0.0)
        return;
      ZoomIn();
    }
  }

  public void Scroll(Vector2 scrollDelta) => base.OnScroll(new PointerEventData(null)
  {
    scrollDelta = scrollDelta
  });

  public void ScrollUp() => Scroll(new Vector2(0.0f, scrollSensitivity));

  public void ScrollDown() => Scroll(new Vector2(0.0f, -scrollSensitivity));

  public void ScrollLeft() => Scroll(new Vector2(scrollSensitivity, 0.0f));

  public void ScrollRight() => Scroll(new Vector2(-scrollSensitivity, 0.0f));

  public void ClampContentToScrollView() => base.OnScroll(new PointerEventData(null)
  {
    scrollDelta = new Vector2(0.0f, 0.0f)
  });

  public void ZoomIn() => ZoomTo(currentZoom + 0.04f);

  public void ZoomOut() => ZoomTo(currentZoom - 0.04f);

  public void ResetToDefaultZoom() => ZoomTo(1f);

  public void ZoomTo(float requestedZoomTarget)
  {
    float num1 = Mathf.Clamp(requestedZoomTarget, MinZoom, MaxZoom);
    if (num1 == (double) currentZoom)
      return;
    float num2 = num1 / currentZoom;
    Vector3 localPosition = content.localPosition;
    content.transform.localScale = Vector3.one * num1;
    content.localPosition = localPosition * num2;
    new PointerEventData(null).scrollDelta = new Vector2(0.0f, 0.0f);
    ClampContentToScrollView();
    currentZoom = num1;
    if (onZoom == null)
      return;
    onZoom(currentZoom);
  }

  public delegate void OnZoomEvent(float zoomLevel);
}
