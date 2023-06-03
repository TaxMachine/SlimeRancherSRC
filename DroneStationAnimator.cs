// Decompiled with JetBrains decompiler
// Type: DroneStationAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DroneStationAnimator : SRAnimator
{
  private static readonly int STATION_ENABLED = Animator.StringToHash(nameof (STATION_ENABLED));

  public void SetEnabled(bool enabled) => animator.SetBool(STATION_ENABLED, enabled);
}
