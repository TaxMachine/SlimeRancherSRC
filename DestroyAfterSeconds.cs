// Decompiled with JetBrains decompiler
// Type: DestroyAfterSeconds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DestroyAfterSeconds : SRBehaviour
{
  public float time;
  private float awakeTime;

  public void Awake() => awakeTime = Time.time;

  public void Update()
  {
    if (Time.time < awakeTime + (double) time)
      return;
    Destroyer.DestroyActor(gameObject, "DestroyAfterSeconds.Update");
  }
}
