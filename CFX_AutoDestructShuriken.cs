// Decompiled with JetBrains decompiler
// Type: CFX_AutoDestructShuriken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
  public bool OnlyDeactivate;
  public bool RecycleOnCompletion;
  public bool RecycleParent;
  private float nextCheckTime;
  private float endTime;
  private const float CHECK_DELAY = 0.5f;
  private ParticlesRunWhilePaused particlesRunWhilePaused;
  private const float LIFETIME_SAFETY_MARGIN = 1.5f;

  public void Awake() => particlesRunWhilePaused = GetComponent<ParticlesRunWhilePaused>();

  public void OnEnable()
  {
    ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
    endTime = main.loop ? float.PositiveInfinity : GetTime() + main.duration * 1.5f;
  }

  private float GetTime() => particlesRunWhilePaused != null && particlesRunWhilePaused.enabled ? Time.unscaledTime : Time.time;

  public void Update()
  {
    float time = GetTime();
    if (nextCheckTime > (double) time)
      return;
    if (endTime <= (double) GetTime() || !GetComponent<ParticleSystem>().IsAlive(true))
    {
      if (OnlyDeactivate)
        gameObject.SetActive(false);
      else if (RecycleOnCompletion)
      {
        if (RecycleParent)
          SRSingleton<SceneContext>.Instance.fxPool.Recycle(transform.parent.gameObject);
        else
          SRSingleton<SceneContext>.Instance.fxPool.Recycle(gameObject);
      }
      else
        Destroyer.Destroy(gameObject, "CFX_AutoDestructShuriken.Update");
    }
    else
      nextCheckTime = time + 0.5f;
  }
}
