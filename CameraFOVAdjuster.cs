// Decompiled with JetBrains decompiler
// Type: CameraFOVAdjuster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CameraFOVAdjuster : SRBehaviour
{
  public static List<CameraFOVAdjuster> Instances = new List<CameraFOVAdjuster>();
  private Camera ownCamera;

  public void Awake()
  {
    Instances.Add(this);
    ownCamera = GetRequiredComponent<Camera>();
  }

  public void OnDestroy() => Instances.Remove(this);

  public void SetFOV(float fov) => ownCamera.fieldOfView = fov;
}
