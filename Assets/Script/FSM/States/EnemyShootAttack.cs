using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAttack : EnemyAttack
{
    public EnemyShootAttack(EnemyRange enemy, Action onInit = null, Action onEnter = null, Action onExit = null) : base(enemy, onInit, onEnter, onExit)
    {
        this.m_State = State.EnemyShootAttack;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("ShootAttack");
        Vector3 bulletPos = enemy.Azhai.unitObj.transform.position;
        enemy.spineAniamtionHelper.PlayAnimation("atk1", false, () => 
        { 
           fsm.SetBool("Hold", true);
           EnemyShootBullet bullet =  (enemy as EnemyRange).NewBullet() as EnemyShootBullet;
           bullet.bulletObj.transform.position = bulletPos;
           bullet.Reset();
        });
    }

}
