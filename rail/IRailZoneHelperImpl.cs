// Decompiled with JetBrains decompiler
// Type: rail.IRailZoneHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailZoneHelperImpl : RailObject, IRailZoneHelper
  {
    internal IRailZoneHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailZoneHelperImpl()
    {
    }

    public virtual RailResult AsyncGetZoneList(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailZoneHelper_AsyncGetZoneList(swigCPtr_, user_data);

    public virtual RailResult AsyncGetRoomListInZone(
      ulong zone_id,
      uint start_index,
      uint end_index,
      List<RoomInfoListSorter> sorter,
      List<RoomInfoListFilter> filter,
      string user_data)
    {
      IntPtr num1 = sorter == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRoomInfoListSorter__SWIG_0();
      if (sorter != null)
        RailConverter.Csharp2Cpp(sorter, num1);
      IntPtr num2 = filter == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRoomInfoListFilter__SWIG_0();
      if (filter != null)
        RailConverter.Csharp2Cpp(filter, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailZoneHelper_AsyncGetRoomListInZone(swigCPtr_, zone_id, start_index, end_index, num1, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRoomInfoListSorter(num1);
        RAIL_API_PINVOKE.delete_RailArrayRoomInfoListFilter(num2);
      }
    }
  }
}
