// Decompiled with JetBrains decompiler
// Type: LineupUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public static class LineupUtils
{
  public static SlimeAppearanceApplicator GenerateAppearancePreview(
    SlimeAppearanceApplicator prefab,
    SlimeDefinition slimeDefinition,
    SlimeAppearance appearance)
  {
    SlimeAppearanceApplicator appearancePreview = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
    appearancePreview.enabled = false;
    appearancePreview.SlimeDefinition = null;
    appearancePreview.Appearance = appearance;
    try
    {
      appearancePreview.ApplyAppearance();
    }
    catch (Exception ex)
    {
      Log.Error("An issue occurred while trying to apply the appearance: " + appearance.name, ex);
    }
    foreach (EnableBasedOnGrounded componentsInChild in appearancePreview.GetComponentsInChildren<EnableBasedOnGrounded>())
      componentsInChild.gameObject.SetActive(!componentsInChild.enableOnGrounded);
    foreach (Behaviour componentsInChild in appearancePreview.GetComponentsInChildren<DeactivateOnHeld>())
      componentsInChild.enabled = false;
    foreach (UnityEngine.Object componentsInChild in appearancePreview.GetComponentsInChildren<NotifyBiteComplete>())
      UnityEngine.Object.Destroy(componentsInChild);
    appearancePreview.transform.localScale = new Vector3(slimeDefinition.PrefabScale, slimeDefinition.PrefabScale, slimeDefinition.PrefabScale);
    return appearancePreview;
  }
}
