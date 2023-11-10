using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//近战没有实体，在触发的地方计算有没有打击到玩家
public class EnemyMeleeBullet : EnemyBullet
{
    public EnemyMeleeBullet(BulletConfig bulletConfig) : base(bulletConfig)
    {


    }

    public override void DamageTo(Vector3 pos)
    {
        Collider2D collider = Physics2D.OverlapCircle(pos, bulletCfg.bulletRadius, LayerMask.GetMask("Hero"));
        if (collider != null)
        {
            Contact heroContact;
            if (collider.gameObject.TryGetComponent<Contact>(out heroContact))
            {
                heroContact.GetDamage(bulletCfg.bulletDamage);
            }
        }
    }
}
