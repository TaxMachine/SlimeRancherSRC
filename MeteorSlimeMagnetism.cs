// Decompiled with JetBrains decompiler
// Type: MeteorSlimeMagnetism
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSlimeMagnetism : SlimeSubbehaviour
{
  public float attractPower = 600f;
  public float attractRadius = 12f;
  public GameObject attractFX;
  public GameObject lowGravFX;
  private float nextPossibleAttract;
  private float nextExplodeDelayTime = 45f;
  private float nextRecoverTime;
  private SlimeFaceAnimator sfAnimator;
  private List<Rigidbody> attractees = new List<Rigidbody>();
  private const float ATTRACT_MIN_DELAY = 10f;
  private const float ATTRACT_MAX_DELAY = 45f;
  private const float ATTRACT_PREP_TIME = 1.5f;
  private const float ATTRACT_RECOVERY_TIME = 5f;
  private const float LOW_GRAV_TIME = 10f;
  private const float LOW_GRAV_FACTOR = 0.2f;
  private const float LOW_GRAV_RAD = 2f;
  private const float MAGNETIC_MASS_FACTOR = 100f;
  private int attracteeMask;
  private State state;

  public override void Awake()
  {
    base.Awake();
    attracteeMask = LayerMask.GetMask("Actor", "ActorIgnorePlayer");
    sfAnimator = GetComponent<SlimeFaceAnimator>();
  }

  public override void Start()
  {
    base.Start();
    nextExplodeDelayTime = AttractDelay();
    nextPossibleAttract = Time.time + nextExplodeDelayTime * Randoms.SHARED.GetInRange(0.25f, 1f);
    GetComponentsInChildren<ExplodeIndicatorMarker>(true)[0].SetActive(false);
  }

  public override float Relevancy(bool isGrounded) => Time.time <= (double) nextPossibleAttract ? 0.0f : 1f;

  public override void Action()
  {
  }

  public override void Selected() => StartCoroutine(DelayedAttract());

  public override void Deselected() => base.Deselected();

  private float AttractDelay() => Mathf.Lerp(10f, 45f, Mathf.Clamp(Randoms.SHARED.GetInRange(-0.1f, 0.1f) + (1f - emotions.GetCurr(SlimeEmotions.Emotion.AGITATION)), 0.0f, 1f));

  private IEnumerator DelayedAttract()
  {
    MeteorSlimeMagnetism meteorSlimeMagnetism = this;
    meteorSlimeMagnetism.state = State.PREPARING;
    meteorSlimeMagnetism.GetComponentsInChildren<ExplodeIndicatorMarker>(true)[0].SetActive(true);
    meteorSlimeMagnetism.sfAnimator.SetTrigger("triggerGrimace");
    float originalMass = meteorSlimeMagnetism.slimeBody.mass;
    meteorSlimeMagnetism.slimeBody.mass *= 100f;
    yield return new WaitForSeconds(1.5f);
    meteorSlimeMagnetism.GetComponentsInChildren<ExplodeIndicatorMarker>(true)[0].SetActive(false);
    meteorSlimeMagnetism.state = State.ATTRACTING;
    meteorSlimeMagnetism.FindAttractees();
    SpawnAndPlayFX(meteorSlimeMagnetism.attractFX, meteorSlimeMagnetism.transform.position, meteorSlimeMagnetism.transform.rotation);
    meteorSlimeMagnetism.nextExplodeDelayTime = meteorSlimeMagnetism.AttractDelay();
    meteorSlimeMagnetism.nextPossibleAttract = Time.time + meteorSlimeMagnetism.nextExplodeDelayTime;
    meteorSlimeMagnetism.state = State.RECOVERING;
    meteorSlimeMagnetism.sfAnimator.SetTrigger("triggerFried");
    meteorSlimeMagnetism.nextRecoverTime = Time.time + 5f;
    yield return new WaitForSeconds(5f);
    meteorSlimeMagnetism.attractees.Clear();
    meteorSlimeMagnetism.slimeBody.mass = originalMass;
    meteorSlimeMagnetism.state = State.IDLE;
  }

  public void FixedUpdate()
  {
    if (state != State.RECOVERING && state != State.ATTRACTING)
      return;
    Attract();
  }

  private void FindAttractees()
  {
    Vector3 position = transform.position;
    HashSet<GameObject> gameObjectSet = new HashSet<GameObject>();
    double attractRadius = this.attractRadius;
    int attracteeMask = this.attracteeMask;
    foreach (Collider collider in Physics.OverlapSphere(position, (float) attractRadius, attracteeMask, QueryTriggerInteraction.Ignore))
    {
      if (collider != null)
      {
        Rigidbody component = collider.GetComponent<Rigidbody>();
        GameObject gameObject = collider.gameObject;
        if (component != null && gameObject != this.gameObject && !gameObjectSet.Contains(gameObject))
        {
          if (gameObject != SRSingleton<SceneContext>.Instance.Player)
            attractees.Add(component);
          gameObjectSet.Add(gameObject);
        }
      }
    }
  }

  private void Attract()
  {
    Vector3 position = transform.position;
    float num = attractPower * Time.fixedDeltaTime;
    for (int index = 0; index < attractees.Count; ++index)
    {
      Rigidbody attractee = attractees[index];
      if (!(attractee == null))
        attractee.AddExplosionForce(-num, position, attractRadius);
    }
  }

  private void ApplyLowGravChargesNearby()
  {
    Vector3 position = transform.position;
    HashSet<GameObject> gameObjectSet = new HashSet<GameObject>();
    foreach (Collider collider in Physics.OverlapSphere(position, 2f))
    {
      if (collider != null && !collider.isTrigger)
      {
        Rigidbody component = collider.GetComponent<Rigidbody>();
        GameObject gameObject = collider.gameObject;
        if (component != null && gameObject != this.gameObject && !gameObjectSet.Contains(gameObject))
        {
          if (gameObject != SRSingleton<SceneContext>.Instance.Player)
            gameObject.AddComponent<LowGravity>().SetLowGrav(0.2f, lowGravFX);
          gameObjectSet.Add(gameObject);
        }
      }
    }
  }

  public override bool CanRethink() => state == State.IDLE;

  public float GetReadiness() => 1f - Mathf.Clamp((nextPossibleAttract - Time.time) / nextExplodeDelayTime, 0.0f, 1f);

  public float GetRecoveriness() => state != State.RECOVERING ? 0.0f : Mathf.Clamp((float) ((nextRecoverTime - (double) Time.time) / 5.0), 0.0f, 1f);

  private enum State
  {
    IDLE,
    PREPARING,
    ATTRACTING,
    RECOVERING,
  }

  private class LowGravity : SRBehaviour
  {
    private Rigidbody body;
    private Vector3 antiGrav;
    private float deathTime;
    private GameObject lowGravFX;

    public void Awake()
    {
      body = GetComponent<Rigidbody>();
      deathTime = Time.time + 10f;
    }

    public void SetLowGrav(float factor, GameObject fxPrefab)
    {
      antiGrav = Physics.gravity * (factor - 1f);
      lowGravFX = Instantiate(fxPrefab);
      lowGravFX.transform.SetParent(transform, false);
    }

    public void OnDestroy() => Destroyer.Destroy(lowGravFX, "MeteorSlimeMagnetism.OnDestroy");

    public void FixedUpdate()
    {
      if (Time.fixedTime >= (double) deathTime)
        Destroyer.Destroy(this, "MeteorSlimeMagnetism.FixedUpdate");
      else
        body.AddForce(antiGrav, ForceMode.Acceleration);
    }
  }
}
