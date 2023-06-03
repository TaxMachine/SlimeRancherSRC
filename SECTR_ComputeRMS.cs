// Decompiled with JetBrains decompiler
// Type: SECTR_ComputeRMS
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
[ExecuteInEditMode]
[AddComponentMenu("")]
public class SECTR_ComputeRMS : MonoBehaviour
{
  private List<BakeInfo> hdrBakeList;
  private List<SECTR_ComputeRMS> activeBakeList = new List<SECTR_ComputeRMS>();
  private int hdrBakeIndex;
  private SECTR_AudioCue cue;
  private SECTR_AudioCue.ClipData clipData;
  private List<float> samples = new List<float>();
  private int numChannels;

  public float Progress
  {
    get
    {
      if (hdrBakeList != null)
      {
        int count1 = hdrBakeList.Count;
        int count2 = activeBakeList.Count;
        float a = (hdrBakeIndex - count2) / (float) count1;
        float b = hdrBakeIndex / (float) count1;
        float num = 1f;
        for (int index = 0; index < count2; ++index)
        {
          SECTR_ComputeRMS activeBake = activeBakeList[index];
          if ((bool) (Object) activeBake)
            num = Mathf.Min(num, activeBake.Progress);
        }
        return Mathf.Lerp(a, b, num);
      }
      AudioSource component = GetComponent<AudioSource>();
      return (bool) (Object) component ? component.time / component.clip.length : 1f;
    }
  }

  private void OnEnable()
  {
  }

  private void OnDisable()
  {
  }

  private void Update()
  {
    bool flag1;
    if (hdrBakeList != null)
    {
      int count1 = hdrBakeList.Count;
      flag1 = count1 == 0;
      if (!flag1)
      {
        if (activeBakeList.Count == 0)
        {
          if (this.hdrBakeIndex == count1)
          {
            flag1 = true;
          }
          else
          {
            int num = Mathf.Min(this.hdrBakeIndex + 4, count1);
            for (int hdrBakeIndex = this.hdrBakeIndex; hdrBakeIndex < num; ++hdrBakeIndex)
            {
              BakeInfo hdrBake = hdrBakeList[hdrBakeIndex];
              GameObject gameObject = new GameObject("Bake " + hdrBake.cue.name + hdrBake.clipData.Clip.name);
              gameObject.transform.parent = transform;
              gameObject.transform.localPosition = Vector3.zero;
              gameObject.hideFlags = HideFlags.HideAndDontSave;
              SECTR_ComputeRMS sectrComputeRms = gameObject.AddComponent<SECTR_ComputeRMS>();
              sectrComputeRms._StartCompute(hdrBake.cue, hdrBake.clipData);
              activeBakeList.Add(sectrComputeRms);
            }
            this.hdrBakeIndex = num;
          }
        }
        else
        {
          bool flag2 = true;
          int count2 = activeBakeList.Count;
          for (int index = 0; index < count2; ++index)
          {
            if (activeBakeList[index] != null)
            {
              flag2 = false;
              break;
            }
          }
          if (flag2)
            activeBakeList.Clear();
        }
      }
    }
    else
    {
      int count = samples.Count;
      flag1 = clipData == null;
      if (!flag1 && count > 0)
      {
        int num1 = AudioSettings.outputSampleRate * numChannels;
        int num2 = (int) (clipData.Clip.length * (double) num1);
        int num3 = num1 / 10;
        AudioSource component = GetComponent<AudioSource>();
        if (!component.isPlaying && count >= num2 - num3 || component.isPlaying && count >= num2)
        {
          int length = Mathf.CeilToInt(count / (float) num1) + 1;
          float[] numArray = new float[length];
          int num4 = 0;
          for (int index1 = 1; index1 < length; ++index1)
          {
            float num5 = 0.0f;
            int num6 = 0;
            for (int index2 = 0; index2 < num1 && num4 < count; ++index2)
            {
              ++num6;
              float sample = samples[num4++];
              num5 += sample * sample;
            }
            float f = Mathf.Sqrt(num5 / num6);
            numArray[index1] = Mathf.Abs(f) >= 1.0 / 1000.0 || index1 != length - 1 || length <= 2 ? Mathf.Clamp(20f * Mathf.Log10(f), -160f, 160f) : numArray[index1 - 1];
          }
          numArray[0] = !cue.Loops ? numArray[1] : numArray[numArray.Length - 1];
          bool flag3 = false;
          for (int index = 0; index < length; ++index)
          {
            if (numArray[index] > -160.0)
            {
              flag3 = false;
              break;
            }
          }
          int num7 = flag3 ? 1 : 0;
          flag1 = true;
        }
        else if (!component.isPlaying)
          flag1 = true;
      }
    }
    if (!flag1)
      return;
    Destroyer.Destroy(this.gameObject, "SECTR_ComputeRMS.Update");
  }

  private void OnAudioFilterRead(float[] samples, int numChannels)
  {
    this.numChannels = numChannels;
    this.samples.AddRange(samples);
  }

  public void _StartCompute(SECTR_AudioCue cue, SECTR_AudioCue.ClipData clipData)
  {
    this.cue = cue;
    this.clipData = clipData;
    AudioSource component = GetComponent<AudioSource>();
    component.clip = clipData.Clip;
    component.dopplerLevel = 0.0f;
    component.ignoreListenerPause = true;
    component.ignoreListenerVolume = true;
    component.bypassListenerEffects = true;
    component.bypassReverbZones = true;
    component.maxDistance = float.MaxValue;
    component.minDistance = float.MaxValue;
    component.rolloffMode = AudioRolloffMode.Linear;
    component.playOnAwake = false;
    component.loop = false;
    component.volume = 1f;
    samples.Clear();
    GetComponent<AudioSource>().Play();
  }

  private struct BakeInfo
  {
    public SECTR_AudioCue cue;
    public SECTR_AudioCue.ClipData clipData;

    public BakeInfo(SECTR_AudioCue cue, SECTR_AudioCue.ClipData clipData)
    {
      this.cue = cue;
      this.clipData = clipData;
    }
  }
}
