using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewBase : MonoBehaviour
{
    protected virtual void Awake()
    {

    }

    public virtual void Anchor(float x, float y, float z)
    {
        this.transform.localPosition = new Vector3(x, y, z);
    }

    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
