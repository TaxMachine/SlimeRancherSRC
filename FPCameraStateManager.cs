// Decompiled with JetBrains decompiler
// Type: FPCameraStateManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FPCameraStateManager : BaseStateManager<FPCameraState, vp_FPCamera>
{
  public FPCameraStateManager(vp_FPCamera managedComponent)
    : base(managedComponent)
  {
    CreateStates();
  }

  private void CreateStates()
  {
    states = new FPCameraState[1];
    AddState(new FPCameraState("Default")
    {
      RenderingFieldOfView = new float?(60f),
      RenderingZoomDamping = new float?(0.2f),
      PositionOffset = new Vector3?(new Vector3(0.0f, 1.75f, 0.1f)),
      PositionGroundLimit = new float?(0.1f),
      PositionSpringStiffness = new float?(0.01f),
      PositionSpringDamping = new float?(0.25f),
      PositionSpring2Stiffness = new float?(0.95f),
      PositionSpring2Damping = new float?(0.25f),
      PositionKneeling = new float?(0.025f),
      PositionKneelingSoftness = new int?(1),
      RotationPitchLimit = new Vector2?(new Vector2(90f, -90f)),
      RotationYawLimit = new Vector2?(new Vector2(-360f, 360f)),
      RotationSpringStiffness = new float?(0.01f),
      RotationSpringDamping = new float?(0.25f),
      RotationKneeling = new float?(0.025f),
      RotationKneelingSoftness = new int?(1),
      RotationStrafeRoll = new float?(0.01f),
      ShakeSpeed = new float?(0.0f),
      ShakeAmplitude = new Vector3?(new Vector3(10f, 10f, 0.0f)),
      BobRate = new Vector4?(new Vector4(0.0f, 1.4f, 0.0f, 0.7f)),
      BobAmplitude = new Vector4?(new Vector4(0.0f, 0.25f, 0.0f, 0.5f)),
      BobInputVelocityScale = new int?(1),
      BobMaxInputVelocity = new int?(100),
      BobStepThreshold = new int?(10)
    }, 0);
  }

  public override void ApplyState(FPCameraState state)
  {
    if (state.RenderingFieldOfView.HasValue)
      managedComponent.RenderingFieldOfView = state.RenderingFieldOfView.Value;
    if (state.RenderingZoomDamping.HasValue)
      managedComponent.RenderingZoomDamping = state.RenderingZoomDamping.Value;
    if (state.PositionOffset.HasValue)
      managedComponent.PositionOffset = state.PositionOffset.Value;
    if (state.PositionGroundLimit.HasValue)
      managedComponent.PositionGroundLimit = state.PositionGroundLimit.Value;
    if (state.PositionSpringStiffness.HasValue)
      managedComponent.PositionSpringStiffness = state.PositionSpringStiffness.Value;
    if (state.PositionSpringDamping.HasValue)
      managedComponent.PositionSpringDamping = state.PositionSpringDamping.Value;
    if (state.PositionSpring2Stiffness.HasValue)
      managedComponent.PositionSpring2Stiffness = state.PositionSpring2Stiffness.Value;
    if (state.PositionSpring2Damping.HasValue)
      managedComponent.PositionSpring2Damping = state.PositionSpring2Damping.Value;
    if (state.PositionKneeling.HasValue)
      managedComponent.PositionKneeling = state.PositionKneeling.Value;
    if (state.PositionKneelingSoftness.HasValue)
      managedComponent.PositionKneelingSoftness = state.PositionKneelingSoftness.Value;
    if (state.PositionEarthQuakeFactor.HasValue)
      managedComponent.PositionEarthQuakeFactor = state.PositionEarthQuakeFactor.Value;
    if (state.RotationPitchLimit.HasValue)
      managedComponent.RotationPitchLimit = state.RotationPitchLimit.Value;
    if (state.RotationYawLimit.HasValue)
      managedComponent.RotationYawLimit = state.RotationYawLimit.Value;
    if (state.RotationSpringStiffness.HasValue)
      managedComponent.RotationSpringStiffness = state.RotationSpringStiffness.Value;
    if (state.RotationSpringDamping.HasValue)
      managedComponent.RotationSpringDamping = state.RotationSpringDamping.Value;
    if (state.RotationKneeling.HasValue)
      managedComponent.RotationKneeling = state.RotationKneeling.Value;
    if (state.RotationKneelingSoftness.HasValue)
      managedComponent.RotationKneelingSoftness = state.RotationKneelingSoftness.Value;
    if (state.RotationStrafeRoll.HasValue)
      managedComponent.RotationStrafeRoll = state.RotationStrafeRoll.Value;
    if (state.RotationEarthQuakeFactor.HasValue)
      managedComponent.RotationEarthQuakeFactor = state.RotationEarthQuakeFactor.Value;
    if (state.ShakeSpeed.HasValue)
      managedComponent.ShakeSpeed = state.ShakeSpeed.Value;
    if (state.ShakeAmplitude.HasValue)
      managedComponent.ShakeAmplitude = state.ShakeAmplitude.Value;
    if (state.BobRate.HasValue)
      managedComponent.BobRate = state.BobRate.Value;
    if (state.BobInputVelocityScale.HasValue)
      managedComponent.BobInputVelocityScale = state.BobInputVelocityScale.Value;
    if (state.BobMaxInputVelocity.HasValue)
      managedComponent.BobMaxInputVelocity = state.BobMaxInputVelocity.Value;
    if (state.BobRequireGroundContact.HasValue)
      managedComponent.BobRequireGroundContact = state.BobRequireGroundContact.Value;
    if (!state.BobStepThreshold.HasValue)
      return;
    managedComponent.BobStepThreshold = state.BobStepThreshold.Value;
  }
}
