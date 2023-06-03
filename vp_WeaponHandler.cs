// Decompiled with JetBrains decompiler
// Type: vp_WeaponHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vp_WeaponHandler : MonoBehaviour, EventHandlerRegistrable
{
  public int StartWeapon;
  public float AttackStateDisableDelay = 0.5f;
  public float SetWeaponRefreshStatesDelay = 0.5f;
  public float SetWeaponDuration = 0.1f;
  public float SetWeaponReloadSleepDuration = 0.3f;
  public float SetWeaponZoomSleepDuration = 0.3f;
  public float SetWeaponAttackSleepDuration = 0.3f;
  public float ReloadAttackSleepDuration = 0.3f;
  public bool ReloadAutomatically = true;
  protected vp_PlayerEventHandler m_Player;
  protected List<vp_Weapon> m_Weapons;
  protected List<List<vp_Weapon>> m_WeaponLists = new List<List<vp_Weapon>>();
  protected int m_CurrentWeaponIndex = -1;
  protected vp_Weapon m_CurrentWeapon;
  protected vp_Timer.Handle m_SetWeaponTimer = new vp_Timer.Handle();
  protected vp_Timer.Handle m_SetWeaponRefreshTimer = new vp_Timer.Handle();
  protected vp_Timer.Handle m_DisableAttackStateTimer = new vp_Timer.Handle();
  protected vp_Timer.Handle m_DisableReloadStateTimer = new vp_Timer.Handle();

  public List<vp_Weapon> Weapons
  {
    get
    {
      if (m_Weapons == null)
        InitWeaponLists();
      return m_Weapons;
    }
    set => m_Weapons = value;
  }

  public vp_Weapon CurrentWeapon => m_CurrentWeapon;

  [Obsolete("Please use the 'CurrentWeaponIndex' parameter instead.")]
  public int CurrentWeaponID => m_CurrentWeaponIndex;

  public int CurrentWeaponIndex => m_CurrentWeaponIndex;

  public vp_Weapon WeaponBeingSet
  {
    get
    {
      if (!m_Player.SetWeapon.Active)
        return null;
      return m_Player.SetWeapon.Argument == null ? null : Weapons[Mathf.Max(0, (int) m_Player.SetWeapon.Argument - 1)];
    }
  }

  protected virtual void Awake()
  {
    m_Player = (vp_PlayerEventHandler) transform.root.GetComponentInChildren(typeof (vp_PlayerEventHandler));
    if (Weapons == null)
      return;
    StartWeapon = Mathf.Clamp(StartWeapon, 0, Weapons.Count);
  }

  protected void InitWeaponLists()
  {
    List<vp_Weapon> vpWeaponList1 = null;
    vp_FPCamera componentInChildren = transform.GetComponentInChildren<vp_FPCamera>();
    if (componentInChildren != null)
    {
      vpWeaponList1 = GetWeaponList(Camera.main.transform);
      if (vpWeaponList1 != null && vpWeaponList1.Count > 0)
        m_WeaponLists.Add(vpWeaponList1);
    }
    List<vp_Weapon> vpWeaponList2 = new List<vp_Weapon>(transform.GetComponentsInChildren<vp_Weapon>());
    if (vpWeaponList1 != null && vpWeaponList1.Count == vpWeaponList2.Count)
    {
      Weapons = m_WeaponLists[0];
    }
    else
    {
      List<Transform> transformList = new List<Transform>();
      foreach (vp_Weapon vpWeapon in vpWeaponList2)
      {
        if ((!(componentInChildren != null) || !vpWeaponList1.Contains(vpWeapon)) && !transformList.Contains(vpWeapon.Parent))
          transformList.Add(vpWeapon.Parent);
      }
      foreach (Transform target in transformList)
      {
        List<vp_Weapon> weaponList = GetWeaponList(target);
        DeactivateAll(weaponList);
        m_WeaponLists.Add(weaponList);
      }
      if (m_WeaponLists.Count < 1)
      {
        Debug.LogError("Error (" + this + ") WeaponHandler found no weapons in its hierarchy. Disabling self.");
        enabled = false;
      }
      else
        Weapons = m_WeaponLists[0];
    }
  }

  public void EnableWeaponList(int index)
  {
    if (m_WeaponLists == null || m_WeaponLists.Count < 1 || index < 0 || index > m_WeaponLists.Count - 1)
      return;
    Weapons = m_WeaponLists[index];
  }

  protected List<vp_Weapon> GetWeaponList(Transform target)
  {
    List<vp_Weapon> weaponList = new List<vp_Weapon>();
    if ((bool) (UnityEngine.Object) target.GetComponent<vp_Weapon>())
    {
      Debug.LogError("Error: (" + this + ") Hierarchy error. This component should sit above any vp_Weapons in the gameobject hierarchy.");
      return weaponList;
    }
    foreach (vp_Weapon componentsInChild in target.GetComponentsInChildren<vp_Weapon>(true))
      weaponList.Insert(weaponList.Count, componentsInChild);
    if (weaponList.Count == 0)
    {
      Debug.LogError("Error: (" + this + ") Hierarchy error. This component must be added to a gameobject with vp_Weapon components in child gameobjects.");
      return weaponList;
    }
    IComparer comparer = new WeaponComparer();
    weaponList.Sort(comparer.Compare);
    return weaponList;
  }

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

  protected virtual void Update()
  {
    InitWeapon();
    UpdateFiring();
  }

  protected virtual void UpdateFiring()
  {
    if (!m_Player.IsLocal.Get() && !m_Player.IsAI.Get() || !m_Player.Attack.Active || m_Player.SetWeapon.Active || m_CurrentWeapon != null && !m_CurrentWeapon.Wielded)
      return;
    int num = m_Player.Fire.Try() ? 1 : 0;
  }

  public virtual void SetWeapon(int weaponIndex)
  {
    if (Weapons == null || Weapons.Count < 1)
      Debug.LogError("Error: (" + this + ") Tried to set weapon with an empty weapon list.");
    else if (weaponIndex < 0 || weaponIndex > Weapons.Count)
    {
      Debug.LogError("Error: (" + this + ") Weapon list does not have a weapon with index: " + weaponIndex);
    }
    else
    {
      if (m_CurrentWeapon != null)
        m_CurrentWeapon.ResetState();
      DeactivateAll(Weapons);
      ActivateWeapon(weaponIndex);
    }
  }

  public void DeactivateAll(List<vp_Weapon> weaponList)
  {
    foreach (vp_Weapon weapon in weaponList)
    {
      weapon.ActivateGameObject(false);
      vp_FPWeapon vpFpWeapon = weapon as vp_FPWeapon;
      if (vpFpWeapon != null && vpFpWeapon.Weapon3rdPersonModel != null)
        vp_Utility.Activate(vpFpWeapon.Weapon3rdPersonModel, false);
    }
  }

  public void ActivateWeapon(int index)
  {
    m_CurrentWeaponIndex = index;
    m_CurrentWeapon = null;
    if (m_CurrentWeaponIndex <= 0)
      return;
    m_CurrentWeapon = Weapons[m_CurrentWeaponIndex - 1];
    if (!(m_CurrentWeapon != null))
      return;
    m_CurrentWeapon.ActivateGameObject();
  }

  public virtual void CancelTimers()
  {
    vp_Timer.CancelAll("EjectShell");
    m_DisableAttackStateTimer.Cancel();
    m_SetWeaponTimer.Cancel();
    m_SetWeaponRefreshTimer.Cancel();
  }

  public virtual void SetWeaponLayer(int layer)
  {
    if (m_CurrentWeaponIndex < 1 || m_CurrentWeaponIndex > Weapons.Count)
      return;
    vp_Layer.Set(Weapons[m_CurrentWeaponIndex - 1].gameObject, layer, true);
  }

  private void InitWeapon()
  {
    if (m_CurrentWeaponIndex != -1)
      return;
    SetWeapon(0);
    vp_Timer.In(SetWeaponDuration + 0.1f, () =>
    {
      if (StartWeapon <= 0 || StartWeapon >= Weapons.Count + 1 || m_Player.SetWeapon.TryStart(StartWeapon))
        return;
      Debug.LogWarning("Warning (" + this + ") Requested 'StartWeapon' (" + Weapons[StartWeapon - 1].name + ") was denied, likely by the inventory. Make sure it's present in the inventory from the beginning.");
    });
  }

  public void RefreshAllWeapons()
  {
    foreach (vp_Weapon weapon in Weapons)
    {
      weapon.Refresh();
      weapon.RefreshWeaponModel();
    }
  }

  public int GetWeaponIndex(vp_Weapon weapon) => Weapons.IndexOf(weapon) + 1;

  protected virtual void OnStart_Reload() => m_Player.Attack.Stop(m_Player.CurrentWeaponReloadDuration.Get() + ReloadAttackSleepDuration);

  protected virtual void OnStart_SetWeapon()
  {
    CancelTimers();
    if (WeaponBeingSet == null || WeaponBeingSet.AnimationType != 2)
    {
      m_Player.Reload.Stop(SetWeaponDuration + SetWeaponReloadSleepDuration);
      m_Player.Zoom.Stop(SetWeaponDuration + SetWeaponZoomSleepDuration);
      m_Player.Attack.Stop(SetWeaponDuration + SetWeaponAttackSleepDuration);
    }
    if (m_CurrentWeapon != null)
      m_CurrentWeapon.Wield(false);
    m_Player.SetWeapon.AutoDuration = SetWeaponDuration;
  }

  protected virtual void OnStop_SetWeapon()
  {
    int weaponIndex = 0;
    if (m_Player.SetWeapon.Argument != null)
      weaponIndex = (int) m_Player.SetWeapon.Argument;
    SetWeapon(weaponIndex);
    if (m_CurrentWeapon != null)
      m_CurrentWeapon.Wield();
    vp_Timer.In(SetWeaponRefreshStatesDelay, () =>
    {
      m_Player.RefreshActivityStates();
      if (!(m_CurrentWeapon != null) || m_Player.CurrentWeaponAmmoCount.Get() != 0)
        return;
      int num = m_Player.AutoReload.Try() ? 1 : 0;
    }, m_SetWeaponRefreshTimer);
  }

  protected virtual bool CanStart_SetWeapon()
  {
    int num = (int) m_Player.SetWeapon.Argument;
    return num != m_CurrentWeaponIndex && num >= 0 && num <= Weapons.Count && !m_Player.Reload.Active;
  }

  protected virtual bool CanStart_Attack() => !(m_CurrentWeapon == null) && !m_Player.Attack.Active && !m_Player.SetWeapon.Active && !m_Player.Reload.Active;

  protected virtual void OnStop_Attack() => vp_Timer.In(AttackStateDisableDelay, () =>
  {
    if (m_Player.Attack.Active || !(m_CurrentWeapon != null))
      return;
    m_CurrentWeapon.SetState("Attack", false);
  }, m_DisableAttackStateTimer);

  protected virtual bool OnAttempt_SetPrevWeapon()
  {
    int num1 = m_CurrentWeaponIndex - 1;
    if (num1 < 1)
      num1 = Weapons.Count;
    int num2 = 0;
    while (!m_Player.SetWeapon.TryStart(num1))
    {
      --num1;
      if (num1 < 1)
        num1 = Weapons.Count;
      ++num2;
      if (num2 > Weapons.Count)
        return false;
    }
    return true;
  }

  protected virtual bool OnAttempt_SetNextWeapon()
  {
    int num1 = m_CurrentWeaponIndex + 1;
    int num2 = 0;
    while (!m_Player.SetWeapon.TryStart(num1))
    {
      if (num1 > Weapons.Count + 1)
        num1 = 0;
      ++num1;
      ++num2;
      if (num2 > Weapons.Count)
        return false;
    }
    return true;
  }

  protected virtual bool OnAttempt_SetWeaponByName(string name)
  {
    for (int index = 0; index < Weapons.Count; ++index)
    {
      if (Weapons[index].name == name)
        return m_Player.SetWeapon.TryStart(index + 1);
    }
    return false;
  }

  protected virtual bool Get_CurrentWeaponWielded() => !(m_CurrentWeapon == null) && m_CurrentWeapon.Wielded;

  protected virtual bool OnValue_CurrentWeaponWielded => !(m_CurrentWeapon == null) && m_CurrentWeapon.Wielded;

  protected virtual string Get_CurrentWeaponName() => m_CurrentWeapon == null || Weapons == null ? "" : m_CurrentWeapon.name;

  protected virtual string OnValue_CurrentWeaponName => m_CurrentWeapon == null || Weapons == null ? "" : m_CurrentWeapon.name;

  protected virtual int Get_CurrentWeaponID() => m_CurrentWeaponIndex;

  protected virtual int OnValue_CurrentWeaponID => m_CurrentWeaponIndex;

  protected virtual int Get_CurrentWeaponIndex() => m_CurrentWeaponIndex;

  protected virtual int OnValue_CurrentWeaponIndex => m_CurrentWeaponIndex;

  public virtual int Get_CurrentWeaponType() => !(CurrentWeapon == null) ? CurrentWeapon.AnimationType : 0;

  public virtual int OnValue_CurrentWeaponType => !(CurrentWeapon == null) ? CurrentWeapon.AnimationType : 0;

  public virtual int Get_CurrentWeaponGrip() => !(CurrentWeapon == null) ? CurrentWeapon.AnimationGrip : 0;

  public virtual int OnValue_CurrentWeaponGrip => !(CurrentWeapon == null) ? CurrentWeapon.AnimationGrip : 0;

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterActivity("Attack", null, OnStop_Attack, CanStart_Attack, null, null, null);
    eventHandler.RegisterActivity("SetWeapon", OnStart_SetWeapon, OnStop_SetWeapon, CanStart_SetWeapon, null, null, null);
    eventHandler.RegisterAttempt("SetNextWeapon", OnAttempt_SetNextWeapon);
    eventHandler.RegisterAttempt("SetPrevWeapon", OnAttempt_SetPrevWeapon);
    eventHandler.RegisterAttempt<string>("SetWeaponByName", OnAttempt_SetWeaponByName);
    eventHandler.RegisterActivity("Reload", OnStart_Reload, null, null, null, null, null);
    eventHandler.RegisterValue("CurrentWeaponGrip", Get_CurrentWeaponGrip, null);
    eventHandler.RegisterValue("CurrentWeaponID", Get_CurrentWeaponID, null);
    eventHandler.RegisterValue("CurrentWeaponIndex", Get_CurrentWeaponIndex, null);
    eventHandler.RegisterValue("CurrentWeaponName", Get_CurrentWeaponName, null);
    eventHandler.RegisterValue("CurrentWeaponType", Get_CurrentWeaponType, null);
    eventHandler.RegisterValue("CurrentWeaponWielded", Get_CurrentWeaponWielded, null);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterActivity("Attack", null, OnStop_Attack, CanStart_Attack, null, null, null);
    eventHandler.UnregisterActivity("SetWeapon", OnStart_SetWeapon, OnStop_SetWeapon, CanStart_SetWeapon, null, null, null);
    eventHandler.UnregisterAttempt("SetNextWeapon", OnAttempt_SetNextWeapon);
    eventHandler.UnregisterAttempt("SetPrevWeapon", OnAttempt_SetPrevWeapon);
    eventHandler.UnregisterAttempt<string>("SetWeaponByName", OnAttempt_SetWeaponByName);
    eventHandler.UnregisterActivity("Reload", OnStart_Reload, null, null, null, null, null);
    eventHandler.UnregisterValue("CurrentWeaponGrip", Get_CurrentWeaponGrip, null);
    eventHandler.UnregisterValue("CurrentWeaponID", Get_CurrentWeaponID, null);
    eventHandler.UnregisterValue("CurrentWeaponIndex", Get_CurrentWeaponIndex, null);
    eventHandler.UnregisterValue("CurrentWeaponName", Get_CurrentWeaponName, null);
    eventHandler.UnregisterValue("CurrentWeaponType", Get_CurrentWeaponType, null);
    eventHandler.UnregisterValue("CurrentWeaponWielded", Get_CurrentWeaponWielded, null);
  }

  protected class WeaponComparer : IComparer
  {
    int IComparer.Compare(object x, object y) => new CaseInsensitiveComparer().Compare(((Component) x).gameObject.name, ((Component) y).gameObject.name);
  }
}
