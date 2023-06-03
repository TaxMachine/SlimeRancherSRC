// Decompiled with JetBrains decompiler
// Type: CameraDepthMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using UnityEngine;

public class CameraDepthMode : MonoBehaviour
{
  public DepthTextureMode depth;

  private void Start() => this.GetRequiredComponent<Camera>().depthTextureMode = depth;
}
