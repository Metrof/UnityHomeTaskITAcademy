using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 200;

    private void Update() => transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.World);
}
