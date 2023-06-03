// Decompiled with JetBrains decompiler
// Type: WaitForChargeup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WaitForChargeup : MonoBehaviour, GadgetModel.Participant
{
  public float waitTimeGameHrs = 2f;
  private TimeDirector timeDir;
  private GadgetDirector gadgetDir;
  private GameObject waitingObj;
  private Text waitingText;
  private int lastWaitingMins = -1;
  private GadgetModel model;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    gadgetDir = SRSingleton<SceneContext>.Instance.GadgetDirector;
  }

  public void InitModel(GadgetModel model) => model.waitForChargeupTime = timeDir.HoursFromNow(waitTimeGameHrs);

  public void SetModel(GadgetModel model) => this.model = model;

  public void Update()
  {
    bool flag = !timeDir.HasReached(model.waitForChargeupTime);
    if (flag && waitingObj == null)
    {
      waitingObj = Instantiate(gadgetDir.waitForChargeupPrefab, transform.position, transform.rotation, transform);
      waitingText = waitingObj.transform.Find("InstallationRing/TextUI/ClockPanel/TimeText").GetComponent<Text>();
    }
    else if (!flag && waitingObj != null)
    {
      Destroyer.Destroy(waitingObj, "WaitForChargeup.Update");
      waitingObj = null;
      waitingText = null;
      lastWaitingMins = -1;
    }
    if (!(waitingText != null))
      return;
    int totalMins = (int) Math.Ceiling(timeDir.HoursUntil(model.waitForChargeupTime) * 60.0);
    if (totalMins == lastWaitingMins)
      return;
    waitingText.text = timeDir.FormatTime(totalMins);
    lastWaitingMins = totalMins;
  }

  public bool IsWaiting() => waitingObj != null;
}
