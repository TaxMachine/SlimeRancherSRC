// Decompiled with JetBrains decompiler
// Type: Attractor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Attractor : SRBehaviour
{
  private float aweFactor;

  public virtual void OnTriggerEnter(Collider col)
  {
    AweTowardsAttractors component = col.GetComponent<AweTowardsAttractors>();
    if (!(component != null))
      return;
    component.RegisterAttractor(this);
  }

  public virtual void OnTriggerExit(Collider col)
  {
    AweTowardsAttractors component = col.GetComponent<AweTowardsAttractors>();
    if (!(component != null))
      return;
    component.UnregisterAttractor(this);
  }

  public void SetAweFactor(float aweFactor) => this.aweFactor = aweFactor;

  public virtual float AweFactor(GameObject slime) => !isActiveAndEnabled ? 0.0f : aweFactor;

  public virtual bool CauseMoveTowards() => false;
}
