// Decompiled with JetBrains decompiler
// Type: rail.IRailScreenshotImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailScreenshotImpl : RailObject, IRailScreenshot, IRailComponent
  {
    internal IRailScreenshotImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailScreenshotImpl()
    {
    }

    public virtual bool SetLocation(string location) => RAIL_API_PINVOKE.IRailScreenshot_SetLocation(swigCPtr_, location);

    public virtual bool SetUsers(List<RailID> users)
    {
      IntPtr num = users == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      if (users != null)
        RailConverter.Csharp2Cpp(users, num);
      try
      {
        return RAIL_API_PINVOKE.IRailScreenshot_SetUsers(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailID(num);
      }
    }

    public virtual bool AssociatePublishedFiles(List<SpaceWorkID> work_files)
    {
      IntPtr num = work_files == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0();
      if (work_files != null)
        RailConverter.Csharp2Cpp(work_files, num);
      try
      {
        return RAIL_API_PINVOKE.IRailScreenshot_AssociatePublishedFiles(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(num);
      }
    }

    public virtual RailResult AsyncPublishScreenshot(string work_name, string user_data) => (RailResult) RAIL_API_PINVOKE.IRailScreenshot_AsyncPublishScreenshot(swigCPtr_, work_name, user_data);

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
