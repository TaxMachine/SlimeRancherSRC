// Decompiled with JetBrains decompiler
// Type: NotifyBiteComplete
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class NotifyBiteComplete : MonoBehaviour
{
  private Chomper chomper;

  public void Awake() => chomper = GetComponentInParent<Chomper>();

  public void DisableBiteModel() => chomper.BiteComplete();
}
