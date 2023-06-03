// Decompiled with JetBrains decompiler
// Type: DestroyPlortAfterTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class DestroyPlortAfterTime : 
  RegisteredActorBehaviour,
  RegistryUpdateable,
  ActorModel.Participant
{
  public float lifeTimeHours = 24f;
  public GameObject destroyFX;
  private TimeDirector timeDir;
  private bool destroying;
  private PlortModel plortModel;

  public void Awake() => timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;

  public void InitModel(ActorModel model) => ((PlortModel) model).destroyTime = timeDir.HoursFromNowOrStart(lifeTimeHours);

  public void SetModel(ActorModel model) => plortModel = (PlortModel) model;

  public void RegistryUpdate()
  {
    if (!timeDir.HasReached(plortModel.destroyTime) || destroying)
      return;
    destroying = true;
    int num = timeDir.HasReached(plortModel.destroyTime + 3600.0) ? 1 : 0;
    GetComponent<DestroyAfterTimeListener>()?.WillDestroyAfterTime();
    if (num != 0)
    {
      DoDestroy("DestroyAfterTime.RegistryUpdate (skippedFX)");
    }
    else
    {
      DoDestroy("DestroyAfterTime.RegistryUpdate");
      if (!(destroyFX != null))
        return;
      SpawnAndPlayFX(destroyFX, transform.position, Quaternion.identity);
    }
  }

  private void DoDestroy(string reason) => Destroyer.DestroyActor(gameObject, reason);
}
