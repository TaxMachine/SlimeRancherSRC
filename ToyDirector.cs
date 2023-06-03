// Decompiled with JetBrains decompiler
// Type: ToyDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class ToyDirector
{
  private static readonly List<Identifiable.Id> BASE_TOYS = new List<Identifiable.Id>()
  {
    Identifiable.Id.BEACH_BALL_TOY,
    Identifiable.Id.BIG_ROCK_TOY,
    Identifiable.Id.YARN_BALL_TOY,
    Identifiable.Id.NIGHT_LIGHT_TOY,
    Identifiable.Id.POWER_CELL_TOY,
    Identifiable.Id.BOMB_BALL_TOY,
    Identifiable.Id.BUZZY_BEE_TOY,
    Identifiable.Id.RUBBER_DUCKY_TOY,
    Identifiable.Id.OCTO_BUDDY_TOY
  };
  private const int CORPORATE_LEVEL_UNLOCK = 10;
  private static readonly List<Identifiable.Id> UPGRADED_TOYS = new List<Identifiable.Id>()
  {
    Identifiable.Id.CRYSTAL_BALL_TOY,
    Identifiable.Id.STUFFED_CHICKEN_TOY,
    Identifiable.Id.PUZZLE_CUBE_TOY,
    Identifiable.Id.DISCO_BALL_TOY,
    Identifiable.Id.GYRO_TOP_TOY,
    Identifiable.Id.CHARCOAL_BRICK_TOY,
    Identifiable.Id.SOL_MATE_TOY,
    Identifiable.Id.STEGO_BUDDY_TOY
  };
  private List<Identifiable.Id> registered = new List<Identifiable.Id>();

  public void Register(Identifiable.Id id)
  {
    registered.RemoveAll(it => it == id);
    registered.Add(id);
  }

  public IEnumerable<Identifiable.Id> GetPurchaseableToys()
  {
    int progress = SRSingleton<SceneContext>.Instance.ProgressDirector.GetProgress(ProgressDirector.ProgressType.CORPORATE_PARTNER);
    foreach (Identifiable.Id purchaseableToy in BASE_TOYS)
      yield return purchaseableToy;
    if (progress >= 10)
    {
      foreach (Identifiable.Id purchaseableToy in UPGRADED_TOYS)
        yield return purchaseableToy;
    }
    foreach (Identifiable.Id purchaseableToy in registered)
      yield return purchaseableToy;
  }
}
