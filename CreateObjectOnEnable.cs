// Decompiled with JetBrains decompiler
// Type: CreateObjectOnEnable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CreateObjectOnEnable : SRBehaviour
{
  public GameObject toCreate;
  public bool attachToParent;

  public void OnEnable() => Instantiate(toCreate, Vector3.zero, Quaternion.identity).transform.SetParent(attachToParent ? transform.parent : transform, false);
}
