// Decompiled with JetBrains decompiler
// Type: PediaAreaTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PediaAreaTrigger : SRBehaviour
{
  public PediaDirector.Id pediaId;

  public void OnTriggerEnter(Collider col)
  {
    if (!(col.gameObject == SRSingleton<SceneContext>.Instance.Player) || !(col is CharacterController))
      return;
    SRSingleton<SceneContext>.Instance.PediaDirector.MaybeShowPopup(pediaId);
  }
}
