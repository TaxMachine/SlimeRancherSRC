// Decompiled with JetBrains decompiler
// Type: vp_SmoothRandom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_SmoothRandom
{
  private static vp_FractalNoise s_Noise;

  public static Vector3 GetVector3(float speed)
  {
    float x = Time.time * 0.01f * speed;
    return new Vector3(Get().HybridMultifractal(x, 15.73f, 0.58f), Get().HybridMultifractal(x, 63.94f, 0.58f), Get().HybridMultifractal(x, 0.2f, 0.58f));
  }

  public static Vector3 GetVector3Centered(float speed)
  {
    float x1 = Time.time * 0.01f * speed;
    float x2 = (float) ((Time.time - 1.0) * 0.0099999997764825821) * speed;
    return new Vector3(Get().HybridMultifractal(x1, 15.73f, 0.58f), Get().HybridMultifractal(x1, 63.94f, 0.58f), Get().HybridMultifractal(x1, 0.2f, 0.58f)) - new Vector3(Get().HybridMultifractal(x2, 15.73f, 0.58f), Get().HybridMultifractal(x2, 63.94f, 0.58f), Get().HybridMultifractal(x2, 0.2f, 0.58f));
  }

  public static float Get(float speed)
  {
    float num = Time.time * 0.01f * speed;
    return Get().HybridMultifractal(num * 0.01f, 15.7f, 0.65f);
  }

  private static vp_FractalNoise Get()
  {
    if (s_Noise == null)
      s_Noise = new vp_FractalNoise(1.27f, 2.04f, 8.36f);
    return s_Noise;
  }
}
