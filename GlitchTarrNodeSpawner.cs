// Decompiled with JetBrains decompiler
// Type: GlitchTarrNodeSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class GlitchTarrNodeSpawner : DirectedSlimeSpawner
{
  private GlitchTarrNode node;
  private GameModeConfig config;

  public override void Awake()
  {
    base.Awake();
    config = SRSingleton<SceneContext>.Instance.GameModeConfig;
    node = GetRequiredComponent<GlitchTarrNode>();
  }

  public override bool CanSpawn(float? forHour = null) => node.GetState() == GlitchTarrNode.State.ACTIVE && !config.GetModeSettings().preventHostiles && base.CanSpawn(forHour);

  protected override void Register(CellDirector director) => (director as GlitchCellDirector).Register(this);
}
