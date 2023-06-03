// Decompiled with JetBrains decompiler
// Type: FashionRemover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FashionRemover : MonoBehaviour
{
  public GameObject removeFX;
  private bool used;

  public void OnCollisionEnter(Collision col)
  {
    if (used)
      return;
    AttachFashions component = col.gameObject.GetComponent<AttachFashions>();
    if (!(component != null))
      return;
    component.DetachAll(this);
    GetComponent<DestroyOnTouching>().NoteDestroying();
    Destroyer.DestroyActor(gameObject, "FashionRemover.OnCollisionEnter");
    used = true;
  }
}
