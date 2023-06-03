// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.ActorModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public abstract class ActorModel
  {
    public readonly Identifiable.Id ident;
    public readonly long actorId;
    private readonly Transform transform;

    public RegionRegistry.RegionSetId currRegionSetId { get; protected set; }

    public ActorModel(
      long actorId,
      Identifiable.Id ident,
      RegionRegistry.RegionSetId regionSetId,
      Transform transform)
    {
      this.actorId = actorId;
      this.ident = ident;
      this.transform = transform;
      currRegionSetId = regionSetId;
    }

    public virtual bool IsEdible() => true;

    public virtual Vector3 GetPos() => transform.position;

    public virtual Quaternion GetRot() => transform.rotation;

    public void Init(GameObject gameObj)
    {
      foreach (Participant componentsInChild in gameObj.GetComponentsInChildren<Participant>(true))
        componentsInChild.InitModel(this);
    }

    public void NotifyParticipants(GameObject gameObj)
    {
      foreach (Participant componentsInChild in gameObj.GetComponentsInChildren<Participant>(true))
        componentsInChild.SetModel(this);
    }

    public interface Participant
    {
      void InitModel(ActorModel model);

      void SetModel(ActorModel model);
    }

    public static class Id
    {
      public const long PLAYER = 1;
      public const long BEGIN_DYNAMIC = 100;
    }
  }
}
