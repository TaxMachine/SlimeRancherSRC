// Decompiled with JetBrains decompiler
// Type: DLCContentMetadata_ChromaPack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "DLC", menuName = "DLC/Content/Chroma Pack Metadata")]
public class DLCContentMetadata_ChromaPack : DLCContentMetadata
{
  public RanchDirector.PaletteEntry paletteEntry;

  public override void Register() => SRSingleton<SceneContext>.Instance.RanchDirector.RegisterPalette(paletteEntry);
}
