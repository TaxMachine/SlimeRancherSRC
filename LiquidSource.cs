// Decompiled with JetBrains decompiler
// Type: LiquidSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSource : IdHandler<LiquidSourceModel>
{
  public Identifiable.Id liquidId;
  [Tooltip("A position marking the top of the water at which objects should float.")]
  public Transform waterTop;
  public float bounceDamp = 0.8f;
  public float floatForcePerDepth = 10f;
  private ReferenceCount<Rigidbody> floating = new ReferenceCount<Rigidbody>();
  protected LiquidSourceModel model;

  protected override string IdPrefix() => nameof (LiquidSource);

  protected override GameModel.Unregistrant Register(GameModel game) => game.LiquidSources.Register(this);

  protected override void InitModel(LiquidSourceModel model)
  {
    model.pos = transform.position;
    model.isScaling = false;
    model.unitsFilled = 0.0f;
  }

  protected override void SetModel(LiquidSourceModel model) => this.model = model;

  public void FixedUpdate()
  {
    if (!CountsAsUnderwater())
      return;
    List<Rigidbody> rigidbodyList = new List<Rigidbody>();
    foreach (Rigidbody key in floating.Keys)
    {
      if (ShouldRemoveBody(key))
      {
        rigidbodyList.Add(key);
        if (key != null)
          UpdateFloatingReactors(key, false);
      }
      else
        Buoyancy(key);
    }
    foreach (Rigidbody key in rigidbodyList)
      floating.Remove(key);
  }

  private bool ShouldRemoveBody(Rigidbody body)
  {
    if (body == null || !body.gameObject.activeInHierarchy)
      return true;
    foreach (Collider component in body.GetComponents<Collider>())
    {
      if (!component.isTrigger && component.enabled)
        return false;
    }
    return true;
  }

  public void OnTriggerEnter(Collider collider)
  {
    Rigidbody floatingRigidBody = GetFloatingRigidBody(collider);
    if (!(floatingRigidBody != null) || floating.Increment(floatingRigidBody) != 1)
      return;
    UpdateFloatingReactors(floatingRigidBody, true);
  }

  public void OnTriggerExit(Collider collider)
  {
    Rigidbody floatingRigidBody = GetFloatingRigidBody(collider);
    if (!(floatingRigidBody != null) || floating.Decrement(floatingRigidBody) != 0)
      return;
    UpdateFloatingReactors(floatingRigidBody, false);
  }

  private Rigidbody GetFloatingRigidBody(Collider collider)
  {
    if (collider.isTrigger || !CountsAsUnderwater())
      return null;
    Identifiable componentInParent = collider.GetComponentInParent<Identifiable>();
    return componentInParent == null || Identifiable.IsWater(componentInParent.id) || Identifiable.IsEcho(componentInParent.id) || Identifiable.IsEchoNote(componentInParent.id) ? null : collider.GetComponentInParent<Rigidbody>();
  }

  public bool CountsAsUnderwater() => waterTop != null;

  private void Buoyancy(Rigidbody body)
  {
    if (!CountsAsUnderwater())
      return;
    BuoyancyOffset component = body.GetComponent<BuoyancyOffset>();
    List<Vector3> vector3List = new List<Vector3>();
    float num1 = 1f;
    if (component != null && component.centersOfBuoyancy.Count > 0)
    {
      foreach (Vector3 position in component.centersOfBuoyancy)
        vector3List.Add(body.transform.TransformPoint(position));
      num1 = component.buoyancyFactor;
    }
    else
      vector3List.Add(body.transform.position);
    foreach (Vector3 position in vector3List)
    {
      float num2 = waterTop.position.y - position.y;
      if (num2 > 0.0)
      {
        float num3 = num2 * floatForcePerDepth;
        float y = body.velocity.y;
        Vector3 force = -Physics.gravity * (num1 * (num3 - y * bounceDamp) / vector3List.Count);
        body.AddForceAtPosition(force, position);
      }
    }
  }

  public void OnDisable() => floating.Clear();

  private void UpdateFloatingReactors(Rigidbody body, bool isFloating)
  {
    foreach (FloatingReactor floatingReactor in body.GetComponentsInParent<FloatingReactor>())
      floatingReactor?.SetIsFloating(isFloating);
  }

  public virtual bool Available() => true;

  public virtual void ConsumeLiquid()
  {
  }

  public virtual bool ReplacesExistingLiquidAmmo() => false;
}
