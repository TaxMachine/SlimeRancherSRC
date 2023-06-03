// Decompiled with JetBrains decompiler
// Type: TentacleGrapple
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class TentacleGrapple : FindConsumable
{
  [Tooltip("The prefab to form the grappling tentacle.")]
  public GameObject tentaclePrefab;
  [Tooltip("Time between tentacles, in seconds.")]
  public float cooldown = 2f;
  [Tooltip("Whether being grappled should cause fear.")]
  public bool causeFear = true;
  [Tooltip("Heights above the slime to grapple from. If empty, will grapple from slime center")]
  public float[] heightsAboveToGrapple;
  [Tooltip("Whether to add slimes to our search list, with a constant drive")]
  public bool addSlimesToSearchList;
  [Tooltip("Whether we should ignore those currently grappling something else.")]
  public bool ignoreGrapplers;
  [Tooltip("Should we only do this behavior when grounded?")]
  public bool groundedOnly;
  private GameObject target;
  private bool grappling;
  private GameObject activeTentacle;
  private float hookTimeout;
  private const float HOOK_TIMEOUT = 10f;
  private float nextHookTime;
  private GameObject playerObj;
  private const float EXTRA_SLIME_SEARCH_DRIVE = 0.2f;

  public override void Awake()
  {
    base.Awake();
    nextHookTime = Time.time + cooldown;
  }

  public override void Start()
  {
    base.Start();
    playerObj = SRSingleton<SceneContext>.Instance.Player;
  }

  protected override Dictionary<Identifiable.Id, DriveCalculator> GetSearchIds()
  {
    Dictionary<Identifiable.Id, DriveCalculator> searchIds = base.GetSearchIds();
    if (addSlimesToSearchList)
    {
      foreach (Identifiable.Id key in Identifiable.SLIME_CLASS)
        searchIds[key] = new DriveCalculator(SlimeEmotions.Emotion.NONE, -0.8f, 0.0f);
      foreach (Identifiable.Id key in Identifiable.LARGO_CLASS)
        searchIds[key] = new DriveCalculator(SlimeEmotions.Emotion.NONE, -0.8f, 0.0f);
    }
    return searchIds;
  }

  public override float Relevancy(bool isGrounded)
  {
    if (Time.time < (double) nextHookTime || IsCaptive() || groundedOnly && !isGrounded)
      return 0.0f;
    float drive;
    target = FindNearestConsumable(out drive);
    if (ignoreGrapplers)
    {
      TentacleGrapple component = target.GetComponent<TentacleGrapple>();
      if (component != null && component.activeTentacle != null)
        return 0.0f;
    }
    return target == playerObj || target == null ? 0.0f : (float) (drive * (double) drive * 0.949999988079071);
  }

  public override void Action()
  {
    if (activeTentacle == null)
    {
      grappling = false;
      plexer.ForceRethink();
    }
    else if (Time.time >= (double) hookTimeout)
    {
      Destroyer.Destroy(activeTentacle, "TentacleGrapple.Action");
      grappling = false;
    }
    else
    {
      if (!(target != null) || !IsGrounded())
        return;
      RotateTowards((GetGotoPos(target) - transform.position).normalized);
    }
  }

  public override void Selected()
  {
    if (!(target != null) || !MaybeGrapple(target))
      return;
    grappling = true;
  }

  public override void Deselected()
  {
    base.Deselected();
    nextHookTime = Time.time + cooldown;
  }

  public override bool CanRethink() => !grappling;

  public bool IsGrappling(GameObject target) => grappling && target == this.target;

  private bool MaybeGrapple(GameObject target)
  {
    RaycastHit hitInfo = new RaycastHit();
    float num1 = 0.0f;
    float[] numArray = heightsAboveToGrapple;
    if (numArray == null || numArray.Length == 0)
      numArray = new float[1];
    foreach (float num2 in numArray)
    {
      Vector3 origin = transform.position + Vector3.up * num2;
      Vector3 direction = GetGotoPos(target) - origin;
      Physics.Raycast(origin, direction, out hitInfo, direction.magnitude);
      if (hitInfo.collider != null && hitInfo.collider.gameObject == target)
      {
        num1 = num2;
        break;
      }
    }
    if (hitInfo.collider == null || hitInfo.collider.gameObject != target || TentacleHook.IsAlreadyHooked(hitInfo.collider.gameObject))
      return false;
    GameObject gameObject1 = Instantiate(tentaclePrefab);
    Attachment component = gameObject1.GetComponent<Attachment>();
    gameObject1.transform.SetParent(transform, false);
    GameObject gameObject2 = gameObject;
    GameObject target1 = target;
    Vector3 point = hitInfo.point;
    int num3 = causeFear ? 1 : 0;
    double intermediateHeight = num1;
    component.Init(gameObject2, target1, point, num3 != 0, (float) intermediateHeight);
    activeTentacle = gameObject1;
    hookTimeout = Time.time + 10f;
    return true;
  }

  public void Release()
  {
    if (!(activeTentacle != null))
      return;
    Destroyer.Destroy(activeTentacle, "TentacleGrapple.Release");
  }
}
