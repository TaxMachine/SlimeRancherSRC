// Decompiled with JetBrains decompiler
// Type: PhaseSite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class PhaseSite : IdHandler
{
  public static List<PhaseSite> allSites = new List<PhaseSite>();
  public PhaseableObject phaseableObject;

  public void Awake() => allSites.Add(this);

  public void OnDestroy() => allSites.Remove(this);

  protected override string IdPrefix() => "phaseSite";
}
