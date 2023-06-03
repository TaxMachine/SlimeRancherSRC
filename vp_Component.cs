// Decompiled with JetBrains decompiler
// Type: vp_Component
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class vp_Component : MonoBehaviour, EventHandlerRegistrable
{
  public bool Persist;
  protected StateManager m_StateManager;
  protected vp_EventHandler m_EventHandler;
  [NonSerialized]
  protected vp_State m_DefaultState;
  protected bool m_Initialized;
  protected Transform m_Transform;
  protected Transform m_Parent;
  protected Transform m_Root;
  protected AudioSource m_Audio;
  protected Collider m_Collider;
  public List<vp_State> States = new List<vp_State>();
  public List<vp_Component> Children = new List<vp_Component>();
  public List<vp_Component> Siblings = new List<vp_Component>();
  public List<vp_Component> Family = new List<vp_Component>();
  public List<Renderer> Renderers = new List<Renderer>();
  public List<AudioSource> AudioSources = new List<AudioSource>();
  protected Type m_Type;
  protected FieldInfo[] m_Fields;
  protected vp_Timer.Handle m_DeactivationTimer = new vp_Timer.Handle();

  protected virtual StateManager GetStateManager() => new EmptyStateManager<vp_Component>(this);

  public vp_EventHandler EventHandler
  {
    get
    {
      if (m_EventHandler == null)
        m_EventHandler = (vp_EventHandler) Transform.root.GetComponentInChildren(typeof (vp_EventHandler));
      return m_EventHandler;
    }
  }

  public Type Type
  {
    get
    {
      if (m_Type == null)
        m_Type = GetType();
      return m_Type;
    }
  }

  public StateManager StateManager
  {
    get
    {
      if (m_StateManager == null)
        m_StateManager = GetStateManager();
      return m_StateManager;
    }
  }

  public vp_State DefaultState => m_DefaultState;

  public float Delta => Time.deltaTime * 60f;

  public float SDelta => Time.smoothDeltaTime * 60f;

  public Transform Transform
  {
    get
    {
      if (m_Transform == null)
        m_Transform = transform;
      return m_Transform;
    }
  }

  public Transform Parent
  {
    get
    {
      if (m_Parent == null)
        m_Parent = transform.parent;
      return m_Parent;
    }
  }

  public Transform Root
  {
    get
    {
      if (m_Root == null)
        m_Root = transform.root;
      return m_Root;
    }
  }

  public AudioSource Audio
  {
    get
    {
      if (m_Audio == null)
        m_Audio = GetComponent<AudioSource>();
      return m_Audio;
    }
  }

  public Collider Collider
  {
    get
    {
      if (m_Collider == null)
        m_Collider = GetComponent<Collider>();
      return m_Collider;
    }
  }

  public bool Rendering
  {
    get => Renderers.Count > 0 && Renderers[0].enabled;
    set
    {
      foreach (Renderer renderer in Renderers)
      {
        if (!(renderer == null))
          renderer.enabled = value;
      }
    }
  }

  protected virtual void Awake()
  {
    CacheChildren();
    CacheSiblings();
    CacheFamily();
    CacheRenderers();
    CacheAudioSources();
    StateManager.SetState("Default", enabled);
  }

  protected virtual void Start() => ResetState();

  protected virtual void Init()
  {
  }

  protected virtual void OnEnable()
  {
    if (!(EventHandler != null))
      return;
    Register(EventHandler);
  }

  protected virtual void OnDisable()
  {
    if (!(EventHandler != null))
      return;
    Unregister(EventHandler);
  }

  protected virtual void Update()
  {
    if (m_Initialized)
      return;
    Init();
    m_Initialized = true;
  }

  protected virtual void FixedUpdate()
  {
  }

  protected virtual void LateUpdate()
  {
  }

  public void SetState(string state, bool enabled = true, bool recursive = false, bool includeDisabled = false)
  {
    StateManager.SetState(state, enabled);
    if (!recursive)
      return;
    foreach (vp_Component child in Children)
    {
      if (includeDisabled || vp_Utility.IsActive(child.gameObject) && child.enabled)
        child.SetState(state, enabled, true, includeDisabled);
    }
  }

  public void ActivateGameObject(bool setActive = true)
  {
    if (setActive)
    {
      Activate();
      foreach (vp_Component sibling in Siblings)
        sibling.Activate();
      VerifyRenderers();
    }
    else
    {
      DeactivateWhenSilent();
      foreach (vp_Component sibling in Siblings)
        sibling.DeactivateWhenSilent();
    }
  }

  public void ResetState()
  {
    StateManager.Reset();
    Refresh();
  }

  public bool StateEnabled(string stateName) => StateManager.IsEnabled(stateName);

  public void RefreshDefaultState()
  {
    vp_State vpState = null;
    if (States.Count == 0)
    {
      vpState = new vp_State(Type.Name, "Default");
      States.Add(vpState);
    }
    else
    {
      for (int index = States.Count - 1; index > -1; --index)
      {
        if (States[index].Name == "Default")
        {
          vpState = States[index];
          States.Remove(vpState);
          States.Add(vpState);
        }
      }
      if (vpState == null)
      {
        vpState = new vp_State(Type.Name, "Default");
        States.Add(vpState);
      }
    }
    if (vpState.Preset == null || vpState.Preset.ComponentType == null)
      vpState.Preset = new vp_ComponentPreset();
    if (vpState.TextAsset == null)
      vpState.Preset.InitFromComponent(this);
    vpState.Enabled = true;
    m_DefaultState = vpState;
  }

  public void ApplyPreset(vp_ComponentPreset preset)
  {
    vp_ComponentPreset.Apply(this, preset);
    RefreshDefaultState();
    Refresh();
  }

  public vp_ComponentPreset Load(string path)
  {
    vp_ComponentPreset vpComponentPreset = vp_ComponentPreset.LoadFromResources(this, path);
    RefreshDefaultState();
    Refresh();
    return vpComponentPreset;
  }

  public vp_ComponentPreset Load(TextAsset asset)
  {
    vp_ComponentPreset vpComponentPreset = vp_ComponentPreset.LoadFromTextAsset(this, asset);
    RefreshDefaultState();
    Refresh();
    return vpComponentPreset;
  }

  public void CacheChildren()
  {
    Children.Clear();
    foreach (vp_Component componentsInChild in GetComponentsInChildren<vp_Component>(true))
    {
      if (componentsInChild.transform.parent == transform)
        Children.Add(componentsInChild);
    }
  }

  public void CacheSiblings()
  {
    Siblings.Clear();
    foreach (vp_Component component in GetComponents<vp_Component>())
    {
      if (component != this)
        Siblings.Add(component);
    }
  }

  public void CacheFamily()
  {
    Family.Clear();
    foreach (vp_Component componentsInChild in transform.root.GetComponentsInChildren<vp_Component>(true))
    {
      if (componentsInChild != this)
        Family.Add(componentsInChild);
    }
  }

  public void CacheRenderers()
  {
    Renderers.Clear();
    foreach (Renderer componentsInChild in GetComponentsInChildren<Renderer>(true))
      Renderers.Add(componentsInChild);
  }

  protected void VerifyRenderers()
  {
    if (Renderers.Count == 0 || !(Renderers[0] == null) && vp_Utility.IsDescendant(Renderers[0].transform, Transform))
      return;
    Renderers.Clear();
    CacheRenderers();
  }

  public void CacheAudioSources()
  {
    AudioSources.Clear();
    foreach (AudioSource componentsInChild in GetComponentsInChildren<AudioSource>(true))
      AudioSources.Add(componentsInChild);
  }

  public virtual void Activate()
  {
    m_DeactivationTimer.Cancel();
    vp_Utility.Activate(gameObject);
  }

  public virtual void Deactivate() => vp_Utility.Activate(gameObject, false);

  public void DeactivateWhenSilent()
  {
    if (this == null)
      return;
    if (vp_Utility.IsActive(gameObject))
    {
      foreach (AudioSource audioSource in AudioSources)
      {
        if (audioSource.isPlaying && !audioSource.loop)
        {
          Rendering = false;
          vp_Timer.In(0.1f, () => DeactivateWhenSilent(), m_DeactivationTimer);
          return;
        }
      }
    }
    Deactivate();
  }

  public virtual void Refresh()
  {
  }

  public virtual void Register(vp_EventHandler eventHandler)
  {
  }

  public virtual void Unregister(vp_EventHandler eventHandler)
  {
  }
}
