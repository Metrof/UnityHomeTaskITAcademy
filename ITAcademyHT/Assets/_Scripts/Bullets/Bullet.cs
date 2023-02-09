using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _shotsPower = 10;
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 7);
    }
    public virtual void Shot(Vector3 weaponForward)
    {
        _rb.AddForce(weaponForward * _shotsPower, ForceMode.Impulse);
    }
}
