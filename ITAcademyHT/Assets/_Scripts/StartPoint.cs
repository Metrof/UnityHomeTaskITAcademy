using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartPoint : MonoBehaviour
{
    [SerializeField] FlyingObject _flyingObjectPref;
    [SerializeField] private float _minScale = 0.2f;
    [SerializeField] private float _maxScale = 0.7f;
    [SerializeField] Mesh[] _meshes;

    private AbstractObjectFactory[] _strategies;
    private Controller _controller;
    private void Awake()
    {
        _strategies = new AbstractObjectFactory[3] { new ScaleChangerFactory(_minScale, _maxScale), new ColorChangerFactory(), new FormChangerFactory(_meshes) };
        _controller = new Controller();
    }
    private void OnEnable()
    {
        _controller.Enable();
        _controller.GameMap.SpawnObject.performed += SpawnObject;
    }

    private void OnDisable()
    {
        _controller.GameMap.SpawnObject.performed -= SpawnObject;
        _controller.Disable();
    }

    private void SpawnObject(InputAction.CallbackContext obj)
    {
        FlyingObject flyingObject = Instantiate(_flyingObjectPref, transform.position, Quaternion.identity);
        flyingObject.SetStrategy(_strategies[Random.Range(0, _strategies.Length)].GetObjectChanger(flyingObject));
    }
}
