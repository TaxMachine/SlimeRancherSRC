// Decompiled with JetBrains decompiler
// Type: SlimeLineup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlimeLineup : MonoBehaviour
{
  public LookupDirector lookupDirector;
  public SlimeDefinitions slimeDefinitions;
  public Vector2 spacing;
  private List<GameObject> slimePreviews = new List<GameObject>();
  private static readonly Type[] componentBlacklist = new Type[12]
  {
    typeof (SlimeSubbehaviourPlexer),
    typeof (SlimeSubbehaviour),
    typeof (DestroyOnTouching),
    typeof (DestroyAfterTime),
    typeof (GlitchTarrSterilizeOnWater),
    typeof (GlintController),
    typeof (SlimeHealth),
    typeof (DestroyOutsideHoursOfDay),
    typeof (MaybeCullOnReenable),
    typeof (DamagePlayerOnTouch),
    typeof (FireSlimeIgnition),
    typeof (RegionMember)
  };

  public void ShowSlime(SlimeDefinition slimeDefinition) => CreateSlimePreviews(new List<SlimeDefinition>(new SlimeDefinition[1]
  {
    slimeDefinition
  }));

  public void ShowSlimeAndLargos(SlimeDefinition slimeDefinition)
  {
    List<SlimeDefinition> definitions = new List<SlimeDefinition>(new SlimeDefinition[1]
    {
      slimeDefinition
    });
    definitions.AddRange(slimeDefinitions.Slimes.Where(slime => slime.BaseSlimes.Contains(slimeDefinition)));
    CreateSlimePreviews(definitions);
  }

  public void ShowAllBaseSlimes() => CreateSlimePreviews(slimeDefinitions.Slimes.Where(slime => !slime.IsLargo).ToList());

  public void ShowAllSlimes() => CreateSlimePreviews(new List<SlimeDefinition>(slimeDefinitions.Slimes));

  private void CreateSlimePreviews(List<SlimeDefinition> definitions)
  {
    ClearPreviews();
    int num1 = Mathf.CeilToInt(Mathf.Sqrt(definitions.Count));
    float num2 = spacing.x * (num1 / 2f);
    float num3 = spacing.y * (num1 / 2f);
    for (int index = 0; index < definitions.Count; ++index)
    {
      Vector3 position = new Vector3(index % num1 * spacing.x - num2 + transform.position.x, transform.position.y, index / num1 * spacing.y - num3 + transform.position.z);
      slimePreviews.Add(CreatePreviewSlime(definitions[index], position));
    }
  }

  private GameObject CreatePreviewSlime(SlimeDefinition slimeDefinition, Vector3 position)
  {
    GameObject previewSlime = SRSingleton<SceneContext>.Instance.GameModel.InstantiateActor(lookupDirector.GetPrefab(slimeDefinition.IdentifiableId), SRSingleton<SceneContext>.Instance.RegionRegistry.GetCurrentRegionSetId(), position, Quaternion.Euler(0.0f, 180f, 0.0f));
    foreach (Type type in componentBlacklist)
    {
      foreach (UnityEngine.Object component in previewSlime.GetComponents(type))
        Destroy(component);
    }
    return previewSlime;
  }

  private void ClearPreviews()
  {
    foreach (UnityEngine.Object slimePreview in slimePreviews)
      Destroy(slimePreview);
    slimePreviews.Clear();
  }
}
