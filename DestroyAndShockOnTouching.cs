// Decompiled with JetBrains decompiler
// Type: DestroyAndShockOnTouching
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndShockOnTouching : SRBehaviour
{
  [Tooltip("When we poof, how large an area is shocked.")]
  public float shockRadius;
  [Tooltip("The effect to play when we poof.")]
  public GameObject destroyFX;
  private bool destroying;
  private Identifiable.Id id;

  public void Awake() => id = GetComponent<Identifiable>().id;

  public void NoteDestroying() => destroying = true;

  private void DestroyAndShock()
  {
    if (destroying)
      return;
    destroying = true;
    if (destroyFX != null)
      SpawnAndPlayFX(destroyFX, transform.position, transform.rotation);
    if (shockRadius > 0.0)
      SphereOverlapTrigger.CreateGameObject(transform.position, shockRadius, colliders =>
      {
        HashSet<ReactToShock> reactToShockSet = new HashSet<ReactToShock>();
        foreach (Component collider in colliders)
        {
          foreach (ReactToShock reactToShock in collider.gameObject.GetComponentsInParent<ReactToShock>())
            reactToShockSet.Add(reactToShock);
        }
        foreach (ReactToShock reactToShock in reactToShockSet)
          reactToShock.DoShock(id);
      }, 15);
    Destroyer.DestroyActor(gameObject, "DestroyAndShockOnTouching.DestroyAndShock");
  }

  public void OnCollisionEnter(Collision col) => StartCoroutine(DestroyAndShockAtEndOfFrame());

  private IEnumerator DestroyAndShockAtEndOfFrame()
  {
    yield return new WaitForEndOfFrame();
    DestroyAndShock();
  }
}
