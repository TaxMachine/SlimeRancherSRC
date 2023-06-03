// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Services.MessageOfTheDayServiceRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Services.Messages;
using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;

namespace MonomiPark.SlimeRancher.Services
{
  public class MessageOfTheDayServiceRequest
  {
    private readonly string url;
    private readonly int timeout;
    private bool isError;
    private bool isComplete;
    private bool isInitiated;
    private MessageOfTheDayV01 message;
    private Texture2D image;
    private UnityWebRequest messageRequest;
    private UnityWebRequest imageRequest;

    public event SuccessHandler OnSuccess;

    public event ErrorHandler OnError;

    public bool IsError() => isError;

    public bool IsComplete() => isComplete;

    public MessageOfTheDayV01 GetResultMessage() => message;

    public Texture2D GetResultImage() => image;

    public MessageOfTheDayServiceRequest(string url, int timeout)
    {
      this.url = url;
      this.timeout = timeout;
    }

    public void Begin()
    {
      isInitiated = !isComplete && !isInitiated ? true : throw new Exception("MessageOfTheDayServiceRequest is already complete or has been initiated. Create a new instance for a new request.");
      messageRequest = UnityWebRequest.Get(url);
      messageRequest.timeout = timeout;
      messageRequest.SendWebRequest().completed += OnMessageRequestComplete;
    }

    private void OnMessageRequestComplete(AsyncOperation operation)
    {
      if (!(operation is UnityWebRequestAsyncOperation operation1))
        return;
      OnMessageRequestComplete(operation1);
    }

    private void OnMessageRequestComplete(UnityWebRequestAsyncOperation operation)
    {
      if (!operation.webRequest.isNetworkError && !operation.webRequest.isHttpError)
      {
        ProcessRequest(operation.webRequest);
      }
      else
      {
        Log.Debug("Initial message request failed.", "message", operation.webRequest.error);
        operation.webRequest.Dispose();
        ProcessFailedRequest();
      }
    }

    private void ProcessRequest(UnityWebRequest request)
    {
      MessageOfTheDayV01 message;
      using (TextReader textReader = new StringReader(request.downloadHandler.text))
        message = (MessageOfTheDayV01) new XmlSerializer(typeof (MessageOfTheDayV01)).Deserialize(textReader);
      LoadImage(message);
    }

    private void LoadImage(MessageOfTheDayV01 message)
    {
      imageRequest = UnityWebRequest.Get(message.ImageUrl);
      imageRequest.timeout = timeout;
      imageRequest.downloadHandler = new DownloadHandlerTexture();
      imageRequest.SendWebRequest().completed += op => OnImageRequestComplete(op, message);
    }

    private void OnImageRequestComplete(AsyncOperation operation, MessageOfTheDayV01 message)
    {
      if (!(operation is UnityWebRequestAsyncOperation operation1))
        return;
      OnImageRequestComplete(operation1, message);
    }

    private void OnImageRequestComplete(
      UnityWebRequestAsyncOperation operation,
      MessageOfTheDayV01 message)
    {
      if (!operation.webRequest.isHttpError && !operation.webRequest.isNetworkError)
      {
        SetMessageResults(message, DownloadHandlerTexture.GetContent(operation.webRequest));
      }
      else
      {
        Log.Debug("Image request failed.", nameof (message), operation.webRequest.error);
        ProcessFailedRequest();
      }
    }

    private void ProcessFailedRequest()
    {
      isError = true;
      isComplete = true;
      message = null;
      image = null;
      if (OnError == null)
        return;
      OnError();
    }

    private void SetMessageResults(MessageOfTheDayV01 message, Texture2D image)
    {
      this.message = message;
      this.image = image;
      isError = false;
      isComplete = true;
      if (OnSuccess == null)
        return;
      OnSuccess(this.message, image);
    }

    ~MessageOfTheDayServiceRequest()
    {
      if (messageRequest != null)
        messageRequest.Dispose();
      if (imageRequest == null)
        return;
      imageRequest.Dispose();
    }

    public delegate void SuccessHandler(MessageOfTheDayV01 message, Texture2D image);

    public delegate void ErrorHandler();
  }
}
