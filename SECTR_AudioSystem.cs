// Decompiled with JetBrains decompiler
// Type: SECTR_AudioSystem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof (AudioListener))]
[ExecuteInEditMode]
[AddComponentMenu("SECTR/Audio/SECTR Audio System")]
public class SECTR_AudioSystem : MonoBehaviour
{
  private static SECTR_AudioSystem system = null;
  private static Stack<Instance> instancePool = null;
  private static Stack<AudioSource> simpleSourcePool = null;
  private static Stack<AudioSource> lowpassSourcePool = null;
  private static Transform sourcePoolParent = null;
  private static List<Instance> activeInstances = null;
  private static Dictionary<SECTR_AudioCue, List<Instance>> maxInstancesTable = null;
  private static Dictionary<SECTR_AudioCue, List<Instance>> proximityTable = null;
  private static float currentTime = 0.0f;
  private static List<SECTR_AudioAmbience> ambienceStack;
  private static SECTR_AudioAmbience currentAmbience = null;
  private static SECTR_AudioCueInstance ambienceLoop;
  private static SECTR_AudioCueInstance ambienceOneShot;
  private static float nextAmbienceOneShotTime = 0.0f;
  private static SECTR_AudioCue currentMusic = null;
  private static SECTR_AudioCueInstance musicLoop;
  private static float windowHDRMax = 0.0f;
  private static float windowHDRMin = 0.0f;
  private static float currentLoudness = 0.0f;
  private static List<SECTR_Graph.Node> occlusionPath;
  private const float EPSILON = 0.001f;
  [SECTR_ToolTip("The maximum number of instances that can be active at once. Inaudible sounds do not count against this limit.")]
  public int MaxInstances = 128;
  [SECTR_ToolTip("The number of instances to allocate with lowpass effects (for occlusion and the like).")]
  public int LowpassInstances = 32;
  [SECTR_ToolTip("The Bus at the top of the mixing heirarchy. Required to play sounds.", null, false)]
  public SECTR_AudioBus MasterBus;
  [SECTR_ToolTip("The baseline settings for any environmental audio. Will be audible when no other ambiences are active.")]
  public SECTR_AudioAmbience DefaultAmbience = new SECTR_AudioAmbience();
  [SECTR_ToolTip("Minimum Loudness for the HDR mixer. Current Loudness will never drop below this.", 0.0f, 200f)]
  public float HDRBaseLoudness = 50f;
  [SECTR_ToolTip("The maximum difference between the loudest sound and the softest sound before sounds are simply culled out.", 0.0f, 200f)]
  public float HDRWindowSize = 50f;
  [SECTR_ToolTip("Speed at which HDR window decays after a loud sound is played.", 0.0f, 100f)]
  public float HDRDecay = 1f;
  [SECTR_ToolTip("Should sounds close to the listener be blended into 2D (to avoid harsh stereo switching).")]
  public bool BlendNearbySounds = true;
  [SECTR_ToolTip("Objects close to the listener will be blended into 2D, as a kind of fake HRTF. This determines the start and end of that blend.", "BlendNearbySounds")]
  public Vector2 NearBlendRange = new Vector2(0.25f, 0.75f);
  [SECTR_ToolTip("Determines what kind of logic to use for computing sound occlusion.", null, typeof (OcclusionModes))]
  public OcclusionModes OcclusionFlags;
  [SECTR_ToolTip("The distance beyond which sounds will be considered occluded, if Distance occlusion is enabled.", "OcclusionFlags")]
  public float OcclusionDistance = 100f;
  [SECTR_ToolTip("The layers to test against when raycasting for occlusion.", "OcclusionFlags")]
  public LayerMask RaycastLayers = (LayerMask) -5;
  public static int LayerMask { get; set; }

  [SECTR_ToolTip("The amount by which to decrease the volume of occluded sounds.", "OcclusionFlags", 0.0f, 1f)]
  public float OcclusionVolume = 0.5f;
  [SECTR_ToolTip("The frequency cutoff of the lowpass filter for occluded sounds.", "OcclusionFlags", 10f, 22000f)]
  public float OcclusionCutoff = 2200f;
  [SECTR_ToolTip("The resonance Q of the lowpass filter for occluded sounds.", "OcclusionFlags", 1f, 10f)]
  public float OcclusionResonanceQ = 1f;
  [SECTR_ToolTip("The amount of time between tests to see if looping sounds should start or stop running.")]
  public Vector2 RetestInterval = new Vector2(0.5f, 1f);
  [SECTR_ToolTip("The amount of buffer to give before culling distant sounds.")]
  public float CullingBuffer = 10f;
  [SECTR_ToolTip("Enable or disable of the in-game audio HUD.", true)]
  public bool ShowAudioHUD;
  [SECTR_ToolTip("In the editor only, puts the listener at the AudioSystem, not at the Scene Camera.", true)]
  public bool Debugging;
  private static bool firstMaxInstanceWarning = true;

  private static int GetInstancesCount(SECTR_AudioCue cue)
  {
    List<Instance> instanceList;
    maxInstancesTable.TryGetValue(cue, out instanceList);
    return instanceList == null ? 0 : instanceList.Count;
  }

  public static bool Initialized => system != null;

  public static SECTR_AudioSystem System => system;

  public static Transform Listener => system.transform;

  public static SECTR_AudioCueInstance Play(SECTR_AudioCue audioCue, Vector3 position, bool loop) => Play(audioCue, null, position, loop);

  public static SECTR_AudioCueInstance Play(
    SECTR_AudioCue audioCue,
    Transform parent,
    Vector3 localPosition,
    bool loop,
    int? clipIndex = null,
    bool hasPriority = false)
  {
    if (!Initialized)
      return new SECTR_AudioCueInstance();
    if (system.MasterBus == null)
    {
      Debug.LogWarning("SECTR_AudioSystem needs a Master Bus before you can play sounds.");
      return new SECTR_AudioCueInstance();
    }
    if (activeInstances.Count >= system.MaxInstances)
    {
      Debug.LogWarning("Global max audio instances exceeded.");
      if (firstMaxInstanceWarning)
      {
        firstMaxInstanceWarning = false;
        foreach (Instance activeInstance in activeInstances)
          Debug.LogWarning("Instance: " + activeInstance.Cue.name);
      }
      return new SECTR_AudioCueInstance();
    }
    if (audioCue == null)
      return new SECTR_AudioCueInstance();
    if (!_CheckInstances(audioCue, false))
    {
      if (!hasPriority || !maxInstancesTable.ContainsKey(audioCue))
        return new SECTR_AudioCueInstance();
      Instance instance = maxInstancesTable[audioCue].FirstOrDefault(i => !i.HasPriority);
      if (instance == null)
        return new SECTR_AudioCueInstance();
      instance.Stop(true);
    }
    else if (audioCue.AudioClips.Count == 0)
    {
      Debug.LogWarning("Cannot play a clipless Audio Cues.");
      return new SECTR_AudioCueInstance();
    }
    SECTR_AudioCue sourceCue = audioCue.SourceCue;
    if (UnityEngine.Random.value <= (double) sourceCue.PlayProbability)
    {
      bool flag = sourceCue.IsLocal || _CheckProximity(audioCue, parent, localPosition, null);
      loop |= sourceCue.Loops;
      if (flag | loop)
      {
        Instance internalInstance = instancePool.Pop();
        if (internalInstance != null)
        {
          internalInstance.Init(audioCue, parent, localPosition, loop, clipIndex, hasPriority);
          if (flag)
            internalInstance.Play();
          activeInstances.Add(internalInstance);
          return new SECTR_AudioCueInstance(internalInstance, internalInstance.Generation, loop);
        }
      }
    }
    return new SECTR_AudioCueInstance();
  }

  public static SECTR_AudioCueInstance Clone(SECTR_AudioCueInstance instance, Vector3 newPosition)
  {
    if (!instance)
      return new SECTR_AudioCueInstance();
    Instance internalInstance = instancePool.Pop();
    internalInstance.Clone((Instance) instance.GetInternalInstance(), newPosition);
    return new SECTR_AudioCueInstance(internalInstance, internalInstance.Generation, internalInstance.Loops);
  }

  public static void PlayMusic(SECTR_AudioCue musicCue)
  {
    if (!Initialized)
    {
      Debug.LogWarning("Cannot play music before Audio System is initialized.");
    }
    else
    {
      if (!(musicCue != null))
        return;
      if (musicCue.Is3D)
        Debug.LogWarning("Music Cue " + musicCue.name + "is 3Dm but music should be Simple 2D.");
      musicLoop.Stop(false);
      currentMusic = musicCue;
      musicLoop = Play(currentMusic, Listener, Vector3.zero, true);
    }
  }

  public static void StopMusic(bool stopImmediate)
  {
    if (!Initialized)
      return;
    musicLoop.Stop(stopImmediate);
    currentMusic = null;
  }

  public static void PushAmbience(SECTR_AudioAmbience ambience)
  {
    if (!Initialized)
    {
      Debug.LogWarning("Cannot activate an ambience before audio system is initialzied.");
    }
    else
    {
      if (ambience == null)
        return;
      ambienceStack.Add(ambience);
    }
  }

  public static void RemoveAmbience(SECTR_AudioAmbience ambience)
  {
    if (!Initialized || ambience == null)
      return;
    ambienceStack.Remove(ambience);
  }

  public static float GetBusVolume(string busName)
  {
    if (!Initialized)
      Debug.LogWarning("Cannot get bus volume before Audio System is initialzied.");
    else if (!string.IsNullOrEmpty(busName))
      return GetBusVolume(_FindBus(system.MasterBus, busName));
    return 0.0f;
  }

  public static float GetBusVolume(SECTR_AudioBus bus)
  {
    if (!Initialized)
      Debug.LogWarning("Cannot get bus volume before Audio System is initialzied.");
    else if ((bool) (UnityEngine.Object) bus)
      return bus.UserVolume;
    return 0.0f;
  }

  public static void SetBusVolume(string busName, float volume)
  {
    if (!Initialized)
    {
      Debug.LogWarning("Cannot activate an ambience before audio system is initialzied.");
    }
    else
    {
      if (string.IsNullOrEmpty(busName))
        return;
      SetBusVolume(_FindBus(system.MasterBus, busName), volume);
    }
  }

  public static void SetBusVolume(SECTR_AudioBus bus, float volume)
  {
    if (!Initialized)
    {
      Debug.LogWarning("Cannot set bus volume before Audio System is initialzied.");
    }
    else
    {
      if (!(bool) (UnityEngine.Object) bus)
        return;
      bus.UserVolume = volume;
    }
  }

  public static void MuteBus(string busName, bool mute)
  {
    if (!Initialized)
    {
      Debug.LogWarning("Cannot mute bus before Audio System is initialzied.");
    }
    else
    {
      if (string.IsNullOrEmpty(busName))
        return;
      MuteBus(_FindBus(system.MasterBus, busName), mute);
    }
  }

  public static void MuteBus(SECTR_AudioBus bus, bool mute)
  {
    if (!Initialized)
    {
      Debug.LogWarning("Cannot mute bus before Audio System is initialzied.");
    }
    else
    {
      if (!(bool) (UnityEngine.Object) bus)
        return;
      bus.Muted = mute;
    }
  }

  public static void PauseBus(string busName, bool paused)
  {
    if (!Initialized)
    {
      Debug.LogWarning("Cannot pause bus before Audio System is initialzied.");
    }
    else
    {
      if (string.IsNullOrEmpty(busName))
        return;
      PauseBus(_FindBus(system.MasterBus, busName), paused);
    }
  }

  public static void PauseBus(SECTR_AudioBus bus, bool paused)
  {
    if (!Initialized)
    {
      Debug.LogWarning("Cannot pause bus before Audio System is initialzied.");
    }
    else
    {
      if (!(bool) (UnityEngine.Object) bus)
        return;
      int num1 = bus.Paused ? 1 : 0;
      bus.Pause(paused);
      int num2 = bus.Paused ? 1 : 0;
      if (num1 == num2)
        return;
      int count = activeInstances.Count;
      for (int index = 0; index < count; ++index)
      {
        Instance activeInstance = activeInstances[index];
        if (bus.IsAncestorOf(activeInstance.Bus))
          activeInstance.Pause(paused);
      }
    }
  }

  public static bool IsOccluded(
    Vector3 worldSpacePosition,
    OcclusionModes occlusionFlags)
  {
    bool flag = false;
    Vector3 position = Listener.position;
    Vector3 direction = position - worldSpacePosition;
    float sqrMagnitude = direction.sqrMagnitude;
    if (!flag && (occlusionFlags & OcclusionModes.Distance) != 0)
      flag = sqrMagnitude >= system.OcclusionDistance * (double) system.OcclusionDistance;
    if (!flag && (occlusionFlags & OcclusionModes.Raycast) != 0)
    {
      float maxDistance = Mathf.Sqrt(sqrMagnitude);
      RaycastHit hitInfo;
      flag = Physics.Raycast(worldSpacePosition, direction, out hitInfo, maxDistance, system.RaycastLayers) && hitInfo.transform != Listener;
    }
    if (!flag && (occlusionFlags & OcclusionModes.Graph) != 0)
    {
      SECTR_Graph.FindShortestPath(ref occlusionPath, worldSpacePosition, position, 0);
      int count = occlusionPath.Count;
      for (int index = 0; index < count && !flag; ++index)
      {
        SECTR_Graph.Node node = occlusionPath[index];
        if ((bool) (UnityEngine.Object) node.Portal && (node.Portal.Flags & SECTR_Portal.PortalFlags.Closed) != 0)
          flag = true;
      }
    }
    return flag;
  }

  private static IEnumerable<SECTR_AudioBus> FindNonUISFX()
  {
    if (System != null && System.MasterBus != null)
    {
      SECTR_AudioBus bus = _FindBus(System.MasterBus, "SFX");
      if (!(bus == null))
      {
        foreach (SECTR_AudioBus child in bus.Children)
        {
          if (child.name != "UI" && child.name != "Pause Transition")
            yield return child;
        }
      }
    }
  }

  public static void PauseNonUISFX(bool pause)
  {
    foreach (SECTR_AudioBus bus in FindNonUISFX())
      PauseBus(bus, pause);
  }

  public static void MuteNonUISFX(bool mute)
  {
    foreach (SECTR_AudioBus bus in FindNonUISFX())
      MuteBus(bus, mute);
  }

  private void OnEnable()
  {
    if ((bool) (UnityEngine.Object) system && system != this)
    {
      Log.Error("Found duplicate SECTR_AudioSystem singleton instance.");
      Destroyer.Destroy(this, "SECTR_AudioSystem.OnEnable");
    }
    else
    {
      if (!(system == null))
        return;
      system = this;
      instancePool = new Stack<Instance>(MaxInstances);
      for (int index = 0; index < MaxInstances; ++index)
        instancePool.Push(new Instance());
      int capacity1 = SECTR_Modules.HasPro() ? Mathf.Max(0, MaxInstances - LowpassInstances) : MaxInstances;
      int capacity2 = MaxInstances - capacity1;
      simpleSourcePool = new Stack<AudioSource>(capacity1);
      lowpassSourcePool = SECTR_Modules.HasPro() ? new Stack<AudioSource>(capacity2) : null;
      HideFlags hideFlags = HideFlags.HideAndDontSave;
      GameObject gameObject1 = new GameObject("SourcePool");
      gameObject1.hideFlags = hideFlags;
      sourcePoolParent = gameObject1.transform;
      sourcePoolParent.transform.parent = sourcePoolParent;
      for (int index = 0; index < capacity1; ++index)
      {
        GameObject gameObject2 = new GameObject("SimpleInstance" + index);
        gameObject2.hideFlags = hideFlags;
        gameObject2.transform.parent = sourcePoolParent.transform;
        AudioSource audioSource = gameObject2.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        gameObject2.SetActive(false);
        simpleSourcePool.Push(audioSource);
      }
      for (int index = 0; index < capacity2; ++index)
      {
        GameObject gameObject3 = new GameObject("LowpassInstance" + index);
        gameObject3.hideFlags = hideFlags;
        gameObject3.transform.parent = sourcePoolParent.transform;
        AudioSource audioSource = gameObject3.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        gameObject3.AddComponent<AudioLowPassFilter>().enabled = false;
        gameObject3.SetActive(false);
        lowpassSourcePool.Push(audioSource);
      }
      ambienceStack = new List<SECTR_AudioAmbience>(32);
      activeInstances = new List<Instance>(MaxInstances);
      maxInstancesTable = new Dictionary<SECTR_AudioCue, List<Instance>>(MaxInstances / 8);
      proximityTable = new Dictionary<SECTR_AudioCue, List<Instance>>(MaxInstances / 8);
      double num = _UpdateTime();
      windowHDRMax = HDRBaseLoudness;
      windowHDRMin = windowHDRMax - HDRWindowSize;
      occlusionPath = new List<SECTR_Graph.Node>(32);
      if (MasterBus != null)
      {
        MasterBus.ResetUserVolume();
        _UpdateBusPitchVolume(MasterBus, 1f, 1f);
      }
      else
        Debug.LogWarning("SECTR AudioSystem has no MasterBus. Game sounds will not play.");
      MasterBus.ResetPauseState();
    }
  }

  private void OnDisable()
  {
    if (!(system == this))
      return;
    int count = activeInstances.Count;
    for (int index = 0; index < count; ++index)
      activeInstances[index].Stop(true);
    MasterBus.ResetPauseState();
    if ((bool) (UnityEngine.Object) sourcePoolParent)
    {
      Destroyer.Destroy(sourcePoolParent.gameObject, "SECTR_AudioSystem.OnDisable#1");
      sourcePoolParent = null;
    }
    system = null;
    activeInstances = null;
    maxInstancesTable = null;
    proximityTable = null;
    instancePool = null;
    simpleSourcePool = null;
    lowpassSourcePool = null;
    currentTime = 0.0f;
    ambienceStack = null;
    currentAmbience = null;
    nextAmbienceOneShotTime = 0.0f;
    currentMusic = null;
    occlusionPath = null;
  }

  private void LateUpdate()
  {
    if (!(system == this) || AudioListener.pause || !(bool) (UnityEngine.Object) MasterBus)
      return;
    float deltaTime = _UpdateTime();
    _UpdateBusPitchVolume(MasterBus, 1f, 1f);
    _UpdateAmbience();
    windowHDRMax = Mathf.Max(HDRBaseLoudness, windowHDRMax - HDRDecay * deltaTime);
    windowHDRMin = windowHDRMax - HDRWindowSize;
    currentLoudness = 0.0f;
    int count = activeInstances.Count;
    int index = 0;
    while (index < count)
    {
      Instance activeInstance = activeInstances[index];
      activeInstance.Update(deltaTime, false);
      if (!activeInstance.Active && !activeInstance.FadingOut)
      {
        activeInstance.Uninit();
        activeInstances.RemoveAt(index);
        instancePool.Push(activeInstance);
        --count;
      }
      else
        ++index;
    }
    currentLoudness = 10f * Mathf.Log10(currentLoudness);
    windowHDRMax = Mathf.Max(currentLoudness, windowHDRMax);
  }

  private static bool _CheckInstances(SECTR_AudioCue audioCue, bool isPlaying)
  {
    int maxInstances = audioCue.SourceCue.MaxInstances;
    if (isPlaying)
      ++maxInstances;
    return maxInstances <= 0 || GetInstancesCount(audioCue) < maxInstances;
  }

  private static bool _CheckProximity(
    SECTR_AudioCue audioCue,
    Transform parent,
    Vector3 position,
    Instance testInstance)
  {
    if ((bool) (UnityEngine.Object) parent)
      position = parent.localToWorldMatrix.MultiplyPoint3x4(position);
    SECTR_AudioCue sourceCue = audioCue.SourceCue;
    float num1 = sourceCue.MaxDistance + system.CullingBuffer;
    if (Vector3.SqrMagnitude(position - Listener.position) > num1 * (double) num1)
      return false;
    int proximityLimit = sourceCue.ProximityLimit;
    List<Instance> instanceList;
    if (proximityLimit > 0 && proximityTable.TryGetValue(audioCue, out instanceList))
    {
      int count = instanceList.Count;
      if (count > proximityLimit)
      {
        float num2 = sourceCue.MaxDistance + sourceCue.MaxDistance;
        int num3 = 0;
        for (int index = 0; index < count; ++index)
        {
          Instance instance = instanceList[index];
          if (instance != testInstance && Vector3.SqrMagnitude(position - instance.Position) < (double) num2 && ++num3 >= proximityLimit)
            return false;
        }
      }
    }
    return true;
  }

  private static float _UpdateTime()
  {
    double dspTime = AudioSettings.dspTime;
    float num = (float) dspTime - currentTime;
    currentTime = (float) dspTime;
    return num;
  }

  private static void _UpdateBusPitchVolume(
    SECTR_AudioBus bus,
    float effectiveVolume,
    float effectivePitch)
  {
    if (!(bool) (UnityEngine.Object) bus)
      return;
    bus.EffectiveVolume = effectiveVolume;
    bus.EffectivePitch = effectivePitch;
    int count = bus.Children.Count;
    for (int index = 0; index < count; ++index)
      _UpdateBusPitchVolume(bus.Children[index], bus.EffectiveVolume, bus.EffectivePitch);
  }

  private static void _UpdateAmbience()
  {
    SECTR_AudioAmbience sectrAudioAmbience = ambienceStack.Count > 0 ? ambienceStack[ambienceStack.Count - 1] : system.DefaultAmbience;
    if (sectrAudioAmbience != currentAmbience)
    {
      ambienceLoop.Stop(false);
      ambienceOneShot.Stop(false);
      currentAmbience = sectrAudioAmbience;
      if (currentAmbience != null)
      {
        if (currentAmbience.OneShots.Count > 0)
          nextAmbienceOneShotTime = currentTime + UnityEngine.Random.Range(currentAmbience.OneShotInterval.x, currentAmbience.OneShotInterval.y);
        if ((bool) (UnityEngine.Object) currentAmbience.BackgroundLoop)
          ambienceLoop = currentAmbience.BackgroundLoop.Spatialization != SECTR_AudioCue.Spatializations.Infinite3D ? Play(currentAmbience.BackgroundLoop, Listener, Vector3.zero, true) : Play(currentAmbience.BackgroundLoop, Listener, UnityEngine.Random.onUnitSphere, true);
      }
    }
    if (currentAmbience == null)
      return;
    if (currentAmbience.OneShots.Count > 0 && currentTime >= (double) nextAmbienceOneShotTime)
    {
      SECTR_AudioCue oneShot = currentAmbience.OneShots[UnityEngine.Random.Range(0, currentAmbience.OneShots.Count)];
      if (oneShot != null)
      {
        if (oneShot.SourceCue.Loops)
        {
          Debug.LogWarning("Cannot play ambient one shot " + oneShot.name + ". It is set to loop.");
        }
        else
        {
          if (!oneShot.IsLocal)
            Debug.LogWarning("Ambient one shot " + oneShot.name + "should be 2D or Infinite 3D.");
          ambienceOneShot = Play(oneShot, Listener, UnityEngine.Random.onUnitSphere, false);
        }
      }
      nextAmbienceOneShotTime = currentTime + UnityEngine.Random.Range(currentAmbience.OneShotInterval.x, currentAmbience.OneShotInterval.y);
    }
    if (ambienceLoop)
      ambienceLoop.Volume = currentAmbience.Volume;
    if (!ambienceOneShot)
      return;
    ambienceOneShot.Volume = currentAmbience.Volume;
  }

  private static SECTR_AudioBus _FindBus(SECTR_AudioBus bus, string busName)
  {
    if ((bool) (UnityEngine.Object) bus)
    {
      if (bus.name == busName)
        return bus;
      int count = bus.Children.Count;
      for (int index = 0; index < count; ++index)
      {
        SECTR_AudioBus bus1 = _FindBus(bus.Children[index], busName);
        if ((bool) (UnityEngine.Object) bus1)
          return bus1;
      }
    }
    return null;
  }

  private class Instance : SECTR_IAudioInstance
  {
    private int? clipIndex;
    private bool hasPriority;
    private int pauseCount;
    private int generation;
    private AudioSource source;
    private AudioLowPassFilter lowpass;
    private SECTR_AudioCue audioCue;
    private Transform parent;
    private Vector3 localPosition = Vector3.zero;
    private Flags flags;
    private float nextTestTime;
    private float fadeStarTime;
    private float basePitch = 1f;
    private float baseVolumeLoudness = 1f;
    private float userVolume = 1f;
    private float userPitch = 1f;
    private float occlusionAlpha = 1f;
    private AnimationCurve hdrCurve;

    public int Generation => generation;

    public bool Active
    {
      get
      {
        if (!Loops && !Delayed && (!(bool) (UnityEngine.Object) source || !source.isPlaying && !Paused && !AudioListener.pause) || FadingOut)
          return false;
        return source == null || source.enabled;
      }
    }

    public Vector3 Position
    {
      get
      {
        Vector3 point = localPosition;
        if ((bool) (UnityEngine.Object) parent)
        {
          if (ThreeD && Local)
            point += parent.transform.position;
          else
            point = parent.localToWorldMatrix.MultiplyPoint3x4(point);
        }
        return point;
      }
      set
      {
        localPosition = !(bool) (UnityEngine.Object) parent ? value : (!ThreeD || !Local ? parent.worldToLocalMatrix.MultiplyPoint3x4(value) : value - parent.transform.position);
        if (!(bool) (UnityEngine.Object) source)
          return;
        source.transform.position = value;
      }
    }

    public Vector3 LocalPosition
    {
      get => localPosition;
      set
      {
        localPosition = value;
        if (!(bool) (UnityEngine.Object) source)
          return;
        source.transform.position = Position;
      }
    }

    public float Volume
    {
      get => userVolume;
      set
      {
        if (userVolume == (double) value)
          return;
        userVolume = Mathf.Clamp01(value);
        Update(0.0f, true);
      }
    }

    public float Pitch
    {
      get => userPitch;
      set
      {
        if (userPitch == (double) value)
          return;
        userPitch = Mathf.Clamp(value, 0.0f, 2f);
        Update(0.0f, true);
      }
    }

    public bool Mute
    {
      get => Mute;
      set
      {
        if (Muted == value)
          return;
        _SetFlag(Flags.Muted, value);
        if (!(bool) (UnityEngine.Object) source)
          return;
        source.mute = value;
      }
    }

    public float TimeSeconds
    {
      get => !(source != null) ? 0.0f : source.time;
      set
      {
        if (!(bool) (UnityEngine.Object) source)
          return;
        source.time = value;
      }
    }

    public int TimeSamples
    {
      get => !(source != null) ? 0 : source.timeSamples;
      set
      {
        if (!(bool) (UnityEngine.Object) source)
          return;
        source.timeSamples = value;
      }
    }

    public bool HasPriority => hasPriority;

    public void ForceInfinite()
    {
      _SetFlag(Flags.ForcedInfinite, true);
      _SetFlag(Flags.Local, true);
      _SetFlag(Flags.ThreeD, true);
      occlusionAlpha = 1f;
      if ((bool) (UnityEngine.Object) source)
      {
        source.rolloffMode = AudioRolloffMode.Linear;
        source.maxDistance = 1000000f;
        source.minDistance = source.maxDistance - 1f / 1000f;
        source.dopplerLevel = 0.0f;
      }
      Update(0.0f, true);
    }

    public void ForceOcclusion(bool occluded)
    {
      if (!(bool) (UnityEngine.Object) audioCue || audioCue.SourceCue.Spatialization != SECTR_AudioCue.Spatializations.Occludable3D)
        return;
      _SetFlag(Flags.Occluded, occluded);
    }

    public bool Loops => (flags & Flags.Loops) != 0;

    public bool Local => (flags & Flags.Local) != 0;

    public bool ThreeD => (flags & Flags.ThreeD) != 0;

    public bool FadingIn => (flags & Flags.FadingIn) != 0;

    public bool FadingOut => (flags & Flags.FadingOut) != 0;

    public bool Muted => (flags & Flags.Muted) != 0;

    public bool Paused => (flags & Flags.Paused) != 0;

    public bool HDR => (flags & Flags.HDR) != 0;

    public bool Occludable => (flags & Flags.Occludable) != 0;

    public bool Occluded => (flags & Flags.Occluded) != 0;

    public bool ForcedInfinite => (flags & Flags.ForcedInfinite) != 0;

    public bool Delayed => (flags & Flags.Delayed) != 0;

    public SECTR_AudioBus Bus => !(audioCue != null) ? null : audioCue.Bus;

    public SECTR_AudioCue Cue => audioCue;

    public void Init(
      SECTR_AudioCue audioCue,
      Transform parent,
      Vector3 localPosition,
      bool loops,
      int? clipIndex,
      bool hasPriority)
    {
      if (!(this.audioCue == null))
        return;
      ++generation;
      this.audioCue = audioCue;
      this.clipIndex = clipIndex;
      this.hasPriority = hasPriority;
      SECTR_AudioCue sourceCue = audioCue.SourceCue;
      flags = 0;
      _SetFlag(Flags.Loops, loops);
      _SetFlag(Flags.Local, sourceCue.IsLocal);
      _SetFlag(Flags.ThreeD, sourceCue.Is3D);
      _SetFlag(Flags.HDR, sourceCue.HDR);
      _SetFlag(Flags.Occludable, system.OcclusionFlags != 0 && sourceCue.Spatialization == SECTR_AudioCue.Spatializations.Occludable3D);
      userVolume = 1f;
      userPitch = 1f;
      this.parent = !Local ? parent : Listener;
      this.localPosition = localPosition;
      _AddProximityInstance(sourceCue);
      _ScheduleNextTest();
    }

    public void Clone(Instance instance, Vector3 newPosition)
    {
      if (!instance.Active)
        return;
      ++generation;
      audioCue = instance.audioCue;
      flags = instance.flags;
      fadeStarTime = instance.fadeStarTime;
      basePitch = instance.basePitch;
      baseVolumeLoudness = instance.baseVolumeLoudness;
      userVolume = instance.userVolume;
      userPitch = instance.userPitch;
      occlusionAlpha = instance.occlusionAlpha;
      hdrCurve = instance.hdrCurve;
      parent = instance.parent;
      Position = newPosition;
      _AddProximityInstance(audioCue.SourceCue);
      _ScheduleNextTest();
      if (!_AcquireSource())
        return;
      Update(0.0f, true);
      if (!(bool) (UnityEngine.Object) source)
        return;
      _SetFlag(Flags.Paused, false);
      source.clip = instance.source.clip;
      source.timeSamples = instance.source.timeSamples;
      PlaySource();
    }

    public void Uninit()
    {
      if (!(audioCue != null))
        return;
      List<Instance> instanceList;
      if (audioCue.SourceCue.ProximityLimit > 0 && proximityTable.TryGetValue(audioCue, out instanceList))
        instanceList.Remove(this);
      _ReleaseSource();
      audioCue = null;
      parent = null;
      flags = 0;
    }

    public void Play()
    {
      SECTR_AudioCue.ClipData clipData = clipIndex.HasValue ? audioCue.AudioClips[clipIndex.Value] : audioCue.GetNextClip();
      if (clipData == null || !(clipData.Clip != null) || !_AcquireSource())
        return;
      if (clipData.Clip.loadState == AudioDataLoadState.Unloaded)
        clipData.Clip.LoadAudioData();
      SECTR_AudioCue sourceCue = audioCue.SourceCue;
      if (sourceCue.FadeInTime > 0.0)
      {
        fadeStarTime = currentTime;
        _SetFlag(Flags.FadingIn, true);
        _SetFlag(Flags.FadingOut, false);
      }
      if (Occludable && !ForcedInfinite)
      {
        _SetFlag(Flags.Occluded, IsOccluded(Position, system.OcclusionFlags));
        occlusionAlpha = Occluded ? 1f : 0.0f;
      }
      baseVolumeLoudness = !HDR ? UnityEngine.Random.Range(sourceCue.Volume.x, sourceCue.Volume.y) : UnityEngine.Random.Range(sourceCue.Loudness.x, sourceCue.Loudness.y);
      baseVolumeLoudness *= clipData.Volume;
      if (HDR)
      {
        if (clipData.HDRCurve != null && clipData.HDRCurve.length > 0)
          hdrCurve = clipData.HDRCurve;
        else
          Debug.LogWarning("Playing " + audioCue.name + " without HDR keys. Bake HDR keys for higher quality audio.");
      }
      Update(0.0f, true);
      if (!(bool) (UnityEngine.Object) source)
        return;
      _SetFlag(Flags.Paused, false);
      source.clip = clipData.Clip;
      if (sourceCue.Delay.y > 0.0)
      {
        _SetFlag(Flags.Delayed, true);
        nextTestTime = currentTime + UnityEngine.Random.Range(sourceCue.Delay.x, sourceCue.Delay.y);
      }
      else
        PlaySource();
    }

    public void Pause(bool paused)
    {
      if (paused)
        ++pauseCount;
      else
        pauseCount = Math.Max(0, pauseCount - 1);
      paused = pauseCount > 0;
      _SetFlag(Flags.Paused, paused);
      if (!(bool) (UnityEngine.Object) source)
        return;
      if (paused)
      {
        source.Pause();
      }
      else
      {
        if (source.isPlaying)
          return;
        PlaySource();
      }
    }

    public void PlaySource()
    {
      if (source != null && Bus != null)
      {
        if (!Bus.Paused)
        {
          source.Play();
        }
        else
        {
          if (!Loops && !source.loop)
            return;
          source.Play();
          source.Pause();
        }
      }
      else
      {
        if (!(source != null))
          return;
        source.Play();
      }
    }

    public void Stop(bool stopImmediately)
    {
      _SetFlag(Flags.Loops, false);
      _Stop(stopImmediately);
    }

    public void SkipFadeIn() => _SetFlag(Flags.FadingIn, false);

    public void Update(float deltaTime, bool volumeOnly)
    {
      if (Delayed)
      {
        if (currentTime < (double) nextTestTime)
          return;
        if (source != null)
          PlaySource();
        _SetFlag(Flags.Delayed, false);
        _ScheduleNextTest();
      }
      Vector3 position1;
      if (ThreeD)
      {
        position1 = Position;
        if ((bool) (UnityEngine.Object) source)
          source.transform.position = position1;
      }
      else
        position1 = Listener.position;
      float num1 = 1f;
      if (FadingIn)
      {
        num1 = Mathf.Clamp01((currentTime - fadeStarTime) / audioCue.SourceCue.FadeInTime);
        if (num1 >= 1.0)
          _SetFlag(Flags.FadingIn, false);
      }
      else if (FadingOut)
      {
        num1 = Mathf.Clamp01((float) (1.0 - (currentTime - fadeStarTime) / (double) audioCue.SourceCue.FadeOutTime));
        if (num1 <= 0.0)
        {
          _SetFlag(Flags.FadingOut, false);
          _Stop(true);
        }
      }
      if ((bool) (UnityEngine.Object) source && ((source.isPlaying ? 1 : (Paused ? 1 : 0)) | (volumeOnly ? 1 : 0)) != 0 && !Muted)
      {
        float num2 = (bool) (UnityEngine.Object) audioCue.Bus ? audioCue.Bus.EffectiveVolume : system.MasterBus.EffectiveVolume;
        float num3 = (bool) (UnityEngine.Object) audioCue.Bus ? audioCue.Bus.EffectivePitch : system.MasterBus.Pitch;
        float num4;
        if (HDR)
        {
          SECTR_AudioCue sourceCue = audioCue.SourceCue;
          float num5 = 1f;
          if (!Local)
          {
            float maxDistance = sourceCue.MaxDistance;
            float minDistance = sourceCue.MinDistance;
            Vector3 position2 = Listener.transform.position;
            float f = Vector3.SqrMagnitude(position1 - position2);
            if (f > maxDistance * (double) maxDistance)
              num5 = 0.0f;
            else if (f > minDistance * (double) minDistance)
            {
              float num6 = Mathf.Sqrt(f);
              switch (audioCue.SourceCue.Falloff)
              {
                case SECTR_AudioCue.FalloffTypes.Linear:
                  num5 = 1f - Mathf.Clamp01((float) ((num6 - (double) minDistance) / (maxDistance - (double) minDistance)));
                  break;
                case SECTR_AudioCue.FalloffTypes.Logrithmic:
                  num5 = Mathf.Clamp01(1f / Mathf.Max((float) (num6 - (double) minDistance - 1.0), 1f / 1000f));
                  break;
              }
            }
          }
          float baseVolumeLoudness = this.baseVolumeLoudness;
          if (hdrCurve != null)
          {
            float num7 = hdrCurve.Evaluate(source.time);
            baseVolumeLoudness += num7;
          }
          float num8 = baseVolumeLoudness + 20f * Mathf.Log10(Mathf.Max(userVolume * num1 * num5, 1f / 1000f));
          if (num8 < (double) windowHDRMin && (volumeOnly || (this.baseVolumeLoudness - (double) windowHDRMin) / system.HDRDecay > source.time - (double) source.clip.length))
          {
            _Stop(false);
            return;
          }
          currentLoudness += Mathf.Pow(10f, num8 * 0.1f);
          num4 = Mathf.Clamp01(Mathf.Pow(10f, (float) ((num8 - (double) windowHDRMax) * 0.05000000074505806)));
        }
        else
          num4 = this.baseVolumeLoudness * num1 * userVolume;
        if (Occludable)
        {
          float num9 = 1f;
          occlusionAlpha += deltaTime * (Occluded ? num9 : -num9);
          occlusionAlpha = Mathf.Clamp01(occlusionAlpha);
          float t = occlusionAlpha * audioCue.SourceCue.OcclusionScale;
          num4 *= Mathf.Lerp(1f, system.OcclusionVolume, t);
          if ((bool) (UnityEngine.Object) lowpass)
          {
            lowpass.enabled = occlusionAlpha > 0.0;
            if (lowpass.enabled)
            {
              lowpass.cutoffFrequency = Mathf.Lerp(22000f, system.OcclusionCutoff, t);
              lowpass.lowpassResonanceQ = Mathf.Lerp(1f, system.OcclusionResonanceQ, t);
            }
          }
        }
        source.volume = Mathf.Clamp01(num4 * num2);
        source.pitch = Mathf.Clamp(userPitch * basePitch * num3, 0.0f, 2f);
      }
      if (volumeOnly)
        return;
      if ((bool) (UnityEngine.Object) source && (source.isPlaying || Paused) && !Local && system.BlendNearbySounds)
      {
        float f = Vector3.SqrMagnitude(Listener.position - position1);
        source.spatialBlend = f > system.NearBlendRange.x * (double) system.NearBlendRange.x ? (f > system.NearBlendRange.y * (double) system.NearBlendRange.y ? 1f : Mathf.Clamp01((float) ((Mathf.Sqrt(f) - (double) system.NearBlendRange.x) / (system.NearBlendRange.y - (double) system.NearBlendRange.x)))) : 0.0f;
      }
      if (!Loops || Paused)
        return;
      bool isPlaying = source != null && source.isPlaying;
      bool flag1 = !isPlaying && (!HDR || this.baseVolumeLoudness >= (double) windowHDRMin);
      if (Local)
      {
        if (!(!isPlaying & flag1) || !_CheckInstances(audioCue, isPlaying))
          return;
        Play();
      }
      else
      {
        if (currentTime < (double) nextTestTime)
          return;
        bool flag2 = _CheckProximity(audioCue, parent, localPosition, this);
        if (((!flag2 ? 0 : (!isPlaying ? 1 : 0)) & (flag1 ? 1 : 0)) != 0 && _CheckInstances(audioCue, isPlaying))
          Play();
        else if (!flag2 & isPlaying)
          _Stop(true);
        else if (Occludable && !ForcedInfinite)
          _SetFlag(Flags.Occluded, IsOccluded(position1, system.OcclusionFlags));
        _ScheduleNextTest();
      }
    }

    private void _SetFlag(Flags flag, bool on)
    {
      if (on)
        flags |= flag;
      else
        flags &= ~flag;
    }

    private bool _AcquireSource()
    {
      if (!(bool) (UnityEngine.Object) source)
      {
        SECTR_AudioCue sourceCue = audioCue.SourceCue;
        bool flag = Occludable && !sourceCue.BypassEffects && SECTR_Modules.HasPro() && lowpassSourcePool.Count > 0;
        source = flag ? lowpassSourcePool.Pop() : simpleSourcePool.Pop();
        if ((bool) (UnityEngine.Object) source)
        {
          if (flag)
          {
            lowpass = source.GetComponent<AudioLowPassFilter>();
            lowpass.enabled = false;
          }
          source.time = 0.0f;
          source.timeSamples = 0;
          source.priority = sourceCue.Priority;
          source.bypassEffects = sourceCue.BypassEffects;
          source.loop = sourceCue.Loops;
          source.spread = sourceCue.Spread;
          source.mute = Muted;
          basePitch = UnityEngine.Random.Range(sourceCue.Pitch.x, sourceCue.Pitch.y);
          if (sourceCue.MaxInstances > 0)
          {
            if (!maxInstancesTable.ContainsKey(audioCue))
              maxInstancesTable[audioCue] = new List<Instance>();
            maxInstancesTable[audioCue].Add(this);
          }
          source.panStereo = 0.0f;
          source.spatialBlend = 1f;
          source.bypassReverbZones = Local;
          if (Local)
          {
            if (ThreeD)
            {
              source.rolloffMode = AudioRolloffMode.Linear;
              source.maxDistance = 1000000f;
              source.minDistance = source.maxDistance - 1f / 1000f;
            }
            else
            {
              source.panStereo = sourceCue.Pan2D;
              source.spatialBlend = 0.0f;
            }
            source.dopplerLevel = 0.0f;
            if (currentAmbience != null && currentAmbience.BackgroundLoop == audioCue || currentMusic != null && currentMusic == audioCue)
              source.priority = 0;
          }
          else
          {
            if (HDR)
            {
              source.rolloffMode = AudioRolloffMode.Linear;
              source.minDistance = 1000000f;
              source.maxDistance = source.minDistance + 1f / 1000f;
            }
            else
            {
              switch (sourceCue.Falloff)
              {
                case SECTR_AudioCue.FalloffTypes.Logrithmic:
                  source.rolloffMode = AudioRolloffMode.Logarithmic;
                  break;
                default:
                  source.rolloffMode = AudioRolloffMode.Linear;
                  break;
              }
              source.minDistance = sourceCue.MinDistance;
              source.maxDistance = Mathf.Max(sourceCue.MaxDistance, sourceCue.MinDistance + 1f / 1000f);
            }
            source.dopplerLevel = sourceCue.DopplerLevel;
            source.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;
          }
          source.transform.position = Position;
          source.gameObject.SetActive(true);
        }
      }
      return source != null;
    }

    private void _ReleaseSource()
    {
      if (!(source != null))
        return;
      if (audioCue.MaxInstances > 0 && maxInstancesTable.ContainsKey(audioCue) && maxInstancesTable[audioCue].Remove(this) && maxInstancesTable[audioCue].Count == 0)
        maxInstancesTable.Remove(audioCue);
      source.Stop();
      source.gameObject.SetActive(false);
      if ((bool) (UnityEngine.Object) lowpass)
      {
        lowpass.enabled = false;
        lowpassSourcePool.Push(source);
      }
      else
        simpleSourcePool.Push(source);
      source = null;
      lowpass = null;
      hdrCurve = null;
    }

    private void _AddProximityInstance(SECTR_AudioCue srcCue)
    {
      int proximityLimit = srcCue.ProximityLimit;
      if (proximityLimit <= 0)
        return;
      List<Instance> instanceList;
      if (!proximityTable.TryGetValue(audioCue, out instanceList))
      {
        instanceList = new List<Instance>(proximityLimit * 2);
        proximityTable[audioCue] = instanceList;
      }
      instanceList.Add(this);
    }

    private void _ScheduleNextTest() => nextTestTime = currentTime + UnityEngine.Random.Range(system.RetestInterval.x, system.RetestInterval.y);

    private void _Stop(bool stopImmediately)
    {
      if (!stopImmediately && (bool) (UnityEngine.Object) source && source.isPlaying && (bool) (UnityEngine.Object) audioCue && audioCue.SourceCue.FadeOutTime > 0.0)
      {
        if (FadingIn)
        {
          float num = 1f - Mathf.Clamp01((currentTime - fadeStarTime) / audioCue.SourceCue.FadeInTime);
          fadeStarTime = currentTime - num * audioCue.SourceCue.FadeOutTime;
        }
        else
          fadeStarTime = currentTime;
        _SetFlag(Flags.FadingOut, true);
        _SetFlag(Flags.FadingIn, false);
      }
      else
        _ReleaseSource();
    }

    [Flags]
    private enum Flags
    {
      Loops = 1,
      FadingIn = 2,
      FadingOut = 4,
      Muted = 8,
      Local = 16, // 0x00000010
      ThreeD = 32, // 0x00000020
      Paused = 64, // 0x00000040
      HDR = 128, // 0x00000080
      Occludable = 256, // 0x00000100
      Occluded = 512, // 0x00000200
      ForcedInfinite = 1024, // 0x00000400
      Delayed = 2048, // 0x00000800
    }
  }

  [Flags]
  public enum OcclusionModes
  {
    Graph = 1,
    Raycast = 2,
    Distance = 4,
  }
}
