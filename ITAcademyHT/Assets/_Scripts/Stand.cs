using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour 
{
    [SerializeField] Bullet _bulletType;
    [SerializeField] int _pullLenght = 20;

    Renderer _renderer;
    ExplosionParticlesManager _explosionParticlesManager;
    private List<Bullet> _bulletsPull = new List<Bullet>();
    private int _currentBullet = 0;

    private AbstractBulletFactory[] _strategies;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        _strategies = new AbstractBulletFactory[2];
        _strategies[0] = new DefoltProjectileFactory(_bulletType, transform.position);
        if(_bulletType is IExplosionMaker)
        {
            _explosionParticlesManager = FindObjectOfType<ExplosionParticlesManager>();
            if(_explosionParticlesManager != null) 
            {
                _strategies[1] = new ExplosionProjectileFactory(_bulletType, transform.position);
                for (int i = 0; i < _pullLenght; i++)
                {
                    _bulletsPull.Add(Instantiate(_strategies[1].GetBullet()));
                    ((IExplosionMaker)_bulletsPull[i]).SetExplosionParticleManager(_explosionParticlesManager);
                    _bulletsPull[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < _pullLenght; i++)
            {
                _bulletsPull.Add(Instantiate(_strategies[0].GetBullet()));
                _bulletsPull[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<RobotShot>(out var robotShot))
        {
            robotShot.SetBulletType(this, _renderer.material.color);
        }
    }

    public Bullet GetBullet()
    {
        _currentBullet++;
        _currentBullet %= _bulletsPull.Count;
        return _bulletsPull[_currentBullet];
    }
}
