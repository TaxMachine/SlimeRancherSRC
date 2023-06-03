// Decompiled with JetBrains decompiler
// Type: SlimeEmotionData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

[Serializable]
public class SlimeEmotionData : Dictionary<SlimeEmotions.Emotion, float>
{
  public SlimeEmotionData()
  {
  }

  public SlimeEmotionData(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public SlimeEmotionData(SlimeEmotions emotions)
  {
    foreach (SlimeEmotions.EmotionState emotionState in emotions.GetAll())
      this[emotionState.emotion] = emotionState.currVal;
  }

  public void AverageIn(SlimeEmotions emotions, float weight)
  {
    float num = 1f - weight;
    foreach (SlimeEmotions.EmotionState emotionState in emotions.GetAll())
      this[emotionState.emotion] = (float) (this[emotionState.emotion] * (double) num + emotionState.currVal * (double) weight);
  }

  public override bool Equals(object o) => o is SlimeEmotionData && this.SequenceEqual((IEnumerable<KeyValuePair<SlimeEmotions.Emotion, float>>) o, new EmotionComparer());

  public override int GetHashCode()
  {
    int hashCode = 0;
    foreach (KeyValuePair<SlimeEmotions.Emotion, float> keyValuePair in this)
      hashCode ^= keyValuePair.Key.GetHashCode() ^ keyValuePair.Value.GetHashCode();
    return hashCode;
  }

  public override string ToString()
  {
    string str = "";
    foreach (KeyValuePair<SlimeEmotions.Emotion, float> keyValuePair in this)
      str = str + keyValuePair.Key + ":" + keyValuePair.Value + ",";
    return str;
  }

  private class EmotionComparer : IEqualityComparer<KeyValuePair<SlimeEmotions.Emotion, float>>
  {
    public bool Equals(
      KeyValuePair<SlimeEmotions.Emotion, float> x,
      KeyValuePair<SlimeEmotions.Emotion, float> y)
    {
      return x.Key == y.Key && Math.Abs(x.Value - y.Value) < 1.0 / 1000.0;
    }

    public int GetHashCode(KeyValuePair<SlimeEmotions.Emotion, float> obj) => throw new NotImplementedException();
  }
}
