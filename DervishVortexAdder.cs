// Decompiled with JetBrains decompiler
// Type: DervishVortexAdder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DervishVortexAdder : VortexAdder
{
  public bool allowNonDervishSlimes;

  protected override bool CanAdd(GameObject gameObj)
  {
    if (!base.CanAdd(gameObj))
      return false;
    Identifiable.Id id = Identifiable.GetId(gameObj);
    if (Identifiable.IsNonSlimeResource(id))
      return true;
    return allowNonDervishSlimes && Identifiable.IsSlime(id) && gameObj.GetComponent<DervishSlimeSpin>() == null;
  }
}
