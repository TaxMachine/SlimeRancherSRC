// Decompiled with JetBrains decompiler
// Type: Noise.Noise
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace Noise
{
  public static class Noise
  {
    private static Grad[] grad3 = new Grad[12]
    {
      new Grad(1.0, 1.0, 0.0),
      new Grad(-1.0, 1.0, 0.0),
      new Grad(1.0, -1.0, 0.0),
      new Grad(-1.0, -1.0, 0.0),
      new Grad(1.0, 0.0, 1.0),
      new Grad(-1.0, 0.0, 1.0),
      new Grad(1.0, 0.0, -1.0),
      new Grad(-1.0, 0.0, -1.0),
      new Grad(0.0, 1.0, 1.0),
      new Grad(0.0, -1.0, 1.0),
      new Grad(0.0, 1.0, -1.0),
      new Grad(0.0, -1.0, -1.0)
    };
    private static short[] p = new short[256]
    {
      151,
      160,
      137,
      91,
      90,
      15,
      131,
      13,
      201,
      95,
      96,
      53,
      194,
      233,
      7,
      225,
      140,
      36,
      103,
      30,
      69,
      142,
      8,
      99,
      37,
      240,
      21,
      10,
      23,
      190,
      6,
      148,
      247,
      120,
      234,
      75,
      0,
      26,
      197,
      62,
      94,
      252,
      219,
      203,
      117,
      35,
      11,
      32,
      57,
      177,
      33,
      88,
      237,
      149,
      56,
      87,
      174,
      20,
      125,
      136,
      171,
      168,
      68,
      175,
      74,
      165,
      71,
      134,
      139,
      48,
      27,
      166,
      77,
      146,
      158,
      231,
      83,
      111,
      229,
      122,
      60,
      211,
      133,
      230,
      220,
      105,
      92,
      41,
      55,
      46,
      245,
      40,
      244,
      102,
      143,
      54,
      65,
      25,
      63,
      161,
      1,
      216,
      80,
      73,
      209,
      76,
      132,
      187,
      208,
      89,
      18,
      169,
      200,
      196,
      135,
      130,
      116,
      188,
      159,
      86,
      164,
      100,
      109,
      198,
      173,
      186,
      3,
      64,
      52,
      217,
      226,
      250,
      124,
      123,
      5,
      202,
      38,
      147,
      118,
      126,
      byte.MaxValue,
      82,
      85,
      212,
      207,
      206,
      59,
      227,
      47,
      16,
      58,
      17,
      182,
      189,
      28,
      42,
      223,
      183,
      170,
      213,
      119,
      248,
      152,
      2,
      44,
      154,
      163,
      70,
      221,
      153,
      101,
      155,
      167,
      43,
      172,
      9,
      129,
      22,
      39,
      253,
      19,
      98,
      108,
      110,
      79,
      113,
      224,
      232,
      178,
      185,
      112,
      104,
      218,
      246,
      97,
      228,
      251,
      34,
      242,
      193,
      238,
      210,
      144,
      12,
      191,
      179,
      162,
      241,
      81,
      51,
      145,
      235,
      249,
      14,
      239,
      107,
      49,
      192,
      214,
      31,
      181,
      199,
      106,
      157,
      184,
      84,
      204,
      176,
      115,
      121,
      50,
      45,
      sbyte.MaxValue,
      4,
      150,
      254,
      138,
      236,
      205,
      93,
      222,
      114,
      67,
      29,
      24,
      72,
      243,
      141,
      128,
      195,
      78,
      66,
      215,
      61,
      156,
      180
    };
    private static short[] perm = new short[512];
    private static short[] permMod12 = new short[512];
    private static double F3 = 1.0 / 3.0;
    private static double G3 = 1.0 / 6.0;

    static Noise()
    {
      for (int index = 0; index < 512; ++index)
      {
        perm[index] = p[index & byte.MaxValue];
        permMod12[index] = (short) (perm[index] % 12);
      }
    }

    private static int fastfloor(double x)
    {
      int num = (int) x;
      return x >= num ? num : num - 1;
    }

    private static double dot(Grad g, double x, double y, double z) => g.x * x + g.y * y + g.z * z;

    public static float PerlinNoise(
      double x,
      float y,
      float z,
      float scale,
      float height,
      float power)
    {
      float f = GetNoise(x / scale, y / (double) scale, z / (double) scale) * height;
      if (power != 0.0)
        f = Mathf.Pow(f, power);
      return f;
    }

    public static float GetNoise(double xin, double yin, double zin)
    {
      double num1 = (xin + yin + zin) * F3;
      int num2 = fastfloor(xin + num1);
      int num3 = fastfloor(yin + num1);
      int num4 = fastfloor(zin + num1);
      double num5 = (num2 + num3 + num4) * G3;
      double num6 = num2 - num5;
      double num7 = num3 - num5;
      double num8 = num4 - num5;
      double x1 = xin - num6;
      double y1 = yin - num7;
      double z1 = zin - num8;
      int num9;
      int num10;
      int num11;
      int num12;
      int num13;
      int num14;
      if (x1 >= y1)
      {
        if (y1 >= z1)
        {
          num9 = 1;
          num10 = 0;
          num11 = 0;
          num12 = 1;
          num13 = 1;
          num14 = 0;
        }
        else if (x1 >= z1)
        {
          num9 = 1;
          num10 = 0;
          num11 = 0;
          num12 = 1;
          num13 = 0;
          num14 = 1;
        }
        else
        {
          num9 = 0;
          num10 = 0;
          num11 = 1;
          num12 = 1;
          num13 = 0;
          num14 = 1;
        }
      }
      else if (y1 < z1)
      {
        num9 = 0;
        num10 = 0;
        num11 = 1;
        num12 = 0;
        num13 = 1;
        num14 = 1;
      }
      else if (x1 < z1)
      {
        num9 = 0;
        num10 = 1;
        num11 = 0;
        num12 = 0;
        num13 = 1;
        num14 = 1;
      }
      else
      {
        num9 = 0;
        num10 = 1;
        num11 = 0;
        num12 = 1;
        num13 = 1;
        num14 = 0;
      }
      double x2 = x1 - num9 + G3;
      double y2 = y1 - num10 + G3;
      double z2 = z1 - num11 + G3;
      double x3 = x1 - num12 + 2.0 * G3;
      double y3 = y1 - num13 + 2.0 * G3;
      double z3 = z1 - num14 + 2.0 * G3;
      double x4 = x1 - 1.0 + 3.0 * G3;
      double y4 = y1 - 1.0 + 3.0 * G3;
      double z4 = z1 - 1.0 + 3.0 * G3;
      int num15 = num2 & byte.MaxValue;
      int num16 = num3 & byte.MaxValue;
      int index1 = num4 & byte.MaxValue;
      int index2 = permMod12[num15 + perm[num16 + perm[index1]]];
      int index3 = permMod12[num15 + num9 + perm[num16 + num10 + perm[index1 + num11]]];
      int index4 = permMod12[num15 + num12 + perm[num16 + num13 + perm[index1 + num14]]];
      int index5 = permMod12[num15 + 1 + perm[num16 + 1 + perm[index1 + 1]]];
      double num17 = 0.6 - x1 * x1 - y1 * y1 - z1 * z1;
      double num18;
      if (num17 < 0.0)
      {
        num18 = 0.0;
      }
      else
      {
        double num19 = num17 * num17;
        num18 = num19 * num19 * dot(grad3[index2], x1, y1, z1);
      }
      double num20 = 0.6 - x2 * x2 - y2 * y2 - z2 * z2;
      double num21;
      if (num20 < 0.0)
      {
        num21 = 0.0;
      }
      else
      {
        double num22 = num20 * num20;
        num21 = num22 * num22 * dot(grad3[index3], x2, y2, z2);
      }
      double num23 = 0.6 - x3 * x3 - y3 * y3 - z3 * z3;
      double num24;
      if (num23 < 0.0)
      {
        num24 = 0.0;
      }
      else
      {
        double num25 = num23 * num23;
        num24 = num25 * num25 * dot(grad3[index4], x3, y3, z3);
      }
      double num26 = 0.6 - x4 * x4 - y4 * y4 - z4 * z4;
      double num27;
      if (num26 < 0.0)
      {
        num27 = 0.0;
      }
      else
      {
        double num28 = num26 * num26;
        num27 = num28 * num28 * dot(grad3[index5], x4, y4, z4);
      }
      return (float) (32.0 * (num18 + num21 + num24 + num27) + 1.0) * 0.5f;
    }

    public static float GetOctaveNoise(double pX, double pY, double pZ, int pOctaves)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      for (int y = 0; y < pOctaves; ++y)
      {
        float num3 = (float) Math.Pow(0.5, y);
        float num4 = (float) Math.Pow(2.0, y);
        num1 += GetNoise(pX * num4, pY * num4, pZ) * num3;
        num2 += num3;
      }
      return num1 / num2;
    }

    private struct Grad
    {
      public double x;
      public double y;
      public double z;
      public double w;

      public Grad(double x, double y, double z)
      {
        this.x = x;
        this.y = y;
        this.z = z;
        w = 0.0;
      }
    }
  }
}
