// Decompiled with JetBrains decompiler
// Type: FPCameraState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FPCameraState : BaseState
{
  public float? RenderingFieldOfView;
  public float? RenderingZoomDamping;
  public Vector3? PositionOffset;
  public float? PositionGroundLimit;
  public float? PositionSpringStiffness;
  public float? PositionSpringDamping;
  public float? PositionSpring2Stiffness;
  public float? PositionSpring2Damping;
  public float? PositionKneeling;
  public int? PositionKneelingSoftness;
  public int? PositionEarthQuakeFactor;
  public Vector2? RotationPitchLimit;
  public Vector2? RotationYawLimit;
  public float? RotationSpringStiffness;
  public float? RotationSpringDamping;
  public float? RotationKneeling;
  public int? RotationKneelingSoftness;
  public float? RotationStrafeRoll;
  public int? RotationEarthQuakeFactor;
  public float? ShakeSpeed;
  public Vector3? ShakeAmplitude;
  public Vector4? BobRate;
  public Vector4? BobAmplitude;
  public int? BobInputVelocityScale;
  public int? BobMaxInputVelocity;
  public bool? BobRequireGroundContact;
  public int? BobStepThreshold;

  public FPCameraState(string name)
    : base(name)
  {
  }
}
