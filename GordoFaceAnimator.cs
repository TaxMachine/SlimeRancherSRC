// Decompiled with JetBrains decompiler
// Type: GordoFaceAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class GordoFaceAnimator : MonoBehaviour
{
  public const string STRAIN_TRIGGER = "Strain";
  private bool inVac;
  private State HAPPY;
  private State HUNGRY;
  private State STRAIN;
  private State currState;
  private SlimeAudio slimeAudio;
  private HashSet<string> triggers = new HashSet<string>();
  private GordoFaceComponents comps;
  private Renderer[] renderers;

  public void Awake()
  {
    comps = GetComponentInParent<GordoFaceComponents>();
    slimeAudio = GetComponentInParent<SlimeAudio>();
    List<Renderer> rendererList = new List<Renderer>();
    foreach (Renderer componentsInChild in GetComponentsInChildren<Renderer>())
    {
      if (componentsInChild.sharedMaterials.Length == 3)
        rendererList.Add(componentsInChild);
    }
    renderers = rendererList.ToArray();
  }

  public void Start()
  {
    InitStates();
    SetDefaultState();
  }

  private void InitStates()
  {
    HAPPY = new State(this, comps.blinkEyes, comps.happyMouth, null, state =>
    {
      if (!inVac)
        return;
      SetState(HUNGRY);
    });
    HUNGRY = new State(this, comps.blinkEyes, comps.chompOpenMouth, null, state =>
    {
      if (inVac)
        return;
      SetState(HAPPY);
    });
    STRAIN = new State(this, comps.strainEyes, comps.strainMouth, null, state => { });
  }

  public void Update() => currState.Update();

  public void SetInVac(bool val) => inVac = val;

  public void SetTrigger(string trigger) => triggers.Add(trigger);

  public void SetDefaultState() => SetState(HAPPY);

  private void SetState(State state)
  {
    if (state == currState)
      return;
    triggers.Clear();
    currState = state;
    currState.Init();
    currState.Update();
  }

  private class State
  {
    public GordoFaceAnimator anim;
    protected Material eyes;
    protected Material mouth;
    protected SECTR_AudioCue cue;
    public float startTime;
    protected UpdateDelegate updateDel;
    protected Dictionary<string, State> reacts = new Dictionary<string, State>();

    public override string ToString() => eyes.name + ":" + mouth.name;

    public State(
      GordoFaceAnimator anim,
      Material eyes,
      Material mouth,
      SECTR_AudioCue cue,
      UpdateDelegate update)
    {
      this.anim = anim;
      this.eyes = eyes;
      this.mouth = mouth;
      this.cue = cue;
      updateDel = update;
    }

    public virtual void Init()
    {
      startTime = Time.fixedTime;
      ApplyMats(eyes, mouth);
      if (cue != null)
        anim.slimeAudio.Play(cue);
      AddReact("Strain", anim.STRAIN);
    }

    public virtual void Update()
    {
      if (React())
        return;
      updateDel(this);
    }

    private bool React()
    {
      if (anim.triggers.Count > 0)
      {
        foreach (string trigger in anim.triggers)
        {
          if (reacts.ContainsKey(trigger))
          {
            anim.SetState(reacts[trigger]);
            anim.triggers.Remove(trigger);
            return true;
          }
        }
      }
      return false;
    }

    public void AddReact(string trigger, State state) => reacts[trigger] = state;

    protected void ApplyMats(Material eyes, Material mouth)
    {
      foreach (Renderer renderer in anim.renderers)
      {
        Material[] sharedMaterials = renderer.sharedMaterials;
        sharedMaterials[1] = eyes;
        sharedMaterials[2] = mouth;
        renderer.sharedMaterials = sharedMaterials;
      }
    }

    public delegate void UpdateDelegate(State state);
  }

  private class BlinkingState : State
  {
    private float blinkTime;
    private float unblinkTime = float.PositiveInfinity;
    private float MIN_BLINK_GAP = 0.5f;
    private float MAX_BLINK_GAP = 1f;
    private float BLINK_TIME = 0.1f;

    public BlinkingState(
      GordoFaceAnimator anim,
      Material eyes,
      Material mouth,
      SECTR_AudioCue cue,
      UpdateDelegate del)
      : base(anim, eyes, mouth, cue, del)
    {
    }

    public override void Init()
    {
      base.Init();
      blinkTime = Time.time + Random.Range(MIN_BLINK_GAP, MAX_BLINK_GAP);
    }

    public override void Update()
    {
      base.Update();
      if (Time.time >= (double) unblinkTime)
      {
        ApplyMats(eyes, mouth);
        unblinkTime = float.PositiveInfinity;
        blinkTime = Time.time + Random.Range(MIN_BLINK_GAP, MAX_BLINK_GAP);
      }
      else
      {
        if (Time.time < (double) blinkTime)
          return;
        ApplyMats(anim.comps.blinkEyes, mouth);
        unblinkTime = Time.time + BLINK_TIME;
        blinkTime = float.PositiveInfinity;
      }
    }
  }
}
