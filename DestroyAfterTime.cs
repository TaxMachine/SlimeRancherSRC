// Decompiled with JetBrains decompiler
// Type: DestroyAfterTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class DestroyAfterTime : RegisteredActorBehaviour, RegistryUpdateable
{
  public float lifeTimeHours = 72f;
  public float nightLifeTimeFactor = 1f;
  public GameObject destroyFX;
  public bool destroyAsActor = true;
  private TimeDirector timeDir;
  private double deathTime;
  private bool scaleDownOnDestroy;
  private SECTR_AudioCue scaleDownCue;
  private bool fizzleParticlesOnDestroy;
  private bool destroying;
  private DestroyAfterTimeCondition destroyAfterTimeCondition;
  private bool hasDestroyAfterTimeCondition;
  private const float SCALE_DOWN_TIME = 4f;
  private const float FIZZLE_TIME = 4f;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    float num = timeDir.CurrHourOrStart();
    deathTime = timeDir.HoursFromNowOrStart((num < 6.0 || num > 18.0 ? nightLifeTimeFactor : 1f) * lifeTimeHours);
    destroyAfterTimeCondition = GetComponent<DestroyAfterTimeCondition>();
    hasDestroyAfterTimeCondition = destroyAfterTimeCondition != null;
  }

  public void RegistryUpdate()
  {
    if (!timeDir.HasReached(deathTime) || destroying || hasDestroyAfterTimeCondition && !destroyAfterTimeCondition.ReadyToDestroy())
      return;
    destroying = true;
    int num = timeDir.HasReached(deathTime + 3600.0) ? 1 : 0;
    GetComponent<DestroyAfterTimeListener>()?.WillDestroyAfterTime();
    if (num != 0)
    {
      DoDestroy("DestroyAfterTime.RegistryUpdate (skippedFX)");
    }
    else
    {
      if (scaleDownOnDestroy)
        StartCoroutine(ScaleThenDestroy());
      else if (fizzleParticlesOnDestroy)
        StartCoroutine(FizzleThenDestroy());
      else
        DoDestroy("DestroyAfterTime.RegistryUpdate");
      if (!(destroyFX != null))
        return;
      SpawnAndPlayFX(destroyFX, transform.position, Quaternion.identity);
    }
  }

  private void DoDestroy(string reason)
  {
    if (destroyAsActor)
      Destroyer.DestroyActor(gameObject, reason);
    else
      Destroyer.Destroy(gameObject, reason);
  }

  private IEnumerator FizzleThenDestroy()
  {
    DestroyAfterTime destroyAfterTime = this;
    foreach (ParticleSystem componentsInChild in destroyAfterTime.GetComponentsInChildren<ParticleSystem>())
      componentsInChild.Stop();
    yield return new WaitForSeconds(4f);
    destroyAfterTime.DoDestroy("DestroyAfterTime.FizzleThenDestroy");
  }

  private IEnumerator ScaleThenDestroy()
  {
    DestroyAfterTime destroyAfterTime = this;
    if (destroyAfterTime.scaleDownCue != null)
      SECTR_AudioSystem.Play(destroyAfterTime.scaleDownCue, destroyAfterTime.transform, Vector3.zero, false);
    destroyAfterTime.TweenScaleDownItem(destroyAfterTime.gameObject);
    yield return new WaitForSeconds(4f);
    destroyAfterTime.DoDestroy("DestroyAfterTime.ScaleThenDestroy");
  }

  private void TweenScaleDownItem(GameObject obj) => TweenUtil.ScaleOut(obj, 4f);

  public void AdvanceHours(float hours) => deathTime -= hours * 3600.0;

  public void MultiplyRemainingHours(float factor)
  {
    double num = deathTime - timeDir.WorldTime();
    deathTime = timeDir.WorldTime() + num * factor;
  }

  public void SetDeathTime(double time) => deathTime = time;

  public void ScaleDownOnDestroy() => scaleDownOnDestroy = true;

  public void SetScaleDownCue(SECTR_AudioCue cue) => scaleDownCue = cue;

  public void FizzleParticlesOnDestroy() => fizzleParticlesOnDestroy = true;

  public double GetDeathTime() => deathTime;
}
