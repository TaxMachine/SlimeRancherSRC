// Decompiled with JetBrains decompiler
// Type: rail.IRailAssetsImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailAssetsImpl : RailObject, IRailAssets, IRailComponent
  {
    internal IRailAssetsImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailAssetsImpl()
    {
    }

    public virtual RailResult AsyncRequestAllAssets(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncRequestAllAssets(swigCPtr_, user_data);

    public virtual RailResult QueryAssetInfo(ulong asset_id, RailAssetInfo asset_info)
    {
      IntPtr num = asset_info == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailAssetInfo__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailAssets_QueryAssetInfo(swigCPtr_, asset_id, num);
      }
      finally
      {
        if (asset_info != null)
          RailConverter.Cpp2Csharp(num, asset_info);
        RAIL_API_PINVOKE.delete_RailAssetInfo(num);
      }
    }

    public virtual RailResult AsyncUpdateAssetsProperty(
      List<RailAssetProperty> asset_property_list,
      string user_data)
    {
      IntPtr num = asset_property_list == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetProperty__SWIG_0();
      if (asset_property_list != null)
        RailConverter.Csharp2Cpp(asset_property_list, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncUpdateAssetsProperty(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailAssetProperty(num);
      }
    }

    public virtual RailResult AsyncDirectConsumeAssets(List<RailAssetItem> assets, string user_data)
    {
      IntPtr num = assets == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0();
      if (assets != null)
        RailConverter.Csharp2Cpp(assets, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncDirectConsumeAssets(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(num);
      }
    }

    public virtual RailResult AsyncStartConsumeAsset(ulong asset_id, string user_data) => (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncStartConsumeAsset(swigCPtr_, asset_id, user_data);

    public virtual RailResult AsyncUpdateConsumeProgress(
      ulong asset_id,
      string progress,
      string user_data)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncUpdateConsumeProgress(swigCPtr_, asset_id, progress, user_data);
    }

    public virtual RailResult AsyncCompleteConsumeAsset(
      ulong asset_id,
      uint quantity,
      string user_data)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncCompleteConsumeAsset(swigCPtr_, asset_id, quantity, user_data);
    }

    public virtual RailResult AsyncExchangeAssets(
      List<RailAssetItem> old_assets,
      RailProductItem to_product_info,
      string user_data)
    {
      IntPtr num1 = old_assets == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0();
      if (old_assets != null)
        RailConverter.Csharp2Cpp(old_assets, num1);
      IntPtr num2 = to_product_info == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailProductItem__SWIG_0();
      if (to_product_info != null)
        RailConverter.Csharp2Cpp(to_product_info, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncExchangeAssets(swigCPtr_, num1, num2, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(num1);
        RAIL_API_PINVOKE.delete_RailProductItem(num2);
      }
    }

    public virtual RailResult AsyncExchangeAssetsTo(
      List<RailAssetItem> old_assets,
      RailProductItem to_product_info,
      ulong add_to_exist_assets,
      string user_data)
    {
      IntPtr num1 = old_assets == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0();
      if (old_assets != null)
        RailConverter.Csharp2Cpp(old_assets, num1);
      IntPtr num2 = to_product_info == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailProductItem__SWIG_0();
      if (to_product_info != null)
        RailConverter.Csharp2Cpp(to_product_info, num2);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncExchangeAssetsTo(swigCPtr_, num1, num2, add_to_exist_assets, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(num1);
        RAIL_API_PINVOKE.delete_RailProductItem(num2);
      }
    }

    public virtual RailResult AsyncSplitAsset(
      ulong source_asset,
      uint to_quantity,
      string user_data)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncSplitAsset(swigCPtr_, source_asset, to_quantity, user_data);
    }

    public virtual RailResult AsyncSplitAssetTo(
      ulong source_asset,
      uint to_quantity,
      ulong add_to_asset,
      string user_data)
    {
      return (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncSplitAssetTo(swigCPtr_, source_asset, to_quantity, add_to_asset, user_data);
    }

    public virtual RailResult AsyncMergeAsset(List<RailAssetItem> source_assets, string user_data)
    {
      IntPtr num = source_assets == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0();
      if (source_assets != null)
        RailConverter.Csharp2Cpp(source_assets, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncMergeAsset(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(num);
      }
    }

    public virtual RailResult AsyncMergeAssetTo(
      List<RailAssetItem> source_assets,
      ulong add_to_asset,
      string user_data)
    {
      IntPtr num = source_assets == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0();
      if (source_assets != null)
        RailConverter.Csharp2Cpp(source_assets, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailAssets_AsyncMergeAssetTo(swigCPtr_, num, add_to_asset, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(num);
      }
    }

    public virtual ulong GetComponentVersion() => RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(swigCPtr_);

    public virtual void Release() => RAIL_API_PINVOKE.IRailComponent_Release(swigCPtr_);
  }
}
