// Decompiled with JetBrains decompiler
// Type: FPControllerStateManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class FPControllerStateManager : BaseStateManager<FPControllerState, vp_FPController>
{
  public FPControllerStateManager(vp_FPController managedComponent)
    : base(managedComponent)
  {
    CreateStates();
  }

  private void CreateStates()
  {
    states = new FPControllerState[9];
    AddState(new FPControllerState("Dead")
    {
      PhysicsForceDamping = new float?(0.07f),
      PhysicsGravityModifier = new float?(0.06f),
      PhysicsWallBounce = new float?(1f)
    }, 0);
    AddState(new FPControllerState("Freeze")
    {
      MotorAcceleration = new float?(0.0f),
      MotorJumpForce = new float?(0.0f),
      MotorAirSpeed = new float?(0.0f),
      MotorSlopeSpeedUp = new float?(0.0f),
      MotorSlopeSpeedDown = new float?(0.0f),
      PhysicsForceDamping = new float?(1f),
      PhysicsPushForce = new float?(0.0f),
      PhysicsGravityModifier = new float?(0.0f),
      PhysicsWallBounce = new float?(0.0f)
    }, 1);
    AddState(new FPControllerState("Zoom")
    {
      MotorDamping = new float?(0.17f),
      MotorAcceleration = new float?(0.18f)
    }, 2);
    AddState(new FPControllerState("Crouch")
    {
      MotorAcceleration = new float?(0.084f),
      MotorDamping = new float?(0.35f),
      MotorJumpForce = new float?(0.0f),
      MotorAirSpeed = new float?(0.0f),
      PhysicsForceDamping = new float?(0.05f),
      PhysicsPushForce = new float?(5f),
      PhysicsGravityModifier = new float?(0.2f),
      PhysicsWallBounce = new float?(0.0f)
    }, 3);
    AddState(new FPControllerState("Run")
    {
      MotorAcceleration = new float?(0.45f),
      MotorAirSpeed = new float?(0.9f)
    }, 4);
    AddState(new FPControllerState("Jetpack1")
    {
      PhysicsGravityModifier = new float?(-0.06f)
    }, 5);
    AddState(new FPControllerState("Jetpack2")
    {
      PhysicsGravityModifier = new float?(-0.06f)
    }, 6);
    AddState(new FPControllerState("Underwater")
    {
      MotorDamping = new float?(0.35f),
      PhysicsGravityModifier = new float?(0.1f),
      MotorJumpForce = new float?(0.09f),
      MotorJumpForceDamping = new float?(0.05f),
      MotorJumpForceHold = new float?(0.0015f)
    }, 7);
    AddState(new FPControllerState("Default")
    {
      MotorAcceleration = new float?(0.25f),
      MotorDamping = new float?(0.15f),
      MotorBackwardsSpeed = new float?(0.65f),
      MotorAirSpeed = new float?(0.9f),
      MotorSlopeSpeedUp = new float?(1f),
      MotorSlopeSpeedDown = new float?(1f),
      MotorJumpForce = new float?(0.18f),
      MotorJumpForceDamping = new float?(0.08f),
      MotorJumpForceHold = new float?(3f / 1000f),
      MotorJumpForceHoldDamping = new float?(0.5f),
      PhysicsForceDamping = new float?(0.2f),
      PhysicsPushForce = new float?(5f),
      PhysicsGravityModifier = new float?(0.2f),
      PhysicsSlopeSlideLimit = new float?(60f),
      PhysicsSlopeSlidiness = new float?(0.15f),
      PhysicsWallBounce = new float?(0.0f),
      PhysicsWallFriction = new float?(0.0f),
      PhysicsHasCollisionTrigger = new bool?(true)
    }, 8);
  }

  public override void ApplyState(FPControllerState state)
  {
    if (state.MotorAcceleration.HasValue)
      managedComponent.MotorAcceleration = state.MotorAcceleration.Value;
    if (state.MotorBackwardsSpeed.HasValue)
      managedComponent.MotorBackwardsSpeed = state.MotorBackwardsSpeed.Value;
    if (state.MotorDamping.HasValue)
      managedComponent.MotorDamping = state.MotorDamping.Value;
    if (state.MotorAirSpeed.HasValue)
      managedComponent.MotorAirSpeed = state.MotorAirSpeed.Value;
    if (state.MotorSlopeSpeedUp.HasValue)
      managedComponent.MotorSlopeSpeedUp = state.MotorSlopeSpeedUp.Value;
    if (state.MotorSlopeSpeedDown.HasValue)
      managedComponent.MotorSlopeSpeedDown = state.MotorSlopeSpeedDown.Value;
    if (state.MotorJumpForce.HasValue)
      managedComponent.MotorJumpForce = state.MotorJumpForce.Value;
    if (state.MotorJumpForceDamping.HasValue)
      managedComponent.MotorJumpForceDamping = state.MotorJumpForceDamping.Value;
    if (state.MotorJumpForceHold.HasValue)
      managedComponent.MotorJumpForceHold = state.MotorJumpForceHold.Value;
    if (state.MotorJumpForceHoldDamping.HasValue)
      managedComponent.MotorJumpForceHoldDamping = state.MotorJumpForceHoldDamping.Value;
    if (state.PhysicsForceDamping.HasValue)
      managedComponent.PhysicsForceDamping = state.PhysicsForceDamping.Value;
    if (state.PhysicsPushForce.HasValue)
      managedComponent.PhysicsPushForce = state.PhysicsPushForce.Value;
    if (state.PhysicsGravityModifier.HasValue)
      managedComponent.PhysicsGravityModifier = state.PhysicsGravityModifier.Value;
    if (state.PhysicsSlopeSlideLimit.HasValue)
      managedComponent.PhysicsSlopeSlideLimit = state.PhysicsSlopeSlideLimit.Value;
    if (state.PhysicsSlopeSlidiness.HasValue)
      managedComponent.PhysicsSlopeSlidiness = state.PhysicsSlopeSlidiness.Value;
    if (state.PhysicsWallBounce.HasValue)
      managedComponent.PhysicsWallBounce = state.PhysicsWallBounce.Value;
    if (state.PhysicsWallFriction.HasValue)
      managedComponent.PhysicsWallFriction = state.PhysicsWallFriction.Value;
    if (!state.PhysicsHasCollisionTrigger.HasValue)
      return;
    managedComponent.PhysicsHasCollisionTrigger = state.PhysicsHasCollisionTrigger.Value;
  }
}
