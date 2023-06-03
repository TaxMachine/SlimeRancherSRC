// Decompiled with JetBrains decompiler
// Type: ActorVortexer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorVortexer : SRBehaviour
{
  public GameObject spawnFX;
  public float spawnRad = 0.5f;
  public float maxRad = 5f;
  public float tornadoHeight = 45f;
  public float tornadoEccentricity = 5f;
  public float heightSpeed = 5f;
  public float heightOffset;
  public float ejectSpeed = 30f;
  public float vertEjectSpeed = 20f;
  public bool treatZAsUp = true;
  [Tooltip("Maximum number of actors we can handle at a time, or 0 for infinite.")]
  public int maxJointedActors;
  private float ejectChancePerSecond = 0.03f;
  public const float MIN_ANGULAR_SPEED = -0.1f;
  public const float MAX_ANGULAR_SPEED = -0.09f;
  private List<Actor> actors = new List<Actor>();
  private TimeDirector timeDir;

  public void Awake() => timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;

  public void OnDestroy()
  {
    for (int index = actors.Count - 1; index >= 0; --index)
      Disconnect(index, false);
  }

  public void FixedUpdate()
  {
    for (int index = actors.Count - 1; index >= 0; --index)
    {
      Actor actor = actors[index];
      if (!IsConnected(actor))
        Disconnect(index, false);
      else if (Randoms.SHARED.GetProbability(ejectChancePerSecond * Time.fixedDeltaTime))
      {
        Disconnect(index, true);
      }
      else
      {
        float num1 = Noise.Noise.PerlinNoise(timeDir.WorldTime(), 0.0f, actor.jointObj.GetInstanceID() * 1000, 500f, tornadoHeight, 1f) + heightOffset;
        float num2 = heightSpeed * Time.fixedDeltaTime;
        actor.height = Mathf.Clamp(num1, actor.height - num2, actor.height + num2);
        actor.angleRads += Randoms.SHARED.GetInRange(-0.1f, -0.09f);
        float num3 = 0.0f;
        float num4 = 0.0f;
        if (tornadoEccentricity != 0.0)
        {
          num3 = Mathf.Sin((float) (actor.height * 3.1415927410125732 * 2.0) / tornadoHeight) * tornadoEccentricity;
          num4 = -Mathf.Sin((float) (actor.height * 3.1415927410125732 * 2.0) / tornadoHeight) * tornadoEccentricity;
        }
        float num5 = Mathf.Lerp(spawnRad, maxRad, actor.height / tornadoHeight);
        actor.jointObj.transform.localPosition = !treatZAsUp ? new Vector3(num5 * Mathf.Cos(actor.angleRads) + num3, actor.height, num5 * Mathf.Sin(actor.angleRads) + num4) : new Vector3(num5 * Mathf.Cos(actor.angleRads) + num3, num5 * Mathf.Sin(actor.angleRads) + num4, actor.height);
        actor.jointObj.transform.eulerAngles = new Vector3(0.0f, (float) (actor.angleRads * 180.0 / 3.1415927410125732), 0.0f);
      }
    }
  }

  public void Connect(GameObject gameObject)
  {
    if (maxJointedActors > 0 && actors.Count >= maxJointedActors)
      Disconnect(Randoms.SHARED.GetInt(actors.Count), true);
    GameObject jointObj = new GameObject("Joint");
    jointObj.transform.position = gameObject.transform.position;
    jointObj.transform.rotation = gameObject.transform.rotation;
    jointObj.transform.SetParent(transform, true);
    float height = gameObject.transform.position.y - transform.position.y;
    float num1 = 0.0f;
    float num2 = 0.0f;
    if (tornadoEccentricity != 0.0)
    {
      num1 = Mathf.Sin((float) (height * 3.1415927410125732 * 2.0) / tornadoHeight) * tornadoEccentricity;
      num2 = -Mathf.Sin((float) (height * 3.1415927410125732 * 2.0) / tornadoHeight) * tornadoEccentricity;
    }
    float angleRads = Mathf.Atan2(gameObject.transform.position.z - (transform.position.z + num2), gameObject.transform.position.x - (transform.position.x + num1));
    Connect(gameObject, jointObj, angleRads, height);
  }

  private void Connect(GameObject gameObject, GameObject jointObj, float angleRads, float height)
  {
    FixedJoint toJoint = jointObj.AddComponent<FixedJoint>();
    toJoint.breakForce = toJoint.breakTorque = 1000f;
    jointObj.GetComponent<Rigidbody>().isKinematic = true;
    SafeJointReference.AttachSafely(gameObject, toJoint);
    toJoint.connectedAnchor = Vector3.zero;
    Vacuumable component = gameObject.GetComponent<Vacuumable>();
    component.capture(toJoint);
    component.SetTornadoed(true);
    SpawnAndPlayFX(spawnFX, jointObj.transform.position, jointObj.transform.rotation);
    actors.Add(new Actor()
    {
      gameObject = gameObject,
      jointObj = jointObj,
      joint = toJoint,
      angleRads = angleRads,
      height = height
    });
  }

  private void Disconnect(int index, bool eject)
  {
    Actor actor = actors[index];
    actors.RemoveAt(index);
    if (actor.gameObject != null)
    {
      actor.gameObject.GetComponent<Vacuumable>().release();
      Rigidbody component = actor.gameObject.GetComponent<Rigidbody>();
      component.velocity = Vector3.zero;
      if (actor.jointObj != null & eject)
      {
        Vector3 normalized = Vector3.Cross((actor.jointObj.transform.position - transform.position) with
        {
          y = 0.0f
        }, Vector3.down).normalized;
        SRSingleton<SceneContext>.Instance.StartCoroutine(DelayedAddVelocity(component, normalized * ejectSpeed + Vector3.up * vertEjectSpeed));
      }
    }
    Destroyer.Destroy(actor.jointObj, "ActorVortexer.Eject");
  }

  private static IEnumerator DelayedAddVelocity(Rigidbody body, Vector3 velChange)
  {
    yield return new WaitForEndOfFrame();
    if (body != null)
      body.AddForce(velChange, ForceMode.VelocityChange);
  }

  private static bool IsConnected(Actor actor) => actor.gameObject != null && actor.jointObj != null && actor.joint != null;

  private class Actor
  {
    public GameObject gameObject;
    public GameObject jointObj;
    public Joint joint;
    public float angleRads;
    public float height;
  }
}
