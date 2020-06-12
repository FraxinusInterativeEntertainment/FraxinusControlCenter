using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapBeacon : MonoBehaviour
{
    protected float m_movementDuration = 1f;

    public virtual void UpdatePosition(Vector3 _pos)
    {
        this.transform.DOLocalMove(_pos, m_movementDuration);
    }

    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }
}
