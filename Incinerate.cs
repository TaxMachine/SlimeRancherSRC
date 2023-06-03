// Decompiled with JetBrains decompiler
// Type: Incinerate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Incinerate : SRBehaviour
{
  public GameObject ExplosionFX;
  public GameObject ashFX;
  public SECTR_AudioCue smallCue;
  public SECTR_AudioCue largeCue;
  public FillableAshSource ashTrough;
  public float ashPerIncineration;
  private SECTR_AudioSource incinerateAudio;

  public void Awake() => incinerateAudio = GetComponent<SECTR_AudioSource>();

  private void OnCollisionEnter(Collision col)
  {
    Identifiable component = col.gameObject.GetComponent<Identifiable>();
    if (component == null || !CanBeIncinerated(component))
      return;
    if (component.id == Identifiable.Id.ELDER_HEN || component.id == Identifiable.Id.ELDER_ROOSTER)
      SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.INCINERATED_ELDER_CHICKENS, 1);
    else if (component.id == Identifiable.Id.CHICK || component.id == Identifiable.Id.BRIAR_CHICK || component.id == Identifiable.Id.STONY_CHICK || component.id == Identifiable.Id.PAINTED_CHICK)
      SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.INCINERATED_CHICKS, 1);
    ProcessIncinerateResults(component.id, 1, col.gameObject.transform.position, col.gameObject.transform.rotation);
    Destroyer.DestroyActor(col.gameObject, "Incinerate.OnCollisionEnter");
  }

  public void ProcessIncinerateResults(
    Identifiable.Id id,
    int amount,
    Vector3 position,
    Quaternion rotation)
  {
    SpawnAndPlayFX(ExplosionFX, position, rotation);
    Vacuumable component = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(id).GetComponent<Vacuumable>();
    if (component == null || component.size == Vacuumable.Size.NORMAL)
    {
      incinerateAudio.Cue = smallCue;
      incinerateAudio.Play();
    }
    else
    {
      incinerateAudio.Cue = largeCue;
      incinerateAudio.Play();
    }
    if (!ProcessIncinerateResults(id, amount))
      return;
    SpawnAndPlayFX(ashFX, position, rotation);
  }

  public bool ProcessIncinerateResults(Identifiable.Id id, int amount)
  {
    if (!(ashTrough != null) || !ashTrough.isActiveAndEnabled || !Identifiable.IsFood(id))
      return false;
    ashTrough.AddAsh(amount * ashPerIncineration);
    return true;
  }

  public int GetAshSpace() => ashTrough != null && ashTrough.isActiveAndEnabled ? Mathf.CeilToInt(ashTrough.GetAshSpace() / ashPerIncineration) : 0;

  private bool CanBeIncinerated(Identifiable ident) => ident.id != Identifiable.Id.FIRE_PLORT && ident.id != Identifiable.Id.FIRE_SLIME && ident.id != Identifiable.Id.CHARCOAL_BRICK_TOY;
}
