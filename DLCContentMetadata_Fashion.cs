// Decompiled with JetBrains decompiler
// Type: DLCContentMetadata_Fashion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "DLC", menuName = "DLC/Content/Fashion Metadata")]
public class DLCContentMetadata_Fashion : DLCContentMetadata
{
  public GameObject prefab;
  public VacItemDefinition vacItemDefinition;
  public GadgetDefinition gadgetDefinition;

  public override void Register()
  {
    SRSingleton<GameContext>.Instance.LookupDirector.RegisterFashion(prefab, vacItemDefinition, gadgetDefinition);
    SRSingleton<SceneContext>.Instance.GameModel.GetPlayerModel().RegisterPotentialAmmo(PlayerState.AmmoMode.DEFAULT, prefab);
    SRSingleton<SceneContext>.Instance.GameModel.GetGadgetsModel().RegisterBlueprint(gadgetDefinition.id);
  }
}
