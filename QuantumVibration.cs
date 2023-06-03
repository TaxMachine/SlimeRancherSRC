// Decompiled with JetBrains decompiler
// Type: QuantumVibration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class QuantumVibration : MonoBehaviour
{
  private State currentState;
  private SlimeEmotions slimeEmotions;
  private QuantumVibrationMarker vibrationMarker;
  public GameObject VibratingFX;
  public float AgitationCutoff = 0.1f;

  public void Awake() => slimeEmotions = gameObject.GetComponent<SlimeEmotions>();

  public void Start()
  {
    vibrationMarker = gameObject.GetComponentInChildren<QuantumVibrationMarker>();
    VibratingFX = vibrationMarker.gameObject;
    switch (currentState)
    {
      case State.Vibrating:
        vibrationMarker.PlayVibrating();
        break;
      case State.Calm:
        vibrationMarker.PlayCalm();
        break;
    }
  }

  public bool IsVibrating() => currentState == State.Vibrating;

  public float GetVibrationLevel()
  {
    if (slimeEmotions.GetCurr(SlimeEmotions.Emotion.AGITATION) < (double) AgitationCutoff)
      return 0.0f;
    float num1 = (float) (1.0 / (1.0 - AgitationCutoff));
    float num2 = (float) (0.0 - num1 * (double) AgitationCutoff);
    return Mathf.Clamp(num1 * slimeEmotions.GetCurr(SlimeEmotions.Emotion.AGITATION) + num2, 0.0f, 1f);
  }

  public void FixedUpdate()
  {
    switch (currentState)
    {
      case State.Vibrating:
        VibratingUpdate();
        break;
      case State.Calm:
        CalmUpdate();
        break;
      default:
        Log.Warning("Unexpected state in QuantumVibration.");
        break;
    }
  }

  private void CalmUpdate()
  {
    if (!IsAgitated())
      return;
    currentState = State.Vibrating;
    VibratingFX.SetActive(true);
    vibrationMarker.PlayVibrating();
  }

  private void VibratingUpdate()
  {
    if (IsAgitated())
      return;
    currentState = State.Calm;
    VibratingFX.SetActive(false);
    vibrationMarker.PlayCalm();
  }

  private bool IsAgitated() => slimeEmotions.GetCurr(SlimeEmotions.Emotion.AGITATION) > (double) AgitationCutoff;

  private enum State
  {
    Vibrating,
    Calm,
  }
}
