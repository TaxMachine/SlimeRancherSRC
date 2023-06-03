// Decompiled with JetBrains decompiler
// Type: ActivateOnPlayerEnter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ActivateOnPlayerEnter : MonoBehaviour
{
  public GameObject toActivate;

  public void Awake() => toActivate.SetActive(false);

  public void OnTriggerEnter(Collider collider)
  {
    Identifiable component = collider.GetComponent<Identifiable>();
    if (!(component != null) || component.id != Identifiable.Id.PLAYER)
      return;
    toActivate.SetActive(true);
  }

  public void OnTriggerExit(Collider collider)
  {
    Identifiable component = collider.GetComponent<Identifiable>();
    if (!(component != null) || component.id != Identifiable.Id.PLAYER)
      return;
    toActivate.SetActive(false);
  }
}
