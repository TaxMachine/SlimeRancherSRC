// Decompiled with JetBrains decompiler
// Type: Range
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class Range
{
  public float min;
  public float max;

  public float Random() => Random(Randoms.SHARED);

  public float Random(Randoms rand) => rand.GetInRange(min, max);
}
