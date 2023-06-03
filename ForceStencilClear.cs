// Decompiled with JetBrains decompiler
// Type: ForceStencilClear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ForceStencilClear : MonoBehaviour
{
  private HashSet<GameObject> enablers;

  public void OnPreRender() => GL.Clear(true, false, Color.black);

  public void RegisterEnabler(GameObject enabler)
  {
    if (enablers == null)
      enablers = new HashSet<GameObject>();
    enablers.Add(enabler);
    enabled = true;
  }

  public void DeregisterEnabler(GameObject enabler)
  {
    if (enablers == null || !enablers.Remove(enabler) || enablers.Count != 0)
      return;
    enabled = false;
  }
}
