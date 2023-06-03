// Decompiled with JetBrains decompiler
// Type: SECTR_SplineSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("SECTR/Audio/SECTR Spline Source")]
public class SECTR_SplineSource : SECTR_PointSource
{
  private List<SplineNode> nodes = new List<SplineNode>(8);
  [SECTR_ToolTip("Array of scene objects to use as control points for the spline")]
  public List<Transform> SplinePoints = new List<Transform>();
  [SECTR_ToolTip("Determines if the spline is open or closed (i.e. a loop).")]
  public bool Closed;

  private void Awake() => _SetupSpline();

  protected override void OnEnable() => base.OnEnable();

  protected override void OnDisable() => base.OnDisable();

  private void Update()
  {
    if (!instance || nodes.Count <= 0)
      return;
    instance.LocalPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(_GetClosestPointOnSpline(SECTR_AudioSystem.Listener.position));
  }

  private void _SetupSpline()
  {
    nodes.Clear();
    int count1 = SplinePoints.Count;
    if (count1 < 2)
      return;
    float num1 = Closed ? 1f / count1 : 1f / (count1 - 1);
    int index1;
    for (index1 = 0; index1 < count1; ++index1)
    {
      Transform splinePoint = SplinePoints[index1];
      if ((bool) (Object) splinePoint)
        nodes.Add(new SplineNode(splinePoint.position, splinePoint.rotation, num1 * index1, new Vector2(0.0f, 1f)));
    }
    if (Closed && nodes.Count > 0)
    {
      float num2 = num1 * index1;
      nodes.Add(new SplineNode(nodes[0]));
      nodes[nodes.Count - 1].T = num2;
      Vector3 vector3 = nodes[1].Point - nodes[0].Point;
      Vector3 normalized1 = vector3.normalized;
      vector3 = nodes[nodes.Count - 2].Point - nodes[nodes.Count - 1].Point;
      Vector3 normalized2 = vector3.normalized;
      vector3 = nodes[1].Point - nodes[0].Point;
      float magnitude1 = vector3.magnitude;
      vector3 = nodes[nodes.Count - 2].Point - nodes[nodes.Count - 1].Point;
      float magnitude2 = vector3.magnitude;
      SplineNode splineNode1 = new SplineNode(nodes[0]);
      splineNode1.Point = nodes[0].Point + normalized2 * magnitude1;
      SplineNode splineNode2 = new SplineNode(nodes[nodes.Count - 1]);
      splineNode2.Point = nodes[0].Point + normalized1 * magnitude2;
      nodes.Insert(0, splineNode1);
      nodes.Add(splineNode2);
    }
    int count2 = nodes.Count;
    for (int index2 = 1; index2 < count2; ++index2)
    {
      SplineNode node1 = nodes[index2];
      SplineNode node2 = nodes[index2 - 1];
      if (Quaternion.Dot(node1.Rot, node2.Rot) < 0.0)
      {
        node1.Rot.x = -node1.Rot.x;
        node1.Rot.y = -node1.Rot.y;
        node1.Rot.z = -node1.Rot.z;
        node1.Rot.w = -node1.Rot.w;
      }
    }
    if (count2 <= 0 || Closed)
      return;
    nodes.Insert(0, nodes[0]);
    nodes.Add(nodes[nodes.Count - 1]);
  }

  private Vector3 _GetClosestPointOnSpline(Vector3 point)
  {
    Vector3 closestPointOnSpline = point;
    float num1 = float.MaxValue;
    int num2 = 20;
    for (int index = 0; index < num2; ++index)
    {
      Vector3 hermiteAtT = _GetHermiteAtT(index / (float) num2);
      float num3 = Vector3.SqrMagnitude(point - hermiteAtT);
      if (num3 < (double) num1)
      {
        num1 = num3;
        closestPointOnSpline = hermiteAtT;
      }
    }
    return closestPointOnSpline;
  }

  private Vector3 _GetHermiteAtT(float timeParam)
  {
    int count = nodes.Count;
    if (timeParam >= (double) nodes[count - 2].T)
      return nodes[count - 2].Point;
    int index1 = 1;
    while (index1 < count - 2 && nodes[index1].T <= (double) timeParam)
      ++index1;
    int index2 = index1 - 1;
    float num1 = _Ease((float) ((timeParam - (double) nodes[index2].T) / (nodes[index2 + 1].T - (double) nodes[index2].T)), nodes[index2].EaseIO.x, nodes[index2].EaseIO.y);
    float num2 = num1 * num1;
    float num3 = num2 * num1;
    Vector3 point1 = nodes[index2 - 1].Point;
    Vector3 point2 = nodes[index2].Point;
    Vector3 point3 = nodes[index2 + 1].Point;
    Vector3 point4 = nodes[index2 + 2].Point;
    double num4 = 0.5;
    Vector3 vector3_1 = (float) num4 * (point3 - point1);
    Vector3 vector3_2 = (float) num4 * (point4 - point2);
    double num5 = 2.0 * num3 - 3.0 * num2 + 1.0;
    float num6 = (float) (-2.0 * num3 + 3.0 * num2);
    float num7 = num3 - 2f * num2 + num1;
    float num8 = num3 - num2;
    Vector3 vector3_3 = point2;
    return (float) num5 * vector3_3 + num6 * point3 + num7 * vector3_1 + num8 * vector3_2;
  }

  private float _Ease(float t, float k1, float k2)
  {
    float num = (float) (k1 * 2.0 / 3.1415927410125732 + k2 - k1 + (1.0 - k2) * 2.0 / 3.1415927410125732);
    return (t >= (double) k1 ? (t >= (double) k2 ? (float) (2.0 * k1 / 3.1415927410125732 + k2 - k1 + (1.0 - k2) * 0.63661974668502808 * Mathf.Sin((float) ((t - (double) k2) / (1.0 - k2) * 3.1415927410125732 / 2.0))) : (float) (2.0 * k1 / 3.1415927410125732) + t - k1) : (float) (k1 * 0.63661974668502808 * (Mathf.Sin((float) (t / (double) k1 * 3.1415927410125732 * 0.5 - 1.5707963705062866)) + 1.0))) / num;
  }

  private class SplineNode
  {
    public Vector3 Point;
    public Quaternion Rot;
    public float T;
    public Vector2 EaseIO;

    public SplineNode(Vector3 p, Quaternion q, float t, Vector2 io)
    {
      Point = p;
      Rot = q;
      T = t;
      EaseIO = io;
    }

    public SplineNode(SplineNode o)
    {
      Point = o.Point;
      Rot = o.Rot;
      T = o.T;
      EaseIO = o.EaseIO;
    }
  }
}
