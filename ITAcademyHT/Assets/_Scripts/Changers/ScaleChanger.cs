using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : IObjectChanger
{
    private readonly float _minScale;
    private readonly float _maxScale;

    public FlyingObject FlyingObj { get; private set; }

    public ScaleChanger(FlyingObject flyingObj, float minScale, float maxScale)
    {
        FlyingObj = flyingObj;
        _minScale = minScale;
        _maxScale = maxScale;
    }

    public void ChangeObject()
    {
        FlyingObj.transform.localScale = Vector3.one * Random.Range(_minScale, _maxScale);
    }
}
