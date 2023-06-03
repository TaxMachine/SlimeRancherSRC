// Decompiled with JetBrains decompiler
// Type: UWPDLCProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Linq;

public class UWPDLCProvider : DLCProvider
{
  public UWPDLCProvider()
    : base(Enumerable.Empty<DLCPackage.Id>())
  {
  }

  public override IEnumerator Refresh()
  {
    yield return null;
  }

  public override void ShowInStore(DLCPackage.Id id) => throw new NotImplementedException();
}
