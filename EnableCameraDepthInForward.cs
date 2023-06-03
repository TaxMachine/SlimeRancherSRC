// Decompiled with JetBrains decompiler
// Type: EnableCameraDepthInForward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Camera))]
public class EnableCameraDepthInForward : MonoBehaviour
{
  private void Start() => Set();

  private void Set()
  {
    if (GetComponent<Camera>().depthTextureMode != DepthTextureMode.None)
      return;
    GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
  }
}
