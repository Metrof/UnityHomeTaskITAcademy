using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenate : Bullet
{
    [SerializeField] float _explosionRadius = 2;
    [SerializeField] float _explosionPower = 10;
    [SerializeField] float _throwHeight = 0.5f;
    [SerializeField] LayerMask _damagedLayers = Physics.AllLayers;
    public override void Shot(Vector3 weaponForward)
    {
        weaponForward.y += _throwHeight;
        base.Shot(weaponForward);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius, _damagedLayers);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                Vector3 knockbackDirection = collider.gameObject.transform.position - transform.position;
                knockbackDirection.Normalize();
                rigidbody.AddForce(knockbackDirection * _explosionPower, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}
