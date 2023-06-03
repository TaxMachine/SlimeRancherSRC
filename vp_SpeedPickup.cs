// Decompiled with JetBrains decompiler
// Type: vp_SpeedPickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class vp_SpeedPickup : vp_Pickup
{
  protected vp_Timer.Handle m_Timer = new vp_Timer.Handle();

  protected override void Update()
  {
    UpdateMotion();
    if (!m_Depleted || m_Audio.isPlaying)
      return;
    Remove();
  }

  protected override bool TryGive(vp_FPPlayerEventHandler player)
  {
    if (m_Timer.Active)
      return false;
    player.SetState("MegaSpeed");
    vp_Timer.In(RespawnDuration, () => player.SetState("MegaSpeed", false), m_Timer);
    return true;
  }
}
