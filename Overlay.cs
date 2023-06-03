// Decompiled with JetBrains decompiler
// Type: Overlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Overlay : SRSingleton<Overlay>
{
  public GameObject teleportFX;
  public GameObject damageFX;
  public GameObject chompFX;
  public GameObject radFX;
  public GameObject firestormFX;
  public GameObject gadgetFX;
  [Tooltip("FX played while the dash pad is active.")]
  public GameObject dashPadFX;
  private GameObject activeRadFX;
  private Material activeRadMat;
  private float tgtRadAlpha;
  private GameObject activeFirestormFX;
  private Material activeFirestormMat;
  private float tgtFirestormAlpha;
  private GameObject activeGadgetFX;
  private Material activeGadgetMat;
  private float tgtGadgetAlpha;
  private const float RAD_ALPHA_DELTA = 2f;
  private const float FIRESTORM_ALPHA_DELTA = 2f;
  private const float GADGET_ALPHA_DELTA = 2f;

  public void Update()
  {
    if (activeRadFX != null)
    {
      Color color = activeRadMat.color;
      float num = color.a;
      if (tgtRadAlpha > (double) num)
        num = Mathf.Min(tgtRadAlpha, num + 2f * Time.deltaTime);
      else if (tgtRadAlpha < (double) num)
        num = Mathf.Max(tgtRadAlpha, num - 2f * Time.deltaTime);
      color.a = num;
      activeRadMat.color = color;
      if (num <= 0.0)
      {
        Destroyer.Destroy(activeRadFX, "Overlay.Update#1");
        Destroyer.Destroy(activeRadMat, "Overlay.Update#2");
        activeRadFX = null;
      }
    }
    if (activeFirestormFX != null)
    {
      Color color = activeFirestormMat.color;
      float num = color.a;
      if (tgtFirestormAlpha > (double) num)
        num = Mathf.Min(tgtFirestormAlpha, num + 2f * Time.deltaTime);
      else if (tgtFirestormAlpha < (double) num)
        num = Mathf.Max(tgtFirestormAlpha, num - 2f * Time.deltaTime);
      color.a = num;
      activeFirestormMat.color = color;
      if (num <= 0.0)
      {
        Destroyer.Destroy(activeFirestormFX, "Overlay.Update#3");
        Destroyer.Destroy(activeFirestormMat, "Overlay.Update#4");
        activeFirestormFX = null;
      }
    }
    if (!(activeGadgetFX != null))
      return;
    Color color1 = activeGadgetMat.color;
    float num1 = color1.a;
    if (tgtGadgetAlpha > (double) num1)
      num1 = Mathf.Min(tgtGadgetAlpha, num1 + 2f * Time.deltaTime);
    else if (tgtGadgetAlpha < (double) num1)
      num1 = Mathf.Max(tgtGadgetAlpha, num1 - 2f * Time.deltaTime);
    color1.a = num1;
    activeGadgetMat.color = color1;
    if (num1 > 0.0)
      return;
    Destroyer.Destroy(activeGadgetFX, "Overlay.Update#5");
    Destroyer.Destroy(activeGadgetMat, "Overlay.Update#6");
    activeGadgetFX = null;
  }

  public void PlayTeleport() => Play(teleportFX);

  public void PlayDamage() => Play(damageFX);

  public void PlayChomp() => Play(chompFX);

  public void SetEnableRad(bool enabled)
  {
    tgtRadAlpha = enabled ? 1f : 0.0f;
    if (!enabled || !(activeRadFX == null))
      return;
    activeRadFX = Play(radFX);
    activeRadMat = activeRadFX.GetComponent<Renderer>().material;
    activeRadMat.color = activeRadMat.color with
    {
      a = 0.0f
    };
  }

  public void SetEnableFirestorm(bool enabled)
  {
    tgtFirestormAlpha = enabled ? 1f : 0.0f;
    if (!enabled || !(activeFirestormFX == null))
      return;
    activeFirestormFX = Play(firestormFX);
    activeFirestormMat = activeFirestormFX.GetComponent<Renderer>().material;
    activeFirestormMat.color = activeFirestormMat.color with
    {
      a = 0.0f
    };
  }

  public void SetEnableGadgetMode(bool enabled)
  {
    tgtGadgetAlpha = enabled ? 1f : 0.0f;
    if (!enabled || !(activeGadgetFX == null))
      return;
    activeGadgetFX = Play(gadgetFX);
    activeGadgetMat = activeGadgetFX.GetComponent<Renderer>().material;
    activeGadgetMat.color = activeGadgetMat.color with
    {
      a = 0.0f
    };
  }

  public GameObject Play(GameObject fxOrig)
  {
    GameObject fxObject = Instantiate(fxOrig, transform.position, transform.rotation);
    fxObject.transform.parent = transform;
    PlayFX(fxObject);
    return fxObject;
  }
}
