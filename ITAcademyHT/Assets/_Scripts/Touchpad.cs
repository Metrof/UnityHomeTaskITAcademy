using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MenuManager))]
public class Touchpad : MonoBehaviour
{
    [SerializeField] float _rotSpeed = 4;
    MenuManager _manager;
    Vector3 _rotateDir = Vector3.up;

    private void Awake()
    {
        _manager = GetComponent<MenuManager>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _manager.RotateCurrentShip(_rotateDir, _rotSpeed * Input.GetAxis("Mouse X"));
        }
    }
}
