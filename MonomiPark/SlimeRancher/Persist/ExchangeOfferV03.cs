// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ExchangeOfferV03
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ExchangeOfferV03 : VersionedPersistedDataSet<ExchangeOfferV02>
  {
    public bool hasOffer;
    public List<RequestedItemEntryV03> requests;
    public List<ItemEntryV03> rewards;
    public double expireTime;
    public string rancherId;
    public string offerId;

    public override string Identifier => "SREO";

    public override uint Version => 3;

    public ExchangeOfferV03()
    {
    }

    public ExchangeOfferV03(ExchangeOfferV02 legacyData) => UpgradeFrom(legacyData);

    public static ExchangeOfferV03 Load(BinaryReader reader)
    {
      ExchangeOfferV03 exchangeOfferV03 = new ExchangeOfferV03();
      exchangeOfferV03.Load(reader.BaseStream);
      return exchangeOfferV03;
    }

    protected override void LoadData(BinaryReader reader)
    {
      hasOffer = reader.ReadBoolean();
      if (!hasOffer)
        return;
      rancherId = reader.ReadString();
      offerId = reader.ReadString();
      expireTime = reader.ReadDouble();
      int num1 = reader.ReadInt32();
      requests = new List<RequestedItemEntryV03>();
      for (; num1 > 0; --num1)
      {
        RequestedItemEntryV03 requestedItemEntryV03 = new RequestedItemEntryV03();
        requestedItemEntryV03.Load(reader.BaseStream);
        requests.Add(requestedItemEntryV03);
      }
      int num2 = reader.ReadInt32();
      rewards = new List<ItemEntryV03>();
      for (; num2 > 0; --num2)
      {
        ItemEntryV03 itemEntryV03 = new ItemEntryV03();
        itemEntryV03.Load(reader.BaseStream);
        rewards.Add(itemEntryV03);
      }
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(hasOffer);
      if (!hasOffer)
        return;
      writer.Write(rancherId);
      writer.Write(offerId);
      writer.Write(expireTime);
      writer.Write(requests.Count);
      foreach (PersistedDataSet request in requests)
        request.Write(writer.BaseStream);
      writer.Write(rewards.Count);
      foreach (PersistedDataSet reward in rewards)
        reward.Write(writer.BaseStream);
    }

    protected override void UpgradeFrom(ExchangeOfferV02 legacyData)
    {
      hasOffer = legacyData.hasOffer;
      if (!hasOffer)
        return;
      requests = UpgradeFrom(legacyData.requests);
      rewards = UpgradeFrom(legacyData.rewards);
      expireTime = legacyData.expireTime;
      rancherId = legacyData.rancherId;
      offerId = legacyData.offerId;
    }

    public static void AssertAreEqual(ExchangeOfferV03 expected, ExchangeOfferV03 actual)
    {
      for (int index = 0; index < expected.requests.Count; ++index)
        RequestedItemEntryV03.AssertAreEqual(expected.requests[index], actual.requests[index]);
      for (int index = 0; index < expected.rewards.Count; ++index)
        ItemEntryV03.AssertAreEqual(expected.rewards[index], actual.rewards[index]);
    }

    public static void AssertAreEqual(ExchangeOfferV02 expected, ExchangeOfferV03 actual)
    {
      if (expected.requests != null)
      {
        for (int index = 0; index < expected.requests.Count; ++index)
          RequestedItemEntryV03.AssertAreEqual(expected.requests[index], actual.requests[index]);
      }
      if (expected.rewards == null)
        return;
      for (int index = 0; index < expected.rewards.Count; ++index)
        ItemEntryV03.AssertAreEqual(expected.rewards[index], actual.rewards[index]);
    }

    private List<ItemEntryV03> UpgradeFrom(List<ItemEntryV02> legacyData)
    {
      List<ItemEntryV03> itemEntryV03List = new List<ItemEntryV03>();
      foreach (ItemEntryV02 legacyData1 in legacyData)
        itemEntryV03List.Add(new ItemEntryV03(legacyData1));
      return itemEntryV03List;
    }

    private List<RequestedItemEntryV03> UpgradeFrom(List<RequestedItemEntryV02> legacyData)
    {
      List<RequestedItemEntryV03> requestedItemEntryV03List = new List<RequestedItemEntryV03>();
      foreach (RequestedItemEntryV02 legacyData1 in legacyData)
        requestedItemEntryV03List.Add(new RequestedItemEntryV03(legacyData1));
      return requestedItemEntryV03List;
    }
  }
}
