// Decompiled with JetBrains decompiler
// Type: FireColumn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireColumn : SRBehaviour
{
  public FireballEntry[] fireballs;
  public Transform fireballParent;
  public GameObject fireEffectObj;
  public float minFireballDelay = 10f;
  public float maxFireballDelay = 20f;
  public float lifetimeHrs = 0.5f;
  public SECTR_AudioCue columnSpawnCue;
  public SECTR_AudioCue columnFireLoopCue;
  private SECTR_AudioCueInstance columnFireLookCueInstance;
  private bool fireActive;
  private double nextFireballTime;
  private double endOfLifeTime;
  private Dictionary<int, float> fireballEntryIdxWeightMap = new Dictionary<int, float>();
  private Dictionary<int, float> hibernatingFireballEntryIdxWeightMap = new Dictionary<int, float>();
  private TimeDirector timeDir;
  private Region region;
  private Collider columnCollider;
  private bool isInOasis;
  private bool deactivating;
  private const float FIZZLE_TIME = 2f;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    region = GetComponentInParent<Region>();
    for (int key = 0; key < fireballs.Length; ++key)
    {
      fireballEntryIdxWeightMap.Add(key, fireballs[key].weight);
      RegionMember component = fireballs[key].prefab.GetComponent<RegionMember>();
      if (component == null || !component.canHibernate)
        hibernatingFireballEntryIdxWeightMap.Add(key, fireballs[key].weight);
    }
    columnCollider = GetComponent<Collider>();
    columnCollider.enabled = false;
  }

  public void OnTriggerEnter(Collider col)
  {
    if (!fireActive)
      return;
    col.GetComponent<Ignitable>()?.Ignite(gameObject);
  }

  public void ActivateFire()
  {
    if (!isActiveAndEnabled || fireActive || deactivating)
      return;
    SECTR_AudioSystem.Play(columnSpawnCue, transform.position, false);
    columnFireLookCueInstance = SECTR_AudioSystem.Play(columnFireLoopCue, transform.position, true);
    fireActive = true;
    fireEffectObj.SetActive(true);
    nextFireballTime = timeDir.WorldTime() + Randoms.SHARED.GetInRange(0.0f, minFireballDelay);
    endOfLifeTime = timeDir.HoursFromNow(lifetimeHrs);
    columnCollider.enabled = true;
  }

  public void DeactivateFire()
  {
    if (!isActiveAndEnabled || !fireActive)
      return;
    deactivating = true;
    fireActive = false;
    StartCoroutine(FizzleThenDeactivateFireParticles());
    columnFireLookCueInstance.Stop(true);
    columnCollider.enabled = false;
  }

  public void OnDisable()
  {
    if (!deactivating)
      return;
    FinishDeactivate();
  }

  private IEnumerator FizzleThenDeactivateFireParticles()
  {
    foreach (ParticleSystem componentsInChild in fireEffectObj.GetComponentsInChildren<ParticleSystem>())
      componentsInChild.Stop();
    yield return new WaitForSeconds(2f);
    FinishDeactivate();
  }

  private void FinishDeactivate()
  {
    fireEffectObj.SetActive(false);
    foreach (ParticleSystem componentsInChild in fireEffectObj.GetComponentsInChildren<ParticleSystem>())
      componentsInChild.Play();
    deactivating = false;
  }

  public bool IsFireActive() => fireActive;

  public void Update()
  {
    if (!fireActive)
      return;
    if (timeDir.HasReached(nextFireballTime))
    {
      SpawnFireball();
      nextFireballTime = timeDir.WorldTime() + Randoms.SHARED.GetInRange(minFireballDelay, maxFireballDelay);
    }
    if (!timeDir.HasReached(endOfLifeTime))
      return;
    DeactivateFire();
  }

  private void SpawnFireball()
  {
    Dictionary<int, float> weightMap = IsHibernating() ? hibernatingFireballEntryIdxWeightMap : fireballEntryIdxWeightMap;
    if (weightMap.Count <= 0)
      return;
    int index = Randoms.SHARED.Pick(weightMap, -1);
    if (index < 0)
      return;
    FireballEntry fireball = fireballs[index];
    (!(fireball.prefab.GetComponent<Identifiable>() == null) ? InstantiateActor(fireball.prefab, region.setId, transform.position + transform.up * Randoms.SHARED.GetInRange(fireball.minBallHeight, fireball.maxBallHeight), Quaternion.identity) : InstantiatePooledDynamic(fireball.prefab, transform.position + transform.up * Randoms.SHARED.GetInRange(fireball.minBallHeight, fireball.maxBallHeight), Quaternion.identity)).GetComponent<Rigidbody>().AddForce(UnityEngine.Random.onUnitSphere * Randoms.SHARED.GetInRange(fireball.minBallEjectForce, fireball.maxBallEjectForce));
  }

  public void NoteInOasis() => isInOasis = true;

  public bool IsInOasis() => isInOasis;

  private bool IsHibernating() => region != null && region.Hibernated;

  [Serializable]
  public class FireballEntry
  {
    public GameObject prefab;
    public float weight;
    public float minBallHeight;
    public float maxBallHeight;
    public float minBallEjectForce;
    public float maxBallEjectForce;
  }
}
