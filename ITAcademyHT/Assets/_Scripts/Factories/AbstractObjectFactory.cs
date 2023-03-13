using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractObjectFactory
{
    public abstract IObjectChanger GetObjectChanger(FlyingObject flyingObject);
}
