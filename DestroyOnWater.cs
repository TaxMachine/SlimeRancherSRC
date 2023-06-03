// Decompiled with JetBrains decompiler
// Type: DestroyOnWater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DestroyOnWater : SRBehaviour, LiquidConsumer
{
  public GameObject destroyFX;
  public bool destroyAsActor;

  public void AddLiquid(Identifiable.Id liquidId, float units)
  {
    if (!Identifiable.IsWater(liquidId))
      return;
    DestroyWithFX();
  }

  public void OnTriggerEnter(Collider col)
  {
    LiquidSource component1 = col.GetComponent<LiquidSource>();
    if (component1 != null && Identifiable.IsWater(component1.liquidId))
    {
      DestroyWithFX();
    }
    else
    {
      DestroyOnTouching component2 = col.GetComponent<DestroyOnTouching>();
      if (!(component2 != null) || component2.wateringUnits <= 0.0)
        return;
      DestroyWithFX();
    }
  }

  private void DestroyWithFX()
  {
    if (destroyFX != null)
      SpawnAndPlayFX(destroyFX, transform.position, transform.rotation);
    if (destroyAsActor)
      Destroyer.DestroyActor(gameObject, "DestroyOnWater.DestroyWithFX");
    else
      RequestDestroy("DestroyOnWater.DestroyWithFX");
  }
}
