using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormChanger : IBehaviorStrategy
{
    public void ChangeObject(FlyingObject flyingObject)
    {
        flyingObject.ObjectMeshFilter.sharedMesh = flyingObject.Meshes[Random.Range(0, flyingObject.Meshes.Length)];
    }
}
