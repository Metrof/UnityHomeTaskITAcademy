using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : IBehaviorStrategy
{
    public void ChangeObject(FlyingObject flyingObject)
    {
        flyingObject.transform.localScale = Vector3.one * Random.Range(0.2f, 0.7f);
    }
}
