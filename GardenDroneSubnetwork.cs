// Decompiled with JetBrains decompiler
// Type: GardenDroneSubnetwork
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class GardenDroneSubnetwork : PathingNetwork
{
  private const float MAX_CONNECTION_DIST = 10f;
  private DronePather pather = new DronePather(10f);

  public override Pather Pather => pather;
}
