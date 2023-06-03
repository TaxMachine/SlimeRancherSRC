// Decompiled with JetBrains decompiler
// Type: PuzzleSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.Collections;
using UnityEngine;

public class PuzzleSlot : IdHandler, PuzzleSlotModel.Participant
{
  public Identifiable.Id catchId;
  public GameObject changeFX;
  public GameObject[] activateOnFill;
  public bool fillOnAwake;
  public SECTR_AudioCue localFillCue;
  private PuzzleSlotLockable puzLockable;
  private PuzzleSlotModel model;
  private const float LOCK_CUE_DELAY = 0.5f;

  public void Awake() => SRSingleton<SceneContext>.Instance.GameModel.RegisterSlot(id, gameObject);

  public void RegisterLock(PuzzleSlotLockable puzzleLockable) => puzLockable = puzzleLockable;

  public void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterSlot(id);
  }

  public void InitModel(PuzzleSlotModel model)
  {
    if (!fillOnAwake)
      return;
    model.filled = true;
  }

  public void SetModel(PuzzleSlotModel model)
  {
    this.model = model;
    OnFilledChanged();
  }

  public void OnFilledChangedFromModel() => OnFilledChanged();

  private void OnFilledChanged()
  {
    if (model.filled)
      ActivateOnFill();
    if (!(puzLockable != null))
      return;
    puzLockable.NotifySlotChanged(true);
  }

  public void OnTriggerEnter(Collider collider)
  {
    if (collider.isTrigger)
      return;
    GameObject gameObject = collider.gameObject;
    Identifiable component1 = gameObject.GetComponent<Identifiable>();
    if (!(component1 != null) || component1.id != catchId || model.filled)
      return;
    model.filled = true;
    SpawnAndPlayFX(changeFX, gameObject.transform.position, gameObject.transform.rotation);
    ActivateOnFill();
    DestroyOnTouching component2 = gameObject.GetComponent<DestroyOnTouching>();
    if (component2 != null)
      component2.NoteDestroying();
    Destroyer.DestroyActor(gameObject, "PuzzleSlot.OnTriggerEnter");
    if (!(puzLockable != null))
      return;
    puzLockable.NotifySlotChanged();
    SECTR_AudioCue cueForLastSlot = puzLockable.GetCueForLastSlot();
    SECTR_AudioSystem.Play(localFillCue, transform.position, false);
    StartCoroutine(DelayedPlayLockCue(cueForLastSlot));
  }

  public IEnumerator DelayedPlayLockCue(SECTR_AudioCue cue)
  {
    yield return new WaitForSeconds(0.5f);
    puzLockable.PlayCue(cue);
  }

  public bool IsLocked() => model == null || !model.filled;

  protected override string IdPrefix() => "puz";

  private void ActivateOnFill()
  {
    if (activateOnFill == null)
      return;
    foreach (GameObject gameObject in activateOnFill)
      gameObject.SetActive(true);
  }
}
