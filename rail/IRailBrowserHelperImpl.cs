// Decompiled with JetBrains decompiler
// Type: rail.IRailBrowserHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailBrowserHelperImpl : RailObject, IRailBrowserHelper
  {
    internal IRailBrowserHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailBrowserHelperImpl()
    {
    }

    public virtual IRailBrowser AsyncCreateBrowser(
      string url,
      uint window_width,
      uint window_height,
      string user_data,
      CreateBrowserOptions options,
      out int result)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateBrowserOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr browserSwig0 = RAIL_API_PINVOKE.IRailBrowserHelper_AsyncCreateBrowser__SWIG_0(swigCPtr_, url, window_width, window_height, user_data, num, out result);
        return browserSwig0 == IntPtr.Zero ? null : (IRailBrowser) new IRailBrowserImpl(browserSwig0);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateBrowserOptions(num);
      }
    }

    public virtual IRailBrowser AsyncCreateBrowser(
      string url,
      uint window_width,
      uint window_height,
      string user_data,
      CreateBrowserOptions options)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateBrowserOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr browserSwig1 = RAIL_API_PINVOKE.IRailBrowserHelper_AsyncCreateBrowser__SWIG_1(swigCPtr_, url, window_width, window_height, user_data, num);
        return browserSwig1 == IntPtr.Zero ? null : (IRailBrowser) new IRailBrowserImpl(browserSwig1);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateBrowserOptions(num);
      }
    }

    public virtual IRailBrowser AsyncCreateBrowser(
      string url,
      uint window_width,
      uint window_height,
      string user_data)
    {
      IntPtr browserSwig2 = RAIL_API_PINVOKE.IRailBrowserHelper_AsyncCreateBrowser__SWIG_2(swigCPtr_, url, window_width, window_height, user_data);
      return !(browserSwig2 == IntPtr.Zero) ? new IRailBrowserImpl(browserSwig2) : (IRailBrowser) null;
    }

    public virtual IRailBrowserRender CreateCustomerDrawBrowser(
      string url,
      string user_data,
      CreateCustomerDrawBrowserOptions options,
      out int result)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateCustomerDrawBrowserOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr drawBrowserSwig0 = RAIL_API_PINVOKE.IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_0(swigCPtr_, url, user_data, num, out result);
        return drawBrowserSwig0 == IntPtr.Zero ? null : (IRailBrowserRender) new IRailBrowserRenderImpl(drawBrowserSwig0);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateCustomerDrawBrowserOptions(num);
      }
    }

    public virtual IRailBrowserRender CreateCustomerDrawBrowser(
      string url,
      string user_data,
      CreateCustomerDrawBrowserOptions options)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateCustomerDrawBrowserOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr drawBrowserSwig1 = RAIL_API_PINVOKE.IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_1(swigCPtr_, url, user_data, num);
        return drawBrowserSwig1 == IntPtr.Zero ? null : (IRailBrowserRender) new IRailBrowserRenderImpl(drawBrowserSwig1);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateCustomerDrawBrowserOptions(num);
      }
    }

    public virtual IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data)
    {
      IntPtr drawBrowserSwig2 = RAIL_API_PINVOKE.IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_2(swigCPtr_, url, user_data);
      return !(drawBrowserSwig2 == IntPtr.Zero) ? new IRailBrowserRenderImpl(drawBrowserSwig2) : (IRailBrowserRender) null;
    }

    public virtual RailResult NavigateWebPage(string url, bool display_in_new_tab) => (RailResult) RAIL_API_PINVOKE.IRailBrowserHelper_NavigateWebPage(swigCPtr_, url, display_in_new_tab);
  }
}
