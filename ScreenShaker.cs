// Decompiled with JetBrains decompiler
// Type: ScreenShaker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
  private vp_FPCamera fpCamera;

  public void Awake() => fpCamera = GetComponentInChildren<vp_FPCamera>();

  public void ShakeDamage(float intensity) => fpCamera.AddForce2(new Vector3(Randoms.SHARED.GetInRange(-1f, 1f), -1f, 0.0f) * intensity);

  public void ShakeFrontImpact(float intensity) => fpCamera.AddForce2(new Vector3(0.0f, 0.0f, -1f) * intensity);
}
