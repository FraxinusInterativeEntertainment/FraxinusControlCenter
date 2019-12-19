using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcesService
{
    public void LoadAsync<T>(string path, MonoBehaviour mono, Action<T> OnDone = null) where T : UnityEngine.Object
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogWarning("No asset linked to this ResourceAsset");
        }
        ResourceRequest request = Resources.LoadAsync(path, typeof(T));

        mono.StartCoroutine(AsyncResourcesLoad(request, OnDone));
    }

    public T Load<T>(string path) where T : UnityEngine.Object
    {
        T obj = Resources.Load<T>(path) as T;
        if (obj == null)
        {
            //Null exception
            Debug.Log("PATH: " + path + " does not exist!");
        }

        return obj;
    }

    private IEnumerator AsyncResourcesLoad<T>(ResourceRequest request, Action<T> OnDone = null) where T : UnityEngine.Object
    {
        while (!request.isDone)
        {
            yield return null;
        }

        if (OnDone != null)
        {
            OnDone(request.asset as T);
        }
    }
}
