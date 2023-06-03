// Decompiled with JetBrains decompiler
// Type: MoveBasedOnGadgetMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MoveBasedOnGadgetMode : MonoBehaviour
{
  public GameObject toMove;
  public Vector2 gadgetModeOnPos;
  public Vector2 gadgetModeOffPos;
  private PlayerState playerState;
  private float lerpVal;
  private const float TRANS_SPEED = 4f;

  public void Awake() => playerState = SRSingleton<SceneContext>.Instance.PlayerState;

  public void Update()
  {
    float a = playerState.InGadgetMode ? 1f : 0.0f;
    lerpVal = a <= (double) lerpVal ? Mathf.Max(a, lerpVal - 4f * Time.deltaTime) : Mathf.Min(a, lerpVal + 4f * Time.deltaTime);
    toMove.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(gadgetModeOffPos, gadgetModeOnPos, lerpVal);
  }
}
