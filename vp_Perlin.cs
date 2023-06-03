// Decompiled with JetBrains decompiler
// Type: vp_Perlin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

public class vp_Perlin
{
  private const int B = 256;
  private const int BM = 255;
  private const int N = 4096;
  private int[] p = new int[514];
  private float[,] g3 = new float[514, 3];
  private float[,] g2 = new float[514, 2];
  private float[] g1 = new float[514];

  private float s_curve(float t) => (float)(t * (double)t * (3.0 - 2.0 * t));

  private float lerp(float t, float a, float b) => a + t * (b - a);

  private void setup(float value, out int b0, out int b1, out float r0, out float r1)
  {
    float num = value + 4096f;
    b0 = (int)num & byte.MaxValue;
    b1 = b0 + 1 & byte.MaxValue;
    r0 = num - (int)num;
    r1 = r0 - 1f;
  }

  private float at2(float rx, float ry, float x, float y) => (float)(rx * (double)x + ry * (double)y);

  private float at3(float rx, float ry, float rz, float x, float y, float z) =>
    (float)(rx * (double)x + ry * (double)y + rz * (double)z);

  public float Noise(float arg)
  {
    int b0;
    int b1;
    float r0;
    float r1;
    setup(arg, out b0, out b1, out r0, out r1);
    return lerp(s_curve(r0), r0 * g1[p[b0]], r1 * g1[p[b1]]);
  }

  public float Noise(float x, float y)
  {
    int b0_1;
    int b1_1;
    float r0_1;
    float r1_1;
    setup(x, out b0_1, out b1_1, out r0_1, out r1_1);
    int b0_2;
    int b1_2;
    float r0_2;
    float r1_2;
    setup(y, out b0_2, out b1_2, out r0_2, out r1_2);
    int num1 = p[b0_1];
    int num2 = p[b1_1];
    int index1 = p[num1 + b0_2];
    int index2 = p[num2 + b0_2];
    int index3 = p[num1 + b1_2];
    int index4 = p[num2 + b1_2];
    float t1 = s_curve(r0_1);
    float t2 = s_curve(r0_2);
    float a1 = at2(r0_1, r0_2, g2[index1, 0], g2[index1, 1]);
    float b1 = at2(r1_1, r0_2, g2[index2, 0], g2[index2, 1]);
    float a2 = lerp(t1, a1, b1);
    float a3 = at2(r0_1, r1_2, g2[index3, 0], g2[index3, 1]);
    float b2 = at2(r1_1, r1_2, g2[index4, 0], g2[index4, 1]);
    float b3 = lerp(t1, a3, b2);
    return lerp(t2, a2, b3);
  }

  public float Noise(float x, float y, float z)
  {
    int b0_1;
    int b1_1;
    float r0_1;
    float r1_1;
    setup(x, out b0_1, out b1_1, out r0_1, out r1_1);
    int b0_2;
    int b1_2;
    float r0_2;
    float r1_2;
    setup(y, out b0_2, out b1_2, out r0_2, out r1_2);
    int b0_3;
    int b1_3;
    float r0_3;
    float r1_3;
    setup(z, out b0_3, out b1_3, out r0_3, out r1_3);
    int num1 = p[b0_1];
    int num2 = p[b1_1];
    int num3 = p[num1 + b0_2];
    int num4 = p[num2 + b0_2];
    int num5 = p[num1 + b1_2];
    int num6 = p[num2 + b1_2];
    float t1 = s_curve(r0_1);
    float t2 = s_curve(r0_2);
    float t3 = s_curve(r0_3);
    float a1 = at3(r0_1, r0_2, r0_3, g3[num3 + b0_3, 0], g3[num3 + b0_3, 1], g3[num3 + b0_3, 2]);
    float b1 = at3(r1_1, r0_2, r0_3, g3[num4 + b0_3, 0], g3[num4 + b0_3, 1], g3[num4 + b0_3, 2]);
    float a2 = lerp(t1, a1, b1);
    float a3 = at3(r0_1, r1_2, r0_3, g3[num5 + b0_3, 0], g3[num5 + b0_3, 1], g3[num5 + b0_3, 2]);
    float b2 = at3(r1_1, r1_2, r0_3, g3[num6 + b0_3, 0], g3[num6 + b0_3, 1], g3[num6 + b0_3, 2]);
    float b3 = lerp(t1, a3, b2);
    float a4 = lerp(t2, a2, b3);
    float a5 = at3(r0_1, r0_2, r1_3, g3[num3 + b1_3, 0], g3[num3 + b1_3, 2], g3[num3 + b1_3, 2]);
    float b4 = at3(r1_1, r0_2, r1_3, g3[num4 + b1_3, 0], g3[num4 + b1_3, 1], g3[num4 + b1_3, 2]);
    float a6 = lerp(t1, a5, b4);
    float a7 = at3(r0_1, r1_2, r1_3, g3[num5 + b1_3, 0], g3[num5 + b1_3, 1], g3[num5 + b1_3, 2]);
    float b5 = at3(r1_1, r1_2, r1_3, g3[num6 + b1_3, 0], g3[num6 + b1_3, 1], g3[num6 + b1_3, 2]);
    float b6 = lerp(t1, a7, b5);
    float b7 = lerp(t2, a6, b6);
    return lerp(t3, a4, b7);
  }

  private void normalize2(ref float x, ref float y)
  {
    float num = (float)Math.Sqrt(x * (double)x + y * (double)y);
    x = y / num;
    y /= num;
  }

  private void normalize3(ref float x, ref float y, ref float z)
  {
    float num = (float)Math.Sqrt(x * (double)x + y * (double)y + z * (double)z);
    x = y / num;
    y /= num;
    z /= num;
  }

  public vp_Perlin()
  {
    Random random = new Random();
    int index1;
    for (index1 = 0; index1 < 256; ++index1)
    {
      /*p[index1] = index1;
      g1[index1] = (random.Next(512) - 256) / 256f;
      for (int index2 = 0; index2 < 2; ++index2)
        g2 = (random.Next(512) - 256) / 256f;
      this.normalize2(ref g2, ref g2);
      for (int index3 = 0; index3 < 3; ++index3)
        g3 = (random.Next(512) - 256) / 256f;
      this.normalize3(ref g3, ref g3, ref g3);*/
    }

    while (--index1 != 0)
    {
      int num = p[index1];
      int index4;
      p[index1] = p[index4 = random.Next(256)];
      p[index4] = num;
    }

    for (int index5 = 0; index5 < 258; ++index5)
    {
      p[256 + index5] = p[index5];
      g1[256 + index5] = g1[index5];
    }
  }
}
