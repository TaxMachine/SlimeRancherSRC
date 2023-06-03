// Decompiled with JetBrains decompiler
// Type: StorageTypeExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public static class StorageTypeExtensions
{
  private static Dictionary<SiloStorage.StorageType, HashSet<Identifiable.Id>> getContentsCache = new Dictionary<SiloStorage.StorageType, HashSet<Identifiable.Id>>(SiloStorage.StorageTypeComparer.Instance);

  public static bool Contains(this SiloStorage.StorageType type, Identifiable.Id id) => type.GetContents().Contains(id);

  public static HashSet<Identifiable.Id> GetContents(this SiloStorage.StorageType type)
  {
    HashSet<Identifiable.Id> contents;
    if (getContentsCache.TryGetValue(type, out contents))
      return contents;
    HashSet<Identifiable.Id> idSet = new HashSet<Identifiable.Id>(Identifiable.idComparer);
    switch (type)
    {
      case SiloStorage.StorageType.NON_SLIMES:
        idSet.UnionWith(Identifiable.NON_SLIMES_CLASS);
        idSet.UnionWith(Identifiable.ORNAMENT_CLASS);
        idSet.UnionWith(Identifiable.ECHO_CLASS);
        idSet.UnionWith(Identifiable.ECHO_NOTE_CLASS);
        break;
      case SiloStorage.StorageType.PLORT:
        idSet.UnionWith(Identifiable.PLORT_CLASS);
        break;
      case SiloStorage.StorageType.FOOD:
        idSet.UnionWith(Identifiable.FOOD_CLASS);
        idSet.UnionWith(Identifiable.CHICK_CLASS);
        break;
      case SiloStorage.StorageType.CRAFTING:
        idSet.UnionWith(Identifiable.PLORT_CLASS);
        idSet.UnionWith(Identifiable.CRAFT_CLASS);
        break;
      case SiloStorage.StorageType.ELDER:
        idSet.Add(Identifiable.Id.ELDER_HEN);
        idSet.Add(Identifiable.Id.ELDER_ROOSTER);
        break;
      default:
        throw new ArgumentException(string.Format("Failed to get contents for storage type. [type={0}]", type));
    }
    idSet.Remove(Identifiable.Id.QUICKSILVER_PLORT);
    return getContentsCache[type] = idSet;
  }
}
