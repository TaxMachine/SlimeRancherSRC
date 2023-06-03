// Decompiled with JetBrains decompiler
// Type: UiParticles.UiParticles
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UiParticles
{
  [RequireComponent(typeof (ParticleSystem))]
  public class UiParticles : MaskableGraphic
  {
    [FormerlySerializedAs("m_ParticleSystem")]
    private ParticleSystem m_ParticleSystem;
    [FormerlySerializedAs("m_ParticleSystemRenderer")]
    private ParticleSystemRenderer m_ParticleSystemRenderer;
    private ParticleSystem.Particle[] m_Particles;

    public ParticleSystem ParticleSystem
    {
      get => m_ParticleSystem;
      set
      {
        if (!SetPropertyUtility.SetClass(ref m_ParticleSystem, value))
          return;
        SetAllDirty();
      }
    }

    public ParticleSystemRenderer particleSystemRenderer
    {
      get => m_ParticleSystemRenderer;
      set
      {
        if (!SetPropertyUtility.SetClass(ref m_ParticleSystemRenderer, value))
          return;
        SetAllDirty();
      }
    }

    public override Texture mainTexture => material != null && material.mainTexture != null ? material.mainTexture : s_WhiteTexture;

    protected override void Awake()
    {
      ParticleSystem component1 = GetComponent<ParticleSystem>();
      ParticleSystemRenderer component2 = GetComponent<ParticleSystemRenderer>();
      if (m_Material == null)
        m_Material = component2.sharedMaterial;
      base.Awake();
      ParticleSystem = component1;
      particleSystemRenderer = component2;
    }

    public override void SetMaterialDirty()
    {
      base.SetMaterialDirty();
      if (!(particleSystemRenderer != null))
        return;
      particleSystemRenderer.sharedMaterial = m_Material;
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
      if (ParticleSystem == null)
        base.OnPopulateMesh(toFill);
      else
        GenerateParticlesBillboards(toFill);
    }

    private void InitParticlesBuffer()
    {
      ParticleSystem.MainModule main = ParticleSystem.main;
      if (m_Particles != null && m_Particles.Length >= main.maxParticles)
        return;
      m_Particles = new ParticleSystem.Particle[main.maxParticles];
    }

    private void GenerateParticlesBillboards(VertexHelper vh)
    {
      InitParticlesBuffer();
      int particles = ParticleSystem.GetParticles(m_Particles);
      vh.Clear();
      for (int index = 0; index < particles; ++index)
        DrawParticleBillboard(m_Particles[index], vh);
    }

    private void DrawParticleBillboard(ParticleSystem.Particle particle, VertexHelper vh)
    {
      Vector3 position1 = particle.position;
      Quaternion quaternion = Quaternion.Euler(particle.rotation3D);
      if (ParticleSystem.main.simulationSpace == ParticleSystemSimulationSpace.World)
        position1 = rectTransform.InverseTransformPoint(position1);
      Vector3 currentSize3D = particle.GetCurrentSize3D(ParticleSystem);
      Vector3 position2 = new Vector3((float) (-(double) currentSize3D.x * 0.5), currentSize3D.y * 0.5f);
      Vector3 vector3_1 = new Vector3(currentSize3D.x * 0.5f, currentSize3D.y * 0.5f);
      Vector3 vector3_2 = new Vector3(currentSize3D.x * 0.5f, (float) (-(double) currentSize3D.y * 0.5));
      Vector3 vector3_3 = new Vector3((float) (-(double) currentSize3D.x * 0.5), (float) (-(double) currentSize3D.y * 0.5));
      position2 = quaternion * position2 + position1;
      Vector3 position3 = quaternion * vector3_1 + position1;
      Vector3 position4 = quaternion * vector3_2 + position1;
      Vector3 position5 = quaternion * vector3_3 + position1;
      Color32 currentColor = particle.GetCurrentColor(ParticleSystem);
      int currentVertCount = vh.currentVertCount;
      Vector2[] vector2Array = new Vector2[4];
      if (!ParticleSystem.textureSheetAnimation.enabled)
      {
        vector2Array[0] = new Vector2(0.0f, 0.0f);
        vector2Array[1] = new Vector2(0.0f, 1f);
        vector2Array[2] = new Vector2(1f, 1f);
        vector2Array[3] = new Vector2(1f, 0.0f);
      }
      else
      {
        ParticleSystem.TextureSheetAnimationModule textureSheetAnimation = ParticleSystem.textureSheetAnimation;
        double num1 = particle.startLifetime - (double) particle.remainingLifetime;
        float num2 = particle.startLifetime / textureSheetAnimation.cycleCount;
        double num3 = num2;
        float time = (float) (num1 % num3) / num2;
        double num4 = textureSheetAnimation.frameOverTime.Evaluate(time);
        int num5 = textureSheetAnimation.numTilesY * textureSheetAnimation.numTilesX;
        double num6 = num5;
        double num7;
        int num8 = (int) (num7 = Mathf.Clamp(Mathf.Floor((float) (num4 * num6)), 0.0f, num5 - 1)) % textureSheetAnimation.numTilesX;
        int num9 = (int) num7 / textureSheetAnimation.numTilesY;
        float num10 = 1f / textureSheetAnimation.numTilesX;
        float num11 = 1f / textureSheetAnimation.numTilesY;
        int num12 = textureSheetAnimation.numTilesY - 1 - num9;
        float x1 = num8 * num10;
        float y1 = num12 * num11;
        float x2 = x1 + num10;
        float y2 = y1 + num11;
        vector2Array[0] = new Vector2(x1, y1);
        vector2Array[1] = new Vector2(x1, y2);
        vector2Array[2] = new Vector2(x2, y2);
        vector2Array[3] = new Vector2(x2, y1);
      }
      vh.AddVert(position5, currentColor, vector2Array[0]);
      vh.AddVert(position2, currentColor, vector2Array[1]);
      vh.AddVert(position3, currentColor, vector2Array[2]);
      vh.AddVert(position4, currentColor, vector2Array[3]);
      vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
      vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
    }

    protected virtual void Update()
    {
      if (ParticleSystem != null && ParticleSystem.isPlaying)
        SetVerticesDirty();
      if (!(particleSystemRenderer != null) || !particleSystemRenderer.enabled)
        return;
      particleSystemRenderer.enabled = false;
    }
  }
}
