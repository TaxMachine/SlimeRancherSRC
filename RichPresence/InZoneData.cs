// Decompiled with JetBrains decompiler
// Type: RichPresence.InZoneData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace RichPresence
{
  public struct InZoneData
  {
    public ZoneDirector.Zone zone;

    public InZoneData(ZoneDirector.Zone zone) => this.zone = zone;

    public override string ToString() => string.Format("{0} [zone={1}]", GetType().Name, zone);
  }
}
