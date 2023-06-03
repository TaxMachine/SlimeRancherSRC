// Decompiled with JetBrains decompiler
// Type: CorralRegion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CorralRegion : SRBehaviour
{
  private static List<CorralRegion> allCorrals = new List<CorralRegion>();

  public void Awake() => allCorrals.Add(this);

  public void OnDestroy() => allCorrals.Remove(this);

  public static bool IsWithin(Vector3 pos)
  {
    foreach (Component allCorral in allCorrals)
    {
      if (allCorral.GetComponent<Collider>().bounds.Contains(pos))
        return true;
    }
    return false;
  }
}
