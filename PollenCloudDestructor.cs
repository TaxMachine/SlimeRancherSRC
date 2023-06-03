// Decompiled with JetBrains decompiler
// Type: PollenCloudDestructor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PollenCloudDestructor : RegisteredActorBehaviour, RegistryUpdateable
{
  public float gameHrsToLive = 0.5f;
  public float gameHrsInContactBeforeDeath = 0.05f;
  public GameObject destroyFX;
  private double dieAtTime;
  private double contactDeathTime = double.PositiveInfinity;
  private int contacts;
  private TimeDirector timeDir;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    dieAtTime = timeDir.HoursFromNow(gameHrsToLive);
  }

  public void RegistryUpdate()
  {
    if (!timeDir.HasReached(dieAtTime) && !timeDir.HasReached(contactDeathTime))
      return;
    Destroyer.DestroyActor(gameObject, "PollenCloudDestructor.RegistryUpdate");
    SpawnAndPlayFX(destroyFX, transform.position, transform.rotation);
  }

  public void AddContact()
  {
    if (contacts == 0)
      contactDeathTime = timeDir.HoursFromNow(gameHrsInContactBeforeDeath);
    ++contacts;
  }

  public void RemoveContact()
  {
    --contacts;
    if (contacts > 0)
      return;
    contactDeathTime = double.PositiveInfinity;
  }

  public override void OnDestroy() => base.OnDestroy();
}
