// Decompiled with JetBrains decompiler
// Type: rail.IRailRoomHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailRoomHelperImpl : RailObject, IRailRoomHelper
  {
    internal IRailRoomHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailRoomHelperImpl()
    {
    }

    public virtual void set_current_zone_id(ulong zone_id) => RAIL_API_PINVOKE.IRailRoomHelper_set_current_zone_id(swigCPtr_, zone_id);

    public virtual ulong get_current_zone_id() => RAIL_API_PINVOKE.IRailRoomHelper_get_current_zone_id(swigCPtr_);

    public virtual IRailRoom CreateRoom(RoomOptions options, string room_name, out int result)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RoomOptions__SWIG_0(0UL);
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr room = RAIL_API_PINVOKE.IRailRoomHelper_CreateRoom(swigCPtr_, num, room_name, out result);
        return room == IntPtr.Zero ? null : (IRailRoom) new IRailRoomImpl(room);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RoomOptions(num);
      }
    }

    public virtual IRailRoom AsyncCreateRoom(
      RoomOptions options,
      string room_name,
      string user_data)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RoomOptions__SWIG_0(0UL);
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr room = RAIL_API_PINVOKE.IRailRoomHelper_AsyncCreateRoom(swigCPtr_, num, room_name, user_data);
        return room == IntPtr.Zero ? null : (IRailRoom) new IRailRoomImpl(room);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RoomOptions(num);
      }
    }

    public virtual IRailRoom OpenRoom(ulong zone_id, ulong room_id, out int result)
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailRoomHelper_OpenRoom(swigCPtr_, zone_id, room_id, out result);
      return !(cPtr == IntPtr.Zero) ? new IRailRoomImpl(cPtr) : (IRailRoom) null;
    }

    public virtual RailResult AsyncGetUserRoomList(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailRoomHelper_AsyncGetUserRoomList(swigCPtr_, user_data);
  }
}
