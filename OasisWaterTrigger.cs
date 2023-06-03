// Decompiled with JetBrains decompiler
// Type: OasisWaterTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class OasisWaterTrigger : SRBehaviour, LiquidConsumer
{
  public Oasis oasisToScale;
  public SECTR_AudioCue scaleCue;
  public GameObject scaleFX;
  public GameObject indicatorObj;
  public GameObject indicatorReplacesObj;
  private bool hasAlreadyActivated;

  public void Start()
  {
    bool flag = oasisToScale.IsLive();
    if (indicatorObj != null)
      indicatorObj.SetActive(flag);
    if (indicatorReplacesObj != null)
      indicatorReplacesObj.SetActive(!flag);
    hasAlreadyActivated = flag;
  }

  public void AddLiquid(Identifiable.Id liquidId, float units)
  {
    if (!(oasisToScale != null) || liquidId != Identifiable.Id.MAGIC_WATER_LIQUID || hasAlreadyActivated)
      return;
    oasisToScale.SetLive(false);
    if (scaleCue != null)
      SECTR_AudioSystem.Play(scaleCue, transform.position, false);
    if (scaleFX != null)
      InstantiateDynamic(scaleFX, transform.position, transform.rotation);
    if (indicatorObj != null)
      indicatorObj.SetActive(true);
    if (indicatorReplacesObj != null)
      indicatorReplacesObj.SetActive(false);
    hasAlreadyActivated = true;
  }
}
