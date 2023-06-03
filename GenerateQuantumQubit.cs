// Decompiled with JetBrains decompiler
// Type: GenerateQuantumQubit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateQuantumQubit : SRBehaviour
{
  public float QubitSearchRadius = 10f;
  private const float POSITION_CHECK_SPHERECAST_START = 1000f;
  private const float SPHERECAST_RADIUS = 0.6f;
  private const float QUBIT_RADIUS = 0.61f;
  private const int SPHERECAST_LAYER_MASK = -539068421;
  private const float MAX_QUBIT_HEIGHT = 20f;
  public float MaxQubitHeight = 10f;
  private const int MAX_GENERATION_ATTEMPTS = 5;
  public int MaxQubits = 5;
  private float nextQubitGenerationTime;
  private List<QubitWander> qubits = new List<QubitWander>();
  private static List<MeshCollider> collidersToReset = new List<MeshCollider>();
  private int generationAttempts;
  private SlimeSubbehaviourPlexer plexer;
  private SlimeEmotions slimeEmotions;
  private CalmedByWaterSpray calmedByWaterSpray;
  public float MinGenerationDelay = 5f;
  public float MaxGenerationDelay = 20f;
  public GameObject QubitPrefab;
  public SlimeAppearanceApplicator AppearanceApplicator;
  public GameObject DissipateFx;
  private const float MIN_PLACEMENT_Y = 0.0f;
  private static Collider[] Local_OverlapCount = new Collider[6];

  private void Awake()
  {
    plexer = gameObject.GetComponent<SlimeSubbehaviourPlexer>();
    slimeEmotions = gameObject.GetComponent<SlimeEmotions>();
    calmedByWaterSpray = GetComponent<CalmedByWaterSpray>();
  }

  public QubitWander GetRandomQubit() => Randoms.SHARED.Pick(qubits.Where(q => q.HasArrived() && Physics.OverlapSphere(q.transform.position, 0.6f, -5).Length == 0), null);

  public int GetQubitCount() => qubits.Count;

  public void ClearQubits() => DestroyQubits(true);

  public void DissipateQubit(QubitWander qubit)
  {
    qubits.Remove(qubit);
    DestroyQubit(qubit.gameObject, true);
  }

  public bool ReadyForSuperposition()
  {
    if (qubits.Count == 0)
      return false;
    for (int index = 0; index < qubits.Count; ++index)
    {
      if (!qubits[index].HasArrived())
        return false;
    }
    return true;
  }

  private float GetGenerationDelay() => Mathf.Lerp(MaxGenerationDelay, MinGenerationDelay, slimeEmotions.GetCurr(SlimeEmotions.Emotion.AGITATION));

  private bool ReadyToGenerate() => nextQubitGenerationTime <= (double) Time.time && !plexer.IsGrounded();

  private void Start()
  {
    UpdateGenerationTime();
    generationAttempts = 5;
  }

  private void UpdateGenerationTime() => nextQubitGenerationTime = Time.time + GetGenerationDelay();

  private void Update()
  {
    if (calmedByWaterSpray.IsCalmed())
    {
      DestroyQubits(true);
      UpdateGenerationTime();
    }
    if (ReadyToGenerate() && generationAttempts >= 5)
      generationAttempts = 0;
    if (generationAttempts >= 5 || !plexer.IsGrounded() || plexer.IsCaptive())
      return;
    Vector3 position;
    if (FindValidQubitLocation(out position))
    {
      GenerateQubit(position);
      if (qubits.Count > MaxQubits)
        DissipateQubit(qubits[0]);
      generationAttempts = 5;
    }
    else
      ++generationAttempts;
    if (generationAttempts < 5)
      return;
    UpdateGenerationTime();
  }

  private void GenerateQubit(Vector3 position)
  {
    SlimeAppearance qubitAppearance = AppearanceApplicator.Appearance.QubitAppearance;
    if (qubitAppearance == null)
      Log.Error("No qubit appearance found for slime.", "name", this.gameObject.name);
    GameObject gameObject = InstantiateDynamic(QubitPrefab, this.gameObject.transform.position, Quaternion.identity);
    SlimeAppearanceApplicator componentInChildren = gameObject.GetComponentInChildren<SlimeAppearanceApplicator>();
    componentInChildren.SlimeDefinition = AppearanceApplicator.SlimeDefinition;
    componentInChildren.Appearance = qubitAppearance;
    componentInChildren.ApplyAppearance();
    float prefabScale = AppearanceApplicator.SlimeDefinition.PrefabScale;
    gameObject.transform.localScale = new Vector3(prefabScale, prefabScale, prefabScale);
    QubitWander component = gameObject.GetComponent<QubitWander>();
    component.EndPosition = position;
    component.parentQuantumGenerator = this;
    qubits.Add(component);
  }

  private bool FindValidQubitLocation(out Vector3 position)
  {
    Vector2 vector2 = UnityEngine.Random.insideUnitCircle * QubitSearchRadius;
    Vector3 position1 = gameObject.transform.position;
    position1.x += vector2.x;
    position1.z += vector2.y;
    return GetAdjustedQubitLocation(position1, out position);
  }

  public static bool GetAdjustedQubitLocation(Vector3 castFrom, out Vector3 position)
  {
    position = Vector3.zero;
    castFrom.y += 1000f;
    RaycastHit[] raycastHitArray = Physics.SphereCastAll(castFrom, 0.6f, Vector3.down, float.PositiveInfinity, -539068421);
    collidersToReset.Clear();
    Vector3 vector3 = Vector3.zero;
    float num1 = float.MaxValue;
    bool adjustedQubitLocation = false;
    if (raycastHitArray.Length != 0)
    {
      float num2 = QuantumCeiling.AdjustMinDist(castFrom, 980f);
      for (int index = 0; index < raycastHitArray.Length; ++index)
      {
        MeshCollider component = raycastHitArray[index].collider.GetComponent<MeshCollider>();
        if (component != null && !component.convex && raycastHitArray[index].collider.GetComponent<Rigidbody>() == null)
        {
          collidersToReset.Add(component);
          try
          {
            component.convex = true;
          }
          catch
          {
            Log.Error("Exception when changing to convex.", "object name", component.name);
            throw;
          }
        }
        if (raycastHitArray[index].distance > (double) num2 && raycastHitArray[index].distance < (double) num1 && raycastHitArray[index].point.y >= 0.0)
        {
          vector3 = new Vector3(raycastHitArray[index].point.x, raycastHitArray[index].point.y + 0.61f, raycastHitArray[index].point.z);
          num1 = raycastHitArray[index].distance;
          adjustedQubitLocation = true;
        }
      }
      if (adjustedQubitLocation && Physics.OverlapSphereNonAlloc(vector3, 0.6f, Local_OverlapCount, -539068421) == 0 && !CorralRegion.IsWithin(vector3))
      {
        adjustedQubitLocation = true;
        position = vector3;
      }
      else
        adjustedQubitLocation = false;
      foreach (MeshCollider meshCollider in collidersToReset)
        meshCollider.convex = false;
    }
    return adjustedQubitLocation;
  }

  private void DestroyQubits(bool spawnFX)
  {
    foreach (QubitWander qubit in qubits)
    {
      if (qubit != null)
        DestroyQubit(qubit.gameObject, spawnFX);
    }
    qubits.Clear();
  }

  private void DestroyQubit(GameObject qubit, bool spawnFX)
  {
    if (spawnFX)
      SpawnAndPlayFX(DissipateFx, qubit.transform.position, Quaternion.identity);
    Destroyer.Destroy(qubit.gameObject, "GenerateQuantumQubit.DestroyQubit");
  }

  private void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    DestroyQubits(false);
  }
}
