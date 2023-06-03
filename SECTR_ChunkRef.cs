// Decompiled with JetBrains decompiler
// Type: SECTR_ChunkRef
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class SECTR_ChunkRef : MonoBehaviour
{
  private static List<SECTR_ChunkRef> allChunkRefs = new List<SECTR_ChunkRef>();
  public Transform RealSector;
  public bool Recentered;

  public static SECTR_ChunkRef FindChunkRef(string chunkName)
  {
    int count = allChunkRefs.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_ChunkRef allChunkRef = allChunkRefs[index];
      if (allChunkRef.name == chunkName)
        return allChunkRef;
    }
    return null;
  }

  private void OnEnable() => allChunkRefs.Add(this);

  private void OnDisable() => allChunkRefs.Remove(this);
}
