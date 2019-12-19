using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVO
{
    public string sceneName { get; private set; }

    public SceneVO(string _name)
    {
        sceneName = _name;
    }
}
