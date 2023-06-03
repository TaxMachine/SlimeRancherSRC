// Decompiled with JetBrains decompiler
// Type: rail.IRailFileImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailFileImpl : RailObject, IRailFile, IRailComponent
  {
    internal IRailFileImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailFileImpl()
    {
    }

    public virtual string GetFilename() => RAIL_API_PINVOKE.IRailFile_GetFilename(swigCPtr_);

    public virtual uint Read(byte[] buff, uint bytes_to_read, out int result) => RAIL_API_PINVOKE.IRailFile_Read__SWIG_0(swigCPtr_, buff, bytes_to_read, out result);

    public virtual uint Read(byte[] buff, uint bytes_to_read) => RAIL_API_PINVOKE.IRailFile_Read__SWIG_1(swigCPtr_, buff, bytes_to_read);

    public virtual uint Write(byte[] buff, uint bytes_to_write, out int result) => RAIL_API_PINVOKE.IRailFile_Write__SWIG_0(swigCPtr_, buff, bytes_to_write, out result);

    public virtual uint Write(byte[] buff, uint bytes_to_write) => RAIL_API_PINVOKE.IRailFile_Write__SWIG_1(swigCPtr_, buff, bytes_to_write);

    public virtual RailResult AsyncRead(uint bytes_to_read, string user_data) => (RailResult) RAIL_API_PINVOKE.IRailFile_AsyncRead(swigCPtr_, bytes_to_read, user_data);

    public virtual RailResult AsyncWrite(byte[] buffer, uint bytes_to_write, string user_data) => (RailResult) RAIL_API_PINVOKE.IRailFile_AsyncWrite(swigCPtr_, buffer, bytes_to_write, user_data);

    public virtual uint GetSize() => RAIL_API_PINVOKE.IRailFile_GetSize(swigCPtr_);

    public virtual void Close() => RAIL_API_PINVOKE.IRailFile_Close(swigCPtr_);

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
