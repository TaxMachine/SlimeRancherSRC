// Decompiled with JetBrains decompiler
// Type: rail.IRailPlayerImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailPlayerImpl : RailObject, IRailPlayer
  {
    internal IRailPlayerImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailPlayerImpl()
    {
    }

    public virtual bool AlreadyLoggedIn() => RAIL_API_PINVOKE.IRailPlayer_AlreadyLoggedIn(swigCPtr_);

    public virtual RailID GetRailID()
    {
      IntPtr railId1 = RAIL_API_PINVOKE.IRailPlayer_GetRailID(swigCPtr_);
      RailID railId2 = new RailID();
      RailID ret = railId2;
      RailConverter.Cpp2Csharp(railId1, ret);
      return railId2;
    }

    public virtual RailResult GetPlayerDataPath(out string path)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailPlayer_GetPlayerDataPath(swigCPtr_, num);
      }
      finally
      {
        path = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual RailResult AsyncAcquireSessionTicket(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailPlayer_AsyncAcquireSessionTicket(swigCPtr_, user_data);

    public virtual RailResult AsyncStartSessionWithPlayer(
      RailSessionTicket player_ticket,
      RailID player_rail_id,
      string user_data)
    {
      IntPtr num1 = player_ticket == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket();
      if (player_ticket != null)
        RailConverter.Csharp2Cpp(player_ticket, num1);
      IntPtr num2 = player_rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (player_rail_id != null)
        RailConverter.Csharp2Cpp(player_rail_id, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailPlayer_AsyncStartSessionWithPlayer(swigCPtr_, num1, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSessionTicket(num1);
        RAIL_API_PINVOKE.delete_RailID(num2);
      }
    }

    public virtual void TerminateSessionOfPlayer(RailID player_rail_id)
    {
      IntPtr num = player_rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (player_rail_id != null)
        RailConverter.Csharp2Cpp(player_rail_id, num);
      try
      {
        RAIL_API_PINVOKE.IRailPlayer_TerminateSessionOfPlayer(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual void AbandonSessionTicket(RailSessionTicket session_ticket)
    {
      IntPtr num = session_ticket == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket();
      if (session_ticket != null)
        RailConverter.Csharp2Cpp(session_ticket, num);
      try
      {
        RAIL_API_PINVOKE.IRailPlayer_AbandonSessionTicket(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailSessionTicket(num);
      }
    }

    public virtual RailResult GetPlayerName(out string name)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailPlayer_GetPlayerName(swigCPtr_, num);
      }
      finally
      {
        name = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual EnumRailPlayerOwnershipType GetPlayerOwnershipType() => (EnumRailPlayerOwnershipType) RAIL_API_PINVOKE.IRailPlayer_GetPlayerOwnershipType(swigCPtr_);

    public virtual RailResult AsyncGetGamePurchaseKey(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailPlayer_AsyncGetGamePurchaseKey(swigCPtr_, user_data);
  }
}
