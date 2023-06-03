// Decompiled with JetBrains decompiler
// Type: Nutcracker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System.Collections;
using UnityEngine;

public class Nutcracker : SRBehaviour
{
  public Identifiable.Id convertFromId = Identifiable.Id.KOOKADOBA_BALL;
  public Identifiable.Id convertToId = Identifiable.Id.KOOKADOBA_FRUIT;
  public int convertToCount = 5;
  public GameObject crackFX;
  public SECTR_AudioCue crackCue;
  private LookupDirector lookupDir;
  private Animator anim;
  private Region region;
  private int activateId;
  private const float SPAWN_RAD = 0.5f;
  private const float TIME_BEFORE_CREATE = 3f;

  public void Awake()
  {
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    region = GetComponentInParent<Region>();
    anim = GetComponentInParent<Animator>();
    activateId = Animator.StringToHash("activate");
  }

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger || Identifiable.GetId(col.gameObject) != convertFromId)
      return;
    StartCoroutine(DoCrack(col.gameObject));
  }

  private IEnumerator DoCrack(GameObject toCrack)
  {
    Nutcracker nutcracker = this;
    toCrack.GetComponent<Rigidbody>().isKinematic = true;
    toCrack.GetComponent<Collider>().enabled = false;
    foreach (Renderer componentsInChild in toCrack.GetComponentsInChildren<Renderer>())
      componentsInChild.enabled = false;
    nutcracker.anim.SetTrigger(nutcracker.activateId);
    if (nutcracker.crackCue != null)
      SECTR_AudioSystem.Play(nutcracker.crackCue, nutcracker.transform.position, false);
    yield return new WaitForSeconds(3f);
    GameObject prefab = nutcracker.lookupDir.GetPrefab(nutcracker.convertToId);
    if (nutcracker.crackFX != null)
      SpawnAndPlayFX(nutcracker.crackFX, toCrack.transform.position, toCrack.transform.rotation);
    for (int index = 0; index < nutcracker.convertToCount; ++index)
    {
      Vector3 position = nutcracker.transform.position + Random.insideUnitSphere * 0.5f;
      InstantiateActor(prefab, nutcracker.region.setId, position, Quaternion.LookRotation(Random.onUnitSphere));
    }
    Destroyer.DestroyActor(toCrack, "Nutcracker.DoCrack");
  }
}
