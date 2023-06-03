// Decompiled with JetBrains decompiler
// Type: CaveLightController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CaveLightController : MonoBehaviour
{
  private Light controlledLight;
  private float defaultIntensity;
  private Dictionary<CaveTrigger, float> triggernessVals = new Dictionary<CaveTrigger, float>();

  public void Awake()
  {
    controlledLight = GetComponent<Light>();
    defaultIntensity = controlledLight.intensity;
  }

  public void SetTriggerness(CaveTrigger trigger, float triggerness) => triggernessVals[trigger] = triggerness;

  public void Update()
  {
    float a = 0.0f;
    foreach (KeyValuePair<CaveTrigger, float> triggernessVal in triggernessVals)
    {
      if (triggernessVal.Key != null && triggernessVal.Key.enabled)
        a = Mathf.Max(a, triggernessVal.Value);
    }
    controlledLight.intensity = defaultIntensity * a;
    controlledLight.enabled = a > 0.0;
  }
}
