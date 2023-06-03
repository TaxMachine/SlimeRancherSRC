// Decompiled with JetBrains decompiler
// Type: PollenCloudController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using UnityEngine;

public class PollenCloudController : SRBehaviour
{
  public float pctGrowthPerGameHour = 1f;
  public float startGrowthAgitation = 0.75f;
  public float maxCloudScale = 5f;
  public GameObject cloudActorPrefab;
  private PollenCloudMarker cloud;
  private SlimeEmotions emotions;
  private RegionMember regionMember;
  private TimeDirector timeDir;
  private float growthFactor;
  private float pctGrowthPerGameSec;
  private const float CLOUD_SPEED = 1f;
  private const float RELEASE_CUTOFF = 0.95f;

  public void Awake()
  {
    emotions = GetComponent<SlimeEmotions>();
    regionMember = GetComponent<RegionMember>();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    growthFactor = (float) (1.0 / (1.0 - startGrowthAgitation));
    pctGrowthPerGameSec = pctGrowthPerGameHour * 0.000277777785f;
  }

  public void Start() => cloud = GetComponentInChildren<PollenCloudMarker>(true);

  public void Update()
  {
    float a = ScaleForAgitation(emotions.GetCurr(SlimeEmotions.Emotion.AGITATION));
    float num = cloud.gameObject.activeSelf ? cloud.transform.localScale.x / maxCloudScale : 0.0f;
    if (num > (double) a)
      num = Mathf.Max(a, num - (float) timeDir.DeltaWorldTime() * pctGrowthPerGameSec);
    else if (num < (double) a)
      num = Mathf.Min(a, num + (float) timeDir.DeltaWorldTime() * pctGrowthPerGameSec);
    if (num >= 0.949999988079071)
    {
      InstantiateActor(cloudActorPrefab, regionMember.setId, transform.position, transform.rotation).GetComponent<Rigidbody>().velocity = transform.forward * 1f;
      num = 0.0f;
    }
    cloud.transform.localScale = Vector3.one * (maxCloudScale * num);
    cloud.gameObject.SetActive(num > 0.0);
  }

  private float ScaleForAgitation(float agitation) => Math.Max(0.0f, (agitation - startGrowthAgitation) * growthFactor);
}
