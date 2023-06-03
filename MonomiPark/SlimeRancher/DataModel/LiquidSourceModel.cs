// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.LiquidSourceModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class LiquidSourceModel : IdHandlerModel
  {
    public Vector3 pos;
    public bool isScaling;
    public float unitsFilled;

    public void Push(float unitsFilled) => this.unitsFilled = unitsFilled;

    public void Pull(out float unitsFilled) => unitsFilled = this.unitsFilled;
  }
}
