// Decompiled with JetBrains decompiler
// Type: vp_FPWeaponReloader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (vp_FPWeapon))]
public class vp_FPWeaponReloader : vp_WeaponReloader
{
  public AnimationClip AnimationReload;

  protected override void OnStart_Reload()
  {
    base.OnStart_Reload();
    if (AnimationReload == null)
      return;
    if (m_Player.Reload.AutoDuration == 0.0)
      m_Player.Reload.AutoDuration = AnimationReload.length;
    ((vp_FPWeapon) m_Weapon).WeaponModel.GetComponent<Animation>().CrossFade(AnimationReload.name);
  }
}
