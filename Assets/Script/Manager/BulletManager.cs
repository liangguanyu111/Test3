using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class BulletManager
{
    public List<Bullet> currentBullets;

    public Dictionary<int, ObjectPool<Bullet>> bulletDic;

    public BulletConfigRoot bulletConfigRoot;
    public Transform bulletTransform;

    public BulletManager()
    {
        bulletDic = new Dictionary<int, ObjectPool<Bullet>>();
        bulletConfigRoot = new BulletConfigRoot();
        currentBullets = new List<Bullet>();
        bulletTransform = GameObject.Find("Bullets").transform;
    }

    public void Update()
    {
        for (int i = currentBullets.Count-1; i >=0; i--)
        {
            currentBullets[i].Update();
        }
    }

    public Bullet GetBullet(int bulletId)
    {
        if(!bulletDic.ContainsKey(bulletId))
        {
            bulletDic.Add(bulletId, new ObjectPool<Bullet>(() => { return NewBullet(bulletId); }, ResetBullet));
        }
        Bullet newBullet = bulletDic[bulletId].New();
        currentBullets.Add(newBullet);
        newBullet.onReturnBullet += RemoveBullet;
        return newBullet;
    }

    public void RemoveBullet(Bullet bullet)
    {
        currentBullets.Remove(bullet);
        bullet.onReturnBullet -= RemoveBullet;
        bulletDic[bullet.bulletCfg.bulletID].ReturnObj(bullet);
    }
    public Bullet NewBullet(int bulletID)
    {
        BulletConfig bulletConfig = bulletConfigRoot.GetBulletCfg(bulletID);
        GameObject BulletObj=null;
        if (Resources.Load<GameObject>(bulletConfig.bulletName)!=null)
        {
             BulletObj = GameObject.Instantiate(Resources.Load<GameObject>(bulletConfig.bulletName), bulletTransform);
        }    
        var assembly = System.Reflection.Assembly.Load(Assembly.GetExecutingAssembly().GetName());
        Type type = assembly.GetType(bulletConfig.bulletName);
        Bullet newBullet;
        if (BulletObj==null)
        {
            //近战
            newBullet = Activator.CreateInstance(type, bulletConfig) as Bullet;
        }
        else
        {
            //远程
            newBullet = Activator.CreateInstance(type, BulletObj,bulletConfig) as Bullet;
        }
        return newBullet;
    }

    public void ResetBullet(Bullet bullet)
    {
        bullet.Reset();
    }
}
