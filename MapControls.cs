// Decompiled with JetBrains decompiler
// Type: MapControls
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MapControls : MonoBehaviour
{
  public GameObject upButton;
  public GameObject downButton;
  public GameObject leftButton;
  public GameObject rightButton;
  public GameObject zoomInButton;
  public GameObject zoomOutButton;
  public GameObject scrollView;
  private MapScrollRect mapScrollRect;

  public void Awake()
  {
    if (!(scrollView != null))
      return;
    mapScrollRect = scrollView.GetComponent<MapScrollRect>();
    WireButton(upButton, mapScrollRect.ScrollUp);
    WireButton(downButton, mapScrollRect.ScrollDown);
    WireButton(leftButton, mapScrollRect.ScrollLeft);
    WireButton(rightButton, mapScrollRect.ScrollRight);
    WireButton(zoomInButton, mapScrollRect.ZoomIn);
    WireButton(zoomOutButton, mapScrollRect.ZoomOut);
  }

  public void OnDestroy()
  {
    if (!(mapScrollRect != null))
      return;
    UnwireButton(upButton, mapScrollRect.ScrollUp);
    UnwireButton(downButton, mapScrollRect.ScrollDown);
    UnwireButton(leftButton, mapScrollRect.ScrollLeft);
    UnwireButton(rightButton, mapScrollRect.ScrollRight);
    UnwireButton(zoomInButton, mapScrollRect.ZoomIn);
    UnwireButton(zoomOutButton, mapScrollRect.ZoomOut);
  }

  private void WireButton(
    GameObject button,
    PerformWhileMouseDown.MouseIsDownEvent eventHandler)
  {
    if (!(button != null))
      return;
    PerformWhileMouseDown component = button.GetComponent<PerformWhileMouseDown>();
    if (!(component != null))
      return;
    component.WhileMouseIsDown += eventHandler;
  }

  private void UnwireButton(
    GameObject button,
    PerformWhileMouseDown.MouseIsDownEvent eventHandler)
  {
    if (!(button != null))
      return;
    PerformWhileMouseDown component = button.GetComponent<PerformWhileMouseDown>();
    if (!(component != null))
      return;
    component.WhileMouseIsDown -= eventHandler;
  }
}
