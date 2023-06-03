// Decompiled with JetBrains decompiler
// Type: EventTriggerCustom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EventTriggerCustom : MonoBehaviour
{
  public GameObject objectToSpawn;
  public SECTR_AudioCue audioToPlay;
  private SECTR_AudioCueInstance cueInstance;

  public void SpawnObjectCustom(AnimationEvent animationEvent)
  {
    Transform transform = this.transform.Find(animationEvent.stringParameter);
    if ((bool) (Object) transform)
    {
      Spawn(objectToSpawn, transform);
      PlayAudio(audioToPlay, transform);
    }
    else
    {
      Spawn(objectToSpawn, this.transform);
      PlayAudio(audioToPlay, this.transform);
    }
  }

  private void Spawn(GameObject obj, Transform targetTransform) => Instantiate(obj, targetTransform.position, targetTransform.rotation);

  private void PlayAudio(SECTR_AudioCue cue, Transform location)
  {
    if (!(cue != null))
      return;
    if (cueInstance.Active)
      cueInstance.Stop(true);
    cueInstance = SECTR_AudioSystem.Play(cue, location.position, false);
  }

  public void OnDestroy()
  {
    if (!cueInstance.Active)
      return;
    cueInstance.Stop(true);
  }
}
