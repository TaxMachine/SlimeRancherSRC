// Decompiled with JetBrains decompiler
// Type: WorldStateMasterSwitch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class WorldStateMasterSwitch : IdHandler, TechActivator, MasterSwitchModel.Participant
{
  public SwitchHandler.Switchable[] switchables;
  public SwitchHandler.State initState;
  public WorldStateSlaveSwitch[] slaves;
  private bool firstUpdate = true;
  private SwitchHandler switchHandler;
  private float blockSwitchActivationUntil;
  private MasterSwitchModel model;
  private const float ACTIVATION_THROTTLE = 2f;

  private bool IsActivationBlocked() => Time.time < (double) blockSwitchActivationUntil;

  public void Awake()
  {
    switchHandler = new SwitchHandler(GetComponent<Animator>(), gameObject);
    SRSingleton<SceneContext>.Instance.GameModel.RegisterSwitch(id, gameObject);
    foreach (WorldStateSlaveSwitch slave in slaves)
      slave.RegisterMaster(this);
  }

  public void OnEnable()
  {
    if (model == null)
      return;
    switchHandler.SetState(model.state, true);
  }

  public void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterSwitch(id);
  }

  public void InitModel(MasterSwitchModel model) => model.state = initState;

  public void SetModel(MasterSwitchModel model)
  {
    this.model = model;
    switchHandler.SetState(model.state, true);
  }

  protected override string IdPrefix() => "switch";

  public void Update()
  {
    if (!firstUpdate)
      return;
    SetStateForAll(model.state, true);
    firstUpdate = false;
  }

  public void Activate()
  {
    if (IsActivationBlocked())
      return;
    blockSwitchActivationUntil = Time.time + 2f;
    SetStateForAll(model.state == SwitchHandler.State.UP ? SwitchHandler.State.DOWN : SwitchHandler.State.UP, false);
  }

  private void SetStateForAll(SwitchHandler.State state, bool immediate)
  {
    model.state = state;
    switchHandler.SetState(state, immediate);
    foreach (WorldStateSlaveSwitch slave in slaves)
      slave.SetState(state, immediate);
    foreach (SwitchHandler.Switchable switchable in switchables)
      switchable.SetState(state, immediate);
  }

  public GameObject GetCustomGuiPrefab() => null;
}
