// Decompiled with JetBrains decompiler
// Type: vp_FractalNoise
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class vp_FractalNoise
{
  private vp_Perlin m_Noise;
  private float[] m_Exponent;
  private int m_IntOctaves;
  private float m_Octaves;
  private float m_Lacunarity;

  public vp_FractalNoise(float inH, float inLacunarity, float inOctaves)
    : this(inH, inLacunarity, inOctaves, null)
  {
  }

  public vp_FractalNoise(float inH, float inLacunarity, float inOctaves, vp_Perlin noise)
  {
    m_Lacunarity = inLacunarity;
    m_Octaves = inOctaves;
    m_IntOctaves = (int) inOctaves;
    m_Exponent = new float[m_IntOctaves + 1];
    float num = 1f;
    for (int index = 0; index < m_IntOctaves + 1; ++index)
    {
      m_Exponent[index] = (float) Math.Pow(m_Lacunarity, -(double) inH);
      num *= m_Lacunarity;
    }
    if (noise == null)
      m_Noise = new vp_Perlin();
    else
      m_Noise = noise;
  }

  public float HybridMultifractal(float x, float y, float offset)
  {
    float num1 = (m_Noise.Noise(x, y) + offset) * m_Exponent[0];
    float num2 = num1;
    x *= m_Lacunarity;
    y *= m_Lacunarity;
    int index;
    for (index = 1; index < m_IntOctaves; ++index)
    {
      if (num2 > 1.0)
        num2 = 1f;
      float num3 = (m_Noise.Noise(x, y) + offset) * m_Exponent[index];
      num1 += num2 * num3;
      num2 *= num3;
      x *= m_Lacunarity;
      y *= m_Lacunarity;
    }
    float num4 = m_Octaves - m_IntOctaves;
    return num1 + num4 * m_Noise.Noise(x, y) * m_Exponent[index];
  }

  public float RidgedMultifractal(float x, float y, float offset, float gain)
  {
    float num1 = Mathf.Abs(m_Noise.Noise(x, y));
    float num2 = offset - num1;
    float num3 = num2 * num2;
    float num4 = num3;
    for (int index = 1; index < m_IntOctaves; ++index)
    {
      x *= m_Lacunarity;
      y *= m_Lacunarity;
      float num5 = Mathf.Clamp01(num3 * gain);
      float num6 = Mathf.Abs(m_Noise.Noise(x, y));
      float num7 = offset - num6;
      num3 = num7 * num7 * num5;
      num4 += num3 * m_Exponent[index];
    }
    return num4;
  }

  public float BrownianMotion(float x, float y)
  {
    float num1 = 0.0f;
    long index;
    for (index = 0L; index < m_IntOctaves; ++index)
    {
      num1 = m_Noise.Noise(x, y) * m_Exponent[index];
      x *= m_Lacunarity;
      y *= m_Lacunarity;
    }
    float num2 = m_Octaves - m_IntOctaves;
    return num1 + num2 * m_Noise.Noise(x, y) * m_Exponent[index];
  }
}
