// Decompiled with JetBrains decompiler
// Type: rail.RailCrashBufferImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class RailCrashBufferImpl : RailObject, RailCrashBuffer
  {
    internal RailCrashBufferImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~RailCrashBufferImpl()
    {
    }

    public virtual string GetData() => RAIL_API_PINVOKE.RailCrashBuffer_GetData(swigCPtr_);

    public virtual uint GetBufferLength() => RAIL_API_PINVOKE.RailCrashBuffer_GetBufferLength(swigCPtr_);

    public virtual uint GetValidLength() => RAIL_API_PINVOKE.RailCrashBuffer_GetValidLength(swigCPtr_);

    public virtual uint SetData(string data, uint length, uint offset) => RAIL_API_PINVOKE.RailCrashBuffer_SetData__SWIG_0(swigCPtr_, data, length, offset);

    public virtual uint SetData(string data, uint length) => RAIL_API_PINVOKE.RailCrashBuffer_SetData__SWIG_1(swigCPtr_, data, length);

    public virtual uint AppendData(string data, uint length) => RAIL_API_PINVOKE.RailCrashBuffer_AppendData(swigCPtr_, data, length);
  }
}
