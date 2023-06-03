// Decompiled with JetBrains decompiler
// Type: Flashlight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
  public SECTR_AudioCue flashlightOn;
  public SECTR_AudioCue flashlightOff;
  private Light activateLight;

  public void Awake() => activateLight = GetComponent<Light>();

  public void Update()
  {
    if (Time.timeScale == 0.0 || !SRInput.Actions.light.WasPressed)
      return;
    activateLight.enabled = !activateLight.enabled;
    AnalyticsUtil.CustomEvent("FlashlightToggled", new Dictionary<string, object>()
    {
      {
        "FlashlightState",
        activateLight.enabled
      }
    });
    SECTR_AudioSystem.Play(activateLight.enabled ? flashlightOn : flashlightOff, Vector3.zero, false);
  }
}
