// Decompiled with JetBrains decompiler
// Type: FPWeaponState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FPWeaponState : BaseState
{
  public float? RenderingZoomDamping;
  public int? RenderingFieldOfView;
  public Vector2? RenderingClippingPlanes;
  public int? RenderingZScale;
  public Vector3? PositionOffset;
  public float? PositionSpringStiffness;
  public float? PositionSpringDamping;
  public int? PositionFallRetract;
  public float? PositionPivotSpringStiffness;
  public float? PositionPivotSpringDamping;
  public float? PositionKneeling;
  public int? PositionKneelingSoftness;
  public Vector3? PositionWalkSlide;
  public Vector3? PositionPivot;
  public Vector3? RotationPivot;
  public float? PositionInputVelocityScale;
  public int? PositionMaxInputVelocity;
  public float? RotationSpringStiffness;
  public float? RotationSpringDamping;
  public float? RotationPivotSpringStiffness;
  public float? RotationPivotSpringDamping;
  public int? RotationKneeling;
  public int? RotationKneelingSoftness;
  public Vector3? RotationLookSway;
  public Vector3? RotationStrafeSway;
  public Vector3? RotationFallSway;
  public float? RotationSlopeSway;
  public float? PositionSpring2Stiffness;
  public float? PositionSpring2Damping;
  public Vector3? RotationOffset;
  public float? RotationSpring2Stiffness;
  public float? RotationSpring2Damping;
  public int? RotationInputVelocityScale;
  public int? RotationMaxInputVelocity;
  public float? RetractionDistance;
  public Vector2? RetractionOffset;
  public float? RetractionRelaxSpeed;
  public float? ShakeSpeed;
  public Vector3? ShakeAmplitude;
  public Vector4? BobRate;
  public Vector4? BobAmplitude;
  public float? BobInputVelocityScale;
  public int? BobMaxInputVelocity;
  public bool? BobRequireGroundContact;
  public Vector3? StepPositionForce;
  public Vector3? StepRotationForce;
  public int? StepSoftness;
  public int? StepMinVelocity;
  public int? StepPositionBalance;
  public int? StepRotationBalance;
  public float? StepForceScale;
  public Vector2? AmbientInterval;
  public Vector3? PositionExitOffset;
  public Vector3? RotationExitOffset;
  public bool? LookDownActive;
  public float? LookDownYawLimit;
  public Vector3? LookDownPositionOffsetMiddle;
  public Vector3? LookDownPositionOffsetLeft;
  public Vector3? LookDownPositionOffsetRight;
  public float? LookDownPositionSpringPower;
  public Vector3? LookDownRotationOffsetMiddle;
  public Vector3? LookDownRotationOffsetLeft;
  public Vector3? LookDownRotationOffsetRight;
  public float? LookDownRotationSpringPower;
  public int? AnimationType;
  public int? AnimationGrip;
  public bool? Persist;

  public FPWeaponState(string name)
    : base(name)
  {
  }
}
