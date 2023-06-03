// Decompiled with JetBrains decompiler
// Type: SlimeEmotions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using UnityEngine;

public class SlimeEmotions : RegisteredActorBehaviour, RegistryUpdateable, ActorModel.Participant
{
  public const float HUNGRY_CUTOFF = 0.666f;
  public const float STARVING_CUTOFF = 0.99f;
  public const float ANGRY_CUTOFF = 0.9f;
  public const float TERRIFIED_CUTOFF = 0.99f;
  public EmotionState initHunger = new EmotionState(Emotion.HUNGER, 0.5f, 1f, 1f, 0.5f);
  public EmotionState initAgitation = new EmotionState(Emotion.AGITATION, 0.0f, 0.0f, 1f, 0.333f);
  public EmotionState initFear = new EmotionState(Emotion.FEAR, 0.0f, 0.0f, 1f, 5f);
  private double lastUpdateTime;
  private TimeDirector timeDir;
  private RegionMember member;
  private List<MusicBoxRegion> musicBoxes = new List<MusicBoxRegion>();
  private float modHungerFactor = 1f;
  private const float STARVING_AGITATION_PER_HOUR = 0.416667f;
  private float FAVORITE_TOY_FACTOR = 0.5f;
  private float NON_FAVORITE_TOY_FACTOR = 0.25f;
  private float POLLEN_SOURCE_AGITATION_PER_HOUR = 0.416667f;
  private int nearbyFavoriteToyCount;
  private int nearbyToyCount;
  private int pollenSourceCount;
  private SlimeModel model;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    member = GetComponent<RegionMember>();
    lastUpdateTime = timeDir.HoursFromNowOrStart(0.0f);
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.ModDirector.RegisterModsListener(OnModsChanged);
  }

  public override void Start()
  {
    base.Start();
    if (Identifiable.IsTarr(Identifiable.GetId(gameObject)) || !member.IsInRegion(RegionRegistry.RegionSetId.SLIMULATIONS))
      return;
    SetEmotionEnabled(Emotion.HUNGER, false);
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    musicBoxes.Clear();
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.ModDirector.UnregisterModsListener(OnModsChanged);
  }

  public void InitModel(ActorModel model) => ((SlimeModel) model).MaybeSetInitEmotions(initAgitation, initFear, initHunger);

  public void SetModel(ActorModel model) => this.model = (SlimeModel) model;

  private void OnModsChanged() => modHungerFactor = SRSingleton<SceneContext>.Instance.ModDirector.SlimeHungerFactor();

  public void RegistryUpdate() => UpdateToTime(timeDir.WorldTime());

  public void UpdateToTime(double worldTime)
  {
    float num1 = (float) ((worldTime - lastUpdateTime) * 0.00027777778450399637);
    if (num1 <= 0.0)
      return;
    foreach (EmotionState allEmotion in model.allEmotions)
    {
      if (allEmotion.enabled)
      {
        float num2 = allEmotion.recoveryPerGameHour + GetBaseRecoveryAdjustment(allEmotion.emotion);
        float recoveryFactor = GetRecoveryFactor(allEmotion.emotion);
        float num3 = num2 >= 0.0 ? num2 * recoveryFactor : num2 / recoveryFactor;
        if (allEmotion.currVal > (double) allEmotion.defVal)
          allEmotion.currVal = Mathf.Max(allEmotion.defVal, allEmotion.currVal - num3 * num1);
        else if (allEmotion.currVal < (double) allEmotion.defVal)
          allEmotion.currVal = Mathf.Min(allEmotion.defVal, allEmotion.currVal + num3 * num1);
        else if (allEmotion.defVal == 0.0 && num3 < 0.0)
          allEmotion.currVal = Mathf.Max(allEmotion.defVal, allEmotion.currVal - num3 * num1);
        else if (allEmotion.defVal == 1.0 && num3 < 0.0)
          allEmotion.currVal = Mathf.Min(allEmotion.defVal, allEmotion.currVal + num3 * num1);
        allEmotion.currVal = Mathf.Clamp01(allEmotion.currVal);
      }
    }
    lastUpdateTime = worldTime;
  }

  private float GetBaseRecoveryAdjustment(Emotion emotion)
  {
    if (emotion != Emotion.AGITATION)
      return 0.0f;
    float recoveryAdjustment = 0.0f;
    if (GetCurr(Emotion.HUNGER) >= 0.99000000953674316 || GetCurr(Emotion.FEAR) >= 0.99000000953674316)
      recoveryAdjustment -= 0.416667f;
    if (pollenSourceCount > 0)
      recoveryAdjustment -= POLLEN_SOURCE_AGITATION_PER_HOUR;
    return recoveryAdjustment;
  }

  private float GetRecoveryFactor(Emotion emotion)
  {
    switch (emotion)
    {
      case Emotion.HUNGER:
        return modHungerFactor;
      case Emotion.AGITATION:
        float recoveryFactor = 1f;
        if (musicBoxes.Count > 0)
          ++recoveryFactor;
        if (nearbyFavoriteToyCount > 0)
          recoveryFactor += FAVORITE_TOY_FACTOR;
        else if (nearbyToyCount > 0)
          recoveryFactor += NON_FAVORITE_TOY_FACTOR;
        return recoveryFactor;
      default:
        return 1f;
    }
  }

  private EmotionState GetEmotion(Emotion emotion)
  {
    switch (emotion)
    {
      case Emotion.HUNGER:
        return model.emotionHunger;
      case Emotion.AGITATION:
        return model.emotionAgitation;
      case Emotion.FEAR:
        return model.emotionFear;
      default:
        return null;
    }
  }

  public bool Adjust(Emotion emotion, float adjust)
  {
    EmotionState emotion1 = GetEmotion(emotion);
    if (emotion1 == null || !emotion1.enabled)
      return false;
    emotion1.currVal = Mathf.Clamp(emotion1.currVal + adjust, 0.0f, 1f);
    return true;
  }

  public void SetAll(SlimeEmotions other)
  {
    foreach (Emotion emotion1 in Enum.GetValues(typeof (Emotion)))
    {
      EmotionState emotion2 = GetEmotion(emotion1);
      if (emotion2 != null && emotion2.enabled)
        emotion2.currVal = other.GetEmotion(emotion1).currVal;
    }
  }

  public void SetAll(Dictionary<Emotion, float> other)
  {
    foreach (KeyValuePair<Emotion, float> keyValuePair in other)
    {
      EmotionState emotion = GetEmotion(keyValuePair.Key);
      if (emotion != null && emotion.enabled)
        emotion.currVal = keyValuePair.Value;
    }
  }

  public IEnumerable<EmotionState> GetAll() => model.allEmotions;

  public float GetCurr(Emotion emotion) => emotion != Emotion.NONE ? GetEmotion(emotion).currVal : 1f;

  public float GetMax()
  {
    float a = 0.0f;
    foreach (EmotionState allEmotion in model.allEmotions)
      a = Mathf.Max(a, allEmotion.currVal);
    return a;
  }

  public void AddMusicBox(MusicBoxRegion box) => musicBoxes.Add(box);

  public void RemoveMusicBox(MusicBoxRegion box) => musicBoxes.Remove(box);

  public void AddNearbyToy(bool isFavorite)
  {
    if (isFavorite)
      ++nearbyFavoriteToyCount;
    else
      ++nearbyToyCount;
  }

  public void RemoveNearbyToy(bool isFavorite)
  {
    if (isFavorite)
      nearbyFavoriteToyCount = Math.Max(0, nearbyFavoriteToyCount - 1);
    else
      nearbyToyCount = Math.Max(0, nearbyToyCount - 1);
  }

  public void AddPollenSource() => ++pollenSourceCount;

  public void RemovePollenSource() => pollenSourceCount = Math.Max(0, pollenSourceCount - 1);

  public void SetEmotionEnabled(Emotion emotion, bool enabled) => GetEmotion(emotion)?.SetEnabled(enabled);

  public enum Emotion
  {
    HUNGER,
    AGITATION,
    FEAR,
    NONE,
  }

  [Serializable]
  public class EmotionState : ISerializable
  {
    public Emotion emotion;
    public float currVal;
    public float defVal;
    public float sensitivity;
    public float recoveryPerGameHour;

    public bool enabled { get; private set; }

    public EmotionState(
      Emotion emotion,
      float currVal,
      float defVal,
      float sensitivity,
      float recoveryPerGameHour)
    {
      this.emotion = emotion;
      this.currVal = currVal;
      this.defVal = defVal;
      this.sensitivity = sensitivity;
      this.recoveryPerGameHour = recoveryPerGameHour;
      enabled = true;
    }

    public void SetEnabled(bool enabled)
    {
      if (this.enabled == enabled)
        return;
      this.enabled = enabled;
      currVal = this.enabled ? defVal : 0.0f;
    }

    protected EmotionState(SerializationInfo info, StreamingContext context)
    {
      emotion = (Emotion) info.GetInt32(nameof (emotion));
      currVal = info.GetSingle(nameof (currVal));
      defVal = info.GetSingle(nameof (defVal));
      sensitivity = info.GetSingle(nameof (sensitivity));
      recoveryPerGameHour = info.GetSingle(nameof (recoveryPerGameHour));
    }

    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("currVal", currVal);
      info.AddValue("defVal", defVal);
      info.AddValue("sensitivity", sensitivity);
      info.AddValue("recoveryPerGameHour", recoveryPerGameHour);
      info.AddValue("emotion", (int) emotion);
    }
  }
}
