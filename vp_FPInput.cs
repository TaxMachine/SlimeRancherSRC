// Decompiled with JetBrains decompiler
// Type: vp_FPInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using System;
using System.Collections.Generic;
using UnityEngine;

public class vp_FPInput : vp_Component
{
  public Vector2 MouseLookSensitivity = new Vector2(5f, 5f);
  public int MouseLookSmoothSteps = 10;
  public float MouseLookSmoothWeight = 0.5f;
  public bool MouseLookAcceleration;
  public float MouseLookAccelerationThreshold = 0.4f;
  public bool MouseLookInvert;
  protected Vector2 m_MouseLookSmoothMove = Vector2.zero;
  protected Vector2 m_MouseLookRawMove = Vector2.zero;
  protected List<Vector2> m_MouseLookSmoothBuffer = new List<Vector2>();
  protected int m_LastMouseLookFrame = -1;
  protected Vector2 m_CurrentMouseLook = Vector2.zero;
  public Rect[] MouseCursorZones;
  public bool MouseCursorForced;
  public bool MouseCursorBlocksMouseLook = true;
  protected Vector2 m_MousePos = Vector2.zero;
  protected Vector2 m_MoveVector = Vector2.zero;
  protected bool m_AllowGameplayInput = true;
  protected vp_FPPlayerEventHandler m_FPPlayer;
  private OptionsDirector optionsDir;
  private InputDirector inputDir;

  public Vector2 MousePos => m_MousePos;

  public bool AllowGameplayInput
  {
    get => m_AllowGameplayInput;
    set => m_AllowGameplayInput = value;
  }

  public vp_FPPlayerEventHandler FPPlayer
  {
    get
    {
      if (m_FPPlayer == null)
        m_FPPlayer = transform.root.GetComponentInChildren<vp_FPPlayerEventHandler>();
      return m_FPPlayer;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    optionsDir = SRSingleton<GameContext>.Instance.OptionsDirector;
    inputDir = SRSingleton<GameContext>.Instance.InputDirector;
  }

  protected override void OnEnable()
  {
    if (!(FPPlayer != null))
      return;
    Register(FPPlayer);
  }

  protected override void OnDisable()
  {
    if (!(FPPlayer != null))
      return;
    Unregister(FPPlayer);
  }

  protected override void Update()
  {
    UpdateCursorLock();
    UpdatePause();
    if (FPPlayer.Pause.Get() || !m_AllowGameplayInput)
      return;
    InputInteract();
    InputMove();
    InputRun();
    InputJump();
    InputCrouch();
    InputAttack();
    InputReload();
    InputSetWeapon();
    InputCamera();
  }

  protected virtual void InputInteract()
  {
    if (SRInput.Actions.interact.WasReleased)
      FPPlayer.Interact.TryStart();
    else
      FPPlayer.Interact.TryStop();
  }

  protected virtual void InputMove()
  {
    Vector2 v = new Vector2((float) (OneAxisInputControl) SRInput.Actions.horizontal, (float) (OneAxisInputControl) SRInput.Actions.vertical);
    FPPlayer.InputMoveVector.Set(InputDirector.UsingGamepad() ? ApplyRadialDeadZone(v, inputDir.ControllerStickDeadZone) : v);
  }

  private Vector2 ApplyRadialDeadZone(Vector2 v, float deadZone)
  {
    float magnitude = v.magnitude;
    return magnitude >= (double) deadZone ? v.normalized * (float) ((magnitude - (double) deadZone) / (1.0 - deadZone)) : Vector2.zero;
  }

  protected virtual void InputRun()
  {
    if ((optionsDir.sprintHold ? (SRInput.Actions.run.IsPressed ? 1 : 0) : (FPPlayer.Run.Active ^ SRInput.Actions.run.WasPressed ? 1 : 0)) != 0)
      FPPlayer.Run.TryStart();
    else
      FPPlayer.Run.TryStop();
  }

  protected virtual void InputJump()
  {
    if (SRInput.Actions.jump.IsPressed)
    {
      if (FPPlayer.Jump.TryStart() || FPPlayer.Jump.Active)
        return;
      FPPlayer.Jetpack.TryStart();
    }
    else
    {
      FPPlayer.Jump.Stop();
      FPPlayer.Jetpack.Stop();
    }
  }

  protected virtual void InputCrouch()
  {
  }

  protected virtual void InputCamera()
  {
  }

  protected virtual void InputAttack()
  {
    if (!vp_Utility.LockCursor)
      return;
    if (SRInput.Actions.attack.IsPressed)
      FPPlayer.Attack.TryStart();
    else
      FPPlayer.Attack.TryStop();
  }

  protected virtual void InputReload()
  {
  }

  protected virtual void InputSetWeapon()
  {
  }

  protected virtual void UpdatePause()
  {
  }

  protected virtual void UpdateCursorLock()
  {
    m_MousePos.x = Input.mousePosition.x;
    m_MousePos.y = Screen.height - Input.mousePosition.y;
    if (MouseCursorForced)
    {
      if (!vp_Utility.LockCursor)
        return;
      vp_Utility.LockCursor = false;
    }
    else
    {
      if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        return;
      if (MouseCursorZones.Length != 0)
      {
        foreach (Rect mouseCursorZone in MouseCursorZones)
        {
          if (mouseCursorZone.Contains(m_MousePos))
          {
            if (!vp_Utility.LockCursor)
              return;
            vp_Utility.LockCursor = false;
            return;
          }
        }
      }
      if (vp_Utility.LockCursor)
        return;
      vp_Utility.LockCursor = true;
    }
  }

  protected virtual Vector2 GetMouseLook()
  {
    if (MouseCursorBlocksMouseLook && !vp_Utility.LockCursor)
      return Vector2.zero;
    if (m_LastMouseLookFrame == Time.frameCount)
      return m_CurrentMouseLook;
    m_LastMouseLookFrame = Time.frameCount;
    Vector2 mouseLook = SRInput.Instance.GetMouseLook();
    Vector2 vector2 = mouseLook;
    if (InputDirector.UsingGamepad())
    {
      vector2 = ApplyRadialDeadZone(mouseLook, inputDir.ControllerStickDeadZone);
      vector2.x *= inputDir.GetGamepadLookSensitivityXFactor();
      vector2.y *= inputDir.GetGamepadLookSensitivityYFactor();
    }
    m_MouseLookSmoothMove.x = vector2.x * Time.timeScale;
    m_MouseLookSmoothMove.y = vector2.y * Time.timeScale;
    MouseLookSmoothSteps = Mathf.Clamp(MouseLookSmoothSteps, 1, 20);
    float num1 = inputDir.GetDisableMouseLookSmooth() ? 0.0f : Mathf.Clamp01(MouseLookSmoothWeight) / Delta;
    while (m_MouseLookSmoothBuffer.Count > MouseLookSmoothSteps)
      m_MouseLookSmoothBuffer.RemoveAt(0);
    m_MouseLookSmoothBuffer.Add(m_MouseLookSmoothMove);
    float num2 = 1f;
    Vector2 zero = Vector2.zero;
    float b = 0.0f;
    for (int index = m_MouseLookSmoothBuffer.Count - 1; index > 0; --index)
    {
      zero += m_MouseLookSmoothBuffer[index] * num2;
      b += 1f * num2;
      num2 *= num1;
    }
    float num3 = Mathf.Max(1f, b);
    m_CurrentMouseLook = vp_MathUtility.NaNSafeVector2(zero / num3);
    float num4 = 0.0f;
    float num5 = Mathf.Abs(m_CurrentMouseLook.x);
    float num6 = Mathf.Abs(m_CurrentMouseLook.y);
    if (MouseLookAcceleration)
    {
      float num7 = Mathf.Sqrt((float) (num5 * (double) num5 + num6 * (double) num6)) / Delta;
      num4 = num7 <= (double) MouseLookAccelerationThreshold ? 0.0f : num7;
    }
    m_CurrentMouseLook.x *= MouseLookSensitivity.x + num4;
    m_CurrentMouseLook.y *= MouseLookSensitivity.y + num4;
    m_CurrentMouseLook.y = MouseLookInvert ? m_CurrentMouseLook.y : -m_CurrentMouseLook.y;
    return m_CurrentMouseLook;
  }

  protected virtual Vector2 GetMouseLookRaw()
  {
    if (MouseCursorBlocksMouseLook && !vp_Utility.LockCursor)
      return Vector2.zero;
    Vector2 vector2 = ApplyRadialDeadZone(SRInput.Instance.GetMouseLookRaw(), inputDir.ControllerStickDeadZone);
    m_MouseLookRawMove.x = vector2.x;
    m_MouseLookRawMove.y = vector2.y;
    return m_MouseLookRawMove;
  }

  protected virtual Vector2 Get_InputMoveVector() => m_MoveVector;

  protected virtual void Set_InputMoveVector(Vector2 value) => m_MoveVector = value.sqrMagnitude > 1.0 ? value.normalized : value;

  protected virtual Vector2 OnValue_InputMoveVector
  {
    get => m_MoveVector;
    set => m_MoveVector = value.sqrMagnitude > 1.0 ? value.normalized : value;
  }

  protected virtual float Get_InputClimbVector() => SRInput.Actions.vertical.RawValue;

  protected virtual float OnValue_InputClimbVector => SRInput.Actions.vertical.RawValue;

  protected virtual bool Get_InputAllowGameplay() => m_AllowGameplayInput;

  protected virtual void Set_InputAllowGameplay(bool value) => m_AllowGameplayInput = value;

  protected virtual bool OnValue_InputAllowGameplay
  {
    get => m_AllowGameplayInput;
    set => m_AllowGameplayInput = value;
  }

  protected virtual bool Get_Pause() => vp_TimeUtility.Paused;

  protected virtual void Set_Pause(bool value) => vp_TimeUtility.Paused = !vp_Gameplay.isMultiplayer && value;

  protected virtual bool OnValue_Pause
  {
    get => vp_TimeUtility.Paused;
    set => vp_TimeUtility.Paused = !vp_Gameplay.isMultiplayer && value;
  }

  protected virtual bool OnMessage_InputGetButton(string button) => throw new NotImplementedException();

  protected virtual bool OnMessage_InputGetButtonUp(string button) => throw new NotImplementedException();

  protected virtual bool OnMessage_InputGetButtonDown(string button) => throw new NotImplementedException();

  protected virtual Vector2 OnValue_InputSmoothLook => GetMouseLook();

  protected virtual Vector2 OnValue_InputRawLook => GetMouseLookRaw();

  public override void Register(vp_EventHandler eventHandler)
  {
    base.Register(eventHandler);
    eventHandler.RegisterMessage<string, bool>("InputGetButton", OnMessage_InputGetButton);
    eventHandler.RegisterMessage<string, bool>("InputGetButtonDown", OnMessage_InputGetButtonDown);
    eventHandler.RegisterMessage<string, bool>("InputGetButtonUp", OnMessage_InputGetButtonUp);
    eventHandler.RegisterValue("InputSmoothLook", GetMouseLook, null);
    eventHandler.RegisterValue("InputRawLook", GetMouseLookRaw, null);
    eventHandler.RegisterValue("InputAllowGameplay", Get_InputAllowGameplay, Set_InputAllowGameplay);
    eventHandler.RegisterValue("InputClimbVector", Get_InputClimbVector, null);
    eventHandler.RegisterValue("InputMoveVector", Get_InputMoveVector, Set_InputMoveVector);
    eventHandler.RegisterValue("Pause", Get_Pause, Set_Pause);
  }

  public override void Unregister(vp_EventHandler eventHandler)
  {
    base.Unregister(eventHandler);
    eventHandler.UnregisterMessage<string, bool>("InputGetButton", OnMessage_InputGetButton);
    eventHandler.UnregisterMessage<string, bool>("InputGetButtonDown", OnMessage_InputGetButtonDown);
    eventHandler.UnregisterMessage<string, bool>("InputGetButtonUp", OnMessage_InputGetButtonUp);
    eventHandler.UnregisterValue("InputSmoothLook", GetMouseLook, null);
    eventHandler.UnregisterValue("InputRawLook", GetMouseLookRaw, null);
    eventHandler.UnregisterValue("InputAllowGameplay", Get_InputAllowGameplay, Set_InputAllowGameplay);
    eventHandler.UnregisterValue("InputClimbVector", Get_InputClimbVector, null);
    eventHandler.UnregisterValue("InputMoveVector", Get_InputMoveVector, Set_InputMoveVector);
    eventHandler.UnregisterValue("Pause", Get_Pause, Set_Pause);
  }
}
