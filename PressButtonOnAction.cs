// Decompiled with JetBrains decompiler
// Type: PressButtonOnAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof (Image))]
public class PressButtonOnAction : MonoBehaviour
{
  public string action;
  [Tooltip("If true, the mouse up/down events will not trigger unless the button is currently selected.")]
  public bool requiresCurrentSelection;
  private Button button;
  private bool isPressed;

  public void Start() => button = GetComponentsInParent<Button>(true)[0];

  public void Update()
  {
    if (!isPressed && SRInput.GetAction(action).WasPressed && IsButtonAvailable())
    {
      ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
      ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
      isPressed = true;
    }
    else if (isPressed && SRInput.GetAction(action).WasReleased && IsButtonAvailable())
    {
      ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
      ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
      ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
      isPressed = false;
    }
    else
    {
      if (!isPressed || IsButtonAvailable())
        return;
      ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
      isPressed = false;
    }
  }

  private bool IsButtonAvailable()
  {
    if (!button.IsInteractable() || !button.isActiveAndEnabled)
      return false;
    return !requiresCurrentSelection || EventSystem.current.currentSelectedGameObject == gameObject;
  }
}
