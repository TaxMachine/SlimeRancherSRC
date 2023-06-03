// Decompiled with JetBrains decompiler
// Type: GordoEatTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GordoEatTrigger : MonoBehaviour
{
  private GordoEat eat;

  public void Awake() => eat = GetComponentInParent<GordoEat>();

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger)
      return;
    eat.MaybeEat(col);
  }
}
