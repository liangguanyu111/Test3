using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//远程子弹有实体
public class EnemyShootBullet : EnemyBullet
{
    public GameObject bulletObj;
    public UnitSpineAnimationHelper spineHelper;
    public EnemyShootBullet(GameObject bulletObj, BulletConfig bulletConfig) : base(bulletConfig)
    {
        this.bulletObj = bulletObj;
        spineHelper = new UnitSpineAnimationHelper(bulletObj);
    }

    public override void Reset()
    {
        bulletObj.SetActive(true);
        spineHelper.PlayAnimation("ammo2", false, RecycleBullet);
        int damageTimer = GameManager._instance.timerManager.AddTimer(DamageTo,0.75f,0.75f);
    }

    public override void DamageTo()
    {
        Collider2D collider = Physics2D.OverlapCircle(bulletObj.transform.position, bulletCfg.bulletRadius,LayerMask.GetMask("Hero"));
        if(collider!=null)
        {
            Contact heroContact;
            if(collider.gameObject.TryGetComponent<Contact>(out heroContact))
            {
                heroContact.GetDamage(bulletCfg.bulletDamage);
            }
        }
    }

    public void RecycleBullet()
    {
        this.bulletObj.SetActive(false);
        this.onReturnBullet?.Invoke(this);
    }
}
