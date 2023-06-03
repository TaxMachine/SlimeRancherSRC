// Decompiled with JetBrains decompiler
// Type: Noise.NoiseGen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace Noise
{
  public class NoiseGen
  {
    public double XScale = 0.02;
    public double YScale = 0.02;
    public double ZScale = 1.0;
    public byte Octaves = 1;

    public double Scale
    {
      set
      {
        XScale = value;
        YScale = value;
      }
    }

    public NoiseGen()
    {
    }

    public NoiseGen(double pScale, byte pOctaves)
    {
      XScale = pScale;
      YScale = pScale;
      Octaves = pOctaves;
    }

    public NoiseGen(double pXScale, double pYScale, byte pOctaves)
    {
      XScale = pXScale;
      YScale = pYScale;
      Octaves = pOctaves;
    }

    public float GetNoise(double x, double y, double z) => Octaves > 1 ? Noise.GetOctaveNoise(x * XScale, y * YScale, z * ZScale, Octaves) : Noise.GetNoise(x * XScale, y * YScale, z * ZScale);
  }
}
