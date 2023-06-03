// Decompiled with JetBrains decompiler
// Type: SlimeAppearanceEqualityComparer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SlimeAppearanceEqualityComparer : IEqualityComparer<SlimeAppearance>
{
  public static SlimeAppearanceEqualityComparer Default = new SlimeAppearanceEqualityComparer();

  public bool Equals(SlimeAppearance x, SlimeAppearance y) => x == y;

  public int GetHashCode(SlimeAppearance obj) => obj.GetHashCode();
}
