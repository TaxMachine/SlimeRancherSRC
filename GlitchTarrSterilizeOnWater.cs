// Decompiled with JetBrains decompiler
// Type: GlitchTarrSterilizeOnWater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class GlitchTarrSterilizeOnWater : TarrSterilizeOnWater, DestroyAfterTimeListener
{
  private RegionMember regionMember;
  private float multiplyChance;

  public override void Awake()
  {
    base.Awake();
    regionMember = GetComponent<RegionMember>();
    multiplyChance = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.tarrBaseMultiplyChance;
  }

  public override void Start()
  {
    base.Start();
    destroyer.SetDeathTime(timeDir.HoursFromNow(SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.tarrLifetime.GetRandom() * 0.0166666675f));
  }

  public void WillDestroyAfterTime()
  {
    if (sterilized || !Randoms.SHARED.GetProbability(multiplyChance))
      return;
    LookupDirector lookupDirector = SRSingleton<GameContext>.Instance.LookupDirector;
    GlitchMetadata glitch = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    GameObject prefab = lookupDirector.GetPrefab(Identifiable.Id.GLITCH_TARR_SLIME);
    for (int index = 0; index < Mathf.RoundToInt(glitch.tarrMultiplyCount.GetRandom()); ++index)
    {
      GameObject gameObject = SRBehaviour.InstantiateActor(prefab, regionMember.setId, this.gameObject.transform.position + Random.insideUnitSphere * 2f, Quaternion.identity);
      gameObject.GetComponent<GlitchTarrSterilizeOnWater>().multiplyChance = multiplyChance * (1f - glitch.tarrMultiplyChanceDegradation);
      gameObject.GetComponent<Rigidbody>().velocity = (Quaternion.Euler(new Vector2(-45f, 30f).GetRandom(), new Vector2(0.0f, 360f).GetRandom(), 0.0f) * gameObject.transform.forward).normalized * 15f;
      float fromValue = gameObject.transform.localScale.x * 0.2f;
      gameObject.transform.DOScale(gameObject.transform.localScale, 0.2f).From(fromValue).SetEase(Ease.Linear);
    }
  }
}
