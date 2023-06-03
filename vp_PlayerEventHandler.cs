// Decompiled with JetBrains decompiler
// Type: vp_PlayerEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class vp_PlayerEventHandler : vp_StateEventHandler
{
  public vp_Value<bool> IsFirstPerson = new vp_Value<bool>(nameof (IsFirstPerson));
  public vp_Value<bool> IsLocal = new vp_Value<bool>(nameof (IsLocal));
  public vp_Value<bool> IsAI = new vp_Value<bool>(nameof (IsAI));
  public vp_Value<float> Health = new vp_Value<float>(nameof (Health));
  public vp_Value<float> MaxHealth = new vp_Value<float>(nameof (MaxHealth));
  public vp_Value<Vector3> Position = new vp_Value<Vector3>(nameof (Position));
  public vp_Value<Vector2> Rotation = new vp_Value<Vector2>(nameof (Rotation));
  public vp_Value<float> BodyYaw = new vp_Value<float>(nameof (BodyYaw));
  public vp_Value<Vector3> LookPoint = new vp_Value<Vector3>(nameof (LookPoint));
  public vp_Value<Vector3> HeadLookDirection = new vp_Value<Vector3>(nameof (HeadLookDirection));
  public vp_Value<Vector3> AimDirection = new vp_Value<Vector3>(nameof (AimDirection));
  public vp_Value<Vector3> MotorThrottle = new vp_Value<Vector3>(nameof (MotorThrottle));
  public vp_Value<bool> MotorJumpDone = new vp_Value<bool>(nameof (MotorJumpDone));
  public vp_Value<Vector2> InputMoveVector = new vp_Value<Vector2>(nameof (InputMoveVector));
  public vp_Value<float> InputClimbVector = new vp_Value<float>(nameof (InputClimbVector));
  public vp_Activity Dead = new vp_Activity(nameof (Dead));
  public vp_Activity Run = new vp_Activity(nameof (Run));
  public vp_Activity Jump = new vp_Activity(nameof (Jump));
  public vp_Activity Jetpack = new vp_Activity(nameof (Jetpack));
  public vp_Activity Crouch = new vp_Activity(nameof (Crouch));
  public vp_Activity Zoom = new vp_Activity(nameof (Zoom));
  public vp_Activity Attack = new vp_Activity(nameof (Attack));
  public vp_Activity Reload = new vp_Activity(nameof (Reload));
  public vp_Activity Climb = new vp_Activity(nameof (Climb));
  public vp_Activity Interact = new vp_Activity(nameof (Interact));
  public vp_Activity<int> SetWeapon = new vp_Activity<int>(nameof (SetWeapon));
  public vp_Activity OutOfControl = new vp_Activity(nameof (OutOfControl));
  public vp_Activity Underwater = new vp_Activity(nameof (Underwater));
  public vp_Message<int> Wield = new vp_Message<int>(nameof (Wield));
  public vp_Message Unwield = new vp_Message(nameof (Unwield));
  public vp_Attempt Fire = new vp_Attempt(nameof (Fire));
  public vp_Message DryFire = new vp_Message(nameof (DryFire));
  public vp_Attempt SetPrevWeapon = new vp_Attempt(nameof (SetPrevWeapon));
  public vp_Attempt SetNextWeapon = new vp_Attempt(nameof (SetNextWeapon));
  public vp_Attempt<string> SetWeaponByName = new vp_Attempt<string>(nameof (SetWeaponByName));
  [Obsolete("Please use the 'CurrentWeaponIndex' vp_Value instead.")]
  public vp_Value<int> CurrentWeaponID = new vp_Value<int>(nameof (CurrentWeaponID));
  public vp_Value<int> CurrentWeaponIndex = new vp_Value<int>(nameof (CurrentWeaponIndex));
  public vp_Value<string> CurrentWeaponName = new vp_Value<string>(nameof (CurrentWeaponName));
  public vp_Value<bool> CurrentWeaponWielded = new vp_Value<bool>(nameof (CurrentWeaponWielded));
  public vp_Attempt AutoReload = new vp_Attempt(nameof (AutoReload));
  public vp_Value<float> CurrentWeaponReloadDuration = new vp_Value<float>(nameof (CurrentWeaponReloadDuration));
  public vp_Message<string, int> GetItemCount = new vp_Message<string, int>(nameof (GetItemCount));
  public vp_Attempt RefillCurrentWeapon = new vp_Attempt(nameof (RefillCurrentWeapon));
  public vp_Value<int> CurrentWeaponAmmoCount = new vp_Value<int>(nameof (CurrentWeaponAmmoCount));
  public vp_Value<int> CurrentWeaponMaxAmmoCount = new vp_Value<int>(nameof (CurrentWeaponMaxAmmoCount));
  public vp_Value<int> CurrentWeaponClipCount = new vp_Value<int>(nameof (CurrentWeaponClipCount));
  public vp_Value<int> CurrentWeaponType = new vp_Value<int>(nameof (CurrentWeaponType));
  public vp_Value<int> CurrentWeaponGrip = new vp_Value<int>(nameof (CurrentWeaponGrip));
  public vp_Attempt<object> AddItem = new vp_Attempt<object>(nameof (AddItem));
  public vp_Attempt<object> RemoveItem = new vp_Attempt<object>(nameof (RemoveItem));
  public vp_Attempt DepleteAmmo = new vp_Attempt(nameof (DepleteAmmo));
  public vp_Message<Vector3> Move = new vp_Message<Vector3>(nameof (Move));
  public vp_Value<Vector3> Velocity = new vp_Value<Vector3>(nameof (Velocity));
  public vp_Value<float> SlopeLimit = new vp_Value<float>(nameof (SlopeLimit));
  public vp_Value<float> StepOffset = new vp_Value<float>(nameof (StepOffset));
  public vp_Value<float> Radius = new vp_Value<float>(nameof (Radius));
  public vp_Value<float> Height = new vp_Value<float>(nameof (Height));
  public vp_Value<float> FallSpeed = new vp_Value<float>(nameof (FallSpeed));
  public vp_Message<float> FallImpact = new vp_Message<float>(nameof (FallImpact));
  public vp_Message<float> HeadImpact = new vp_Message<float>(nameof (HeadImpact));
  public vp_Message Stop = new vp_Message(nameof (Stop));
  public vp_Value<Transform> Platform = new vp_Value<Transform>(nameof (Platform));
  public vp_Value<vp_Interactable> Interactable = new vp_Value<vp_Interactable>(nameof (Interactable));
  public vp_Value<bool> CanInteract = new vp_Value<bool>(nameof (CanInteract));
  public vp_Value<Texture> GroundTexture = new vp_Value<Texture>(nameof (GroundTexture));
  public vp_Value<vp_SurfaceIdentifier> SurfaceType = new vp_Value<vp_SurfaceIdentifier>(nameof (SurfaceType));

  public vp_PlayerEventHandler() => AddHandledEvents();

  private bool GetTrue() => true;

  protected override void Awake()
  {
    base.Awake();
    BindStateToActivity(Run);
    BindStateToActivity(Jump);
    BindStateToActivity(Crouch);
    BindStateToActivity(Zoom);
    BindStateToActivity(Reload);
    BindStateToActivity(Dead);
    BindStateToActivity(Climb);
    BindStateToActivity(OutOfControl);
    BindStateToActivityOnStart(Attack);
    BindStateToActivity(Underwater);
    SetWeapon.AutoDuration = 1f;
    Reload.AutoDuration = 1f;
    Zoom.MinDuration = 0.2f;
    Crouch.MinDuration = 0.5f;
    Jump.MinPause = 0.0f;
    SetWeapon.MinPause = 0.2f;
  }

  private void AddHandledEvents()
  {
    m_HandlerEvents.Add("IsFirstPerson", IsFirstPerson);
    m_HandlerEvents.Add("IsLocal", IsLocal);
    m_HandlerEvents.Add("IsAI", IsAI);
    m_HandlerEvents.Add("Health", Health);
    m_HandlerEvents.Add("MaxHealth", MaxHealth);
    m_HandlerEvents.Add("Position", Position);
    m_HandlerEvents.Add("Rotation", Rotation);
    m_HandlerEvents.Add("BodyYaw", BodyYaw);
    m_HandlerEvents.Add("LookPoint", LookPoint);
    m_HandlerEvents.Add("HeadLookDirection", HeadLookDirection);
    m_HandlerEvents.Add("AimDirection", AimDirection);
    m_HandlerEvents.Add("MotorThrottle", MotorThrottle);
    m_HandlerEvents.Add("MotorJumpDone", MotorJumpDone);
    m_HandlerEvents.Add("InputMoveVector", InputMoveVector);
    m_HandlerEvents.Add("InputClimbVector", InputClimbVector);
    m_HandlerEvents.Add("Dead", Dead);
    m_HandlerEvents.Add("Run", Run);
    m_HandlerEvents.Add("Jump", Jump);
    m_HandlerEvents.Add("Jetpack", Jetpack);
    m_HandlerEvents.Add("Crouch", Crouch);
    m_HandlerEvents.Add("Zoom", Zoom);
    m_HandlerEvents.Add("Attack", Attack);
    m_HandlerEvents.Add("Reload", Reload);
    m_HandlerEvents.Add("Climb", Climb);
    m_HandlerEvents.Add("Interact", Interact);
    m_HandlerEvents.Add("SetWeapon", SetWeapon);
    m_HandlerEvents.Add("OutOfControl", OutOfControl);
    m_HandlerEvents.Add("Underwater", Underwater);
    m_HandlerEvents.Add("Wield", Wield);
    m_HandlerEvents.Add("Unwield", Unwield);
    m_HandlerEvents.Add("Fire", Fire);
    m_HandlerEvents.Add("DryFire", DryFire);
    m_HandlerEvents.Add("SetPrevWeapon", SetPrevWeapon);
    m_HandlerEvents.Add("SetNextWeapon", SetNextWeapon);
    m_HandlerEvents.Add("SetWeaponByName", SetWeaponByName);
    m_HandlerEvents.Add("CurrentWeaponID", CurrentWeaponID);
    m_HandlerEvents.Add("CurrentWeaponIndex", CurrentWeaponIndex);
    m_HandlerEvents.Add("CurrentWeaponName", CurrentWeaponName);
    m_HandlerEvents.Add("CurrentWeaponWielded", CurrentWeaponWielded);
    m_HandlerEvents.Add("AutoReload", AutoReload);
    m_HandlerEvents.Add("CurrentWeaponReloadDuration", CurrentWeaponReloadDuration);
    m_HandlerEvents.Add("GetItemCount", GetItemCount);
    m_HandlerEvents.Add("RefillCurrentWeapon", RefillCurrentWeapon);
    m_HandlerEvents.Add("CurrentWeaponAmmoCount", CurrentWeaponAmmoCount);
    m_HandlerEvents.Add("CurrentWeaponMaxAmmoCount", CurrentWeaponMaxAmmoCount);
    m_HandlerEvents.Add("CurrentWeaponClipCount", CurrentWeaponClipCount);
    m_HandlerEvents.Add("CurrentWeaponType", CurrentWeaponType);
    m_HandlerEvents.Add("CurrentWeaponGrip", CurrentWeaponGrip);
    m_HandlerEvents.Add("AddItem", AddItem);
    m_HandlerEvents.Add("RemoveItem", RemoveItem);
    m_HandlerEvents.Add("DepleteAmmo", DepleteAmmo);
    m_HandlerEvents.Add("Move", Move);
    m_HandlerEvents.Add("Velocity", Velocity);
    m_HandlerEvents.Add("SlopeLimit", SlopeLimit);
    m_HandlerEvents.Add("StepOffset", StepOffset);
    m_HandlerEvents.Add("Radius", Radius);
    m_HandlerEvents.Add("Height", Height);
    m_HandlerEvents.Add("FallSpeed", FallSpeed);
    m_HandlerEvents.Add("FallImpact", FallImpact);
    m_HandlerEvents.Add("HeadImpact", HeadImpact);
    m_HandlerEvents.Add("Stop", Stop);
    m_HandlerEvents.Add("Platform", Platform);
    m_HandlerEvents.Add("Interactable", Interactable);
    m_HandlerEvents.Add("CanInteract", CanInteract);
    m_HandlerEvents.Add("GroundTexture", GroundTexture);
    m_HandlerEvents.Add("SurfaceType", SurfaceType);
    IsFirstPerson.Get = GetTrue;
  }
}
