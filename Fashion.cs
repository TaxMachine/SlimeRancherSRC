// Decompiled with JetBrains decompiler
// Type: Fashion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Fashion : MonoBehaviour
{
  public Slot slot;
  public GameObject attachPrefab;
  public GameObject attachFX;
  private bool used;

  public void OnCollisionEnter(Collision col)
  {
    if (used)
      return;
    AttachFashions component = col.gameObject.GetComponent<AttachFashions>();
    if (!(component != null))
      return;
    component.Attach(this);
    GetComponent<DestroyOnTouching>().NoteDestroying();
    Destroyer.DestroyActor(gameObject, "Fashion.OnCollisionEnter");
    used = true;
  }

  public enum Slot
  {
    TOP,
    FRONT,
  }
}
