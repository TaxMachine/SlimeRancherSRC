// Decompiled with JetBrains decompiler
// Type: AweTowardsLargos
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class AweTowardsLargos : FindConsumable
{
  private GameObject target;
  private static DriveCalculator largoDriveCalc = new DriveCalculator(SlimeEmotions.Emotion.NONE, 0.0f, 0.0f);
  private TimeDirector timeDir;
  private SlimeFaceAnimator sfAnimator;
  private double nextActivationTime;
  private float endTime;
  private static List<GameObjectActorModelIdentifiableIndex.Entry> localStaticLargoEntries = new List<GameObjectActorModelIdentifiableIndex.Entry>();

  public override void Awake()
  {
    base.Awake();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    sfAnimator = GetComponent<SlimeFaceAnimator>();
  }

  public override float Relevancy(bool isGrounded)
  {
    if (!isGrounded || !timeDir.HasReached(nextActivationTime))
      return 0.0f;
    localStaticLargoEntries.Clear();
    CellDirector.GetLargosNearMember(member, localStaticLargoEntries);
    target = FindNearestConsumable(localStaticLargoEntries, out float _);
    return !(target == null) ? Randoms.SHARED.GetInRange(0.1f, 1f) : 0.0f;
  }

  public override void Action()
  {
    if (!(target != null))
      return;
    RotateTowards(GetGotoPos(target) - transform.position);
  }

  public override void Selected()
  {
    sfAnimator.SetTrigger("triggerLongAwe");
    nextActivationTime = timeDir.HoursFromNow(1f);
    endTime = Time.time + 3f;
  }

  public override bool CanRethink() => Time.time >= (double) endTime;

  protected override Dictionary<Identifiable.Id, DriveCalculator> GetSearchIds()
  {
    SlimeVarietyModules component1 = GetComponent<SlimeVarietyModules>();
    Dictionary<Identifiable.Id, DriveCalculator> searchIds = new Dictionary<Identifiable.Id, DriveCalculator>(Identifiable.idComparer);
    foreach (Identifiable.Id id in Identifiable.LARGO_CLASS)
    {
      GameObject prefab = lookupDir.GetPrefab(id);
      if (prefab == null)
        Log.Error("Null prefab!", "id", id);
      SlimeVarietyModules component2 = prefab.GetComponent<SlimeVarietyModules>();
      if (component2 != null)
      {
        bool flag = false;
        foreach (GameObject slimeModule in component1.slimeModules)
        {
          if (Array.IndexOf(component2.slimeModules, slimeModule) != -1)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          searchIds[id] = largoDriveCalc;
      }
    }
    return searchIds;
  }
}
