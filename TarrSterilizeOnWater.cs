// Decompiled with JetBrains decompiler
// Type: TarrSterilizeOnWater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TarrSterilizeOnWater : MonoBehaviour, LiquidConsumer
{
  private static readonly int ColorRampPropertyId = Shader.PropertyToID("_ColorRamp");
  public float timeRemainingFactor = 0.5f;
  public float hoursPerHit = 0.1f;
  public Texture sterileRampTex;
  protected bool sterilized;
  protected DestroyAfterTime destroyer;
  private SlimeEat slimeEat;
  private Material sterileMat;
  protected TimeDirector timeDir;
  private int inWaterCount;
  private double nextInWaterCheck;
  private float flashFactor;
  private const float TIME_BETWEEN_CHECKS = 0.0166666675f;
  private const float FLASH_DECAY_PER_SEC = 2f;
  private List<Renderer> flashRenderers;

  public virtual void Awake()
  {
    destroyer = GetComponent<DestroyAfterTime>();
    slimeEat = GetComponent<SlimeEat>();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    GetComponent<SlimeAppearanceApplicator>().OnAppearanceChanged += CollectFlashRenderers;
  }

  public virtual void Start() => CollectFlashRenderers(null);

  private void CollectFlashRenderers(SlimeAppearance newAppearance) => flashRenderers = GetComponentsInChildren<TarrFlashMarker>().Select(marker => marker.GetComponent<Renderer>()).ToList();

  public void OnTriggerEnter(Collider col)
  {
    LiquidSource component = col.GetComponent<LiquidSource>();
    if (component != null && Identifiable.IsWater(component.liquidId))
      ++inWaterCount;
    if (!(col.GetComponent<Oasis>() != null))
      return;
    ++inWaterCount;
  }

  public void OnTriggerExit(Collider col)
  {
    LiquidSource component = col.GetComponent<LiquidSource>();
    if (component != null && Identifiable.IsWater(component.liquidId))
      --inWaterCount;
    if (!(col.GetComponent<Oasis>() != null))
      return;
    --inWaterCount;
  }

  public void Update()
  {
    if (inWaterCount > 0 && timeDir.HasReached(nextInWaterCheck))
    {
      AddLiquid(Identifiable.Id.WATER_LIQUID, 1f);
      nextInWaterCheck = timeDir.HoursFromNow(0.0166666675f);
    }
    if (flashFactor <= 0.0 || !(sterileMat != null))
      return;
    flashFactor = Mathf.Max(0.0f, flashFactor - Time.deltaTime * 2f);
    sterileMat.SetFloat("_HitFlash", flashFactor);
  }

  public void AddLiquid(Identifiable.Id liquidId, float units)
  {
    if (!Identifiable.IsLiquid(liquidId))
      return;
    if (!sterilized)
    {
      sterilized = true;
      SetSterilized();
      destroyer.MultiplyRemainingHours(timeRemainingFactor);
    }
    FlashHit();
    float num = liquidId == Identifiable.Id.MAGIC_WATER_LIQUID ? 10f : 1f;
    destroyer.AdvanceHours(hoursPerHit * units * num);
  }

  public void OnDestroy()
  {
    if (!(sterileMat != null))
      return;
    Destroyer.Destroy(sterileMat, "TarrSterilizeOnWater.OnDestroy");
  }

  private void FlashHit() => flashFactor = 1f;

  private void SetSterilized()
  {
    slimeEat.chanceToSkipProduce = 1f;
    if (sterileMat != null)
      Log.Warning("Already have a sterile material");
    Material material = null;
    foreach (Renderer flashRenderer in flashRenderers)
    {
      if (material == null)
      {
        material = flashRenderer.material;
        sterileMat = material;
        material.SetTexture(ColorRampPropertyId, sterileRampTex);
      }
      flashRenderer.material = material;
    }
  }

  public void FromSerializable(bool sterilized)
  {
    this.sterilized = sterilized;
    if (!sterilized)
      return;
    SetSterilized();
  }

  public void ToSerializable(out bool sterilized) => sterilized = this.sterilized;
}
