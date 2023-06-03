// Decompiled with JetBrains decompiler
// Type: PollenSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PollenSource : SRBehaviour
{
  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger)
      return;
    Identifiable component1 = col.gameObject.GetComponent<Identifiable>();
    if (!(component1 != null) || Identifiable.IsAllergyFree(component1.id))
      return;
    SlimeEmotions component2 = col.gameObject.GetComponent<SlimeEmotions>();
    if (component2 != null)
      component2.AddPollenSource();
    CauseSneeze(col.gameObject);
  }

  public void OnTriggerExit(Collider col)
  {
    if (col.isTrigger)
      return;
    Identifiable component1 = col.gameObject.GetComponent<Identifiable>();
    if (!(component1 != null) || Identifiable.IsAllergyFree(component1.id))
      return;
    SlimeEmotions component2 = col.gameObject.GetComponent<SlimeEmotions>();
    if (!(component2 != null))
      return;
    component2.RemovePollenSource();
  }

  private void CauseSneeze(GameObject gameObject)
  {
    SlimeFaceAnimator component = gameObject.GetComponent<SlimeFaceAnimator>();
    if (!(component != null))
      return;
    component.SetTrigger("triggerSneeze");
  }
}
