// Decompiled with JetBrains decompiler
// Type: Gif.Components.NeuQuant
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace Gif.Components
{
  public class NeuQuant
  {
    protected static readonly int netsize = 128;
    protected static readonly int prime1 = 499;
    protected static readonly int prime2 = 491;
    protected static readonly int prime3 = 487;
    protected static readonly int prime4 = 503;
    protected static readonly int minpicturebytes = 3 * prime4;
    protected static readonly int maxnetpos = netsize - 1;
    protected static readonly int netbiasshift = 4;
    protected static readonly int ncycles = 100;
    protected static readonly int intbiasshift = 16;
    protected static readonly int intbias = 1 << intbiasshift;
    protected static readonly int gammashift = 10;
    protected static readonly int gamma = 1 << gammashift;
    protected static readonly int betashift = 10;
    protected static readonly int beta = intbias >> betashift;
    protected static readonly int betagamma = intbias << gammashift - betashift;
    protected static readonly int initrad = netsize >> 3;
    protected static readonly int radiusbiasshift = 6;
    protected static readonly int radiusbias = 1 << radiusbiasshift;
    protected static readonly int initradius = initrad * radiusbias;
    protected static readonly int radiusdec = 30;
    protected static readonly int alphabiasshift = 10;
    protected static readonly int initalpha = 1 << alphabiasshift;
    protected int alphadec;
    protected static readonly int radbiasshift = 8;
    protected static readonly int radbias = 1 << radbiasshift;
    protected static readonly int alpharadbshift = alphabiasshift + radbiasshift;
    protected static readonly int alpharadbias = 1 << alpharadbshift;
    protected byte[] thepicture;
    protected int lengthcount;
    protected int samplefac;
    protected int[][] network;
    protected int[] netindex = new int[256];
    protected int[] bias = new int[netsize];
    protected int[] freq = new int[netsize];
    protected int[] radpower = new int[initrad];

    public NeuQuant(byte[] thepic, int len, int sample)
    {
      thepicture = thepic;
      lengthcount = len;
      samplefac = sample;
      network = new int[netsize][];
      for (int index = 0; index < netsize; ++index)
      {
        network[index] = new int[4];
        int[] numArray = network[index];
        numArray[0] = numArray[1] = numArray[2] = (index << netbiasshift + 8) / netsize;
        freq[index] = intbias / netsize;
        bias[index] = 0;
      }
    }

    public byte[] ColorMap()
    {
      byte[] numArray1 = new byte[3 * netsize];
      int[] numArray2 = new int[netsize];
      for (int index = 0; index < netsize; ++index)
        numArray2[network[index][3]] = index;
      int num1 = 0;
      for (int index1 = 0; index1 < netsize; ++index1)
      {
        int index2 = numArray2[index1];
        byte[] numArray3 = numArray1;
        int index3 = num1;
        int num2 = index3 + 1;
        int num3 = (byte) network[index2][0];
        numArray3[index3] = (byte) num3;
        byte[] numArray4 = numArray1;
        int index4 = num2;
        int num4 = index4 + 1;
        int num5 = (byte) network[index2][1];
        numArray4[index4] = (byte) num5;
        byte[] numArray5 = numArray1;
        int index5 = num4;
        num1 = index5 + 1;
        int num6 = (byte) network[index2][2];
        numArray5[index5] = (byte) num6;
      }
      return numArray1;
    }

    public void Inxbuild()
    {
      int index1 = 0;
      int num1 = 0;
      for (int index2 = 0; index2 < netsize; ++index2)
      {
        int[] numArray1 = network[index2];
        int index3 = index2;
        int num2 = numArray1[1];
        for (int index4 = index2 + 1; index4 < netsize; ++index4)
        {
          int[] numArray2 = network[index4];
          if (numArray2[1] < num2)
          {
            index3 = index4;
            num2 = numArray2[1];
          }
        }
        int[] numArray3 = network[index3];
        if (index2 != index3)
        {
          int num3 = numArray3[0];
          numArray3[0] = numArray1[0];
          numArray1[0] = num3;
          int num4 = numArray3[1];
          numArray3[1] = numArray1[1];
          numArray1[1] = num4;
          int num5 = numArray3[2];
          numArray3[2] = numArray1[2];
          numArray1[2] = num5;
          int num6 = numArray3[3];
          numArray3[3] = numArray1[3];
          numArray1[3] = num6;
        }
        if (num2 != index1)
        {
          netindex[index1] = num1 + index2 >> 1;
          for (int index5 = index1 + 1; index5 < num2; ++index5)
            netindex[index5] = index2;
          index1 = num2;
          num1 = index2;
        }
      }
      netindex[index1] = num1 + maxnetpos >> 1;
      for (int index6 = index1 + 1; index6 < 256; ++index6)
        netindex[index6] = maxnetpos;
    }

    public void Learn()
    {
      if (this.lengthcount < minpicturebytes)
        samplefac = 1;
      alphadec = 30 + (samplefac - 1) / 3;
      byte[] thepicture = this.thepicture;
      int index1 = 0;
      int lengthcount = this.lengthcount;
      int num1 = this.lengthcount / (3 * samplefac);
      int num2 = num1 / ncycles;
      int initalpha = NeuQuant.initalpha;
      int initradius = NeuQuant.initradius;
      int rad = initradius >> radiusbiasshift;
      if (rad <= 1)
        rad = 0;
      for (int index2 = 0; index2 < rad; ++index2)
        radpower[index2] = initalpha * ((rad * rad - index2 * index2) * radbias / (rad * rad));
      int num3 = this.lengthcount >= minpicturebytes ? (this.lengthcount % prime1 == 0 ? (this.lengthcount % prime2 == 0 ? (this.lengthcount % prime3 == 0 ? 3 * prime4 : 3 * prime3) : 3 * prime2) : 3 * prime1) : 3;
      int num4 = 0;
      while (num4 < num1)
      {
        int b = (thepicture[index1] & byte.MaxValue) << netbiasshift;
        int g = (thepicture[index1 + 1] & byte.MaxValue) << netbiasshift;
        int r = (thepicture[index1 + 2] & byte.MaxValue) << netbiasshift;
        int i = Contest(b, g, r);
        Altersingle(initalpha, i, b, g, r);
        if (rad != 0)
          Alterneigh(rad, i, b, g, r);
        index1 += num3;
        if (index1 >= lengthcount)
          index1 -= this.lengthcount;
        ++num4;
        if (num2 == 0)
          num2 = 1;
        if (num4 % num2 == 0)
        {
          initalpha -= initalpha / alphadec;
          initradius -= initradius / radiusdec;
          rad = initradius >> radiusbiasshift;
          if (rad <= 1)
            rad = 0;
          for (int index3 = 0; index3 < rad; ++index3)
            radpower[index3] = initalpha * ((rad * rad - index3 * index3) * radbias / (rad * rad));
        }
      }
    }

    public int Map(int b, int g, int r)
    {
      int num1 = 1000;
      int num2 = -1;
      int netsize = netindex[g];
      int index = netsize - 1;
      while (netsize < NeuQuant.netsize || index >= 0)
      {
        if (netsize < NeuQuant.netsize)
        {
          int[] numArray = network[netsize];
          int num3 = numArray[1] - g;
          if (num3 >= num1)
          {
            netsize = NeuQuant.netsize;
          }
          else
          {
            ++netsize;
            if (num3 < 0)
              num3 = -num3;
            int num4 = numArray[0] - b;
            if (num4 < 0)
              num4 = -num4;
            int num5 = num3 + num4;
            if (num5 < num1)
            {
              int num6 = numArray[2] - r;
              if (num6 < 0)
                num6 = -num6;
              int num7 = num5 + num6;
              if (num7 < num1)
              {
                num1 = num7;
                num2 = numArray[3];
              }
            }
          }
        }
        if (index >= 0)
        {
          int[] numArray = network[index];
          int num8 = g - numArray[1];
          if (num8 >= num1)
          {
            index = -1;
          }
          else
          {
            --index;
            if (num8 < 0)
              num8 = -num8;
            int num9 = numArray[0] - b;
            if (num9 < 0)
              num9 = -num9;
            int num10 = num8 + num9;
            if (num10 < num1)
            {
              int num11 = numArray[2] - r;
              if (num11 < 0)
                num11 = -num11;
              int num12 = num10 + num11;
              if (num12 < num1)
              {
                num1 = num12;
                num2 = numArray[3];
              }
            }
          }
        }
      }
      return num2;
    }

    public byte[] Process()
    {
      Learn();
      Unbiasnet();
      Inxbuild();
      return ColorMap();
    }

    public void Unbiasnet()
    {
      for (int index = 0; index < netsize; ++index)
      {
        network[index][0] >>= netbiasshift;
        network[index][1] >>= netbiasshift;
        network[index][2] >>= netbiasshift;
        network[index][3] = index;
      }
    }

    protected void Alterneigh(int rad, int i, int b, int g, int r)
    {
      int num1 = i - rad;
      if (num1 < -1)
        num1 = -1;
      int num2 = i + rad;
      if (num2 > netsize)
        num2 = netsize;
      int num3 = i + 1;
      int num4 = i - 1;
      int num5 = 1;
      while (num3 < num2 || num4 > num1)
      {
        int num6 = radpower[num5++];
        if (num3 < num2)
        {
          int[] numArray = network[num3++];
          try
          {
            numArray[0] -= num6 * (numArray[0] - b) / alpharadbias;
            numArray[1] -= num6 * (numArray[1] - g) / alpharadbias;
            numArray[2] -= num6 * (numArray[2] - r) / alpharadbias;
          }
          catch (Exception ex)
          {
          }
        }
        if (num4 > num1)
        {
          int[] numArray = network[num4--];
          try
          {
            numArray[0] -= num6 * (numArray[0] - b) / alpharadbias;
            numArray[1] -= num6 * (numArray[1] - g) / alpharadbias;
            numArray[2] -= num6 * (numArray[2] - r) / alpharadbias;
          }
          catch (Exception ex)
          {
          }
        }
      }
    }

    protected void Altersingle(int alpha, int i, int b, int g, int r)
    {
      int[] numArray = network[i];
      numArray[0] -= alpha * (numArray[0] - b) / initalpha;
      numArray[1] -= alpha * (numArray[1] - g) / initalpha;
      numArray[2] -= alpha * (numArray[2] - r) / initalpha;
    }

    protected int Contest(int b, int g, int r)
    {
      int num1 = int.MaxValue;
      int num2 = num1;
      int index1 = -1;
      int num3 = index1;
      for (int index2 = 0; index2 < netsize; ++index2)
      {
        int[] numArray = network[index2];
        int num4 = numArray[0] - b;
        if (num4 < 0)
          num4 = -num4;
        int num5 = numArray[1] - g;
        if (num5 < 0)
          num5 = -num5;
        int num6 = num4 + num5;
        int num7 = numArray[2] - r;
        if (num7 < 0)
          num7 = -num7;
        int num8 = num6 + num7;
        if (num8 < num1)
        {
          num1 = num8;
          index1 = index2;
        }
        int num9 = num8 - (bias[index2] >> intbiasshift - netbiasshift);
        if (num9 < num2)
        {
          num2 = num9;
          num3 = index2;
        }
        int num10 = freq[index2] >> betashift;
        freq[index2] -= num10;
        bias[index2] += num10 << gammashift;
      }
      freq[index1] += beta;
      bias[index1] -= betagamma;
      return num3;
    }
  }
}
