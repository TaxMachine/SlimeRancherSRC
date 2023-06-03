// Decompiled with JetBrains decompiler
// Type: EventGordoEat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof (GordoFaceAnimator))]
public class EventGordoEat : GordoEat
{
  [Tooltip("SFX played when the EventGordo is enabled.")]
  public SECTR_AudioCue onActiveCue;
  private SECTR_AudioCueInstance onActiveCueInstance;

  public void OnEnable()
  {
    if (!Application.isPlaying)
      return;
    if (gordoModel == null || gordoModel.gordoEatenCount == -1 || !SRSingleton<SceneContext>.Instance.GameModel.GetHolidayModel().eventGordos.Any(e => e.objectId == id))
    {
      gameObject.SetActive(false);
    }
    else
    {
      onActiveCueInstance.Stop(false);
      onActiveCueInstance = SECTR_AudioSystem.Play(onActiveCue, transform.position, true);
    }
  }

  public void OnDisable()
  {
    if (!Application.isPlaying)
      return;
    onActiveCueInstance.Stop(false);
  }

  public override void SetModel(GordoModel model)
  {
    gordoModel = model;
    if (gordoModel.gordoEatenCount == -1)
      return;
    base.SetModel(model);
  }

  protected override PediaDirector.Id GetPediaId() => PediaDirector.Id.PARTY_GORDO_SLIME;

  protected override void DidCompleteBurst()
  {
    base.DidCompleteBurst();
    new GameObject("EventGordoMusic")
    {
      transform = {
        position = transform.position
      }
    }.AddComponent<EventGordoMusic>();
  }

  private class EventGordoMusic : MonoBehaviour
  {
    private const float MAX_DISTANCE = 30f;
    private const float MAX_DISTANCE_SQR = 900f;
    private GameObject player;
    private MusicDirector music;
    private float time;

    public void Awake()
    {
      player = SRSingleton<SceneContext>.Instance.Player;
      music = SRSingleton<GameContext>.Instance.MusicDirector;
      music.SetEventGordoMode(true);
      time = Time.unscaledTime + music.eventGordoMusic.MinClipLength() - music.eventGordoMusic.FadeOutTime;
    }

    public void Update()
    {
      if (Time.unscaledTime < (double) time && (transform.position - player.transform.position).sqrMagnitude < 900.0)
        return;
      Destroyer.Destroy(gameObject, "EventGordoMusic.Update");
    }

    public void OnDestroy() => music.SetEventGordoMode(false);
  }
}
