// Decompiled with JetBrains decompiler
// Type: NotifyTutorialInBarnArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class NotifyTutorialInBarnArea : MonoBehaviour
{
  private TutorialDirector tutDir;

  public void Awake() => tutDir = SRSingleton<SceneContext>.Instance.TutorialDirector;

  public void OnTriggerEnter(Collider collider)
  {
    Identifiable component = collider.GetComponent<Identifiable>();
    if (!(component != null) || component.id != Identifiable.Id.PLAYER)
      return;
    tutDir.SetInBarnArea(true);
  }

  public void OnTriggerExit(Collider collider)
  {
    Identifiable component = collider.GetComponent<Identifiable>();
    if (!(component != null) || component.id != Identifiable.Id.PLAYER)
      return;
    tutDir.SetInBarnArea(false);
  }
}
