using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapBeacon : MonoBehaviour
{
    protected float m_movementDuration = 1f;
    [SerializeField]
    protected Image m_beaconImage;

    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }

    public virtual void UpdatePosition(Vector3 _pos)
    {
        this.transform.DOLocalMove(_pos, m_movementDuration);
    }

    public virtual void SetBeaconColor(Color _color)
    {
        m_beaconImage.color = _color;
    }
}
