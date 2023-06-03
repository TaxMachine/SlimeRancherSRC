// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.FallbackCapable`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace MonomiPark.SlimeRancher.Persist
{
  public interface FallbackCapable<T> where T : PersistedDataSet, new()
  {
    void UpgradeFrom(T legacyData);
  }
}
