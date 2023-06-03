// Decompiled with JetBrains decompiler
// Type: vp_SimpleCrosshair
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_SimpleCrosshair : MonoBehaviour, EventHandlerRegistrable
{
  public Texture m_ImageCrosshair;
  public bool HideOnFirstPersonZoom = true;
  public bool HideOnDeath = true;
  protected vp_FPPlayerEventHandler m_Player;

  protected virtual void Awake() => m_Player = FindObjectOfType(typeof (vp_FPPlayerEventHandler)) as vp_FPPlayerEventHandler;

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

  private void OnGUI()
  {
    if (m_ImageCrosshair == null || HideOnFirstPersonZoom && m_Player.Zoom.Active && m_Player.IsFirstPerson.Get() || HideOnDeath && m_Player.Dead.Active)
      return;
    GUI.color = new Color(1f, 1f, 1f, 0.8f);
    GUI.DrawTexture(new Rect((float) (Screen.width * 0.5 - m_ImageCrosshair.width * 0.5), (float) (Screen.height * 0.5 - m_ImageCrosshair.height * 0.5), m_ImageCrosshair.width, m_ImageCrosshair.height), m_ImageCrosshair);
    GUI.color = Color.white;
  }

  protected virtual Texture Get_Crosshair() => m_ImageCrosshair;

  protected virtual void Set_Crosshair(Texture value) => m_ImageCrosshair = value;

  protected virtual Texture OnValue_Crosshair
  {
    get => m_ImageCrosshair;
    set => m_ImageCrosshair = value;
  }

  public void Register(vp_EventHandler eventHandler) => eventHandler.RegisterValue("Crosshair", Get_Crosshair, Set_Crosshair);

  public void Unregister(vp_EventHandler eventHandler) => eventHandler.UnregisterValue("Crosshair", Get_Crosshair, Set_Crosshair);
}
