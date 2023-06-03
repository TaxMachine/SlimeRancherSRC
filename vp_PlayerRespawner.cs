// Decompiled with JetBrains decompiler
// Type: vp_PlayerRespawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class vp_PlayerRespawner : vp_Respawner, EventHandlerRegistrable
{
  private vp_PlayerEventHandler m_Player;

  public vp_PlayerEventHandler Player
  {
    get
    {
      if (m_Player == null)
        m_Player = transform.GetComponent<vp_PlayerEventHandler>();
      return m_Player;
    }
  }

  protected override void Awake() => base.Awake();

  protected override void OnEnable()
  {
    if (Player != null)
      Register(Player);
    base.OnEnable();
  }

  protected override void OnDisable()
  {
    if (!(Player != null))
      return;
    Unregister(Player);
  }

  public override void Reset()
  {
    if (!Application.isPlaying || Player == null)
      return;
    Player.Position.Set(Placement.Position);
    Player.Rotation.Set(Placement.Rotation.eulerAngles);
    Player.Stop.Send();
  }

  public void Register(vp_EventHandler eventHandler) => throw new NotImplementedException();

  public void Unregister(vp_EventHandler eventHandler) => throw new NotImplementedException();
}
