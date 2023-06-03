// Decompiled with JetBrains decompiler
// Type: rail.IRailAssets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailAssets : IRailComponent
  {
    RailResult AsyncRequestAllAssets(string user_data);

    RailResult QueryAssetInfo(ulong asset_id, RailAssetInfo asset_info);

    RailResult AsyncUpdateAssetsProperty(
      List<RailAssetProperty> asset_property_list,
      string user_data);

    RailResult AsyncDirectConsumeAssets(List<RailAssetItem> assets, string user_data);

    RailResult AsyncStartConsumeAsset(ulong asset_id, string user_data);

    RailResult AsyncUpdateConsumeProgress(ulong asset_id, string progress, string user_data);

    RailResult AsyncCompleteConsumeAsset(ulong asset_id, uint quantity, string user_data);

    RailResult AsyncExchangeAssets(
      List<RailAssetItem> old_assets,
      RailProductItem to_product_info,
      string user_data);

    RailResult AsyncExchangeAssetsTo(
      List<RailAssetItem> old_assets,
      RailProductItem to_product_info,
      ulong add_to_exist_assets,
      string user_data);

    RailResult AsyncSplitAsset(ulong source_asset, uint to_quantity, string user_data);

    RailResult AsyncSplitAssetTo(
      ulong source_asset,
      uint to_quantity,
      ulong add_to_asset,
      string user_data);

    RailResult AsyncMergeAsset(List<RailAssetItem> source_assets, string user_data);

    RailResult AsyncMergeAssetTo(
      List<RailAssetItem> source_assets,
      ulong add_to_asset,
      string user_data);
  }
}
