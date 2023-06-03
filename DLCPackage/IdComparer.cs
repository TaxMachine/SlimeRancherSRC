// Decompiled with JetBrains decompiler
// Type: DLCPackage.IdComparer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace DLCPackage
{
  public class IdComparer : IEqualityComparer<Id>
  {
    public static IdComparer Instance = new IdComparer();

    public bool Equals(Id a, Id b) => a == b;

    public int GetHashCode(Id a) => (int) a;
  }
}
