// Decompiled with JetBrains decompiler
// Type: DeathObscurer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class DeathObscurer : MonoBehaviour
{
  private Image bgImage;
  private float targetAlpha;
  private float adjust;

  public void Start()
  {
    SRSingleton<LockOnDeath>.Instance.onLockChanged += OnLocked;
    bgImage = GetComponent<Image>();
    adjust = 1f;
    SRSingleton<SceneContext>.Instance.PlayerState.onEndGame += () =>
    {
      bgImage.color = bgImage.color with
      {
        a = 0.0f
      };
      targetAlpha = 0.0f;
    };
  }

  public void OnLocked(bool locked)
  {
    if (locked)
    {
      targetAlpha = 1f;
      gameObject.SetActive(true);
    }
    else
      targetAlpha = 0.0f;
  }

  public void Update()
  {
    float a = bgImage.color.a;
    if (a < (double) targetAlpha)
      bgImage.color = new Color(bgImage.color.r, bgImage.color.g, bgImage.color.b, Mathf.Min(targetAlpha, a + adjust * Time.deltaTime));
    else if (a > (double) targetAlpha)
      bgImage.color = new Color(bgImage.color.r, bgImage.color.g, bgImage.color.b, Mathf.Max(targetAlpha, a - adjust * Time.deltaTime));
    if (bgImage.color.a != 0.0)
      return;
    gameObject.SetActive(false);
  }
}
