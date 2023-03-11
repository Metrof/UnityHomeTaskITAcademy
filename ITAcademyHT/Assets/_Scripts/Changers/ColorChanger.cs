using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : IBehaviorStrategy
{
    public void ChangeObject(FlyingObject flyingObject)
    {
        flyingObject.ObjectRenderer.material.color = new Color(Random.value, Random.value, Random.value, 1);
    }
}
