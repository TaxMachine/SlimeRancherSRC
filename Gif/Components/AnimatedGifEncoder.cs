// Decompiled with JetBrains decompiler
// Type: Gif.Components.AnimatedGifEncoder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using UnityEngine;

namespace Gif.Components
{
  public class AnimatedGifEncoder
  {
    protected int width;
    protected int height;
    protected Color32 transparent = new Color32(0, 0, 0, 0);
    protected int transIndex;
    protected int repeat = -1;
    protected int delay;
    protected bool started;
    protected FileStream fs;
    protected byte[] pixels;
    protected byte[] indexedPixels;
    protected int colorDepth;
    protected byte[] colorTab;
    protected bool[] usedEntry = new bool[256];
    protected int palSize = 7;
    protected int dispose = -1;
    protected bool closeStream;
    protected bool firstFrame = true;
    protected bool sizeSet;
    protected int sample = 10;

    public void SetDelay(int ms) => delay = (int) Math.Round(ms / 10.0);

    public void SetDispose(int code)
    {
      if (code < 0)
        return;
      dispose = code;
    }

    public void SetRepeat(int iter)
    {
      if (iter < 0)
        return;
      repeat = iter;
    }

    public void SetTransparent(Color32 c) => transparent = c;

    public bool AddFrame(Color32[] pixels, int imgWidth, int imgHeight)
    {
      if (pixels == null || !started)
        return false;
      bool flag = true;
      try
      {
        if (!sizeSet)
          SetSize(imgWidth, imgHeight);
        this.pixels = ConvertToBytePixels(pixels);
        AnalyzePixels();
        if (firstFrame)
        {
          WriteLSD();
          WritePalette();
          if (repeat >= 0)
            WriteNetscapeExt();
        }
        WriteGraphicCtrlExt();
        WriteImageDesc();
        if (!firstFrame)
          WritePalette();
        WritePixels();
        firstFrame = false;
      }
      catch (IOException ex)
      {
        flag = false;
      }
      return flag;
    }

    private byte[] ConvertToBytePixels(Color32[] intPixels)
    {
      byte[] bytePixels = new byte[intPixels.Length * 3];
      for (int index1 = 0; index1 < height; ++index1)
      {
        for (int index2 = 0; index2 < width; ++index2)
        {
          int index3 = (index2 + index1 * width) * 3;
          int index4 = index2 + (height - index1 - 1) * width;
          bytePixels[index3] = intPixels[index4].r;
          bytePixels[index3 + 1] = intPixels[index4].g;
          bytePixels[index3 + 2] = intPixels[index4].b;
        }
      }
      return bytePixels;
    }

    public bool Finish()
    {
      if (!started)
        return false;
      bool flag = true;
      started = false;
      try
      {
        fs.WriteByte(59);
        fs.Flush();
        if (closeStream)
          fs.Close();
      }
      catch (IOException ex)
      {
        flag = false;
      }
      transIndex = 0;
      fs = null;
      pixels = null;
      indexedPixels = null;
      colorTab = null;
      closeStream = false;
      firstFrame = true;
      return flag;
    }

    public void SetFrameRate(float fps)
    {
      if (fps == 0.0)
        return;
      delay = (int) Math.Round(100.0 / fps);
    }

    public void SetQuality(int quality)
    {
      if (quality < 1)
        quality = 1;
      sample = quality;
    }

    public void SetSize(int w, int h)
    {
      if (started && !firstFrame)
        return;
      width = w;
      height = h;
      if (width < 1)
        width = 320;
      if (height < 1)
        height = 240;
      sizeSet = true;
    }

    public bool Start(FileStream os)
    {
      if (os == null)
        return false;
      bool flag = true;
      closeStream = false;
      fs = os;
      try
      {
        WriteString("GIF89a");
      }
      catch (IOException ex)
      {
        flag = false;
      }
      return started = flag;
    }

    public bool Start(string file)
    {
      bool flag;
      try
      {
        fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        flag = Start(fs);
        closeStream = true;
      }
      catch (IOException ex)
      {
        flag = false;
      }
      return started = flag;
    }

    protected void AnalyzePixels()
    {
      int length1 = pixels.Length;
      int length2 = length1 / 3;
      indexedPixels = new byte[length2];
      NeuQuant neuQuant1 = new NeuQuant(pixels, length1, sample);
      colorTab = neuQuant1.Process();
      int num1 = 0;
      for (int index1 = 0; index1 < length2; ++index1)
      {
        NeuQuant neuQuant2 = neuQuant1;
        byte[] pixels1 = pixels;
        int index2 = num1;
        int num2 = index2 + 1;
        int b = pixels1[index2] & byte.MaxValue;
        byte[] pixels2 = pixels;
        int index3 = num2;
        int num3 = index3 + 1;
        int g = pixels2[index3] & byte.MaxValue;
        byte[] pixels3 = pixels;
        int index4 = num3;
        num1 = index4 + 1;
        int r = pixels3[index4] & byte.MaxValue;
        int index5 = neuQuant2.Map(b, g, r);
        usedEntry[index5] = true;
        indexedPixels[index1] = (byte) index5;
      }
      pixels = null;
      colorDepth = 8;
      palSize = 7;
      if (!(transparent != Color.clear))
        return;
      transIndex = FindClosest(transparent);
    }

    protected int FindClosest(Color32 c)
    {
      if (colorTab == null)
        return -1;
      int r = c.r;
      int g = c.g;
      int b = c.b;
      int closest = 0;
      int num1 = 16777216;
      int length = colorTab.Length;
      int index1;
      for (int index2 = 0; index2 < length; index2 = index1 + 1)
      {
        int num2 = r;
        byte[] colorTab1 = colorTab;
        int index3 = index2;
        int num3 = index3 + 1;
        int num4 = colorTab1[index3] & byte.MaxValue;
        int num5 = num2 - num4;
        int num6 = g;
        byte[] colorTab2 = colorTab;
        int index4 = num3;
        index1 = index4 + 1;
        int num7 = colorTab2[index4] & byte.MaxValue;
        int num8 = num6 - num7;
        int num9 = b - (colorTab[index1] & byte.MaxValue);
        int num10 = num5 * num5 + num8 * num8 + num9 * num9;
        int index5 = index1 / 3;
        if (usedEntry[index5] && num10 < num1)
        {
          num1 = num10;
          closest = index5;
        }
      }
      return closest;
    }

    protected void WriteGraphicCtrlExt()
    {
      fs.WriteByte(33);
      fs.WriteByte(249);
      fs.WriteByte(4);
      int num1;
      int num2;
      if (transparent == Color.clear)
      {
        num1 = 0;
        num2 = 0;
      }
      else
      {
        num1 = 1;
        num2 = 2;
      }
      if (dispose >= 0)
        num2 = dispose & 7;
      fs.WriteByte(Convert.ToByte(0 | num2 << 2 | 0 | num1));
      WriteShort(delay);
      fs.WriteByte(Convert.ToByte(transIndex));
      fs.WriteByte(0);
    }

    protected void WriteImageDesc()
    {
      fs.WriteByte(44);
      WriteShort(0);
      WriteShort(0);
      WriteShort(width);
      WriteShort(height);
      if (firstFrame)
        fs.WriteByte(0);
      else
        fs.WriteByte(Convert.ToByte(128 | palSize));
    }

    protected void WriteLSD()
    {
      WriteShort(width);
      WriteShort(height);
      fs.WriteByte(Convert.ToByte(240 | palSize));
      fs.WriteByte(0);
      fs.WriteByte(0);
    }

    protected void WriteNetscapeExt()
    {
      fs.WriteByte(33);
      fs.WriteByte(byte.MaxValue);
      fs.WriteByte(11);
      WriteString("NETSCAPE2.0");
      fs.WriteByte(3);
      fs.WriteByte(1);
      WriteShort(repeat);
      fs.WriteByte(0);
    }

    protected void WritePalette()
    {
      fs.Write(colorTab, 0, colorTab.Length);
      int num = 768 - colorTab.Length;
      for (int index = 0; index < num; ++index)
        fs.WriteByte(0);
    }

    protected void WritePixels() => new LZWEncoder(width, height, indexedPixels, colorDepth).Encode(fs);

    protected void WriteShort(int value)
    {
      fs.WriteByte(Convert.ToByte(value & byte.MaxValue));
      fs.WriteByte(Convert.ToByte(value >> 8 & byte.MaxValue));
    }

    protected void WriteString(string s)
    {
      foreach (byte num in s.ToCharArray())
        fs.WriteByte(num);
    }
  }
}
