﻿// Decompiled with JetBrains decompiler
// Type: rail.IRailScreenshotHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailScreenshotHelperImpl : RailObject, IRailScreenshotHelper
  {
    internal IRailScreenshotHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailScreenshotHelperImpl()
    {
    }

    public virtual IRailScreenshot CreateScreenshotWithRawData(
      byte[] rgb_data,
      uint len,
      uint width,
      uint height)
    {
      IntPtr screenshotWithRawData = RAIL_API_PINVOKE.IRailScreenshotHelper_CreateScreenshotWithRawData(swigCPtr_, rgb_data, len, width, height);
      return !(screenshotWithRawData == IntPtr.Zero) ? new IRailScreenshotImpl(screenshotWithRawData) : (IRailScreenshot) null;
    }

    public virtual IRailScreenshot CreateScreenshotWithLocalImage(
      string image_file,
      string thumbnail_file)
    {
      IntPtr screenshotWithLocalImage = RAIL_API_PINVOKE.IRailScreenshotHelper_CreateScreenshotWithLocalImage(swigCPtr_, image_file, thumbnail_file);
      return !(screenshotWithLocalImage == IntPtr.Zero) ? new IRailScreenshotImpl(screenshotWithLocalImage) : (IRailScreenshot) null;
    }

    public virtual void AsyncTakeScreenshot(string user_data) => RAIL_API_PINVOKE.IRailScreenshotHelper_AsyncTakeScreenshot(swigCPtr_, user_data);

    public virtual void HookScreenshotHotKey(bool hook) => RAIL_API_PINVOKE.IRailScreenshotHelper_HookScreenshotHotKey(swigCPtr_, hook);
  }
}
