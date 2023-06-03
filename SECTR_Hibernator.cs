// Decompiled with JetBrains decompiler
// Type: SECTR_Hibernator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (SECTR_Member))]
[AddComponentMenu("SECTR/Stream/SECTR Hibernator")]
public class SECTR_Hibernator : MonoBehaviour
{
  private bool hibernating;
  private SECTR_Member cachedMember;
  [SECTR_ToolTip("Hibernate components on children as well as ones on this game object.")]
  public bool HibernateChildren = true;
  [SECTR_ToolTip("Disable Behavior components during hibernation.")]
  public bool HibernateBehaviors = true;
  [SECTR_ToolTip("Disable Collder components during hibernation.")]
  public bool HibernateColliders = true;
  [SECTR_ToolTip("Disable RigidBody components during hibernation.")]
  public bool HibernateRigidBodies = true;
  [SECTR_ToolTip("Hide Render components during hibernation.")]
  public bool HibernateRenderers = true;
  private Vector3 vel;
  private Vector3 angularVel;
  private bool kinematic;
  private SECTR_Sector.SectorSetId setId = SECTR_Sector.SectorSetId.UNSET;

  public bool isHibernating => hibernating;

  public event HibernateCallback Awoke;

  public event HibernateCallback Hibernated;

  public event HibernateCallback HibernateUpdate;

  private void OnEnable()
  {
    cachedMember = GetComponent<SECTR_Member>();
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.SECTRDirector != null))
      return;
    SRSingleton<SceneContext>.Instance.SECTRDirector.RegisterHibernator(this);
    RegisterMember();
    OnUpdate();
  }

  private void OnDisable()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.SECTRDirector != null))
      return;
    SRSingleton<SceneContext>.Instance.SECTRDirector.DeregisterHibernator(this);
    DeregisterMember();
  }

  private void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.SECTRDirector != null))
      return;
    SRSingleton<SceneContext>.Instance.SECTRDirector.DeregisterHibernator(this);
    DeregisterMember();
  }

  private void RegisterMember()
  {
    SECTR_Member component = GetComponent<SECTR_Member>();
    if (!(component != null) || !(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.SECTRDirector != null))
      return;
    SRSingleton<SceneContext>.Instance.SECTRDirector.RegisterMember(component);
  }

  private void DeregisterMember()
  {
    SECTR_Member component = GetComponent<SECTR_Member>();
    if (!(component != null) || !(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.SECTRDirector != null))
      return;
    SRSingleton<SceneContext>.Instance.SECTRDirector.DeregisterMember(component);
  }

  public void OnUpdate()
  {
    if (setId == SECTR_Sector.SectorSetId.UNSET)
      setId = SECTR_Sector.GetSectorSetForPos(transform.position);
    bool flag1 = SECTR_Sector.IsCurrSectorSet(setId);
    bool flag2 = !flag1;
    int count = cachedMember.sectors.Count;
    bool flag3 = count > 0 || !flag1;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Sector sector = cachedMember.sectors[index];
      if (sector.isFrozen)
        flag2 = true;
      if (!sector.isHibernating)
        flag3 = false;
    }
    if (flag2 | flag3 && !hibernating)
      _Hibernate();
    else if (!(flag2 | flag3) && hibernating)
      _WakeUp();
    if (!hibernating || HibernateUpdate == null)
      return;
    HibernateUpdate();
  }

  private void _WakeUp()
  {
    if (!hibernating)
      return;
    hibernating = false;
    RegisterMember();
    _UpdateComponents();
    if (Awoke == null)
      return;
    Awoke();
  }

  private void _Hibernate()
  {
    if (hibernating)
      return;
    hibernating = true;
    DeregisterMember();
    _UpdateComponents();
    if (Hibernated == null)
      return;
    Hibernated();
  }

  private void _UpdateComponents()
  {
    if (HibernateBehaviors)
    {
      Behaviour[] behaviourArray = HibernateChildren ? GetComponentsInChildren<Behaviour>() : GetComponents<Behaviour>();
      int length = behaviourArray.Length;
      for (int index = 0; index < length; ++index)
      {
        Behaviour behaviour = behaviourArray[index];
        if (behaviour.GetType() != typeof (SECTR_Hibernator) && behaviour.GetType() != typeof (SECTR_Member))
          behaviour.enabled = !hibernating;
      }
    }
    if (HibernateRigidBodies)
    {
      Rigidbody[] rigidbodyArray = HibernateChildren ? GetComponentsInChildren<Rigidbody>() : GetComponents<Rigidbody>();
      int length = rigidbodyArray.Length;
      for (int index = 0; index < length; ++index)
      {
        Rigidbody rigidbody = rigidbodyArray[index];
        if (hibernating)
        {
          vel = rigidbody.velocity;
          angularVel = rigidbody.angularVelocity;
          kinematic = rigidbody.isKinematic;
          rigidbody.Sleep();
          rigidbody.isKinematic = true;
        }
        else if (rigidbody.IsSleeping())
        {
          rigidbody.isKinematic = kinematic;
          rigidbody.WakeUp();
          rigidbody.velocity = vel;
          rigidbody.angularVelocity = angularVel;
        }
      }
    }
    if (HibernateColliders)
    {
      Collider[] colliderArray = HibernateChildren ? GetComponentsInChildren<Collider>() : GetComponents<Collider>();
      int length = colliderArray.Length;
      for (int index = 0; index < length; ++index)
        colliderArray[index].enabled = !hibernating;
    }
    if (!HibernateRenderers)
      return;
    Renderer[] rendererArray = HibernateChildren ? GetComponentsInChildren<Renderer>() : GetComponents<Renderer>();
    int length1 = rendererArray.Length;
    for (int index = 0; index < length1; ++index)
      rendererArray[index].enabled = !hibernating;
  }

  internal void RecheckHibernation() => OnUpdate();

  public delegate void HibernateCallback();
}
