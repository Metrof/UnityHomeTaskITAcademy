using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormChangerFactory : AbstractObjectFactory
{
    private Mesh[] _meshes;
    public FormChangerFactory(Mesh[] meshes)
    {
        _meshes = meshes;
    }

    public override IObjectChanger GetObjectChanger(FlyingObject flyingObject)
    {
        return new FormChanger(flyingObject, _meshes);
    }
}
