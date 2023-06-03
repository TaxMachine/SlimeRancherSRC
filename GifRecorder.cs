// Decompiled with JetBrains decompiler
// Type: GifRecorder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Gif.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class GifRecorder : MonoBehaviour
{
  public GameObject storeGifUI;
  public GameObject cannotStoreGifUI;
  private Queue<Frame> frames = new Queue<Frame>();
  private float nextFrameTime;
  private float lastFrameTime;
  private List<CameraHook> hooks = new List<CameraHook>();
  private static float MIN_FRAME_DELAY = 0.075f;
  private static float GIF_LENGTH = 3.5f;
  private RenderTexture renderTex;
  private static int GIF_WIDTH = 560;
  private static int GIF_HEIGHT = 315;
  private static float MILLIS_PER_SEC = 1000f;

  public void Update()
  {
    if (!SRSingleton<GameContext>.Instance.OptionsDirector.bufferForGif)
    {
      foreach (Frame frame in frames)
        Destroyer.Destroy(frame.tex, "GifRecorder.Update#1");
      frames.Clear();
      if (hooks.Count <= 0)
        return;
      foreach (UnityEngine.Object hook in hooks)
        Destroyer.Destroy(hook, "GifRecorder.Update#2");
      hooks.Clear();
    }
    else
    {
      if (hooks.Count == 0)
      {
        frames.Clear();
        foreach (Component allCamera in Camera.allCameras)
        {
          CameraHook cameraHook = allCamera.gameObject.AddComponent<CameraHook>();
          cameraHook.recorder = this;
          hooks.Add(cameraHook);
        }
      }
      float time = Time.time;
      if (time >= (double) nextFrameTime)
      {
        renderTex = new RenderTexture(GIF_WIDTH, GIF_HEIGHT, 24);
        float delay = frames.Count == 0 ? MIN_FRAME_DELAY : time - lastFrameTime;
        frames.Enqueue(new Frame(renderTex, time, delay));
        if (frames.Peek().time < time - (double) GIF_LENGTH)
          Destroyer.Destroy(frames.Dequeue().tex, "GifRecorder.Update#3");
        lastFrameTime = time;
        nextFrameTime = time + MIN_FRAME_DELAY;
      }
      else
        renderTex = null;
    }
  }

  public void DoRenderImage(RenderTexture source)
  {
    if (!(renderTex != null))
      return;
    Graphics.Blit(source, renderTex);
  }

  public void MaybeSaveGif()
  {
    if (SRSingleton<GameContext>.Instance.OptionsDirector.bufferForGif)
    {
      StoreGifUI ui = Instantiate(storeGifUI).GetComponent<StoreGifUI>();
      ui.onConfirm = () => StartCoroutine(SaveGif(() => ui.Close()));
    }
    else
      Instantiate(cannotStoreGifUI);
  }

  private IEnumerator SaveGif(UnityAction onComplete)
  {
    Debug.Log("Starting saving Gif...");
    using (FileStream fileStream = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SlimeRancher-" + string.Format("{0:yyyy-MM-dd-hh-mm-ss-ff}", DateTime.Now) + ".gif"), FileMode.OpenOrCreate))
    {
      AnimatedGifEncoder gifEncoder = new AnimatedGifEncoder();
      try
      {
        gifEncoder.SetRepeat(0);
        gifEncoder.Start(fileStream);
        int count = 0;
        foreach (Frame frame1 in frames)
        {
          Frame frame = frame1;
          yield return new WaitForEndOfFrame();
          Texture2D texture2D = new Texture2D(GIF_WIDTH, GIF_HEIGHT, TextureFormat.RGB24, false);
          try
          {
            RenderTexture.active = frame.tex;
            texture2D.ReadPixels(new Rect(0.0f, 0.0f, GIF_WIDTH, GIF_HEIGHT), 0, 0);
          }
          finally
          {
            RenderTexture.active = null;
          }
          gifEncoder.AddFrame(texture2D.GetPixels32(), GIF_WIDTH, GIF_HEIGHT);
          gifEncoder.SetDelay(Mathf.RoundToInt(frame.delay * MILLIS_PER_SEC));
          if (++count > 1000)
          {
            Debug.Log("Too many frames in gif, ejecting...");
            break;
          }
          frame = null;
        }
      }
      finally
      {
        gifEncoder.Finish();
      }
      gifEncoder = null;
    }
    Debug.Log("...finished Gif");
    if (onComplete != null)
      onComplete();
  }

  private class Frame
  {
    public RenderTexture tex;
    public float time;
    public float delay;

    public Frame(RenderTexture tex, float time, float delay)
    {
      this.tex = tex;
      this.time = time;
      this.delay = delay;
    }
  }

  private class CameraHook : SRBehaviour
  {
    public GifRecorder recorder;

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
      recorder.DoRenderImage(source);
      Graphics.Blit(source, destination);
    }

    public void OnDestroy() => recorder.hooks.Remove(this);
  }
}
