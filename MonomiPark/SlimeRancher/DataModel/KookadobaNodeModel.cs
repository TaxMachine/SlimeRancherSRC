// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.KookadobaNodeModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class KookadobaNodeModel : PositionalModel
  {
    private Participant part;

    public void SetParticipant(Participant part) => this.part = part;

    public void Init() => part.InitModel(this);

    public void NotifyParticipants() => part.SetModel(this);

    public void Grow(GameObject gameObject) => part.Grow(gameObject);

    public interface Participant
    {
      void InitModel(KookadobaNodeModel model);

      void SetModel(KookadobaNodeModel model);

      void Grow(GameObject kookadoba);
    }
  }
}
