// Decompiled with JetBrains decompiler
// Type: SlimeDefinitionEqualityComparer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SlimeDefinitionEqualityComparer : IEqualityComparer<SlimeDefinition>
{
  public static SlimeDefinitionEqualityComparer Default = new SlimeDefinitionEqualityComparer();

  public bool Equals(SlimeDefinition x, SlimeDefinition y) => x == y;

  public int GetHashCode(SlimeDefinition obj) => obj.GetHashCode();
}
