// Decompiled with JetBrains decompiler
// Type: TotemLinker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TotemLinker : RegisteredActorBehaviour, RegistryFixedUpdateable, RegistryUpdateable
{
  [Tooltip("Probability that a totem linker will be receptive to linking another.")]
  public float receptivenessProb = 0.25f;
  [Tooltip("Minimum game hours between rethinking whether we're receptive.")]
  public float rethinkReceptivenessMin = 6f;
  [Tooltip("Maximum game hours between rethinking whether we're receptive.")]
  public float rethinkReceptivenessMax = 12f;
  [Tooltip("How much to allow gravity to do its thing on slimes while totemed.")]
  public float gravFactorWhileTotemed = 0.5f;
  private TotemLinker linkTo;
  private TotemLinker linkedFrom;
  private Joint joint;
  private SlimeEmotions emotions;
  private Vacuumable vacuumable;
  private Rigidbody body;
  private TimeDirector timeDir;
  private Vector3 antiGrav;
  private float totemActiveTime;
  private bool totemActive;
  private double rethinkReceptivenessTime;
  private bool stackReceptive;
  private bool initted;
  private const float STACK_DIST = 0.8f;
  private const float HALF_STACK_DIST = 0.4f;
  private const float AGITATION_BREAK = 0.5f;
  private const float DELAY = 1f;
  private const int CIRCULAR_LINK_STEPS = 20;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    emotions = GetComponentInParent<SlimeEmotions>();
    vacuumable = GetComponentInParent<Vacuumable>();
    body = GetComponentInParent<Rigidbody>();
  }

  public override void Start()
  {
    base.Start();
    totemActiveTime = Time.time + 1f;
    antiGrav = Physics.gravity * (gravFactorWhileTotemed - 1f);
    initted = true;
  }

  public Rigidbody JointBody() => body;

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger)
      return;
    TotemLinker componentInChildren = col.GetComponentInChildren<TotemLinker>();
    if (!(componentInChildren != null) || !(componentInChildren != this) || !CanLink() || !componentInChildren.CanBeLinked() || componentInChildren.IndirectlyLinks(this, 20))
      return;
    RelinkTo(componentInChildren);
  }

  private bool IndirectlyLinks(TotemLinker checkLink, int checkSteps)
  {
    if (linkTo == null)
      return false;
    if (linkTo == checkLink)
      return true;
    if (checkSteps != 0)
      return linkTo.IndirectlyLinks(checkLink, checkSteps - 1);
    Log.Warning("Failed to complete check for circular totem link.");
    return false;
  }

  public void DisableToteming()
  {
    BreakLink();
    SetStackReceptive(false);
  }

  public void EnableToteming()
  {
    if (stackReceptive)
      return;
    rethinkReceptivenessTime = timeDir.WorldTime();
  }

  protected void RelinkTo(TotemLinker totem)
  {
    if (joint != null)
    {
      Destroyer.Destroy(joint, "TotemLinker.RelinkTo");
      joint = null;
    }
    linkTo = totem;
    if (!(totem != null))
      return;
    totem.LinkFrom(this);
    totem.SetStackReceptive(true);
    SpringJoint springJoint = JointBody().gameObject.AddComponent<SpringJoint>();
    totem.JointBody().MovePosition(transform.position);
    springJoint.autoConfigureConnectedAnchor = false;
    SafeJointReference.AttachSafely(totem.JointBody().gameObject, springJoint);
    springJoint.connectedAnchor = new Vector3(0.0f, -0.4f, 0.0f);
    springJoint.anchor = new Vector3(0.0f, 0.4f, 0.0f);
    springJoint.spring = 200f;
    springJoint.breakForce = 100f;
    joint = springJoint;
  }

  public void RegistryUpdate()
  {
    if (linkTo != null && (joint == null || !Linkable() || !stackReceptive))
      BreakLink();
    if (totemActive || Time.time < (double) totemActiveTime)
      return;
    totemActive = true;
  }

  public void UpdateEvenWhenInactive()
  {
    if (!initted)
      return;
    if (timeDir.HasReached(rethinkReceptivenessTime))
      SetStackReceptive(Randoms.SHARED.GetProbability(receptivenessProb));
    bool flag = CanLink();
    if (gameObject.activeSelf == flag)
      return;
    gameObject.SetActive(flag);
  }

  public void RegistryFixedUpdate()
  {
    if (!(linkedFrom != null))
      return;
    body.AddForce(antiGrav, ForceMode.Acceleration);
  }

  public bool IsLinkedFrom() => linkedFrom != null;

  public void SetStackReceptive(bool receptive)
  {
    if (stackReceptive != receptive)
      stackReceptive = receptive;
    rethinkReceptivenessTime = timeDir.HoursFromNowOrStart(Randoms.SHARED.GetInRange(rethinkReceptivenessMin, rethinkReceptivenessMax));
  }

  private void BreakLink()
  {
    if (linkedFrom != null && linkedFrom != linkTo)
      linkedFrom.RelinkTo(linkTo);
    else if (linkTo != null)
      linkTo.LinkFrom(null);
    linkTo = null;
    linkedFrom = null;
    if (!(joint != null))
      return;
    Destroyer.Destroy(joint, "TotemLinker.BreakLink");
    joint = null;
  }

  public bool CanLink() => stackReceptive && linkTo == null && Linkable();

  public bool CanBeLinked() => linkedFrom == null && Linkable();

  public void LinkFrom(TotemLinker from) => linkedFrom = from;

  private bool Linkable() => initted && (!(vacuumable == null) || !(emotions == null)) && (!(vacuumable != null) || !vacuumable.isCaptive()) && (!(emotions != null) || emotions.GetCurr(SlimeEmotions.Emotion.AGITATION) <= 0.5);
}
