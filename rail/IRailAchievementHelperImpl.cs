// Decompiled with JetBrains decompiler
// Type: rail.IRailAchievementHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailAchievementHelperImpl : RailObject, IRailAchievementHelper
  {
    internal IRailAchievementHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailAchievementHelperImpl()
    {
    }

    public virtual IRailPlayerAchievement CreatePlayerAchievement(RailID player)
    {
      IntPtr num = player == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0();
      if (player != null)
        RailConverter.Csharp2Cpp(player, num);
      try
      {
        IntPtr playerAchievement = RAIL_API_PINVOKE.IRailAchievementHelper_CreatePlayerAchievement(swigCPtr_, num);
        return playerAchievement == IntPtr.Zero ? null : (IRailPlayerAchievement) new IRailPlayerAchievementImpl(playerAchievement);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailID(num);
      }
    }

    public virtual IRailGlobalAchievement GetGlobalAchievement()
    {
      IntPtr globalAchievement = RAIL_API_PINVOKE.IRailAchievementHelper_GetGlobalAchievement(swigCPtr_);
      return !(globalAchievement == IntPtr.Zero) ? new IRailGlobalAchievementImpl(globalAchievement) : (IRailGlobalAchievement) null;
    }
  }
}
