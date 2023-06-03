// Decompiled with JetBrains decompiler
// Type: GatherIdentifiableItems
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class GatherIdentifiableItems : FindConsumable
{
  [Tooltip("The item types we will gather.")]
  public ItemClass[] itemClasses;
  public float maxJump = 6f;
  public float pauseBetweenGathers = 10f;
  public float minGatherDist = 5f;
  private GameObject target;
  private Vector3? gatherToPos;
  private FixedJoint joint;
  private float nextLeapAvail;
  private float giveUpOnGatherTime;
  private float disallowSelectionUntil;
  private bool isAttached;
  private static HashSet<GameObject> allGathering = new HashSet<GameObject>();
  private const float GIVE_UP_GATHER_TIME = 10f;
  private const float CLOSE_ENOUGH = 1f;
  private const float CLOSE_ENOUGH_SQR = 1f;
  private const float GATHER_RAD = 3f;
  private const float GATHER_RAD_SQR = 9f;
  private List<GameObject> nearbyGameObjectsLst = new List<GameObject>();

  public override void Awake() => base.Awake();

  public override float Relevancy(bool isGrounded)
  {
    Release();
    if (Time.time >= (double) disallowSelectionUntil)
      target = FindNearestConsumable(out float _);
    return !(target == null) ? Randoms.SHARED.GetInRange(0.3f, 0.5f) : 0.0f;
  }

  public override void Selected() => giveUpOnGatherTime = Time.time + 10f;

  public override void Action()
  {
    if (target == null || joint == null && isAttached || Time.time > (double) giveUpOnGatherTime || vacuumable.isHeld())
      Release();
    else if (joint == null)
    {
      Vector3 gotoPos = GetGotoPos(target);
      bool shouldJump = IsBlocked(target);
      MoveTowards(gotoPos, shouldJump, ref nextLeapAvail, maxJump);
      if ((gotoPos - transform.position).sqrMagnitude > 1.0 * transform.localScale.z * transform.localScale.z)
        return;
      if (shouldJump || !(gatherToPos = GetGatherToPosition()).HasValue || !allGathering.Add(target))
      {
        Release();
      }
      else
      {
        joint = SlimeUtil.AttachToMouth(gameObject, target);
        giveUpOnGatherTime = Time.time + 10f;
        isAttached = true;
        slimeAudio.Play(slimeAudio.slimeSounds.gatherCue);
      }
    }
    else if ((gatherToPos.Value - transform.position).sqrMagnitude <= 9.0)
    {
      Release();
    }
    else
    {
      Rigidbody component = GetComponent<Rigidbody>();
      MoveTowards(gatherToPos.Value, true, ref nextLeapAvail, maxJump * (target.GetComponent<Rigidbody>().mass + component.mass) / component.mass);
      RotateTowards(gatherToPos.Value - transform.position);
    }
  }

  private Vector3? GetGatherToPosition()
  {
    Identifiable component = target.GetComponent<Identifiable>();
    GameObject itemNotOfType = component == null ? null : FindItemNotOfType(component.id, maxSearchRad);
    return !(itemNotOfType == null) ? new Vector3?(GetGotoPos(itemNotOfType)) : new Vector3?();
  }

  private void Release()
  {
    Destroyer.Destroy(joint, "GatherIdentifiableItems.Release");
    joint = null;
    if (isAttached)
      allGathering.Remove(target);
    target = null;
    isAttached = false;
  }

  public override bool CanRethink() => !isAttached;

  public override void Deselected()
  {
    base.Deselected();
    Release();
    disallowSelectionUntil = Time.time + pauseBetweenGathers;
  }

  public override void OnDisable()
  {
    base.OnDisable();
    Release();
  }

  protected override Dictionary<Identifiable.Id, DriveCalculator> GetSearchIds()
  {
    Dictionary<Identifiable.Id, DriveCalculator> searchIds = new Dictionary<Identifiable.Id, DriveCalculator>(Identifiable.idComparer);
    foreach (ItemClass itemClass in itemClasses)
    {
      foreach (Identifiable.Id searchId in GetSearchIds(itemClass))
        searchIds[searchId] = new DriveCalculator(SlimeEmotions.Emotion.NONE, 0.0f, 0.0f);
    }
    return searchIds;
  }

  private static ICollection<Identifiable.Id> GetSearchIds(
    ItemClass itemClass)
  {
    if (itemClass == ItemClass.FRUIT)
      return Identifiable.FRUIT_CLASS;
    return itemClass == ItemClass.VEGGIE ? Identifiable.VEGGIE_CLASS : (ICollection<Identifiable.Id>) new HashSet<Identifiable.Id>();
  }

  protected GameObject FindItemNotOfType(Identifiable.Id ineligibleId, float maxDist)
  {
    float num1 = maxDist * maxDist;
    List<GameObject> iterable = new List<GameObject>();
    foreach (KeyValuePair<Identifiable.Id, DriveCalculator> searchId in searchIds)
    {
      if (searchId.Key != ineligibleId)
      {
        nearbyGameObjectsLst.Clear();
        CellDirector.Get(searchId.Key, member, nearbyGameObjectsLst);
        Vector3 position = transform.position;
        float num2 = minGatherDist * minGatherDist;
        for (int index = 0; index < nearbyGameObjectsLst.Count; ++index)
        {
          GameObject gameObject = nearbyGameObjectsLst[index];
          if (Identifiable.IsEdible(gameObject))
          {
            float sqrMagnitude = (GetGotoPos(gameObject) - position).sqrMagnitude;
            if (sqrMagnitude <= (double) num1 && sqrMagnitude >= (double) num2)
              iterable.Add(gameObject);
          }
        }
      }
    }
    nearbyGameObjectsLst.Clear();
    return Randoms.SHARED.Pick(iterable, null);
  }

  public enum ItemClass
  {
    FRUIT,
    VEGGIE,
  }
}
