// Decompiled with JetBrains decompiler
// Type: rail.IRailInGamePurchaseImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace rail
{
  public class IRailInGamePurchaseImpl : RailObject, IRailInGamePurchase
  {
    internal IRailInGamePurchaseImpl(IntPtr cPtr) => swigCPtr_ = cPtr;

    ~IRailInGamePurchaseImpl()
    {
    }

    public virtual RailResult AsyncRequestAllPurchasableProducts(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailInGamePurchase_AsyncRequestAllPurchasableProducts(swigCPtr_, user_data);

    public virtual RailResult AsyncRequestAllProducts(string user_data) => (RailResult) RAIL_API_PINVOKE.IRailInGamePurchase_AsyncRequestAllProducts(swigCPtr_, user_data);

    public virtual RailResult GetProductInfo(uint product_id, RailPurchaseProductInfo product)
    {
      IntPtr num = product == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailPurchaseProductInfo__SWIG_0();
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailInGamePurchase_GetProductInfo(swigCPtr_, product_id, num);
      }
      finally
      {
        if (product != null)
          RailConverter.Cpp2Csharp(num, product);
        RAIL_API_PINVOKE.delete_RailPurchaseProductInfo(num);
      }
    }

    public virtual RailResult AsyncPurchaseProducts(
      List<RailProductItem> cart_items,
      string user_data)
    {
      IntPtr num = cart_items == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailProductItem__SWIG_0();
      if (cart_items != null)
        RailConverter.Csharp2Cpp(cart_items, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailInGamePurchase_AsyncPurchaseProducts(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailProductItem(num);
      }
    }

    public virtual RailResult AsyncFinishOrder(string order_id, string user_data) => (RailResult) RAIL_API_PINVOKE.IRailInGamePurchase_AsyncFinishOrder(swigCPtr_, order_id, user_data);

    public virtual RailResult AsyncPurchaseProductsToAssets(
      List<RailProductItem> cart_items,
      string user_data)
    {
      IntPtr num = cart_items == null ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailProductItem__SWIG_0();
      if (cart_items != null)
        RailConverter.Csharp2Cpp(cart_items, num);
      try
      {
        return (RailResult) RAIL_API_PINVOKE.IRailInGamePurchase_AsyncPurchaseProductsToAssets(swigCPtr_, num, user_data);
      }
      finally
      {
        RAIL_API_PINVOKE.delete_RailArrayRailProductItem(num);
      }
    }
  }
}
