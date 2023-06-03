// Decompiled with JetBrains decompiler
// Type: DriveCalculator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DriveCalculator
{
  public SlimeEmotions.Emotion emotion;
  public float extraDrive;
  public float minDrive;

  public DriveCalculator(SlimeEmotions.Emotion emotion, float extraDrive, float minDrive)
  {
    this.emotion = emotion;
    this.extraDrive = extraDrive;
    this.minDrive = minDrive;
  }

  public virtual float Drive(SlimeEmotions emotions, Identifiable.Id id) => Mathf.Max(0.0f, Mathf.Max(minDrive, emotions.GetCurr(emotion)) + extraDrive);
}
