// Decompiled with JetBrains decompiler
// Type: SlimeDefinitions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Slimes/Slime Definitions")]
public class SlimeDefinitions : ScriptableObject
{
  public SlimeDefinition[] Slimes;
  private Dictionary<Identifiable.Id, SlimeDefinition> slimeDefinitionsByIdentifiable = new Dictionary<Identifiable.Id, SlimeDefinition>(Identifiable.idComparer);
  private Dictionary<PlortPair, SlimeDefinition> largoDefinitionByBasePlorts = new Dictionary<PlortPair, SlimeDefinition>(PlortPair.EqualityComparer.Default);
  private Dictionary<SlimeDefinitionPair, SlimeDefinition> largoDefinitionByBaseDefinitions = new Dictionary<SlimeDefinitionPair, SlimeDefinition>(SlimeDefinitionPair.EqualityComparer.Default);

  public void OnEnable()
  {
    RefreshIndexes();
    RefreshDefinitions();
  }

  public void RefreshIndexes()
  {
    foreach (SlimeDefinition slime in Slimes)
    {
      try
      {
        slimeDefinitionsByIdentifiable.Add(slime.IdentifiableId, slime);
        if (slime.IsLargo)
        {
          if (slime.BaseSlimes.Length == 2)
          {
            largoDefinitionByBasePlorts.Add(new PlortPair(slime.BaseSlimes[0].Diet.Produces[0], slime.BaseSlimes[1].Diet.Produces[0]), slime);
            largoDefinitionByBaseDefinitions.Add(new SlimeDefinitionPair(slime.BaseSlimes[0], slime.BaseSlimes[1]), slime);
          }
        }
      }
      catch (Exception ex)
      {
        Log.Error("Exception caught while attempting to process slime.", "name", slime.Name, "Exception", ex.Message, "Stacktrace", ex.StackTrace.ToString());
      }
    }
  }

  public void RefreshDefinitions()
  {
    foreach (SlimeDefinition slime in Slimes)
      slime.Diet.RefreshEatMap(this, slime);
  }

  public SlimeDefinition GetLargoByPlorts(Identifiable.Id plort1, Identifiable.Id plort2)
  {
    SlimeDefinition largoByPlorts = null;
    largoDefinitionByBasePlorts.TryGetValue(new PlortPair(plort1, plort2), out largoByPlorts);
    return largoByPlorts;
  }

  public SlimeDefinition GetLargoByBaseSlimes(
    SlimeDefinition slimeDefinition1,
    SlimeDefinition slimeDefinition2)
  {
    SlimeDefinition largoByBaseSlimes = null;
    largoDefinitionByBaseDefinitions.TryGetValue(new SlimeDefinitionPair(slimeDefinition1, slimeDefinition2), out largoByBaseSlimes);
    return largoByBaseSlimes;
  }

  public SlimeDefinition GetSlimeByIdentifiableId(Identifiable.Id id)
  {
    SlimeDefinition byIdentifiableId = null;
    slimeDefinitionsByIdentifiable.TryGetValue(id, out byIdentifiableId);
    return byIdentifiableId;
  }

  private struct PlortPair
  {
    public Identifiable.Id Plort1;
    public Identifiable.Id Plort2;

    public PlortPair(Identifiable.Id plort1, Identifiable.Id plort2)
    {
      if (plort1 <= plort2)
      {
        Plort1 = plort1;
        Plort2 = plort2;
      }
      else
      {
        Plort1 = plort2;
        Plort2 = plort1;
      }
    }

    public class EqualityComparer : IEqualityComparer<PlortPair>
    {
      public static EqualityComparer Default = new EqualityComparer();

      public bool Equals(PlortPair x, PlortPair y) => x.Plort1 == y.Plort1 && x.Plort2 == y.Plort2;

      public int GetHashCode(PlortPair obj) => (int) (((int) obj.Plort1 << 5) + obj.Plort1 ^ obj.Plort2);
    }
  }

  private struct SlimeDefinitionPair
  {
    public SlimeDefinition SlimeDefinition1;
    public SlimeDefinition SlimeDefinition2;

    public SlimeDefinitionPair(SlimeDefinition slimeDefinition1, SlimeDefinition slimeDefinition2)
    {
      if (slimeDefinition1.GetHashCode() <= slimeDefinition2.GetHashCode())
      {
        SlimeDefinition1 = slimeDefinition1;
        SlimeDefinition2 = slimeDefinition2;
      }
      else
      {
        SlimeDefinition1 = slimeDefinition1;
        SlimeDefinition2 = slimeDefinition2;
      }
    }

    public class EqualityComparer : IEqualityComparer<SlimeDefinitionPair>
    {
      public static EqualityComparer Default = new EqualityComparer();

      public bool Equals(
        SlimeDefinitionPair x,
        SlimeDefinitionPair y)
      {
        return x.SlimeDefinition1 == y.SlimeDefinition1 && x.SlimeDefinition2 == y.SlimeDefinition2;
      }

      public int GetHashCode(SlimeDefinitionPair obj) => (obj.SlimeDefinition1.GetHashCode() << 5) + obj.SlimeDefinition1.GetHashCode() ^ obj.SlimeDefinition2.GetHashCode();
    }
  }
}
