// Decompiled with JetBrains decompiler
// Type: vp_Activity`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class vp_Activity<V> : vp_Activity
{
  public vp_Activity(string name)
    : base(name)
  {
  }

  public bool TryStart<T>(T argument)
  {
    if (m_Active)
      return false;
    m_Argument = argument;
    return TryStart();
  }
}
