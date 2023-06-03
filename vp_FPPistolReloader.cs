// Decompiled with JetBrains decompiler
// Type: vp_FPPistolReloader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_FPPistolReloader : vp_FPWeaponReloader
{
  private vp_Timer.Handle m_Timer = new vp_Timer.Handle();

  protected override void OnStart_Reload()
  {
    if (m_Weapon.gameObject != gameObject || m_Timer.Active)
      return;
    base.OnStart_Reload();
    vp_Timer.In(0.4f, () =>
    {
      if (!vp_Utility.IsActive(m_Weapon.gameObject) || !m_Weapon.StateEnabled("Reload"))
        return;
      m_Weapon.AddForce2(new Vector3(0.0f, 0.05f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f));
      vp_Timer.In(0.15f, () =>
      {
        if (!vp_Utility.IsActive(m_Weapon.gameObject) || !m_Weapon.StateEnabled("Reload"))
          return;
        m_Weapon.SetState("Reload", false);
        m_Weapon.SetState("Reload2");
        m_Weapon.RotationOffset.z = 0.0f;
        m_Weapon.Refresh();
        vp_Timer.In(0.35f, () =>
        {
          if (!vp_Utility.IsActive(m_Weapon.gameObject) || !m_Weapon.StateEnabled("Reload2"))
            return;
          m_Weapon.AddForce2(new Vector3(0.0f, 0.0f, -0.05f), new Vector3(5f, 0.0f, 0.0f));
          vp_Timer.In(0.1f, () => m_Weapon.SetState("Reload2", false));
        });
      });
    }, m_Timer);
  }
}
