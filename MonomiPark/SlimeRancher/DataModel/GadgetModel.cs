// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GadgetModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public abstract class GadgetModel
  {
    public Gadget.Id ident;
    public string siteId;
    public Transform transform;
    public double waitForChargeupTime;

    public GadgetModel(Gadget.Id ident, string siteId, Transform transform)
    {
      this.ident = ident;
      this.siteId = siteId;
      this.transform = transform;
    }

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

    public void PushBase(double waitForChargeupTime, float yRotation)
    {
      this.waitForChargeupTime = waitForChargeupTime;
      transform.localRotation = Quaternion.Euler(0.0f, yRotation, 0.0f);
    }

    public void PullBase(out double waitForChargeupTime, out float yRotation)
    {
      waitForChargeupTime = this.waitForChargeupTime;
      yRotation = transform.localRotation.eulerAngles.y;
    }

    public interface Participant
    {
      void InitModel(GadgetModel model);

      void SetModel(GadgetModel model);
    }
  }
}
