using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectChanger 
{
    FlyingObject FlyingObj { get; }
    void ChangeObject();
}
