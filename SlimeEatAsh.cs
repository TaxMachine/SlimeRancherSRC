// Decompiled with JetBrains decompiler
// Type: SlimeEatAsh
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using MonomiPark.SlimeRancher.Regions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEatAsh : SRBehaviour
{
  [Tooltip("The plort to produce when we eat.")]
  public GameObject plort;
  public GameObject produceFX;
  public float eatRate = 3f;
  private SlimeEmotions emotions;
  private SlimeEat slimeEat;
  private RegionMember regionMember;
  private float nextChompTime;
  private HashSet<AshSource> ashes = new HashSet<AshSource>();
  private SlimeAudio slimeAudio;
  private static readonly Vector3 LOCAL_PRODUCE_LOC = new Vector3(0.0f, 0.5f, 0.0f);
  private static readonly Vector3 LOCAL_PRODUCE_VEL = new Vector3(0.0f, 1f, 0.0f);
  private const float PRODUCE_SCALE_UP_TIME = 0.5f;

  public void Awake()
  {
    emotions = GetComponent<SlimeEmotions>();
    slimeEat = GetComponent<SlimeEat>();
    slimeAudio = GetComponent<SlimeAudio>();
    regionMember = GetComponent<RegionMember>();
    ResetEatClock();
  }

  public void Update()
  {
    if (ashes.Count <= 0 || Time.time < (double) nextChompTime || emotions.GetCurr(SlimeEmotions.Emotion.HUNGER) <= (double) slimeEat.minDriveToEat)
      return;
    AshSource ashSource = Randoms.SHARED.Pick(ashes, null);
    if (!ashSource.Available())
      return;
    ashSource.ConsumeAsh();
    StartCoroutine(ProduceAfterDelay(1, plort, 2f));
    OnEat(SlimeEmotions.Emotion.HUNGER, Identifiable.Id.NONE);
  }

  public void OnCollisionEnter(Collision col)
  {
    AshSource component = col.gameObject.GetComponent<AshSource>();
    if (!(component != null))
      return;
    ashes.Add(component);
  }

  public void OnCollisionExit(Collision col)
  {
    AshSource component = col.gameObject.GetComponent<AshSource>();
    if (!(component != null))
      return;
    ashes.Remove(component);
  }

  public void ResetEatClock() => nextChompTime = Time.time + eatRate;

  private void OnEat(SlimeEmotions.Emotion driver, Identifiable.Id otherId)
  {
    ResetEatClock();
    emotions.Adjust(driver, -slimeEat.drivePerEat);
    if (otherId == Identifiable.Id.PLAYER)
      return;
    emotions.Adjust(SlimeEmotions.Emotion.AGITATION, -slimeEat.agitationPerEat);
  }

  private IEnumerator ProduceAfterDelay(int count, GameObject produces, float delay)
  {
    SlimeEatAsh slimeEatAsh = this;
    yield return new WaitForSeconds(delay);
    if (slimeEatAsh.gameObject != null)
    {
      for (int index = 0; index < count; ++index)
      {
        Vector3 position = slimeEatAsh.transform.TransformPoint(LOCAL_PRODUCE_LOC);
        Vector3 vector3 = slimeEatAsh.transform.TransformVector(LOCAL_PRODUCE_VEL);
        if (slimeEatAsh.produceFX != null)
          SpawnAndPlayFX(slimeEatAsh.produceFX, position, slimeEatAsh.transform.rotation);
        GameObject gameObject = InstantiateActor(produces, slimeEatAsh.regionMember.setId, position, slimeEatAsh.transform.rotation);
        Rigidbody component1 = gameObject.GetComponent<Rigidbody>();
        if (component1 != null)
          component1.velocity = vector3;
        PlortInvulnerability component2 = gameObject.GetComponent<PlortInvulnerability>();
        if (component2 != null)
          component2.GoInvulnerable();
        gameObject.transform.DOScale(gameObject.transform.localScale, 0.5f).From(1f / 1000f);
      }
      slimeEatAsh.slimeAudio.Play(slimeEatAsh.slimeAudio.slimeSounds.plortCue);
    }
  }
}
