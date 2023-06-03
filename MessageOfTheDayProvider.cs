// Decompiled with JetBrains decompiler
// Type: MessageOfTheDayProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class MessageOfTheDayProvider : ScriptableObject
{
  public void Get(
    SuccessHandler onSuccess,
    ErrorHandler onError)
  {
    RetrieveMessage(onSuccess, onError);
  }

  protected abstract void RetrieveMessage(
    SuccessHandler onSuccess,
    ErrorHandler onError);

  public delegate void SuccessHandler(MessageOfTheDay message);

  public delegate void ErrorHandler();
}
