// Decompiled with JetBrains decompiler
// Type: CFX_Demo_RotateCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CFX_Demo_RotateCamera : MonoBehaviour
{
  public static bool rotating = true;
  public float speed = 30f;
  public Transform rotationCenter;

  private void Update()
  {
    if (!rotating)
      return;
    transform.RotateAround(rotationCenter.position, Vector3.up, speed * Time.deltaTime);
  }
}
