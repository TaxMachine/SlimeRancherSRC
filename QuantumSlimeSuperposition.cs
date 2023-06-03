// Decompiled with JetBrains decompiler
// Type: QuantumSlimeSuperposition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class QuantumSlimeSuperposition : SlimeSubbehaviour
{
  private bool superposePerformed;
  private GenerateQuantumQubit qubitGenerator;
  private QuantumVibration quantumVibration;
  private SlimeEmotions slimeEmotions;
  private RubberBoneEffect rubberBoneEffect;
  private float nextPossibleSuperposeTime;
  public GameObject SuperposeParticleFx;
  private SlimeFaceAnimator slimeFaceAnimator;
  public float MinSuperposeDelay = 5f;
  public float MaxSuperposeDelay = 30f;

  public override void Awake()
  {
    base.Awake();
    qubitGenerator = gameObject.GetComponent<GenerateQuantumQubit>();
    slimeEmotions = gameObject.GetComponent<SlimeEmotions>();
    rubberBoneEffect = gameObject.GetComponentInChildren<RubberBoneEffect>();
    quantumVibration = gameObject.GetComponentInChildren<QuantumVibration>();
    slimeFaceAnimator = gameObject.GetComponent<SlimeFaceAnimator>();
    nextPossibleSuperposeTime = Time.time + MaxSuperposeDelay;
  }

  public override void Start() => nextPossibleSuperposeTime = Time.time + MaxSuperposeDelay;

  public override void Action()
  {
    if (!CanSuperpose())
      return;
    QubitWander randomQubit = qubitGenerator.GetRandomQubit();
    if (!(randomQubit != null))
      return;
    Superpose(randomQubit.gameObject);
    qubitGenerator.ClearQubits();
  }

  public override float Relevancy(bool isGrounded) => quantumVibration.IsVibrating() && Time.time > (double) nextPossibleSuperposeTime && qubitGenerator.ReadyForSuperposition() && !plexer.IsCaptive() ? 1f : 0.0f;

  public override void Selected() => superposePerformed = false;

  public override void Deselected()
  {
    base.Deselected();
    nextPossibleSuperposeTime = Time.time + GetNextSuperposeDelay();
  }

  public override bool CanRethink() => !CanSuperpose();

  private float GetNextSuperposeDelay() => Mathf.Lerp(MaxSuperposeDelay, MinSuperposeDelay, slimeEmotions.GetCurr(SlimeEmotions.Emotion.AGITATION));

  private void Superpose(GameObject qubit)
  {
    SpawnAndPlayFX(SuperposeParticleFx, gameObject.transform.position, Quaternion.identity);
    SpawnAndPlayFX(SuperposeParticleFx, qubit.transform.position, Quaternion.identity);
    gameObject.transform.position = qubit.transform.position;
    rubberBoneEffect.Reset();
    superposePerformed = true;
  }

  private bool CanSuperpose() => !superposePerformed && !plexer.IsCaptive();
}
