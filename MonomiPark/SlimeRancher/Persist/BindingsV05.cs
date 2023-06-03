// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.BindingsV05
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonomiPark.SlimeRancher.Persist
{
  public class BindingsV05 : VersionedPersistedDataSet<BindingsV04>
  {
    public List<BindingV01> bindings = new List<BindingV01>();

    public override string Identifier => "SRBINDINGS";

    public override uint Version => 5;

    public BindingsV05()
    {
    }

    public BindingsV05(BindingsV04 previous) => UpgradeFrom(previous);

    protected override void LoadData(BinaryReader reader) => bindings = LoadList<BindingV01>(reader);

    protected override void WriteData(BinaryWriter writer) => WriteList(writer, bindings);

    protected override void UpgradeFrom(BindingsV04 previous)
    {
      bindings = previous.bindings.Select(b => new BindingV01()
      {
        action = b.action,
        primKey = b.primKey,
        primMouse = b.primMouse,
        secKey = b.secKey,
        secMouse = b.secMouse,
        gamepad = b.gamepad
      }).ToList();
      BindingV01 bindingV01 = bindings.FirstOrDefault(b => b.action == "OpenMap");
      if (bindingV01 == null)
        return;
      bindings.Add(new BindingV01()
      {
        action = "CloseMap",
        primKey = bindingV01.primKey,
        primMouse = bindingV01.primMouse,
        secKey = bindingV01.secKey,
        secMouse = bindingV01.secMouse,
        gamepad = bindingV01.gamepad
      });
    }

    public static void AssertAreEqual(BindingsV04 expected, BindingsV05 actual) => TestUtil.AssertAreEqual(expected.bindings, actual.bindings, (e, a, s) => BindingV01.AssertAreEqual(e, a));

    public static void AssertAreEqual(BindingsV05 expected, BindingsV05 actual) => TestUtil.AssertAreEqual(expected.bindings, actual.bindings, (e, a, s) => BindingV01.AssertAreEqual(e, a));
  }
}
