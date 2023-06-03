// Decompiled with JetBrains decompiler
// Type: DroneProgramSourceFreeRange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class DroneProgramSourceFreeRange : DroneProgramSourceDynamic
{
  protected override GardenDroneSubnetwork GetSubnetwork() => drone.network.GetContaining(source)?.subnetwork;

  protected override IEnumerable<Orientation> GetTargetOrientations(Identifiable source) => GetTargetOrientations_Gather(source.gameObject, new GatherConfig()
  {
    distanceVertical = 1.25f
  });
}
