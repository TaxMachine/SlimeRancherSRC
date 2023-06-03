// Decompiled with JetBrains decompiler
// Type: TrackSlimeTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class TrackSlimeTypes : SRBehaviour
{
  public Mode mode;
  public AchievementsDirector.IntStat stat;
  public bool trackPlayerEnteredSlimesStat;
  private HashSet<Identifiable.Id> workingSet = new HashSet<Identifiable.Id>();
  private List<Identifiable> workingList = new List<Identifiable>();
  private TrackContainedIdentifiables trackingContainer;

  public void Awake()
  {
    trackingContainer = GetComponent<TrackContainedIdentifiables>();
    trackingContainer.OnIdentifiableEntered += OnIdentifiableEntered;
    trackingContainer.OnNewIdentifiableTypeEntered += OnNewIdentifiableTypeEntered;
  }

  public void OnDestroy()
  {
    trackingContainer.OnIdentifiableEntered -= OnIdentifiableEntered;
    trackingContainer.OnNewIdentifiableTypeEntered -= OnNewIdentifiableTypeEntered;
  }

  public void OnIdentifiableEntered(TrackContainedIdentifiables container, Identifiable ident)
  {
    HashSet<Identifiable.Id> identifiablesForMode = GetIdentifiablesForMode();
    if (!trackPlayerEnteredSlimesStat || ident.id != Identifiable.Id.PLAYER)
      return;
    workingList.Clear();
    container.GetTrackedItemsOfClass(identifiablesForMode, workingList);
    SRSingleton<SceneContext>.Instance.AchievementsDirector.MaybeUpdateMaxStat(AchievementsDirector.IntStat.ENTERED_CORRAL_SLIMES, workingList.Count);
    workingList.Clear();
  }

  public void OnNewIdentifiableTypeEntered(
    TrackContainedIdentifiables container,
    Identifiable ident)
  {
    workingSet.Clear();
    workingSet.UnionWith(GetIdentifiablesForMode());
    container.GetTrackedIdentifiableTypes(workingSet);
    SRSingleton<SceneContext>.Instance.AchievementsDirector.MaybeUpdateMaxStat(stat, workingSet.Count);
    workingSet.Clear();
  }

  private HashSet<Identifiable.Id> GetIdentifiablesForMode()
  {
    switch (mode)
    {
      case Mode.SLIMES:
        return Identifiable.EATERS_CLASS;
      case Mode.LARGOS:
        return Identifiable.LARGO_CLASS;
      default:
        return new HashSet<Identifiable.Id>();
    }
  }

  public enum Mode
  {
    SLIMES,
    LARGOS,
  }
}
