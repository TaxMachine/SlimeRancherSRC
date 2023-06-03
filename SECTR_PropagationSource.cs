// Decompiled with JetBrains decompiler
// Type: SECTR_PropagationSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof (SECTR_Member))]
[AddComponentMenu("SECTR/Audio/SECTR Propagation Source")]
public class SECTR_PropagationSource : SECTR_AudioSource
{
  private SECTR_Member cachedMember;
  private List<SECTR_Graph.Node> path = new List<SECTR_Graph.Node>(32);
  private List<PathSound> activeSounds = new List<PathSound>(4);
  private float directDistanceToListener;
  private bool playing;
  private bool played;
  [SECTR_ToolTip("When the listener gets within this distance of a portal, the sound direction will start to blend towards the next portal or source position.", 0.0f, -1f)]
  public float InterpDistance = 2f;

  public override bool IsPlaying => playing || activeSounds.Count > 0;

  public override void Play()
  {
    playing = true;
    played = false;
  }

  public override void Stop(bool stopImmediately)
  {
    int count = activeSounds.Count;
    for (int index = 0; index < count; ++index)
      activeSounds[index]?.instance.Stop(stopImmediately);
    activeSounds.Clear();
    playing = false;
    played = false;
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    cachedMember = GetComponent<SECTR_Member>();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    cachedMember = null;
  }

  private void Update()
  {
    if (!playing || !(Cue != null) || cachedMember.Sectors.Count <= 0 || !SECTR_AudioSystem.Initialized)
      return;
    Vector3 position1 = SECTR_AudioSystem.Listener.position;
    Vector3 position2 = transform.position;
    directDistanceToListener = Vector3.Distance(position2, position1);
    bool flag1 = Cue.SourceCue.Spatialization == SECTR_AudioCue.Spatializations.Occludable3D;
    int count1 = activeSounds.Count;
    if (played && !Loop && !Cue.SourceCue.Loops && count1 == 0)
      Stop(false);
    else if (directDistanceToListener <= (double) Cue.SourceCue.MaxDistance)
    {
      PathSound pathSound = null;
      SECTR_Graph.FindShortestPath(ref path, position1, transform.position, 0);
      int count2 = path.Count;
      if (count2 > 0)
      {
        SECTR_Portal portal1 = path[0].Portal;
        SECTR_Portal portal2 = count2 > 1 ? path[1].Portal : null;
        bool flag2 = false;
        for (int index = 0; index < count1; ++index)
        {
          PathSound activeSound = activeSounds[index];
          if (portal1 == activeSound.firstPortal || portal1 == activeSound.secondPortal || portal2 == activeSound.firstPortal)
          {
            pathSound = activeSound;
            break;
          }
        }
        if (pathSound == null)
        {
          pathSound = new PathSound();
          flag2 = true;
        }
        pathSound.firstPortal = portal1;
        pathSound.secondPortal = portal2;
        pathSound.occluded = false;
        pathSound.firstDistance = 0.0f;
        pathSound.secondDistance = 0.0f;
        pathSound.distance = 0.0f;
        SECTR_AudioSystem.OcclusionModes occlusionModes = flag1 ? SECTR_AudioSystem.System.OcclusionFlags : 0;
        bool flag3 = (occlusionModes & SECTR_AudioSystem.OcclusionModes.Graph) != 0;
        if (count2 == 1 && path[0].Portal == null)
        {
          pathSound.firstDistance = directDistanceToListener;
          pathSound.secondDistance = directDistanceToListener;
        }
        else
        {
          for (int index = 0; index < count2; ++index)
          {
            SECTR_Portal portal3 = path[index].Portal;
            SECTR_Portal portal4 = index < count2 - 1 ? path[index + 1].Portal : null;
            Vector3 center = portal3.Center;
            switch (index)
            {
              case 0:
                pathSound.firstDistance += Vector3.Distance(center, position1);
                break;
              case 1:
                if ((bool) (Object) portal3)
                {
                  pathSound.secondDistance += Vector3.Distance(center, position1);
                  break;
                }
                break;
            }
            float num = !(bool) (Object) portal3 || !(bool) (Object) portal4 ? Vector3.Distance(center, position2) : Vector3.Distance(center, portal4.Center);
            pathSound.firstDistance += num;
            if (index >= 1)
              pathSound.secondDistance += num;
            if ((bool) (Object) portal3 & flag3 && !pathSound.occluded && (portal3.Flags & SECTR_Portal.PortalFlags.Closed) != 0)
              pathSound.occluded = true;
          }
        }
        SECTR_AudioSystem.OcclusionModes occlusionFlags = occlusionModes & ~SECTR_AudioSystem.OcclusionModes.Graph;
        if (!pathSound.occluded && occlusionFlags != 0)
          pathSound.occluded = SECTR_AudioSystem.IsOccluded(position2, occlusionFlags);
        _ComputeSoundSpatialization(position1, directDistanceToListener, pathSound);
        if (!pathSound.instance)
        {
          pathSound.instance = activeSounds.Count <= 0 ? SECTR_AudioSystem.Play(Cue, pathSound.position, Loop) : SECTR_AudioSystem.Clone(activeSounds[0].instance, pathSound.position);
          pathSound.instance.ForceInfinite();
          if (flag2)
          {
            activeSounds.Add(pathSound);
            ++count1;
          }
        }
        else
          pathSound.instance.Position = pathSound.position;
        pathSound.lastListenerPosition = position1;
        played = true;
      }
      float b = 1f;
      for (int index = 0; index < count1; ++index)
      {
        PathSound activeSound = activeSounds[index];
        if (activeSound != pathSound)
        {
          _ComputeSoundSpatialization(position1, directDistanceToListener, activeSound);
          activeSound.weight = activeSound.instance ? 1f - Mathf.Clamp01(Vector3.Distance(activeSound.lastListenerPosition, position1) * 0.5f) : 0.0f;
          b -= activeSound.weight;
        }
      }
      if (pathSound != null)
        pathSound.weight = Mathf.Max(0.0f, b);
      int index1 = 0;
      float minDistance = Cue.SourceCue.MinDistance;
      float num1 = (float) (1.0 / (Cue.SourceCue.MaxDistance - (double) minDistance));
      while (index1 < count1)
      {
        PathSound activeSound = activeSounds[index1];
        if (activeSound.instance)
        {
          activeSound.instance.Position = activeSound.position;
          float num2 = 1f;
          switch (Cue.SourceCue.Falloff)
          {
            case SECTR_AudioCue.FalloffTypes.Linear:
              num2 = 1f - Mathf.Clamp01((activeSound.distance - minDistance) * num1);
              break;
            case SECTR_AudioCue.FalloffTypes.Logrithmic:
              num2 = Mathf.Clamp01(1f / Mathf.Max((float) (activeSound.distance - (double) minDistance - 1.0), 1f / 1000f));
              break;
          }
          activeSound.instance.Volume = num2 * activeSound.weight * volume;
          activeSound.instance.Pitch = pitch;
          if (flag1)
            activeSound.instance.ForceOcclusion(activeSound.occluded);
        }
        if (activeSound.weight <= 0.0 || !activeSound.instance)
        {
          activeSound.instance.Stop(true);
          activeSounds.RemoveAt(index1);
          --count1;
        }
        else
          ++index1;
      }
    }
    else
    {
      for (int index = 0; index < count1; ++index)
        activeSounds[index]?.instance.Stop(false);
      activeSounds.Clear();
    }
  }

  protected override void OnVolumePitchChanged()
  {
  }

  private void _ComputeSoundSpatialization(
    Vector3 listenerPosition,
    float distanceToListener,
    PathSound pathSound)
  {
    if (pathSound.firstPortal != null)
    {
      Vector3 center = pathSound.firstPortal.Center;
      Vector3 a = (bool) (Object) pathSound.secondPortal ? pathSound.secondPortal.Center : transform.position;
      float f = pathSound.firstPortal.BoundingBox.SqrDistance(listenerPosition);
      Vector3 vector3;
      float num;
      if (f >= InterpDistance * (double) InterpDistance)
      {
        vector3 = center;
        num = pathSound.firstDistance;
      }
      else
      {
        float t = Mathf.Clamp01(Mathf.Sqrt(f) / InterpDistance);
        vector3 = Vector3.Lerp(a, center, t);
        num = Mathf.Lerp(pathSound.secondDistance, pathSound.firstDistance, t);
      }
      pathSound.position = vector3;
      pathSound.distance = num;
    }
    else
    {
      pathSound.position = transform.position;
      pathSound.distance = distanceToListener;
    }
  }

  private class PathSound
  {
    public SECTR_AudioCueInstance instance;
    public SECTR_Portal firstPortal;
    public SECTR_Portal secondPortal;
    public float firstDistance;
    public float secondDistance;
    public float distance;
    public Vector3 position;
    public Vector3 lastListenerPosition;
    public float weight = 1f;
    public bool occluded;
  }
}
