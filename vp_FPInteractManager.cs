// Decompiled with JetBrains decompiler
// Type: vp_FPInteractManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class vp_FPInteractManager : MonoBehaviour
{
  public float InteractDistance = 2f;
  public float InteractDistance3rdPerson = 3f;
  public float MaxInteractDistance = 25f;
  protected vp_FPPlayerEventHandler m_Player;
  protected vp_FPCamera m_Camera;
  protected vp_Interactable m_CurrentInteractable;
  protected Texture m_OriginalCrosshair;
  protected Dictionary<Collider, vp_Interactable> m_Interactables = new Dictionary<Collider, vp_Interactable>();
  protected vp_Interactable m_LastInteractable;
  protected vp_Timer.Handle m_ShowTextTimer = new vp_Timer.Handle();
  protected bool m_CanInteract;

  public float CrosshairTimeoutTimer { get; set; }

  protected virtual void Awake()
  {
    m_Player = GetComponent<vp_FPPlayerEventHandler>();
    m_Camera = GetComponentInChildren<vp_FPCamera>();
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

  public virtual void OnStart_Dead() => ShouldFinishInteraction();

  public virtual void LateUpdate()
  {
    if (m_Player.Dead.Active)
      return;
    if (m_OriginalCrosshair == null && m_Player.Crosshair.Get() != null)
      m_OriginalCrosshair = m_Player.Crosshair.Get();
    InteractCrosshair();
  }

  protected virtual bool CanStart_Interact()
  {
    if (ShouldFinishInteraction() || m_Player.SetWeapon.Active || !(m_LastInteractable != null) || m_LastInteractable.InteractType != vp_Interactable.vp_InteractType.Normal || !m_LastInteractable.TryInteract(m_Player))
      return false;
    ResetCrosshair(false);
    return true;
  }

  protected virtual bool ShouldFinishInteraction()
  {
    if (!(m_Player.Interactable.Get() != null))
      return false;
    m_LastInteractable = null;
    ResetCrosshair();
    m_Player.Interactable.Get().FinishInteraction();
    m_Player.Interactable.Set(null);
    return true;
  }

  protected virtual void InteractCrosshair()
  {
    if (m_Player.Crosshair.Get() == null || m_Player.Interactable.Get() != null)
      return;
    vp_Interactable interactable = null;
    if (FindInteractable(out interactable))
    {
      if (!(interactable != m_LastInteractable) || CrosshairTimeoutTimer > (double) Time.time && m_LastInteractable != null && interactable.GetType() == m_LastInteractable.GetType())
        return;
      m_CanInteract = true;
      m_LastInteractable = interactable;
      if (interactable.InteractText != "" && !m_ShowTextTimer.Active)
        vp_Timer.In(interactable.DelayShowingText, () => m_Player.HUDText.Send(interactable.InteractText), m_ShowTextTimer);
      if (interactable.m_InteractCrosshair == null)
        return;
      m_Player.Crosshair.Set(interactable.m_InteractCrosshair);
    }
    else
    {
      m_CanInteract = false;
      ResetCrosshair();
    }
  }

  protected virtual bool FindInteractable(out vp_Interactable interactable)
  {
    interactable = null;
    RaycastHit hitInfo;
    if (!Physics.Raycast(m_Camera.Transform.position, m_Camera.Transform.forward, out hitInfo, MaxInteractDistance, -754974997))
      return false;
    if (!m_Interactables.TryGetValue(hitInfo.collider, out interactable))
      m_Interactables.Add(hitInfo.collider, interactable = hitInfo.collider.GetComponent<vp_Interactable>());
    return !(interactable == null) && (interactable.InteractDistance != 0.0 || hitInfo.distance < (m_Player.IsFirstPerson.Get() ? InteractDistance : (double) InteractDistance3rdPerson)) && (interactable.InteractDistance <= 0.0 || hitInfo.distance < (double) interactable.InteractDistance);
  }

  protected virtual void ResetCrosshair(bool reset = true)
  {
    if (m_OriginalCrosshair == null || m_Player.Crosshair.Get() == m_OriginalCrosshair)
      return;
    m_ShowTextTimer.Cancel();
    if (reset)
      m_Player.Crosshair.Set(m_OriginalCrosshair);
    m_LastInteractable = null;
  }

  protected virtual void OnControllerColliderHit(ControllerColliderHit hit)
  {
    Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
    if (attachedRigidbody == null || attachedRigidbody.isKinematic)
      return;
    vp_Interactable vpInteractable = null;
    if (!m_Interactables.TryGetValue(hit.collider, out vpInteractable))
      m_Interactables.Add(hit.collider, vpInteractable = hit.collider.GetComponent<vp_Interactable>());
    if (vpInteractable == null || vpInteractable.InteractType != vp_Interactable.vp_InteractType.CollisionTrigger)
      return;
    hit.gameObject.SendMessage("TryInteract", m_Player, SendMessageOptions.DontRequireReceiver);
  }

  protected virtual vp_Interactable Get_Interactable() => m_CurrentInteractable;

  protected virtual void Set_Interactable(vp_Interactable value) => m_CurrentInteractable = value;

  protected virtual vp_Interactable OnValue_Interactable
  {
    get => m_CurrentInteractable;
    set => m_CurrentInteractable = value;
  }

  protected virtual bool Get_CanInteract() => m_CanInteract;

  protected virtual void Set_CanInteract(bool value) => m_CanInteract = value;

  protected virtual bool OnValue_CanInteract
  {
    get => m_CanInteract;
    set => m_CanInteract = value;
  }

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterActivity("Interact", null, null, CanStart_Interact, null, null, null);
    eventHandler.RegisterActivity("Dead", OnStart_Dead, null, null, null, null, null);
    eventHandler.RegisterValue("Interactable", Get_Interactable, Set_Interactable);
    eventHandler.RegisterValue("CanInteract", Get_CanInteract, Set_CanInteract);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterActivity("Interact", null, null, CanStart_Interact, null, null, null);
    eventHandler.UnregisterActivity("Dead", OnStart_Dead, null, null, null, null, null);
    eventHandler.UnregisterValue("Interactable", Get_Interactable, Set_Interactable);
    eventHandler.UnregisterValue("CanInteract", Get_CanInteract, Set_CanInteract);
  }
}
