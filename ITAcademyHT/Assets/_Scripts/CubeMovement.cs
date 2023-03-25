using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    float _speed = 2;
    Vector3 _moveDir;
    public void SetMoveDirection(Vector3 dir)
    {
        _moveDir = dir;
    }
    private void Update()
    {
        transform.Translate(_moveDir * Time.deltaTime * _speed);
    }
}
