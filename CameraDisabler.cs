// Decompiled with JetBrains decompiler
// Type: CameraDisabler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CameraDisabler : MonoBehaviour
{
  private Camera cam;
  private List<Component> blockers = new List<Component>();
  private LayerMask standardMask;
  private LayerMask uiOnlyMask;

  public void Start()
  {
    uiOnlyMask = LayerMask.GetMask("UI");
    cam = GetComponent<Camera>();
    standardMask = cam.cullingMask;
  }

  public void AddBlocker(Component comp)
  {
    blockers.Add(comp);
    if (blockers.Count <= 0)
      return;
    cam.cullingMask = uiOnlyMask;
  }

  public void RemoveBlocker(Component comp)
  {
    blockers.Remove(comp);
    if (blockers.Count > 0 || !(cam != null))
      return;
    cam.cullingMask = standardMask;
  }
}
