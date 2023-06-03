// Decompiled with JetBrains decompiler
// Type: SENBDLGlowingOrbitingCube
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SENBDLGlowingOrbitingCube : MonoBehaviour
{
  private float pulseSpeed;
  private float phase;

  private Vector3 Vec3(float x) => new Vector3(x, x, x);

  private void Start()
  {
    transform.localScale = Vec3(1.5f);
    pulseSpeed = Random.Range(4f, 8f);
    phase = Random.Range(0.0f, 6.28318548f);
  }

  private void Update()
  {
    Color glowColor = SENBDLGlobal.mainCube.glowColor;
    glowColor.r = 1f - glowColor.r;
    glowColor.g = 1f - glowColor.g;
    glowColor.b = 1f - glowColor.b;
    Color color = Color.Lerp(glowColor, Color.white, 0.1f) * Mathf.Pow((float) (Mathf.Sin(Time.time * pulseSpeed + phase) * 0.49000000953674316 + 0.50999999046325684), 2f);
    GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    GetComponent<Light>().color = color;
  }
}
