// Decompiled with JetBrains decompiler
// Type: vp_SwimmingTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class vp_SwimmingTrigger : MonoBehaviour
{
  public LayerMask LayerMask = 256;
  public string StateName = "Swimming";
  protected static string m_IsPlayerKey = "Is Player";
  protected static string m_PlayerKey = "Player";

  protected virtual void Start() => GetComponent<Collider>().isTrigger = true;

  protected virtual void OnTriggerEnter(Collider col)
  {
    Dictionary<string, object> dataForCollider = GetDataForCollider(col);
    if (!(bool) dataForCollider[m_IsPlayerKey])
      return;
    vp_FPPlayerEventHandler playerEventHandler = (vp_FPPlayerEventHandler) dataForCollider[m_PlayerKey];
    playerEventHandler.SetState(StateName);
    playerEventHandler.MotorThrottle.Set(Vector3.zero);
    playerEventHandler.Jump.TryStop();
    Vector3 force = new Vector3(0.0f, playerEventHandler.Velocity.Get().normalized.y * 0.25f, 0.0f);
    playerEventHandler.Stop.Send();
    playerEventHandler.GetComponent<vp_FPController>().AddSoftForce(force, 10f);
  }

  protected virtual void OnTriggerExit(Collider col)
  {
    Dictionary<string, object> dataForCollider = GetDataForCollider(col);
    if (!(bool) dataForCollider[m_IsPlayerKey])
      return;
    ((vp_StateEventHandler) dataForCollider[m_PlayerKey]).SetState(StateName, false);
  }

  protected virtual Dictionary<string, object> GetDataForCollider(Collider col)
  {
    if ((LayerMask.value & 1 << col.gameObject.layer) == 0)
      return new Dictionary<string, object>()
      {
        {
          m_IsPlayerKey,
          false
        }
      };
    vp_FPPlayerEventHandler component = col.gameObject.GetComponent<vp_FPPlayerEventHandler>();
    if (component == null)
      return new Dictionary<string, object>()
      {
        {
          m_IsPlayerKey,
          false
        }
      };
    return new Dictionary<string, object>()
    {
      {
        m_IsPlayerKey,
        true
      },
      {
        m_PlayerKey,
        component
      }
    };
  }
}
