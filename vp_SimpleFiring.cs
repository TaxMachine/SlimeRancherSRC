// Decompiled with JetBrains decompiler
// Type: vp_SimpleFiring
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class vp_SimpleFiring : MonoBehaviour, EventHandlerRegistrable
{
  protected vp_PlayerEventHandler m_Player;

  protected virtual void Awake() => m_Player = (vp_PlayerEventHandler) transform.root.GetComponentInChildren(typeof (vp_PlayerEventHandler));

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
    if (!m_Player.Attack.Active)
      return;
    int num = m_Player.Fire.Try() ? 1 : 0;
  }

  public void Register(vp_EventHandler eventHandler) => throw new NotImplementedException();

  public void Unregister(vp_EventHandler eventHandler) => throw new NotImplementedException();
}
