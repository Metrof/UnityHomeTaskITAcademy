using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerFactory : AbstractObjectFactory
{
    public override IObjectChanger GetObjectChanger(FlyingObject flyingObject)
    {
        return new ColorChanger(flyingObject);
    }
}
