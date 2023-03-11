using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviorStrategy 
{
    FlyingObject FlyingObj { get; }
    void AddObjectComponent(FlyingObject flyingObject);
    void ChangeObject();
}
