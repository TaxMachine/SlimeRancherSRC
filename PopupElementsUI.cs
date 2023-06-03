// Decompiled with JetBrains decompiler
// Type: PopupElementsUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PopupElementsUI : SRSingleton<PopupElementsUI>
{
  public RectTransform container;
  public GameObject coinsPopup;
  public Sprite mochiIcon;
  public Color mochiColor;
  public SECTR_AudioCue mochiCue;
  public DroneMetadata droneMetadata;
  private HashSet<GameObject> blockers = new HashSet<GameObject>();
  private Queue<CoinsEntry> queuedCoins = new Queue<CoinsEntry>();
  private float nextCoinAt;
  private const float MIN_TIME_BETWEEN_COINS = 0.1f;

  public void CreateCoinsPopup(int amount, PlayerState.CoinsType coinsType)
  {
    if (amount == 0 || coinsType == PlayerState.CoinsType.NONE)
      return;
    queuedCoins.Enqueue(new CoinsEntry(amount, coinsType));
  }

  public void Update()
  {
    if (blockers.Count != 0 || queuedCoins.Count <= 0 || Time.unscaledTime < (double) nextCoinAt)
      return;
    CoinsPopupUI component = Instantiate(coinsPopup, container).GetComponent<CoinsPopupUI>();
    CoinsEntry coinsEntry = queuedCoins.Dequeue();
    int amount = coinsEntry.amount;
    Sprite sprite = GetSprite(coinsEntry.coinsType);
    Color? colorOverride = GetColorOverride(coinsEntry.coinsType);
    SECTR_AudioCue sfxOverride = GetSFXOverride(coinsEntry.coinsType);
    component.Init(amount, sprite, colorOverride, sfxOverride);
    nextCoinAt = Time.unscaledTime + 0.1f;
  }

  public void RegisterBlocker(GameObject blocker) => blockers.Add(blocker);

  public void DeregisterBlocker(GameObject blocker) => blockers.Remove(blocker);

  private Sprite GetSprite(PlayerState.CoinsType coinsType)
  {
    if (coinsType == PlayerState.CoinsType.MOCHI)
      return mochiIcon;
    return coinsType == PlayerState.CoinsType.DRONE ? droneMetadata.coinsIcon : null;
  }

  private Color? GetColorOverride(PlayerState.CoinsType coinsType)
  {
    if (coinsType == PlayerState.CoinsType.MOCHI)
      return new Color?(mochiColor);
    return coinsType == PlayerState.CoinsType.DRONE ? new Color?(droneMetadata.coinsColor) : new Color?();
  }

  private SECTR_AudioCue GetSFXOverride(PlayerState.CoinsType coinsType)
  {
    if (coinsType == PlayerState.CoinsType.MOCHI)
      return mochiCue;
    return coinsType == PlayerState.CoinsType.DRONE ? droneMetadata.coinsCue : null;
  }

  private class CoinsEntry
  {
    public int amount;
    public PlayerState.CoinsType coinsType;

    public CoinsEntry(int amount, PlayerState.CoinsType coinsType)
    {
      this.amount = amount;
      this.coinsType = coinsType;
    }
  }
}
