using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShot : MonoBehaviour
{
    [SerializeField] Transform _bulletParent;

    Stand _bulletType;
    Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _bulletType != null)
        {
            Bullet bullet = _bulletType.GetBullet();
            bullet.Shot(transform.forward, transform.position);
        }
    }
    public void SetBulletType(Stand bulletType, Color bulletColor)
    {
        _bulletType = bulletType;
        _renderer.material.color = bulletColor;
    }
}
