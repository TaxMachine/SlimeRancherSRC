// Decompiled with JetBrains decompiler
// Type: GordoStealth
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GordoStealth : MonoBehaviour
{
  private int vacTriggerCount;
  private bool stealth;
  private double goStealthAt = double.PositiveInfinity;
  private float tgtOpacity;
  private float currOpacity;
  private TimeDirector timeDir;
  private MaterialStealthController materialStealthController;
  private const float OPACITY_CHANGE_PER_SEC = 2f;
  private const float STEALTH_OPACITY = 0.0f;
  private const float STEALTH_DELAY_HRS = 0.0833333358f;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    materialStealthController = new MaterialStealthController(gameObject);
    SetStealth(true);
  }

  public void OnTriggerEnter(Collider col)
  {
    if (!(col.GetComponentInParent<TrackCollisions>() != null))
      return;
    ++vacTriggerCount;
    if (vacTriggerCount != 1)
      return;
    SetStealth(false);
  }

  public void OnTriggerExit(Collider col)
  {
    if (!(col.GetComponentInParent<TrackCollisions>() != null))
      return;
    --vacTriggerCount;
    if (vacTriggerCount != 0)
      return;
    goStealthAt = timeDir.HoursFromNow(0.0833333358f);
  }

  public void Update()
  {
    if (!stealth && timeDir.HasReached(goStealthAt))
      SetStealth(true);
    UpdateStealthOpacity();
  }

  private void UpdateStealthOpacity()
  {
    if (tgtOpacity > (double) currOpacity)
      currOpacity = Mathf.Min(tgtOpacity, currOpacity + 2f * Time.deltaTime);
    else if (tgtOpacity < (double) currOpacity)
      currOpacity = Mathf.Max(tgtOpacity, currOpacity - 2f * Time.deltaTime);
    materialStealthController.SetOpacity(currOpacity);
  }

  private void SetStealth(bool stealth)
  {
    this.stealth = stealth;
    tgtOpacity = stealth ? 0.0f : 1f;
    goStealthAt = double.PositiveInfinity;
  }
}
