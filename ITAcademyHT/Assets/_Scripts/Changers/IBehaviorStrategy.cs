using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile 
{
    FlyingObject FlyingObj { get; }
    void AddObjectComponent(FlyingObject flyingObject);
    void ChangeObject();
}
