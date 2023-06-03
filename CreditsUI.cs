// Decompiled with JetBrains decompiler
// Type: CreditsUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.IO;
using UnityEngine;

public class CreditsUI : BaseUI
{
  public bool skippable;
  public bool doPreCredits;
  public CanvasGroup root;
  public CanvasGroup background;
  public CanvasGroup preCreditsLine1;
  public CanvasGroup preCreditsLine2;
  public CanvasGroup preCreditsLine3;
  public const float fadeTime = 1f;
  public const float fadeTimeMargin = 0.25f;
  public const float preCreditsLineFadeTime = 1f;
  public const float preCreditsLineTime = 5f;
  public const float creditsLifetime = 169f;
  private bool endReached;
  private CameraDisabler camDisabler;
  public OnCreditsEndedEvent OnCreditsEnded;
  private const float DEFAULT_CREDITS_MASTER_VOLUME = 0.1f;
  private const float DEFAULT_CREDITS_MUSIC_VOLUME = 0.1f;
  private float preCreditsMasterVolume;
  private float preCreditsMusicVolume;
  private const string MUSIC_BUS_NAME = "Music";
  private const string MASTER_BUS_NAME = "MasterBus";
  private MusicDirector musicDirector;

  public void OnEnable()
  {
    musicDirector = SRSingleton<GameContext>.Instance.MusicDirector;
    musicDirector.RegisterSuppressor(this);
    if (SRSingleton<SceneContext>.Instance.Player != null)
      camDisabler = SRSingleton<SceneContext>.Instance.Player.GetComponentInChildren<CameraDisabler>();
    else if (Camera.main != null)
      camDisabler = Camera.main.GetComponent<CameraDisabler>();
    preCreditsMasterVolume = SECTR_AudioSystem.GetBusVolume("MasterBus");
    preCreditsMusicVolume = SECTR_AudioSystem.GetBusVolume("Music");
    SECTR_AudioSystem.PauseNonUISFX(true);
    StartCoroutine(DoSequence());
  }

  public void OnDisable()
  {
    if (musicDirector != null)
    {
      musicDirector.SetCreditsMode(false);
      musicDirector.DeregisterSuppressor(this);
      musicDirector.ForceStopCurrent();
      musicDirector = null;
    }
    if (SRSingleton<SceneContext>.Instance != null)
      SRSingleton<SceneContext>.Instance.ProgressDirector.NoteReturnedToRanch();
    SECTR_AudioSystem.SetBusVolume("MasterBus", preCreditsMasterVolume);
    SECTR_AudioSystem.SetBusVolume("Music", preCreditsMusicVolume);
    SECTR_AudioSystem.PauseNonUISFX(false);
    if (!(camDisabler != null))
      return;
    camDisabler.RemoveBlocker(this);
  }

  public IEnumerator DoSequence()
  {
    CreditsUI comp = this;
    if (comp.doPreCredits)
    {
      if (comp.camDisabler != null)
        comp.camDisabler.AddBlocker(comp);
      comp.preCreditsLine1.DOFade(1f, 1f).SetUpdate(true);
      yield return new WaitForSecondsRealtime(5f);
      comp.preCreditsLine2.DOFade(1f, 1f).SetUpdate(true);
      yield return new WaitForSecondsRealtime(5f);
      comp.preCreditsLine3.DOFade(1f, 1f).SetUpdate(true);
      yield return new WaitForSecondsRealtime(5f);
      yield return new WaitForEndOfFrame();
    }
    else
    {
      comp.background.DOFade(1f, 1f).From(0.0f).SetUpdate(true);
      yield return new WaitForSecondsRealtime(1.25f);
      yield return new WaitForEndOfFrame();
      if (comp.camDisabler != null)
        comp.camDisabler.AddBlocker(comp);
    }
    comp.SetCreditsVolume();
    comp.musicDirector.DeregisterSuppressor(comp);
    comp.musicDirector.SetCreditsMode(true);
    GameObject scroller = comp.GetCreditsScroll();
    yield return new WaitForSecondsRealtime(0.25f);
    scroller.GetComponent<Animator>().SetBool("ReadyToRun", true);
    if (comp.doPreCredits)
    {
      comp.preCreditsLine1.DOFade(0.0f, 1f).SetUpdate(true);
      comp.preCreditsLine2.DOFade(0.0f, 1f).SetUpdate(true);
      comp.preCreditsLine3.DOFade(0.0f, 1f).SetUpdate(true);
    }
    yield return new WaitForSecondsRealtime(169f);
    if (comp.camDisabler != null)
      comp.camDisabler.RemoveBlocker(comp);
    comp.root.DOFade(0.0f, 1f).SetUpdate(true);
    yield return new WaitForSecondsRealtime(1f);
    comp.endReached = true;
    if (comp.OnCreditsEnded != null)
      comp.OnCreditsEnded();
    comp.Close();
  }

  protected override bool Closeable()
  {
    if (!base.Closeable())
      return false;
    return skippable || endReached;
  }

  private GameObject GetCreditsScroll()
  {
    GameObject creditsScrollPrefab = CreateCreditsScrollPrefab();
    creditsScrollPrefab.transform.SetParent(transform, false);
    return creditsScrollPrefab;
  }

  private void SetCreditsVolume()
  {
    if (preCreditsMasterVolume <= 0.0)
      SECTR_AudioSystem.SetBusVolume("MasterBus", 0.1f);
    if (preCreditsMusicVolume > 0.0)
      return;
    SECTR_AudioSystem.SetBusVolume("Music", 0.1f);
  }

  private GameObject CreateCreditsScrollPrefab()
  {
    AssetBundle creditsBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "credits"));
    if (creditsBundle == null)
    {
      Debug.Log("Failed to load AssetBundle!: " + Application.streamingAssetsPath);
      return null;
    }
    GameObject creditsScrollPrefab = Instantiate(creditsBundle.LoadAsset<GameObject>("Credit_screen"));
    onDestroy = () =>
    {
      if (!(this != null) || !(gameObject != null))
        return;
      creditsBundle.Unload(true);
    };
    return creditsScrollPrefab;
  }

  public delegate void OnCreditsEndedEvent();
}
