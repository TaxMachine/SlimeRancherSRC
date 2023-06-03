// Decompiled with JetBrains decompiler
// Type: vp_WeaponReloader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_WeaponReloader : MonoBehaviour, EventHandlerRegistrable
{
  protected vp_Weapon m_Weapon;
  protected vp_PlayerEventHandler m_Player;
  protected AudioSource m_Audio;
  public AudioClip SoundReload;
  public float ReloadDuration = 1f;

  protected virtual void Awake()
  {
    m_Audio = GetComponent<AudioSource>();
    m_Player = (vp_PlayerEventHandler) transform.root.GetComponentInChildren(typeof (vp_PlayerEventHandler));
  }

  protected virtual void Start() => m_Weapon = transform.GetComponent<vp_Weapon>();

  protected virtual void OnEnable()
  {
    if (!(m_Player != null))
      return;
    Register(m_Player);
  }

  protected virtual void OnDisable()
  {
    if (!(m_Player != null))
      return;
    Unregister(m_Player);
  }

  protected virtual bool CanStart_Reload() => m_Player.CurrentWeaponWielded.Get() && (m_Player.CurrentWeaponMaxAmmoCount.Get() == 0 || m_Player.CurrentWeaponAmmoCount.Get() != m_Player.CurrentWeaponMaxAmmoCount.Get()) && m_Player.CurrentWeaponClipCount.Get() >= 1;

  protected virtual void OnStart_Reload()
  {
    m_Player.Reload.AutoDuration = m_Player.CurrentWeaponReloadDuration.Get();
    if (!(m_Audio != null))
      return;
    m_Audio.pitch = Time.timeScale;
    m_Audio.PlayOneShot(SoundReload);
  }

  protected virtual void OnStop_Reload()
  {
    int num = m_Player.RefillCurrentWeapon.Try() ? 1 : 0;
  }

  protected virtual float Get_CurrentWeaponReloadDuration() => ReloadDuration;

  protected virtual float OnValue_CurrentWeaponReloadDuration => ReloadDuration;

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterValue("CurrentWeaponReloadDuration", Get_CurrentWeaponReloadDuration, null);
    eventHandler.RegisterActivity("Reload", OnStart_Reload, OnStop_Reload, CanStart_Reload, null, null, null);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterValue("CurrentWeaponReloadDuration", Get_CurrentWeaponReloadDuration, null);
    eventHandler.UnregisterActivity("Reload", OnStart_Reload, OnStop_Reload, CanStart_Reload, null, null, null);
  }
}
