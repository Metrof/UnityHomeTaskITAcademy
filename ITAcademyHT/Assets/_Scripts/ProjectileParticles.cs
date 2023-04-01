using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParticles : MonoBehaviour
{
    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    public void Ricochet()
    {
        _particle.Play();
    }
}
