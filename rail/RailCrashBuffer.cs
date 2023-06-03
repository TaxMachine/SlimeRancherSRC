// Decompiled with JetBrains decompiler
// Type: rail.RailCrashBuffer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

namespace rail
{
  public interface RailCrashBuffer
  {
    string GetData();

    uint GetBufferLength();

    uint GetValidLength();

    uint SetData(string data, uint length, uint offset);

    uint SetData(string data, uint length);

    uint AppendData(string data, uint length);
  }
}
