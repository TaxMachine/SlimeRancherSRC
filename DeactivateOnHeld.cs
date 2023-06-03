// Decompiled with JetBrains decompiler
// Type: DeactivateOnHeld
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DeactivateOnHeld : SRBehaviour
{
  private Vacuumable parent;

  public void Start()
  {
    parent = GetComponentInParent<Vacuumable>();
    if (!(parent != null))
      return;
    parent.onSetHeld += OnSetHeld;
    OnSetHeld(parent.isHeld());
  }

  public void OnDestroy()
  {
    if (!(parent != null))
      return;
    parent.onSetHeld -= OnSetHeld;
    parent = null;
  }

  private void OnSetHeld(bool held) => gameObject.SetActive(!held);
}
