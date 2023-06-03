// Decompiled with JetBrains decompiler
// Type: CoopRegion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CoopRegion : SRBehaviour
{
  private static List<CoopRegion> allCoops = new List<CoopRegion>();
  private bool isDeluxe;

  public void Awake() => allCoops.Add(this);

  public void OnDestroy() => allCoops.Remove(this);

  public void SetDeluxe() => isDeluxe = true;

  public static bool IsWithin(Vector3 pos) => IsWithin(pos, false);

  public static bool IsWithinDeluxe(Vector3 pos) => IsWithin(pos, true);

  private static bool IsWithin(Vector3 pos, bool mustBeDeluxe)
  {
    bool flag = false;
    foreach (CoopRegion allCoop in allCoops)
    {
      if (allCoop.GetComponent<Collider>().bounds.Contains(pos))
        flag = ((flag ? 1 : 0) | (!mustBeDeluxe ? 1 : (allCoop.isDeluxe ? 1 : 0))) != 0;
    }
    return flag;
  }
}
