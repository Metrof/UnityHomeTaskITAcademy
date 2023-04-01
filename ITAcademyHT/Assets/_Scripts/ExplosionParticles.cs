using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticles : MonoBehaviour
{
    private List<ParticleSystem> _particles;
    private Vector3 _defoltPos;
    private float _longerDuration;

    private void Awake()
    {
        _particles = new List<ParticleSystem>
        {
            GetComponent<ParticleSystem>()
        };
        _particles.AddRange(GetComponentsInChildren<ParticleSystem>());
        _longerDuration = _particles[0].main.duration;
        for (int i = 0; i < _particles.Count; i++)
        {
            _longerDuration = _longerDuration < _particles[i].main.duration ? _particles[i].main.duration : _longerDuration;
        }
        gameObject.SetActive(false);
    }

    public void SetDefoltPosition(Vector3 defoltPos)
    {
        _defoltPos = defoltPos;
    }

    public void ExplosionInPoint(Vector3 pos)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        for (int i = 0; i < _particles.Count; i++)
        {
            _particles[i].Play();
        }
        StartCoroutine(OnExplosionDeactivate());
    }

    IEnumerator OnExplosionDeactivate()
    {
        yield return new WaitForSeconds(_longerDuration);
        transform.position = _defoltPos;
        gameObject.SetActive(false);
    }
}
