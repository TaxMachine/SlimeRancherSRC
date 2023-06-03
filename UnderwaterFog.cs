// Decompiled with JetBrains decompiler
// Type: UnderwaterFog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UnderwaterFog : MonoBehaviour
{
  private Color normalColor;
  private float normalDensity;
  private Color underwaterColor;
  private bool isUnderwater;

  private void Start()
  {
    normalColor = RenderSettings.fogColor;
    normalDensity = RenderSettings.fogDensity;
    underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
  }

  private void Update()
  {
    if (GetComponent<Collider>().bounds.Contains(Camera.main.transform.position))
      SetUnderwater();
    else
      SetNormal();
  }

  private void SetNormal()
  {
    if (!isUnderwater)
      return;
    RenderSettings.fogColor = normalColor;
    RenderSettings.fogDensity = normalDensity;
    isUnderwater = false;
  }

  private void SetUnderwater()
  {
    if (isUnderwater)
      return;
    RenderSettings.fogColor = underwaterColor;
    RenderSettings.fogDensity = 0.05f;
    isUnderwater = true;
  }
}
