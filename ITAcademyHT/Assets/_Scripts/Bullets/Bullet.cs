using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _shotsPower = 10;
    [SerializeField] float _bulletLifeTime = 3;

    Rigidbody _rb;
    ProjectileParticles _manager;

    private Vector3 _pullPosition;

    public virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _manager = GetComponent<ProjectileParticles>();
    }
    public virtual void Shot(Vector3 weaponForward, Vector3 shotPos)
    {
        gameObject.SetActive(true);
        transform.position = shotPos;
        _rb.AddForce(weaponForward * _shotsPower, ForceMode.Impulse);
        StartCoroutine(OnBulletReturn());
    }
    public void SetDefoltPos(Vector3 defoltPos)
    {
        _pullPosition = defoltPos;
    }
    private void OnCollisionEnter(Collision collision)
    {
        _manager.Ricochet();
    }
    protected IEnumerator OnBulletReturn()
    {
        yield return new WaitForSeconds(_bulletLifeTime);
        Return();
    }
    protected void Return()
    {
        transform.position = _pullPosition;
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
