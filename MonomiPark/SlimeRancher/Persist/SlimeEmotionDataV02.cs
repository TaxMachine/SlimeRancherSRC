// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.SlimeEmotionDataV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class SlimeEmotionDataV02 : PersistedDataSet
  {
    public Dictionary<SlimeEmotions.Emotion, float> emotionData = new Dictionary<SlimeEmotions.Emotion, float>();

    public override string Identifier => "SRSED";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      emotionData = new Dictionary<SlimeEmotions.Emotion, float>();
      int num = reader.ReadInt32();
      for (int index = 0; index < num; ++index)
        emotionData.Add((SlimeEmotions.Emotion) reader.ReadInt32(), reader.ReadSingle());
    }

    protected override void WriteData(BinaryWriter writer)
    {
      if (emotionData == null)
      {
        writer.Write(0);
      }
      else
      {
        writer.Write(emotionData.Count);
        foreach (KeyValuePair<SlimeEmotions.Emotion, float> keyValuePair in emotionData)
        {
          writer.Write((int) keyValuePair.Key);
          writer.Write(keyValuePair.Value);
        }
      }
    }

    public static void AssertAreEqual(SlimeEmotionDataV02 expected, SlimeEmotionDataV02 actual)
    {
      foreach (KeyValuePair<SlimeEmotions.Emotion, float> keyValuePair in expected.emotionData)
        ;
    }
  }
}
