// Decompiled with JetBrains decompiler
// Type: DirectedSlimeSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DirectedSlimeSpawner : DirectedActorSpawner
{
  [Tooltip("An extra slime effect to play along with each spawn.")]
  public GameObject slimeSpawnFX;
  protected ModDirector modDir;
  protected LookupDirector lookupDir;
  private Oasis oasis;

  public override void Awake()
  {
    base.Awake();
    modDir = SRSingleton<SceneContext>.Instance.ModDirector;
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    oasis = GetComponentInParent<Oasis>();
  }

  public override bool CanSpawn(float? forHour = null) => base.CanSpawn(forHour) && !IsOasisFull();

  protected override void Register(CellDirector cellDir) => cellDir.Register(this);

  protected override GameObject MaybeReplacePrefab(GameObject prefab) => Randoms.SHARED.GetProbability(modDir.ChanceOfTarrSpawn()) ? lookupDir.GetPrefab(Identifiable.Id.TARR_SLIME) : prefab;

  protected override void SpawnFX(GameObject spawnedObj, Vector3 pos)
  {
    base.SpawnFX(spawnedObj, pos);
    SlimeAppearance.Palette appearancePalette = spawnedObj.GetComponent<SlimeAppearanceApplicator>().GetAppearancePalette();
    foreach (RecolorSlimeMaterial componentsInChild in SpawnAndPlayFX(slimeSpawnFX, spawnedObj.transform.position, spawnedObj.transform.rotation).GetComponentsInChildren<RecolorSlimeMaterial>())
      componentsInChild.SetColors(appearancePalette.Top, appearancePalette.Middle, appearancePalette.Bottom);
  }

  private bool IsOasisFull() => oasis != null && !oasis.NeedsMoreSlimes();
}
