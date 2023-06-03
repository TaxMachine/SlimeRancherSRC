// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.VersionedPersistedDataSet`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;
using System.Text;

namespace MonomiPark.SlimeRancher.Persist
{
  public abstract class VersionedPersistedDataSet<T> : PersistedDataSet where T : Persistable, new()
  {
    public override void Load(Stream stream, bool skipPayloadEnd)
    {
      BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
      long position = stream.Position;
      int num = IsValidHeader(reader) ? 1 : 0;
      stream.Seek(position, SeekOrigin.Begin);
      if (num != 0)
        base.Load(stream, skipPayloadEnd);
      else
        UpgradeFrom(LoadLegacy(new T(), stream, skipPayloadEnd));
    }

    protected virtual T LoadLegacy(T instance, Stream stream, bool skipPayloadEnd)
    {
      if (instance is PersistedDataSet persistedDataSet)
        persistedDataSet.Load(stream, skipPayloadEnd);
      else
        instance.Load(stream);
      return instance;
    }

    protected abstract void UpgradeFrom(T legacyData);
  }
}
