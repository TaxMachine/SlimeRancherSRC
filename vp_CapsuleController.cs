// Decompiled with JetBrains decompiler
// Type: vp_CapsuleController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
public class vp_CapsuleController : vp_Controller
{
  protected CapsuleCollider m_CapsuleCollider;

  protected CapsuleCollider CapsuleCollider
  {
    get
    {
      if (m_CapsuleCollider == null)
      {
        m_CapsuleCollider = Collider as CapsuleCollider;
        if (m_CapsuleCollider != null && m_CapsuleCollider.isTrigger)
          m_CapsuleCollider = null;
      }
      return m_CapsuleCollider;
    }
  }

  protected override void InitCollider()
  {
    m_NormalHeight = CapsuleCollider.height;
    CapsuleCollider.center = m_NormalCenter = m_NormalHeight * (Vector3.up * 0.5f);
    CapsuleCollider.radius = m_NormalHeight * 0.25f;
    m_CrouchHeight = m_NormalHeight * PhysicsCrouchHeightModifier;
    m_CrouchCenter = m_NormalCenter * PhysicsCrouchHeightModifier;
    Collider.transform.localPosition = Vector3.zero;
  }

  protected override void RefreshCollider()
  {
    if (Player.Crouch.Active && (!MotorFreeFly || Grounded))
    {
      CapsuleCollider.height = m_NormalHeight * PhysicsCrouchHeightModifier;
      CapsuleCollider.center = m_NormalCenter * PhysicsCrouchHeightModifier;
    }
    else
    {
      CapsuleCollider.height = m_NormalHeight;
      CapsuleCollider.center = m_NormalCenter;
    }
  }

  public override void EnableCollider(bool isEnabled = true)
  {
    if (!(CapsuleCollider != null))
      return;
    CapsuleCollider.enabled = isEnabled;
  }

  protected override float Get_Radius() => CapsuleCollider.radius;

  protected override float OnValue_Radius => CapsuleCollider.radius;

  protected override float Get_Height() => CapsuleCollider.height;

  protected override float OnValue_Height => CapsuleCollider.height;
}
