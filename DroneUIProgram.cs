// Decompiled with JetBrains decompiler
// Type: DroneUIProgram
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class DroneUIProgram : SRBehaviour
{
  [Tooltip("Drone program button: target")]
  public DroneUIProgramButton buttonTarget;
  [Tooltip("Drone program button: source")]
  public DroneUIProgramButton buttonSource;
  [Tooltip("Drone program button: destination")]
  public DroneUIProgramButton buttonDestination;

  public DroneUIProgram Init(DroneMetadata.Program program, int? index)
  {
    buttonTarget.Init(program.target, new DroneUIProgramButton.Title()
    {
      type = DroneUIProgramButton.Title.Type.TARGET,
      index = index
    });
    buttonSource.Init(program.source, new DroneUIProgramButton.Title()
    {
      type = DroneUIProgramButton.Title.Type.SOURCE,
      index = index
    });
    buttonDestination.Init(program.destination, new DroneUIProgramButton.Title()
    {
      type = DroneUIProgramButton.Title.Type.DESTINATION,
      index = index
    });
    return this;
  }

  public void LinkGamepadNav(DroneUIProgram down)
  {
    LinkDefaultNavigation();
    LinkNavigation(buttonTarget.button, down.GetButtonOrRightmost(down.buttonTarget.button), NavigationDirection.DOWN);
    LinkNavigation(buttonSource.button, down.GetButtonOrRightmost(down.buttonSource.button), NavigationDirection.DOWN);
    LinkNavigation(buttonDestination.button, down.GetButtonOrRightmost(down.buttonDestination.button), NavigationDirection.DOWN);
    LinkNavigation(down.buttonTarget.button, GetButtonOrRightmost(buttonTarget.button), NavigationDirection.UP);
    LinkNavigation(down.buttonSource.button, GetButtonOrRightmost(buttonSource.button), NavigationDirection.UP);
    LinkNavigation(down.buttonDestination.button, GetButtonOrRightmost(buttonDestination.button), NavigationDirection.UP);
  }

  public void LinkGamepadNav(Selectable down)
  {
    LinkDefaultNavigation();
    LinkNavigation(buttonTarget.button, down, NavigationDirection.DOWN_UP);
    LinkNavigation(buttonSource.button, down, NavigationDirection.DOWN);
    LinkNavigation(buttonDestination.button, down, NavigationDirection.DOWN);
  }

  private void LinkDefaultNavigation()
  {
    LinkNavigation(buttonTarget.button, buttonSource.button, NavigationDirection.RIGHT_LEFT);
    LinkNavigation(buttonSource.button, buttonDestination.button, NavigationDirection.RIGHT_LEFT);
  }

  private Selectable GetButtonOrRightmost(Selectable selectable)
  {
    if (selectable.interactable)
      return selectable;
    return selectable == buttonDestination.button ? GetButtonOrRightmost(buttonSource.button) : buttonTarget.button;
  }
}
