// Decompiled with JetBrains decompiler
// Type: UIDetector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using UnityEngine;

public class UIDetector : SRBehaviour
{
  public GameObject activationGuiPrefab;
  public GameObject gadgetModeActivationGuiPrefab;
  public GameObject slimeGateActivationGuiPrefab;
  public GameObject slimeGateNoKeyActivationGuiPrefab;
  public GameObject puzzleGateActivationGuiPrefab;
  public GameObject puzzleGateLockedActivationGuiPrefab;
  public GameObject treasurePodActivationGuiPrefab;
  public GameObject treasurePodInsufKeyActivationGuiPrefab;
  public GameObject treasurePodNoKeyActivationGuiPrefab;
  public float interactDistance = 2f;
  private GameObject displayingGui;
  private GameObject displayingGuiPrefab;
  private vp_FPInput fpInput;
  private Camera mainCamera;
  private WeaponVacuum weaponVac;

  public void Awake() => fpInput = GetComponentInChildren<vp_FPInput>();

  public void Start()
  {
    mainCamera = Camera.main;
    weaponVac = GetComponentInChildren<WeaponVacuum>();
  }

  public void OnDisable() => Destroyer.Destroy(displayingGui, "UIDetector.OnDisable");

  private void Update()
  {
    RaycastHit hitInfo;
    Physics.Raycast(mainCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f)), out hitInfo, interactDistance);
    UIActivator uiActivator = null;
    SlimeGateActivator slimeGateActivator = null;
    TreasurePod treasurePod = null;
    TechActivator techActivator = null;
    GadgetInteractor gadgetInteractor = null;
    GadgetSite gadgetSite = null;
    if (hitInfo.collider != null)
    {
      GameObject gameObject = hitInfo.collider.gameObject;
      uiActivator = gameObject.GetComponent<UIActivator>();
      slimeGateActivator = gameObject.GetComponent<SlimeGateActivator>();
      treasurePod = gameObject.GetComponent<TreasurePod>();
      techActivator = gameObject.GetComponent<TechActivator>();
      gadgetInteractor = gameObject.GetComponentInParent<GadgetInteractor>();
      gadgetSite = gameObject.GetComponentInParent<GadgetSite>();
    }
    if (uiActivator != null && uiActivator.CanActivate() && InteractionEnabled())
    {
      if (false && uiActivator.blockInExpoPrefab != null)
      {
        MaybeInstantiateDisplayGui(uiActivator.blockInExpoPrefab);
      }
      else
      {
        MaybeInstantiateDisplayGui(activationGuiPrefab);
        if (!SRInput.Actions.interact.WasReleased)
          return;
        uiActivator.Activate();
      }
    }
    else if (slimeGateActivator != null && slimeGateActivator.gateDoor.CurrState == AccessDoor.State.LOCKED && InteractionEnabled())
    {
      int num = SRSingleton<SceneContext>.Instance.PlayerState.GetKeys() > 0 ? 1 : 0;
      if (num != 0)
        MaybeInstantiateDisplayGui(slimeGateActivationGuiPrefab);
      else
        MaybeInstantiateDisplayGui(slimeGateNoKeyActivationGuiPrefab);
      if (num == 0 || !SRInput.Actions.interact.WasReleased)
        return;
      slimeGateActivator.Activate();
    }
    else if (treasurePod != null && treasurePod.CurrState == TreasurePod.State.LOCKED && InteractionEnabled())
    {
      int num = treasurePod.HasKey() ? 1 : 0;
      bool flag = treasurePod.HasAnyKey();
      if (num != 0)
        MaybeInstantiateDisplayGui(treasurePodActivationGuiPrefab);
      else if (flag)
        MaybeInstantiateDisplayGui(treasurePodInsufKeyActivationGuiPrefab);
      else
        MaybeInstantiateDisplayGui(treasurePodNoKeyActivationGuiPrefab);
      if (num == 0 || !SRInput.Actions.interact.WasReleased)
        return;
      treasurePod.Activate();
    }
    else if (techActivator != null && InteractionEnabled())
    {
      GameObject prefab = techActivator.GetCustomGuiPrefab();
      if (prefab == null)
        prefab = activationGuiPrefab;
      MaybeInstantiateDisplayGui(prefab);
      if (!SRInput.Actions.interact.WasReleased)
        return;
      techActivator.Activate();
      Destroyer.Destroy(displayingGui, "UIDetector.Update");
      displayingGui = null;
    }
    else if (gadgetSite != null && InteractionEnabled() && weaponVac.InGadgetMode())
    {
      if (MaybeInstantiateDisplayGui(gadgetModeActivationGuiPrefab))
      {
        RotationRowUI component = displayingGui.GetComponent<RotationRowUI>();
        if (component != null)
        {
          if (gadgetSite.HasAttached())
            component.ShowRow();
          else
            component.HideRow();
        }
      }
      if (SRInput.Actions.interact.WasReleased)
        gadgetSite.Activate();
      if ((bool) (OneAxisInputControl) SRInput.Actions.vac)
      {
        gadgetSite.OnRotateCCW();
      }
      else
      {
        if (!(bool) (OneAxisInputControl) SRInput.Actions.attack)
          return;
        gadgetSite.OnRotateCW();
      }
    }
    else if (gadgetInteractor != null && gadgetInteractor.CanInteract() && InteractionEnabled())
    {
      MaybeInstantiateDisplayGui(activationGuiPrefab);
      if (!SRInput.Actions.interact.WasReleased)
        return;
      gadgetInteractor.OnInteract();
    }
    else
    {
      if (!(displayingGui != null))
        return;
      Destroyer.Destroy(displayingGui, "UIDetector.Update");
      displayingGui = null;
      displayingGuiPrefab = null;
    }
  }

  private bool MaybeInstantiateDisplayGui(GameObject prefab)
  {
    if (displayingGui != null && displayingGuiPrefab != null && displayingGuiPrefab != prefab)
    {
      displayingGui.SetActive(false);
      Destroyer.Destroy(displayingGui, "UIDetector.InstantiateGuiPrefab");
      displayingGui = null;
      displayingGuiPrefab = null;
    }
    if (!(displayingGui == null))
      return false;
    displayingGui = Instantiate(prefab);
    displayingGuiPrefab = prefab;
    return true;
  }

  private bool InteractionEnabled() => Time.timeScale > 0.0 && fpInput.enabled;
}
