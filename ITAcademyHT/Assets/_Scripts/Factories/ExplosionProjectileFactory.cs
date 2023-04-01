using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileFactory : AbstractBulletFactory
{
    public ExplosionProjectileFactory(Bullet bulletType, Vector3 standPos) : base(bulletType)
    {
        _bulletType = bulletType;
        _bulletType.SetDefoltPos(standPos);
    }
    public override Bullet GetBullet()
    {
        return _bulletType;
    }
}
