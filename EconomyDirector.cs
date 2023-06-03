// Decompiled with JetBrains decompiler
// Type: EconomyDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EconomyDirector : SRBehaviour, WorldModel.Participant
{
  public ValueMap[] baseValueMap;
  public float saturationSensitivity = 0.05f;
  public float saturationRecovery = 0.25f;
  public float dailyShutdownMins = 5f;
  private double nextUpdateTime;
  private TimeDirector timeDir;
  public DidUpdate didUpdateDelegate;
  public OnRegisterSold onRegisterSold;
  private WorldModel worldModel;
  private Dictionary<Identifiable.Id, CurrValueEntry> currValueMap = new Dictionary<Identifiable.Id, CurrValueEntry>(Identifiable.idComparer);

  public void InitForLevel()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    nextUpdateTime = 0.0;
    if (saturationRecovery < 0.0 || saturationRecovery > 1.0)
      throw new ArgumentException("Saturation Recovery must be [0-1]");
    SRSingleton<SceneContext>.Instance.GameModel.RegisterWorldParticipant(this);
  }

  public void InitModel(WorldModel model)
  {
    model.econSeed = new Randoms().GetFloat(1000000f);
    foreach (ValueMap baseValue in baseValueMap)
    {
      currValueMap[baseValue.accept.id] = new CurrValueEntry(baseValue.value, baseValue.value, baseValue.value, baseValue.fullSaturation);
      model.marketSaturation[baseValue.accept.id] = baseValue.fullSaturation * 0.5f;
    }
    ResetPrices(model, 0);
  }

  public void SetModel(WorldModel model) => worldModel = model;

  public void ResetPrices(WorldModel worldModel, int day)
  {
    foreach (KeyValuePair<Identifiable.Id, CurrValueEntry> currValue in currValueMap)
    {
      if (nextUpdateTime > 0.0)
        worldModel.marketSaturation[currValue.Key] *= 1f - saturationRecovery;
      float targetValue = GetTargetValue(worldModel, currValue.Key, currValue.Value.baseValue, currValue.Value.fullSaturation, day);
      currValue.Value.prevValue = currValue.Value.currValue;
      currValue.Value.currValue = targetValue;
    }
    if (didUpdateDelegate == null)
      return;
    didUpdateDelegate();
  }

  public void Update()
  {
    if (!SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().plortMarketDynamic || !timeDir.HasReached(nextUpdateTime))
      return;
    ResetPrices(worldModel, timeDir.CurrDay());
    nextUpdateTime = timeDir.GetNextHour(0.0f);
  }

  public bool IsMarketShutdown() => SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().plortMarketDynamic && timeDir.CurrHour() * 60.0 < dailyShutdownMins;

  public int? GetCurrValue(Identifiable.Id id) => currValueMap.ContainsKey(id) ? new int?(Mathf.RoundToInt(currValueMap[id].currValue)) : new int?();

  public int? GetChangeInValue(Identifiable.Id id) => currValueMap.ContainsKey(id) ? new int?(Mathf.RoundToInt(currValueMap[id].currValue) - Mathf.RoundToInt(currValueMap[id].prevValue)) : new int?();

  public void RegisterSold(Identifiable.Id id, int count)
  {
    worldModel.marketSaturation[id] += count;
    if (onRegisterSold == null)
      return;
    onRegisterSold(id);
  }

  private float GetTargetValue(
    WorldModel worldModel,
    Identifiable.Id id,
    float baseValue,
    float fullSaturation,
    float day)
  {
    if (!SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().plortMarketDynamic)
      return baseValue * 1.5f;
    float num1 = 1f + Mathf.Clamp01((fullSaturation - worldModel.marketSaturation[id]) / fullSaturation);
    float num2 = Noise.Noise.PerlinNoise(day, worldModel.econSeed, -10000f, 10f, 0.6f, 1f) + 0.7f;
    float num3 = Noise.Noise.PerlinNoise(day, worldModel.econSeed, id.GetHashCode() * 10000, 10f, 0.6f, 1f) + 0.7f;
    return baseValue * num1 * num2 * num3;
  }

  [Serializable]
  public class ValueMap
  {
    public Identifiable accept;
    public float value;
    public float fullSaturation;
  }

  public delegate void DidUpdate();

  public delegate void OnRegisterSold(Identifiable.Id id);

  private class CurrValueEntry
  {
    public readonly float baseValue;
    public float currValue;
    public float prevValue;
    public float fullSaturation;

    public CurrValueEntry(float baseValue, float currValue, float prevValue, float fullSaturation)
    {
      this.baseValue = baseValue;
      this.currValue = currValue;
      this.prevValue = prevValue;
      this.fullSaturation = fullSaturation;
    }
  }
}
