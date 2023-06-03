// Decompiled with JetBrains decompiler
// Type: FeralizeOnLargoTransformed
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FeralizeOnLargoTransformed : MonoBehaviour, OnTransformed
{
  public void OnTransformed()
  {
    Vacuumable component1 = GetComponent<Vacuumable>();
    SlimeFeral component2 = GetComponent<SlimeFeral>();
    if (!(component2 != null) || !(component1 != null) || component1.size == Vacuumable.Size.NORMAL)
      return;
    component2.SetFeral();
  }
}
