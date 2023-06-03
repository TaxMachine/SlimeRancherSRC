// Decompiled with JetBrains decompiler
// Type: FPControllerState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class FPControllerState : BaseState
{
  public float? MotorAcceleration;
  public float? MotorBackwardsSpeed;
  public float? MotorDamping;
  public float? MotorAirSpeed;
  public float? MotorSlopeSpeedUp;
  public float? MotorSlopeSpeedDown;
  public bool? MotorFreeFly;
  public float? MotorJumpForce;
  public float? MotorJumpForceDamping;
  public float? MotorJumpForceHold;
  public float? MotorJumpForceHoldDamping;
  public float? PhysicsForceDamping;
  public float? PhysicsPushForce;
  public float? PhysicsGravityModifier;
  public float? PhysicsSlopeSlideLimit;
  public float? PhysicsSlopeSlidiness;
  public float? PhysicsWallBounce;
  public float? PhysicsWallFriction;
  public bool? PhysicsHasCollisionTrigger;

  public FPControllerState(string name)
    : base(name)
  {
  }
}
