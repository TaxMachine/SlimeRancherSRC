// Decompiled with JetBrains decompiler
// Type: PopupDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PopupDirector : MonoBehaviour
{
  private Queue<PopupCreator> popupQueue = new Queue<PopupCreator>();
  private Popup currPopup;
  private bool quitting;
  private TimeDirector timeDir;
  private int suppressors;

  public void Awake() => timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;

  public void InitForLevel() => popupQueue.Clear();

  public bool IsQueued(PopupCreator creator) => popupQueue.Contains(creator);

  public void QueueForPopup(PopupCreator creator)
  {
    popupQueue.Enqueue(creator);
    MaybePopupNext();
  }

  public void MaybePopupNext()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || popupQueue.Count <= 0 || currPopup != null || suppressors > 0)
      return;
    PopupCreator popupCreator = popupQueue.Dequeue();
    if (popupCreator.ShouldClear())
      MaybePopupNext();
    else
      popupCreator.Create();
  }

  public void CheckShouldClear()
  {
    if (currPopup == null || !currPopup.ShouldClear())
      return;
    Destroyer.Destroy(((Component) currPopup).gameObject, "PopupDirector.CheckShouldClear");
  }

  public void PopupActivated(Popup popup)
  {
    if (currPopup != null)
      Log.Warning("Popup arrived with already-active popup.");
    currPopup = popup;
  }

  public void PopupDeactivated(Popup popup)
  {
    if (currPopup == popup && !quitting)
    {
      currPopup = null;
      timeDir.OnUnpause(OnUnpause);
    }
    else
      Log.Warning("Popup deactivated, but wasn't current popup.");
  }

  public void RegisterSuppressor() => ++suppressors;

  public void UnregisterSuppressor()
  {
    --suppressors;
    if (suppressors > 0)
      return;
    MaybePopupNext();
  }

  public void OnUnpause() => MaybePopupNext();

  public void OnApplicationQuit() => quitting = true;

  public void OnDestroy() => timeDir.ClearOnUnpause(OnUnpause);

  public interface Popup
  {
    bool ShouldClear();
  }

  public abstract class PopupCreator
  {
    public abstract override bool Equals(object other);

    public abstract override int GetHashCode();

    public abstract void Create();

    public abstract bool ShouldClear();
  }
}
