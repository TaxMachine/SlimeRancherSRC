// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DLCV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DLCPackage;
using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DLCV02 : PersistedDataSet
  {
    public List<Id> installed;

    public override string Identifier => "SRDLC";

    public override uint Version => 2;

    public DLCV02() => installed = new List<Id>();

    protected override void LoadData(BinaryReader reader) => installed = LoadList(reader, (Func<int, Id>) (ii => (Id) ii));

    protected override void WriteData(BinaryWriter writer) => WriteList(writer, installed, package => (int) package);
  }
}
