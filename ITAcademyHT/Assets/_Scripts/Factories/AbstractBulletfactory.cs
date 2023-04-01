using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public abstract class AbstractBulletFactory 
{
    protected Bullet _bulletType;
    public AbstractBulletFactory(Bullet bulletType)
    {
        _bulletType = bulletType;
    }
    public abstract Bullet GetBullet();
}
