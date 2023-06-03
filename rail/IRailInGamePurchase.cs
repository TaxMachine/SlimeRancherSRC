// Decompiled with JetBrains decompiler
// Type: rail.IRailInGamePurchase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace rail
{
  public interface IRailInGamePurchase
  {
    RailResult AsyncRequestAllPurchasableProducts(string user_data);

    RailResult AsyncRequestAllProducts(string user_data);

    RailResult GetProductInfo(uint product_id, RailPurchaseProductInfo product);

    RailResult AsyncPurchaseProducts(List<RailProductItem> cart_items, string user_data);

    RailResult AsyncFinishOrder(string order_id, string user_data);

    RailResult AsyncPurchaseProductsToAssets(List<RailProductItem> cart_items, string user_data);
  }
}
