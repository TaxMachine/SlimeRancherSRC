// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ExchangeOfferV04
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ExchangeOfferV04 : VersionedPersistedDataSet<ExchangeOfferV03>
  {
    public bool hasOffer;
    public List<RequestedItemEntryV03> requests;
    public List<ItemEntryV03> rewards;
    public double expireTime;
    public double earlyExchangeTime;
    public string rancherId;
    public string offerId;

    public override string Identifier => "SREO";

    public override uint Version => 4;

    public ExchangeOfferV04()
    {
    }

    public ExchangeOfferV04(ExchangeOfferV03 legacyData) => UpgradeFrom(legacyData);

    public static ExchangeOfferV04 Load(BinaryReader reader)
    {
      ExchangeOfferV04 exchangeOfferV04 = new ExchangeOfferV04();
      exchangeOfferV04.Load(reader.BaseStream);
      return exchangeOfferV04;
    }

    protected override void LoadData(BinaryReader reader)
    {
      hasOffer = reader.ReadBoolean();
      if (!hasOffer)
        return;
      rancherId = reader.ReadString();
      offerId = reader.ReadString();
      expireTime = reader.ReadDouble();
      earlyExchangeTime = reader.ReadDouble();
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
      writer.Write(earlyExchangeTime);
      writer.Write(requests.Count);
      foreach (PersistedDataSet request in requests)
        request.Write(writer.BaseStream);
      writer.Write(rewards.Count);
      foreach (PersistedDataSet reward in rewards)
        reward.Write(writer.BaseStream);
    }

    protected override void UpgradeFrom(ExchangeOfferV03 legacyData)
    {
      hasOffer = legacyData.hasOffer;
      requests = legacyData.requests;
      rewards = legacyData.rewards;
      expireTime = legacyData.expireTime;
      rancherId = legacyData.rancherId;
      offerId = legacyData.offerId;
      earlyExchangeTime = double.NegativeInfinity;
    }

    public static void AssertAreEqual(ExchangeOfferV04 expected, ExchangeOfferV04 actual)
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

    public static void AssertAreEqual(ExchangeOfferV03 expected, ExchangeOfferV04 actual)
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
  }
}
