// Decompiled with JetBrains decompiler
// Type: rail.IRailGlobalAchievementImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace rail
{
  public class IRailGlobalAchievementImpl : RailObject, IRailGlobalAchievement, IRailComponent
  {
    internal IRailGlobalAchievementImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailGlobalAchievementImpl()
    {
    }

    public virtual RailResult AsyncRequestAchievement(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailGlobalAchievement_AsyncRequestAchievement(swigCPtr_, user_data);

    public virtual RailResult GetGlobalAchievedPercent(string name, out double percent) => (RailResult) RAIL_API_PINVOKE.IRailGlobalAchievement_GetGlobalAchievedPercent(swigCPtr_, name, out percent);

    public virtual RailResult GetGlobalAchievedPercentDescending(
      int index,
      out string name,
      out double percent)
    {
      IntPtr num = RAIL_API_PINVOKE.new_RailString__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailGlobalAchievement_GetGlobalAchievedPercentDescending(swigCPtr_, index, num, out percent);
      }
      finally
      {
        name = RAIL_API_PINVOKE.RailString_c_str(num);
        RAIL_API_PINVOKE.delete_RailString(num);
      }
    }

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
