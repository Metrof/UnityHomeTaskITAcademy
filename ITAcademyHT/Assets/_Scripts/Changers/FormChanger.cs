using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormChanger : IProjectile
{
    private MeshFilter _meshFilter;
    public MeshFilter ObjectMeshFilter { get { return _meshFilter ??= FlyingObj.GetComponent<MeshFilter>(); } }
    public FlyingObject FlyingObj { get; private set; }

    public void AddObjectComponent(FlyingObject flyingObject)
    {
        FlyingObj = flyingObject;
        _meshFilter = FlyingObj.GetComponent<MeshFilter>();
    }

    public void ChangeObject()
    {
        if (FlyingObj.Meshes.Length > 0)
        {
            ObjectMeshFilter.sharedMesh = FlyingObj.Meshes[Random.Range(0, FlyingObj.Meshes.Length)];
        }
    }
}
