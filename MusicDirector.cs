// Decompiled with JetBrains decompiler
// Type: MusicDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Script.Util.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MusicDirector : SRBehaviour
{
  public SECTR_AudioCue defaultMusic;
  public SECTR_AudioCue menuMusic;
  public SECTR_AudioCue tarrMusic;
  public SECTR_AudioCue slimeHoopMusic;
  public SECTR_AudioCue firestormPrepMusic;
  public SECTR_AudioCue firestormMusic;
  public SECTR_AudioCue valleyRaceMusic;
  public SECTR_AudioCue rushModeWarning1Music;
  public SECTR_AudioCue rushModeWarning2Music;
  public SECTR_AudioCue eventGordoMusic;
  public Music.Credits creditsMusic;
  public Music.Credits rushModeCreditsMusic;
  public Music.Zone.Default ranchMusic;
  public Music.Zone_Wistful ranchWistfulMusic;
  public Music.Zone_Wiggly wigglyMusic;
  public Music.Zone.Default reefMusic;
  public Music.Zone.Default quarryMusic;
  public Music.Zone.Default mossMusic;
  public Music.Zone.Default desertMusic;
  public Music.Zone.Default seaMusic;
  public Music.Zone.Default ruinsMusic;
  public Music.Zone.Default ruinsTransMusic;
  public Music.Zone.Default wildsMusic;
  public Music.Zone.Default ogdenRanchMusic;
  public Music.Zone.Default mochiRanchMusic;
  public Music.Zone.Default valleyMusic;
  public Music.Zone.Special oasisMusic;
  public Music.House houseMusic;
  private const Priority PRIORITY_ZONE = Priority.ZONE_DEFAULT;
  private const Priority PRIORITY_EVENT = Priority.VALLEY_RACE;
  private const Priority PRIORITY_HIGH = Priority.HOUSE;
  private const int HOUR_MORNING = 6;
  private const int HOUR_AFTERNOON = 12;
  private const int HOUR_NIGHT = 18;
  private PriorityQueue queue = new PriorityQueue();
  private CurrentMetadata current = new CurrentMetadata();
  private HashSet<UnityEngine.Object> suppressors = new HashSet<UnityEngine.Object>();
  private TimeDirector timeDirector;
  private bool timeDirectorSet;

  private bool SetupTimeDirector()
  {
    if (!timeDirectorSet && SRSingleton<SceneContext>.Instance != null)
    {
      timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
      timeDirectorSet = true;
    }
    return timeDirectorSet;
  }

  public void Awake() => SceneManager.sceneLoaded += OnSceneLoaded;

  public void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;

  public void Update()
  {
    if (SetupTimeDirector() && timeDirector.IsFastForwarding())
      return;
    if (suppressors.Count > 0)
    {
      current.instance.Stop(false);
    }
    else
    {
      Music music = queue.Peek();
      SECTR_AudioCue currentCue = music.GetCurrentCue();
      if (current.cue != currentCue)
      {
        CurrentMetadata currentMetadata = new CurrentMetadata()
        {
          music = music,
          cue = currentCue,
          priority = music.Priority,
          startTime = Time.unscaledTime,
          offset = 0.0f
        };
        if (current.instance.Active)
        {
          if (currentMetadata.music.GetWaitForFadeOut(current.music))
            currentMetadata.startTime += current.cue.FadeOutTime;
          if (currentMetadata.music.GetStartMidway(current.music))
            currentMetadata.offset += currentMetadata.cue.MinClipLength() * Randoms.SHARED.GetInRange(0.1f, 0.3f);
        }
        current.instance.Stop(false);
        current = currentMetadata;
      }
      if (current.instance.Active || Time.unscaledTime < (double) current.startTime)
        return;
      current.cue.Loops = current.music.Loops;
      current.instance = SECTR_AudioSystem.Play(current.cue, Vector3.zero, false);
      current.instance.TimeSeconds = current.offset;
      if (Mathf.Approximately(current.offset, 0.0f))
        current.instance.SkipFadeIn();
      if (!current.music.Loops)
        current.startTime = float.MaxValue;
      current.offset = 0.0f;
    }
  }

  public void RegisterSuppressor(UnityEngine.Object obj) => suppressors.Add(obj);

  public void DeregisterSuppressor(UnityEngine.Object obj) => suppressors.Remove(obj);

  public void ForceStopCurrent() => current.instance.Stop(true);

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    SetupTimeDirector();
    queue = new PriorityQueue();
    Enqueue(GetSceneCue(scene), Priority.DEFAULT);
  }

  private SECTR_AudioCue GetSceneCue(Scene scene) => scene.name == "MainMenu" ? menuMusic : defaultMusic;

  public void OnRegionsChanged(RegionMember member)
  {
    Music regionMusic = GetRegionMusic(member);
    if (regionMusic == null)
      return;
    Enqueue(regionMusic, true);
  }

  private Music GetRegionMusic(RegionMember member)
  {
    for (int index = member.regions.Count - 1; index >= 0; --index)
    {
      RegionBackgroundMusic component = member.regions[index].gameObject.GetComponent<RegionBackgroundMusic>();
      if (component != null && component.backgroundMusic != null)
        return component.backgroundMusic;
    }
    HashSet<ZoneDirector.Zone> source = ZoneDirector.Zones(member);
    ZoneDirector.Zone zone = source.Any() ? source.Max() : ZoneDirector.Zone.NONE;
    if (ranchWistfulMusic.Enabled())
    {
      if (zone == ZoneDirector.Zone.RANCH)
        return ranchWistfulMusic;
      ranchWistfulMusic.Stop();
    }
    switch (zone)
    {
      case ZoneDirector.Zone.RANCH:
        return ranchMusic;
      case ZoneDirector.Zone.REEF:
        return reefMusic;
      case ZoneDirector.Zone.QUARRY:
        return quarryMusic;
      case ZoneDirector.Zone.MOSS:
        return mossMusic;
      case ZoneDirector.Zone.DESERT:
        return desertMusic;
      case ZoneDirector.Zone.SEA:
        return seaMusic;
      case ZoneDirector.Zone.RUINS:
        return ruinsMusic;
      case ZoneDirector.Zone.RUINS_TRANSITION:
        return ruinsTransMusic;
      case ZoneDirector.Zone.WILDS:
        return wildsMusic;
      case ZoneDirector.Zone.OGDEN_RANCH:
        return ogdenRanchMusic;
      case ZoneDirector.Zone.VALLEY:
        return valleyMusic;
      case ZoneDirector.Zone.MOCHI_RANCH:
        return mochiRanchMusic;
      case ZoneDirector.Zone.SLIMULATIONS:
        return SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.musicSlimulation;
      case ZoneDirector.Zone.VIKTOR_LAB:
        return SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.musicViktorLab;
      default:
        return null;
    }
  }

  public void SetTarrMode(bool enabled) => Enqueue(tarrMusic, Priority.TARR, enabled);

  public void SetOasisMode(bool enabled) => Enqueue(oasisMusic, enabled);

  public void SetEventGordoMode(bool enabled) => Enqueue(eventGordoMusic, Priority.PARTY_GORDO, enabled);

  public void SetValleyRaceMode(bool enabled) => Enqueue(valleyRaceMusic, Priority.VALLEY_RACE, enabled);

  public void SetCreditsMode(bool enabled) => Enqueue(creditsMusic, enabled);

  public void EnableSlimeHoopMode(double endTime)
  {
    SRSingleton<SceneContext>.Instance.TimeDirector.AddPassedTimeDelegate(endTime, () => Dequeue(Priority.SLIME_HOOP));
    Enqueue(slimeHoopMusic, Priority.SLIME_HOOP, true);
  }

  public void SetHouseMode(bool enabled) => Enqueue(houseMusic, enabled);

  public void SetWistfulRanchMode()
  {
    ranchWistfulMusic.ResetEndTime();
    Enqueue(ranchWistfulMusic, true);
  }

  public void SetWigglyMode() => Enqueue(wigglyMusic, true);

  public void SetRushWarningMode(SECTR_AudioCue music)
  {
    if (music != null)
      Enqueue(music, Priority.RUSH_MODE_WARNING);
    else
      Dequeue(Priority.RUSH_MODE_WARNING);
  }

  public void SetRushCreditsMode(bool enabled) => Enqueue(rushModeCreditsMusic, enabled);

  public void SetFirestormMode(FirestormActivator.Mode mode)
  {
    if (mode == FirestormActivator.Mode.ACTIVE)
      Enqueue(firestormMusic, Priority.FIRESTORM);
    else if (mode == FirestormActivator.Mode.PREPARING)
      Enqueue(firestormPrepMusic, Priority.FIRESTORM);
    else
      Dequeue(Priority.FIRESTORM);
  }

  private void Enqueue(SECTR_AudioCue cue, Priority priority) => Enqueue(cue, priority, true);

  private void Enqueue(SECTR_AudioCue cue, Priority priority, bool enabled) => Enqueue(new Music.Basic()
  {
    cue = cue,
    priority = priority
  }, enabled);

  private void Enqueue(Music music, bool enabled)
  {
    if (enabled)
      queue.Enqueue(music);
    else
      queue.Dequeue(music.Priority);
  }

  private void Dequeue(Priority priority) => queue.Dequeue(priority);

  public abstract class Music
  {
    public abstract Priority Priority { get; }

    public abstract bool Loops { get; }

    public abstract SECTR_AudioCue GetCurrentCue();

    public abstract bool GetWaitForFadeOut(Music previous);

    public abstract bool GetStartMidway(Music previous);

    public class Basic : Music
    {
      public SECTR_AudioCue cue;
      public Priority priority;

      public override Priority Priority => priority;

      public override bool Loops => true;

      public override SECTR_AudioCue GetCurrentCue() => cue;

      public override bool GetWaitForFadeOut(Music previous) => false;

      public override bool GetStartMidway(Music previous) => false;
    }

    public abstract class Zone : Music
    {
      [Tooltip("Background music played during the day.")]
      public SECTR_AudioCue background;
      [Tooltip("Background music played during the night.")]
      public SECTR_AudioCue nightBackground;

      public override bool Loops => true;

      public override SECTR_AudioCue GetCurrentCue()
      {
        int num = Mathf.FloorToInt(SRSingleton<SceneContext>.Instance.TimeDirector.CurrHour());
        return num >= 6 && num < 18 ? background : nightBackground;
      }

      public override bool GetWaitForFadeOut(Music previous) => true;

      public override bool GetStartMidway(Music previous) => previous.Priority >= Priority.VALLEY_RACE && previous.Priority < Priority.HOUSE;

      [Serializable]
      public class Default : Zone
      {
        public override Priority Priority => Priority.ZONE_DEFAULT;
      }

      [Serializable]
      public class Special : Zone
      {
        public override Priority Priority => Priority.ZONE_SPECIAL;
      }
    }

    [Serializable]
    public class Zone_Wistful : Music
    {
      [Tooltip("Background music to play during the wistful ranch.")]
      public SECTR_AudioCue cue;
      private double? endTime;

      public override Priority Priority => Priority.ZONE_DEFAULT;

      public override bool Loops => true;

      public override bool GetWaitForFadeOut(Music previous) => true;

      public override bool GetStartMidway(Music previous) => false;

      public override SECTR_AudioCue GetCurrentCue()
      {
        double? endTime = this.endTime;
        double num = SRSingleton<SceneContext>.Instance.TimeDirector.WorldTime();
        if (endTime.GetValueOrDefault() < num & endTime.HasValue)
          SRSingleton<GameContext>.Instance.MusicDirector.OnRegionsChanged(SRSingleton<SceneContext>.Instance.Player.GetRequiredComponent<RegionMember>());
        return cue;
      }

      public void ResetEndTime() => endTime = new double?(SRSingleton<SceneContext>.Instance.TimeDirector.GetNextDawnAfterNextDusk());

      public bool Enabled()
      {
        TimeDirector timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
        double? endTime = this.endTime;
        double num = timeDirector.WorldTime();
        return endTime.GetValueOrDefault() >= num & endTime.HasValue;
      }

      public void Stop() => endTime = new double?();
    }

    [Serializable]
    public class Zone_Wiggly : Music
    {
      [Tooltip("Background wiggly music played after the chime changer button is pressed).")]
      public SECTR_AudioCue cue;

      public override Priority Priority => Priority.ZONE_DEFAULT;

      public override bool Loops => false;

      public override bool GetWaitForFadeOut(Music previous) => false;

      public override bool GetStartMidway(Music previous) => false;

      public override SECTR_AudioCue GetCurrentCue()
      {
        MusicDirector musicDirector = SRSingleton<GameContext>.Instance.MusicDirector;
        if (musicDirector.current.cue == cue && !musicDirector.current.instance.Active)
        {
          RegionMember requiredComponent = SRSingleton<SceneContext>.Instance.Player.GetRequiredComponent<RegionMember>();
          musicDirector.OnRegionsChanged(requiredComponent);
        }
        return cue;
      }
    }

    [Serializable]
    public class House : Music
    {
      [Tooltip("Background music played during the morning.")]
      public SECTR_AudioCue morning;
      [Tooltip("Background music played during the afternoon.")]
      public SECTR_AudioCue afternoon;
      [Tooltip("Background music played during the night.")]
      public SECTR_AudioCue night;

      public override bool Loops => true;

      public override SECTR_AudioCue GetCurrentCue()
      {
        int num = Mathf.FloorToInt(SRSingleton<SceneContext>.Instance.TimeDirector.CurrHour());
        if (num >= 6 && num < 12)
          return morning;
        return num >= 12 && num < 18 ? afternoon : night;
      }

      public override Priority Priority => Priority.HOUSE;

      public override bool GetWaitForFadeOut(Music previous) => false;

      public override bool GetStartMidway(Music previous) => false;
    }

    [Serializable]
    public class Credits : Music
    {
      [Tooltip("Background music to play once during credits.")]
      public SECTR_AudioCue cue;

      public override Priority Priority => Priority.CREDITS;

      public override bool Loops => false;

      public override SECTR_AudioCue GetCurrentCue() => cue;

      public override bool GetWaitForFadeOut(Music previous) => false;

      public override bool GetStartMidway(Music previous) => false;
    }
  }

  public enum Priority
  {
    DEFAULT = 0,
    ZONE_DEFAULT = 10, // 0x0000000A
    ZONE_SPECIAL = 11, // 0x0000000B
    VALLEY_RACE = 100, // 0x00000064
    TARR = 101, // 0x00000065
    RUSH_MODE_WARNING = 102, // 0x00000066
    FIRESTORM = 103, // 0x00000067
    SLIME_HOOP = 104, // 0x00000068
    PARTY_GORDO = 105, // 0x00000069
    HOUSE = 900, // 0x00000384
    CREDITS = 1000, // 0x000003E8
  }

  private class CurrentMetadata
  {
    public Music music;
    public SECTR_AudioCue cue;
    public SECTR_AudioCueInstance instance;
    public Priority priority;
    public float startTime;
    public float offset;
  }

  private class PriorityQueue
  {
    private List<Music> queue = new List<Music>();

    public void Enqueue(Music music)
    {
      for (int index = 0; index < queue.Count; ++index)
      {
        if (queue[index].Priority == music.Priority)
        {
          queue[index] = music;
          return;
        }
        if (music.Priority > queue[index].Priority)
        {
          queue.Insert(index, music);
          return;
        }
      }
      queue.Add(music);
    }

    public void Dequeue(Priority priority)
    {
      for (int index = 0; index < queue.Count; ++index)
      {
        if (queue[index].Priority == priority)
        {
          queue.RemoveAt(index);
          break;
        }
      }
    }

    public Music Peek() => queue.Count != 0 ? queue[0] : throw new InvalidOperationException();
  }
}
