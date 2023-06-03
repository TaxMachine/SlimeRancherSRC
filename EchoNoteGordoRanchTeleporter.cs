// Decompiled with JetBrains decompiler
// Type: EchoNoteGordoRanchTeleporter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EchoNoteGordoRanchTeleporter : SRBehaviour
{
  [Tooltip("Parent GameObject containing the portal ring.")]
  public GameObject ring;

  public void OnEnable()
  {
    HolidayModel holiday = SRSingleton<SceneContext>.Instance.GameModel.GetHolidayModel();
    ring.SetActive(SRSingleton<SceneContext>.Instance.GameModel.AllEchoNoteGordos().Any(pair => pair.Value.state == EchoNoteGordoModel.State.POPPED && holiday.eventEchoNoteGordos.Any(e => e.objectId == pair.Key)));
  }
}
