// Decompiled with JetBrains decompiler
// Type: rail.IRailBrowserImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailBrowserImpl : RailObject, IRailBrowser, IRailComponent
  {
    internal IRailBrowserImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailBrowserImpl()
    {
    }

    public virtual bool GetCurrentUrl(out string url)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailBrowser_GetCurrentUrl(swigCPtr_, num);
      }
      finally
      {
        url = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual bool ReloadWithUrl(string new_url) => RAIL_API_PINVOKE.IRailBrowser_ReloadWithUrl__SWIG_0(swigCPtr_, new_url);

    public virtual bool ReloadWithUrl() => RAIL_API_PINVOKE.IRailBrowser_ReloadWithUrl__SWIG_1(swigCPtr_);

    public virtual void StopLoad() => RAIL_API_PINVOKE.IRailBrowser_StopLoad(swigCPtr_);

    public virtual bool AddJavascriptEventListener(string event_name) => RAIL_API_PINVOKE.IRailBrowser_AddJavascriptEventListener(swigCPtr_, event_name);

    public virtual bool RemoveAllJavascriptEventListener() => RAIL_API_PINVOKE.IRailBrowser_RemoveAllJavascriptEventListener(swigCPtr_);

    public virtual void AllowNavigateNewPage(bool allow) => RAIL_API_PINVOKE.IRailBrowser_AllowNavigateNewPage(swigCPtr_, allow);

    public virtual void Close() => RAIL_API_PINVOKE.IRailBrowser_Close(swigCPtr_);

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
