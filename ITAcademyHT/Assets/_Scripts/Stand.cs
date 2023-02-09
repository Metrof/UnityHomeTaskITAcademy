using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    [SerializeField] Bullet _bulletType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<RobotShot>(out var robotShot))
        {
            robotShot.SetBulletType(_bulletType);
        }
    }
}
