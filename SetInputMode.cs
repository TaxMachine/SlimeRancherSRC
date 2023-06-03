// Decompiled with JetBrains decompiler
// Type: SetInputMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SetInputMode : MonoBehaviour
{
  [Tooltip("InputMode to set.")]
  public SRInput.InputMode mode;
  private SRInput input;

  public void Awake() => input = SRInput.Instance;

  public void OnEnable() => input.SetInputMode(mode, gameObject.GetInstanceID());

  public void OnDisable() => input.ClearInputMode(gameObject.GetInstanceID());
}
