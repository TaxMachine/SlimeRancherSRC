// Decompiled with JetBrains decompiler
// Type: rail.IRailNetChannelImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailNetChannelImpl : RailObject, IRailNetChannel
  {
    internal IRailNetChannelImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailNetChannelImpl()
    {
    }

    public virtual RailResult AsyncCreateChannel(RailID local_peer, string user_data)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_AsyncCreateChannel(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult AsyncJoinChannel(
      RailID local_peer,
      ulong channel_id,
      string user_data)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_AsyncJoinChannel(swigCPtr_, num, channel_id, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult AsyncInviteMemberToChannel(
      RailID local_peer,
      ulong channel_id,
      List<RailID> remote_peers,
      string user_data)
    {
      IntPtr num1 = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num1);
      IntPtr num2 = remote_peers == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      if (remote_peers != null)
        RailConverter.Csharp2Cpp(remote_peers, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_AsyncInviteMemberToChannel(swigCPtr_, num1, channel_id, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailArrayRailID(num2);
      }
    }

    public virtual RailResult GetAllMembers(
      RailID local_peer,
      ulong channel_id,
      List<RailID> remote_peers)
    {
      IntPtr num1 = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num1);
      IntPtr num2 = remote_peers == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_GetAllMembers(swigCPtr_, num1, channel_id, num2);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        if (remote_peers != null)
          RailConverter.Cpp2Csharp(num2, remote_peers);
        RAIL_API_PINVOKE.delete_RailArrayRailID(num2);
      }
    }

    public virtual RailResult SendDataToChannel(
      RailID local_peer,
      ulong channel_id,
      byte[] data_buf,
      uint data_len,
      uint message_type)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_SendDataToChannel__SWIG_0(swigCPtr_, num, channel_id, data_buf, data_len, message_type);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult SendDataToChannel(
      RailID local_peer,
      ulong channel_id,
      byte[] data_buf,
      uint data_len)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_SendDataToChannel__SWIG_1(swigCPtr_, num, channel_id, data_buf, data_len);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult SendDataToMember(
      RailID local_peer,
      ulong channel_id,
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
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_SendDataToMember__SWIG_0(swigCPtr_, num1, channel_id, num2, data_buf, data_len, message_type);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual RailResult SendDataToMember(
      RailID local_peer,
      ulong channel_id,
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
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_SendDataToMember__SWIG_1(swigCPtr_, num1, channel_id, num2, data_buf, data_len);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual bool IsDataReady(
      RailID local_peer,
      out ulong channel_id,
      out uint data_len,
      out uint message_type)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailNetChannel_IsDataReady__SWIG_0(swigCPtr_, num, out channel_id, out data_len, out message_type);
      }
      finally
      {
        if (local_peer != null)
          RailConverter.Cpp2Csharp(num, local_peer);
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual bool IsDataReady(RailID local_peer, out ulong channel_id, out uint data_len)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      try
      {
        return RAIL_API_PINVOKE.IRailNetChannel_IsDataReady__SWIG_1(swigCPtr_, num, out channel_id, out data_len);
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
      ulong channel_id,
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
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_ReadData__SWIG_0(swigCPtr_, num1, channel_id, num2, data_buf, data_len, message_type);
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
      ulong channel_id,
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
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_ReadData__SWIG_1(swigCPtr_, num1, channel_id, num2, data_buf, data_len);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num1);
        if (remote_peer != null)
          RailConverter.Cpp2Csharp(num2, remote_peer);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual RailResult BlockMessageType(
      RailID local_peer,
      ulong channel_id,
      uint message_type)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_BlockMessageType(swigCPtr_, num, channel_id, message_type);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult UnblockMessageType(
      RailID local_peer,
      ulong channel_id,
      uint message_type)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_UnblockMessageType(swigCPtr_, num, channel_id, message_type);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult ExitChannel(RailID local_peer, ulong channel_id)
    {
      IntPtr num = local_peer == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (local_peer != null)
        RailConverter.Csharp2Cpp(local_peer, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailNetChannel_ExitChannel(swigCPtr_, num, channel_id);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }
  }
}
