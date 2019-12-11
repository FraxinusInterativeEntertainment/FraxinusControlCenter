using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVO
{
    public int sceneIndex { get; private set; }

    public SceneVO(int _index)
    {
        sceneIndex = _index;
    }
}
