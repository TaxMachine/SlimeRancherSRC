// Decompiled with JetBrains decompiler
// Type: vp_SlomoPickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_SlomoPickup : vp_Pickup
{
  private vp_FPPlayerEventHandler m_Player;

  protected override void Update()
  {
    UpdateMotion();
    if (m_Depleted)
    {
      if (m_Player != null && m_Player.Dead.Active && !m_RespawnTimer.Active)
        Respawn();
      else if (Time.timeScale > 0.20000000298023224 && !vp_TimeUtility.Paused)
      {
        vp_TimeUtility.FadeTimeScale(0.2f, 0.1f);
      }
      else
      {
        if (m_Audio.isPlaying)
          return;
        Remove();
      }
    }
    else
    {
      if (Time.timeScale >= 1.0 || vp_TimeUtility.Paused)
        return;
      vp_TimeUtility.FadeTimeScale(1f, 0.05f);
    }
  }

  protected override bool TryGive(vp_FPPlayerEventHandler player)
  {
    m_Player = player;
    return !m_Depleted && Time.timeScale == 1.0;
  }
}
