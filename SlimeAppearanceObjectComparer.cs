// Decompiled with JetBrains decompiler
// Type: SlimeAppearanceObjectComparer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class SlimeAppearanceObjectComparer : IEqualityComparer<SlimeAppearanceObject>
{
  public bool Equals(SlimeAppearanceObject id1, SlimeAppearanceObject id2) => id1.GetInstanceID() == id2.GetInstanceID();

  public int GetHashCode(SlimeAppearanceObject obj) => obj.GetHashCode();
}
