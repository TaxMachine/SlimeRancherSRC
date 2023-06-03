// Decompiled with JetBrains decompiler
// Type: SRStandaloneInputModule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SRStandaloneInputModule : PointerInputModule
{
  private float m_NextAction;
  private Vector2 m_LastMousePosition;
  private Vector2 m_MousePosition;
  [SerializeField]
  private float m_InputActionsPerSecond = 10f;
  [SerializeField]
  private bool m_AllowActivationOnMobileDevice;
  private bool lastNavigatedViaButtons;
  private GameObject lastSelectedViaButtons;
  private bool m_ProcessMouseEvents = true;
  [SerializeField]
  private float m_repeatDelay = 0.5f;
  private int m_ConsecutiveMovementCount;
  private Vector2 m_LastMoveVector;
  private float m_PrevActionTime;

  protected SRStandaloneInputModule()
  {
  }

  public bool allowActivationOnMobileDevice
  {
    get => m_AllowActivationOnMobileDevice;
    set => m_AllowActivationOnMobileDevice = value;
  }

  public bool processMouseEvents
  {
    get => m_ProcessMouseEvents;
    set => m_ProcessMouseEvents = value;
  }

  public float inputActionsPerSecond
  {
    get => m_InputActionsPerSecond;
    set => m_InputActionsPerSecond = value;
  }

  public float repeatDelay
  {
    get => m_repeatDelay;
    set => m_repeatDelay = value;
  }

  public override void UpdateModule()
  {
    m_LastMousePosition = m_MousePosition;
    m_MousePosition = Input.mousePosition;
    if (eventSystem.currentSelectedGameObject == null && lastNavigatedViaButtons)
    {
      if (lastSelectedViaButtons == null || !lastSelectedViaButtons.activeInHierarchy)
      {
        InitSelected current = InitSelected.Current;
        if (!(current != null))
          return;
        eventSystem.SetSelectedGameObject(current.gameObject, GetBaseEventData());
      }
      else
        eventSystem.SetSelectedGameObject(lastSelectedViaButtons, GetBaseEventData());
    }
    else
    {
      if (!(eventSystem.currentSelectedGameObject != null) || !(eventSystem.currentSelectedGameObject.GetComponent<InputField>() == null) || lastNavigatedViaButtons)
        return;
      lastSelectedViaButtons = eventSystem.currentSelectedGameObject;
      eventSystem.SetSelectedGameObject(null, GetBaseEventData());
    }
  }

  public override bool IsModuleSupported() => m_AllowActivationOnMobileDevice || Input.mousePresent;

  public override bool ShouldActivateModule() => base.ShouldActivateModule() && SRInput.PauseActions.submit.WasReleased | SRInput.PauseActions.cancel.WasReleased | SRInput.PauseActions.menuUp.WasReleased | SRInput.PauseActions.menuDown.WasReleased | SRInput.PauseActions.menuLeft.WasReleased | SRInput.PauseActions.menuRight.WasReleased | (m_MousePosition - m_LastMousePosition).sqrMagnitude > 0.0 | Input.GetMouseButtonDown(0);

  public override void ActivateModule()
  {
    base.ActivateModule();
    m_MousePosition = Input.mousePosition;
    m_LastMousePosition = Input.mousePosition;
    lastNavigatedViaButtons = InputDirector.UsingGamepad();
    if (!lastNavigatedViaButtons)
      return;
    GameObject selectedGameObject = eventSystem.currentSelectedGameObject;
    if (selectedGameObject == null)
      selectedGameObject = eventSystem.firstSelectedGameObject;
    eventSystem.SetSelectedGameObject(selectedGameObject, GetBaseEventData());
  }

  public override void DeactivateModule()
  {
    base.DeactivateModule();
    ClearSelection();
  }

  public override void Process()
  {
    bool selectedObject = SendUpdateEventToSelectedObject();
    if (eventSystem.sendNavigationEvents)
    {
      if (!selectedObject)
        selectedObject |= SendMoveEventToSelectedObject();
      if (!selectedObject)
        SendSubmitEventToSelectedObject();
    }
    if (!m_ProcessMouseEvents || !Cursor.visible)
      return;
    ProcessMouseEvent();
  }

  private bool SendSubmitEventToSelectedObject()
  {
    if (eventSystem.currentSelectedGameObject == null)
      return false;
    BaseEventData baseEventData = GetBaseEventData();
    if (SRInput.PauseActions.submit.WasReleased)
      ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
    if (SRInput.PauseActions.cancel.WasReleased)
      ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
    return baseEventData.used;
  }

  private bool SendMoveEventToSelectedObject()
  {
    float unscaledTime = Time.unscaledTime;
    Vector2 lhs = new Vector2(SRInput.PauseActions.menuRight.RawValue - SRInput.PauseActions.menuLeft.RawValue, SRInput.PauseActions.menuUp.RawValue - SRInput.PauseActions.menuDown.RawValue);
    if (Mathf.Approximately(lhs.x, 0.0f) && Mathf.Approximately(lhs.y, 0.0f))
    {
      m_ConsecutiveMovementCount = 0;
      return false;
    }
    bool flag = Vector2.Dot(lhs, m_LastMoveVector) > 0.0;
    if (flag && m_ConsecutiveMovementCount == 1)
    {
      if (unscaledTime <= m_PrevActionTime + (double) m_repeatDelay)
        return false;
    }
    else if (unscaledTime <= m_PrevActionTime + 1.0 / m_InputActionsPerSecond)
      return false;
    AxisEventData axisEventData = GetAxisEventData(lhs.x, lhs.y, 0.5f);
    if (lhs.sqrMagnitude > 0.25)
    {
      ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
      if (!flag)
        m_ConsecutiveMovementCount = 0;
      ++m_ConsecutiveMovementCount;
      m_PrevActionTime = unscaledTime;
      m_LastMoveVector = lhs;
      lastNavigatedViaButtons = true;
      if (axisEventData.moveDir == MoveDirection.None)
        m_ConsecutiveMovementCount = 0;
      return true;
    }
    if (axisEventData.moveDir == MoveDirection.None)
      m_ConsecutiveMovementCount = 0;
    return false;
  }

  protected void ProcessMouseEvent() => ProcessMouseEvent(0);

  private void ProcessMouseEvent(int id)
  {
    MouseState pointerEventData = GetMousePointerEventData(id);
    int num1 = pointerEventData.AnyPressesThisFrame() ? 1 : 0;
    bool flag = pointerEventData.AnyReleasesThisFrame();
    MouseButtonEventData eventData = pointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;
    int num2 = flag ? 1 : 0;
    PointerEventData buttonData = eventData.buttonData;
    if (!UseMouse(num1 != 0, num2 != 0, buttonData))
      return;
    if (Cursor.visible)
      lastNavigatedViaButtons = false;
    ProcessMousePress(eventData);
    ProcessMove(eventData.buttonData);
    ProcessDrag(eventData.buttonData);
    ProcessMousePress(pointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData);
    ProcessDrag(pointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData);
    ProcessMousePress(pointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData);
    ProcessDrag(pointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData.buttonData);
    if (Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0.0f))
      return;
    ExecuteEvents.ExecuteHierarchy(ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject), eventData.buttonData, ExecuteEvents.scrollHandler);
  }

  private static bool UseMouse(bool pressed, bool released, PointerEventData pointerData) => pressed | released || pointerData.IsPointerMoving() || pointerData.IsScrolling();

  private bool SendUpdateEventToSelectedObject()
  {
    if (eventSystem.currentSelectedGameObject == null)
      return false;
    BaseEventData baseEventData = GetBaseEventData();
    ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
    return baseEventData.used;
  }

  private void ProcessMousePress(MouseButtonEventData data)
  {
    PointerEventData buttonData = data.buttonData;
    GameObject gameObject1 = buttonData.pointerCurrentRaycast.gameObject;
    if (data.PressedThisFrame())
    {
      buttonData.eligibleForClick = true;
      buttonData.delta = Vector2.zero;
      buttonData.dragging = false;
      buttonData.useDragThreshold = true;
      buttonData.pressPosition = buttonData.position;
      buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
      DeselectIfSelectionChanged(gameObject1, buttonData);
      GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy(gameObject1, buttonData, ExecuteEvents.pointerDownHandler);
      if (gameObject2 == null)
        gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject1);
      float unscaledTime = Time.unscaledTime;
      if (gameObject2 == buttonData.lastPress)
      {
        if (unscaledTime - (double) buttonData.clickTime < 0.30000001192092896)
          ++buttonData.clickCount;
        else
          buttonData.clickCount = 1;
        buttonData.clickTime = unscaledTime;
      }
      else
        buttonData.clickCount = 1;
      buttonData.pointerPress = gameObject2;
      buttonData.rawPointerPress = gameObject1;
      buttonData.clickTime = unscaledTime;
      buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject1);
      if (buttonData.pointerDrag != null)
        ExecuteEvents.Execute(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
    }
    if (!data.ReleasedThisFrame())
      return;
    ExecuteEvents.Execute(buttonData.pointerPress, buttonData, ExecuteEvents.pointerUpHandler);
    GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject1);
    if (buttonData.pointerPress == eventHandler && buttonData.eligibleForClick)
      ExecuteEvents.Execute(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
    else if (buttonData.pointerDrag != null)
      ExecuteEvents.ExecuteHierarchy(gameObject1, buttonData, ExecuteEvents.dropHandler);
    buttonData.eligibleForClick = false;
    buttonData.pointerPress = null;
    buttonData.rawPointerPress = null;
    if (buttonData.pointerDrag != null && buttonData.dragging)
      ExecuteEvents.Execute(buttonData.pointerDrag, buttonData, ExecuteEvents.endDragHandler);
    buttonData.dragging = false;
    buttonData.pointerDrag = null;
    if (!(gameObject1 != buttonData.pointerEnter))
      return;
    HandlePointerExitAndEnter(buttonData, null);
    HandlePointerExitAndEnter(buttonData, gameObject1);
  }
}
