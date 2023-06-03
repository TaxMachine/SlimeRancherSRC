// Decompiled with JetBrains decompiler
// Type: SlimeDiet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SlimeDiet
{
  public SlimeEat.FoodGroup[] MajorFoodGroups;
  public Identifiable.Id[] Favorites;
  public Identifiable.Id[] AdditionalFoods;
  public Identifiable.Id[] Produces;
  public int FavoriteProductionCount;
  [HideInInspector]
  public List<EatMapEntry> EatMap;

  public static SlimeDiet Combine(SlimeDiet diet1, SlimeDiet diet2) => new SlimeDiet()
  {
    MajorFoodGroups = diet1.MajorFoodGroups.Union(diet2.MajorFoodGroups).ToArray(),
    Favorites = diet1.Favorites.Union(diet2.Favorites).ToArray(),
    AdditionalFoods = diet1.AdditionalFoods.Union(diet2.AdditionalFoods).ToArray(),
    Produces = diet1.Produces.Union(diet2.Produces).ToArray(),
    FavoriteProductionCount = diet1.FavoriteProductionCount
  };

  public IEnumerable<Identifiable.Id> GetDietIdentifiableIds() => new HashSet<Identifiable.Id>(MajorFoodGroups.SelectMany(group => SlimeEat.GetFoodGroupIds(group)).Concat(AdditionalFoods)).AsEnumerable();

  public void RefreshEatMap(SlimeDefinitions definitions, SlimeDefinition definition)
  {
    EatMap = new List<EatMapEntry>();
    foreach (Identifiable.Id dietIdentifiableId in GetDietIdentifiableIds())
    {
      SlimeEmotions.Emotion emotion = dietIdentifiableId != Identifiable.Id.SPICY_TOFU ? SlimeEmotions.Emotion.HUNGER : SlimeEmotions.Emotion.NONE;
      foreach (Identifiable.Id produce in Produces)
        EatMap.Add(new EatMapEntry()
        {
          eats = dietIdentifiableId,
          producesId = produce,
          isFavorite = Favorites.Contains(dietIdentifiableId),
          favoriteProductionCount = FavoriteProductionCount,
          driver = emotion,
          minDrive = 0.0f,
          extraDrive = 0.0f,
          becomesId = Identifiable.Id.NONE
        });
    }
    if (!ProducePlorts() || !definition.IsLargo && !definition.CanLargofy)
      return;
    foreach (Identifiable.Id plort1 in SlimeEat.GetFoodGroupIds(SlimeEat.FoodGroup.PLORTS).Where(id => id != Identifiable.Id.QUICKSILVER_PLORT))
    {
      if (!Produces.Contains(plort1))
      {
        Identifiable.Id id;
        if (definition.IsLargo)
        {
          id = Identifiable.Id.TARR_SLIME;
        }
        else
        {
          SlimeDefinition largoByPlorts = definitions.GetLargoByPlorts(plort1, Produces[0]);
          if (!(largoByPlorts == null))
            id = largoByPlorts.IdentifiableId;
          else
            continue;
        }
        EatMap.Add(new EatMapEntry()
        {
          eats = plort1,
          producesId = Identifiable.Id.NONE,
          isFavorite = false,
          favoriteProductionCount = FavoriteProductionCount,
          driver = SlimeEmotions.Emotion.AGITATION,
          minDrive = 0.5f,
          extraDrive = 0.0f,
          becomesId = id
        });
      }
    }
  }

  public void AddEatMapEntries(Identifiable.Id id, IList<EatMapEntry> targetEntries)
  {
    for (int index = 0; index < EatMap.Count; ++index)
    {
      if (EatMap[index].eats == id)
        targetEntries.Add(EatMap[index]);
    }
  }

  private bool ProducePlorts() => Produces.Count(id => Identifiable.IsPlort(id)) > 0;

  public static string GetFoodCategoryMsg(Identifiable.Id id)
  {
    if (Array.IndexOf(SlimeEat.GetFoodGroupIds(SlimeEat.FoodGroup.VEGGIES), id) != -1)
      return "m.foodgroup.veggies";
    if (Array.IndexOf(SlimeEat.GetFoodGroupIds(SlimeEat.FoodGroup.FRUIT), id) != -1)
      return "m.foodgroup.fruit";
    if (Array.IndexOf(SlimeEat.GetFoodGroupIds(SlimeEat.FoodGroup.MEAT), id) != -1)
      return "m.foodgroup.meat";
    return Array.IndexOf(SlimeEat.GetFoodGroupIds(SlimeEat.FoodGroup.GINGER), id) != -1 ? "m.foodgroup.ginger" : "m.foodgroup.none";
  }

  public string GetDirectFoodGroupsMsg() => GetGroupsMsg(MajorFoodGroups);

  public string GetModulesFoodGroupsMsg()
  {
    HashSet<SlimeEat.FoodGroup> groups = new HashSet<SlimeEat.FoodGroup>();
    foreach (SlimeEat.FoodGroup majorFoodGroup in MajorFoodGroups)
      groups.Add(majorFoodGroup);
    return GetGroupsMsg(groups);
  }

  private string GetGroupsMsg(ICollection<SlimeEat.FoodGroup> groups)
  {
    switch (groups.Count)
    {
      case 0:
        return "m.foodgroup.none";
      case 1:
        return "m.foodgroup." + Enum.GetName(typeof (SlimeEat.FoodGroup), groups.First()).ToLowerInvariant();
      case 3:
        return "m.foodgroup.all";
      default:
        string[] strArray = new string[groups.Count];
        int num = 0;
        foreach (int group in groups)
        {
          string lowerInvariant = Enum.GetName(typeof (SlimeEat.FoodGroup), (SlimeEat.FoodGroup) group).ToLowerInvariant();
          strArray[num++] = "m.foodgroup." + lowerInvariant;
        }
        return MessageUtil.Compose("m.andlist" + groups.Count, strArray);
    }
  }

  public class EatMapEntry
  {
    public Identifiable.Id eats;
    public bool isFavorite;
    public int favoriteProductionCount = 2;
    public Identifiable.Id producesId;
    public Identifiable.Id becomesId;
    public SlimeEmotions.Emotion driver;
    public float extraDrive;
    public float minDrive;

    public int NumToProduce() => !isFavorite ? 1 : favoriteProductionCount;
  }
}
