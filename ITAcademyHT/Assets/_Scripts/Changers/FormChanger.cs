using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormChanger : IObjectChanger
{
    private MeshFilter _meshFilter;
    private Mesh[] _meshes;

    public MeshFilter ObjectMeshFilter { get { return _meshFilter ??= FlyingObj.GetComponent<MeshFilter>(); } }
    public FlyingObject FlyingObj { get; private set; }

    public FormChanger(FlyingObject flyingObj, Mesh[] meshes)
    {
        FlyingObj = flyingObj;
        _meshes = meshes;
        _meshFilter = FlyingObj.GetComponent<MeshFilter>();
    }

    public void ChangeObject()
    {
        if (_meshes.Length > 0)
        {
            ObjectMeshFilter.sharedMesh = _meshes[Random.Range(0, _meshes.Length)];
        }
    }
}
