// Decompiled with JetBrains decompiler
// Type: rail.IRailLeaderboardHelperImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailLeaderboardHelperImpl : RailObject, IRailLeaderboardHelper
  {
    internal IRailLeaderboardHelperImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailLeaderboardHelperImpl()
    {
    }

    public virtual IRailLeaderboard OpenLeaderboard(string leaderboard_name)
    {
      IntPtr cPtr = RAIL_API_PINVOKE.IRailLeaderboardHelper_OpenLeaderboard(swigCPtr_, leaderboard_name);
      return !(cPtr == IntPtr.Zero) ? new IRailLeaderboardImpl(cPtr) : (IRailLeaderboard) null;
    }
  }
}
