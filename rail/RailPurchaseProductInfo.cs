// Decompiled with JetBrains decompiler
// Type: rail.RailPurchaseProductInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public class RailPurchaseProductInfo
  {
    public string category;
    public float original_price;
    public string description;
    public DiscountInfo discount = new DiscountInfo();
    public bool is_purchasable;
    public string name;
    public string currency_type;
    public string product_thumbnail;
    public RailPurchaseProductExtraInfo extra_info = new RailPurchaseProductExtraInfo();
    public uint product_id;
  }
}
