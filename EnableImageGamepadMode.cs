// Decompiled with JetBrains decompiler
// Type: EnableImageGamepadMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Image))]
public class EnableImageGamepadMode : MonoBehaviour
{
  private Image img;

  public void Start() => img = GetComponent<Image>();

  public void Update() => img.enabled = InputDirector.UsingGamepad();
}
