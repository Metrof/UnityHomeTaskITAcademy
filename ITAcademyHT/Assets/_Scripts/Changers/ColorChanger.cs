using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : IObjectChanger
{
    private Renderer _renderer;
    public Renderer ObjectRenderer { get { return _renderer ??= FlyingObj.GetComponent<Renderer>(); } }
    public FlyingObject FlyingObj { get; private set; }

    public void AddObjectComponent(FlyingObject flyingObject)
    {
        FlyingObj = flyingObject;
        _renderer = FlyingObj.GetComponent<Renderer>();
    }

    public void ChangeObject()
    {
        ObjectRenderer.material.color = new Color(Random.value, Random.value, Random.value, 1);
    }
}
