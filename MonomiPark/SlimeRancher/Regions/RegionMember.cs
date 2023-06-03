// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Regions.RegionMember
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonomiPark.SlimeRancher.Regions
{
  public class RegionMember : 
    SRBehaviour,
    ActorModel.Participant,
    GadgetModel.Participant,
    PlayerModel.Participant
  {
    public bool canHibernate = true;
    [NonSerialized]
    public List<Region> regions = new List<Region>(4);
    private ActorModel actorModel;
    private DroneModel droneModel;
    private Vector3 lastMemberCheckPos;
    private RegionRegistry regionRegistry;
    private static List<Region> newRegions = new List<Region>(4);
    private static List<Region> leftRegions = new List<Region>(4);
    private const float MEMBER_RECHECK_DIST = 1f;
    private const float MEMBER_RECHECK_DIST_SQR = 1f;
    private bool regionsInitialized;
    private bool hibernating;
    private Vector3 hibernatingVelocity;
    private Vector3 hibernatingAngularVelocity;
    private bool hibernatingIsKinematic;
    private CollisionDetectionMode hibernatingCollisionDetectionMode;
    private List<Behaviour> hibernatingBehaviours = new List<Behaviour>();
    private List<Collider> hibernatingColliders = new List<Collider>();
    private List<Renderer> hibernatingRenderers = new List<Renderer>();

    public event MembershipChanged regionsChanged;

    public RegionRegistry.RegionSetId setId
    {
      get
      {
        if (actorModel != null)
          return actorModel.currRegionSetId;
        return droneModel == null ? RegionRegistry.RegionSetId.UNSET : droneModel.currRegionSetId;
      }
    }

    public void Awake()
    {
      if (Identifiable.GetId(gameObject) != Identifiable.Id.PLAYER)
        return;
      SRSingleton<SceneContext>.Instance.GameModel.RegisterPlayerParticipant(this);
    }

    public void InitModel(ActorModel model)
    {
    }

    public void SetModel(ActorModel model) => actorModel = model;

    public void InitModel(GadgetModel model)
    {
    }

    public void SetModel(GadgetModel model) => droneModel = model as DroneModel;

    public void RegionSetChanged(
      RegionRegistry.RegionSetId previous,
      RegionRegistry.RegionSetId current)
    {
      UpdateRegionMembership(true);
    }

    public void Start()
    {
      regionRegistry = SRSingleton<SceneContext>.Instance.RegionRegistry;
      regionRegistry.RegisterMember(this);
      UpdateRegionMembership(false);
    }

    public void OnEnable()
    {
      if (!(regionRegistry != null))
        return;
      regionRegistry.RegisterMember(this);
      UpdateRegionMembership(false);
    }

    public void OnDisable()
    {
      if (!(regionRegistry != null))
        return;
      regionRegistry.DeregisterMember(this);
    }

    public void OnDestroy()
    {
      for (int index = 0; index < regions.Count; ++index)
        regions[index].RemoveMember(this);
      if (regionsChanged != null && regions.Count > 0)
        regionsChanged(regions, null);
      regions.Clear();
    }

    public void RegistryUpdate()
    {
      if ((transform.position - lastMemberCheckPos).sqrMagnitude < 1.0)
        return;
      UpdateRegionMembership(false);
    }

    public bool IsInZone(ZoneDirector.Zone zone) => regions.Any(r => r.GetZoneId() == zone);

    public bool IsInRegion(RegionRegistry.RegionSetId regionSetId) => setId == regionSetId;

    public void UpdateRegionMembership(bool forceUpdateEvenWhenHibernating)
    {
      if (!Application.isPlaying || hibernating && !forceUpdateEvenWhenHibernating)
        return;
      newRegions.Clear();
      leftRegions.Clear();
      regionRegistry.GetContaining(setId, ref newRegions, transform.position);
      lastMemberCheckPos = transform.position;
      int index1 = 0;
      int count1 = regions.Count;
      while (index1 < count1)
      {
        Region region = regions[index1];
        if (newRegions.Contains(region))
        {
          newRegions.Remove(region);
          ++index1;
        }
        else
        {
          leftRegions.Add(region);
          regions[index1].RemoveMember(this);
          regions.RemoveAt(index1);
          --count1;
        }
      }
      int count2 = newRegions.Count;
      if (count2 > 0)
      {
        for (int index2 = 0; index2 < count2; ++index2)
        {
          Region newRegion = newRegions[index2];
          regions.Add(newRegion);
          newRegion.AddMember(this);
        }
      }
      if (leftRegions.Count > 0 || newRegions.Count > 0 || !regionsInitialized)
        UpdateHibernation();
      regionsInitialized = true;
      if (leftRegions.Count <= 0 && newRegions.Count <= 0 || regionsChanged == null)
        return;
      regionsChanged(leftRegions, newRegions);
    }

    public void UpdateHibernation()
    {
      if (!canHibernate)
        return;
      bool flag1;
      if (!regionRegistry.IsCurrRegionSet(setId))
      {
        flag1 = true;
      }
      else
      {
        bool flag2 = false;
        int count = regions.Count;
        bool flag3 = count > 0;
        for (int index = 0; index < count; ++index)
        {
          Region region = regions[index];
          if (region.Proxied)
            flag2 = true;
          if (!region.Hibernated)
            flag3 = false;
        }
        flag1 = flag2 | flag3;
      }
      if (flag1 && !hibernating)
      {
        Hibernate();
        gameObject.SetActive(false);
      }
      else
      {
        if (flag1 || !hibernating)
          return;
        Unhibernate();
        gameObject.SetActive(true);
      }
    }

    private void Unhibernate()
    {
      if (!hibernating)
        return;
      hibernating = false;
      UpdateComponents();
    }

    private void Hibernate()
    {
      if (hibernating)
        return;
      hibernating = true;
      UpdateComponents();
    }

    private void UpdateComponents()
    {
      if (hibernating)
      {
        Behaviour[] componentsInChildren = GetComponentsInChildren<Behaviour>();
        int length = componentsInChildren.Length;
        for (int index = 0; index < length; ++index)
        {
          Behaviour behaviour = componentsInChildren[index];
          if (behaviour != null && behaviour.enabled && behaviour.GetType() != typeof (RegionMember))
          {
            hibernatingBehaviours.Add(behaviour);
            behaviour.enabled = false;
          }
        }
      }
      else
      {
        int count = hibernatingBehaviours.Count;
        for (int index = 0; index < count; ++index)
        {
          Behaviour hibernatingBehaviour = hibernatingBehaviours[index];
          if (hibernatingBehaviour != null)
            hibernatingBehaviour.enabled = true;
        }
        hibernatingBehaviours.Clear();
      }
      Rigidbody component = GetComponent<Rigidbody>();
      if (component != null)
      {
        if (hibernating)
        {
          hibernatingVelocity = component.velocity;
          hibernatingAngularVelocity = component.angularVelocity;
          hibernatingIsKinematic = component.isKinematic;
          hibernatingCollisionDetectionMode = component.collisionDetectionMode;
          component.Sleep();
          component.collisionDetectionMode = CollisionDetectionMode.Discrete;
          component.isKinematic = true;
        }
        else if (component.IsSleeping())
        {
          component.isKinematic = hibernatingIsKinematic;
          component.collisionDetectionMode = hibernatingCollisionDetectionMode;
          component.WakeUp();
          component.velocity = hibernatingVelocity;
          component.angularVelocity = hibernatingAngularVelocity;
        }
      }
      if (hibernating)
      {
        Collider[] componentsInChildren = GetComponentsInChildren<Collider>();
        int length = componentsInChildren.Length;
        for (int index = 0; index < length; ++index)
        {
          Collider collider = componentsInChildren[index];
          if (collider.enabled)
          {
            hibernatingColliders.Add(collider);
            collider.enabled = false;
          }
        }
      }
      else
      {
        int count = hibernatingColliders.Count;
        for (int index = 0; index < count; ++index)
        {
          Collider hibernatingCollider = hibernatingColliders[index];
          if (hibernatingCollider != null)
            hibernatingCollider.enabled = true;
        }
        hibernatingColliders.Clear();
      }
      if (hibernating)
      {
        Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
        int length = componentsInChildren.Length;
        for (int index = 0; index < length; ++index)
        {
          Renderer renderer = componentsInChildren[index];
          if (renderer.enabled)
          {
            hibernatingRenderers.Add(renderer);
            renderer.enabled = false;
          }
        }
      }
      else
      {
        int count = hibernatingRenderers.Count;
        for (int index = 0; index < count; ++index)
        {
          Renderer hibernatingRenderer = hibernatingRenderers[index];
          if (hibernatingRenderer != null)
            hibernatingRenderer.enabled = true;
        }
        hibernatingRenderers.Clear();
      }
    }

    public void InitModel(PlayerModel model)
    {
    }

    public void SetModel(PlayerModel model)
    {
    }

    public void TransformChanged(Vector3 pos, Quaternion rot)
    {
    }

    public void RegisteredPotentialAmmoChanged(
      Dictionary<PlayerState.AmmoMode, List<GameObject>> registeredPotentialAmmo)
    {
    }

    public void KeyAdded()
    {
    }

    public delegate void MembershipChanged(List<Region> left, List<Region> joined);
  }
}
