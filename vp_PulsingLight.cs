// Decompiled with JetBrains decompiler
// Type: vp_PulsingLight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_PulsingLight : MonoBehaviour
{
  private Light m_Light;
  public float m_MinIntensity = 2f;
  public float m_MaxIntensity = 5f;
  public float m_Rate = 1f;

  private void Start() => m_Light = GetComponent<Light>();

  private void Update()
  {
    if (m_Light == null)
      return;
    m_Light.intensity = m_MinIntensity + Mathf.Abs(Mathf.Cos(Time.time * m_Rate) * (m_MaxIntensity - m_MinIntensity));
  }
}
