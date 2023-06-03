// Decompiled with JetBrains decompiler
// Type: ToyTreasureChest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Vacuumable))]
public class ToyTreasureChest : MonoBehaviour
{
  [Tooltip("Rigidbody of the chest lid.")]
  public Rigidbody chestLid;
  private Vacuumable vacuumable;

  public void Awake() => vacuumable = GetComponent<Vacuumable>();

  public void Update() => chestLid.isKinematic = vacuumable.isHeld();
}
