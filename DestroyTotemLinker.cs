// Decompiled with JetBrains decompiler
// Type: DestroyTotemLinker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DestroyTotemLinker : MonoBehaviour
{
  public void Start()
  {
    TotemLinker componentInChildren = GetComponentInChildren<TotemLinker>();
    if (componentInChildren != null)
      Destroyer.Destroy(componentInChildren.gameObject, "DestroyTotemLinker.Start#1");
    TotemLinkerHelper component = GetComponent<TotemLinkerHelper>();
    if (!(component != null))
      return;
    Destroyer.Destroy(component, "DestroyTotemLinker.Start#2");
  }
}
