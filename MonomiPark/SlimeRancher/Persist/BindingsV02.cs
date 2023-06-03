// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.BindingsV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class BindingsV02 : PersistedDataSet
  {
    public List<Binding> bindings = new List<Binding>();

    public override string Identifier => "SRBINDINGS";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      int num = reader.ReadInt32();
      for (int index = 0; index < num; ++index)
      {
        try
        {
          LoadNextBinding(reader);
        }
        catch (PersistedDataReadException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          throw new PersistedDataReadException("Exception raised while reading key bindings.", ex);
        }
      }
    }

    private void LoadNextBinding(BinaryReader reader)
    {
      ReadElementSeparator(reader);
      bindings.Add(new Binding()
      {
        action = reader.ReadString(),
        primKey = reader.ReadInt32(),
        primMouse = reader.ReadInt32(),
        secondary = reader.ReadInt32(),
        gamepad = reader.ReadInt32()
      });
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(bindings.Count);
      foreach (Binding binding in bindings)
        WriteBinding(binding, writer);
    }

    private void WriteBinding(Binding binding, BinaryWriter writer)
    {
      WriteElementSeparator(writer);
      writer.Write(binding.action);
      writer.Write(binding.primKey);
      writer.Write(binding.primMouse);
      writer.Write(binding.secondary);
      writer.Write(binding.gamepad);
    }

    public class Binding
    {
      public string action;
      public int primKey;
      public int primMouse;
      public int secondary;
      public int gamepad;

      public Binding()
      {
      }

      public Binding(string action, int primKey, int primMouse, int secondary, int gamepad)
      {
        this.action = action;
        this.primKey = primKey;
        this.primMouse = primMouse;
        this.secondary = secondary;
        this.gamepad = gamepad;
      }
    }
  }
}
