using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartPoint : MonoBehaviour
{
    [SerializeField] FlyingObject _flyingObjectPref;

    private readonly IObjectChanger[] _strategies = new IObjectChanger[3] { new ScaleChanger(), new ColorChanger(), new FormChanger() };
    private Controller _controller;

    private void Awake()
    {
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
        Instantiate(_flyingObjectPref, transform.position, Quaternion.identity).SetStrategy(_strategies[Random.Range(0, _strategies.Length)]);
    }
}
