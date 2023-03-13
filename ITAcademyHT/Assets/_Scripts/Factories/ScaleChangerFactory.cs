using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChangerFactory : AbstractObjectFactory
{
    private float _minScale;
    private float _maxScale;
    public ScaleChangerFactory(float minScale, float maxScale)
    {
        _minScale = minScale;
        _maxScale = maxScale;
    }

    public override IObjectChanger GetObjectChanger(FlyingObject flyingObject)
    {
        return new ScaleChanger(flyingObject, _minScale, _maxScale);
    }
}
