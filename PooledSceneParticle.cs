// Decompiled with JetBrains decompiler
// Type: PooledSceneParticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PooledSceneParticle : SRBehaviour
{
  public GameObject particlePrefab;
  [Tooltip("Any animators we need to inform that we've messed with the hierarchy")]
  public Animator[] animsToRebind;
  protected GameObject particle;
  private bool initialized;
  private bool isShuttingDown;

  public void Awake() => SRSingleton<SceneContext>.Instance.SceneParticleDirector.AddSecondFrameListener(this);

  public void OnEnable()
  {
    if (!initialized)
      return;
    InitParticle();
  }

  public void OnSecondFrame()
  {
    initialized = true;
    if (!isActiveAndEnabled)
      return;
    InitParticle();
  }

  protected virtual void InitParticle()
  {
    if (!(particlePrefab != null) || !(particle == null))
      return;
    particle = SpawnAndPlayFX(particlePrefab, gameObject);
    foreach (Animator animator in animsToRebind)
      animator.Rebind();
  }

  public void OnApplicationQuit() => isShuttingDown = true;

  public void OnDisable()
  {
    if (!(particle != null) || isShuttingDown)
      return;
    if (SRSingleton<SceneContext>.Instance != null && SRSingleton<SceneContext>.Instance.fxPool != null)
    {
      SRSingleton<SceneContext>.Instance.fxPool.RecycleAfterFrame(particle);
      particle = null;
    }
    foreach (Animator animator in animsToRebind)
      animator.Rebind();
  }

  public ParticleSystem GetParticleSystem() => !(particle != null) ? null : particle.GetComponent<ParticleSystem>();
}
