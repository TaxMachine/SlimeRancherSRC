// Decompiled with JetBrains decompiler
// Type: WeatherEffectAttachment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class WeatherEffectAttachment : MonoBehaviour
{
  private GameObject currWeather;
  private int blockers;

  public void OnTriggerEnter(Collider col)
  {
    if (!(col.GetComponent<WeatherBlockingTrigger>() != null))
      return;
    ++blockers;
    UpdateBlocked();
  }

  public void OnTriggerExit(Collider col)
  {
    if (!(col.GetComponent<WeatherBlockingTrigger>() != null))
      return;
    --blockers;
    UpdateBlocked();
  }

  public void SetWeather(GameObject weatherPrefab)
  {
    if (currWeather != null)
      Destroyer.Destroy(currWeather, "WeatherEffectAttachment.SetWeather");
    if (!(weatherPrefab != null))
      return;
    currWeather = Instantiate(weatherPrefab);
    currWeather.transform.SetParent(transform, false);
    UpdateBlocked();
  }

  private void UpdateBlocked()
  {
    if (!(currWeather != null))
      return;
    currWeather.SetActive(blockers == 0);
  }
}
