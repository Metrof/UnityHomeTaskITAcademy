using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticlesManager : MonoBehaviour
{
    [SerializeField] ExplosionParticles _explosionPref;
    [SerializeField] float _pullLenght = 20;

    private List<ExplosionParticles> _explosionsPull = new List<ExplosionParticles>();
    private int _currentExplosion = 0;

    private void Awake()
    {
        for (int i = 0; i < _pullLenght; i++)
        {
            _explosionsPull.Add(Instantiate(_explosionPref));
            _explosionsPull[i].SetDefoltPosition(transform.position);
            _explosionsPull[i].gameObject.SetActive(false);
        }
    }

    public void GetExplosion(Vector3 explosionPos)
    {
        _currentExplosion++;
        _currentExplosion %= _explosionsPull.Count;
        _explosionsPull[_currentExplosion].ExplosionInPoint(explosionPos);
    }
}
