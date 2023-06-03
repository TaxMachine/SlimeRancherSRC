// Decompiled with JetBrains decompiler
// Type: LightCurves
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class LightCurves : MonoBehaviour
{
  public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1f, 1f);
  public float GraphScaleX = 1f;
  public float GraphScaleY = 1f;
  private float startTime;
  private Light lightSource;

  private void Start() => lightSource = GetComponent<Light>();

  private void OnEnable() => startTime = Time.time;

  private void Update()
  {
    float num = Time.time - startTime;
    if (num > (double) GraphScaleX)
      return;
    lightSource.intensity = LightCurve.Evaluate(num / GraphScaleX) * GraphScaleY;
  }
}
