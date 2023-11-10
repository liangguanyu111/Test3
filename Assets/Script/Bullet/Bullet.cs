using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bullet 
{
    public BulletConfig bulletCfg;
    public Action<Bullet> onReturnBullet;
    public Bullet(BulletConfig bulletConfig)
    {
        this.bulletCfg = bulletConfig;
    }

    public virtual void Update()
    {

    }

    public virtual void Reset()
    {

    }

    //远程
    public virtual void DamageTo()
    {

    }
    //近战
    public virtual void DamageTo(Vector3 pos)
    {

    }
}
