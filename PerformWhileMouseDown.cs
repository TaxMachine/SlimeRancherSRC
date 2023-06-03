// Decompiled with JetBrains decompiler
// Type: PerformWhileMouseDown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class PerformWhileMouseDown : 
  MonoBehaviour,
  IPointerDownHandler,
  IEventSystemHandler,
  IPointerUpHandler
{
  private bool isMouseDown;
  public MouseIsDownEvent WhileMouseIsDown;

  public void Update()
  {
    if (!isMouseDown)
      return;
    WhileMouseIsDown();
  }

  public void OnPointerDown(PointerEventData eventData) => isMouseDown = true;

  public void OnPointerUp(PointerEventData eventData) => isMouseDown = false;

  public void OnDestroy() => WhileMouseIsDown = null;

  public delegate void MouseIsDownEvent();
}
