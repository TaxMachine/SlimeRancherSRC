// Decompiled with JetBrains decompiler
// Type: QuicksilverPlortCollector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class QuicksilverPlortCollector : SRBehaviour
{
  [Tooltip("Duration, in game minutes, until the plort is collected.")]
  public Range activeMinutes;
  [Tooltip("FX spawned when the countdown to collection begins. (optional)")]
  public GameObject activeFX;
  [Tooltip("FX spawned when the plort is collected. (optional)")]
  public GameObject destroyFX;
  [Tooltip("SFX played when the quicksilver plort begins collection. (optional)")]
  public SECTR_AudioCue onCollectionCue;
  private TimeDirector timeDirector;
  private PediaDirector pediaDirector;
  private double timer;

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    pediaDirector = SRSingleton<SceneContext>.Instance.PediaDirector;
    ResetCollectionTime();
  }

  public void Start()
  {
    if (!(activeFX != null))
      return;
    SpawnAndPlayFX(activeFX, transform.position, Quaternion.identity);
  }

  public void OnEnable() => ResetCollectionTime();

  public void Update()
  {
    if (!timeDirector.HasReached(timer))
      return;
    Identifiable component = GetComponent<Identifiable>();
    foreach (KeyValuePair<PlayerState.AmmoMode, Ammo> keyValuePair in SRSingleton<SceneContext>.Instance.PlayerState.GetAmmoDict())
    {
      if (keyValuePair.Value.MaybeAddToSlot(component.id, component))
      {
        if (destroyFX != null)
          SpawnAndPlayFX(destroyFX, transform.position, Quaternion.identity);
        SECTR_AudioSystem.Play(onCollectionCue, transform.position, false);
        Destroyer.DestroyActor(gameObject, "QuicksilverPlortCollector.Update");
        pediaDirector.MaybeShowPopup(component.id);
        break;
      }
    }
    ResetCollectionTime();
  }

  private void ResetCollectionTime() => timer = timeDirector.HoursFromNow(activeMinutes.Random() * 0.0166666675f);
}
