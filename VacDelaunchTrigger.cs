// Decompiled with JetBrains decompiler
// Type: VacDelaunchTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class VacDelaunchTrigger : MonoBehaviour
{
  private Vacuumable vacuumable;

  public void Start() => vacuumable = GetComponentInParent<Vacuumable>();

  public void SetTriggerEnabled(bool enabled) => gameObject.SetActive(enabled);

  public void Delaunch()
  {
    if (vacuumable == null)
      vacuumable = GetComponentInParent<Vacuumable>();
    if (!(vacuumable != null) || !vacuumable.delaunch())
      return;
    Identifiable component = vacuumable.GetComponent<Identifiable>();
    if (!(component != null) || !Identifiable.IsSlime(component.id))
      return;
    SRSingleton<SceneContext>.Instance.TutorialDirector.OnDelaunchedSlime();
  }
}
