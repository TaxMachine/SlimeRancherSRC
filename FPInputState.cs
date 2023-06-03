// Decompiled with JetBrains decompiler
// Type: FPInputState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FPInputState : BaseState
{
  public Vector2? MouseLookSensitivity;
  public int? MouseLookSmoothSteps;
  public bool? MouseLookAcceleration;
  public float? MouseLookSmoothWeight;
  public float? MouseLookAccelerationThreshold;
  public bool? MouseLookInvert;
  public bool? MouseCursorForced;
  public bool? MouseCursorBlocksMouseLook;
  public bool? Persist;

  public FPInputState(string name)
    : base(name)
  {
  }
}
