// Decompiled with JetBrains decompiler
// Type: EventTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EventTrigger : MonoBehaviour
{
  public void PrintEvent(string LOG) => Debug.Log(LOG);

  public void PlayAudio(AudioClip AUD)
  {
  }

  public void SpawnObject(AnimationEvent animationEvent)
  {
    string stringParameter = animationEvent.stringParameter;
    GameObject referenceParameter = (GameObject) animationEvent.objectReferenceParameter;
    Transform transform = this.transform.Find(stringParameter);
    if ((bool) (Object) transform)
      Instantiate(referenceParameter, transform.position, transform.rotation);
    else
      Instantiate(referenceParameter, this.transform.position, this.transform.rotation);
  }
}
