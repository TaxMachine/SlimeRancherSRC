// Decompiled with JetBrains decompiler
// Type: rail.IRailGameServerHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailGameServerHelperImpl : RailObject, IRailGameServerHelper
  {
    internal IRailGameServerHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailGameServerHelperImpl()
    {
    }

    public virtual RailResult AsyncGetGameServerPlayerList(
      RailID gameserver_rail_id,
      string user_data)
    {
      IntPtr num = gameserver_rail_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (gameserver_rail_id != null)
        RailConverter.Csharp2Cpp(gameserver_rail_id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetGameServerPlayerList(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult AsyncGetGameServerList(
      uint start_index,
      uint end_index,
      List<GameServerListFilter> alternative_filters,
      List<GameServerListSorter> sorter,
      string user_data)
    {
      IntPtr num1 = alternative_filters == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerListFilter__SWIG_0();
      if (alternative_filters != null)
        RailConverter.Csharp2Cpp(alternative_filters, num1);
      IntPtr num2 = sorter == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerListSorter__SWIG_0();
      if (sorter != null)
        RailConverter.Csharp2Cpp(sorter, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetGameServerList(swigCPtr_, start_index, end_index, num1, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayGameServerListFilter(num1);
        RAIL_API_PINVOKE.delete_RailArrayGameServerListSorter(num2);
      }
    }

    public virtual IRailGameServer AsyncCreateGameServer(
      CreateGameServerOptions options,
      string game_server_name,
      string user_data)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateGameServerOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr gameServerSwig0 = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_0(swigCPtr_, num, game_server_name, user_data);
        return gameServerSwig0 == IntPtr.Zero ? null : (IRailGameServer) new IRailGameServerImpl(gameServerSwig0);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateGameServerOptions(num);
      }
    }

    public virtual IRailGameServer AsyncCreateGameServer(
      CreateGameServerOptions options,
      string game_server_name)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateGameServerOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr gameServerSwig1 = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_1(swigCPtr_, num, game_server_name);
        return gameServerSwig1 == IntPtr.Zero ? null : (IRailGameServer) new IRailGameServerImpl(gameServerSwig1);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateGameServerOptions(num);
      }
    }

    public virtual IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options)
    {
      IntPtr num = options == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateGameServerOptions__SWIG_0();
      if (options != null)
        RailConverter.Csharp2Cpp(options, num);
      try
      {
        IntPtr gameServerSwig2 = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_2(swigCPtr_, num);
        return gameServerSwig2 == IntPtr.Zero ? null : (IRailGameServer) new IRailGameServerImpl(gameServerSwig2);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_CreateGameServerOptions(num);
      }
    }

    public virtual IRailGameServer AsyncCreateGameServer()
    {
      IntPtr gameServerSwig3 = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_3(swigCPtr_);
      return !(gameServerSwig3 == IntPtr.Zero) ? new IRailGameServerImpl(gameServerSwig3) : (IRailGameServer) null;
    }

    public virtual RailResult AsyncGetFavoriteGameServers(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_0(swigCPtr_, user_data);

    public virtual RailResult AsyncGetFavoriteGameServers() => (RailResult) RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_1(swigCPtr_);

    public virtual RailResult AsyncAddFavoriteGameServer(RailID game_server_id, string user_data)
    {
      IntPtr num = game_server_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (game_server_id != null)
        RailConverter.Csharp2Cpp(game_server_id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_0(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult AsyncAddFavoriteGameServer(RailID game_server_id)
    {
      IntPtr num = game_server_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (game_server_id != null)
        RailConverter.Csharp2Cpp(game_server_id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_1(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id, string user_Data)
    {
      IntPtr num = game_server_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (game_server_id != null)
        RailConverter.Csharp2Cpp(game_server_id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_0(swigCPtr_, num, user_Data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id)
    {
      IntPtr num = game_server_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (game_server_id != null)
        RailConverter.Csharp2Cpp(game_server_id, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_1(swigCPtr_, num);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }
  }
}
