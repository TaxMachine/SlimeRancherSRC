// Decompiled with JetBrains decompiler
// Type: rail.ExchangeAssetsToFinished
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public class ExchangeAssetsToFinished : EventBase
  {
    public ulong exchange_to_asset_id;
    public RailProductItem to_product_info = new RailProductItem();
    public List<RailAssetItem> old_assets = new List<RailAssetItem>();
  }
}
