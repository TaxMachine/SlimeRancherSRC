// Decompiled with JetBrains decompiler
// Type: StorageSlotUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public abstract class StorageSlotUI : MonoBehaviour
{
  public Image slotIcon;
  public Image frontFrameIcon;
  public Image backFrameIcon;
  public WorldStatusBar bar;
  public Sprite backEmpty;
  public Sprite backFilled;
  public Sprite frontEmpty;
  public Sprite frontFilled;
  private LookupDirector lookupDir;
  private Identifiable.Id? currentlyStoredId;

  public virtual void Awake() => lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;

  public void Update()
  {
    Identifiable.Id currentId = GetCurrentId();
    int num = (int) currentId;
    Identifiable.Id? currentlyStoredId = this.currentlyStoredId;
    int valueOrDefault = (int) currentlyStoredId.GetValueOrDefault();
    if (!(num == valueOrDefault & currentlyStoredId.HasValue))
    {
      if (currentId == Identifiable.Id.NONE)
      {
        slotIcon.enabled = false;
        bar.currValue = 0.0f;
        bar.barColor = Color.black;
        frontFrameIcon.sprite = frontEmpty;
        backFrameIcon.sprite = backEmpty;
      }
      else
      {
        slotIcon.sprite = lookupDir.GetIcon(currentId);
        slotIcon.enabled = true;
        bar.barColor = lookupDir.GetColor(currentId);
        frontFrameIcon.sprite = frontFilled;
        backFrameIcon.sprite = backFilled;
      }
      this.currentlyStoredId = new Identifiable.Id?(currentId);
    }
    if (currentId == Identifiable.Id.NONE)
      return;
    bar.currValue = GetCurrentCount();
  }

  protected abstract Identifiable.Id GetCurrentId();

  protected abstract int GetCurrentCount();
}
