// Decompiled with JetBrains decompiler
// Type: WorldStateSlaveSwitch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class WorldStateSlaveSwitch : MonoBehaviour, TechActivator
{
  private WorldStateMasterSwitch master;
  private SwitchHandler switchHandler;
  private SwitchHandler.State currState;

  public void Awake() => switchHandler = new SwitchHandler(GetComponent<Animator>(), gameObject);

  public void Start()
  {
  }

  public void OnEnable() => SetState(currState, true);

  public void Activate() => master.Activate();

  public void RegisterMaster(WorldStateMasterSwitch master) => this.master = master;

  internal void SetState(SwitchHandler.State state, bool immediate)
  {
    currState = state;
    if (!isActiveAndEnabled)
      return;
    switchHandler.SetState(state, immediate);
  }

  public GameObject GetCustomGuiPrefab() => null;
}
