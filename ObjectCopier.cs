// Decompiled with JetBrains decompiler
// Type: ObjectCopier
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class ObjectCopier
{
  public static T Clone<T>(T source)
  {
    if (!typeof (T).IsSerializable)
      Debug.Log("The type must be serializable.");
    if (source == null)
      return default (T);
    IFormatter formatter = new BinaryFormatter();
    SurrogateSelector surrogateSelector = new SurrogateSelector();
    surrogateSelector.AddSurrogate(typeof (Vector3), new StreamingContext(StreamingContextStates.All), new Vector3Surrogate());
    formatter.SurrogateSelector = surrogateSelector;
    Stream serializationStream = new MemoryStream();
    using (serializationStream)
    {
      formatter.Serialize(serializationStream, source);
      serializationStream.Seek(0L, SeekOrigin.Begin);
      return (T) formatter.Deserialize(serializationStream);
    }
  }
}
