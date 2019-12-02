using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class HttpService
{
    private string m_serverUrl;
    private HttpRequestType m_type;
    private WWWForm m_form;

    public HttpService(string _serverUrl, HttpRequestType _type, WWWForm _form = null)
    {
        m_serverUrl = _serverUrl;
        m_type = _type;
        m_form = _form;
    }

    public void SendRequest<T>(System.Action<T> _callback) where T : HttpResponse
    {
        if (m_type == HttpRequestType.Get)
        {
            GameManager.instance.StartCoroutine(DownloadFromServer<T>(m_serverUrl, _callback));
        }
        else if (m_type == HttpRequestType.Post)
        {
            GameManager.instance.StartCoroutine(UploadToServer<T>(m_serverUrl, m_form, _callback));
        }
    }

    private IEnumerator DownloadFromServer<T>(string _url, System.Action<T> _callback) where T : HttpResponse
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            //_callback((new HttpResponse(-99, www.error) as T));
        }
        else
        {
            T serverResponse = JsonConvert.DeserializeObject<T>(www.downloadHandler.text);
            _callback(serverResponse);
        }
    }

    private IEnumerator UploadToServer<T>(string _url, WWWForm _form, System.Action<T> _callback) where T : HttpResponse
    {
        UnityWebRequest www = UnityWebRequest.Post(_url, _form);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            //_callback(new HttpResponse(-99, www.error));
            //Throw NetworkError Exception
            Debug.Log(www.error);
        }
        else
        {
            T serverResponse = JsonConvert.DeserializeObject<T>(www.downloadHandler.text);
            _callback(serverResponse);
        }
    }
}

public enum HttpRequestType
{
    Get,
    Post
}

public class HttpResponse
{
    public int err_code { get; set; }
    public string err_msg { get; set; }

    public HttpResponse(int _errCode, string _errMsg)
    {
        err_code = _errCode;
        err_msg = _errMsg;
    }
}
