// Decompiled with JetBrains decompiler
// Type: CFX_LightIntensityFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Light))]
public class CFX_LightIntensityFade : MonoBehaviour
{
  public float duration = 1f;
  public float delay;
  public float finalIntensity;
  private float baseIntensity;
  public bool autodestruct;
  private float p_lifetime;
  private float p_delay;

  private void Start() => baseIntensity = GetComponent<Light>().intensity;

  private void OnEnable()
  {
    p_lifetime = 0.0f;
    p_delay = delay;
    if (delay <= 0.0)
      return;
    GetComponent<Light>().enabled = false;
  }

  private void Update()
  {
    if (p_delay > 0.0)
    {
      p_delay -= Time.deltaTime;
      if (p_delay > 0.0)
        return;
      GetComponent<Light>().enabled = true;
    }
    else if (p_lifetime / (double) duration < 1.0)
    {
      GetComponent<Light>().intensity = Mathf.Lerp(baseIntensity, finalIntensity, p_lifetime / duration);
      p_lifetime += Time.deltaTime;
    }
    else
    {
      if (!autodestruct)
        return;
      Destroyer.Destroy(gameObject, "CFX_LightIntensityFade.Update");
    }
  }
}
