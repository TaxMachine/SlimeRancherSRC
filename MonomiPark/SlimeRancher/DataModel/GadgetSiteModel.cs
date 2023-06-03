// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GadgetSiteModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class GadgetSiteModel
  {
    public readonly string id;
    public readonly Transform transform;
    public GadgetModel attached;
    private Participant participant;

    public GadgetSiteModel(string id, Transform transform)
    {
      this.id = id;
      this.transform = transform;
    }

    public void SetParticipant(Participant part) => participant = part;

    public void Init() => participant.InitModel(this);

    public void NotifyParticipants() => participant.SetModel(this);

    public bool HasAttached() => attached != null;

    public void Detach()
    {
      attached = null;
      participant.SetAttached(null);
    }

    public void Attach(GameObject gameObj, GadgetModel gadgetModel)
    {
      attached = gadgetModel;
      participant.SetAttached(gadgetModel);
    }

    public void Push()
    {
    }

    public void Pull()
    {
    }

    public interface Participant
    {
      void InitModel(GadgetSiteModel model);

      void SetModel(GadgetSiteModel model);

      void SetAttached(GadgetModel gadgetModel);
    }
  }
}
