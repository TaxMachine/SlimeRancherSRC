// Decompiled with JetBrains decompiler
// Type: EchoNoteGordo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EchoNoteGordo : IdHandler, EchoNoteGordoModel.Participant
{
  [Tooltip("Parent GameObject containing the gordo model.")]
  public GameObject gordo;
  [Tooltip("SFX played when the EchoNoteGordo is active.")]
  public SECTR_AudioCue onActiveCue;
  private SECTR_AudioCueInstance onActiveCueInstance;
  [Tooltip("SFX played when the EchoNoteGordo is popping.")]
  public SECTR_AudioCue onPoppingCue;
  private SECTR_AudioCueInstance onPoppingCueInstance;
  [Tooltip("Parent GameObject containing the portal ring.")]
  public GameObject ring;
  [Tooltip("Instruments unlocked in addition to the next instrument in the unlock sequence.")]
  public InstrumentModel.Instrument[] bonusInstruments;
  private const float POPPING_DISTANCE = 8f;
  private const float POPPING_DISTANCE_SQR = 64f;
  private static readonly int PROPERTY_FADE = Shader.PropertyToID("_Fade");
  private EchoNoteGordoModel model;
  private Animator animator;
  private TeleportSource teleporter;

  public void Awake()
  {
    animator = GetComponentInChildren<Animator>();
    teleporter = GetComponentInChildren<TeleportSource>();
    SRSingleton<SceneContext>.Instance.GameModel.RegisterEchoNoteGordo(id, gameObject);
  }

  public void Start()
  {
  }

  public void OnEnable()
  {
    if (model == null || !SRSingleton<SceneContext>.Instance.GameModel.GetHolidayModel().eventEchoNoteGordos.Any(e => e.objectId == id))
    {
      teleporter.waitForExternalActivation = true;
      gameObject.SetActive(false);
    }
    else
    {
      if (model.state == EchoNoteGordoModel.State.NOT_POPPED)
      {
        onActiveCueInstance.Stop(true);
        onActiveCueInstance = SECTR_AudioSystem.Play(onActiveCue, gordo.transform.position, true);
      }
      onPoppingCueInstance.Pause(false);
      teleporter.waitForExternalActivation = model.state != EchoNoteGordoModel.State.POPPED;
      gordo.SetActive(model.state != EchoNoteGordoModel.State.POPPED);
      ring.SetActive(model.state == EchoNoteGordoModel.State.POPPED);
    }
  }

  public void OnDisable()
  {
    onActiveCueInstance.Stop(true);
    onPoppingCueInstance.Pause(true);
  }

  public void OnDestroy()
  {
    if (SRSingleton<SceneContext>.Instance != null)
      SRSingleton<SceneContext>.Instance.GameModel.UnregisterEchoNoteGordo(id);
    onPoppingCueInstance.Stop(true);
  }

  public void Update()
  {
    if (model.state != EchoNoteGordoModel.State.NOT_POPPED || (SRSingleton<SceneContext>.Instance.Player.transform.position - gordo.transform.position).sqrMagnitude > 64.0)
      return;
    SRSingleton<SceneContext>.Instance.PediaDirector.MaybeShowPopup(PediaDirector.Id.ECHO_NOTE_GORDO_SLIME);
    model.state = EchoNoteGordoModel.State.POPPING_1;
    animator.SetBool("ACTIVATED", true);
  }

  protected override string IdPrefix() => "gordoEchoNote";

  public void InitModel(EchoNoteGordoModel model) => model.state = EchoNoteGordoModel.State.NOT_POPPED;

  public void SetModel(EchoNoteGordoModel model)
  {
    this.model = model;
    if (this.model.state >= EchoNoteGordoModel.State.POPPED)
      return;
    this.model.state = EchoNoteGordoModel.State.NOT_POPPED;
  }

  public void OnAnimationEvent_StateEnter(EchoNoteGordoAnimatorState.Id id)
  {
    if (id != EchoNoteGordoAnimatorState.Id.ACTIVATION || model.state != EchoNoteGordoModel.State.POPPING_1)
      return;
    model.state = EchoNoteGordoModel.State.POPPING_2;
    onActiveCueInstance.Stop(false);
    onPoppingCueInstance.Stop(true);
    onPoppingCueInstance = SECTR_AudioSystem.Play(onPoppingCue, gordo.transform.position, false);
  }

  public void OnAnimationEvent_StateExit(EchoNoteGordoAnimatorState.Id id)
  {
    if (id != EchoNoteGordoAnimatorState.Id.ACTIVATION || model.state != EchoNoteGordoModel.State.POPPED)
      return;
    teleporter.waitForExternalActivation = false;
    gordo.SetActive(false);
    ring.SetActive(true);
  }

  public void OnAnimationEvent_Popped()
  {
    if (model.state != EchoNoteGordoModel.State.POPPING_2)
      return;
    model.state = EchoNoteGordoModel.State.POPPED;
    AnalyticsUtil.CustomEvent("TwinkleSlimeBurst", new Dictionary<string, object>()
    {
      {
        "type",
        name
      },
      {
        "twinkleId",
        id
      }
    });
    RegionRegistry.RegionSetId setId = GetComponentInParent<Region>().setId;
    SRSingleton<SceneContext>.Instance.InstrumentDirector.UnlockNextInstrument();
    foreach (InstrumentModel.Instrument bonusInstrument in bonusInstruments)
      SRSingleton<SceneContext>.Instance.InstrumentDirector.UnlockInstrument(bonusInstrument);
    foreach (EchoNoteMetadata componentsInChild in GetComponentsInChildren<EchoNoteMetadata>(true))
    {
      GameObject gameObject = InstantiateActor(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(componentsInChild.id), setId, componentsInChild.transform.position, componentsInChild.transform.rotation);
      float inRange = Randoms.SHARED.GetInRange(0.0f, 1f);
      Renderer chimeRenderer = gameObject.GetComponentInChildren<Renderer>();
      chimeRenderer.gameObject.AddComponent<PauseTweenOnDisable>().tween = DOTween.To(() => GetMaterialFade(chimeRenderer), fade => SetMaterialFade(chimeRenderer, fade), 1f, 3f).From(0.0f).SetDelay(inRange);
    }
    teleporter.waitForExternalActivation = false;
    ring.SetActive(true);
  }

  private float GetMaterialFade(Renderer targetRenderer)
  {
    MaterialPropertyBlock properties = new MaterialPropertyBlock();
    targetRenderer.GetPropertyBlock(properties);
    return properties.GetFloat(PROPERTY_FADE);
  }

  private void SetMaterialFade(Renderer targetRenderer, float fade)
  {
    MaterialPropertyBlock properties = new MaterialPropertyBlock();
    targetRenderer.GetPropertyBlock(properties);
    properties.SetFloat(PROPERTY_FADE, fade);
    targetRenderer.SetPropertyBlock(properties);
  }

  public void PrepareWorldGenerated()
  {
    GameObject gameObject1 = new GameObject("cluster_notes_metadata");
    gameObject1.transform.SetParent(transform, false);
    for (int index = transform.childCount - 1; index >= 0; --index)
    {
      EchoNote[] componentsInChildren = transform.GetChild(index).GetComponentsInChildren<EchoNote>(true);
      if (componentsInChildren.Length != 0)
      {
        foreach (EchoNote echoNote in componentsInChildren)
        {
          GameObject gameObject2 = new GameObject(string.Format("echoNote{0}", echoNote.clip.ToString("D2")));
          gameObject2.transform.SetParent(gameObject1.transform, false);
          gameObject2.transform.position = echoNote.transform.position;
          gameObject2.AddComponent<EchoNoteMetadata>().id = (Identifiable.Id) (17000 + echoNote.clip - 1);
        }
        if (Application.isPlaying)
          Destroyer.Destroy(transform.GetChild(index).gameObject, 0.0f, "EchoNoteGordo.Start", true);
        else
          DestroyImmediate(transform.GetChild(index).gameObject);
      }
    }
    int num = Application.isPlaying ? 1 : 0;
  }

  public void OnDrawGizmosSelected()
  {
    if (!(gordo != null))
      return;
    Gizmos.color = Color.magenta;
    Gizmos.DrawWireSphere(gordo.transform.position, 8f);
  }
}
