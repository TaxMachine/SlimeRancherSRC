// Decompiled with JetBrains decompiler
// Type: rail.IRailAssetsHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailAssetsHelperImpl : RailObject, IRailAssetsHelper
  {
    internal IRailAssetsHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailAssetsHelperImpl()
    {
    }

    public virtual IRailAssets OpenAssets()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailAssetsHelper_OpenAssets(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailAssetsImpl(cPtr) : (IRailAssets) null;
    }
  }
}
