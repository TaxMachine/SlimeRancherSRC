// Decompiled with JetBrains decompiler
// Type: GlitchBreadcrumbNetworkNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GlitchBreadcrumbNetworkNode : PathingNetworkNode
{
  [Tooltip("GameObject that is enabled while this breadcrumb is active. (optional)")]
  public GameObject onActiveFX;

  public void Awake()
  {
    if (!(onActiveFX != null))
      return;
    onActiveFX.SetActive(false);
  }

  public void Activate(Vector3 nextPoint)
  {
    if (!(onActiveFX != null))
      return;
    onActiveFX.SetActive(true);
    onActiveFX.transform.rotation = Quaternion.LookRotation(nextPoint - position);
  }

  public void Deactivate()
  {
    if (!(onActiveFX != null))
      return;
    onActiveFX.SetActive(false);
  }
}
