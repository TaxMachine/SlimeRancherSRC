// Decompiled with JetBrains decompiler
// Type: vp_3rdPersonWeaponAim
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_3rdPersonWeaponAim : MonoBehaviour, EventHandlerRegistrable
{
  public GameObject Hand;
  [Range(0.0f, 360f)]
  public float AngleAdjustX;
  [Range(0.0f, 360f)]
  public float AngleAdjustY;
  [Range(0.0f, 360f)]
  public float AngleAdjustZ;
  [Range(0.0f, 5f)]
  public float RecoilFactorX = 1f;
  [Range(0.0f, 5f)]
  public float RecoilFactorY = 1f;
  [Range(0.0f, 5f)]
  public float RecoilFactorZ = 1f;
  protected Quaternion m_DefaultRotation;
  protected Vector3 m_ReferenceUpDir;
  protected Vector3 m_ReferenceLookDir;
  protected Quaternion m_HandBoneRotDif;
  protected Vector3 m_WorldDir = Vector3.zero;
  protected Transform m_Transform;
  protected vp_PlayerEventHandler m_Player;
  private vp_WeaponHandler m_WeaponHandler;
  protected Animator m_Animator;
  private Transform m_Root;
  private Transform m_LowerArmObj;
  private Transform m_HandObj;

  public Transform Transform
  {
    get
    {
      if (m_Transform == null)
        m_Transform = transform;
      return m_Transform;
    }
  }

  public vp_PlayerEventHandler Player
  {
    get
    {
      if (m_Player == null)
        m_Player = (vp_PlayerEventHandler) Root.GetComponentInChildren(typeof (vp_PlayerEventHandler));
      return m_Player;
    }
  }

  public vp_WeaponHandler WeaponHandler
  {
    get
    {
      if (m_WeaponHandler == null)
        m_WeaponHandler = (vp_WeaponHandler) Root.GetComponentInChildren(typeof (vp_WeaponHandler));
      return m_WeaponHandler;
    }
  }

  protected Animator Animator
  {
    get
    {
      if (m_Animator == null)
        m_Animator = Root.GetComponentInChildren<Animator>();
      return m_Animator;
    }
  }

  private Transform Root
  {
    get
    {
      if (m_Root == null)
        m_Root = Transform.root;
      return m_Root;
    }
  }

  private Transform LowerArmObj
  {
    get
    {
      if (m_LowerArmObj == null)
        m_LowerArmObj = HandObj.parent;
      return m_LowerArmObj;
    }
  }

  private Transform HandObj
  {
    get
    {
      if (m_HandObj == null)
      {
        if (Hand != null)
        {
          m_HandObj = Hand.transform;
        }
        else
        {
          m_HandObj = vp_Utility.GetTransformByNameInAncestors(Transform, "hand", true, true);
          if (m_HandObj == null && Transform.parent != null)
            m_HandObj = Transform.parent;
          if (m_HandObj != null)
            Hand = m_HandObj.gameObject;
        }
      }
      return m_HandObj;
    }
  }

  protected virtual void OnEnable()
  {
    if (!(Player != null))
      return;
    Register(Player);
  }

  protected virtual void OnDisable()
  {
    if (!(Player != null))
      return;
    Unregister(Player);
  }

  protected virtual void Awake()
  {
    m_DefaultRotation = Transform.localRotation;
    if (LowerArmObj == null || HandObj == null)
    {
      Debug.LogError("Hierarchy Error (" + this + ") This script should be placed on a 3rd person weapon gameobject childed to a hand bone in a rigged character.");
      enabled = false;
    }
    else
    {
      Quaternion quaternion = Quaternion.Inverse(LowerArmObj.rotation);
      m_ReferenceLookDir = quaternion * Root.rotation * Vector3.forward;
      m_ReferenceUpDir = quaternion * Root.rotation * Vector3.up;
      Quaternion rotation1 = HandObj.rotation;
      HandObj.rotation = Root.rotation;
      Quaternion rotation2 = HandObj.rotation;
      HandObj.rotation = rotation1;
      m_HandBoneRotDif = Quaternion.Inverse(rotation2) * rotation1;
    }
  }

  protected virtual void LateUpdate()
  {
    if (Time.timeScale == 0.0)
      return;
    UpdateAiming();
  }

  protected virtual void UpdateAiming()
  {
    if (Animator == null)
      return;
    if (!Animator.GetBool("IsAttacking") && !Animator.GetBool("IsZooming") || Animator.GetBool("IsReloading") || Animator.GetBool("IsOutOfControl") || Player.CurrentWeaponIndex.Get() == 0)
    {
      Transform.localRotation = m_DefaultRotation;
    }
    else
    {
      Quaternion rotation = Transform.rotation;
      Transform.rotation = Quaternion.LookRotation(Player.AimDirection.Get());
      m_WorldDir = Transform.forward;
      Transform.rotation = rotation;
      HandObj.rotation = vp_3DUtility.GetBoneLookRotationInWorldSpace(HandObj.rotation, LowerArmObj.rotation, m_WorldDir, 1f, m_ReferenceUpDir, m_ReferenceLookDir, m_HandBoneRotDif);
      HandObj.Rotate(Transform.forward, AngleAdjustZ + WeaponHandler.CurrentWeapon.Recoil.z * RecoilFactorZ, Space.World);
      HandObj.Rotate(Transform.up, AngleAdjustY + WeaponHandler.CurrentWeapon.Recoil.y * RecoilFactorY, Space.World);
      HandObj.Rotate(Transform.right, AngleAdjustX + WeaponHandler.CurrentWeapon.Recoil.x * RecoilFactorX, Space.World);
    }
  }

  public void Register(vp_EventHandler eventHandler)
  {
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
  }
}
