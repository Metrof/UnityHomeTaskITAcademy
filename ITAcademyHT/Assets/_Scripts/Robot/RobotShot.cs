using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShot : MonoBehaviour
{
    [SerializeField] Transform _bulletParent;

    Bullet _bulletType;
    Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _bulletType != null)
        {
            Bullet bullet = Instantiate(_bulletType, transform.position, Quaternion.identity, _bulletParent);
            bullet.Shot(transform.forward);
        }
    }
    public void SetBulletType(Bullet bullet, Color bulletColor)
    {
        _bulletType = bullet;
        _renderer.material.color = bulletColor;
    }
}
