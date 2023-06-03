// Decompiled with JetBrains decompiler
// Type: SRBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;
using UnityEngine.UI;

public class SRBehaviour : MonoBehaviour
{
  private DestroyRequestHandler destroyRequestHandler;

  public I GetInterfaceComponent<I>() where I : class => GetComponent(typeof (I)) as I;

  public static GameObject InstantiateDynamic(GameObject original) => Instantiate(original);

  public static GameObject InstantiateDynamic(
    GameObject original,
    Vector3 position,
    Quaternion rotation,
    bool asActor = false)
  {
    return Instantiate(original, position, rotation);
  }

  public static GameObject InstantiateActor(
    GameObject original,
    RegionRegistry.RegionSetId regionSetId,
    bool nonActorOk = false)
  {
    return InstantiateActor(original, regionSetId, Vector3.zero, Quaternion.identity, nonActorOk);
  }

  public static GameObject InstantiateActor(
    GameObject original,
    RegionRegistry.RegionSetId regionSetId,
    Vector3 position,
    Quaternion rotation,
    bool nonActorOk = false)
  {
    return SRSingleton<SceneContext>.Instance.GameModel.InstantiateActor(original, regionSetId, position, rotation, nonActorOk);
  }

  public static GameObject InstantiatePooledDynamic(
    GameObject original,
    Vector3 position,
    Quaternion rotation)
  {
    return SRSingleton<SceneContext>.Instance.fxPool.Spawn(original, null, position, rotation);
  }

  public static GameObject SpawnAndPlayFX(GameObject prefab) => SpawnAndPlayFX(prefab, Vector3.zero, Quaternion.identity);

  public static GameObject SpawnAndPlayFX(GameObject prefab, GameObject parentObject) => SpawnAndPlayFX(prefab, parentObject, Vector3.zero, Quaternion.identity);

  public static GameObject SpawnAndPlayFX(GameObject prefab, Vector3 position, Quaternion rotation) => SpawnAndPlayFX(prefab, null, position, rotation);

  public static GameObject SpawnAndPlayFX(
    GameObject prefab,
    GameObject parentObject,
    Vector3 position,
    Quaternion rotation)
  {
    GameObject fxObject = null;
    if (SRSingleton<SceneContext>.Instance != null)
    {
      fxObject = SRSingleton<SceneContext>.Instance.fxPool.Spawn(prefab, parentObject != null ? parentObject.transform : null, position, rotation);
      PlayFX(fxObject);
    }
    return fxObject;
  }

  public static void RecycleAndStopFX(GameObject obj)
  {
    StopFX(obj);
    SRSingleton<SceneContext>.Instance.fxPool.Recycle(obj);
  }

  public static void PlayFX(GameObject fxObject)
  {
    if (!(fxObject != null))
      return;
    ParticleSystem particleSystem = fxObject.GetComponent<ParticleSystem>();
    if (particleSystem == null)
      particleSystem = fxObject.GetComponentInChildren<ParticleSystem>();
    if (!(particleSystem != null))
      return;
    particleSystem.Play();
    if (!(particleSystem.gameObject != null))
      return;
    SECTR_PointSource[] components = particleSystem.gameObject.GetComponents<SECTR_PointSource>();
    for (int index = 0; index < components.Length; ++index)
    {
      if (components[index] != null)
        components[index].Play();
    }
  }

  public static void StopFX(GameObject fxObject)
  {
    if (!(fxObject != null))
      return;
    ParticleSystem particleSystem = fxObject.GetComponent<ParticleSystem>();
    if (particleSystem == null)
      particleSystem = fxObject.GetComponentInChildren<ParticleSystem>();
    if (!(particleSystem != null))
      return;
    particleSystem.Stop();
    if (!(particleSystem.gameObject != null))
      return;
    SECTR_PointSource[] components = particleSystem.gameObject.GetComponents<SECTR_PointSource>();
    for (int index = 0; index < components.Length; ++index)
    {
      if (components[index] != null)
        components[index].Stop(true);
    }
  }

  private DestroyRequestHandler GetDestroyRequestHandler()
  {
    if (destroyRequestHandler == null)
      destroyRequestHandler = GetComponent<DestroyRequestHandler>();
    return destroyRequestHandler;
  }

  public void RequestDestroy(string source)
  {
    DestroyRequestHandler destroyRequestHandler = GetDestroyRequestHandler();
    if (destroyRequestHandler != null)
      destroyRequestHandler.OnDestroyRequest(gameObject);
    else
      Destroyer.Destroy(gameObject, source);
  }

  public static void RequestDestroy(GameObject obj, string source) => obj.GetComponent<SRBehaviour>().RequestDestroy(source);

  public static void LinkNavigation(
    Selectable source,
    Selectable target,
    NavigationDirection direction)
  {
    switch (direction)
    {
      case NavigationDirection.UP:
        if (!(source != null))
          break;
        Navigation navigation1 = source.navigation with
        {
          mode = Navigation.Mode.Explicit,
          selectOnUp = !(target != null) || !target.interactable ? null : target
        };
        source.navigation = navigation1;
        break;
      case NavigationDirection.DOWN:
        if (!(source != null))
          break;
        Navigation navigation2 = source.navigation with
        {
          mode = Navigation.Mode.Explicit,
          selectOnDown = !(target != null) || !target.interactable ? null : target
        };
        source.navigation = navigation2;
        break;
      case NavigationDirection.RIGHT:
        if (!(source != null))
          break;
        Navigation navigation3 = source.navigation with
        {
          mode = Navigation.Mode.Explicit,
          selectOnRight = !(target != null) || !target.interactable ? null : target
        };
        source.navigation = navigation3;
        break;
      case NavigationDirection.LEFT:
        if (!(source != null))
          break;
        Navigation navigation4 = source.navigation with
        {
          mode = Navigation.Mode.Explicit,
          selectOnLeft = !(target != null) || !target.interactable ? null : target
        };
        source.navigation = navigation4;
        break;
      case NavigationDirection.RIGHT_LEFT:
        LinkNavigation(source, target, NavigationDirection.RIGHT);
        LinkNavigation(target, source, NavigationDirection.LEFT);
        break;
      case NavigationDirection.DOWN_UP:
        LinkNavigation(source, target, NavigationDirection.DOWN);
        LinkNavigation(target, source, NavigationDirection.UP);
        break;
    }
  }

  public T GetComponentInParent<T>(bool includeInactive) where T : Component => Assets.Script.Util.Extensions.ComponentExtensions.GetComponentInParent<T>(this, includeInactive);

  public T GetRequiredComponent<T>() where T : Component => Assets.Script.Util.Extensions.ComponentExtensions.GetRequiredComponent<T>(this);

  public T GetRequiredComponentInParent<T>(bool includeInactive = false) where T : Component => Assets.Script.Util.Extensions.ComponentExtensions.GetRequiredComponentInParent<T>(this, includeInactive);

  public T GetRequiredComponentInChildren<T>(bool includeInactive = false) where T : Component => Assets.Script.Util.Extensions.ComponentExtensions.GetRequiredComponentInChildren<T>(this, includeInactive);

  public enum NavigationDirection
  {
    UP,
    DOWN,
    RIGHT,
    LEFT,
    RIGHT_LEFT,
    DOWN_UP,
  }
}
