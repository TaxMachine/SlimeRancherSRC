// Decompiled with JetBrains decompiler
// Type: Map
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class Map : SRSingleton<Map>
{
  public MapUI mapUI;
  private TimeDirector timeDir;

  public override void Awake()
  {
    base.Awake();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
  }

  public void Start() => mapUI.gameObject.SetActive(false);

  public void Update()
  {
    if (SRInput.Actions.openMap.WasPressed && !timeDir.IsFastForwarding())
    {
      if (mapUI.gameObject.activeSelf)
        return;
      OpenMap(ZoneDirector.Zone.NONE);
    }
    else
    {
      if (!SRInput.PauseActions.closeMap.WasPressed || !mapUI.gameObject.activeSelf)
        return;
      CloseMap();
    }
  }

  public void OpenMap(ZoneDirector.Zone unlockedZone)
  {
    if (unlockedZone != ZoneDirector.Zone.NONE)
      mapUI.AddZoneToReveal(unlockedZone);
    mapUI.gameObject.SetActive(true);
    mapUI.OpenMap();
  }

  private void CloseMap() => mapUI.Close();

  public void RegisterMarker(DisplayOnMap marker) => mapUI.RegisterObject(marker);

  public void DeregisterMarker(DisplayOnMap marker) => mapUI.DeregisterObject(marker);

  public override void OnDestroy() => base.OnDestroy();
}
