// Decompiled with JetBrains decompiler
// Type: rail.rail_api
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class rail_api
  {
    public static readonly int USE_MANUAL_ALLOC = RAIL_API_PINVOKE.USE_MANUAL_ALLOC_get();

    public static int RAIL_DEFAULT_MAX_ROOM_MEMBERS => RAIL_API_PINVOKE.RAIL_DEFAULT_MAX_ROOM_MEMBERS_get();

    public static bool RailNeedRestartAppForCheckingEnvironment(
      RailGameID game_id,
      int argc,
      string[] argv)
    {
      IntPtr num = game_id == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGameID__SWIG_0();
      if (game_id != null)
        RailConverter.Csharp2Cpp(game_id, num);
      try
      {
        return RAIL_API_PINVOKE.RailNeedRestartAppForCheckingEnvironment(num, argc, argv);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailGameID(num);
      }
    }

    public static bool RailInitialize() => RAIL_API_PINVOKE.RailInitialize();

    public static void RailFinalize() => RAIL_API_PINVOKE.RailFinalize();

    public static void RailFireEvents() => RAIL_API_PINVOKE.RailFireEvents();

    public static IRailFactory RailFactory()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.RailFactory();
      return !(cPtr == IntPtr.Zero) ? new IRailFactoryImpl(cPtr) : (IRailFactory) null;
    }

    public static void RailGetSdkVersion(out string version, out string description)
    {
      IntPtr jarg1 = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        RAIL_API_PINVOKE.RailGetSdkVersion(jarg1, num);
      }
      finally
      {
        version = RAIL_API_PINVOKE.RailString_c_str(jarg1);
        RAIL_API_PINVOKE.delete_RailString(jarg1);
        description = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public static void CSharpRailRegisterEvent(
      RAILEventID event_id,
      RailEventCallBackFunction callback)
    {
      RAIL_API_PINVOKE.CSharpRailRegisterEvent((int) event_id, callback);
    }

    public static void CSharpRailUnRegisterEvent(
      RAILEventID event_id,
      RailEventCallBackFunction callback)
    {
      RAIL_API_PINVOKE.CSharpRailUnRegisterEvent((int) event_id, callback);
    }

    public static void CSharpRailUnRegisterAllEvent() => RAIL_API_PINVOKE.CSharpRailUnRegisterAllEvent();
  }
}
