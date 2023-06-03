// Decompiled with JetBrains decompiler
// Type: MosaicAttractor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MosaicAttractor : Attractor
{
  public void Start() => SetAweFactor(1f);

  public override void OnTriggerEnter(Collider col)
  {
    if (!(col.GetComponentInChildren<MosaicAttractor>() == null))
      return;
    base.OnTriggerEnter(col);
  }

  public override bool CauseMoveTowards() => true;
}
