// Decompiled with JetBrains decompiler
// Type: CrosshairUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
  public float normScale = 1f;
  public float highScale = 1.5f;
  public float gadgetScale = 2f;
  public float vibrateAmount = 0.4f;
  public Color hasTarget = Color.green;
  public Color noTarget = Color.white;
  public Color gadgetTarget = Color.white;
  public Sprite normalSprite;
  public Sprite gadgetSprite;
  private PlayerState player;
  private float hudCrosshairScale;
  private float hudCrosshairScaleGoal;
  private Image img;
  private WeaponVacuum vacuum;

  private void Start()
  {
    player = SRSingleton<SceneContext>.Instance.PlayerState;
    vacuum = SRSingleton<SceneContext>.Instance.Player.GetComponentInChildren<WeaponVacuum>();
    hudCrosshairScale = normScale;
    hudCrosshairScaleGoal = normScale;
    img = GetComponent<Image>();
  }

  public void Update()
  {
    if (Time.timeScale <= 0.0)
      return;
    bool flag = vacuum.InGadgetMode();
    if (flag && img.sprite != gadgetSprite)
      img.sprite = gadgetSprite;
    else if (!flag && img.sprite != normalSprite)
      img.sprite = normalSprite;
    if (flag)
      img.color = gadgetTarget;
    else if (player.PointedAtVaccable)
      img.color = hasTarget;
    else
      img.color = noTarget;
    hudCrosshairScaleGoal = !flag ? (!vacuum.InVacMode() ? normScale : highScale) : gadgetScale;
    hudCrosshairScale += (float) ((hudCrosshairScaleGoal - (double) hudCrosshairScale) * 0.949999988079071 * Time.deltaTime * 4.0);
    if (vacuum.InVacMode() && hudCrosshairScaleGoal >= (double) highScale)
      hudCrosshairScale = highScale + Randoms.SHARED.GetInRange(0.0f, vibrateAmount);
    img.transform.localScale = new Vector3(hudCrosshairScale, hudCrosshairScale, hudCrosshairScale);
  }
}
