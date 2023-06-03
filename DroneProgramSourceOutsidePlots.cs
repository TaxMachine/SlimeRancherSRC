// Decompiled with JetBrains decompiler
// Type: DroneProgramSourceOutsidePlots
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class DroneProgramSourceOutsidePlots : DroneProgramSourceDynamic
{
  protected override bool SourcePredicate(
    DroneNetwork.LandPlotMetadata metadata,
    Identifiable source)
  {
    return metadata == null && base.SourcePredicate(metadata, source);
  }
}
