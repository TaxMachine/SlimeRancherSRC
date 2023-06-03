// Decompiled with JetBrains decompiler
// Type: GordoRewardsBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GordoRewardsBase : SRBehaviour
{
  public GameObject slimePrefab;
  public GameObject slimeSpawnFXPrefab;
  private List<GameObject> activeRewards;
  private const float SPAWN_RAD = 1.2f;
  private const float SPAWN_VERT_OFFSET = 1.7f;
  private const float SPAWN_TORQUE = 10f;
  private static readonly Vector3 SPAWN_OFFSET = new Vector3(0.0f, 1.7f, 0.0f);
  private static Vector3[] spawns = new Vector3[13];

  static GordoRewardsBase()
  {
    spawns[0] = Vector3.zero;
    for (int index = 0; index < 6; ++index)
    {
      float f = (float) (6.2831854820251465 * index / 6.0);
      spawns[index + 1] = new Vector3(Mathf.Cos(f), 0.0f, Mathf.Sin(f));
    }
    for (int index = 0; index < 3; ++index)
    {
      float f = (float) (6.2831854820251465 * index / 3.0 + 0.52359879016876221);
      spawns[index + 7] = new Vector3(Mathf.Cos(f) * 0.5f, 0.866f, Mathf.Sin(f) * 0.5f);
    }
    for (int index = 0; index < 3; ++index)
    {
      float f = (float) (6.2831854820251465 * index / 3.0 - 0.52359879016876221);
      spawns[index + 10] = new Vector3(Mathf.Cos(f) * 0.5f, -0.866f, Mathf.Sin(f) * 0.5f);
    }
  }

  public void Start() => SetupActiveRewards();

  public void SetupActiveRewards()
  {
    if (activeRewards != null)
      return;
    activeRewards = new List<GameObject>(SelectActiveRewardPrefabs());
  }

  public bool HasKeyReward()
  {
    if (activeRewards == null)
      return false;
    foreach (GameObject activeReward in activeRewards)
    {
      Identifiable component = activeReward.GetComponent<Identifiable>();
      if (component != null && component.id == Identifiable.Id.KEY)
        return true;
    }
    return false;
  }

  public void GiveRewards()
  {
    if (activeRewards == null)
    {
      Log.Error("Active rewards on gordo are null.", "gordo", name);
    }
    else
    {
      List<Identifiable.Id> allFashions = GetComponent<AttachFashions>().GetAllFashions();
      Identifiable component1 = gameObject.GetComponent<Identifiable>();
      Color[] colors = SlimeUtil.GetColors(gameObject, component1 != null ? component1.id : Identifiable.Id.NONE, true);
      Region componentInParent = GetComponentInParent<Region>();
      List<Vector3> iterable = new List<Vector3>(spawns.Skip(1));
      int index = 0;
      while (iterable.Count > 0)
      {
        GameObject original = index < activeRewards.Count ? MaybeReplaceCratePrefab(activeRewards[index]) : slimePrefab;
        Vector3 forward = index == 0 ? spawns[0] : Randoms.SHARED.Pluck(iterable, Vector3.zero);
        Vector3 position1 = transform.position + forward * 1.2f + SPAWN_OFFSET;
        Quaternion rotation1 = index == 0 ? Quaternion.identity : Quaternion.LookRotation(forward, Vector3.up);
        int setId = (int) componentInParent.setId;
        Vector3 position2 = position1;
        Quaternion rotation2 = rotation1;
        GameObject instance = InstantiateActor(original, (RegionRegistry.RegionSetId) setId, position2, rotation2, true);
        instance.GetComponent<Rigidbody>().AddTorque(Randoms.SHARED.GetInRange(-10f, 10f), Randoms.SHARED.GetInRange(-10f, 10f), Randoms.SHARED.GetInRange(-10f, 10f));
        AttachFashions component2 = instance.GetComponent<AttachFashions>();
        if (component2 != null)
          component2.SetFashions(allFashions);
        foreach (RecolorSlimeMaterial componentsInChild in SpawnAndPlayFX(slimeSpawnFXPrefab, position1, rotation1).GetComponentsInChildren<RecolorSlimeMaterial>())
          componentsInChild.SetColors(colors[0], colors[1], colors[2]);
        OnInstantiatedReward(instance);
        ++index;
      }
    }
  }

  private static GameObject MaybeReplaceCratePrefab(GameObject prefab) => !SRSingleton<SceneContext>.Instance.GameModel.GetHolidayModel().eventGordos.Any() || !Identifiable.STANDARD_CRATE_CLASS.Contains(Identifiable.GetId(prefab)) || !Randoms.SHARED.GetProbability(HolidayModel.EventGordo.CRATE_CHANCE) ? prefab : SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(HolidayModel.EventGordo.CRATE);

  protected abstract IEnumerable<GameObject> SelectActiveRewardPrefabs();

  protected virtual void OnInstantiatedReward(GameObject instance)
  {
  }
}
