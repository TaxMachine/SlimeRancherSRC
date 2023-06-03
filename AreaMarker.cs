// Decompiled with JetBrains decompiler
// Type: AreaMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AreaMarker : SRBehaviour
{
  public Color color;
  public float radius;

  public static GameObject CreateMarkerObject(
    Transform parent,
    Vector3 pos,
    Color color,
    float radius)
  {
    GameObject markerObject = new GameObject();
    markerObject.transform.parent = parent;
    markerObject.transform.localPosition = pos;
    AreaMarker areaMarker = markerObject.AddComponent<AreaMarker>();
    areaMarker.color = color;
    areaMarker.radius = radius;
    return markerObject;
  }

  public static void Link(GameObject gameObj1, GameObject gameObj2, Color color)
  {
    LinkMarker linkMarker = gameObj1.AddComponent<LinkMarker>();
    linkMarker.color = color;
    linkMarker.from = gameObj1.transform.position;
    linkMarker.to = gameObj2.transform.position;
  }

  public static void Link(Vector3 pos1, Vector3 pos2, Color color)
  {
    LinkMarker linkMarker = new GameObject().AddComponent<LinkMarker>();
    linkMarker.color = color;
    linkMarker.from = pos1;
    linkMarker.to = pos2;
  }

  private class LinkMarker : SRBehaviour
  {
    public Color color;
    public Vector3 from;
    public Vector3 to;
  }
}
