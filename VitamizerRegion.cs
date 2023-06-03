// Decompiled with JetBrains decompiler
// Type: VitamizerRegion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class VitamizerRegion : SRBehaviour
{
  public const float TWINS_PROB = 0.5f;
  private static List<VitamizerRegion> allVitamizers = new List<VitamizerRegion>();

  public void Awake() => allVitamizers.Add(this);

  public void OnDestroy() => allVitamizers.Remove(this);

  public static bool IsWithin(Vector3 pos)
  {
    foreach (Component allVitamizer in allVitamizers)
    {
      if (allVitamizer.GetComponent<Collider>().bounds.Contains(pos))
        return true;
    }
    return false;
  }
}
