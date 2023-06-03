// Decompiled with JetBrains decompiler
// Type: EnableOnlyDuringTimeWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EnableOnlyDuringTimeWindow : MonoBehaviour
{
  public float startHour;
  public float endHour;
  public GameObject[] toEnable;
  private TimeDirector timeDir;

  public void Awake() => timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;

  public void Update()
  {
    float num = timeDir.CurrHour();
    bool flag = startHour <= (double) num && num <= (double) endHour || startHour > (double) endHour && (num >= (double) startHour || num <= (double) endHour);
    foreach (GameObject gameObject in toEnable)
      gameObject.SetActive(flag);
  }
}
