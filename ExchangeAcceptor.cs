// Decompiled with JetBrains decompiler
// Type: ExchangeAcceptor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ExchangeAcceptor : SRBehaviour, VacShootAccelerator
{
  public GameObject storeFX;
  public ExchangeDirector.OfferType[] offerTypes;
  private ExchangeDirector.Awarder[] awarders;
  private ExchangeDirector exchangeDir;
  private SECTR_AudioSource acceptAudio;
  private HashSet<GameObject> acceptedThisFrame = new HashSet<GameObject>();
  private VacAccelerationHelper accelerationInput = VacAccelerationHelper.CreateInput();

  public void Awake()
  {
    exchangeDir = SRSingleton<SceneContext>.Instance.ExchangeDirector;
    acceptAudio = GetComponent<SECTR_AudioSource>();
    awarders = transform.parent.GetComponentsInChildren<ExchangeDirector.Awarder>();
  }

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger)
      return;
    Identifiable component = col.gameObject.GetComponent<Identifiable>();
    if (!(component != null) || acceptedThisFrame.Contains(col.gameObject) || !TryAcceptAllOfferTypes(component.id))
      return;
    if (storeFX != null)
    {
      SpawnAndPlayFX(storeFX, col.transform.position, col.transform.rotation);
      acceptAudio.Play();
    }
    acceptedThisFrame.Add(col.gameObject);
    Destroyer.DestroyActor(col.gameObject, "ExchangeAcceptor.OnTriggerEnter");
    accelerationInput.OnTriggered();
  }

  private bool TryAcceptAllOfferTypes(Identifiable.Id id)
  {
    bool flag = false;
    foreach (ExchangeDirector.OfferType offerType in offerTypes)
      flag |= exchangeDir.TryAccept(offerType, id, awarders);
    return flag;
  }

  public void LateUpdate() => acceptedThisFrame.Clear();

  public float GetVacShootSpeedFactor() => accelerationInput.Factor;
}
