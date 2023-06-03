// Decompiled with JetBrains decompiler
// Type: FireBall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FireBall : SRBehaviour
{
  public int bouncesToDie = 3;
  public float hoursToLive = 0.1f;
  private int numBounces;
  private double timeToDie;
  protected bool defused;
  public GameObject vaporizeFx;
  public GameObject expireFX;
  public SECTR_AudioCue spawnCue;
  public SECTR_AudioCue bounceCue;
  public SECTR_AudioCue shatterCue;
  private TimeDirector timeDir;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    timeToDie = timeDir.HoursFromNow(hoursToLive);
  }

  public void OnEnable() => Reset();

  private void Reset()
  {
    timeToDie = timeDir.HoursFromNow(hoursToLive);
    SECTR_AudioSystem.Play(spawnCue, transform.position, false);
    numBounces = 0;
    defused = false;
  }

  public void Update()
  {
    if (!HasBouncedTooMuch() && !timeDir.HasReached(timeToDie))
      return;
    OnExpire();
    RequestDestroy("FireBall.Update");
  }

  private bool HasBouncedTooMuch() => bouncesToDie > 0 && numBounces >= bouncesToDie;

  protected virtual void OnExpire()
  {
    if (!(expireFX != null))
      return;
    SpawnAndPlayFX(expireFX, transform.position, transform.rotation);
  }

  public void OnCollisionEnter(Collision col)
  {
    if (name.Contains("Fireball"))
      Log.Warning("Bounced!", "this.name", name, "col.name", col.gameObject.name);
    ++numBounces;
    Ignitable component = col.gameObject.GetComponent<Ignitable>();
    SECTR_AudioSystem.Play(bounceCue, transform.position, false);
    component?.Ignite(gameObject);
  }

  public void Vaporize()
  {
    DefuseAndDestroy();
    SECTR_AudioSystem.Play(shatterCue, transform.position, false);
    if (!(vaporizeFx != null))
      return;
    SpawnAndPlayFX(vaporizeFx, gameObject.transform.position, Quaternion.identity);
  }

  public void DefuseAndDestroy()
  {
    defused = true;
    RequestDestroy("FireBall.DefuseAndDestroy");
  }
}
