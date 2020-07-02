using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapConfig
{
    public float realWorldWidth { get; set; }
    public float realWorldLength { get; set; }
    public float mapImageWidth { get; set; }
    public float mapImageLength { get; set; }

    public GameMapConfig(float _rwWidth, float _rwLength, float _mapImageWidth, float _mapImageLength)
    {
        realWorldWidth = _rwWidth;
        realWorldLength = _rwLength;
        mapImageWidth = _mapImageWidth;
        mapImageLength = _mapImageLength;
    }
}
