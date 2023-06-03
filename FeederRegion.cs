// Decompiled with JetBrains decompiler
// Type: FeederRegion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FeederRegion : SRBehaviour
{
  public const float GROWTH_FACTOR = 2f;

  public void OnTriggerEnter(Collider collider)
  {
    TransformAfterTime component = collider.gameObject.GetComponent<TransformAfterTime>();
    if (!(component != null))
      return;
    component.AddFeeder(this);
  }

  public void OnTriggerExit(Collider collider)
  {
    TransformAfterTime component = collider.gameObject.GetComponent<TransformAfterTime>();
    if (!(component != null))
      return;
    component.RemoveFeeder(this);
  }
}
