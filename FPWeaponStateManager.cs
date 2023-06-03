// Decompiled with JetBrains decompiler
// Type: FPWeaponStateManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FPWeaponStateManager : BaseStateManager<FPWeaponState, vp_FPWeapon>
{
  public FPWeaponStateManager(vp_FPWeapon managedComponent)
    : base(managedComponent)
  {
    CreateStates();
  }

  private void CreateStates()
  {
    states = new FPWeaponState[4];
    AddState(new FPWeaponState("Zoom")
    {
      ShakeSpeed = new float?(0.05f),
      ShakeAmplitude = new Vector3?(new Vector3(0.5f, 0.0f, 0.0f)),
      PositionOffset = new Vector3?(new Vector3(0.0f, -0.25f, 0.17f)),
      RotationOffset = new Vector3?(new Vector3(0.4917541f, 0.015994f, 0.0f)),
      PositionSpringStiffness = new float?(0.055f),
      PositionSpringDamping = new float?(0.45f),
      RotationSpringStiffness = new float?(0.025f),
      RotationSpringDamping = new float?(0.35f),
      RenderingFieldOfView = new int?(35),
      RenderingZoomDamping = new float?(0.2f),
      BobAmplitude = new Vector4?(new Vector4(0.5f, 0.4f, 0.2f, 0.005f)),
      BobRate = new Vector4?(new Vector4(0.8f, -0.4f, 0.4f, 0.4f)),
      BobInputVelocityScale = new float?(15f),
      PositionWalkSlide = new Vector3?(new Vector3(0.2f, 0.5f, 0.2f))
    }, 0);
    AddState(new FPWeaponState("Attack"), 1);
    AddState(new FPWeaponState("Run")
    {
      BobAmplitude = new Vector4?(new Vector4(1.5f, 1.2f, 0.6f, 0.015f))
    }, 2);
    AddState(new FPWeaponState("Default")
    {
      RenderingZoomDamping = new float?(0.5f),
      RenderingZScale = new int?(1),
      PositionSpringStiffness = new float?(0.01f),
      PositionSpringDamping = new float?(0.25f),
      PositionFallRetract = new int?(1),
      PositionPivotSpringStiffness = new float?(0.1f),
      PositionPivotSpringDamping = new float?(0.5f),
      PositionKneeling = new float?(0.06f),
      PositionKneelingSoftness = new int?(1),
      PositionWalkSlide = new Vector3?(new Vector3(1f, 1f, 1f)),
      PositionPivot = new Vector3?(new Vector3(0.0f, 0.0f, -0.2741375f)),
      RotationPivot = new Vector3?(new Vector3(0.0f, 0.0f, 0.0f)),
      PositionInputVelocityScale = new float?(0.5f),
      PositionMaxInputVelocity = new int?(1),
      RotationSpringStiffness = new float?(0.01f),
      RotationSpringDamping = new float?(0.25f),
      RotationPivotSpringStiffness = new float?(0.01f),
      RotationPivotSpringDamping = new float?(0.25f),
      RotationKneeling = new int?(0),
      RotationKneelingSoftness = new int?(1),
      RotationLookSway = new Vector3?(new Vector3(1f, 1f, 0.0f)),
      RotationStrafeSway = new Vector3?(new Vector3(1f, 1f, -1f)),
      RotationFallSway = new Vector3?(new Vector3(2f, 0.0f, 0.0f)),
      RotationSlopeSway = new float?(0.75f),
      RotationInputVelocityScale = new int?(1),
      RotationMaxInputVelocity = new int?(5),
      RetractionDistance = new float?(0.4f),
      RetractionOffset = new Vector2?(new Vector2(0.0f, 0.0f)),
      RetractionRelaxSpeed = new float?(0.25f),
      ShakeSpeed = new float?(0.1f),
      ShakeAmplitude = new Vector3?(new Vector3(0.1f, 0.0f, 0.3f)),
      BobRate = new Vector4?(new Vector4(1.4f, 0.7f, 0.7f, 0.7f)),
      BobAmplitude = new Vector4?(new Vector4(0.5f, 0.4f, 0.2f, 0.005f)),
      BobInputVelocityScale = new float?(3.5f),
      BobMaxInputVelocity = new int?(250),
      BobRequireGroundContact = new bool?(true),
      StepPositionForce = new Vector3?(new Vector3(0.0f, 0.015f, 0.0f)),
      StepRotationForce = new Vector3?(new Vector3(1.5f, 0.0f, 0.0f)),
      StepSoftness = new int?(5),
      StepMinVelocity = new int?(2),
      StepPositionBalance = new int?(0),
      StepRotationBalance = new int?(0),
      StepForceScale = new float?(0.05f),
      LookDownActive = new bool?(false),
      LookDownYawLimit = new float?(60f),
      LookDownPositionOffsetMiddle = new Vector3?(new Vector3(0.35f, -0.37f, 0.78f)),
      LookDownPositionOffsetLeft = new Vector3?(new Vector3(0.27f, -0.31f, 0.7f)),
      LookDownPositionOffsetRight = new Vector3?(new Vector3(0.6f, -0.41f, 0.86f)),
      LookDownPositionSpringPower = new float?(1f),
      LookDownRotationOffsetMiddle = new Vector3?(new Vector3(-3.9f, 2.24f, 4.69f)),
      LookDownRotationOffsetLeft = new Vector3?(new Vector3(-7f, -10.5f, 15.6f)),
      LookDownRotationOffsetRight = new Vector3?(new Vector3(-9.2f, -9.8f, 48.84f)),
      LookDownRotationSpringPower = new float?(1f),
      AmbientInterval = new Vector2?(new Vector2(0.0f, 0.0f)),
      PositionExitOffset = new Vector3?(new Vector3(0.0f, -1f, 0.0f)),
      PositionOffset = new Vector3?(new Vector3(0.1493672f, -0.83f, -0.75f)),
      PositionSpring2Stiffness = new float?(0.2f),
      PositionSpring2Damping = new float?(0.5615942f),
      RotationExitOffset = new Vector3?(new Vector3(40f, 0.0f, 0.0f)),
      RotationOffset = new Vector3?(new Vector3(-3.158258f, -5f, 0.0f)),
      RotationSpring2Stiffness = new float?(0.85f),
      RotationSpring2Damping = new float?(0.6f),
      AnimationType = new int?(1),
      AnimationGrip = new int?(1),
      Persist = new bool?(false)
    }, 3);
  }

  public override void ApplyState(FPWeaponState state)
  {
    if (state.RenderingZoomDamping.HasValue)
      managedComponent.RenderingZoomDamping = state.RenderingZoomDamping.Value;
    if (state.RenderingZScale.HasValue)
      managedComponent.RenderingZScale = state.RenderingZScale.Value;
    if (state.PositionOffset.HasValue)
      managedComponent.PositionOffset = state.PositionOffset.Value;
    if (state.PositionSpringStiffness.HasValue)
      managedComponent.PositionSpringStiffness = state.PositionSpringStiffness.Value;
    if (state.PositionSpringDamping.HasValue)
      managedComponent.PositionSpringDamping = state.PositionSpringDamping.Value;
    if (state.PositionFallRetract.HasValue)
      managedComponent.PositionFallRetract = state.PositionFallRetract.Value;
    if (state.PositionPivotSpringStiffness.HasValue)
      managedComponent.PositionPivotSpringStiffness = state.PositionPivotSpringStiffness.Value;
    if (state.PositionPivotSpringDamping.HasValue)
      managedComponent.PositionPivotSpringDamping = state.PositionPivotSpringDamping.Value;
    if (state.PositionSpring2Stiffness.HasValue)
      managedComponent.PositionSpring2Stiffness = state.PositionSpring2Stiffness.Value;
    if (state.PositionSpring2Damping.HasValue)
      managedComponent.PositionSpring2Damping = state.PositionSpring2Damping.Value;
    if (state.PositionKneeling.HasValue)
      managedComponent.PositionKneeling = state.PositionKneeling.Value;
    if (state.PositionKneelingSoftness.HasValue)
      managedComponent.PositionKneelingSoftness = state.PositionKneelingSoftness.Value;
    if (state.PositionWalkSlide.HasValue)
      managedComponent.PositionWalkSlide = state.PositionWalkSlide.Value;
    if (state.PositionPivot.HasValue)
      managedComponent.PositionPivot = state.PositionPivot.Value;
    if (state.RotationPivot.HasValue)
      managedComponent.RotationPivot = state.RotationPivot.Value;
    if (state.PositionInputVelocityScale.HasValue)
      managedComponent.PositionInputVelocityScale = state.PositionInputVelocityScale.Value;
    if (state.PositionMaxInputVelocity.HasValue)
      managedComponent.PositionMaxInputVelocity = state.PositionMaxInputVelocity.Value;
    if (state.RotationOffset.HasValue)
      managedComponent.RotationOffset = state.RotationOffset.Value;
    if (state.RotationSpringStiffness.HasValue)
      managedComponent.RotationSpringStiffness = state.RotationSpringStiffness.Value;
    if (state.RotationSpringDamping.HasValue)
      managedComponent.RotationSpringDamping = state.RotationSpringDamping.Value;
    if (state.RotationPivotSpringStiffness.HasValue)
      managedComponent.RotationPivotSpringStiffness = state.RotationPivotSpringStiffness.Value;
    if (state.RotationPivotSpringDamping.HasValue)
      managedComponent.RotationPivotSpringDamping = state.RotationPivotSpringDamping.Value;
    if (state.RotationSpring2Stiffness.HasValue)
      managedComponent.RotationSpring2Stiffness = state.RotationSpring2Stiffness.Value;
    if (state.RotationSpring2Damping.HasValue)
      managedComponent.RotationSpring2Damping = state.RotationSpring2Damping.Value;
    if (state.RotationKneeling.HasValue)
      managedComponent.RotationKneeling = state.RotationKneeling.Value;
    if (state.RotationKneelingSoftness.HasValue)
      managedComponent.RotationKneelingSoftness = state.RotationKneelingSoftness.Value;
    if (state.RotationLookSway.HasValue)
      managedComponent.RotationLookSway = state.RotationLookSway.Value;
    if (state.RotationStrafeSway.HasValue)
      managedComponent.RotationStrafeSway = state.RotationStrafeSway.Value;
    if (state.RotationFallSway.HasValue)
      managedComponent.RotationFallSway = state.RotationFallSway.Value;
    if (state.RotationSlopeSway.HasValue)
      managedComponent.RotationSlopeSway = state.RotationSlopeSway.Value;
    if (state.RotationInputVelocityScale.HasValue)
      managedComponent.RotationInputVelocityScale = state.RotationInputVelocityScale.Value;
    if (state.RotationMaxInputVelocity.HasValue)
      managedComponent.RotationMaxInputVelocity = state.RotationMaxInputVelocity.Value;
    if (state.RetractionDistance.HasValue)
      managedComponent.RetractionDistance = state.RetractionDistance.Value;
    if (state.RetractionOffset.HasValue)
      managedComponent.RetractionOffset = state.RetractionOffset.Value;
    if (state.RetractionRelaxSpeed.HasValue)
      managedComponent.RetractionRelaxSpeed = state.RetractionRelaxSpeed.Value;
    if (state.ShakeSpeed.HasValue)
      managedComponent.ShakeSpeed = state.ShakeSpeed.Value;
    if (state.ShakeAmplitude.HasValue)
      managedComponent.ShakeAmplitude = state.ShakeAmplitude.Value;
    if (state.BobRate.HasValue)
      managedComponent.BobRate = state.BobRate.Value;
    if (state.BobAmplitude.HasValue)
      managedComponent.BobAmplitude = state.BobAmplitude.Value;
    if (state.BobInputVelocityScale.HasValue)
      managedComponent.BobInputVelocityScale = state.BobInputVelocityScale.Value;
    if (state.BobMaxInputVelocity.HasValue)
      managedComponent.BobMaxInputVelocity = state.BobMaxInputVelocity.Value;
    if (state.BobRequireGroundContact.HasValue)
      managedComponent.BobRequireGroundContact = state.BobRequireGroundContact.Value;
    if (state.StepPositionForce.HasValue)
      managedComponent.StepPositionForce = state.StepPositionForce.Value;
    if (state.StepRotationForce.HasValue)
      managedComponent.StepRotationForce = state.StepRotationForce.Value;
    if (state.StepSoftness.HasValue)
      managedComponent.StepSoftness = state.StepSoftness.Value;
    if (state.StepMinVelocity.HasValue)
      managedComponent.StepMinVelocity = state.StepMinVelocity.Value;
    if (state.StepPositionBalance.HasValue)
      managedComponent.StepPositionBalance = state.StepPositionBalance.Value;
    if (state.StepRotationBalance.HasValue)
      managedComponent.StepRotationBalance = state.StepRotationBalance.Value;
    if (state.StepForceScale.HasValue)
      managedComponent.StepForceScale = state.StepForceScale.Value;
    if (state.AmbientInterval.HasValue)
      managedComponent.AmbientInterval = state.AmbientInterval.Value;
    if (state.PositionExitOffset.HasValue)
      managedComponent.PositionExitOffset = state.PositionExitOffset.Value;
    if (state.RotationExitOffset.HasValue)
      managedComponent.RotationExitOffset = state.RotationExitOffset.Value;
    if (state.LookDownActive.HasValue)
      managedComponent.LookDownActive = state.LookDownActive.Value;
    if (state.LookDownYawLimit.HasValue)
      managedComponent.LookDownYawLimit = state.LookDownYawLimit.Value;
    if (state.LookDownPositionOffsetMiddle.HasValue)
      managedComponent.LookDownPositionOffsetMiddle = state.LookDownPositionOffsetMiddle.Value;
    if (state.LookDownPositionOffsetLeft.HasValue)
      managedComponent.LookDownPositionOffsetLeft = state.LookDownPositionOffsetLeft.Value;
    if (state.LookDownPositionOffsetRight.HasValue)
      managedComponent.LookDownPositionOffsetRight = state.LookDownPositionOffsetRight.Value;
    if (state.LookDownPositionSpringPower.HasValue)
      managedComponent.LookDownPositionSpringPower = state.LookDownPositionSpringPower.Value;
    if (state.LookDownRotationOffsetMiddle.HasValue)
      managedComponent.LookDownRotationOffsetMiddle = state.LookDownRotationOffsetMiddle.Value;
    if (state.LookDownRotationOffsetLeft.HasValue)
      managedComponent.LookDownRotationOffsetLeft = state.LookDownRotationOffsetLeft.Value;
    if (state.LookDownRotationOffsetRight.HasValue)
      managedComponent.LookDownRotationOffsetRight = state.LookDownRotationOffsetRight.Value;
    if (state.LookDownRotationSpringPower.HasValue)
      managedComponent.LookDownRotationSpringPower = state.LookDownRotationSpringPower.Value;
    if (state.AnimationType.HasValue)
      managedComponent.AnimationType = state.AnimationType.Value;
    if (state.AnimationGrip.HasValue)
      managedComponent.AnimationGrip = state.AnimationGrip.Value;
    if (!state.Persist.HasValue)
      return;
    managedComponent.Persist = state.Persist.Value;
  }
}
