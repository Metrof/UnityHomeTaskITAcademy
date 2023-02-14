using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamRotation : MonoBehaviour
{
    [SerializeField] float _maxVert = 10;
    [SerializeField] float _minVert = -20;
    [SerializeField] float _minHor = -20;
    [SerializeField] float _maxHor = 20;
    [SerializeField] float _sensitivity = 9;

    float _rotationX = 0;
    float _rotationY = 0;
    private void Update()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _sensitivity;
        _rotationY += Input.GetAxis("Mouse X") * _sensitivity;
        _rotationX = Mathf.Clamp(_rotationX, _minVert, _maxVert);
        _rotationY = Mathf.Clamp(_rotationY, _minHor, _maxHor);

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
    }
}
