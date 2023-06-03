// Decompiled with JetBrains decompiler
// Type: AirNet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AirNet : MonoBehaviour
{
  public int hitForceToDestroy = 300;
  public float hoursToStartRecovery = 0.1f;
  public float hoursToRecover = 0.1f;
  public Color fullColor = Color.white;
  public Color brokenColor = Color.red;
  private TimeDirector timeDir;
  private Collider netCollider;
  private Material netMaterial;
  private float netStrength = 1f;
  private double recoverStartTime;
  private float dmgPerImpulse;
  private float recoverFactor;
  private const float NEW_NET_STRENGTH = 0.33f;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    netCollider = GetComponent<Collider>();
    netMaterial = GetComponent<Renderer>().material;
    dmgPerImpulse = 1f / hitForceToDestroy;
    recoverFactor = (float) (1.0 / (hoursToRecover * 3600.0));
  }

  public void OnDestroy() => Destroyer.Destroy(netMaterial, "AirNet.OnDestroy");

  public void OnCollisionEnter(Collision col)
  {
    if (!Identifiable.IsSlime(Identifiable.GetId(col.gameObject)))
      return;
    netStrength = Mathf.Max(0.0f, netStrength - col.impulse.magnitude * dmgPerImpulse);
    recoverStartTime = timeDir.HoursFromNow(hoursToRecover);
  }

  public void Update()
  {
    if (netStrength < 1.0 && timeDir.HasReached(recoverStartTime))
      netStrength = Mathf.Clamp(netStrength + (float) timeDir.DeltaWorldTime() * recoverFactor, 0.33f, 1f);
    netCollider.enabled = netStrength > 0.0;
    netMaterial.color = CurrColor();
  }

  private Color CurrColor() => netStrength <= 0.0 ? Color.clear : Color.Lerp(brokenColor, fullColor, netStrength);

  public bool IsNetActive() => gameObject.activeInHierarchy && netStrength > 0.0;
}
