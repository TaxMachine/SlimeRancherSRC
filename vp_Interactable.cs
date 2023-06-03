// Decompiled with JetBrains decompiler
// Type: vp_Interactable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Collider))]
public abstract class vp_Interactable : MonoBehaviour
{
  public vp_InteractType InteractType;
  public List<string> RecipientTags = new List<string>();
  public float InteractDistance;
  public Texture m_InteractCrosshair;
  public string InteractText = "";
  public float DelayShowingText = 2f;
  protected Transform m_Transform;
  protected vp_FPController m_Controller;
  protected vp_FPCamera m_Camera;
  protected vp_WeaponHandler m_WeaponHandler;
  protected vp_PlayerEventHandler m_Player;

  protected virtual void Start()
  {
    m_Transform = transform;
    if (RecipientTags.Count == 0)
      RecipientTags.Add("Player");
    if (InteractType != vp_InteractType.Trigger || !(GetComponent<Collider>() != null))
      return;
    GetComponent<Collider>().isTrigger = true;
  }

  protected virtual void OnEnable()
  {
  }

  protected virtual void OnDisable()
  {
  }

  public virtual bool TryInteract(vp_PlayerEventHandler player) => false;

  protected virtual void OnTriggerEnter(Collider col)
  {
    if (InteractType != vp_InteractType.Trigger)
      return;
    using (List<string>.Enumerator enumerator = RecipientTags.GetEnumerator())
    {
      string current;
      do
      {
        if (enumerator.MoveNext())
          current = enumerator.Current;
        else
          goto label_1;
      }
      while (!(col.gameObject.tag == current));
      goto label_9;
label_1:
      return;
    }
label_9:
    m_Player = col.gameObject.GetComponent<vp_PlayerEventHandler>();
    if (m_Player == null)
      return;
    TryInteract(m_Player);
  }

  public virtual void FinishInteraction()
  {
  }

  public enum vp_InteractType
  {
    Normal,
    Trigger,
    CollisionTrigger,
  }
}
