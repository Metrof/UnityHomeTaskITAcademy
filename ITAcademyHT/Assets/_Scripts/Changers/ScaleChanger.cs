using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : IProjectile
{
    private readonly float _minScale = 0.2f;
    private readonly float _maxScale = 0.7f;

    public FlyingObject FlyingObj { get; private set; }

    public void AddObjectComponent(FlyingObject flyingObject)
    {
        FlyingObj = flyingObject;
    }

    public void ChangeObject()
    {
        FlyingObj.transform.localScale = Vector3.one * Random.Range(_minScale, _maxScale);
    }
}
