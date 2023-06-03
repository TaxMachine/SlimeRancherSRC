// Decompiled with JetBrains decompiler
// Type: Gif.Components.LZWEncoder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;

namespace Gif.Components
{
  public class LZWEncoder
  {
    private static readonly int EOF = -1;
    private int imgW;
    private int imgH;
    private byte[] pixAry;
    private int initCodeSize;
    private int remaining;
    private int curPixel;
    private static readonly int BITS = 12;
    private static readonly int HSIZE = 5003;
    private int n_bits;
    private int maxbits = BITS;
    private int maxcode;
    private int maxmaxcode = 1 << BITS;
    private int[] htab = new int[HSIZE];
    private int[] codetab = new int[HSIZE];
    private int hsize = HSIZE;
    private int free_ent;
    private bool clear_flg;
    private int g_init_bits;
    private int ClearCode;
    private int EOFCode;
    private int cur_accum;
    private int cur_bits;
    private int[] masks = new int[17]
    {
      0,
      1,
      3,
      7,
      15,
      31,
      63,
      sbyte.MaxValue,
      byte.MaxValue,
      511,
      1023,
      2047,
      4095,
      8191,
      16383,
      short.MaxValue,
      ushort.MaxValue
    };
    private int a_count;
    private byte[] accum = new byte[256];

    public LZWEncoder(int width, int height, byte[] pixels, int color_depth)
    {
      imgW = width;
      imgH = height;
      pixAry = pixels;
      initCodeSize = Math.Max(2, color_depth);
    }

    private void Add(byte c, Stream outs)
    {
      accum[a_count++] = c;
      if (a_count < 254)
        return;
      Flush(outs);
    }

    private void ClearTable(Stream outs)
    {
      ResetCodeTable(hsize);
      free_ent = ClearCode + 2;
      clear_flg = true;
      Output(ClearCode, outs);
    }

    private void ResetCodeTable(int hsize)
    {
      for (int index = 0; index < hsize; ++index)
        htab[index] = -1;
    }

    private void Compress(int init_bits, Stream outs)
    {
      g_init_bits = init_bits;
      clear_flg = false;
      n_bits = g_init_bits;
      maxcode = MaxCode(n_bits);
      ClearCode = 1 << init_bits - 1;
      EOFCode = ClearCode + 1;
      free_ent = ClearCode + 2;
      a_count = 0;
      int code = NextPixel();
      int num1 = 0;
      for (int hsize = this.hsize; hsize < 65536; hsize *= 2)
        ++num1;
      int num2 = 8 - num1;
      int hsize1 = this.hsize;
      ResetCodeTable(hsize1);
      Output(ClearCode, outs);
label_17:
      int num3;
      while ((num3 = NextPixel()) != EOF)
      {
        int num4 = (num3 << maxbits) + code;
        int index = num3 << num2 ^ code;
        if (htab[index] == num4)
        {
          code = codetab[index];
        }
        else
        {
          if (htab[index] >= 0)
          {
            int num5 = hsize1 - index;
            if (index == 0)
              num5 = 1;
            do
            {
              if ((index -= num5) < 0)
                index += hsize1;
              if (htab[index] == num4)
              {
                code = codetab[index];
                goto label_17;
              }
            }
            while (htab[index] >= 0);
          }
          Output(code, outs);
          code = num3;
          if (free_ent < maxmaxcode)
          {
            codetab[index] = free_ent++;
            htab[index] = num4;
          }
          else
            ClearTable(outs);
        }
      }
      Output(code, outs);
      Output(EOFCode, outs);
    }

    public void Encode(Stream os)
    {
      os.WriteByte(Convert.ToByte(initCodeSize));
      remaining = imgW * imgH;
      curPixel = 0;
      Compress(initCodeSize + 1, os);
      os.WriteByte(0);
    }

    private void Flush(Stream outs)
    {
      if (a_count <= 0)
        return;
      outs.WriteByte(Convert.ToByte(a_count));
      outs.Write(accum, 0, a_count);
      a_count = 0;
    }

    private int MaxCode(int n_bits) => (1 << n_bits) - 1;

    private int NextPixel()
    {
      if (remaining == 0)
        return EOF;
      --remaining;
      return curPixel + 1 < pixAry.GetUpperBound(0) ? pixAry[curPixel++] & byte.MaxValue : byte.MaxValue;
    }

    private void Output(int code, Stream outs)
    {
      cur_accum &= masks[cur_bits];
      if (cur_bits > 0)
        cur_accum |= code << cur_bits;
      else
        cur_accum = code;
      for (cur_bits += n_bits; cur_bits >= 8; cur_bits -= 8)
      {
        Add((byte) (cur_accum & byte.MaxValue), outs);
        cur_accum >>= 8;
      }
      if (free_ent > maxcode || clear_flg)
      {
        if (clear_flg)
        {
          maxcode = MaxCode(n_bits = g_init_bits);
          clear_flg = false;
        }
        else
        {
          ++n_bits;
          maxcode = n_bits != maxbits ? MaxCode(n_bits) : maxmaxcode;
        }
      }
      if (code != EOFCode)
        return;
      for (; cur_bits > 0; cur_bits -= 8)
      {
        Add((byte) (cur_accum & byte.MaxValue), outs);
        cur_accum >>= 8;
      }
      Flush(outs);
    }
  }
}
