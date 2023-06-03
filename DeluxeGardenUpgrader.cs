// Decompiled with JetBrains decompiler
// Type: DeluxeGardenUpgrader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DeluxeGardenUpgrader : PlotUpgrader
{
  public GameObject deluxeStuff;

  public override void Apply(LandPlot.Upgrade upgrade)
  {
    if (upgrade != LandPlot.Upgrade.DELUXE_GARDEN || !(deluxeStuff != null) || deluxeStuff.activeSelf)
      return;
    deluxeStuff.SetActive(true);
    Identifiable.Id attachedCropId = GetComponent<LandPlot>().GetAttachedCropId();
    if (attachedCropId == Identifiable.Id.NONE)
      return;
    SpawnResource componentInChildren = GetComponentInChildren<SpawnResource>();
    Destroyer.Destroy(componentInChildren.gameObject, "DeluxeGardenUpgrader.Apply");
    GetComponentInChildren<GardenCatcher>().Plant(attachedCropId, true).GetComponent<SpawnResource>().InitAsReplacement(componentInChildren);
  }
}
