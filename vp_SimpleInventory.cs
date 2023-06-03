// Decompiled with JetBrains decompiler
// Type: vp_SimpleInventory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vp_SimpleInventory : MonoBehaviour, EventHandlerRegistrable
{
  protected vp_FPPlayerEventHandler m_Player;
  [SerializeField]
  protected List<InventoryItemStatus> m_ItemTypes;
  [SerializeField]
  protected List<InventoryWeaponStatus> m_WeaponTypes;
  protected Dictionary<string, InventoryItemStatus> m_ItemStatusDictionary;
  protected InventoryWeaponStatus m_CurrentWeaponStatus;
  protected int m_RefreshWeaponStatusIterations;

  public InventoryWeaponStatus CurrentWeaponStatus
  {
    get => m_CurrentWeaponStatus;
    set => m_CurrentWeaponStatus = value;
  }

  public List<InventoryItemStatus> Weapons
  {
    get
    {
      List<InventoryItemStatus> weapons = new List<InventoryItemStatus>();
      foreach (InventoryItemStatus weaponType in m_WeaponTypes)
        weapons.Add(weaponType);
      return weapons;
    }
  }

  public List<InventoryItemStatus> EquippedWeapons
  {
    get
    {
      List<InventoryItemStatus> equippedWeapons = new List<InventoryItemStatus>();
      foreach (InventoryItemStatus inventoryItemStatus in m_ItemStatusDictionary.Values)
      {
        if (inventoryItemStatus.GetType() == typeof (InventoryWeaponStatus) && inventoryItemStatus.Have == 1)
          equippedWeapons.Add(inventoryItemStatus);
      }
      return equippedWeapons;
    }
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

  private void Awake()
  {
    m_Player = (vp_FPPlayerEventHandler) transform.root.GetComponentInChildren(typeof (vp_FPPlayerEventHandler));
    m_WeaponTypes.Sort(((IComparer) new InventoryWeaponStatusComparer()).Compare);
  }

  protected Dictionary<string, InventoryItemStatus> ItemStatusDictionary
  {
    get
    {
      if (m_ItemStatusDictionary == null)
      {
        m_ItemStatusDictionary = new Dictionary<string, InventoryItemStatus>();
        for (int index = m_ItemTypes.Count - 1; index > -1; --index)
        {
          if (!m_ItemStatusDictionary.ContainsKey(m_ItemTypes[index].Name))
            m_ItemStatusDictionary.Add(m_ItemTypes[index].Name, m_ItemTypes[index]);
          else
            m_ItemTypes.Remove(m_ItemTypes[index]);
        }
        for (int index = m_WeaponTypes.Count - 1; index > -1; --index)
        {
          if (!m_ItemStatusDictionary.ContainsKey(m_WeaponTypes[index].Name))
            m_ItemStatusDictionary.Add(m_WeaponTypes[index].Name, m_WeaponTypes[index]);
          else
            m_WeaponTypes.Remove(m_WeaponTypes[index]);
        }
      }
      return m_ItemStatusDictionary;
    }
  }

  public bool HaveItem(object name)
  {
    InventoryItemStatus inventoryItemStatus;
    return ItemStatusDictionary.TryGetValue((string) name, out inventoryItemStatus) && inventoryItemStatus.Have >= 1;
  }

  private InventoryItemStatus GetItemStatus(string name)
  {
    InventoryItemStatus itemStatus;
    if (!ItemStatusDictionary.TryGetValue(name, out itemStatus))
      Debug.LogError("Error: (" + this + "). Unknown item type: '" + name + "'.");
    return itemStatus;
  }

  private InventoryWeaponStatus GetWeaponStatus(string name)
  {
    if (name == null)
      return null;
    InventoryItemStatus weaponStatus;
    if (!ItemStatusDictionary.TryGetValue(name, out weaponStatus))
    {
      Debug.LogError("Error: (" + this + "). Unknown item type: '" + name + "'.");
      return null;
    }
    if (!(weaponStatus.GetType() != typeof (InventoryWeaponStatus)))
      return (InventoryWeaponStatus) weaponStatus;
    Debug.LogError("Error: (" + this + "). Item is not a weapon: '" + name + "'.");
    return null;
  }

  protected void RefreshWeaponStatus()
  {
    if (!m_Player.CurrentWeaponWielded.Get() && m_RefreshWeaponStatusIterations < 50)
    {
      ++m_RefreshWeaponStatusIterations;
      vp_Timer.In(0.1f, RefreshWeaponStatus);
    }
    else
    {
      m_RefreshWeaponStatusIterations = 0;
      string name = m_Player.CurrentWeaponName.Get();
      if (string.IsNullOrEmpty(name))
        return;
      m_CurrentWeaponStatus = GetWeaponStatus(name);
    }
  }

  protected virtual int Get_CurrentWeaponAmmoCount() => m_CurrentWeaponStatus == null ? 0 : m_CurrentWeaponStatus.LoadedAmmo;

  protected virtual void Set_CurrentWeaponAmmoCount(int value)
  {
    if (m_CurrentWeaponStatus == null)
      return;
    m_CurrentWeaponStatus.LoadedAmmo = value;
  }

  protected virtual int OnValue_CurrentWeaponAmmoCount
  {
    get => m_CurrentWeaponStatus == null ? 0 : m_CurrentWeaponStatus.LoadedAmmo;
    set
    {
      if (m_CurrentWeaponStatus == null)
        return;
      m_CurrentWeaponStatus.LoadedAmmo = value;
    }
  }

  protected virtual int Get_CurrentWeaponClipCount()
  {
    InventoryItemStatus inventoryItemStatus;
    return m_CurrentWeaponStatus == null || !ItemStatusDictionary.TryGetValue(m_CurrentWeaponStatus.ClipType, out inventoryItemStatus) ? 0 : inventoryItemStatus.Have;
  }

  protected virtual int OnValue_CurrentWeaponClipCount
  {
    get
    {
      InventoryItemStatus inventoryItemStatus;
      return m_CurrentWeaponStatus == null || !ItemStatusDictionary.TryGetValue(m_CurrentWeaponStatus.ClipType, out inventoryItemStatus) ? 0 : inventoryItemStatus.Have;
    }
  }

  protected virtual string Get_CurrentWeaponClipType() => m_CurrentWeaponStatus == null ? "" : m_CurrentWeaponStatus.ClipType;

  protected virtual string OnValue_CurrentWeaponClipType => m_CurrentWeaponStatus == null ? "" : m_CurrentWeaponStatus.ClipType;

  protected virtual int OnMessage_GetItemCount(string name)
  {
    InventoryItemStatus inventoryItemStatus;
    return !ItemStatusDictionary.TryGetValue(name, out inventoryItemStatus) ? 0 : inventoryItemStatus.Have;
  }

  protected virtual bool OnAttempt_DepleteAmmo()
  {
    if (m_CurrentWeaponStatus == null)
      return false;
    if (m_CurrentWeaponStatus.LoadedAmmo < 1)
      return m_CurrentWeaponStatus.MaxAmmo == 0;
    --m_CurrentWeaponStatus.LoadedAmmo;
    return true;
  }

  protected virtual bool OnAttempt_AddAmmo(object arg)
  {
    object[] objArray = (object[]) arg;
    string name = (string) objArray[0];
    int num = objArray.Length == 2 ? (int) objArray[1] : -1;
    InventoryWeaponStatus weaponStatus = GetWeaponStatus(name);
    if (weaponStatus == null)
      return false;
    weaponStatus.LoadedAmmo = num != -1 ? Mathf.Min(weaponStatus.LoadedAmmo + num, weaponStatus.MaxAmmo) : weaponStatus.MaxAmmo;
    return true;
  }

  protected virtual bool OnAttempt_AddItem(object args)
  {
    object[] objArray = (object[]) args;
    string name = (string) objArray[0];
    int num = objArray.Length == 2 ? (int) objArray[1] : 1;
    InventoryItemStatus itemStatus = GetItemStatus(name);
    if (itemStatus == null)
      return false;
    itemStatus.CanHave = Mathf.Max(1, itemStatus.CanHave);
    if (itemStatus.Have >= itemStatus.CanHave)
      return false;
    itemStatus.Have = Mathf.Min(itemStatus.Have + num, itemStatus.CanHave);
    return true;
  }

  protected virtual bool OnAttempt_RemoveItem(object args)
  {
    object[] objArray = (object[]) args;
    string name = (string) objArray[0];
    int num = objArray.Length == 2 ? (int) objArray[1] : 1;
    InventoryItemStatus itemStatus = GetItemStatus(name);
    if (itemStatus == null || itemStatus.Have <= 0)
      return false;
    itemStatus.Have = Mathf.Max(itemStatus.Have - num, 0);
    return true;
  }

  protected virtual bool OnAttempt_RemoveClip()
  {
    if (m_CurrentWeaponStatus == null || GetItemStatus(m_CurrentWeaponStatus.ClipType) == null || m_CurrentWeaponStatus.LoadedAmmo >= m_CurrentWeaponStatus.MaxAmmo)
      return false;
    return m_Player.RemoveItem.Try(new object[1]
    {
      m_CurrentWeaponStatus.ClipType
    });
  }

  protected virtual bool CanStart_SetWeapon()
  {
    int num = (int) m_Player.SetWeapon.Argument;
    if (num == 0)
      return true;
    return num >= 0 && num <= m_WeaponTypes.Count && HaveItem(m_WeaponTypes[num - 1].Name);
  }

  protected virtual void OnStop_SetWeapon() => RefreshWeaponStatus();

  protected virtual void OnStart_Dead()
  {
    if (m_ItemStatusDictionary == null)
      return;
    foreach (InventoryItemStatus inventoryItemStatus in m_ItemStatusDictionary.Values)
    {
      if (inventoryItemStatus.ClearOnDeath)
      {
        inventoryItemStatus.Have = 0;
        if (inventoryItemStatus.GetType() == typeof (InventoryWeaponStatus))
          ((InventoryWeaponStatus) inventoryItemStatus).LoadedAmmo = 0;
      }
    }
  }

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterActivity("SetWeapon", null, OnStop_SetWeapon, CanStart_SetWeapon, null, null, null);
    eventHandler.RegisterActivity("Dead", OnStart_Dead, null, null, null, null, null);
    eventHandler.RegisterAttempt<object>("AddAmmo", OnAttempt_AddAmmo);
    eventHandler.RegisterAttempt<object>("AddItem", OnAttempt_AddItem);
    eventHandler.RegisterAttempt("DepleteAmmo", OnAttempt_DepleteAmmo);
    eventHandler.RegisterAttempt("RemoveClip", OnAttempt_RemoveClip);
    eventHandler.RegisterAttempt<object>("RemoveItem", OnAttempt_RemoveItem);
    eventHandler.RegisterMessage<string, int>("GetItemCount", OnMessage_GetItemCount);
    eventHandler.RegisterValue("CurrentWeaponAmmoCount", Get_CurrentWeaponAmmoCount, Set_CurrentWeaponAmmoCount);
    eventHandler.RegisterValue("CurrentWeaponClipCount", Get_CurrentWeaponClipCount, null);
    eventHandler.RegisterValue("CurrentWeaponClipType", Get_CurrentWeaponClipType, null);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterActivity("SetWeapon", null, OnStop_SetWeapon, CanStart_SetWeapon, null, null, null);
    eventHandler.UnregisterActivity("Dead", OnStart_Dead, null, null, null, null, null);
    eventHandler.UnregisterAttempt<object>("AddAmmo", OnAttempt_AddAmmo);
    eventHandler.UnregisterAttempt<object>("AddItem", OnAttempt_AddItem);
    eventHandler.UnregisterAttempt("DepleteAmmo", OnAttempt_DepleteAmmo);
    eventHandler.UnregisterAttempt("RemoveClip", OnAttempt_RemoveClip);
    eventHandler.UnregisterAttempt<object>("RemoveItem", OnAttempt_RemoveItem);
    eventHandler.UnregisterMessage<string, int>("GetItemCount", OnMessage_GetItemCount);
    eventHandler.UnregisterValue("CurrentWeaponAmmoCount", Get_CurrentWeaponAmmoCount, Set_CurrentWeaponAmmoCount);
    eventHandler.UnregisterValue("CurrentWeaponClipCount", Get_CurrentWeaponClipCount, null);
    eventHandler.UnregisterValue("CurrentWeaponClipType", Get_CurrentWeaponClipType, null);
  }

  protected class InventoryWeaponStatusComparer : IComparer
  {
    int IComparer.Compare(object x, object y) => new CaseInsensitiveComparer().Compare(((InventoryItemStatus) x).Name, ((InventoryItemStatus) y).Name);
  }

  [Serializable]
  public class InventoryItemStatus
  {
    public string Name = "Unnamed";
    public int Have;
    public int CanHave = 1;
    public bool ClearOnDeath = true;
  }

  [Serializable]
  public class InventoryWeaponStatus : InventoryItemStatus
  {
    public string ClipType = "";
    public int LoadedAmmo;
    public int MaxAmmo = 10;
  }
}
