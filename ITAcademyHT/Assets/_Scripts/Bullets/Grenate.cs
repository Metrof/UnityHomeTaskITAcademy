using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenate : Bullet, IExplosionMaker
{
    [SerializeField] float _explosionRadius = 2;
    [SerializeField] float _explosionPower = 10;
    [SerializeField] float _throwHeight = 0.5f;
    [SerializeField] int _maxCollider = 30;
    [SerializeField] LayerMask _damagedLayers = Physics.AllLayers;

    private ExplosionParticlesManager _explosionParticlesManager;
    private Collider[] _colliders;

    public override void Awake()
    {
        _colliders = new Collider[_maxCollider];
        base.Awake();
    }
    public override void Shot(Vector3 weaponForward, Vector3 shotPos)
    {
        weaponForward.y += _throwHeight;
        base.Shot(weaponForward, shotPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        int numColl = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _colliders, _damagedLayers);
        for (int i = 0; i < numColl; i++)
        {
            if (_colliders[i].gameObject.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                Vector3 knockbackDirection = _colliders[i].gameObject.transform.position - transform.position;
                knockbackDirection.Normalize();
                rigidbody.AddForce(knockbackDirection * _explosionPower, ForceMode.Impulse);
            }
        }
        _explosionParticlesManager.GetExplosion(transform.position);
        Return();
        StopCoroutine(OnBulletReturn());
    }

    public void SetExplosionParticleManager(ExplosionParticlesManager explosionParticles)
    {
        _explosionParticlesManager = explosionParticles;
    }
}
