// Decompiled with JetBrains decompiler
// Type: PooledSlimeAppearanceObjectProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PooledSlimeAppearanceObjectProvider : SlimeAppearanceObjectProvider
{
  private ObjectPool pool;

  public PooledSlimeAppearanceObjectProvider(ObjectPool poolInstance) => pool = poolInstance;

  public SlimeAppearanceObject Get(
    SlimeAppearanceObject appearanceObjectPrefab,
    GameObject targetParent)
  {
    return pool.Spawn(appearanceObjectPrefab, targetParent.transform, appearanceObjectPrefab.transform.position, appearanceObjectPrefab.transform.rotation);
  }

  public void Put(
    SlimeAppearanceObject appearanceObjectPrefab,
    SlimeAppearanceObject appearanceObject)
  {
    pool.Recycle(appearanceObject);
  }
}
