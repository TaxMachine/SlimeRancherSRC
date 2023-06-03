// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ExchangeOfferV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ExchangeOfferV02 : PersistedDataSet
  {
    public bool hasOffer;
    public List<RequestedItemEntryV02> requests;
    public List<ItemEntryV02> rewards;
    public float expireTime;
    public string rancherId;
    public string offerId;

    public override string Identifier => "SREO";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      hasOffer = reader.ReadBoolean();
      if (!hasOffer)
        return;
      rancherId = reader.ReadString();
      offerId = reader.ReadString();
      expireTime = reader.ReadSingle();
      int num1 = reader.ReadInt32();
      requests = new List<RequestedItemEntryV02>();
      for (; num1 > 0; --num1)
      {
        RequestedItemEntryV02 requestedItemEntryV02 = new RequestedItemEntryV02();
        requestedItemEntryV02.Load(reader.BaseStream);
        requests.Add(requestedItemEntryV02);
      }
      int num2 = reader.ReadInt32();
      rewards = new List<ItemEntryV02>();
      for (; num2 > 0; --num2)
      {
        ItemEntryV02 itemEntryV02 = new ItemEntryV02();
        itemEntryV02.Load(reader.BaseStream);
        rewards.Add(itemEntryV02);
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

    public static void AssertAreEqual(ExchangeOfferV02 expected, ExchangeOfferV02 actual)
    {
      for (int index = 0; index < expected.requests.Count; ++index)
        RequestedItemEntryV02.AssertAreEqual(expected.requests[index], actual.requests[index]);
      for (int index = 0; index < expected.rewards.Count; ++index)
        ItemEntryV02.AssertAreEqual(expected.rewards[index], actual.rewards[index]);
    }
  }
}
