// Decompiled with JetBrains decompiler
// Type: HoldToPress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HoldToPress : MonoBehaviour
{
  public float holdTime;
  public Image holdToPressPrefab;
  private Image holdToPress;
  public UnityEvent OnHoldComplete;
  private bool holdComplete;
  private const float INITIAL_FILL_AMOUNT = 0.25f;

  public void OnEnable()
  {
    holdToPress = Instantiate(holdToPressPrefab, transform);
    holdToPress.fillAmount = 0.25f;
    holdComplete = false;
  }

  public void OnDisable()
  {
    Destroyer.Destroy(holdToPress.gameObject, "HoldToPress.OnDisable");
    holdToPress = null;
    holdComplete = false;
  }

  public void Update()
  {
    if (!(holdToPress != null))
      return;
    if (holdToPress.fillAmount < 1.0)
    {
      holdToPress.fillAmount += Time.unscaledDeltaTime / holdTime;
    }
    else
    {
      if (holdComplete)
        return;
      holdComplete = true;
      OnHoldComplete.Invoke();
    }
  }

  public class HoldCompleteEvent : UnityEvent
  {
  }
}
