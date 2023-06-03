// Decompiled with JetBrains decompiler
// Type: rail.IRailNetworkImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailNetworkImpl : RailObject, IRailNetwork
  {
    internal IRailNetworkImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailNetworkImpl()
    {
    }

    public virtual RailResult AcceptSessionRequest(RailID local_peer, RailID remote_peer)
    {
      IntPtr num1 = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num1);
      IntPtr num2 = remote_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (remote_peer != null)
        RailConverter.Csharp2Cpp(remote_peer, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_AcceptSessionRequest(swigCPtr_, num1, num2);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual RailResult SendData(
      RailID local_peer,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len,
      uint message_type)
    {
      IntPtr num1 = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num1);
      IntPtr num2 = remote_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (remote_peer != null)
        RailConverter.Csharp2Cpp(remote_peer, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_SendData__SWIG_0(swigCPtr_, num1, num2, data_buf, data_len, message_type);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual RailResult SendData(
      RailID local_peer,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len)
    {
      IntPtr num1 = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num1);
      IntPtr num2 = remote_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (remote_peer != null)
        RailConverter.Csharp2Cpp(remote_peer, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_SendData__SWIG_1(swigCPtr_, num1, num2, data_buf, data_len);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual RailResult SendReliableData(
      RailID local_peer,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len,
      uint message_type)
    {
      IntPtr num1 = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num1);
      IntPtr num2 = remote_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (remote_peer != null)
        RailConverter.Csharp2Cpp(remote_peer, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_SendReliableData__SWIG_0(swigCPtr_, num1, num2, data_buf, data_len, message_type);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual RailResult SendReliableData(
      RailID local_peer,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len)
    {
      IntPtr num1 = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num1);
      IntPtr num2 = remote_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (remote_peer != null)
        RailConverter.Csharp2Cpp(remote_peer, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_SendReliableData__SWIG_1(swigCPtr_, num1, num2, data_buf, data_len);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual bool IsDataReady(RailID local_peer, out uint data_len, out uint message_type)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailNetwork_IsDataReady__SWIG_0(swigCPtr_, num, out data_len, out message_type);
      }
      finally
      {
        if (local_peer != null)
          RailConverter.Cpp2Csharp(num, local_peer);
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual bool IsDataReady(RailID local_peer, out uint data_len)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailNetwork_IsDataReady__SWIG_1(swigCPtr_, num, out data_len);
      }
      finally
      {
        if (local_peer != null)
          RailConverter.Cpp2Csharp(num, local_peer);
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult ReadData(
      RailID local_peer,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len,
      uint message_type)
    {
      IntPtr num1 = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num1);
      IntPtr num2 = remote_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_ReadData__SWIG_0(swigCPtr_, num1, num2, data_buf, data_len, message_type);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        if (remote_peer != null)
          RailConverter.Cpp2Csharp(num2, remote_peer);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual RailResult ReadData(
      RailID local_peer,
      RailID remote_peer,
      byte[] data_buf,
      uint data_len)
    {
      IntPtr num1 = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num1);
      IntPtr num2 = remote_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_ReadData__SWIG_1(swigCPtr_, num1, num2, data_buf, data_len);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        if (remote_peer != null)
          RailConverter.Cpp2Csharp(num2, remote_peer);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual RailResult BlockMessageType(RailID local_peer, uint message_type)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_BlockMessageType(swigCPtr_, num, message_type);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult UnblockMessageType(RailID local_peer, uint message_type)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_UnblockMessageType(swigCPtr_, num, message_type);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult CloseSession(RailID local_peer, RailID remote_peer)
    {
      IntPtr num1 = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num1);
      IntPtr num2 = remote_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (remote_peer != null)
        RailConverter.Csharp2Cpp(remote_peer, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_CloseSession(swigCPtr_, num1, num2);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual RailResult ResolveHostname(string domain, List<string> ip_list)
    {
      IntPtr num = ip_list == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetwork_ResolveHostname(swigCPtr_, domain, num);
      }
      finally
      {
        if (ip_list != null)
          RailConverter.Cpp2Csharp(num, ip_list);
        RAIL_API_PINVOKE.delete_RailArrayRailString(num);
      }
    }
  }
}
