// Decompiled with JetBrains decompiler
// Type: SRFPCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SRFPCamera : vp_FPCamera
{
  private OptionsDirector optionsDir;
  private Vector4 defaultBobAmp;
  private Vector4 NO_BOB = new Vector4(0.0f, 1f / 1000f, 0.0f, 0.0f);

  protected override void Awake()
  {
    base.Awake();
    optionsDir = SRSingleton<GameContext>.Instance.OptionsDirector;
    GetComponent<Camera>().cullingMask &= -8193;
    defaultBobAmp = BobAmplitude;
  }

  protected override void UpdateBob()
  {
    if (!optionsDir.disableCameraBob)
      BobAmplitude = defaultBobAmp;
    else
      BobAmplitude = NO_BOB;
    base.UpdateBob();
  }
}
