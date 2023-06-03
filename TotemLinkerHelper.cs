// Decompiled with JetBrains decompiler
// Type: TotemLinkerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TotemLinkerHelper : MonoBehaviour
{
  private TotemLinker totemLinker;

  public void Start()
  {
    TotemLinker[] componentsInChildren = GetComponentsInChildren<TotemLinker>(true);
    if (componentsInChildren == null || componentsInChildren.Length == 0)
      return;
    totemLinker = componentsInChildren[0];
  }

  public void Update() => totemLinker.UpdateEvenWhenInactive();
}
