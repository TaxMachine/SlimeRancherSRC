// Decompiled with JetBrains decompiler
// Type: SlimeDefinition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Slimes/Slime Definition")]
public class SlimeDefinition : ScriptableObject
{
  public string Name;
  public Identifiable.Id IdentifiableId;
  public SlimeDefinition[] BaseSlimes;
  public bool IsLargo;
  public bool CanLargofy;
  public float PrefabScale;
  [Tooltip("Default slime appearances.")]
  [FormerlySerializedAs("Appearances")]
  public SlimeAppearance[] AppearancesDefault;
  [NonSerialized]
  public List<SlimeAppearance> AppearancesDynamic = new List<SlimeAppearance>();
  public SlimeSounds Sounds;
  public SlimeDiet Diet;
  public Identifiable.Id[] FavoriteToys;
  public GameObject BaseModule;
  public GameObject[] SlimeModules;

  public IEnumerable<SlimeAppearance> Appearances => AppearancesDefault.Concat(AppearancesDynamic);

  public void LoadDietFromBaseSlimes()
  {
    if (!IsLargo || BaseSlimes.Length != 2)
      Log.Warning("Can't load diet. Slime is either not a largo or does not have two base slimes.", "name", Name);
    else
      Diet = SlimeDiet.Combine(BaseSlimes[0].Diet, BaseSlimes[1].Diet);
  }

  public void LoadFavoriteToysFromBaseSlimes()
  {
    if (!IsLargo || BaseSlimes.Length != 2)
      Log.Warning("Can't load favorite toys. Slime is either not a largo or does not have two base slimes.", "name", Name);
    else
      FavoriteToys = BaseSlimes[0].FavoriteToys.Union(BaseSlimes[1].FavoriteToys).ToArray();
  }

  public void RegisterDynamicAppearance(SlimeAppearance appearance)
  {
    if (AppearancesDynamic.Contains(appearance))
      return;
    AppearancesDynamic.Add(appearance);
    SRSingleton<SceneContext>.Instance.SlimeAppearanceDirector.RegisterDependentAppearances(this, appearance);
  }

  public SlimeAppearance GetAppearanceForSet(SlimeAppearance.AppearanceSaveSet set) => Appearances.FirstOrDefault(appearance => appearance.SaveSet == set);
}
