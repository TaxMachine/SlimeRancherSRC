// Decompiled with JetBrains decompiler
// Type: HoldToPressButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldToPressButton : Button, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
{
  public HoldToPress holdToPress;
  public bool stillPressed;

  protected override void Awake()
  {
    base.Awake();
    holdToPress = GetComponent<HoldToPress>();
    stillPressed = false;
  }

  public void Update()
  {
    if (IsPressed() && !stillPressed)
    {
      BeginPress();
      stillPressed = true;
    }
    else
    {
      if (!stillPressed || IsPressed())
        return;
      EndPress();
      stillPressed = false;
    }
  }

  public void BeginPress() => holdToPress.enabled = true;

  public void EndPress() => holdToPress.enabled = false;

  public void OnHoldComplete() => holdToPress.enabled = false;
}
