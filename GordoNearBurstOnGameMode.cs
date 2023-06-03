// Decompiled with JetBrains decompiler
// Type: GordoNearBurstOnGameMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GordoNearBurstOnGameMode : MonoBehaviour
{
  [Tooltip("Number of eaten counts remaining on the gordo.")]
  public uint remaining = 1;

  public bool NearBurstForGameMode(PlayerState.GameMode currGameMode) => currGameMode == PlayerState.GameMode.TIME_LIMIT_V2;
}
