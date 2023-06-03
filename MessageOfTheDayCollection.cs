// Decompiled with JetBrains decompiler
// Type: MessageOfTheDayCollection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Services/MessageOfTheDayCollection")]
[Serializable]
public class MessageOfTheDayCollection : ScriptableObject
{
  public List<BundledMessageOfTheDay> messages;

  public BundledMessageOfTheDay GetRandomMessage() => Randoms.SHARED.Pick(messages, null);

  public BundledMessageOfTheDay GetRandomMessage(Predicate<BundledMessageOfTheDay> messageFilter) => Randoms.SHARED.Pick(messages.Where(msg => messageFilter(msg)), null);
}
