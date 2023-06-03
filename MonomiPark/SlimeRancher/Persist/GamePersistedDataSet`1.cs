// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GamePersistedDataSet`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public abstract class GamePersistedDataSet<T> : VersionedPersistedDataSet<T>, GamePersistedDataSet where T : Persistable, new()
  {
    private bool isLoadSummary;

    public void LoadSummary(Stream stream)
    {
      isLoadSummary = true;
      Load(stream, true);
      isLoadSummary = false;
    }

    protected override T LoadLegacy(T instance, Stream stream, bool skipPayloadEnd)
    {
      GamePersistedDataSet persistedDataSet = (object) instance as GamePersistedDataSet;
      if (!isLoadSummary || persistedDataSet == null)
        return base.LoadLegacy(instance, stream, skipPayloadEnd);
      persistedDataSet.LoadSummary(stream);
      return instance;
    }

    protected override sealed void LoadData(BinaryReader reader)
    {
      LoadSummaryData(reader);
      if (isLoadSummary)
        return;
      LoadGameData(reader);
    }

    protected override sealed void WriteData(BinaryWriter writer)
    {
      WriteSummaryData(writer);
      WriteGameData(writer);
    }

    protected abstract void LoadSummaryData(BinaryReader reader);

    protected abstract void LoadGameData(BinaryReader reader);

    protected abstract void WriteSummaryData(BinaryWriter writer);

    protected abstract void WriteGameData(BinaryWriter writer);
  }
}
