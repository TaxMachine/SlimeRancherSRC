// Decompiled with JetBrains decompiler
// Type: FPInputStateManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class FPInputStateManager : BaseStateManager<FPInputState, vp_FPInput>
{
  public FPInputStateManager(vp_FPInput managedComponent)
    : base(managedComponent)
  {
    CreateStates();
  }

  private void CreateStates()
  {
    states = new FPInputState[1];
    AddState(new FPInputState("Default"), 0);
  }

  public override void ApplyState(FPInputState state)
  {
    if (state.MouseLookSensitivity.HasValue)
      managedComponent.MouseLookSensitivity = state.MouseLookSensitivity.Value;
    if (state.MouseLookSmoothSteps.HasValue)
      managedComponent.MouseLookSmoothSteps = state.MouseLookSmoothSteps.Value;
    if (state.MouseLookAcceleration.HasValue)
      managedComponent.MouseLookAcceleration = state.MouseLookAcceleration.Value;
    if (state.MouseLookSmoothWeight.HasValue)
      managedComponent.MouseLookSmoothWeight = state.MouseLookSmoothWeight.Value;
    if (state.MouseLookAccelerationThreshold.HasValue)
      managedComponent.MouseLookAccelerationThreshold = state.MouseLookAccelerationThreshold.Value;
    if (state.MouseLookInvert.HasValue)
      managedComponent.MouseLookInvert = state.MouseLookInvert.Value;
    if (state.MouseCursorForced.HasValue)
      managedComponent.MouseCursorForced = state.MouseCursorForced.Value;
    if (state.MouseCursorBlocksMouseLook.HasValue)
      managedComponent.MouseCursorBlocksMouseLook = state.MouseCursorBlocksMouseLook.Value;
    if (!state.Persist.HasValue)
      return;
    managedComponent.Persist = state.Persist.Value;
  }
}
