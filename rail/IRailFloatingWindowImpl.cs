// Decompiled with JetBrains decompiler
// Type: rail.IRailFloatingWindowImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailFloatingWindowImpl : RailObject, IRailFloatingWindow
  {
    internal IRailFloatingWindowImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailFloatingWindowImpl()
    {
    }

    public virtual RailResult AsyncShowRailFloatingWindow(
      EnumRailWindowType window_type,
      string user_data)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailFloatingWindow_AsyncShowRailFloatingWindow(swigCPtr_, (int) window_type, user_data);
    }

    public virtual RailResult SetNotifyWindowPosition(
      EnumRailNotifyWindowType window,
      EnumRailNotifyWindowPosition position)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailFloatingWindow_SetNotifyWindowPosition(swigCPtr_, (int) window, (int) position);
    }

    public virtual RailResult AsyncShowStoreWindow(
      ulong id,
      RailStoreOptions options,
      string user_data)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailStoreOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailFloatingWindow_AsyncShowStoreWindow(swigCPtr_, id, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailStoreOptions(num);
      }
    }

    public virtual bool IsFloatingWindowAvailable() => RAIL_API_PINVOKE.IRailFloatingWindow_IsFloatingWindowAvailable(swigCPtr_);

    public virtual RailResult AsyncShowDefaultGameStoreWindow(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailFloatingWindow_AsyncShowDefaultGameStoreWindow(swigCPtr_, user_data);

    public virtual RailResult AsyncShowChatWindowWithFriend(RailID rail_id)
    {
      IntPtr num = rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (rail_id != null)
        RailConverter.Csharp2Cpp(rail_id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailFloatingWindow_AsyncShowChatWindowWithFriend(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }
  }
}
