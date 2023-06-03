// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.SlimeModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class SlimeModel : ActorModel
  {
    public SlimeEmotions.EmotionState emotionAgitation;
    public SlimeEmotions.EmotionState emotionFear;
    public SlimeEmotions.EmotionState emotionHunger;
    public SlimeEmotions.EmotionState[] allEmotions;
    public double? disabledAtTime;
    public bool isFeral;
    public bool isGlitch;
    public List<Identifiable.Id> fashions = new List<Identifiable.Id>();

    public SlimeModel(
      long actorId,
      Identifiable.Id ident,
      RegionRegistry.RegionSetId regionSetId,
      Transform transform)
      : base(actorId, ident, regionSetId, transform)
    {
    }

    public void MaybeSetInitEmotions(
      SlimeEmotions.EmotionState initAgitation,
      SlimeEmotions.EmotionState initFear,
      SlimeEmotions.EmotionState initHunger)
    {
      if (allEmotions != null)
        return;
      emotionAgitation = initAgitation;
      emotionFear = initFear;
      emotionHunger = initHunger;
      allEmotions = new SlimeEmotions.EmotionState[3]
      {
        emotionAgitation,
        emotionFear,
        emotionHunger
      };
    }

    public void Push(ActorDataV09 persistence)
    {
      emotionAgitation.currVal = persistence.emotions.emotionData[SlimeEmotions.Emotion.AGITATION];
      emotionFear.currVal = persistence.emotions.emotionData[SlimeEmotions.Emotion.FEAR];
      emotionHunger.currVal = persistence.emotions.emotionData[SlimeEmotions.Emotion.HUNGER];
      disabledAtTime = persistence.disabledAtTime;
      isFeral = persistence.isFeral;
      isGlitch = persistence.isGlitch;
      fashions = persistence.fashions;
    }

    public void Pull(ref ActorDataV09 persistence)
    {
      persistence.disabledAtTime = disabledAtTime;
      persistence.isFeral = isFeral;
      persistence.isGlitch = isGlitch;
      persistence.fashions = new List<Identifiable.Id>(fashions);
      persistence.emotions.emotionData = new Dictionary<SlimeEmotions.Emotion, float>()
      {
        {
          SlimeEmotions.Emotion.AGITATION,
          emotionAgitation.currVal
        },
        {
          SlimeEmotions.Emotion.FEAR,
          emotionFear.currVal
        },
        {
          SlimeEmotions.Emotion.HUNGER,
          emotionHunger.currVal
        }
      };
    }
  }
}
