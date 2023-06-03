// Decompiled with JetBrains decompiler
// Type: DLCContentMetadata_SlimeAppearance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "DLC", menuName = "DLC/Content/Slime Appearance Metadata")]
public class DLCContentMetadata_SlimeAppearance : DLCContentMetadata
{
  [Tooltip("SlimeDefinition to add the appearance to.")]
  public SlimeDefinition definition;
  [Tooltip("SlimeAppearance to add to the definition.")]
  public SlimeAppearance appearance;

  public override void Register() => definition.RegisterDynamicAppearance(appearance);
}
