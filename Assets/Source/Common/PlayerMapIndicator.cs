using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapIndicator : MonoBehaviour
{
    public void UpdatePosition(Vector3 _pos)
    {
        this.transform.localPosition = _pos;
    }
}
