// Decompiled with JetBrains decompiler
// Type: ImageCycler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Image))]
public class ImageCycler : MonoBehaviour
{
  public Sprite[] sprites;
  [Tooltip("Seconds between frames.")]
  public float timePerFrame = 1f;
  private Image img;
  private float flipTime;
  private int idx;

  public void Awake()
  {
    img = GetComponent<Image>();
    if (sprites == null)
      return;
    SetSprites(sprites);
  }

  public void OnEnable() => flipTime = Time.time;

  public void SetSprites(Sprite[] sprites)
  {
    this.sprites = sprites;
    if (sprites.Length != 0)
    {
      idx = 0;
      img.sprite = sprites[idx];
    }
    flipTime = Time.time + timePerFrame;
  }

  public void Update()
  {
    if (sprites.Length < 2 || Time.time <= (double) flipTime)
      return;
    idx = (idx + 1) % sprites.Length;
    img.sprite = sprites[idx];
    flipTime += timePerFrame;
  }
}
