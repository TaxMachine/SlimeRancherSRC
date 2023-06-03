// Decompiled with JetBrains decompiler
// Type: vp_Gameplay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class vp_Gameplay
{
  public static bool isMultiplayer = false;
  protected static bool m_IsMaster = true;

  public static bool isMaster
  {
    get => !isMultiplayer || m_IsMaster;
    set
    {
      if (!isMultiplayer)
        return;
      m_IsMaster = value;
    }
  }
}
