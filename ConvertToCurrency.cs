// Decompiled with JetBrains decompiler
// Type: ConvertToCurrency
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ConvertToCurrency : MonoBehaviour
{
  [Tooltip("Delay, in real-time seconds, before the currency is granted.")]
  public float delay = 0.25f;
  [Tooltip("Amount of currency to grant.")]
  public int amount;
  public GameObject destroyFX;
  private PlayerState playerState;
  private float convertTime;
  private const float ANIMATION_DURATION = 4f;
  private float destroyTime;

  public void Awake()
  {
    convertTime = Time.time + delay;
    destroyTime = convertTime + 4f;
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    playerState.AddCurrency(amount, PlayerState.CoinsType.NONE);
    playerState.AddCurrencyDisplayDelta(-amount);
  }

  private void Update()
  {
    if (Time.time >= (double) convertTime && amount > 0)
    {
      SRSingleton<PopupElementsUI>.Instance.CreateCoinsPopup(amount, PlayerState.CoinsType.NORM);
      playerState.AddCurrencyDisplayDelta(amount);
      amount = 0;
    }
    if (Time.time < (double) destroyTime)
      return;
    if (destroyFX != null)
      Instantiate(destroyFX, gameObject.transform.position, gameObject.transform.rotation);
    Destroyer.Destroy(gameObject, "ConvertToCurrency.Update");
  }
}
