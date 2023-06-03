// Decompiled with JetBrains decompiler
// Type: TutorialRadar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class TutorialRadar : MonoBehaviour
{
  public TutorialDirector.Id tutorialId;
  public static List<TutorialRadar> allRadars = new List<TutorialRadar>();

  public void Awake() => allRadars.Add(this);

  public void OnDestroy() => allRadars.Remove(this);
}
