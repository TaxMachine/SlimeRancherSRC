// Decompiled with JetBrains decompiler
// Type: FeralSlimeButtstomp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FeralSlimeButtstomp : SlimeSubbehaviour, Collidable
{
  public GameObject stompFX;
  public float explodePower = 600f;
  public float explodeRadius = 7f;
  public float minPlayerDamage = 15f;
  public float maxPlayerDamage = 45f;
  private Mode mode;
  private float nextStompTime;
  private SlimeFeral feral;
  private SlimeAudio slimeAudio;
  private GameModeConfig theGameModeConfig;
  private const float MAX_DIST = 20f;
  private const float MAX_DIST_SQR = 400f;
  private const float MIN_DIST = 5f;
  private const float MIN_DIST_SQR = 25f;
  private const float STOMP_RESET_TIME = 5f;
  private const float PLAYER_FORCE_FACTOR = 0.001f;
  private const float UNDERNEATH_THRESHOLD = 0.5f;

  public override void Awake()
  {
    base.Awake();
    feral = GetComponent<SlimeFeral>();
    slimeAudio = GetComponent<SlimeAudio>();
    theGameModeConfig = SRSingleton<SceneContext>.Instance.GameModeConfig;
  }

  public override float Relevancy(bool isGrounded)
  {
    if (isGrounded && feral.IsFeral() && !theGameModeConfig.GetModeSettings().preventHostiles && Time.time >= (double) nextStompTime)
    {
      float sqrMagnitude = (GetGotoPos(SRSingleton<SceneContext>.Instance.Player) - transform.position).sqrMagnitude;
      if (sqrMagnitude <= 400.0 && sqrMagnitude >= 25.0)
        return Randoms.SHARED.GetInRange(0.3f, 1f);
    }
    return 0.0f;
  }

  public override bool CanRethink() => mode == Mode.WAITING || mode == Mode.LANDED;

  public override void Selected() => mode = Mode.WAITING;

  public override void Action()
  {
    switch (mode)
    {
      case Mode.WAITING:
        LaunchAction();
        break;
      case Mode.MIDAIR:
        MidairAction();
        break;
      case Mode.WAIT_FOR_GROUND_IMPACT:
        if (!plexer.IsFloating())
          break;
        mode = Mode.STOMPING;
        break;
      case Mode.STOMPING:
        StompingAction();
        break;
    }
  }

  private void LaunchAction()
  {
    Vector3 vector3 = SRSingleton<SceneContext>.Instance.Player.transform.TransformPoint(new Vector3(0.0f, 0.0f, 2f)) - transform.position;
    double sqrMagnitude = vector3.sqrMagnitude;
    Vector3 normalized = vector3.normalized;
    RotateTowards(normalized, 1f, 5f);
    float num1 = 1.2f;
    float num2 = 1.4f;
    float num3 = Mathf.Sqrt(Mathf.Sqrt((float) sqrMagnitude) * Physics.gravity.magnitude) * num1 * num2;
    slimeBody.AddForce((normalized + Vector3.up).normalized * num3, ForceMode.VelocityChange);
    slimeAudio.Play(slimeAudio.slimeSounds.jumpCue);
    slimeAudio.Play(slimeAudio.slimeSounds.voiceJumpCue);
    slimeAudio.Play(slimeAudio.slimeSounds.stompJumpCue);
    mode = Mode.MIDAIR;
  }

  private void MidairAction()
  {
    if (slimeBody.velocity.y > 0.0)
      return;
    slimeBody.velocity = new Vector3(0.0f, -slimeBody.velocity.magnitude, 0.0f);
    mode = Mode.WAIT_FOR_GROUND_IMPACT;
  }

  private void StompingAction()
  {
    if (stompFX != null)
      SpawnAndPlayFX(stompFX, transform.position, transform.rotation);
    slimeAudio.Play(slimeAudio.slimeSounds.stompLandCue);
    Explode();
    mode = Mode.LANDED;
    nextStompTime = Time.time + 5f;
  }

  private void Explode() => PhysicsUtil.Explode(gameObject, explodeRadius, explodePower, minPlayerDamage, maxPlayerDamage);

  public void ProcessCollisionEnter(Collision col)
  {
    if (mode != Mode.WAIT_FOR_GROUND_IMPACT || transform.position.y - (double) col.contacts[0].point.y < 0.5)
      return;
    mode = Mode.STOMPING;
  }

  public void ProcessCollisionExit(Collision col)
  {
  }

  private enum Mode
  {
    WAITING,
    MIDAIR,
    WAIT_FOR_GROUND_IMPACT,
    STOMPING,
    LANDED,
  }
}
