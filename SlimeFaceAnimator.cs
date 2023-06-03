// Decompiled with JetBrains decompiler
// Type: SlimeFaceAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SlimeFaceAnimator : RegisteredActorBehaviour, RegistryUpdateable
{
  private bool feral;
  private bool glitch;
  private bool seekingFood;
  private bool shouldBlush;
  public SlimeAppearanceApplicator appearanceApplicator;
  private State HAPPY;
  private State ANGRY;
  private State HUNGRY;
  private State STARVING;
  private State SCARED;
  private State ELATED;
  private State FERAL;
  private State AWE;
  private State LONG_AWE;
  private State ALARM;
  private State WINCE;
  private State MINOR_WINCE;
  private State ATTACK_TELEGRAPH;
  private State CHOMP_OPEN;
  private State CHOMP_OPEN_QUICK;
  private State CHOMP_CLOSED;
  private State INVOKE;
  private State GRIMACE;
  private State FRIED;
  private State SNEEZE;
  private State GLITCH;
  private State currState;
  private SlimeEmotions emotions;
  private SlimeAudio slimeAudio;
  private HashSet<string> triggers = new HashSet<string>();

  public void Awake()
  {
    emotions = GetComponentInParent<SlimeEmotions>();
    slimeAudio = GetComponentInParent<SlimeAudio>();
  }

  public override void Start()
  {
    base.Start();
    InitStates();
    SetState(HAPPY);
  }

  private void InitStates()
  {
    HAPPY = new BlinkingState(this, SlimeFace.SlimeExpression.Happy, null, state =>
    {
      float curr1 = emotions.GetCurr(SlimeEmotions.Emotion.AGITATION);
      float curr2 = emotions.GetCurr(SlimeEmotions.Emotion.HUNGER);
      float curr3 = emotions.GetCurr(SlimeEmotions.Emotion.FEAR);
      if (feral)
        SetState(FERAL);
      else if (glitch)
        SetState(GLITCH);
      else if (curr1 > 0.89999997615814209)
        SetState(ANGRY);
      else if (curr2 > 0.66600000858306885 && seekingFood)
        SetState(HUNGRY);
      else if (curr2 > 0.99000000953674316)
        SetState(STARVING);
      else if (curr3 > 0.60000002384185791)
      {
        SetState(SCARED);
      }
      else
      {
        if (curr1 >= 0.0099999997764825821 || curr2 >= 0.33000001311302185 || curr3 >= 0.0099999997764825821)
          return;
        SetState(ELATED);
      }
    });
    ANGRY = new State(this, SlimeFace.SlimeExpression.Angry, null, state =>
    {
      if (emotions.GetCurr(SlimeEmotions.Emotion.AGITATION) >= 0.699999988079071)
        return;
      SetState(HAPPY);
    });
    FERAL = new BlinkingState(this, SlimeFace.SlimeExpression.Feral, null, state =>
    {
      if (feral)
        return;
      SetState(HAPPY);
    });
    HUNGRY = new State(this, SlimeFace.SlimeExpression.Hungry, null, state =>
    {
      float curr = emotions.GetCurr(SlimeEmotions.Emotion.HUNGER);
      if (emotions.GetCurr(SlimeEmotions.Emotion.AGITATION) > 0.89999997615814209)
        SetState(ANGRY);
      else if (curr > 0.99000000953674316)
      {
        SetState(STARVING);
      }
      else
      {
        if (curr >= 0.40000000596046448 && seekingFood)
          return;
        SetState(HAPPY);
      }
    });
    STARVING = new State(this, SlimeFace.SlimeExpression.Starving, null, state =>
    {
      float curr = emotions.GetCurr(SlimeEmotions.Emotion.HUNGER);
      if (emotions.GetCurr(SlimeEmotions.Emotion.AGITATION) > 0.89999997615814209)
      {
        SetState(ANGRY);
      }
      else
      {
        if (curr >= 0.98000001907348633)
          return;
        SetState(HAPPY);
      }
    });
    SCARED = new State(this, SlimeFace.SlimeExpression.Scared, slimeAudio.slimeSounds.voiceFearCue, state =>
    {
      if (emotions.GetCurr(SlimeEmotions.Emotion.FEAR) >= 0.40000000596046448)
        return;
      SetState(HAPPY);
    });
    ELATED = new BlinkingState(this, SlimeFace.SlimeExpression.Elated, slimeAudio.slimeSounds.voiceFunCue, state =>
    {
      double curr4 = emotions.GetCurr(SlimeEmotions.Emotion.AGITATION);
      float curr5 = emotions.GetCurr(SlimeEmotions.Emotion.HUNGER);
      float curr6 = emotions.GetCurr(SlimeEmotions.Emotion.FEAR);
      if (curr4 <= 0.019999999552965164 && curr5 <= 0.34999999403953552 && curr6 <= 0.019999999552965164)
        return;
      SetState(HAPPY);
    });
    AWE = new State(this, SlimeFace.SlimeExpression.Awe, slimeAudio.slimeSounds.voiceAweCue, state =>
    {
      if (Time.fixedTime <= state.startTime + 1.0)
        return;
      SetState(HAPPY);
    });
    LONG_AWE = new State(this, SlimeFace.SlimeExpression.Awe, slimeAudio.slimeSounds.voiceAweCue, state =>
    {
      if (Time.fixedTime <= state.startTime + 3.0)
        return;
      SetState(HAPPY);
    });
    ALARM = new State(this, SlimeFace.SlimeExpression.Alarm, slimeAudio.slimeSounds.voiceAlarmCue, state =>
    {
      if (Time.fixedTime <= state.startTime + 1.0)
        return;
      SetState(HAPPY);
    });
    WINCE = new State(this, SlimeFace.SlimeExpression.Wince, slimeAudio.slimeSounds.voiceDamageCue, state =>
    {
      if (Time.fixedTime <= state.startTime + 1.0)
        return;
      SetState(HAPPY);
    });
    SNEEZE = new State(this, SlimeFace.SlimeExpression.Wince, slimeAudio.slimeSounds.sneezeCue, state =>
    {
      if (Time.fixedTime <= state.startTime + 1.0)
        return;
      SetState(HAPPY);
    });
    MINOR_WINCE = new State(this, SlimeFace.SlimeExpression.Wince, null, state =>
    {
      if (Time.fixedTime <= state.startTime + 0.20000000298023224)
        return;
      SetState(HAPPY);
    });
    ATTACK_TELEGRAPH = new State(this, SlimeFace.SlimeExpression.AttackTelegraph, null, state =>
    {
      if (Time.fixedTime <= state.startTime + 1.0)
        return;
      SetState(HAPPY);
    });
    CHOMP_OPEN = new State(this, SlimeFace.SlimeExpression.ChompOpen, null, state =>
    {
      if (Time.fixedTime <= state.startTime + 1.0)
        return;
      SetState(CHOMP_CLOSED);
    });
    CHOMP_OPEN_QUICK = new State(this, SlimeFace.SlimeExpression.ChompOpen, null, state =>
    {
      if (Time.fixedTime <= state.startTime + 0.25)
        return;
      SetState(CHOMP_CLOSED);
    });
    CHOMP_CLOSED = new State(this, SlimeFace.SlimeExpression.ChompClosed, null, state =>
    {
      if (Time.fixedTime <= state.startTime + 2.0)
        return;
      SetState(HAPPY);
    });
    INVOKE = new State(this, SlimeFace.SlimeExpression.Invoke, null, state =>
    {
      if (Time.fixedTime <= state.startTime + 3.0)
        return;
      SetState(HAPPY);
    });
    GRIMACE = new State(this, SlimeFace.SlimeExpression.Grimace, null, state =>
    {
      if (Time.fixedTime <= state.startTime + (double) BoomSlimeExplode.EXPLOSION_PREP_TIME)
        return;
      SetState(HAPPY);
    });
    FRIED = new State(this, SlimeFace.SlimeExpression.Fried, null, state =>
    {
      if (Time.fixedTime <= state.startTime + (double) BoomSlimeExplode.EXPLOSION_RECOVERY_TIME)
        return;
      SetState(HAPPY);
    });
    GLITCH = new GlitchState(this, SlimeFace.SlimeExpression.Glitch);
    HAPPY.AddReact("triggerAwe", AWE);
    HUNGRY.AddReact("triggerAwe", AWE);
    HAPPY.AddReact("triggerLongAwe", LONG_AWE);
    HUNGRY.AddReact("triggerLongAwe", LONG_AWE);
  }

  public void RegistryUpdate()
  {
    if (!hasStarted)
      return;
    currState.Update();
  }

  public void SetGlitch()
  {
    glitch = true;
    SetState(GLITCH);
  }

  public void SetFeral()
  {
    feral = true;
    triggers.Clear();
    if (!(currState is BlinkingState))
      return;
    ((BlinkingState) currState).ClearBlinkTime();
  }

  public void ClearFeral()
  {
    feral = false;
    if (currState == CHOMP_OPEN || currState == CHOMP_OPEN_QUICK)
      return;
    triggers.Clear();
    if (!(currState is BlinkingState))
      return;
    ((BlinkingState) currState).ClearBlinkTime();
  }

  public void SetSeekingFood(bool val) => seekingFood = val;

  public void SetShouldBlush(bool blush) => shouldBlush = blush;

  public void SetTrigger(string trigger) => triggers.Add(trigger);

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
    public SlimeFaceAnimator anim;
    protected SlimeFace.SlimeExpression facialExpression;
    protected SECTR_AudioCue cue;
    public float startTime;
    protected UpdateDelegate updateDel;
    protected Dictionary<string, State> reacts = new Dictionary<string, State>();

    public override string ToString() => facialExpression.ToString();

    public State(
      SlimeFaceAnimator anim,
      SlimeFace.SlimeExpression facialExpression,
      SECTR_AudioCue cue,
      UpdateDelegate update)
    {
      this.facialExpression = facialExpression;
      this.anim = anim;
      this.cue = cue;
      updateDel = update;
    }

    public virtual void Init()
    {
      startTime = Time.fixedTime;
      ApplyFacialExpression(facialExpression);
      if (cue != null)
        anim.slimeAudio.Play(cue);
      AddReact("triggerAlarm", anim.ALARM);
      AddReact("triggerAttackTelegraph", anim.ATTACK_TELEGRAPH);
      AddReact("triggerChompOpen", anim.CHOMP_OPEN);
      AddReact("triggerChompOpenQuick", anim.CHOMP_OPEN_QUICK);
      AddReact("triggerChompClosed", anim.CHOMP_CLOSED);
      AddReact("triggerWince", anim.WINCE);
      AddReact("triggerMinorWince", anim.MINOR_WINCE);
      AddReact("triggerConcentrate", anim.INVOKE);
      AddReact("triggerGrimace", anim.GRIMACE);
      AddReact("triggerFried", anim.FRIED);
      AddReact("triggerSneeze", anim.SNEEZE);
    }

    public virtual void Update()
    {
      if (React())
        return;
      updateDel(this);
    }

    protected virtual bool React()
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

    protected void ApplyFacialExpression(SlimeFace.SlimeExpression faceExpression) => anim.appearanceApplicator.SetExpression(faceExpression);

    public delegate void UpdateDelegate(State state);
  }

  private class BlinkingState : State
  {
    private float blinkTime;
    private float unblinkTime = float.PositiveInfinity;
    private const float MIN_BLINK_GAP = 0.5f;
    private const float MAX_BLINK_GAP = 4.5f;
    private const float BLINK_TIME = 0.1f;

    public BlinkingState(
      SlimeFaceAnimator anim,
      SlimeFace.SlimeExpression facialExpression,
      SECTR_AudioCue cue,
      UpdateDelegate del)
      : base(anim, facialExpression, cue, del)
    {
    }

    public override void Init()
    {
      base.Init();
      blinkTime = Time.time + Random.Range(0.5f, 4.5f);
    }

    public override void Update()
    {
      base.Update();
      if (anim.currState != this)
        return;
      if (Time.time >= (double) unblinkTime)
      {
        ApplyFacialExpression(facialExpression);
        if (anim.shouldBlush)
          ApplyFacialExpression(SlimeFace.SlimeExpression.Blush);
        unblinkTime = float.PositiveInfinity;
        blinkTime = Time.time + Random.Range(0.5f, 4.5f);
      }
      else
      {
        if (Time.time < (double) blinkTime)
          return;
        if (!anim.feral)
        {
          ApplyFacialExpression(facialExpression);
          ApplyFacialExpression(anim.shouldBlush ? SlimeFace.SlimeExpression.BlushBlink : SlimeFace.SlimeExpression.Blink);
        }
        unblinkTime = Time.time + 0.1f;
        blinkTime = float.PositiveInfinity;
      }
    }

    public void ClearBlinkTime()
    {
      unblinkTime = 0.0f;
      blinkTime = 0.0f;
    }
  }

  private class GlitchState : State
  {
    public GlitchState(SlimeFaceAnimator anim, SlimeFace.SlimeExpression facialExpression)
      : base(anim, facialExpression, null, s => { })
    {
    }

    protected override bool React() => false;
  }
}
