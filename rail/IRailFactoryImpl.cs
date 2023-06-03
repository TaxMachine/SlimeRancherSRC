// Decompiled with JetBrains decompiler
// Type: rail.IRailFactoryImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailFactoryImpl : RailObject, IRailFactory
  {
    internal IRailFactoryImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailFactoryImpl()
    {
    }

    public virtual IRailPlayer RailPlayer()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailPlayer(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailPlayerImpl(cPtr) : (IRailPlayer) null;
    }

    public virtual IRailUsersHelper RailUsersHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailUsersHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailUsersHelperImpl(cPtr) : (IRailUsersHelper) null;
    }

    public virtual IRailFriends RailFriends()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailFriends(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailFriendsImpl(cPtr) : (IRailFriends) null;
    }

    public virtual IRailFloatingWindow RailFloatingWindow()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailFloatingWindow(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailFloatingWindowImpl(cPtr) : (IRailFloatingWindow) null;
    }

    public virtual IRailBrowserHelper RailBrowserHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailBrowserHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailBrowserHelperImpl(cPtr) : (IRailBrowserHelper) null;
    }

    public virtual IRailInGamePurchase RailInGamePurchase()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailInGamePurchase(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailInGamePurchaseImpl(cPtr) : (IRailInGamePurchase) null;
    }

    public virtual IRailZoneHelper RailZoneHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailZoneHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailZoneHelperImpl(cPtr) : (IRailZoneHelper) null;
    }

    public virtual IRailRoomHelper RailRoomHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailRoomHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailRoomHelperImpl(cPtr) : (IRailRoomHelper) null;
    }

    public virtual IRailGameServerHelper RailGameServerHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailGameServerHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailGameServerHelperImpl(cPtr) : (IRailGameServerHelper) null;
    }

    public virtual IRailStorageHelper RailStorageHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailStorageHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailStorageHelperImpl(cPtr) : (IRailStorageHelper) null;
    }

    public virtual IRailUserSpaceHelper RailUserSpaceHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailUserSpaceHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailUserSpaceHelperImpl(cPtr) : (IRailUserSpaceHelper) null;
    }

    public virtual IRailStatisticHelper RailStatisticHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailStatisticHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailStatisticHelperImpl(cPtr) : (IRailStatisticHelper) null;
    }

    public virtual IRailLeaderboardHelper RailLeaderboardHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailLeaderboardHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailLeaderboardHelperImpl(cPtr) : (IRailLeaderboardHelper) null;
    }

    public virtual IRailAchievementHelper RailAchievementHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailAchievementHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailAchievementHelperImpl(cPtr) : (IRailAchievementHelper) null;
    }

    public virtual IRailNetChannel RailNetChannelHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailNetChannelHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailNetChannelImpl(cPtr) : (IRailNetChannel) null;
    }

    public virtual IRailNetwork RailNetworkHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailNetworkHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailNetworkImpl(cPtr) : (IRailNetwork) null;
    }

    public virtual IRailApps RailApps()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailApps(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailAppsImpl(cPtr) : (IRailApps) null;
    }

    public virtual IRailUtils RailUtils()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailUtils(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailUtilsImpl(cPtr) : (IRailUtils) null;
    }

    public virtual IRailAssetsHelper RailAssetsHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailAssetsHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailAssetsHelperImpl(cPtr) : (IRailAssetsHelper) null;
    }

    public virtual IRailDlcHelper RailDlcHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailDlcHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailDlcHelperImpl(cPtr) : (IRailDlcHelper) null;
    }

    public virtual IRailScreenshotHelper RailScreenshotHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailScreenshotHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailScreenshotHelperImpl(cPtr) : (IRailScreenshotHelper) null;
    }

    public virtual IRailVoiceHelper RailVoiceHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailVoiceHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailVoiceHelperImpl(cPtr) : (IRailVoiceHelper) null;
    }

    public virtual IRailSystemHelper RailSystemHelper()
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailFactory_RailSystemHelper(swigCPtr_);
      return !(cPtr == IntPtr.Zero) ? new IRailSystemHelperImpl(cPtr) : (IRailSystemHelper) null;
    }
  }
}
