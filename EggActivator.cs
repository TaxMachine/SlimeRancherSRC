// Decompiled with JetBrains decompiler
// Type: EggActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EggActivator : SRBehaviour
{
  public float eggPeriod = 3f;
  public GameObject activateObj;
  private float endEgg;

  public void AddEgg()
  {
    endEgg = Time.time + eggPeriod;
    enabled = true;
    activateObj.SetActive(true);
  }

  public void Update()
  {
    if (Time.time < (double) endEgg)
      return;
    endEgg = 0.0f;
    enabled = false;
    activateObj.SetActive(false);
    Destroyer.Destroy(this, "EggActivator.Update");
  }
}
