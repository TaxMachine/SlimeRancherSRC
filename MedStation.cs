// Decompiled with JetBrains decompiler
// Type: MedStation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class MedStation : MonoBehaviour
{
  public GameObject medFX;
  public float healthPerEnergy = 1f;
  public float healthPerSecond = 100f;
  private int counts;
  private PlayerState playerState;
  private WaitForChargeup waiter;

  public void Awake()
  {
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    waiter = GetComponentInParent<WaitForChargeup>();
  }

  public void OnTriggerEnter(Collider col)
  {
    if (!PhysicsUtil.IsPlayerMainCollider(col))
      return;
    ++counts;
  }

  public void OnTriggerExit(Collider col)
  {
    if (!PhysicsUtil.IsPlayerMainCollider(col))
      return;
    --counts;
  }

  public void Update()
  {
    if (waiter.IsWaiting())
      return;
    bool flag = false;
    if (counts > 0)
    {
      int val1 = Mathf.Max(playerState.GetMaxHealth() - playerState.GetCurrHealth(), playerState.GetCurrRad());
      int val2 = Mathf.CeilToInt(playerState.GetCurrEnergy() * healthPerEnergy);
      if (val1 > 0 && val2 > 0)
      {
        int num = Math.Min(Math.Min(val1, val2), Mathf.CeilToInt(Time.deltaTime * healthPerSecond));
        if (num > 0)
        {
          flag = true;
          playerState.SpendEnergy(num / healthPerEnergy);
          playerState.Heal(num);
          playerState.RemoveRads(num);
        }
      }
    }
    if (flag == medFX.activeSelf)
      return;
    medFX.SetActive(flag);
  }
}
